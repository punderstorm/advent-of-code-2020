using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var instructions = System.IO.File.ReadAllLines(@$"input.txt").Select(i => new Instruction(i)).ToList();

            BootLoop(instructions);

            FixBootLoop(instructions, new List<int>(), 0, 0);
        }

        static void BootLoop(List<Instruction> instructions)
        {
            var accumulator = 0;
            var currentInstruction = 0;
            var workingList = new List<int>();

            while (!workingList.Contains(currentInstruction))
            {
                workingList.Add(currentInstruction);
                switch ((Operations)instructions[currentInstruction].Operation)
                {
                    case Operations.acc:
                        accumulator += instructions[currentInstruction].Argument;
                        currentInstruction += 1;
                        break;
                    case Operations.jmp:
                        currentInstruction += instructions[currentInstruction].Argument;
                        break;
                    case Operations.nop:
                        currentInstruction += 1;
                        break;
                }
            }

            Console.WriteLine($@"The last accumulator value before the boot loop was {accumulator}.");
        }

        static bool FixBootLoop(List<Instruction> instructions, List<int> workingList, int accumulator, int currentInstruction, bool hardPass = false)
        {
            if (currentInstruction < instructions.Count())
            {
                if (workingList.Contains(currentInstruction))
                {
                    return false;
                }
                else
                {
                    workingList.Add(currentInstruction);
                    var updatedAccumulator = accumulator;
                    var nextInstruction = currentInstruction;
                    ProcessInstruction(instructions, ref updatedAccumulator, ref nextInstruction);

                    var pass = FixBootLoop(instructions, workingList, updatedAccumulator, nextInstruction, hardPass);
                    if (!pass)
                    {
                        if (!hardPass && instructions[currentInstruction].CanChange)
                        {
                            hardPass = true;
                            instructions[currentInstruction].Operation *= -1;
                            updatedAccumulator = accumulator;
                            nextInstruction = currentInstruction;
                            ProcessInstruction(instructions, ref updatedAccumulator, ref nextInstruction);

                            pass = FixBootLoop(instructions, workingList, updatedAccumulator, nextInstruction, hardPass);
                            if (!pass)
                            {
                                instructions[currentInstruction].Operation *= -1;
                            }
                        }
                        
                        workingList.Remove(currentInstruction);
                    }

                    return pass;
                }
            }
            else
            {
                Console.WriteLine($@"Fixed the boot loop.  Last accumulator value was {accumulator}.");
                return true;
            }
        }

        static void ProcessInstruction(List<Instruction> instructions, ref int accumulator, ref int nextInstruction)
        {
            switch ((Operations)instructions[nextInstruction].Operation)
            {
                case Operations.acc:
                    accumulator += instructions[nextInstruction].Argument;
                    nextInstruction += 1;
                    break;
                case Operations.jmp:
                    nextInstruction += instructions[nextInstruction].Argument;
                    break;
                case Operations.nop:
                    nextInstruction += 1;
                    break;
            }
        }
    }

    public class Instruction
    {
        public bool CanChange {
            get
            {
                return (Operations)Operation == Operations.jmp || (Operations)Operation == Operations.nop;
            }
        }
        public int Operation {get;set;}
        public int Argument {get;set;}

        public Instruction(string instruction)
        {
            var match = Regex.Match(instruction, "(acc|jmp|nop) ((?:\\+|\\-)\\d*)");
            Operation = (int)Enum.Parse(typeof(Operations), match.Groups[1].Value);
            Argument = int.Parse(match.Groups[2].Value);
        }
    }

    public enum Operations
    {
        jmp = -1,
        acc = 0,
        nop = 1
    }
}
