using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SqlConnectionTests
{
    public class InsertValueList
		: BaseDal
	{
		public async Task<DateTime[]> GetDummyValueList(int valueListSize, int delayInMilliseconds)
		{
			var arr = new DateTime[valueListSize];

			for (var i = 0; i < arr.Length; i++)
			{
				if(delayInMilliseconds > 0) await Task.Delay(delayInMilliseconds);

				arr[i] = DateTime.UtcNow;
			}

			return arr;
		}

		public async Task ParameterizedValueList(int valueListSize, int delayInMilliseconds = 100)
		{
			var arr = await GetDummyValueList(valueListSize, delayInMilliseconds);

			ParameterizedValueList(arr);
		}

		public void ParameterizedValueList(params DateTime[] valueList)
		{
			var statement = "INSERT INTO dbo.BulkCopyTest(CreatedOnUtc) VALUES ";
			
			var lstValues = new List<string>(valueList.Length);

			var lst = new List<SqlParameter>(valueList.Length);

			for (var i = 0; i < valueList.Length; i++)
			{
				var sqlVariable = $"@dtm{i}";

				var p = new SqlParameter($"@dtm{i}", SqlDbType.DateTime2);
				p.Value = valueList[i];

				lstValues.Add($"({sqlVariable})");

				lst.Add(p);
			}

			var sql = statement + string.Join(",", lstValues);

			Console.WriteLine(sql);

			using (var con = new SqlConnection(_connectionString))
			{
				con.Open();

				using (var cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.AddRange(lst.ToArray());

					cmd.ExecuteNonQuery();
				}
			}
		}

        public void InsertViaDbCommandBuilder(params DateTime[] valueList)
        {
            var cb = new SqlDataAdapter();


           
        }
	}
}
