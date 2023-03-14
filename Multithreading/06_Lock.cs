namespace Multithreading;

internal class _06_Lock
{
	static int Zahl = 0;

	static object Lock = new();

	static void Main(string[] args)
	{
		for (int i = 0; i < 1000; i++)
			new Thread(ZahlPlusPlus).Start();
	}

	static void ZahlPlusPlus()
	{
		for (int i = 0; i < 1000; i++)
		{
			lock (Lock) //Zahl sperren damit nicht mehrere Threads gleichzeitig draufgreifen können
			{
				Zahl++;
			} //Lock wird geöffnet für einen anderen Thread

			Monitor.Enter(Lock); //Monitor und Lock haben 1:1 den selben Code
			Zahl++;
			Monitor.Exit(Lock);
		}
	}
}
