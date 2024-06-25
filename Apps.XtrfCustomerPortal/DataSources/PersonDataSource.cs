using Apps.XtrfCustomerPortal.Actions;
using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Requests;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XtrfCustomerPortal.DataSources;

public class PersonDataSource(InvocationContext invocationContext, [ActionParameter] QuoteCreateRequest request)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var quoteActions = new QuoteActions(invocationContext, null!);
        FullOfficeDto officeDto;
        if (string.IsNullOrEmpty(request.OfficeId))
        {
            officeDto = await quoteActions.GetDefaultOffice();
        }
        else
        {
            officeDto = await quoteActions.GetOfficeById(request.OfficeId);
        }

        return officeDto.Persons
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.Name.Contains(context.SearchString))
            .ToDictionary(x => x.Id.ToString(), x => x.Name);
    }
}