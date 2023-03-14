namespace Multithreading;

internal class _03_ThreadBeenden
{
	static void Main(string[] args)
	{
		try
		{
			Thread t = new Thread(Run);
			t.Start();

			Thread.Sleep(500);

			//t.Abort();
			t.Interrupt(); //Thread beenden mit Interrupt statt Abort, wirft ThreadInterruptedException
		}
		catch (ThreadInterruptedException)
		{
			//Exception kann nicht oben gefangen werden
		}
	}

	static void Run()
	{
		try
		{
			Thread.Sleep(1000);
		}
		catch (ThreadInterruptedException)
		{
			Console.WriteLine("Thread beendet");
		}
	}
}
