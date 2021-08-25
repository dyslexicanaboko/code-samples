using System.Threading.Tasks;

namespace SqlConnectionTests
{
	public partial class Program
	{
		private static void MainInsertValueList(string[] args)
		{
			var svc = new InsertValueList();

			svc.ParameterizedValueList(1000, 0);
		}
	}
}
