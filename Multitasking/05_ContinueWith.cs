namespace Multitasking;

internal class _05_ContinueWith
{
	static void Main(string[] args)
	{
		Task<double> t1 = Task.Run(() =>
		{
			Thread.Sleep(1000);
			//throw new Exception(); Zweiter Task wird übersprungen
			return Math.Pow(4, 30);
		});
		t1.ContinueWith(vorherigerTask => Console.WriteLine(vorherigerTask.Result));
		//Tasks verketten, Code wird ausgeführt wenn der vorherige Task fertig ist
		//Verhindert Blockieren des Main Threads
		//Ergebnis des vorherigen Tasks kann verwendet werden
		t1.ContinueWith(t => Console.WriteLine(t.Result * 2), TaskContinuationOptions.NotOnFaulted);


		//for (int i = 0; ; i++)
		//	Console.WriteLine(i);

		Console.ReadKey();
	}
}
