using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var adapters = System.IO.File.ReadAllLines(@$"input.txt").Select(n => int.Parse(n)).ToList();
            adapters.Add(0);
            adapters.Add(adapters.Max() + 3);
            adapters.Sort();

            TotalDifferences(adapters);
            TotalCombinations(adapters);
        }

        static void TotalDifferences(List<int> adapters)
        {
            var differences = new List<int>();

            for (var i = 0; i < adapters.Count() - 1; i++)
            {
                differences.Add(adapters[i+1] - adapters[i]);
            }

            Console.WriteLine($@"Total one jolt differences = {differences.Where(n => n == 1).Count()}, total three jolt differences = {differences.Where(n => n == 3).Count()}.");
            Console.WriteLine($@"Multiplied = {differences.Where(n => n == 1).Count() * differences.Where(n => n == 3).Count()}");
        }

        static void TotalCombinations(List<int> adapters)
        {
            var paths = new Dictionary<int, long> { [0] = 1 };
            var adaptersWithIndexes = adapters.Select((adapter, index) => new KeyValuePair<int,int>(index, adapter)).ToList();
            for (int i = 1; i < adapters.Count(); i++)
            {
                paths[i] = adaptersWithIndexes.Where(a => a.Value < adapters[i] &&  a.Value >= adapters[i] - 3).Sum(a => paths[a.Key]);
            }
            Console.WriteLine($@"{paths.Last().Value}");
        }
    }
}
