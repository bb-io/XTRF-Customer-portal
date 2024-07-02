using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.DataSources.Static;

public class ProjectStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "OPENED", "Opened" },
            { "CLOSED", "Closed" },
            { "CLAIM", "Claim" },
            { "CANCELLED", "Cancelled" },
            { "REQUESTED_PROJECT", "Requested"}
        };
    }
}