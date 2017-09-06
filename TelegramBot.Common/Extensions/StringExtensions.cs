using System;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace TelegramBot.Common.Extensions{
    public static class StringExtensions{
        private static readonly Regex EmailExpression =
            new Regex(@"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$",
                RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex WebUrlExpression = new Regex(
            @"(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?",
            RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        private static readonly Regex StripHtmlExpression = new Regex("<\\S[^><]*>",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant |
            RegexOptions.Compiled);
        private static readonly Regex BlockNameExpression = new Regex(@"name:([a-zA-Z0-9 ])+",
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Compiled);
        private static readonly Regex CellPhoneExpression =
            new Regex(@"(0|\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}");
        private static  readonly Regex PersianDateFormat = new Regex(@"\d{4}(?:/\d{1,2}){2}");
        private static readonly Regex NumberExpression = new Regex(@"^\d+$",
            RegexOptions.Singleline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        [DebuggerStepThrough]
        public static string FormatWith(this string instance, params object[] args){
            Check.Argument.IsNotNullOrEmpty(instance, "instance");
            return string.Format(CultureInfo.CurrentUICulture, instance, args);
        }
        [DebuggerStepThrough]
        public static string Hash(this string instance){
            Check.Argument.IsNotNullOrEmpty(instance, "instance");
            using (var md5 = MD5.Create()){
                var data = Encoding.Unicode.GetBytes(instance);
                var hash = md5.ComputeHash(data);
                return Convert.ToBase64String(hash);
            }
        }
        [DebuggerStepThrough]
        public static T ToEnum<T>(this string instance, T defaultValue) where T : struct, IComparable, IFormattable{
            var convertedValue = defaultValue;
            if (!string.IsNullOrWhiteSpace(instance) && !Enum.TryParse(instance.Trim(), true, out convertedValue)) {
                convertedValue = defaultValue;
            }
            return convertedValue;
        }
        [DebuggerStepThrough]
        public static string StripHtml(this string instance){
            Check.Argument.IsNotNullOrEmpty(instance, "instance");
            return StripHtmlExpression.Replace(instance, string.Empty);
        }
        [DebuggerStepThrough]
        public static bool IsEmail(this string instance){
            return !string.IsNullOrWhiteSpace(instance) && EmailExpression.IsMatch(instance);
        }
        [DebuggerStepThrough]
        public static bool IsCellPhone(this string instance){
            return !string.IsNullOrWhiteSpace(instance) && CellPhoneExpression.IsMatch(instance);
        }
        public static bool IsValidNationalId(this string instance){
            if (string.IsNullOrEmpty(instance)) return false;
            if (!NumberExpression.IsMatch(instance)) return false;
            if (instance.Length != 10) return false;
            if (instance == "1111111111" || instance == "0000000000" || instance == "2222222222" ||
                instance == "3333333333" || instance == "4444444444" || instance == "5555555555" ||
                instance == "6666666666" || instance == "7777777777" || instance == "8888888888" ||
                instance == "9999999999") return false;
            var c = int.Parse(instance.Substring(instance.Length - 1));
            var n = 0;
            for (var i = 0; i <= 8; i++) { n += int.Parse(instance[i].ToString())*(10 - i); }
            var r = n - (n/11)*11;
            if ((r == 0 && r == c) || (r == 1 && c == 1) || (r > 1 && c == 11 - r)) return true;
            return false;
        }

        public static bool IsValidPersianDateFormat(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance) && PersianDateFormat.IsMatch(instance);
        }

        [DebuggerStepThrough]
        public static string ConvertToValidCellPhone(this string instance){
            if (instance.StartsWith("09")) { instance = instance.Remove(0, 1); }
            return "98" + instance.TrimEnd();
        }
        [DebuggerStepThrough]
        public static bool IsWebUrl(this string instance){
            return !string.IsNullOrWhiteSpace(instance) && WebUrlExpression.IsMatch(instance);
        }
        [DebuggerStepThrough]
        public static bool IsIpAddress(this string instance){
            IPAddress ip;
            return !string.IsNullOrWhiteSpace(instance) && IPAddress.TryParse(instance, out ip);
        }
        [DebuggerStepThrough]
        public static string ToSlug(this string value){
            if (String.IsNullOrEmpty(value)) return string.Empty;
            value = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
            value = value.Replace("-", "");
            value = value.ToAlphaNumeric().ToSingleSpace(); // remove invalid chars
            //value = value.Replace(" ", ""); // remove all spaces
            value = value.Replace(" ", "-"); // replace spaces
            value = value.Substring(0, value.Length <= 170 ? value.Length : 170).Trim(); // cut and trim it
            return value.ToLower();
        }
        [DebuggerStepThrough]
        public static string ToAlphaNumeric(this string value){
            return Regex.Replace(value, @"[^a-zA-Z0-9\s-]", "");
        }
        [DebuggerStepThrough]
        public static string ToSingleSpace(this string value){
            return Regex.Replace(value, @"\s+", " ").Trim();
        }
        [DebuggerStepThrough]
        public static string ToHumanFromPascal(this string s){
            if (2 > s.Length) { return s; }
            var sb = new StringBuilder();
            var ca = s.ToCharArray();
            sb.Append(ca[0]);
            for (var i = 1; i < ca.Length - 1; i++){
                var c = ca[i];
                if (char.IsUpper(c) && (char.IsLower(ca[i + 1]) || char.IsLower(ca[i - 1]))) { sb.Append(' '); }
                sb.Append(c);
            }
            sb.Append(ca[ca.Length - 1]);
            return sb.ToString();
        }
        public static string ToHumanFromSlug(this string s){
            if (2 > s.Length) { return s; }
            var sb = new StringBuilder();
            var ca = s.ToCharArray();
            for (var i = 0; i < ca.Length; i++){
                var c = ca[i];
                if (i == 0) c = char.ToUpper(c);
                if (char.IsLower(c) && i > 0 && ca[i - 1] == '-' && c != 'a') { c = char.ToUpper(c); }
                sb.Append(c);
            }
            return sb.ToString().Replace("-", " ");
        }
        public static string Nl2Br(this string value){
            return value.Replace("\n", "<br />").Trim();
        }
        public static string Truncate(this string value){
            if (value.Length > 30){
                value = value.Substring(0, 30);
                var lastSpace = value.LastIndexOf(" ");
                if (lastSpace > 0) value = value.Substring(0, lastSpace);
                value = value + "...";
            }
            return value;
        }

        //public static string ToHash(this string value, string salt)
        //{
        //    var cryptographer = ObjectFactory.GetInstance<ICryptographer>();
        //    return cryptographer.GetHashFromKeyAndSalt(value, salt);
        //}
        public static string Append(this string target, string value){
            return target + value;
        }
        public static string GetBlockName(this string str){
            return BlockNameExpression.Match(str).ToString().Split(':')[1];
        }
        public static string NormalizeWhitespaceToSingleSpaces(this string s){
            return Regex.Replace(s, @"\s+", " ");
        }
        public static string StripFileExtension(this string target){
            return target.Substring(0, target.LastIndexOf('.'));
        }
        public static string SafeFarsiStr(this string input)
        {
            return input.Replace("ي", "ی").Replace("ك", "ک");
        }
        //حذف اعراب عربی
        public static string RemoveDiacritics(this string input)
        {
            var normalizedString = input.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
                else
                {
                    //اسامی مانند آفتاب نباید خراب شوند
                    if (c == 1619) //آ
                    {
                        stringBuilder.Append(c);
                    }
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}