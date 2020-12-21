using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_5
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardingPasses = System.IO.File.ReadAllLines(@$"input.txt").ToList();
            
            var seatsTaken = new List<int>();
            foreach(var boardingPass in boardingPasses)
            {
                var seat = new Seat { MinRow = 0, MaxRow = 127, MinSeat = 0, MaxSeat = 7 };
                boardingPass.ToList().ForEach(c => ProcessBoardingPass(c, seat));
                seatsTaken.Add(seat.MinRow * 8 + seat.MinSeat);
            }

            Console.WriteLine($@"The highest seat number taken is {seatsTaken.Max()}.");

            var minID = seatsTaken.Min();
            var maxID = seatsTaken.Max();
            var seatIDs = Enumerable.Range(minID, maxID-minID).ToList();
            var missing = seatIDs.Except(seatsTaken).FirstOrDefault();
            Console.WriteLine($@"My seat ID is {missing}.");
        }

        static void ProcessBoardingPass(char c, Seat seat)
        {
            if (c == 'F')
            {
                seat.MaxRow = (int)Math.Floor((seat.MaxRow + seat.MinRow) / 2.0);
            }
            else if (c == 'B')
            {
                seat.MinRow = (int)Math.Ceiling((seat.MaxRow + seat.MinRow) / 2.0);
            }
            else if (c == 'L')
            {
                seat.MaxSeat = (int)Math.Floor((seat.MaxSeat + seat.MinSeat) / 2.0);
            }
            else if (c == 'R')
            {
                seat.MinSeat = (int)Math.Ceiling((seat.MaxSeat + seat.MinSeat) / 2.0);
            }
        }
    }

    public class Seat
    {
        public int MinRow {get;set;}
        public int MaxRow {get;set;}
        public int MinSeat {get;set;}
        public int MaxSeat {get;set;}
    }
}
