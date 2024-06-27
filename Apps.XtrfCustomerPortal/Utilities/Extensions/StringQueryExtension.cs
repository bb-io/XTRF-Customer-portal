namespace Apps.XtrfCustomerPortal.Utilities.Extensions;

public static class StringQueryExtension
{
    public static string AddQueryParameter(this string url, string key, string value)
    {
        if (string.IsNullOrEmpty(value))
            return url;

        var separator = url.Contains("?") ? "&" : "?";
        return $"{url}{separator}{Uri.EscapeDataString(key)}={Uri.EscapeDataString(value)}";
    }
}