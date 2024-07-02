using Apps.XtrfCustomerPortal.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class SearchInvoicesRequest
{
    [StaticDataSource(typeof(ViewDataSource))]
    public string? View { get; set; }
    
    [Display("Search (Final number, draft number or accountancy person)")]
    public string? Search { get; set; }
    
    [Display("Invoice date from")]
    public DateTime? InvoiceDateFrom { get; set; }
    
    [Display("Invoice date to")]
    public DateTime? InvoiceDateTo { get; set; }
}