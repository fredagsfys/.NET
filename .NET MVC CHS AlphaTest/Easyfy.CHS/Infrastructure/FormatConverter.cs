using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Easyfy.CHS.Infrastructure
{
    public class FormatConverter
    {
        // Capitalizes the first letter in a string
        public static string StringToChange(string s, bool isName)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            // Remove white spaces
            s = s.Trim();

            // Remove redundant white spaces between words
            s = Regex.Replace(s, @"\s+", " ");

            // Format ABCDE to
            if (isName == false)
            {
                s = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s.ToLower());
            }

            return s;
        }

        // Split the zipcode info format 123 45
        public static string NumberToChange(string s)
        {
            var firstPart = s.Substring(0, 3);
            var secondPart = s.Substring(3, 2);

            return firstPart + " " + secondPart;
        }
    }
}