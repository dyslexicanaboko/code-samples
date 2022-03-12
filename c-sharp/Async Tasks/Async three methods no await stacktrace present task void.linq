<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public async Task Main()
{
	try
	{
		//Stack trace is going to show the whole lineage and only one Task is awaited
		await Blah1();
	}
	catch (Exception ex)
	{
		ex.ToString().Dump();
	}
}

public Task Blah1()
{
	return Blah2();
}

public Task Blah2()
{
	return Blah3();
}

public Task Blah3()
{
	var x = 1;
	var y = 0;
	var z = x / y;
	
	return Task.CompletedTask;
}