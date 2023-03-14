using System.Diagnostics;

namespace Multitasking;

internal class ParallelForDemo
{
	static void Main(string[] args)
	{
		int[] durchgänge = { 1000, 10000, 50000, 100_000, 250_000, 500_000, 1_000_000, 5_000_000, 10_000_000, 100_000_000 };
		foreach (int i in durchgänge)
		{
			Stopwatch sw = Stopwatch.StartNew();
			RegularFor(i);
			sw.Stop();
			Console.WriteLine($"For Durchgänge {i}: {sw.ElapsedMilliseconds}");

			Stopwatch sw2 = Stopwatch.StartNew();
			ParallelFor(i);
			sw2.Stop();
			Console.WriteLine($"Parallel For Durchgänge {i}: {sw2.ElapsedMilliseconds}");
		}

		/*
			For Durchgänge 1000: 0
			Parallel For Durchgänge 1000: 23
			For Durchgänge 10000: 2
			Parallel For Durchgänge 10000: 0
			For Durchgänge 50000: 11
			Parallel For Durchgänge 50000: 3
			For Durchgänge 100000: 18
			Parallel For Durchgänge 100000: 5
			For Durchgänge 250000: 41
			Parallel For Durchgänge 250000: 32
			For Durchgänge 500000: 99
			Parallel For Durchgänge 500000: 33
			For Durchgänge 1000000: 150
			Parallel For Durchgänge 1000000: 47
			For Durchgänge 5000000: 1327
			Parallel For Durchgänge 5000000: 827
			For Durchgänge 10000000: 2743
			Parallel For Durchgänge 10000000: 624
			For Durchgänge 100000000: 18698
			Parallel For Durchgänge 100000000: 9966
		 */
	}

	static void RegularFor(int iterations)
	{
		double[] erg = new double[iterations];
		for (int i = 0; i < iterations; i++)
			erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100);
	}

	static void ParallelFor(int iterations)
	{
		double[] erg = new double[iterations];
		//int i = 0; i < iterations; i++
		Parallel.For(0, iterations, i => erg[i] = (Math.Pow(i, 0.333333333333) * Math.Sin(i + 2) / Math.Exp(i) + Math.Log(i + 1)) * Math.Sqrt(i + 100));
	}
}
