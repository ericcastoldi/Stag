﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace Stag.Utility
{
    public static class StringExtensions
    {
        private static CultureInfo DefaultCulture = new CultureInfo("pt-BR");

        public static string GenerateSlug(this string phrase)
        {
            if (string.IsNullOrWhiteSpace(phrase))
            {
                return string.Empty;
            }

            string str = phrase.Trim().RemoveAccent().ToLower(DefaultCulture);

            // invalid chars
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            str = Regex.Replace(str, @"\s+", " ").Trim();

            str = Regex.Replace(str, @"\s", "-"); // hyphens

            str = Regex.Replace(str, @"([-_]){2,}", "$1", RegexOptions.Compiled);

            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}