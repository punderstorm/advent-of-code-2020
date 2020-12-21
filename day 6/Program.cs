using System;
using System.Collections.Generic;
using System.Linq;

namespace day_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var answers = System.IO.File.ReadAllLines(@$"input.txt").ToList();

            var allGroupsAny = ParseAnswersAny(answers);
            var sumAny = allGroupsAny.Sum(g => g.Length);
            Console.WriteLine($@"Total 'yes' any answers across all groups is {sumAny}.");

            var allGroupsAll = ParseAnswersAll(answers);
            var sumAll = allGroupsAll.Sum(g => g.Length);
            Console.WriteLine($@"Total 'yes' all answers across all groups is {sumAll}.");
        }

        static List<string> ParseAnswersAny(List<string> answers)
        {
            var allGroups = new List<string>();
            var groupAnswers = "";
            foreach (var line in answers)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    allGroups.Add(new string(groupAnswers.Distinct().ToArray()));
                    groupAnswers = "";
                }
                else
                {
                    groupAnswers += line;
                }
            }
            allGroups.Add(new string(groupAnswers.Distinct().ToArray()));
            return allGroups;
        }

        static List<string> ParseAnswersAll(List<string> answers)
        {
            var allGroups = new List<string>();
            string groupAnswers = null;
            foreach (var line in answers)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (groupAnswers != null)
                    {
                        allGroups.Add(new string(groupAnswers.Distinct().ToArray()));
                    }
                    groupAnswers = null;
                }
                else
                {
                    if (groupAnswers == null)
                    {
                        groupAnswers = line;
                    }
                    else
                    {
                        groupAnswers = new string(groupAnswers.Intersect(line.ToArray()).ToArray());
                    }
                }
            }
            allGroups.Add(new string(groupAnswers.Distinct().ToArray()));
            return allGroups;
        }
    }
}
