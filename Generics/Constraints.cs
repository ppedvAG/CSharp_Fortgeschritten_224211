namespace Generics;

internal class Constraints
{
	static void Main(string[] args)
	{

	}

	public class DataStore1<T> where T : class { } //T muss ein Referenztyp sein (string, verschiedene Objekte, ...)

	public class DataStore2<T> where T : struct { } //T muss ein Wertetyp sein (int, bool, ...)

	public class DataStore3<T> where T : Program { } //T muss Program sein oder eine Unterklasse von Program

	public class DataStore4<T> where T : new() { } //T muss einen leeren Standardkonstruktor haben

	public class DataStore5<T> where T : Enum { } //T muss ein Enum sein (keine Enumwerte)

	public class DataStore6<T> where T : Delegate { } //T muss ein Delegate sein

	public class DataStore7<T> where T : unmanaged { }
	//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/unmanaged-types

	public class DataStore8<T> where T : class, Enum, new() { } //Mehrere Constraints auf ein Generic

	public class DataStore9<T1, T2> //Mehrere Constraints auf mehrere Generics machen
		where T1 : class
		where T2 : struct
	{ }

	public void Test<MyType>() where MyType : struct //Constraints bei Methode hinzufügen
	{

	}
}
