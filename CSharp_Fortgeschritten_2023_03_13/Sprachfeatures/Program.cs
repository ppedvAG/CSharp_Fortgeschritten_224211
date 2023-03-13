namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		Console.WriteLine("Hello, World!");
		
		string format = "lUkas";
		string formatiert = char.ToUpper(format[0]) + format[1..].ToLower();
		int zahl = 5;
		bool b = true;

		//Interpolated String
		string stringPlus = "Der Name ist: " + formatiert + ", die Zahl ist: " + zahl + ", der boolean ist: " + b;
		string interpolated = $"Der Name ist: {formatiert}, die Zahl ist {zahl //Auch Switch Pattern in Interpolated String möglich
			switch 
			{ 
				5 => "Fünf", 
				6 => "Sechs" 
			}}, der boolean ist: {b}";

		//Verbatim String
		string verbatim = @"\n"; //Escape Sequenzen werden nicht mehr erkannt
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_03_13";

		if (pfad is null)
		if (pfad == null)
		if (pfad is not null)
		if (pfad != null)
			Console.WriteLine();
	}

	string HeutigerTag()
	{
		//Person p = new("Max");
		var p = new Person("Max", "Mustermann");

		return p switch
		{
			{ Vorname: "X" } => "",
			{ Vorname: "", Nachname: "" } => "",
			{ Vorname: { Length: 6 } } => "", //Erweitertes Property Pattern
			{ Vorname.Length: 5 } => "" //Mit Punkten statt 20 Klammern
		};

		return p switch
		{
			var (vn, nn) when vn.Length > 7 => "", //hier extra Bedingung mit >, <, ... möglich, oben nicht möglich
			var (vn, nn) when vn.Length > 7 && nn.Length < 5 => ""
		};

		//switch (DateTime.Now.DayOfWeek)
		//{
		//	case > DayOfWeek.Monday and < DayOfWeek.Friday: return "";
		//}
	}

	void Test()
	{
		HeutigerTag();
	}
}

//public class Person
//{
//	public string Vorname { get; set; }

//	public string Nachname { get; set; }

//	public void Deconstruct(out string Vorname, out string Nachname)
//	{
//		Vorname = this.Vorname;
//		Nachname = this.Nachname;
//	}
//}

public record Person(string Vorname, string Nachname);