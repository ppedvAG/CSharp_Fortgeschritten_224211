namespace Multitasking;

internal class _03_TaskBeenden
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new();
		CancellationToken ct = cts.Token;

		Task t = new Task(Run, ct); //Hier Token direkt übergeben
		t.Start();

		Thread.Sleep(1000);

		cts.Cancel();

		Console.ReadKey();
	}

	static void Run(object o)
	{
		if (o is CancellationToken ct)
		{
			for (int i = 0; i < 100; i++)
			{
				ct.ThrowIfCancellationRequested(); //Task wirft Exception ist aber nicht sichtbar
				Console.WriteLine(i);
				Thread.Sleep(100);
			}
		}
	}
}
