using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.DataSources.Static;

public class SurveyStatusDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            { "SURVEYED", "Surveyed" },
            { "NOT_SURVEYED", "Not surveyed" },
            { "ANY", "Any" }
        };
    }
}