using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Describe your bag");
            var myBagDescription = Console.ReadLine().ToLower();


            var rules = System.IO.File.ReadAllLines(@$"input.txt").ToList();


            var bagList = rules.Select(r =>
                new Bag() {
                    Description = Regex.Match(r, "(.*) bags contain").Groups[1].Value,
                    Children = Regex.Matches(r, "(\\d+) (\\w+ \\w+) bags?").Select(m =>
                        new InnerBag() {
                            Count = int.Parse(m.Groups[1].Value),
                            Description = m.Groups[2].Value
                        }).ToList()
                }).ToList();
            

            // see which bags can eventually hold mine
            var bagMatchList = bagList.Where(b => b.Children.Where(c => c.Description == myBagDescription).Any()).ToList();
            List<Bag> canHoldMyBag = bagMatchList.ToList();
            foreach (var bagMatch in bagMatchList)
            {
                FindParentBags(bagList, canHoldMyBag, bagMatch);
            }
            Console.WriteLine($@"{canHoldMyBag.Count()} bag combinations can hold my {myBagDescription} bag!");


            // see how many bags mine has to hold
            var myBag = bagList.Where(b => b.Description == myBagDescription).FirstOrDefault();
            var totalChildBags = 0;
            FindChildBags(bagList, myBag, 1, ref totalChildBags);
            Console.WriteLine($@"My {myBagDescription} bag must contain {totalChildBags} bags!");


            Console.ReadLine();
        }

        static void FindParentBags(in List<Bag> bagList, List<Bag> canHoldMyBag, Bag bagMatch)
        {
            var otherBags = bagList.Where(b => b.Children.Where(c => c.Description == bagMatch.Description).Any()).ToList();
            
            foreach (var bag in otherBags)
            {
                if (!canHoldMyBag.Contains(bagMatch)) { canHoldMyBag.Add(bagMatch); }
                FindParentBags(bagList, canHoldMyBag, bag);
            }
        }

        static void FindChildBags(in List<Bag> bagList, Bag bag, int multiplier, ref int childBags)
        {
            foreach (var childBag in bag.Children)
            {
                childBags += multiplier * childBag.Count;
                FindChildBags(bagList, bagList.Where(b => b.Description == childBag.Description).FirstOrDefault(), multiplier * childBag.Count, ref childBags);
            }
        }
    }

    public class Bag
    {
        public string Description {get;set;}

        public List<InnerBag> Children {get;set;}
    }

    public class InnerBag
    {
        public string Description {get;set;}

        public int Count {get;set;}
    }
}
