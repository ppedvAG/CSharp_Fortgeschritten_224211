namespace DelegatesEvents;

internal class Delegates
{
	public delegate void Vorstellungen(string name); //Definition von Delegate, speichert Referenzen auf Methoden, können zur Laufzeit hinzugefügt oder weggenommen werden

	static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Variablendeklaration + Erstellung mit einer Initialmethode
		v("Max"); //Delegate ausführen

		v += new Vorstellungen(VorstellungEN); //Methode anhängen (lang)
		v += VorstellungEN; //Kurzform
		v("Max"); //Methoden können mehrmals angehängt werden, werden in der Reihenfolge ausgeführt in der sie angehängt wurden

		v -= VorstellungDE; //Methode abnehmen
		v -= VorstellungDE;
		v -= VorstellungDE; //Methode die nicht angehängt ist abnehmen gibt keinen Fehler
		v -= VorstellungDE;
		v("Max");

		v -= VorstellungEN;
		v -= VorstellungEN; //Delegate ist null wenn die letzte Methode abgenommen wird
		v("Max");

		if (v is not null)
			v("Max");

		v?.Invoke("Max"); //Schneller Null-Check: wenn v nicht null ist, führe die Methode aus, sonst überspringe den Rest der Zeile

		v = null; //Delegate entleeren

		foreach (Delegate dg in v.GetInvocationList()) //Delegate durchgehen
		{
			Console.WriteLine(dg.Method.Name);
		}
	}

	static void VorstellungDE(string name)
	{
		Console.WriteLine($"Hallo mein Name ist {name}");
	}

	static void VorstellungEN(string name)
	{
		Console.WriteLine($"Hello my name is {name}");
	}
}