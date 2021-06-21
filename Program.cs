

// Author: FreeDOOM#4231 on Discord


using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace ASD___1
{
	// Podsumowanie:
	// Skuteczniejszy i prostszy do zrozumienia jest algorytm pierwszy, co prawda polega na wcześniejszym posortowaniu
	//  tablicy wejściowej, jednak nie znam zastosowania w którym sortowanie byłoby na tyle nie opłacalne by skorzystać
	//  z 2giego algorytmu.

	class Program
	{
        //const string fileName = "_test.txt";
        const string fileName = "5.txt";
        //const string fileName = "_E_1_Pietrzeniuk.txt";

        static void Main(string[] args)
		{
			//CreateTestFile();
			List<int[]> list1 = LoadIntervals($"in{fileName}");
			List<int[]> list2 = LoadIntervals($"in{fileName}");
			list1 = FirstSolution.SumIntervals(list1);
			list2 = SecondSolution.SumIntervals(list2);
			// Drugi algorytm nie sortuje przedziałów, więc jeśli potrzebne są posortowane, trzeba to zrobić samemu.
			//list2.Sort((a, b) => a[0].CompareTo(b[0]));
			SaveIntervals($"out{fileName}", list1, list2);
		}

		#region >>> Obsługa plików <<<

		static List<int[]> LoadIntervals(string fileName__)
		{
			string[] lines = File.ReadAllLines(fileName__);

			int count = Convert.ToInt32(lines[0]);
			var resultList = new List<int[]>(count);
			string[] a;
			int[] newInterval;
			for (int i = 0; i < count; i++)
			{
				newInterval = new int[2];

				a = lines[i + 1].Split(' ');
				newInterval[0] = Convert.ToInt16(a[0]);
				newInterval[1] = Convert.ToInt16(a[1]);

				resultList.Add(newInterval);
			}

			return resultList;
		}

		static void SaveIntervals(string fileName__, List<int[]> intervals1, List<int[]> intervals2)
		{
			if (File.Exists(fileName__))
				File.Delete(fileName__);

			File.AppendAllText(fileName__, $"{intervals1.Count}\n");
			foreach(int[] interval in intervals1)
				File.AppendAllText(fileName__, $"{interval[0]} {interval[1]}\n");

			File.AppendAllText(fileName__, $"\n{intervals2.Count}\n");
			foreach (int[] interval in intervals2)
				File.AppendAllText(fileName__, $"{interval[0]} {interval[1]}\n");

			Console.WriteLine($"\n{fileName__} contents:\n");
			Console.WriteLine(File.ReadAllText(fileName__));
		}

		static void CreateTestFile()
		{
			string fileName__ = "in" + fileName;
			if (File.Exists(fileName__))
				File.Delete(fileName__);

			Random rng = new Random();

			int sampleSize = rng.Next(30, 100);

			File.AppendAllText(fileName__, $"{sampleSize}\n");

			for (int i = 1, a, b; i <= sampleSize; i++)
			{
				a = rng.Next(1, 91); //991
				b = rng.Next(a, a + 10);

				File.AppendAllText(fileName__, $"{a} {b}\n");
			}

			Console.WriteLine($"\n{fileName__} contents:\n");
			Console.WriteLine(File.ReadAllText(fileName__));
		}

        #endregion
    }
}
