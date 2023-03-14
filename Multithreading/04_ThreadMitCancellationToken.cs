namespace Multithreading;

internal class _04_ThreadMitCancellationToken
{
	static void Main(string[] args)
	{
		CancellationTokenSource cts = new CancellationTokenSource(); //Source
		CancellationToken ct = cts.Token; //Aus der Source einen Token entnehmen

		ParameterizedThreadStart pt = new(Run);
		Thread t = new Thread(pt);
		t.Start(ct);

		Thread.Sleep(500);

		cts.Cancel(); //Cancel alle Tokens von der Source
	}

	static void Run(object o)
	{
		try
		{
			if (o is CancellationToken ct)
			{
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(25);
					ct.ThrowIfCancellationRequested();
				}
			}
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Thread wurde beendet mit CancellationToken");
		}
	}
}
