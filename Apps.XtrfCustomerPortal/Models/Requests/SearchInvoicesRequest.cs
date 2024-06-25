using Blackbird.Applications.Sdk.Common;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class SearchInvoicesRequest
{
    public string? View { get; set; }
    
    [Display("Search (Final number, draft number or accountancy person)")]
    public string? Search { get; set; }
    
    [Display("Invoice date from")]
    public DateTime? InvoiceDateFrom { get; set; }
    
    [Display("Invoice date to")]
    public DateTime? InvoiceDateTo { get; set; }
}