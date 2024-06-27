using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.XtrfCustomerPortal;

public class Application : IApplication, ICategoryProvider
{
    public string Name
    {
        get => "App";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ApplicationCategory> Categories
    {
        get
        {
            return new[]
            {
                ApplicationCategory.LspPortal,
                ApplicationCategory.CatAndTms
            };
        }
        set { }
    }
}