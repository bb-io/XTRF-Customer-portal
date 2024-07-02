using Apps.XtrfCustomerPortal.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class SearchProjectsRequest
{
    [StaticDataSource(typeof(ProjectStatusDataSource))]
    public IEnumerable<string>? Statuses { get; set; }
    
    [Display("Survey status"), StaticDataSource(typeof(SurveyStatusDataSource))]
    public string? SurveyStatus { get; set; }

    [Display("Search (Project ID, ID number, reference number, or project name)")]
    public string? Search { get; set; }
    
    [Display("Customer's project number")]
    public string? CustomerProjectNumber { get; set; }
    
    [Display("Created on from")]
    public DateTime? CreatedOnFrom { get; set; }
    
    [Display("Created on to")]
    public DateTime? CreatedOnTo { get; set; }
    
    [Display("Expiration from")]
    public DateTime? ExpirationFrom { get; set; }
    
    [Display("Expiration to")]
    public DateTime? ExpirationTo { get; set; }
}