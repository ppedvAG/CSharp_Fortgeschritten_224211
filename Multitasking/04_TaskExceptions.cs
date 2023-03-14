namespace Multitasking;

internal class _04_TaskExceptions
{
	static void Main(string[] args)
	{
		try
		{
			Task t1, t2, t3;
			t1 = Task.Run(Exception1);
			t2 = Task.Run(Exception2);
			t3 = Task.Run(Exception3);

			Task.WaitAll(t1, t2, t3);
		}
		catch (AggregateException e)
		{
			foreach (Exception x in e.InnerExceptions) //Alle gesammelten Exceptions durchgehen
				Console.WriteLine(x.Message);
		}
	}

	static void Exception1()
	{
		throw new DivideByZeroException();
	}

	static void Exception2()
	{
		throw new StackOverflowException();
	}

	static void Exception3()
	{
		throw new OutOfMemoryException();
	}
}
