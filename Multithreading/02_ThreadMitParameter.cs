namespace Multithreading;

internal class _02_ThreadMitParameter
{
	static void Main(string[] args)
	{
		ParameterizedThreadStart pt = new ParameterizedThreadStart(Run); //Funktionszeiger hier diesmal statt bei Thread Erstellung
		Thread t = new Thread(pt); //pt hier übergeben
		t.Start(200); //Parameter hier übergeben

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main Thread: {i}");
	}

	static void Run(object o) //Nur object möglich
	{
		if (o is int x) //Direkter Cast
		{
			for (int i = 0; i < x; i++)
				Console.WriteLine($"Side Thread: {i}");
		}
	}
}
