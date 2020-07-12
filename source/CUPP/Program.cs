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
                            // first word + second word
                            string combo1 = $"{fstWord}{sndWord}";
                            if (ValidateRequirements(combo1)) { passwords.Add(combo1); }

                            // first word + special char + second word
                            string combo2 = $"{fstWord}{specialChar}{sndWord}";
                            if (ValidateRequirements(combo2)) { passwords.Add(combo2); }

                            foreach (string number in numbers)
                            {
                                // first word + second word + number
                                string combo3 = $"{fstWord}{sndWord}{number}";
                                if (ValidateRequirements(combo3)) { passwords.Add(combo3); }

                                // first word + special char + second word + number
                                string combo4 = $"{fstWord}{specialChar}{sndWord}{number}";
                                if (ValidateRequirements(combo4)) { passwords.Add(combo4); }

                                // first word + second word + special char + number
                                string combo5 = $"{fstWord}{sndWord}{specialChar}{number}";
                                if (ValidateRequirements(combo5)) { passwords.Add(combo5); }
                            }
                        }
                    }
                }
            }

            List<string> passwordsInverted = new List<string>();

            foreach (string password in passwords)
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

            passwords.AddRange(passwordsInverted);

            File.WriteAllLines("passwords.txt", passwords);
        }

        private static bool ValidateRequirements(string password, int minChecks = 2)
        {
            int checkCounts = 0;

            // validate min length
            if (password.Length >= minLength)
                checkCounts++;

            // validate max length
            if (maxLength == 0)
                checkCounts++;
            else if (password.Length <= maxLength)
                checkCounts++;


            // validate only numbers
            if (!IsDigitsOnly(password))
                checkCounts++;

            // validate only letters
            if (!IsLettersOnly(password))
                checkCounts++;

            // validate +1 upper case letter(s)
            if (!password.Equals(password.ToLower()))
                checkCounts++;

            return checkCounts >= minChecks ? true : false;
        }

        private static bool IsDigitsOnly(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsDigit(c))
                    return false;
            }

            return true;
        }

        private static bool IsLettersOnly(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsLetter(c))
                    return false;
            }

            return true;
        }

        private static bool IsAllLettersOrDigits(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }

            foreach (char c in str)
            {
                if (!Char.IsLetterOrDigit(c))
                    return false;
            }

            return true;
        }
    }
}