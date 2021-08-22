using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SqlConnectionTests
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			//90 connections will be opened and they will stay open for 4-8 minutes or
			//until this executable is terminated which ever comes first.

			//100 connections will be opened which is the default pool max
			//The pool is allotted to the executable's instance.

			//200 connections will attempt to be opened which is greater than the default
			//pool max and will throw an System.InvalidOperationException
			if (args.Length == 0)
			{
				Console.WriteLine("Provide the number of connections you want to start.");
				
				return;
			}

			if (!int.TryParse(args[0], out var numberOfConnections))
			{
				Console.WriteLine("Argument must be an integer.");

				return;
			}

			try
			{
				var p = await OpenConnections(numberOfConnections);

				if (p.ErrorsOnOpen.Count > 0)
				{
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine($"Total Errors: {p.ErrorsOnOpen.Values.Sum()}");

					var i = 1;

					Console.ForegroundColor = ConsoleColor.Cyan;

					foreach (var (key, value) in p.ErrorsOnOpen)
					{
						Console.WriteLine($"Distinct error number: {i}\n");
						Console.WriteLine($"Error Message:\n{key}\n");
						Console.WriteLine($"Occurrences: {value}\n\n");

						i++;
					}
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unexpected Exception");
				Console.WriteLine(ex.Message);
			}

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine($"Finished @ {DateTime.Now}");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.ReadLine();
		}

		private static async Task<DataGenerator> OpenConnections(int numberOfConnections)
		{
			if(numberOfConnections > 100)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("WARNING: The default connection pool max size is 100. An exception is more than likely going to be thrown.");
				Console.ForegroundColor = ConsoleColor.Gray;
			}

			var p = new DataGenerator(100);

			var lst = new List<Task>(numberOfConnections);

			Parallel.For(1, numberOfConnections + 1, 
				i => lst.Add(
					//p.InsertStatementAwaitEachIndividualExecution(i) //All 100 connections opened simultaneously, but execution is serial
					//p.InsertStatementAwaitAllExecutions(i) //All executions are started and awaited for all to finish
					p.InsertStatementFireAndForget(i) //All executions are started and not waited for
				));

			try
			{
				await Task.WhenAll(lst);
			}
			catch (InvalidOperationException)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine("Expected exceptions were caught and logged.");
			}
			
			return p;
		}
	}
}
