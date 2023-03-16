using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CUPP
{
    class Validator
    {
        private static readonly char[] SpecialChars = "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~".ToCharArray();

        public static bool Validate(string[] args, string password)
        {
            // set min and max length

            int minLength = 8, maxLength = 16;

            try
            {
                minLength = Convert.ToInt32(args[0]);
            }
            catch { }

            try
            {
                maxLength = Convert.ToInt32(args[1]);
            }
            catch { }

            // value for minimal checks required in the last position of the array

            int minChecksRequired = IsDigitsOnly(args[args.Length - 1]) ? Convert.ToInt32(args[args.Length - 1]) : 0;
            int minChecksVerified = 0;

            // validate length
            
            if (maxLength == 0)
                minChecksVerified++;
            else if (password.Length > minLength && password.Length <= maxLength)
                minChecksVerified++;

            // validate only numbers
            
            if (!IsDigitsOnly(password))
                minChecksVerified++;

            // validate only letters
            
            if (!IsLettersOnly(password))
                minChecksVerified++;

            // validate upper case letter(s)
            
            if (args.Contains("c"))
                if (!password.Equals(password.ToLower()))
                    minChecksVerified++;

            // validate special char(s)
            
            if (args.Contains("s"))
                if (ContainsSpecialChar(password))
                    minChecksVerified++;

            return minChecksVerified >= minChecksRequired;
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

        private static bool ContainsSpecialChar(string str)
        {
            int indexOf = str.IndexOfAny(SpecialChars);

            if (indexOf == -1)
                return false;

            return true;
        }
    }
}
