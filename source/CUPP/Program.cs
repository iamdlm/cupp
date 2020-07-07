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
        private static readonly int minLength = 8;
        private static readonly int maxLength = 16;

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
                            // first word + special char + second word
                            string combo1 = $"{fstWord}{specialChar}{sndWord}";
                            if (ValidateRequirements(combo1)) { passwords.Add(combo1);  }

                            foreach (string number in numbers)
                            {
                                // first word + second word + special char + numbers
                                string combo2 = $"{fstWord}{sndWord}{specialChar}{number}";
                                if (ValidateRequirements(combo2)) { passwords.Add(combo2); }
                            }
                        }
                    }
                }
            }

            List<string> passwordsInverted = new List<string>();

            foreach (string password in passwords)
            {
                foreach(string specialChar in chars)
                {
                    if (password.Contains(specialChar))
                    {
                        string[] segments = password.Split(specialChar.ToCharArray()[0]);
                        passwordsInverted.Add($"{segments[1]}{specialChar}{segments[0]}");
                    }
                }
            }

            passwords.AddRange(passwordsInverted);

            File.WriteAllLines("passwords.txt", passwords);
        }

        private static bool ValidateRequirements(string password)
        {
            // validate length
            if (password.Length < minLength || password.Length > maxLength) { return false; }

            // validate only numbers
            if (Regex.IsMatch(ToAlphaNumericOnly(password), @"^\d+$")) { return false; }

            // validate only letters
            if (Regex.IsMatch(ToAlphaNumericOnly(password), @"^[a-zA-Z]+$")) { return false; };

            return true;
        }

        private static string ToAlphaNumericOnly(string input)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9]");
            return rgx.Replace(input, "");
        }
    }
}