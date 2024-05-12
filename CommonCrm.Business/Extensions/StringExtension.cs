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
    public static List<string> GetCityFromTurkey()
    {
        List<string> cities = new List<string>
        {
            "Adana",
            "Adıyaman",
            "Afyonkarahisar",
            "Ağrı",
            "Amasya",
            "Ankara",
            "Antalya",
            "Artvin",
            "Aydın",
            "Balıkesir",
            "Bilecik",
            "Bingöl",
            "Bitlis",
            "Bolu",
            "Burdur",
            "Bursa",
            "Çanakkale",
            "Çankırı",
            "Çorum",
            "Denizli",
            "Diyarbakır",
            "Edirne",
            "Elazığ",
            "Erzincan",
            "Erzurum",
            "Eskişehir",
            "Gaziantep",
            "Giresun",
            "Gümüşhane",
            "Hakkari",
            "Hatay",
            "Isparta",
            "Mersin",
            "İstanbul",
            "İzmir",
            "Kars",
            "Kastamonu",
            "Kayseri",
            "Kırklareli",
            "Kırşehir",
            "Kocaeli",
            "Konya",
            "Kütahya",
            "Malatya",
            "Manisa",
            "Kahramanmaraş",
            "Mardin",
            "Muğla",
            "Muş",
            "Nevşehir",
            "Niğde",
            "Ordu",
            "Rize",
            "Sakarya",
            "Samsun",
            "Siirt",
            "Sinop",
            "Sivas",
            "Tekirdağ",
            "Tokat",
            "Trabzon",
            "Tunceli",
            "Şanlıurfa",
            "Uşak",
            "Van",
            "Yozgat",
            "Zonguldak",
            "Aksaray",
            "Bayburt",
            "Karaman",
            "Kırıkkale",
            "Batman",
            "Şırnak",
            "Bartın",
            "Ardahan",
            "Iğdır",
            "Yalova",
            "Karabük",
            "Kilis",
            "Osmaniye",
            "Düzce"
        };

        return cities;
    }


}