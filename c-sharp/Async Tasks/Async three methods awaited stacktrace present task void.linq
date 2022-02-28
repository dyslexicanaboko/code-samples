<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public async Task Main()
{
	try
	{
		//Stack trace is going to show the whole lineage because every step of the way is awaited for
		await Blah1();
	}
	catch (Exception ex)
	{
		ex.ToString().Dump();
	}
}

public async Task Blah1()
{
	await Blah2();
}

public async Task Blah2()
{
	await Blah3();
}

public Task Blah3()
{
	var x = 1;
	var y = 0;
	var z = x / y;
	
	return Task.CompletedTask;
}