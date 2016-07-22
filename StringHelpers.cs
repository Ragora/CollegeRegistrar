using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace RegistrarConsole
{
    /// <summary>
    /// Helper static class that contains helper methods for dealing with various types of string data.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// A regular expression pattern representing an area code.
        /// </summary>
        private const string AreaCodePattern = "\\x28[0-9]{1,3}\\x29";

        /// <summary>
        /// A regular expression pattern representing a whole phone number.
        /// </summary>
        private const string PhoneNumberPattern = "[0-9]{1,3}-" + AreaCodePattern + "[0-9]{3}-[0-9]{4}";

        /// <summary>
        /// A regular expression pattern representing an email address.
        /// </summary>
        private const string EMailPattern = "([A-z]|[0-9]|[._])+@([A-z]|[0-9]|[._])+([A-z]+)*.(net|edu|com|mil)";

        /// <summary>
        /// Helper method that formats the given input as an email and returns it.
        /// </summary>
        /// <param name="input">The input string to format.</param>
        /// <returns>The result of the format.</returns>
        public static bool FormatEMail(string input, out string result)
        {
            Match match = Regex.Match(input, EMailPattern);

            result = match.Success && match.Index == 0 && match.Length == input.Length ? input : "<Please enter a valid EMail>";
            return match.Success;
        }

        public static bool FormatPhoneNumber(string input, out string result)
        {
            input = input.Replace('.', '-');
            Match phoneMatch = Regex.Match(input, PhoneNumberPattern);

            if (!phoneMatch.Success)
            {
                // Modify for area code
                string[] components = input.Split('-');

                if (components.Length == 3)
                {
                    if (!Regex.Match(components[0], AreaCodePattern).Success)
                        components[0] = string.Format("({0})", components[0]);

                    input = string.Format("1-{0}{1}-{2}", components[0], components[1],
                        components[2]);
                }
                else if (components.Length == 4)
                    input = string.Format("{0}-({1}){2}-{3}", components[0], components[1],
                        components[2], components[3]);
                else
                {
                    result = "<Please enter a valid Phone Number>";
                    return false;
                }
            }
            else
            {
                result = input;
                return true;
            }

            phoneMatch = Regex.Match(input, PhoneNumberPattern);

            if (phoneMatch.Success && phoneMatch.Index == 0)
            {
                result = input;
                return true;
            }
            else if (!phoneMatch.Success && input.Length == 10)
            {
                result = string.Format("{0}{1}-{2}", input.Substring(0, 3),
                    input.Substring(3, 3), input.Substring(6, 4));
                return true;
            }

            result = "<Please enter a Valid Phone Number>";
            return false;
        }
    }
}
