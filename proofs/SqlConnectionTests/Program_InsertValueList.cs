using SqlConnectionTests.Models;
using System;
using System.Diagnostics;

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

			var lst = svc.GetNumberCollection<NumberCollection10>(10, 10);

			var sw = new Stopwatch();
			sw.Start();

			var values = svc.GenerateSql(lst, "dbo.NumberCollection", "NumberCollectionId");

			sw.Stop();

			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms");
			Console.WriteLine(values.Sql);
			Console.WriteLine();

			svc.ExecuteInsertValueList(values);
		}
	}
}
