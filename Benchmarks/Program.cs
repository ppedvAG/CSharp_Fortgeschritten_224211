using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System.Text;

namespace Benchmarks;

public class Program
{
	static void Main(string[] args)
	{
		BenchmarkRunner.Run<Benchmarks>();
		//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());
		//Benchmarks debuggen
	}
}

[MemoryDiagnoser(false)]
//[RankColumn]
//[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
public class Benchmarks
{
	public List<Fahrzeug> Fahrzeuge;

	[Params(5000, 10000, 25000)]
	public int Anzahl;

	[GlobalSetup]
	public void Setup()
	{
		Fahrzeuge = new();
		Random rnd = new Random();
		for (int i = 0; i < Anzahl; i++)
		{
			Fahrzeug f = new(i, rnd.Next(100, 500), (FahrzeugMarke) rnd.Next(0, 3));
			Fahrzeuge.Add(f);
		}
		//for (int i = 0; i < 1000; i++)
		//	File.Create($@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_01_02\Benchmarks\bin\Release\net6.0\Test\{i}.txt");
	}

	[GlobalCleanup]
	public void Cleanup()
	{

	}

	//[Benchmark]
	[IterationCount(30)]
	public void TestStringPlus()
	{
		string str = "";
		for (int i = 0; i < 10000; i++)
			str += i;
	}

	//[Benchmark]
	[IterationCount(30)]
	public void TestStringBuilder()
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < 10000; i++)
			sb.Append(i);
		string str = sb.ToString();
	}

	//[Benchmark]
	[IterationCount(30)]
	public void TestOhneSelect()
	{
		string[] pfade = Directory.GetFiles("C:\\Users\\lk3\\source\\repos\\CSharp_Fortgeschritten_2023_01_02\\Benchmarks\\bin\\Release\\net6.0\\Test");
		List<string> p = new();
		foreach (string s in pfade)
			p.Add(Path.GetFileNameWithoutExtension(s));
	}

	//[Benchmark]
	[IterationCount(30)]
	public void TestMitSelect()
	{
		Directory.GetFiles(@"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_01_02\Benchmarks\bin\Release\net6.0\Test").Select(e => Path.GetFileNameWithoutExtension(e)).ToList();
	}

	[Benchmark]
	[IterationCount(30)]
	public void ForEach()
	{
		List<Fahrzeug> bmwsForEach = new();
		foreach (Fahrzeug f in Fahrzeuge)
			if (f.Marke == FahrzeugMarke.BMW)
				bmwsForEach.Add(f);
	}

	[Benchmark]
	[IterationCount(30)]
	public void LinqSQL()
	{
		List<Fahrzeug> bmws = (from fzg in Fahrzeuge
							   where fzg.Marke == FahrzeugMarke.BMW
							   select fzg).ToList();
	}

	[Benchmark]
	[IterationCount(30)]
	public void Linq()
	{
		List<Fahrzeug> bmwsNeu = Fahrzeuge.Where(fzg => fzg.Marke == FahrzeugMarke.BMW).ToList();
	}
}

public class Fahrzeug
{
	public int ID;
	public int MaxGeschwindigkeit;
	public FahrzeugMarke Marke;
	public List<Sitzplatz> Sitze;

	public Fahrzeug(int id, int v, FahrzeugMarke fm)
	{
		ID = id;
		MaxGeschwindigkeit = v;
		Marke = fm;
		Sitze = new();

		//Anzahl Sitzplätze anhand der Geschwindigkeit (6: max 150km/h, 5 bis 250km/h, 4 ab 250km/h)
		int sitze = v <= 150 ? 6 : v <= 250 ? 5 : 4;

		//Sitzplätze erstellen
		for (int i = 0; i < sitze; i++)
			Sitze.Add(new Sitzplatz());

		//Sitzplätze semi-zufällig belegen damit die Übung zwischen Teilnehmern gleiche Ergebnisse liefert
		//Geschwindigkeit modulo Anzahl Sitzplätze besetzen
		for (int i = 0; i < v % (sitze + 1); i++)
			Sitze[i].IstBesetzt = true;
	}
}

public class Sitzplatz
{
	public bool IstBesetzt;
}

public enum FahrzeugMarke
{
	Audi, BMW, VW
}