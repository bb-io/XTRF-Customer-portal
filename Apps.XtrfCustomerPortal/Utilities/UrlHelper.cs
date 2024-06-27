namespace Apps.XtrfCustomerPortal.Utilities;

public class UrlHelper
{
    public static string BuildBaseUrl(string host)
    {
        return $"https://{host}/customer-api";
    }
    
    public static string BuildRequestUrl(string baseUrl, string endpoint, string jsessionCookie)
    {
        var uriBuilder = new UriBuilder(baseUrl + endpoint);
        var path = uriBuilder.Path;
        var query = uriBuilder.Query;

        path += $";jsessionid={jsessionCookie}";
        uriBuilder.Path = path;
        if (!string.IsNullOrEmpty(query))
        {
            uriBuilder.Query = query.TrimStart('?');
        }

        var updatedEndpoint = uriBuilder.ToString();
        return updatedEndpoint;
    }
}