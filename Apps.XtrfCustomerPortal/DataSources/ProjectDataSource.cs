using Apps.XtrfCustomerPortal.Actions;
using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.XtrfCustomerPortal.DataSources;

public class ProjectDataSource(InvocationContext invocationContext)
    : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var projectActions = new ProjectActions(invocationContext, null!);

        var quotes = await projectActions.SearchProjects(new SearchProjectsRequest()
        {
            Search = context.SearchString ?? string.Empty
        });
        
        return quotes.Projects
            .ToDictionary(x => x.ProjectId, x => x.ProjectName);
    }
}