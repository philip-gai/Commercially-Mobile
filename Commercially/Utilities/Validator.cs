using System;
using System.Text.RegularExpressions;

namespace Commercially
{
    public static class Validator
    {
        /* Email Requirements:
		 * - Has a name before the @
		 * - Has the @ symbol
		 * - Has a domain
		 * - Ends with . and 3 chars
		 */
        public static bool Email(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            try
            {
                string name = "^[a-zA-Z0-9._%+-]+";
                string at = "@";
                string domain = "[a-zA-Z0-9.+-]+";
                string dot = "\\.[a-zA-Z]{3}$";
                return Regex.IsMatch(email, name + at + domain + dot, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /* Password Requirements:
		 * - At least 8 characters
		 * - Cannot begin or end with a space
		 * - Must have at least 1 lower case letter
		 * - Must have at least 1 upper case letter
		 * - Must have at least 1 number
		 * - Must have at least 1 special character
		 * - Must have no bad characters
		 */
        public static bool Password(string password)
        {
            const int length = 8;
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }
            // Password must be at least length (8) chars long
            if (password.Length < length)
            {
                return false;
            }
            // Cannot begin or end with a space
            if (password[0] == ' ' || password[password.Length - 1] == ' ')
            {
                return false;
            }
            string lowerCasePattern = "[a-z]";
            string upperCasePattern = "[A-Z]";
            //  ! " # $ % & ‘ ( ) * + , - . / : ; < = > ? @ [ ] ^ { | } ~
            string specialCharPattern = "[!\"#$%&‘\\(\\)\\*\\+,\\-\\./:;<=>\\?@[\\]^\\{\\|\\}~]";
            bool hasLowerCase = Regex.Matches(password, lowerCasePattern).Count > 0;
            bool hasUpperCase = Regex.Matches(password, upperCasePattern).Count > 0;
            bool hasNumbers = Regex.Matches(password, "[\\d]").Count > 0;
            bool hasSpecialChars = Regex.Matches(password, specialCharPattern).Count > 0;
            bool hasBadChars = Regex.Matches(password, "[^\\d\\w!\"#$%&‘\\(\\)\\*\\+,\\-\\./:;<=>\\?@[\\]^\\{\\|\\}~]").Count > 0;
            return hasLowerCase && hasUpperCase && hasNumbers && hasSpecialChars && !hasBadChars;
        }
    }
}
