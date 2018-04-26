using System;
using System.Collections.Generic;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Dapper.Infrastructure.Extensions
{
    public enum EncryptType
    {
        Md5
    }

    public static class StringExtension
    {
        private static readonly string unreservedChars =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~";

        public static bool IsEmail(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            var regularExpression =
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(value.Trim(), regularExpression, RegexOptions.IgnoreCase);
        }

        public static string Encrypt(this string source, EncryptType encryptType)
        {
            switch (encryptType)
            {
                case EncryptType.Md5:
                    return Md5(source);
                default:
                    return source;
            }
        }

        private static string Md5(string source)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(source);
            var hash = md5.ComputeHash(inputBytes);
            var result = new StringBuilder();
            foreach (var b in hash)
                result.Append(b.ToString("X2"));
            return result.ToString();
        }

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static string GetFileName(this string source)
        {
            var elements = source.Split(new[] { "/", "\\" }, StringSplitOptions.RemoveEmptyEntries);
            return elements[elements.Length - 1];
        }

        public static string ReplaceUnicode(this string strInput, string replaceSpace = "-")
        {
            if (string.IsNullOrEmpty(strInput))
                return strInput;
            for (var i = 32; i < 48; i++)
            {
                strInput = strInput.Replace(((char)i).ToString(), " ");
            }
            strInput = strInput.Replace(".", replaceSpace);
            strInput = strInput.Replace(" ", replaceSpace);
            strInput = strInput.Replace(",", replaceSpace);
            strInput = strInput.Replace(";", replaceSpace);
            strInput = strInput.Replace(":", replaceSpace);
            strInput = strInput.Replace("|", replaceSpace);
            strInput = strInput.Replace("--", replaceSpace);
            strInput = strInput.Replace("?", replaceSpace);
            var regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            var strFormD = strInput.Normalize(NormalizationForm.FormD).ToLower();
            return
                regex.Replace(strFormD, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ReplaceMultiSpaces();
        }

        public static string ReplaceMultiSpaces(this string value)
        {
            var options = RegexOptions.None;
            var regex = new Regex("[ ]{2,}", options);
            value = regex.Replace(value, " ");
            return value;
        }

        public static string RemoveParameters(this string value)
        {
            var values = value.Split('?');
            if (values.Length > 1)
                return values[0];
            return value;
        }

        public static int ParseToInt(this string src, int defaultValue = 0)
        {
            int value;
            var isNumeric = int.TryParse(src, out value);
            if (isNumeric)
                return value;
            return defaultValue;
        }

        public static int ParseToInt(this object src, int defaultValue = 0)
        {
            int value;
            var isNumeric = int.TryParse(src.ToString(), out value);
            if (isNumeric)
                return value;
            return defaultValue;
        }

        public static long ParseToLong(this string source)
        {
            long value;
            return long.TryParse(source, out value) ? value : value;
        }

        public static decimal ParseToDecimal(this string source)
        {
            decimal value;
            return decimal.TryParse(source, out value) ? value : value;
        }

        public static string ConvertToCurrency(this double value)
        {
            return value.ToString("N0");
        }

        public static string GenerateRandom(int length = 8)
        {
            var random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length).Select(x => x[random.Next(x.Length)]).ToArray());
        }

        public static string UrlEncode(this string value)
        {
            var result = new StringBuilder();

            foreach (var symbol in value)
            {
                if (unreservedChars.IndexOf(symbol) != -1)
                {
                    result.Append(symbol);
                }
                else
                {
                    result.Append('%' + $"{(int)symbol:X2}");
                }
            }

            return result.ToString();
        }

        public static string UppercaseWords(this string value)
        {
            var array = value.ToCharArray();
            // Handle the first letter in the string.
            if (array.Length >= 1)
            {
                if (char.IsLower(array[0]))
                {
                    array[0] = char.ToUpper(array[0]);
                }
            }
            // Scan through the letters, checking for spaces.
            // ... Uppercase the lowercase letters following spaces.
            for (var i = 1; i < array.Length; i++)
            {
                if (array[i - 1] == ' ')
                {
                    if (char.IsLower(array[i]))
                    {
                        array[i] = char.ToUpper(array[i]);
                    }
                }
            }
            return new string(array);
        }

        public static string ToStringInvariant(this double? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        public static string ToStringGeo(this double? value)
        {
            if (value.HasValue)
            {
                return value.Value.ToString(CultureInfo.InvariantCulture);
            }

            return string.Empty;
        }

        public static string ToTitleCase(this object value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToString());
        }

        /// <summary>
        /// Return int second
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ConvertStringToTimeLength(this string value)
        {
            var valueArraySplited = value.Split(':');
            switch (valueArraySplited.Length)
            {
                case 1:
                    return valueArraySplited[0].ParseToInt();
                case 2:
                    return valueArraySplited[0].ParseToInt() * 60 + valueArraySplited[1].ParseToInt();
                case 3:
                    return valueArraySplited[0].ParseToInt() * 60 + valueArraySplited[1].ParseToInt() * 60 + valueArraySplited[2].ParseToInt();
            }

            return 0;
        }


        private static string GetIndexByIndexString(string title, string indexString)
        {
            if (title.ToLower().Contains(indexString))
            {
                var endString = title
                    .Remove(0, title.ToLower().IndexOf(indexString, StringComparison.InvariantCulture))
                    .ToLower()
                    .Replace(indexString, "").Trim();
                if (string.IsNullOrEmpty(endString))
                    return null;
                if (endString.Contains(" "))
                {
                    endString = endString.Split(' ')[0];
                }

                var indexValue = endString.ParseToInt();
                return indexValue.ToString();
            }

            return null;
        }

        public static string GetIndex(this string title)
        {
            if (string.IsNullOrEmpty(title))
                return null;
            var titleUnicode = title.ReplaceUnicode(" ");
            var result = GetIndexByIndexString(title.ReplaceUnicode(" "), "tap");
            if (string.IsNullOrEmpty(result))
                result = GetIndexByIndexString(title, "#");

            return result;
        }

        public static string ConvertStringToBase64(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        public static string ConvertBase64ToString(this string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string DecodeHtml(this string value)
        {
            return HttpUtility.HtmlDecode(value);
        }

        public static string ConvertToPlural(this string value)
        {
            var ps = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en-us"));
            return !ps.IsPlural(value) ? ps.Pluralize(value) : value;
        }
    }
}
