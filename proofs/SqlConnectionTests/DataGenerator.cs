using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace SqlConnectionTests
{
	public class DataGenerator
	{
		private readonly int _insertLoops;
		private readonly string _connectionString;
		
		public ConcurrentDictionary<string, int> ErrorsOnOpen { get; private set; }

		public DataGenerator(int insertLoops)
		{
			_insertLoops = insertLoops;

			_connectionString = LoadConnectionString();

			ErrorsOnOpen = new ConcurrentDictionary<string, int>();
		}

		/// <summary>
		/// Awaiting a list of tasks has less of a chance of throwing a InvalidOperationException.
		/// The downside is this will open up a new connection for every waiting task. The connections
		/// are less likely to be shared.
		/// </summary>
		/// <param name="taskIndex"></param>
		/// <returns></returns>
		public async Task InsertStatementAwaitEachIndividualExecution(int taskIndex)
		{
			Console.WriteLine($"Task {taskIndex} started");

			//This is written poorly on purpose to cause intentional drag
			for (var i = 0; i < _insertLoops; i++)
			{
				var dtm = DateTime.UtcNow;

				//dt.Dump();
				using (var con = new SqlConnection(_connectionString))
				{
					OpenConnection(con);

					var sql = GetQuery();

					using (var cmd = new SqlCommand(sql, con))
					{
						//Since the execution is being waited on, it causes each tandem task to acquire its own connection
						//This can be seen clearly in the SSMS activity monitor or querying for processes
						await cmd.ExecuteNonQueryAsync();
					}
				}
			}

			Console.WriteLine($"Task {taskIndex} finished");
		}

		/// <summary>
		/// Awaiting a list of tasks has less of a chance of throwing a InvalidOperationException.
		/// </summary>
		/// <param name="taskIndex"></param>
		/// <returns></returns>
		public async Task InsertStatementAwaitAllExecutions(int taskIndex)
		{
			Console.WriteLine($"Task {taskIndex} started");

			var lst = new List<Task>(_insertLoops);

			//This is written poorly on purpose to cause intentional drag
			for (var i = 0; i < _insertLoops; i++)
			{
				var dtm = DateTime.UtcNow;

				//dt.Dump();
				using (var con = new SqlConnection(_connectionString))
				{
					OpenConnection(con);

					var sql = GetQuery();

					using (var cmd = new SqlCommand(sql, con))
					{
						//Don't wait for execution to finish, just grab the task and continue
						lst.Add(cmd.ExecuteNonQueryAsync());
					}
				}
			}

			//Wait for all executions in this Task to be finished
			await Task.WhenAll(lst);

			Console.WriteLine($"Task {taskIndex} finished");
		}

		/// <summary>
		/// This has more of a chance to cause an InvalidOperationException because the execution is being
		/// fired and the result is being ignored (forgotten).
		/// </summary>
		/// <param name="taskIndex"></param>
		/// <returns></returns>
		public Task InsertStatementFireAndForget(int taskIndex)
		{
			Console.WriteLine($"Task {taskIndex} fired");

			//This is written poorly on purpose to cause intentional drag
			for (var i = 0; i < _insertLoops; i++)
			{
				var dtm = DateTime.UtcNow;

				//dt.Dump();
				using (var con = new SqlConnection(_connectionString))
				{
					OpenConnection(con);

					var sql = GetQuery();

					using (var cmd = new SqlCommand(sql, con))
					{
						//Don't wait for anything, fire and forget
						_ = cmd.ExecuteNonQueryAsync();
					}
				}
			}

			Console.WriteLine($"Task {taskIndex} forgotten");

			return Task.CompletedTask;
		}

		private void OpenConnection(SqlConnection connection)
		{
			try
			{
				connection.Open();
			}
			catch (Exception ex)
			{
				ErrorsOnOpen.AddOrUpdate(ex.Message, 1, (k, v) => ++v);

				//Trapping the exception because I don't want to kill the execution, but this is not ideal
			}
		}

		private string GetQuery()
		{
			var dtm = DateTime.UtcNow;

			var sql =
				$"INSERT INTO dbo.BulkCopyTest(CreatedOnUtc) VALUES ('{dtm:yyyy-MM-dd HH:mm:ss.fffffff}');";

			return sql;
		}

		public async Task BulkCopy()
		{
			//This is written poorly on purpose to cause intentional drag
			for (int i = 0; i < _insertLoops; i++)
			{
				var dt = GetDataTable();

				//dt.Dump();
				using (var con = new SqlConnection(_connectionString))
				{
					con.Open();

					using (var bulkCopy = new SqlBulkCopy(con, SqlBulkCopyOptions.CheckConstraints, null))
					{
						bulkCopy.DestinationTableName = "dbo.BulkCopyTest";
						bulkCopy.BatchSize = 5000;
						bulkCopy.BulkCopyTimeout = 120;
						await bulkCopy.WriteToServerAsync(dt);
						//bulkCopy.WriteToServer(dt);
						bulkCopy.Close();
					}
				}
			}
		}

		private DataTable GetDataTable()
		{
			var pk = new DataColumn();
			pk.AllowDBNull = false;
			pk.AutoIncrement = true;
			pk.ColumnName = "BulkCopyTestId";
			pk.DataType = typeof(int);

			var dc = new DataColumn();
			dc.AllowDBNull = false;
			dc.ColumnName = "CreatedOnUtc";
			dc.DataType = typeof(DateTime);

			var dt = new DataTable("Table1");
			dt.Columns.Add(pk);
			dt.Columns.Add(dc);

			var dr = dt.NewRow();
			dr["BulkCopyTestId"] = 0;
			dr["CreatedOnUtc"] = DateTime.UtcNow;

			dt.Rows.Add(dr);

			return dt;
		}

		private static string LoadConnectionString()
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			var configuration = builder.Build();

			var connectionString = configuration.GetConnectionString("ScratchSpace");

			return connectionString;
		}
	}
}
