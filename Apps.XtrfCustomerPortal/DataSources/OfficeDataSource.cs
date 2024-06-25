using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.XtrfCustomerPortal.DataSources;

public class OfficeDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var offices = await Client.ExecuteRequestAsync<List<FullOfficeDto>>("/offices", Method.Get, null);
        return offices
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString))
            .ToDictionary(x => x.Id.ToString(), x => x.Name);
    }
}