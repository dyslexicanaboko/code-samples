using System;
using System.Diagnostics;
using SqlConnectionTests.Models;

namespace SqlConnectionTests
{
	public partial class Program
	{
		private static void MainTargetedUpdate()
		{
			var svc = new AutoBuildUpdate();

			NumberCollection10 model;

			//var model = svc.GetNumberCollectionSingle<NumberCollection10>(10, 7);

			model = new NumberCollection10();
			model.SetNumbers(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);

			model.NumberCollectionId = 1;

			var sw = new Stopwatch();
			sw.Start();

			var values = svc.GenerateSql(model, "dbo.NumberCollection", "NumberCollectionId");

			sw.Stop();

			Console.WriteLine($"Elapsed {sw.ElapsedMilliseconds}ms");
			Console.WriteLine(values.Sql);
			Console.WriteLine();

			svc.ExecuteNonQuery(values);
		}
	}
}
