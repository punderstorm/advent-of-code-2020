using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var slopes = new List<Slope>() {
                new Slope { Down = 1, Right = 1 },
                new Slope { Down = 1, Right = 3 },
                new Slope { Down = 1, Right = 5 },
                new Slope { Down = 1, Right = 7 },
                new Slope { Down = 2, Right = 1 }
            };
            var pattern = System.IO.File.ReadAllLines(@$"input.txt").Select(input => input).ToList();

            var totalTreesEncountered = 1UL;
            foreach (var slope in slopes)
            {
                totalTreesEncountered *= TraverseSlope(pattern, slope);
            }

            Console.WriteLine($@"We would encounter {totalTreesEncountered} trees on all slopes.");
        }

        static ulong TraverseSlope(List<string> pattern, Slope slope)
        {
            var treesEncountered = 0UL;
            var patternLengthRight = pattern[0].Count();
            var bottomOfSlope = pattern.Count();
            var currentRow = 0;
            var currentColumn = 0;

            while (currentRow <= bottomOfSlope)
            {
                currentColumn += slope.Right;
                currentRow += slope.Down;

                var patternColumn = currentColumn - (patternLengthRight * (currentColumn / patternLengthRight));

                if (currentRow < bottomOfSlope && pattern[currentRow][patternColumn] == '#')
                {
                    treesEncountered++;
                }
            }

            Console.WriteLine($@"We would encounter {treesEncountered} trees on a slope of (Down {slope.Down}, Right {slope.Right}).");

            return treesEncountered;
        }

        static ulong TraverseSlopeWithMap(List<string> pattern, Slope slope)
        {
            var treesEncountered = 0UL;
            var patternLengthRight = pattern[0].Count();
            var patternLengthDown = pattern.Count();
            var repeatPatternRight = slope.Right * (patternLengthDown / slope.Down);
            var repeatPatternDown = 1;
            var totalColumns = patternLengthRight * repeatPatternRight;
            var totalRows = patternLengthDown * repeatPatternDown;
            var currentRow = 0;
            var currentColumn = 0;
            var map = new List<string>();

            for (var i = 0; i < pattern.Count(); i++)
            {
                var row = string.Concat(Enumerable.Repeat(pattern[i], repeatPatternRight));
                map.Add(row);
            }

            System.IO.File.WriteAllLines($@"map_{slope.Down}_{slope.Right}.txt", map);

            var mapTraversed = map.ToList();

            while (currentRow <= totalRows)
            {
                currentColumn += slope.Right;
                currentRow += slope.Down;

                if (currentRow < totalRows)
                {
                    if (mapTraversed[currentRow][currentColumn] == '#')
                    {
                        mapTraversed[currentRow] = MarkMap(mapTraversed[currentRow], currentColumn, 'X');
                        treesEncountered++;
                    }
                    else
                    {
                        mapTraversed[currentRow] = MarkMap(mapTraversed[currentRow], currentColumn, 'O');
                    }
                }
            }

            System.IO.File.WriteAllLines($@"maptraversed_{slope.Down}_{slope.Right}.txt", mapTraversed);

            Console.WriteLine($@"We would encounter {treesEncountered} trees on a slope of (Down {slope.Down}, Right {slope.Right}).");

            return treesEncountered;
        }

        static string MarkMap(string row, int index, char mark)
        {
            char[] chars = row.ToCharArray();
            chars[index] = mark;
            return new string(chars);
        }
    }

    public class Slope
    {
        public int Right {get;set;}

        public int Down {get;set;}
    }
}
