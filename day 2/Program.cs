using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var passwords = System.IO.File.ReadAllLines(@$"input.txt");
            Console.WriteLine($@"There are {GetNumberOfValidPasswords1(passwords)} valid passwords according to policy 1.");
            Console.WriteLine($@"There are {GetNumberOfValidPasswords2(passwords)} valid passwords according to policy 2.");
        }

        static int GetNumberOfValidPasswords1 (string[] input)
        {
            return input.Select(p => GetPasswordWithPolicy1(p)).Where(p => ValidatePasswordAgainstPolicy1(p)).Count();
        }

        static PasswordWithPolicy1 GetPasswordWithPolicy1(string input)
        {
            var match = Regex.Match(input, "(\\d*)-(\\d*) ([A-Za-z]): ([A-Za-z]*)");
            return new PasswordWithPolicy1 {
                    MinTimes = int.Parse(match.Groups[1].Value),
                    MaxTimes = int.Parse(match.Groups[2].Value),
                    Letter = match.Groups[3].Value[0],
                    Password = match.Groups[4].Value,
                };
        }

        static bool ValidatePasswordAgainstPolicy1(PasswordWithPolicy1 password)
        {
            var letters = password.Password.Where(c => c == password.Letter);
            return (letters.Count() >= password.MinTimes && letters.Count() <= password.MaxTimes);
        }

        static int GetNumberOfValidPasswords2 (string[] input)
        {
            return input.Select(p => GetPasswordWithPolicy2(p)).Where(p => ValidatePasswordAgainstPolicy2(p)).Count();
        }

        static PasswordWithPolicy2 GetPasswordWithPolicy2(string input)
        {
            var match = Regex.Match(input, "(\\d*)-(\\d*) ([A-Za-z]): ([A-Za-z]*)");
            return new PasswordWithPolicy2 {
                    Position1 = int.Parse(match.Groups[1].Value),
                    Position2 = int.Parse(match.Groups[2].Value),
                    Letter = match.Groups[3].Value[0],
                    Password = match.Groups[4].Value,
                };
        }

        static bool ValidatePasswordAgainstPolicy2(PasswordWithPolicy2 password)
        {
            //Console.WriteLine($@"Validating '{password.Position1}-{password.Position2} {password.Letter}: {password.Password}'");
            var position1ContainsLetter = (password.Password[password.Position1 - 1] == password.Letter);
            var position2ContainsLetter = (password.Password[password.Position2 - 1] == password.Letter);
            return (position1ContainsLetter ^ position2ContainsLetter);
        }
    }

    public class PasswordWithPolicy1
    {
        public int MinTimes {get;set;}

        public int MaxTimes {get;set;}

        public char Letter {get;set;}

        public string Password {get;set;}
    }

    public class PasswordWithPolicy2
    {
        public int Position1 {get;set;}

        public int Position2 {get;set;}

        public char Letter {get;set;}

        public string Password {get;set;}
    }
}
