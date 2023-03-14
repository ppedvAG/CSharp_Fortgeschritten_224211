namespace Multithreading;

internal class _05_Threadpool
{
	static void Main(string[] args)
	{
		ThreadPool.QueueUserWorkItem(Methode1); //Aufgabe starten im Hintergrund
		ThreadPool.QueueUserWorkItem(Methode2);
		ThreadPool.QueueUserWorkItem(Methode3);

		Thread.Sleep(500);
		//Main Thread ist vor Hintegrundthread fertig

		Console.ReadKey();
	}

	static void Methode1(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode1: {i}");
		}
	}

	static void Methode2(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode2: {i}");
		}
	}

	static void Methode3(object o)
	{
		for (int i = 0; i < 100; i++)
		{
			Thread.Sleep(25);
			Console.WriteLine($"Methode3: {i}");
		}
	}
}
