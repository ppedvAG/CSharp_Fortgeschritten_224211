using System.Collections;

namespace Sonstiges;

internal class Program
{
	static void Main(string[] args)
	{
		Wagon w1 = new() { AnzSitze = 10, Farbe = "Rot" };
		Wagon w2 = new() { AnzSitze = 10, Farbe = "Rot" };
		Console.WriteLine(w1 == w2);

		Zug z = new();
		z += w1;
		z += w2;
		z++;
		z++;
		z++;
		z++;

		foreach (Wagon w in z)
		{
			Console.WriteLine(w.Farbe);
		}

		Console.WriteLine(z[1]);
		//z[1] = new Wagon();
		Console.WriteLine(z["Rot"]);
		Console.WriteLine(z[10, "Rot"]);

		var x = z.Wagons.Select(e => new { e.AnzSitze, HashCode = e.GetHashCode() }).First();
		Console.WriteLine(x.HashCode);

		System.Timers.Timer timer = new();
		timer.Interval = 1000;
		timer.Elapsed += Timer_Elapsed;
		timer.Start();

		Console.ReadKey();
	}

	private static void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
	{
		Console.WriteLine(e.SignalTime);
	}
}

public class Zug : IEnumerable
{
	public List<Wagon> Wagons { get; set; } = new();

	public IEnumerator GetEnumerator()
	{
		return Wagons.GetEnumerator();
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public Wagon this[int i]
	{
		get => Wagons[i];
	}

	public Wagon this[string farbe]
	{
		get => Wagons.First(e => e.Farbe == farbe);
	}

	public Wagon this[int anzSitze, string farbe]
	{
		get => Wagons.First(e => anzSitze == e.AnzSitze && e.Farbe == farbe);
	}
}

public class Wagon
{
	public int AnzSitze { get; set; }

	public string Farbe { get; set; }

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		return !(a == b);
	}
}