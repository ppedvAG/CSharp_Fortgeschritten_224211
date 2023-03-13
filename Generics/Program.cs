namespace Generics;

internal class Program
{
	static void Main(string[] args)
	{
		List<string> list = new List<string>(); //Generic: T wird nach unten übernommen (hier T = string)
		list.Add("123"); //T wird durch string ersetzt Add(T) -> Add(string)

		List<int> ints = new List<int>(); //T wird durch int ersetzt
		ints.Add(1); //Add(T) -> Add(int)

		Dictionary<string, int> keyValuePairs = new Dictionary<string, int>(); //Klasse mit 2 Generics: TKey -> string, TValue -> int
		keyValuePairs.Add("123", 1);
	}
}

public class DataStore<T>
	: IProgress<T>, //T wird bei Vererbung weitergegeben
	  IEquatable<int> //int statt T als fixer Typ
{
	public T[] data { get; set; } //T bei einem Feld/Property

	public List<T> Data => data.ToList(); //Generic nach unten weitergeben

	public void Add(int index, T item) //T bei Parameter
	{
		data[index] = item;
	}

	public T Get(int index) //T als Rückgabewert
	{
		if (index < 0 || index > data.Length)
			return default(T); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		return data[index];
	}

	public void Report(T value) //T kommt von Interface
	{
		throw new NotImplementedException();
	}

	public bool Equals(int other)
	{
		throw new NotImplementedException();
	}

	public void PrintType<MyType>()
	{
		Console.WriteLine(typeof(MyType)); //Typ Objekt aus dem Generic erzeugen
		Console.WriteLine(default(MyType)); //default(T): Standardwert von T (int: 0, string: null, bool: false, ...)
		Console.WriteLine(nameof(MyType)); //Gibt den Namen des Typs als string aus

		//if (MyType is int) { } //Nicht möglich
		if (typeof(MyType) == typeof(int)) { } //Typvergleiche mit Generics
	}
}

public class DataStore2<T> : DataStore<T>
{

}