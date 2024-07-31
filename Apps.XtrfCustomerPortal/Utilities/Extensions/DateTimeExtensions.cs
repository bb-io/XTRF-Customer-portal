using System.Globalization;

namespace Apps.XtrfCustomerPortal.Utilities.Extensions;

public static class DateTimeExtensions
{
    public static long ToUnixTimeMilliseconds(this DateTime dateTime)
    {
        return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
    }
    
    public static DateTime ParseDate(this string? dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return DateTime.MinValue;
        }

        const string format = "yyyy-MM-dd HH:mm zzz";
        dateString = dateString.Replace("CEST", "+02:00").Replace("CET", "+01:00");

        if (DateTimeOffset.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset parsedDate))
        {
            return parsedDate.DateTime.ToUniversalTime();
        }

        return DateTime.MinValue;
    }
}