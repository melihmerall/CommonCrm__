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
}