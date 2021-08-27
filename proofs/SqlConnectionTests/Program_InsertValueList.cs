using SqlConnectionTests.Models;
using System;
using System.Diagnostics;

namespace SqlConnectionTests
{
	public partial class Program
	{
		private static void MainMultiRowInsert()
		{
			var svc = new AutoBuildInsert();

			var lst = svc.GetNumberCollectionList<NumberCollection10>(10, 10);

			var sw = new Stopwatch();
			sw.Start();

			var values = svc.GenerateSql(lst, "dbo.NumberCollection", "NumberCollectionId");

			sw.Stop();

			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms");
			Console.WriteLine(values.Sql);
			Console.WriteLine();

			svc.ExecuteNonQuery(values);
		}
	}
}
