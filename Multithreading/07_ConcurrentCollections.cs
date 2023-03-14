using System.Collections.Concurrent;

namespace Multithreading;

internal class _07_ConcurrentCollections
{
	static void Main(string[] args)
	{
		ConcurrentBag<int> ints = new ConcurrentBag<int>(); //Thread-sichere Liste
		ints.Add(1);

		ConcurrentDictionary<string, int> keyValuePairs = new ConcurrentDictionary<string, int>(); //Thread-sicheres Dictionary
		keyValuePairs.TryAdd("key1", 1);
	}
}
