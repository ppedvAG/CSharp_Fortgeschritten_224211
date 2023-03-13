namespace DelegatesEvents;

internal class ActionPredicateFunc
{
	static void Main(string[] args)
	{
		Action<int, int> action = Addiere; //Action: Methode mit void und bis zu 16 Parametern
		action += Subtrahiere;
		action(6, 2);
		action?.Invoke(4, 2);

		DoAction(6, 2, Addiere); //Das Verhalten der Methode anpassen über den Action Parameter
		DoAction(6, 2, Subtrahiere);
		DoAction(6, 2, action);

		Predicate<int> pred = CheckForZero; //Predicate: Methode mit bool als Rückgabewert und genau einem Parameter
		pred += CheckForOne;
		bool b = pred(1); //Ergebnis der letzten Methode wird genommen
		bool? b2 = pred?.Invoke(1); //Drei mögliche Ergebnisse: true, false oder null wenn das Predicate null ist
									//bool?: Nullable Boolean, funktioniert bei allen Typen z.B.: int?, double?, Enum?, ...
		bool b3 = pred?.Invoke(1) == true; //false oder null => false, sonst true

		DoPredicate(1, CheckForZero); //Das Verhalten der Methode anpassen über den Predicate Parameter
		DoPredicate(1, CheckForOne);
		DoPredicate(1, pred);

		Func<int, int, double> func = Multipliziere; //Func: Methode mit Rückgabewert (letztes Generic ist der Rückgabetyp) und bis zu 16 Parametern
		func += Dividiere;
		double d = func(5, 2); //Das letzte Ergebnis wie bei Predicate
		double? d2 = func?.Invoke(5, 2);

		DoFunc(5, 2, Multipliziere);
		DoFunc(5, 2, Dividiere);
		DoFunc(5, 2, func);

		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		DoAction(6, 2, (x, y) => Console.WriteLine(x + y)); //Hier kein Rückgabewert möglich -> CW
		DoPredicate(5, (x) => x % 2 == 0); //Ist die Zahl durch 2 teilbar (gerade)?
		DoFunc(6, 2, (x, y) => x % y); //Anonyme Methode die einen double als Ergebnis geben muss

		List<int> x = Enumerable.Range(0, 100).ToList();
		IEnumerable<int> ergebnis = x.Where(e => e % 2 == 0); //Alle geraden Zahlen finden
		IEnumerable<int> ergebnis2 = x.Where(e => e % 5 == 0); //Alle Zahlen finden die durch 5 teilbar sind
	}

	#region Action
	private static void Addiere(int arg1, int arg2) => Console.WriteLine(arg1 + arg2);

	private static void Subtrahiere(int arg1, int arg2) => Console.WriteLine(arg1 - arg2);

	private static void DoAction(int x, int y, Action<int, int> action) => action(x, y);
	#endregion

	#region Predicate
	private static bool CheckForZero(int obj) => obj == 0;

	private static bool CheckForOne(int obj) => obj == 1;

	private static bool DoPredicate(int x, Predicate<int> predicate) => predicate(x);
	#endregion

	#region Func
	private static double Multipliziere(int arg1, int arg2) => arg1 * arg2;

	private static double Dividiere(int arg1, int arg2) => (double) arg1 / arg2;

	private static double DoFunc(int x, int y, Func<int, int, double> func) => func(x, y);
	#endregion
}
