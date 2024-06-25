using Apps.XtrfCustomerPortal.Invocables;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XtrfCustomerPortal.DataSources;

public class SpecializationDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var specializationDtos = await Client.ExecuteRequestAsync<List<SpecializationDto>>("/system/values/services", Method.Get, null);
        return specializationDtos
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.Id, x => x.Name);
    }
}
 
 public class SpecializationDto
 {
     public string Id { get; set; }
     
     public string Name { get; set; }
     
     public string LocalizedName { get; set; }
 }