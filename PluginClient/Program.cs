using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	static void Main(string[] args)
	{
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2023_03_13\CalculatorPlugin\bin\Debug\net7.0\Plugin.dll";
		Assembly loaded = Assembly.LoadFrom(pfad);
		Type t = loaded.GetTypes().First(e => e.GetInterface("IPlugin") != null);
		IPlugin plugin = Activator.CreateInstance(t) as IPlugin;

		Console.WriteLine($"Name: {plugin.Name}");
		Console.WriteLine($"Desc: {plugin.Description}");
		Console.WriteLine($"Version: {plugin.Version}");
		Console.WriteLine($"Autor: {plugin.Author}");

		List<MethodInfo> methods = t.GetMethods().Where(e => e.GetCustomAttribute(typeof(ReflectionVisibleAttribute)) != null).ToList();
		Console.WriteLine("Wähle eine Methode aus");
		for (int i = 0; i < methods.Count; i++)
		{
			Console.WriteLine($"{i}: {methods[i].Name}");
		}

		int auswahl = int.Parse(Console.ReadLine());
		Console.WriteLine(methods[auswahl].Invoke(plugin, new object[] { 2.2, 3.3 }));
	}
}