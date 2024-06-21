using System.Xml.Serialization;
using Apps.XtrfCustomerPortal.Constants;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Utilities;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Blackbird.Applications.Sdk.Utils.RestSharp;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Api;

public class ApiClient(List<AuthenticationCredentialsProvider> credentials) 
    : BlackBirdRestClient(new RestClientOptions(UrlHelper.BuildBaseUrl(credentials.Get(CredsNames.Host).Value)))
{
    private const string TokenKey = "XTRF-CP-Auth-Token";
    private const string JsessionCookie = "JSESSIONID";
    
    public async Task<T> ExecuteRequestAsync<T>(string endpoint, Method method, object? bodyObj)
    {
        var loginDto = await GetTokenAsync();
        var request = new RestRequest(endpoint, method)
            .AddHeader(TokenKey, loginDto.JSessionId);
        
        request.Resource = UrlHelper.BuildRequestUrl(Options.BaseUrl!.ToString(), endpoint, loginDto.JsessionCookie);
        if (bodyObj is not null)
        {
            request.WithJsonBody(bodyObj);
        }
        
        var response = await ExecuteWithErrorHandling<T>(request);
        return response;
    }
    
    private async Task<LoginDto> GetTokenAsync()
    {
        var username = credentials.Get(CredsNames.Username).Value;
        var password = credentials.Get(CredsNames.Password).Value;
        var request = new RestRequest("/system/login", Method.Post)
            .AddHeader("content-type", "application/x-www-form-urlencoded")
            .AddParameter("username", username)
            .AddParameter("password", password);
        
        var response = await ExecuteWithErrorHandling(request);
        
        var serializer = new XmlSerializer(typeof(LoginDto));
        using StringReader reader = new StringReader(response.Content!);
        var result = (LoginDto)serializer.Deserialize(reader)!;
        
        var cookie = response.Cookies?.FirstOrDefault(x => x.Name == JsessionCookie);
        return new LoginDto
        {
            JsessionCookie = cookie?.Value,
            JSessionId = result.JSessionId
        };
    }

    protected override Exception ConfigureErrorException(RestResponse response)
    {
        return new Exception($"Error message: {response.Content}; StatusCode: {response.StatusCode}");
    }
}