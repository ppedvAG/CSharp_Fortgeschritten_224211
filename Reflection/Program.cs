using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		Program p = new();
		Type pt = p.GetType(); //Typ holen mit GetType() über Objekt
		Type t = typeof(Program); //Typ holen durch typeof(<Klassenname>)

		object o = Activator.CreateInstance(pt); //Objekt über Typ erstellen

		pt.GetMethods(); //Alle Methoden anschauen
		pt.GetMethod("Test").Invoke(o, null); //Methode über Reflection aufrufen, braucht ein Objekt des entsprechenden Typs
		pt.GetMethod("Test2").Invoke(o, new[] { "Zwei Test" }); //Methode mit Parameter aufrufen

		pt.GetField("Zahl").SetValue(o, 5); //Feld setzen
		Console.WriteLine((int) pt.GetField("Zahl").GetValue(o)); //Wert holen aus Feld

		/////////////////////////////////////////////////

		object o2 = Activator.CreateInstance("Reflection", "Reflection.Program"); //Objekt nur über Strings erstellen

		Assembly assembly = Assembly.GetExecutingAssembly(); //Information über das derzeitige Projekt erhalten

		assembly.GetTypes(); //Alle Typen des Assemblies holen

		assembly.GetType("Program"); //einzelnen Typen finden

		//object o3 = Activator.CreateInstance(assembly.GetType("Program"));

		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_03_13\DelegatesEvents\bin\Debug\net7.0\DelegatesEvents.dll";

		Assembly loaded = Assembly.LoadFrom(pfad); //DLL laden

		Type loadedType = loaded.GetType("DelegatesEvents.Component");

		object comp = Activator.CreateInstance(loadedType);
		loadedType.GetEvent("ProcessCompleted").AddEventHandler(comp, () => Console.WriteLine("Prozess fertig"));
		loadedType.GetEvent("Progress").AddEventHandler(comp, (int i) => Console.WriteLine($"Fortschritt: {i}"));
		loadedType.GetMethod("StartProcess").Invoke(comp, null);
	}

	public int Zahl;

	public void Test()
	{
		Console.WriteLine("Ein Test");
	}

	public void Test2(string x)
	{
		Console.WriteLine(x);
	}
}