using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.DataSources.Static;

public class QuoteStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "REQUESTED", "Requested" },
            { "PENDING", "Pending" },
            { "SENT", "Sent" },
            { "ACCEPTED", "Accepted" },
            { "ACCEPTED_BY_CUSTOMER", "Accepted by customer" },
            { "REJECTED", "Rejected" },
            { "SPLITTED", "Splitted" }
        };
    }
}