using Apps.XtrfCustomerPortal.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class SearchQuotesRequest
{
    [StaticDataSource(typeof(StatusDataSource))]
    public string? Status { get; set; }

    [Display("Search (Reference number, ID number, or quote name)")]
    public string? Search { get; set; }
    
    [Display("Created on from")]
    public DateTime? CreatedOnFrom { get; set; }
    
    [Display("Created on to")]
    public DateTime? CreatedOnTo { get; set; }
    
    [Display("Expiration from")]
    public DateTime? ExpirationFrom { get; set; }
    
    [Display("Expiration to")]
    public DateTime? ExpirationTo { get; set; }
}