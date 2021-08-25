using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace SqlConnectionTests
{
	public class InsertValueList
		: BaseDal
	{
		public void ParameterizedValueList(int valueListSize, int delayInMilliseconds = 100)
		{
			var arr = GetDummyValueList(valueListSize, delayInMilliseconds);

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

			using (var con = new SqlConnection(ConnectionString))
			{
				con.Open();

				using (var cmd = new SqlCommand(sql, con))
				{
					cmd.Parameters.AddRange(lst.ToArray());

					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
