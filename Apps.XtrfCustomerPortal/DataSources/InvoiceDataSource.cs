using Apps.XtrfCustomerPortal.Actions;
using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XtrfCustomerPortal.DataSources;

public class InvoiceDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var quoteActions = new InvoiceActions(invocationContext, null!);

        var quotes = await quoteActions.SearchInvoices(new SearchInvoicesRequest
        {
            Search = context.SearchString ?? string.Empty
        });
        
        return quotes.Invoices
            .ToDictionary(x => x.InvoiceId, x => x.FinalNumber);
    }
}