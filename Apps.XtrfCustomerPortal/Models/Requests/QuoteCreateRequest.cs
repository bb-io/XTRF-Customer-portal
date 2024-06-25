using Apps.XtrfCustomerPortal.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class QuoteCreateRequest
{
    [Display("Quote name")]
    public string QuoteName { get; set; }
    
    [Display("Customer project number")]
    public string? CustomerProjectNumber { get; set; }
    
    public IEnumerable<FileReference> Files { get; set; }

    [Display("Service ID")]
    public string ServiceId { get; set; }
    
    [Display("Source language ID")]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language IDs")]
    public IEnumerable<string> TargetLanguageIds { get; set; }
    
    [Display("Specialization ID")]
    public string SpecializationId { get; set; }

    [Display("Delivery date", Description = "By default, the delivery date is set to the current date + 7 days.")]
    public DateTime? DeliveryDate { get; set; }

    [Display("Note")]
    public string? Note { get; set; }

    [Display("Price profile ID")]
    public string PriceProfileId { get; set; }

    [Display("Person ID")]
    public string PersonId { get; set; }

    [Display("Send back to ID")]
    public string? SendBackToId { get; set; }

    [Display("Additional person IDs")]
    public IEnumerable<string>? AdditionalPersonIds { get; set; }
    
    [Display("Additional email addresses")]
    public IEnumerable<FileReference>? ReferenceFiles { get; set; }

    [Display("Office ID")]
    public string OfficeId { get; set; }
    
    [Display("Budget code")]
    public string? BudgetCode { get; set; }

    [Display("Cat tool type", Description = "By default, the value is set to 'Trados'."), StaticDataSource(typeof(CatToolTypeDataSource))]
    public string? CatToolType { get; set; }
}