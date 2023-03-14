using System.Diagnostics;

namespace AsyncAwait;

internal class Program
{
	static async Task Main(string[] args)
	{
		Stopwatch sw = Stopwatch.StartNew();
		//Toast();
		//Geschirr();
		//Kaffee();
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds); //7s

		//sw.Restart();
		//Task.Run(() =>
		//{
		//	Toast();
		//	Geschirr();
		//	Kaffee();
		//}); //0s, Main Thread läuft weiter
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds);

		//sw.Start();
		//await ToastAsync(); //Methoden werden hier als Tasks gestartet -> Main Thread läuft weiter
		//GeschirrAsync();
		//KaffeeAsync(); //Methoden müssen awaited werden -> nicht möglich, da void
		//sw.Stop();
		//Console.WriteLine(sw.ElapsedMilliseconds);

		sw.Start();
		Task<Toast> toast = ToastTaskAsync(); //Toast Task wird gestartet
		Task<Tasse> tasse = TasseTaskAsync(); //Tasse Task wird gestartet
		Tasse t = await tasse; //Warte hier auf die Tasse
		Task<Kaffee> kaffee = KaffeeTaskAsync(t); //Tasse von vorher direkt weitergeben
		Kaffee k = await kaffee; //Warte auf Kaffee
		Toast t2 = await toast; //Warte auf Toast (Dauer schätzen -> längste Tasks sollten als letztes awaited werden)
		sw.Stop();
		Console.WriteLine(sw.ElapsedMilliseconds);

		//Kurzform von obigen
		Task<Toast> t1 = ToastTaskAsync();
		Task<Tasse> ta1 = TasseTaskAsync();
		Kaffee k1 = await KaffeeTaskAsync(await ta1);
		Toast t3 = await t1;
	}

	public static void Toast()
	{
		Thread.Sleep(4000);
		Console.WriteLine("Toast fertig");
	}

	public static void Geschirr()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	public static void Kaffee()
	{
		Thread.Sleep(1500);
		Console.WriteLine("Kaffee fertig");
	}

	public static async Task ToastAsync()
	{
		await Task.Delay(4000); //Warte hier das 4s vorbei sind
		Console.WriteLine("Toast fertig");
	}

	public static async void GeschirrAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Geschirr hergerrichtet");
	}

	public static async void KaffeeAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
	}

	public static async Task<Toast> ToastTaskAsync()
	{
		await Task.Delay(4000);
		Console.WriteLine("Toast fertig");
		return new Toast();
	}

	public static async Task<Tasse> TasseTaskAsync()
	{
		await Task.Delay(1500);
		Console.WriteLine("Tasse fertig");
		return new Tasse();
	}

	public static async Task<Kaffee> KaffeeTaskAsync(Tasse t)
	{
		await Task.Delay(1500);
		Console.WriteLine("Kaffee fertig");
		return new Kaffee();
	}
}

public class Toast { }

public class Tasse { }

public class Kaffee { }