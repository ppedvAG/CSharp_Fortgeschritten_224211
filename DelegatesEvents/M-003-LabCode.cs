public class Program
{
	static void Main(string[] args)
	{
		//Eigenen Code hier schreiben
	}

	public void Addition(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} + {zahl2} = {zahl1 + zahl2}");
	}

	public void Subtraktion(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} - {zahl2} = {zahl1 - zahl2}");
	}

	public void Multiplikation(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} * {zahl2} = {zahl1 * zahl2}");
	}
}

public class DivisionsCalculator
{
	public void Division(double zahl1, double zahl2)
	{
		Console.WriteLine($"{zahl1} : {zahl2} = {zahl1 /  zahl2}");
	}
}

// public bool CheckPrime(int num)
// {
// if (num % 2 == 0)
	//{
		// return false;
	//}
	
	// for (int i = 3; i <= num / 2; i += 2)
	// {
		// if (num % i == 0)
		// {
			// return false;
		// }
	// }
	// return true;
// }