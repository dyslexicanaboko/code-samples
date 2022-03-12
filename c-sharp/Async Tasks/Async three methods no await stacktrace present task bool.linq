<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public async Task Main()
{
	try
	{
		//Stack trace shows the whole lineage and only one Task is awaited
		await Blah1();
	}
	catch (Exception ex)
	{
		ex.ToString().Dump();
	}
}

public Task<bool> Blah1()
{
	return Blah2();
}

public Task<bool> Blah2()
{
	return Blah3();
}

public Task<bool> Blah3()
{
	var x = 1;
	var y = 0;
	var z = x / y;

	//Nothing to await in this example
	return Task.FromResult(true);
}