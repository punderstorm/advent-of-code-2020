using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_13
{
    class Program
    {
        static void Main(string[] args)
        {
            var notes = System.IO.File.ReadAllLines(@$"input.txt").ToList();

            Part1(notes);
            Part2(notes[1]);
        }

        static void Part1 (List<string> notes)
        {
            var earliestTimestamp = int.Parse(notes[0]);
            var currentTimestamp = earliestTimestamp;
            var busIDs = Regex.Matches(notes[1], "\\d+").Cast<Match>().Select(b => int.Parse(b.Value)).ToList();
            int? busMatch = null;

            while (busMatch == null)
            {
                var matches = busIDs.Where(b => currentTimestamp % b == 0).ToList();
                if (matches.Count() > 0)
                {
                    busMatch = matches.First();
                }
                else
                {
                    currentTimestamp++;
                }
            }

            Console.WriteLine($@"Bus ID {busMatch.Value} departing at {currentTimestamp}.");
            Console.WriteLine($@"Answer (time difference * bus id): {(currentTimestamp - earliestTimestamp) * busMatch.Value}");
        }

        // static void Part2 (string busIDs)
        // {
        //     int busID;
        //     var busesInService = busIDs.Split(",")
        //         .Select((b, index) => new KeyValuePair<int, string>(index, b))
        //         .Where(b => int.TryParse(b.Value, out busID) == true)
        //         .Select(b => new KeyValuePair<int, int> (b.Key, int.Parse(b.Value)))
        //         .ToDictionary(b => b.Key, b => b.Value);
        //     int firstBus = busesInService.First().Value;
        //     int lastBus = busesInService.Last().Value;
        //     int firstLastDiff = busesInService.Last().Key - busesInService.First().Key;
        //     int highestBus = busesInService.OrderBy(b => b.Value).Last().Value;
        //     int firstHighestDiff = busesInService.OrderBy(b => b.Value).Last().Key - busesInService.First().Key;

        //     Console.WriteLine($@"First bus {firstBus}, Last bus {lastBus}, Time difference {firstLastDiff}");
        //     Console.WriteLine($@"First bus {firstBus}, Highest bus {highestBus}, Time difference {firstHighestDiff}");

        //     var startTime = busesInService.First().Value;
        //     var multiplier = 0;
        //     long currentTime = 0;
        //     var matchesFound = 0;
        //     while (matchesFound < busesInService.Count())
        //     {
        //         multiplier++;
        //         matchesFound = 0;
        //         currentTime = startTime * multiplier;
        //         //if ((currentTime + firstLastDiff) % lastBus == 0)
        //         if ((currentTime + firstHighestDiff) % highestBus == 0)
        //         {
        //             Console.WriteLine($@"Trying with start time {currentTime}");
        //             foreach (var bus in busesInService)
        //             {
        //                 if ((currentTime + (bus.Key - startTime)) % bus.Value == 0)
        //                 {
        //                     matchesFound++;
        //                 }
        //                 else
        //                 {
        //                     break;
        //                 }
        //             }
        //             Console.WriteLine($@"{matchesFound} out of {busesInService.Count()} match");
        //         }
        //     }

        //     Console.WriteLine($@"Earliest time {currentTime}");
        // }

        static void Part2 (string busIDs)
        {
            var sw = Stopwatch.StartNew();
            Console.WriteLine(System.IO.File.ReadLines("input.txt")
                .Last(s => !string.IsNullOrWhiteSpace(s))
                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select((s, i) => new KeyValuePair<int, int>(i, (s == "x") ? 1 : int.Parse(s)))
                .Aggregate(new { T = 0L, Lcm = 1L }, (acc, b) =>
                    new
                    {
                        T = Enumerable.Range(0, Int32.MaxValue)
                            .Select(n => acc.T + (acc.Lcm * n))
                            .First((n) => (n + b.Key) % b.Value == 0),
                        Lcm = acc.Lcm * b.Value
                    }
                )
                .T);
            sw.Stop();
            Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
            Console.ReadKey(true);
        }
    }
}

// using System;
// using System.Collections.Immutable;
// using System.IO;
// using System.Linq;

// var notes = File.ReadAllLines("Input.txt");

// var buses = notes[1].Split(',')
//   .Select((value, offset) => (value, offset))
//   .Where(pair => pair.value != "x")
//   .Select(pair => new Bus(long.Parse(pair.value), pair.offset))
//   .ToImmutableList();

// var arrival = long.Parse(notes[0]);

// var (nextBusId, _) = buses
//   .OrderBy(bus => GetWaitTime(bus.Id))
//   .First();

// long GetWaitTime(long schedule) =>
//   arrival / schedule * schedule + schedule - arrival;

// var (baseBusId, _) = buses[0];
// var (time, period) = (baseBusId, baseBusId);

// foreach (var (schedule, offset) in buses.Skip(1))
// {
//   while ((time + offset) % schedule != 0) time += period;

//   period *= schedule;
// }

// Console.WriteLine(nextBusId * GetWaitTime(nextBusId));
// Console.WriteLine(time);

// internal record Bus(long Id, int Offset);