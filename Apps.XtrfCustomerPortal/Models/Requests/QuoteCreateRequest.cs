using Apps.XtrfCustomerPortal.DataSources;
using Apps.XtrfCustomerPortal.DataSources.Static;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XtrfCustomerPortal.Models.Requests;

public class QuoteCreateRequest
{
    [Display("Quote name")]
    public string QuoteName { get; set; }
    
    [Display("Customer project number")]
    public string? CustomerProjectNumber { get; set; }
    
    public IEnumerable<FileReference>? Files { get; set; }

    [Display("Service ID"), DataSource(typeof(ServiceDataSource))]
    public string ServiceId { get; set; }
    
    [Display("Source language ID"), DataSource(typeof(LanguageDataSource))]
    public string SourceLanguageId { get; set; }
    
    [Display("Target language IDs"), DataSource(typeof(LanguageDataSource))]
    public IEnumerable<string>? TargetLanguageIds { get; set; }
    
    [Display("Specialization ID"), DataSource(typeof(SpecializationDataSource))]
    public string SpecializationId { get; set; }

    [Display("Delivery date", Description = "By default, the delivery date is set to the current date + 7 days.")]
    public DateTime? DeliveryDate { get; set; }

    [Display("Note")]
    public string? Note { get; set; }

    [Display("Price profile ID"), DataSource(typeof(PriceProfileDataSource))]
    public string PriceProfileId { get; set; }

    [Display("Person ID"), DataSource(typeof(PersonDataSource))]
    public string PersonId { get; set; }

    [Display("Send back to ID"), DataSource(typeof(PersonDataSource))]
    public string? SendBackToId { get; set; }

    [Display("Additional person IDs"), DataSource(typeof(PersonDataSource))]
    public IEnumerable<string>? AdditionalPersonIds { get; set; }
    
    [Display("Reference files")]
    public IEnumerable<FileReference>? ReferenceFiles { get; set; }

    [Display("Office ID"), DataSource(typeof(OfficeDataSource))]
    public string? OfficeId { get; set; }
    
    [Display("Budget code")]
    public string? BudgetCode { get; set; }

    [Display("Cat tool type", Description = "By default, the value is set to 'Trados'."), StaticDataSource(typeof(CatToolTypeDataSource))]
    public string? CatToolType { get; set; }
}