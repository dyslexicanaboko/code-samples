using System.Threading.Tasks;

namespace SqlConnectionTests
{
	public partial class Program
	{
		private static async Task MainInsertValueList(string[] args)
		{
			var svc = new InsertValueList();

			await svc.ParameterizedValueList(1000, 0);
		}
	}
}
