namespace DelegatesEvents;

internal class ComponentWithEvent
{
	static void Main(string[] args)
	{
		Component comp = new Component();
		comp.ProcessCompleted += () => Console.WriteLine("Fertig"); //Action ohne Parameter mit ()
		comp.Progress += (i) => Console.WriteLine($"Fortschritt: {i}"); //Action mit einem Parameter (i)
		comp.StartProcess();
	}
}

public class Component
{
	public event Action ProcessCompleted; //Action als Delegate statt EventHandler

	public event Action<int> Progress; //Action mit einem Paramter (der Fortschritt)

	public void StartProcess()
	{
		for (int i = 0; i < 10; i++)
		{
			Thread.Sleep(200);
			Progress?.Invoke(i); //Programmierer kann das Progress Event weglassen, wenn es ihn nicht interessiert
		}
		ProcessCompleted?.Invoke();
	}
}