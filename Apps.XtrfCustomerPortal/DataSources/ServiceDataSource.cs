using Apps.XtrfCustomerPortal.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XtrfCustomerPortal.DataSources;

public class ServiceDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var services = await Client.ExecuteRequestAsync<List<ServiceDto>>("/system/values/services", Method.Get, null);
        return services
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}

public class ServiceDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public string LocalizedName { get; set; }
    
    public string Type { get; set; }
}