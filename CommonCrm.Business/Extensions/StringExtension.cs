using System.Globalization;
using Microsoft.IdentityModel.Tokens;

namespace CommonCrm.Business.Extensions;

public static class StringExtension
{
    public static string ToOneLineString<T>(this IEnumerable<T> list, Func<T, string> fn = null, string separator = null,
        string emptyValue = "N/A", Func<T, bool> preFn = null)
    {
        if (list.IsNullOrEmpty())
        {
            return emptyValue;
        }
        fn ??= x => x?.ToString();
        preFn ??= x => true;
        separator ??= ", ";
        return string.Join(separator, list.Where(preFn).Select(x => x is null ? emptyValue : fn(x)));
    }
    public static string ToOneLineStringJustNotNulls<T>(this IEnumerable<T> list, Func<T, string> fn = null, string separator = null,
        string emptyValue = "N/A", Func<T, bool> preFn = null)
    {
        return list?.Where(x => x != null).ToOneLineString(fn, separator, emptyValue,preFn);
    }
    public static string CurrencyFormatWithDollarSign(this decimal d) => d.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")).Replace("$", "$");
    public static string CurrencyFormatWithTlSignForDollar(this decimal d) => d.ToString("C2", CultureInfo.CreateSpecificCulture("en-US")).Replace("₺", "₺");

    public static string? CurrencyFormatWithTlSign(this decimal d) => d.ToString("C2", CultureInfo.CreateSpecificCulture("tr-TR")).Replace("\u20ba", "\u20ba");
    public static string CurrencyFormatWithEuroSign(this decimal d) => d.ToString("C2", CultureInfo.CreateSpecificCulture("eu-ES")).Replace("\u20ac", "\u20ac").Trim();
    public static string CurrencyFormatWithTlSignForEuro(this decimal d) => d.ToString("C2", CultureInfo.CreateSpecificCulture("eu-ES")).Replace("₺", "₺").Trim();

    public static bool IsNullOrEmpty<T>(this IEnumerable<T> l)
    {
        return l == null || !l.Any();
    }
    public static string RemoveTurkishCharacters(string input)
    {
        string[] turkishChars = { "Ğ", "ğ", "Ü", "ü", "Ş", "ş", "İ", "ı", "Ö", "ö", "Ç", "ç", " ", "ı", "İ" };
        string[] englishChars = { "G", "g", "U", "u", "S", "s", "I", "i", "O", "o", "C", "c", "", "i", "i" };

        for (int i = 0; i < turkishChars.Length; i++)
        {
            input = input.Replace(turkishChars[i], englishChars[i]);
        }

        return input;
    }


}