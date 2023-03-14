namespace Multitasking;

internal class _02_TaskMitReturn
{
	static void Main(string[] args)
	{
		Task<int> t = Task.Run(Run); //Task.Run nimmt automatisch <int> an
		Console.WriteLine(t.Result); //t.Result blockiert den Main Thread

		for (int i = 0; i < 100; i++)
			Console.WriteLine(i);

		Task t2 = Task.Run(() => Console.WriteLine("Etwas")); //Task mit anonymer Methode/Action

		Task t3 = Task.Run(() => 
		{
			Console.WriteLine("Mehrzeilige");
			Console.WriteLine("anonyme");
			Console.WriteLine("Methode");
		});

		t.Wait(); //Warte hier bis der Task fertig ist
		Task.WaitAll(t, t2, t3); //Warte bis alle Tasks fertig sind
		Task.WaitAny(t, t2, t3); //Warte bis ein beliebiger Task fertig ist
	}

	static int Run()
	{
		int sum = 0;
		for (int i = 0; i < 1000; i++)
		{
			sum += i;
			Console.WriteLine(i);
		}
		return sum;
	}
}
