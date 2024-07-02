using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.DataSources.Static;

public class ViewDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new()
        {
            {"UNPAID", "Unpaid"},
            {"OVERDUE", "Overdue"},
            {"EARLY_UNPAID", "Early unpaid"},
            {"PAID", "Paid"},
            {"ALL", "All"}
        };
    }
}