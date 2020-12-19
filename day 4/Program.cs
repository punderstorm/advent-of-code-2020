using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace day_4
{
    class Program
    {
        static void Main(string[] args)
        {
            var passportData = System.IO.File.ReadAllLines(@$"input.txt").ToList();

            var passports = ParsePassports(passportData);

            var validPassports = passports.Where(p => p.IsValid()).Count();

            Console.WriteLine($@"There are {validPassports} valid passports.");
        }

        static List<PassportInfo> ParsePassports(List<string> passportData)
        {
            var passports = new List<PassportInfo>();
            var passport = new PassportInfo();
            foreach (var line in passportData)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    passports.Add(passport);
                    passport = new PassportInfo();
                }
                else
                {
                    var matches = Regex.Matches(line, "(byr|iyr|eyr|hgt|hcl|ecl|pid|cid):([A-Za-z0-9#]*)").ToList();
                    foreach (var match in matches)
                    {
                        PropertyInfo pinfo = typeof(PassportInfo).GetProperty(match.Groups[1].Value);
                        pinfo.SetValue(passport, match.Groups[2].Value);
                    }
                }
            }
            passports.Add(passport);
            return passports;
        }
    }

    public class PassportInfo
    {
        [DisplayName("Birth Year")]
        [Required]
        [Range(1920, 2002)]
        public string byr {get;set;}

        [DisplayName("Issue Year")]
        [Required]
        [Range(2010, 2020)]
        public string iyr {get;set;}

        [DisplayName("Expiration Year")]
        [Required]
        [Range(2020, 2030)]
        public string eyr {get;set;}

        [DisplayName("Height")]
        [Required]
        [RegularExpression("(1[5-8][0-9]cm|19[0-3]cm|59in|6[0-9]in|7[0-6]in)")]
        public string hgt {get;set;}

        [DisplayName("Hair Color")]
        [Required]
        [RegularExpression("#[0-9a-f]{6}")]
        public string hcl {get;set;}

        [DisplayName("Eye Color")]
        [Required]
        [RegularExpression("(amb|blu|brn|gry|grn|hzl|oth)")]
        public string ecl {get;set;}

        [DisplayName("Passport ID")]
        [Required]
        [RegularExpression("[0-9]{9}")]
        public string pid {get;set;}

        [DisplayName("Country ID")]
        public string cid {get;set;}

        public bool IsValid()
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(this, null, null);           
            if(Validator.TryValidateObject(this, context, results, true))
            {     
                return true;
            }
            
            return false;
        }
    }
}
