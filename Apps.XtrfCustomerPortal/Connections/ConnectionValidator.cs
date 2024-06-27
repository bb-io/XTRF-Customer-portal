using Apps.XtrfCustomerPortal.Api;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Connections;

public class ConnectionValidator : IConnectionValidator
{
    public async ValueTask<ConnectionValidationResponse> ValidateConnection(
        IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProviders,
        CancellationToken cancellationToken)
    {
        try
        {
            var client = new ApiClient(authenticationCredentialsProviders.ToList());
            await client.ExecuteRequestAsync<SystemAccountDto>("/system/account", Method.Get, null);
            return new ConnectionValidationResponse()
            {
                IsValid = true
            };
        }
        catch (Exception e)
        {
            return new ConnectionValidationResponse()
            {
                IsValid = false,
                Message = e.Message
            };
        }
    }
}