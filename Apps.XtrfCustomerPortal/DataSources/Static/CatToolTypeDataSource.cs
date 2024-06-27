using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.DataSources.Static;

public class CatToolTypeDataSource : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData()
    {
        return new Dictionary<string, string>
        {
            {"MEMSOURCE", "Memsource"},
            {"XTM", "XTM"},
            {"TRADOS", "Trados"},
            {"MEMOQ", "MemoQ"}
        };
    }
}