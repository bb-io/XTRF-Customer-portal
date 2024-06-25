using Apps.XtrfCustomerPortal.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XtrfCustomerPortal.DataSources;

public class LanguageDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var languages = await Client.ExecuteRequestAsync<List<LanguageDto>>("/system/values/services", Method.Get, null);
        return languages
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}

public class LanguageDto
{
    public string Id { get; set; }
    
    public string Symbol { get; set; }
    
    public string Name { get; set; }
    
    public string DisplayName { get; set; }
}