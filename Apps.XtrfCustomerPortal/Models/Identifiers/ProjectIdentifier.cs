using Apps.XtrfCustomerPortal.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XtrfCustomerPortal.Models.Identifiers;

public class ProjectIdentifier
{
    [Display("Project ID"), DataSource((typeof(ProjectDataSource)))]
    public string ProjectId { get; set; }
}