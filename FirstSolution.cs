using System;
using System.Collections.Generic;
using System.Text;

namespace ASD___1
{
	// Algorytm ma złożoność czasową O(n)=n*lg(n)
	// Wynika to z tego, że musi przeiterować przez całą tablicę przedziałów i wykonać na niektórych z nich operacje.
	// Do czasu jego wykonywania trzeba jednak także dodać czas stracony na posortowanie tablicy, ale przy QuickSort
	//  jest to O(n)=n*lg(n).

	public static class FirstSolution
    {
		public static List<int[]> SumIntervals(List<int[]> intervals)
		{
			// Z racji że Lista jest obiektem dynamicznym będziemy w miarę rozwiązywania problemu dodawać do niej przedziały.
			var result = new List<int[]>();

			// Przed rozpoczęciem operacji pomoże nam jeśli przedziały wejściowe będą posortowane rosnąco,
			//  (mogą być malejąco, jest to obojętne ale w takim przypadku trzeba zmodyfikować algorytm),
			//  to sprawi, że wszystkie przedziały które można zsumować będą koło siebie.
			// Przedziały będą sortowane pod względem początkowych wartości bez zaprzątania sobie głowy końcowymi.
			// Żeby nie wymyślać koła na nowo, użyłem dostępnej w .NET metody list.Sort(), która to używa QuickSort'a.
			intervals.Sort((a, b) => a[0].CompareTo(b[0]));

			for (int i = 0; i < intervals.Count;)
			{
				// Rozpoczynamy od dodania przedziału z początku grupy.
				// Czym jest grupa w tym kontekście?
				// Są to przedziały z listy wejściowej które utworzą jeden przedział w liście wyjściowej:
				//  {[0,2], [1,5], [2,4], [5,11]} tworzą jedną grupę, ale [13, 115] już do niej nie należy.
				var newInterval = new int[2];
				newInterval = intervals[i++];

				// W tym miejscu dochodzi do właściwego łączenia ze sobą przedziałów będących w jednej grupie.
				for (; (i < intervals.Count) && (intervals[i][0] - 1 <= newInterval[1]); i++)
					// Wybierany jest większy z końców przedziałów.
					newInterval[1] = Math.Max(intervals[i][1], newInterval[1]);

				// Dodajemy nowy (będący sumą grupy) przedział do listy.
				result.Add(newInterval);
			}

			return result;
		}

	}
}
