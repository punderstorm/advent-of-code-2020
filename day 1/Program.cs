using System;
using System.Collections.Generic;
using System.Linq;

namespace day_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = System.IO.File.ReadAllLines(@$"input.txt").Select(n => int.Parse(n)).OrderBy(n => n).ToList();

            FindTwoNumbers(numbers);

            FindThreeNumbers(numbers);
        }

        static void FindTwoNumbers(List<int> numbers)
        {
            for (var i = 0; i < numbers.Count(); i++)
            {
                for (var j = numbers.Count() - 1; j >= 0; j--)
                {
                    if (numbers[i] + numbers[j] == 2020)
                    {
                        Console.WriteLine($@"{numbers[i]} + {numbers[j]} = 2020");
                        Console.WriteLine($@"These numbers multiplied together is {numbers[i] * numbers[j]}");
                        return;
                    }
                    else if (numbers[i] + numbers[j] < 2020)
                    {
                        break;
                    }
                }
            }
        }

        static void FindThreeNumbers(List<int> numbers)
        {
            for (var i = 0; i < numbers.Count(); i++)
            {
                for (var j = 0; j < numbers.Count() ; j++)
                {
                    for (var k = 0; k < numbers.Count() ; k++)
                    {
                        if (i != j && i != k)
                        {
                            if (numbers[i] + numbers[j] + numbers[k] == 2020)
                            {
                                Console.WriteLine($@"{numbers[i]} + {numbers[j]} + {numbers[k]} = 2020");
                                Console.WriteLine($@"These numbers multiplied together is {numbers[i] * numbers[j] * numbers[k]}");
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
