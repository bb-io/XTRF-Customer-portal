using Apps.XtrfCustomerPortal.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XtrfCustomerPortal.Models.Identifiers;

public class InvoiceIdentifier
{
    [Display("Invoice ID"), DataSource(typeof(InvoiceDataSource))]
    public string InvoiceId { get; set; }
}