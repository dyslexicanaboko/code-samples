using System;
using System.Threading.Tasks;

namespace SqlConnectionTests
{
	public partial class Program
	{
		static async Task Main(string[] args)
		{
			//await MainConnectionTest(args);
			//MainInsertValueList(args);

            MainGetSql();

			Console.ForegroundColor = ConsoleColor.Magenta;
			Console.WriteLine($"Finished @ {DateTime.Now}");
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.ReadLine();
		}
	}
}
