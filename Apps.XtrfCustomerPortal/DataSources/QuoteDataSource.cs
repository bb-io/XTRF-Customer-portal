using Apps.XtrfCustomerPortal.Actions;
using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XtrfCustomerPortal.DataSources;

public class QuoteDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var quoteActions = new QuoteActions(invocationContext, null!);

        var quotes = await quoteActions.SearchQuotes(new SearchQuotesRequest()
        {
            Search = context.SearchString ?? string.Empty
        });
        
        return quotes.Quotes
            .ToDictionary(x => x.QuoteId, x => x.QuoteName);
    }
}