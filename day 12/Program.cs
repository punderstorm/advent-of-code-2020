using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_12
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = System.IO.File.ReadAllLines(@$"input.txt").Select(i => new Instruction(i)).ToList();
            
            Part1(instructions);
            Part2(instructions);
        }

        static void Part1(List<Instruction> instructions)
        {
            var position = new Position { Vertical = 0, Horizontal = 0 };
            var facing = Directions.East;

            Console.WriteLine($@"Part 1:");

            foreach (var instruction in instructions)
            {
                switch(instruction.What)
                {
                    case 'N':
                        position.Vertical += instruction.HowMuch;
                        break;
                    case 'S':
                        position.Vertical -= instruction.HowMuch;
                        break;
                    case 'E':
                        position.Horizontal += instruction.HowMuch;
                        break;
                    case 'W':
                        position.Horizontal -= instruction.HowMuch;
                        break;
                    case 'R':
                    case 'L':
                        if (instruction.What == 'R')
                        {
                            facing += instruction.HowMuch;
                        }
                        else
                        {
                            facing -= instruction.HowMuch;
                        }

                        if (facing < 0)
                        {
                            facing = 360 + facing;
                        }
                        else if (facing >= 360)
                        {
                            facing = facing - 360;
                        }

                        break;
                    case 'F':
                        switch (facing)
                        {
                            case Directions.North:
                                position.Vertical += instruction.HowMuch;
                                break;
                            case Directions.South:
                                position.Vertical -= instruction.HowMuch;
                                break;
                            case Directions.East:
                                position.Horizontal += instruction.HowMuch;
                                break;
                            case Directions.West:
                                position.Horizontal -= instruction.HowMuch;
                                break;
                        }
                        break;
                }
            }

            Console.WriteLine($@"North/South: {position.Vertical}, East/West {position.Horizontal}");

            var manhattanDistance = Math.Abs(position.Vertical) + Math.Abs(position.Horizontal);
            Console.WriteLine($@"Manhattan Distance: {manhattanDistance}");
        }

        static void Part2(List<Instruction> instructions)
        {
            var position = new Position { Vertical = 0, Horizontal = 0 };
            var waypoint = new Position { Vertical = 1, Horizontal = 10 };

            Console.WriteLine($@"Part 2:");

            foreach (var instruction in instructions)
            {
                switch(instruction.What)
                {
                    case 'N':
                        waypoint.Vertical += instruction.HowMuch;
                        break;
                    case 'S':
                        waypoint.Vertical -= instruction.HowMuch;
                        break;
                    case 'E':
                        waypoint.Horizontal += instruction.HowMuch;
                        break;
                    case 'W':
                        waypoint.Horizontal -= instruction.HowMuch;
                        break;
                    case 'R':
                    case 'L':
                        var waypointStart = new Position { Vertical = waypoint.Vertical, Horizontal = waypoint.Horizontal };

                        if (instruction.What == 'R')
                        {
                            switch (instruction.HowMuch)
                            {
                                case 90:
                                    waypoint.Vertical = waypointStart.Horizontal * -1;
                                    waypoint.Horizontal = waypointStart.Vertical;
                                    break;
                                case 180:
                                    waypoint.Vertical = waypointStart.Vertical * -1;
                                    waypoint.Horizontal = waypointStart.Horizontal * -1;
                                    break;
                                case 270:
                                    waypoint.Vertical = waypointStart.Horizontal;
                                    waypoint.Horizontal = waypointStart.Vertical * -1;
                                    break;
                            }
                        }
                        else
                        {
                            switch (instruction.HowMuch)
                            {
                                case 90:
                                    waypoint.Vertical = waypointStart.Horizontal;
                                    waypoint.Horizontal = waypointStart.Vertical * -1;
                                    break;
                                case 180:
                                    waypoint.Vertical = waypointStart.Vertical * -1;
                                    waypoint.Horizontal = waypointStart.Horizontal * -1;
                                    break;
                                case 270:
                                    waypoint.Vertical = waypointStart.Horizontal * -1;
                                    waypoint.Horizontal = waypointStart.Vertical;
                                    break;
                            }
                        }

                        break;
                    case 'F':
                        position.Vertical += instruction.HowMuch * waypoint.Vertical;
                        position.Horizontal += instruction.HowMuch * waypoint.Horizontal;
                        break;
                }
            }

            Console.WriteLine($@"North/South: {position.Vertical}, East/West {position.Horizontal}");

            var manhattanDistance = Math.Abs(position.Vertical) + Math.Abs(position.Horizontal);
            Console.WriteLine($@"Manhattan Distance: {manhattanDistance}");
        }
    }

    public class Directions
    {
        public const int North = 0;
        public const int East = 90;
        public const int South = 180;
        public const int West = 270;
    }

    public class Instruction
    {
        public char What {get;set;}
        public int HowMuch {get;set;}

        public Instruction (string instruction)
        {
            What = Regex.Match(instruction, "[A-Za-z]").Value.ToUpper().First();
            HowMuch = int.Parse(Regex.Match(instruction, "\\d+").Value);
        }
    }

    public class Position
    {
        public int Vertical {get;set;}

        public int Horizontal {get;set;}
    }
}
