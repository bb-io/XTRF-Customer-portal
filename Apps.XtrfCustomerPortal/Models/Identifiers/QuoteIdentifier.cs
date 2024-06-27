using Apps.XtrfCustomerPortal.DataSources;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.XtrfCustomerPortal.Models.Identifiers;

public class QuoteIdentifier
{
    [Display("Quote ID"), DataSource(typeof(QuoteDataSource))]
    public string QuoteId { get; set; }
}