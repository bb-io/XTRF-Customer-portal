using Blackbird.Applications.Sdk.Common;

namespace Apps.XtrfCustomerPortal;

public class Application : IApplication
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
}