using Apps.XtrfCustomerPortal.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.XtrfCustomerPortal.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public List<ConnectionProperty> ConnectionProperties => new()
    {
        new ConnectionProperty(CredsNames.Host)
        {
            DisplayName = "Host",
            Description = "The URL of the XTRF Customer Portal API. Example: organization-test.s.xtrf.eu"
        },
        new ConnectionProperty(CredsNames.Username)
        {
            DisplayName = "Username" 
        },
        new ConnectionProperty(CredsNames.Password)
        {
            DisplayName = "Password",
            Sensitive = true
        }
    };
    
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = ConnectionProperties
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        return values.Select(x =>
            new AuthenticationCredentialsProvider(AuthenticationCredentialsRequestLocation.Body, x.Key, x.Value));
    }
}