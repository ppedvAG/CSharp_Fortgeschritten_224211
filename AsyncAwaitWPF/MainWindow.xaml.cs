using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncAwaitWPF
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				Thread.Sleep(25);
				Progress.Value++;
			} //UI Thread wird blockiert
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Task.Run(() => //UI Updates sind von Side Tasks sind nicht erlaubt
			{
				Dispatcher.Invoke(() => Progress.Value = 0); //Dispatcher.Invoke um UI Updates auf dem Main Thread auszuführen
				for (int i = 0; i < 100; i++)
				{
					Thread.Sleep(25);
					Dispatcher.Invoke(() => Progress.Value++);
				}
			});
		}

		private async void Button_Click_2(object sender, RoutedEventArgs e)
		{
			Progress.Value = 0;
			for (int i = 0; i < 100; i++)
			{
				await Task.Delay(25);
				Progress.Value++;
			}
		}

		private async void Button_Click_3(object sender, RoutedEventArgs e)
		{
			HttpClient client = new();
			Task<HttpResponseMessage> response = client.GetAsync(@"http://www.gutenberg.org/files/54700/54700-0.txt");
			//UI Update
			TB.Text = "Text lädt...";
			for (int i = 0; ; i++)
			{
				Progress.Value++;
				await Task.Delay(5);
				if (response.IsCompletedSuccessfully)
					break;
			}
			HttpResponseMessage resp = await response;
			TB.Text = await resp.Content.ReadAsStringAsync();
		}

		private async void Button_Click_4(object sender, RoutedEventArgs e)
		{
			List<int> ints = Enumerable.Range(0, 1_000_000_000).ToList();
			await Parallel.ForEachAsync(ints, (item, ct) => 
			{
				Console.WriteLine(item * 10);
				return ValueTask.CompletedTask;
			});

			IAsyncEnumerable<int> liste = null;
			await foreach(int i in liste)
			{

			}
		}
	}
}
