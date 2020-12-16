using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CUPP
{
    class Program
    {   
        static void Main(string[] args)
        {
            string[] chars = File.ReadLines("chars.txt").ToArray();
            string[] words = File.ReadLines("words.txt").ToArray();
            string[] numbers = File.ReadLines("numbers.txt").ToArray();

            List<string> passwords = new List<string>();

            foreach (string fstWord in words)
            {
                foreach (string specialChar in chars)
                {
                    foreach (string sndWord in words)
                    {
                        if (fstWord != sndWord)
                        {
                            string combo1 = $"{fstWord}{sndWord}";
                            if (Validator.Validate(args, combo1)) { passwords.Add(combo1); }

                            // s - special chars
                            if (args.Contains("s"))
                            {
                                string combo2 = $"{fstWord}{specialChar}{sndWord}";
                                if (Validator.Validate(args, combo2)) { passwords.Add(combo2); }

                            }

                            // n - numbers
                            if (args.Contains("n"))
                            {
                                foreach (string number in numbers)
                                {
                                    string combo3 = $"{fstWord}{sndWord}{number}";
                                    if (Validator.Validate(args, combo3)) { passwords.Add(combo3); }

                                    // s - special chars
                                    if (args.Contains("s"))
                                    {
                                        string combo4 = $"{fstWord}{specialChar}{sndWord}{number}";
                                        if (Validator.Validate(args, combo4)) { passwords.Add(combo4); }

                                        string combo5 = $"{fstWord}{sndWord}{specialChar}{number}";
                                        if (Validator.Validate(args, combo5)) { passwords.Add(combo5); }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            List<string> passwordsInverted = new List<string>();
            List<string> passwordsCapital = new List<string>();

            foreach (string password in passwords)
            {
                // c - capitalization
                if (args.Contains("c") && char.IsLetter(password[0]))
                {
                    passwordsCapital.Add(Helper.FirstCharToUpper(password));
                }

                // s - special chars
                if (args.Contains("i") && args.Contains("s"))
                {
                    foreach (string specialChar in chars)
                    {
                        if (password.Contains(specialChar))
                        {
                            string[] segments = password.Split(specialChar.ToCharArray()[0]);
                            passwordsInverted.Add($"{segments[1]}{specialChar}{segments[0]}");
                        }
                    }
                }
            }

            if (passwordsInverted.Any())
                passwords.AddRange(passwordsInverted);

            if (passwordsCapital.Any())
                passwords.AddRange(passwordsCapital);

            File.WriteAllLines("passwords.txt", passwords);
        }

        
    }
}