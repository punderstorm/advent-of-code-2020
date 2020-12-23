using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_9
{
    class Program
    {
        static void Main(string[] args)
        {
            var xmas = System.IO.File.ReadAllLines(@$"input.txt").Select(n => ulong.Parse(n)).ToList();
            var preamble = 25;
            ulong invalid = default(ulong);
            int invalidPosition = default(int);

            for (var i = preamble; i < xmas.Count(); i++)
            {
                var searchRange = xmas.GetRange(i - preamble, preamble);
                if (searchRange.Where(n => searchRange.Contains(xmas[i] - n)).Count() == 0)
                {
                    invalid = xmas[i];
                    invalidPosition = i;
                    Console.WriteLine($@"{xmas[i]} at position {i} does not have a match.");
                    break;
                }
            }
            
            FindContiguous(xmas, invalid, invalidPosition);
        }

        static void FindContiguous(List<ulong> xmas, ulong invalid, int invalidPosition)
        {
            for (var i = 0; i < xmas.Count(); i++)
            {
                var sum = xmas[i];

                for (var j = i + 1; j < xmas.Count(); j++)
                {
                    sum += xmas[j];
                    if (sum == invalid)
                    {
                        Console.WriteLine($@"Contiguous numbers from position {i} to position {j} equal {invalid}.");
                        var contiguous = xmas.GetRange(i, j - i);
                        Console.WriteLine($@"Min = {contiguous.Min()}; Max = {contiguous.Max()}");
                        Console.WriteLine($@"Added = {contiguous.Min() + contiguous.Max()}");
                        return;
                    }
                    else if (sum > invalid)
                    {
                        break;
                    }
                }
            }
        }
    }
}
