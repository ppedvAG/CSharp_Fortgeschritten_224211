namespace DelegatesEvents;

internal class Events
{
	//event: Statischer Punkt an den Methoden angehängt werden können
	static event EventHandler TestEvent;

	static event EventHandler<TestEventArgs> ArgsEvent;
		
	static void Main(string[] args)
	{
		TestEvent += Events_TestEvent; //Methode anhängen ohne new, Event kann nicht erstellt werden
		TestEvent(null, EventArgs.Empty); //Event ausführen
		TestEvent += (sender, args) => { }; //Anonymes Event anhängen

		ArgsEvent += Events_ArgsEvent;
		ArgsEvent(null, new TestEventArgs() { Status = "Fertig" });
	}

	private static void Events_ArgsEvent(object? sender, TestEventArgs e)
	{
		Console.WriteLine($"Der Status ist {e.Status}");
	}

	private static void Events_TestEvent(object? sender, EventArgs e)
	{
		Console.WriteLine("TestEvent wurde aufgerufen");
	}
}

internal class TestEventArgs : EventArgs
{
	public string Status { get; set; }
}