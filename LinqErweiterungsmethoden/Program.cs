using System.Diagnostics;
using System.Text;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		#region Einfaches Linq
		List<int> ints = Enumerable.Range(1, 20).ToList();

		Console.WriteLine(ints.Average());
		Console.WriteLine(ints.Min());
		Console.WriteLine(ints.Max());
		Console.WriteLine(ints.Sum());

		Console.WriteLine(ints.First()); //Gibt das erste Element zurück, Exception wenn kein Element existiert oder wenn die Bedingung kein Element findet
		Console.WriteLine(ints.FirstOrDefault()); //Gibt das erste Element zurück, null wenn kein Element gefunden

		Console.WriteLine(ints.Last());
		Console.WriteLine(ints.LastOrDefault());

		Console.WriteLine(ints.First(e => e % 5 == 0)); //Die erste Zahl die durch 5 teilbar ist
		//Console.WriteLine(ints.First(e => e % 50 == 0)); //Exception
		//Console.WriteLine(ints.FirstOrDefault(e => e % 50 == 0)); //0 -> default Wert von int
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Vergleich Linq Schreibweisen
		//Alle BMWs finden
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);

		//Standard-Linq: SQL-ähnliche Schreibweise (alt)
		List<Fahrzeug> bmwsAlt = (from f in fahrzeuge
								  where f.Marke == FahrzeugMarke.BMW
								  select f).ToList();

		//Methodenkette
		List<Fahrzeug> bmwsNeu = fahrzeuge.Where(e => e.Marke == FahrzeugMarke.BMW).ToList();
		#endregion

		#region Linq mit Lambda
		//Alle Fahrzeuge mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200);

		//Alle VWs mit MaxV >= 200
		fahrzeuge.Where(e => e.MaxV >= 200 && e.Marke == FahrzeugMarke.VW);

		//Autos nach Marke sortieren
		fahrzeuge.OrderBy(e => e.Marke); //Originale Liste wird nicht verändert
		fahrzeuge.OrderBy(e => e.Marke).ThenBy(e => e.MaxV); //Sekundäre Sortierung
		fahrzeuge.OrderByDescending(e => e.Marke).ThenByDescending(e => e.MaxV); //Absteigend

		//Alle Marken in der Liste finden
		fahrzeuge.Select(e => e.Marke); //Liste von Marken aus der Fahrzeugliste entnehmen
		fahrzeuge.Select(e => e.Marke).Distinct(); //Marken eindeutig machen

		//Praktisches für Select
		//Ohne Select
		//string[] pfade = Directory.GetFiles("Test"); //Pfad + Dateieendung
		//List<string> p = new();
		//foreach (string s in pfade)
		//	p.Add(Path.GetFileNameWithoutExtension(s));

		////Mit Select
		//Directory.GetFiles("Test").Select(e => Path.GetFileNameWithoutExtension(e)).ToList();

		//Fahren alle Fahrzeuge schneller als 200km/h?
		fahrzeuge.All(e => e.MaxV > 200);

		//Fährt mindestens ein Fahrzeug schneller als 200km/h?
		fahrzeuge.Any(e => e.MaxV > 200);

		fahrzeuge.Any(); //fahrzeuge.Count > 0

		//Wieviele Audis haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.Audi); //4

		//Linq Abfragen vereinfachen
		fahrzeuge.Where(e => e.MaxV > 200).Count();
		fahrzeuge.Count(e => e.MaxV > 200);
		fahrzeuge.Where(e => e.MaxV > 200).First();
		fahrzeuge.First(e => e.MaxV > 200);

		//Wie schnell sind unsere Autos im Durchschnitt?
		fahrzeuge.Select(e => e.MaxV).Average();
		fahrzeuge.Average(e => e.MaxV); //Vereinfachung von oben

		fahrzeuge.Sum(e => e.MaxV);

		fahrzeuge.Min(e => e.MaxV); //Die kleinste Geschwindigkeit
		fahrzeuge.MinBy(e => e.MaxV); //Das Fahrzeug mit der kleinsten Geschwindigkeit

		fahrzeuge.Max(e => e.MaxV); //Die größte Geschwindigkeit
		fahrzeuge.MaxBy(e => e.MaxV); //Das Fahrzeug mit der größten Geschwindigkeit

		//Fahrzeuge in X große Teile aufteilen (Rest kommt in den letzten Teil)
		fahrzeuge.Chunk(5);

		//Überspringe die ersten X Elemente, nimm danach Y Elemente
		fahrzeuge.Skip(2).Take(5);

		fahrzeuge.OrderByDescending(e => e.MaxV).Take(5); //Die schnellsten 5 Fahrzeuge

		//Liste umdrehen
		fahrzeuge.Reverse(); //Funktion von der List
		fahrzeuge.Reverse<Fahrzeug>(); //Funktion von Linq (Generic angeben um Linq Funktion zu verwenden)

		//ID hinzufügen
		fahrzeuge.Zip(Enumerable.Range(0, fahrzeuge.Count));
		Enumerable.Range(0, fahrzeuge.Count).Zip(fahrzeuge);

		//ToDictionary um ein Dictionary zu erzeugen (um damit besser arbeiten zu können)
		Dictionary<int, Fahrzeug> x = Enumerable.Range(0, fahrzeuge.Count).Zip(fahrzeuge).ToDictionary(e => e.First, e => e.Second);

		//Aggregate
		//Output der Collection erzeugen
		Console.WriteLine(fahrzeuge.Aggregate(string.Empty, (agg, fzg) => agg + 
			$"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n"));

		foreach (Fahrzeug f in fahrzeuge)
			Console.WriteLine($"Das Fahrzeug hat die Marke {f.Marke} und kann maximal {f.MaxV}km/h fahren.");

		//Am Ende einer langen Linq Kette eine Ausgabe erzeugen
		fahrzeuge
			.Where(e => e.MaxV > 200)
			.OrderBy(e => e.Marke)
			.Aggregate(string.Empty, (agg, fzg) => agg + $"Das Fahrzeug hat die Marke {fzg.Marke} und kann maximal {fzg.MaxV}km/h fahren.\n");

		//Alle Geschwindigkeiten quadrieren und aufsummieren
		fahrzeuge.Aggregate(0.0, (agg, fzg) => agg + Math.Pow(fzg.MaxV, 2));

		//Fahrzeuge nach Marke gruppieren (Audi-Gruppe, BMW-Gruppe, VW-Gruppe)
		fahrzeuge.GroupBy(e => e.Marke);

		IEnumerable<IGrouping<FahrzeugMarke, Fahrzeug>> y = fahrzeuge.GroupBy(e => e.Marke); //Typ umständlich

		//Besseren Typen erzeugen
		Dictionary<FahrzeugMarke, List<Fahrzeug>> valuePairs = fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.ToList());

		//valuePairs[FahrzeugMarke.Audi] //Einzelne Liste angreifen

		//Die schnellsten Fahrzeuge pro Marke
		fahrzeuge.GroupBy(e => e.Marke).ToDictionary(e => e.Key, e => e.OrderByDescending(e => e.MaxV).First()); //Linq im Value Selector



		//Ohne Linq
		HttpClient client = new();
		string s = Task.Run(() => client.GetStringAsync("http://www.gutenberg.org/files/54700/54700-0.txt")).Result;
		string[] words = s.Split(new char[] { ' ', '\n', ',', '.', ';', ':', '-', '_', '/' }, StringSplitOptions.RemoveEmptyEntries);

		Dictionary<string, int> anzahlen = new();
		foreach (string wort in words)
		{
			if (wort.Length > 6)
			{
				if (!anzahlen.ContainsKey(wort.ToLower()))
					anzahlen.Add(wort.ToLower(), 0);
				anzahlen[wort.ToLower()]++;
			}
		}

		IEnumerable<KeyValuePair<string, int>> top10 = anzahlen.OrderByDescending(e => e.Key).Take(10);

		StringBuilder sb = new StringBuilder();
		sb.AppendLine("Die häufigsten Wörter sind:");
		foreach (var v in top10)
		{
			sb.AppendLine("  " + v);
		}
		Console.WriteLine(sb.ToString());

		//Mit viel Linq aber ohne Aggregate
		IEnumerable<KeyValuePair<string, int>> a = words.Where(e => e.Length > 6)
			.GroupBy(e => e.ToLower())
			.ToDictionary(e => e.Key, e => e.Count())
			.OrderByDescending(e => e.Value)
			.Take(10);

		foreach (KeyValuePair<string, int> kv in a)
			Console.WriteLine(kv);

		//Mit Aggregate
		Console.WriteLine
		(
			words
				.Where(e => e.Length > 6)
				.GroupBy(e => e.ToLower())
				.ToDictionary(e => e.Key, e => e.Count())
				.OrderByDescending(e => e.Value)
				.Take(10)
				.Aggregate(string.Empty, (agg, e) => $"{agg}{e} \n")
		);
		#endregion

		#region Erweiterungsmethoden
		int z = 832597;
		z.Quersumme();
		Console.WriteLine(132759237.Quersumme());

		fahrzeuge.Shuffle();
		anzahlen.Shuffle();
		#endregion
	}
}

[DebuggerDisplay("Marke: {Marke}, Geschwindigkeit: {MaxV} - {typeof(Fahrzeug).FullName}")]
public class Fahrzeug
{
	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }

	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }