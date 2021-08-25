using SqlConnectionTests.Models;
using System;
using System.Collections.Generic;

namespace SqlConnectionTests
{
	public partial class Program
	{
		private static void MainInsertValueList(string[] args)
		{
			var svc = new InsertValueListTest();

			svc.ParameterizedValueList(1000, 0);
		}

		private static void MainGetSql()
		{
			var svc = new AutoBuildInsertList();

			var r = new Random();

			var lst = new List<NumberCollection2>(10);

			for (var i = 0; i < lst.Capacity; i++)
			{
				//lst.Add(new NumberCollection1(r.Next()));
				lst.Add(new NumberCollection2(r.Next(), r.Next()));
			}

			var values = svc.GenerateSql(lst, "dbo.NumberCollection", "NumberCollectionId");

			Console.WriteLine(values.Sql);
			Console.WriteLine();

			svc.ExecuteInsertValueList(values);
		}
	}
}
