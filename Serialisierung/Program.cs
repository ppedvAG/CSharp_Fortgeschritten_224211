using Microsoft.VisualBasic.FileIO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using System.Xml;
using System.Xml.Serialization;

namespace Serialisierung;

internal class Program
{
	static void Main(string[] args)
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//NewtonsoftJson();

		SystemTextJson();

		//XML();

		//Binary();

		//CSV();
	}

	static void NewtonsoftJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//JsonSerializerSettings settings = new JsonSerializerSettings(); //Einstellungen für die Serialisierung/Deserialisierung
		//settings.Formatting = Formatting.Indented; //Schönes Json generieren
		//settings.TypeNameHandling = TypeNameHandling.Objects; //Vererbungshiearchien beachten und speichern/laden

		//string json = JsonConvert.SerializeObject(fahrzeuge, settings); //JsonConvert: Klasse zum Erstellen von Json Strings und konvertieren von Json Strings zu Objekten
		//File.WriteAllText(filePath, json);

		//string readJson = File.ReadAllText(filePath);
		//Fahrzeug[] readFzg = JsonConvert.DeserializeObject<Fahrzeug[]>(readJson, settings);

		///////////////////////////////////////

		//JToken document = JToken.Parse(readJson); //Json zu einem Json Document umwandeln um es Stück für Stück durchzugehen
		//foreach (JToken jt in document)
		//{
		//	Console.WriteLine(jt["MaxV"].Value<int>()); //Mit [] auf ein Feld zugreifen, mit Value<T> in einen Typen konvertieren

		//	Fahrzeug fzg = JsonConvert.DeserializeObject<Fahrzeug>(jt.ToString());
		//}
	}

	static void SystemTextJson()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new PKW(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Schiff(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		JsonSerializerOptions options = new();
		options.WriteIndented = true;
		JsonTypeInfo info = JsonTypeInfo.CreateJsonTypeInfo<Fahrzeug>(options);
		options.TypeInfoResolver = resolver;

		string json = JsonSerializer.Serialize(fahrzeuge);
		File.WriteAllText(filePath, json);

		string readJson = File.ReadAllText(filePath);
		Fahrzeug[] readFzg = JsonSerializer.Deserialize<Fahrzeug[]>(readJson);

		////////////////////////////////////

		JsonDocument doc = JsonDocument.Parse(readJson);
		foreach (JsonElement je in doc.RootElement.EnumerateArray())
		{
			Console.WriteLine(je.GetProperty("MaxV").GetInt32());

			Fahrzeug f = je.Deserialize<Fahrzeug>(); //Kann direkt in ein Objekt konvertieren
			Console.WriteLine(f.ID);
		}
	}

	static void XML()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		//NewtonsoftJson();

		//SystemTextJson();

		XmlSerializer xml = new XmlSerializer(fahrzeuge.GetType()); //XmlSerializer muss erstellt werden und braucht einen Typen
		using (FileStream fs = new(filePath, FileMode.Create)) //Using-Block: wird am Ende des Blocks geschlossen
		{
			xml.Serialize(fs, fahrzeuge);
		}

		using FileStream readStream = new(filePath, FileMode.Open); //Using-Statement: bleibt bis zum Ende der Methode offen
		List<Fahrzeug> readFzg = xml.Deserialize(readStream) as List<Fahrzeug>;

		////////////////////////////////////

		XmlDocument doc = new XmlDocument();
		doc.LoadXml(File.ReadAllText(filePath));
		foreach (XmlNode node in doc.ChildNodes[1])
		{
			Console.WriteLine(node.ChildNodes.OfType<XmlNode>().First(e => e.Name == "MaxV").InnerText); //Einzelnen Wert von Feld holen
			Console.WriteLine(node.Attributes["MaxV"].Value); //Einzelne Attribute auslesen
		}
	}

	static void Binary()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		List<Fahrzeug> fahrzeuge = new()
		{
			new Fahrzeug(0, 251, FahrzeugMarke.BMW),
			new Fahrzeug(1, 274, FahrzeugMarke.BMW),
			new Fahrzeug(2, 146, FahrzeugMarke.BMW),
			new Fahrzeug(3, 208, FahrzeugMarke.Audi),
			new Fahrzeug(4, 189, FahrzeugMarke.Audi),
			new Fahrzeug(5, 133, FahrzeugMarke.VW),
			new Fahrzeug(6, 253, FahrzeugMarke.VW),
			new Fahrzeug(7, 304, FahrzeugMarke.BMW),
			new Fahrzeug(8, 151, FahrzeugMarke.VW),
			new Fahrzeug(9, 250, FahrzeugMarke.VW),
			new Fahrzeug(10, 217, FahrzeugMarke.Audi),
			new Fahrzeug(11, 125, FahrzeugMarke.Audi)
		};

		BinaryFormatter formatter = new();
		using (FileStream fs = new FileStream(filePath, FileMode.Create))
		{
			formatter.Serialize(fs, fahrzeuge);
		}

		using FileStream readStream = new(filePath, FileMode.Open);
		List<Fahrzeug> readFzg = formatter.Deserialize(readStream) as List<Fahrzeug>;
	}

	static void CSV()
	{
		string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory); //Pfad zum Desktop
		string folderPath = Path.Combine(desktop, "Test"); //Pfad zum Ordner
		string filePath = Path.Combine(folderPath, "Test.txt"); //Pfad zum File

		Directory.CreateDirectory(folderPath);

		using TextFieldParser tfp = new(filePath);
		tfp.SetDelimiters(";");
		//tfp.ReadFields(); //Header einlesen falls vorhanden
		while (!tfp.EndOfData)
		{
			string[] fields = tfp.ReadFields();
			//Fahrzeug f = new Fahrzeug(int.Parse(fields[0]), int.Parse(fields[1]), (FahrzeugMarke) int.Parse(fields[2])); 
		}
	}
}

[Serializable]
public class Fahrzeug
{
	//[XmlIgnore]
	//[XmlAttribute] //Feld als Attribut speichern
	//[XmlElement("Identifier")]
	public int ID { get; set; }

	//[field: NonSerialized] //"BinaryIgnore"
	public int MaxV { get; set; }

	//[JsonIgnore] //Feld ignorieren (beide Frameworks)
	//[JsonPropertyName("FM")] //Feld umbenennen (System.Text.Json)
	//[JsonProperty("FM")] //Feld umbenennen (Newtonsoft.Json)
	public FahrzeugMarke Marke { get; set; }

	public int AnzReifen; //Ohne Property wird dieses Feld nicht serialisiert (in Json)

	public Fahrzeug(int iD, int maxV, FahrzeugMarke marke)
	{
		ID = iD;
		MaxV = maxV;
		Marke = marke;
	}

	public Fahrzeug()
	{
		//Für XML
	}
}

public class PKW : Fahrzeug
{
	public PKW(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}
}

public class Schiff : Fahrzeug
{
	public Schiff(int iD, int maxV, FahrzeugMarke marke) : base(iD, maxV, marke)
	{
	}
}

public enum FahrzeugMarke { Audi, BMW, VW }