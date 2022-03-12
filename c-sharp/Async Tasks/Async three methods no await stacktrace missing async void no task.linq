<Query Kind="Program">
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

public async Task Main()
{
	try
	{
		//Exception never reaches this point
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
	Blah3();
	
	return Task.CompletedTask;
}

/* Compiler Warning: CS1998 This async method lacks 'await' 
 * operators and will run synchronously. Consider using the 
 * 'await' operator to await non-blocking API calls, or 'await 
 * Task.Run(...)' to do CPU-bound work on a background thread. */
public async void Blah3()
{
	var x = 1;
	var y = 0;
	//Exception will be thrown here, no stack trace provided because no task is returned here
	var z = x / y;
}