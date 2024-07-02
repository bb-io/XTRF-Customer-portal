using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XtrfCustomerPortal.Models.Responses.Quotes;

public class QuoteResponse
{
    [Display("Quote ID")] public string QuoteId { get; set; }
    
    [Display("Quote ID number")] public string QuoteIdNumber { get; set; }
    
    [Display("Reference number")] public string ReferenceNumber { get; set; } 
    
    [Display("Quote name")] public string QuoteName { get; set; }
    
    [Display("Amount")] public double Amount { get; set; }
    
    [Display("Currency")] public string Currency { get; set; }
    
    [Display("Formatted amount")] public string FormattedAmount { get; set; } 
    
    [Display("Service")] public string Service { get; set; } 
    
    [Display("Workflow")] public string Workflow { get; set; }
    
    [Display("Specialization")] public string Specialization { get; set; }
    
    [Display("Language combinations")] public List<LanguageCombinationResponse> LanguageCombinations { get; set; }
    
    [Display("Start date")] public DateTime StartDate { get; set; } = new();
    
    [Display("Deadline")] public DateTime Deadline { get; set; } = new();
    
    [Display("Office")] public string Office { get; set; }
    
    [Display("Status")] public string Status { get; set; } 
    
    [Display("Sales person ID")] public string SalesPersonId { get; set; }
    
    [Display("Sales person name")] public string SalesPersonName { get; set; }

    public QuoteResponse(QuoteDto quoteDtoDto)
    {
        QuoteId = quoteDtoDto.Id.ToString();
        QuoteIdNumber = quoteDtoDto.IdNumber;
        ReferenceNumber = quoteDtoDto.RefNumber;
        QuoteName = quoteDtoDto.Name;
        Amount = (double)quoteDtoDto.TotalAgreed.Amount;
        Currency = quoteDtoDto.TotalAgreed.Currency;
        FormattedAmount = quoteDtoDto.TotalAgreed.FormattedAmount;
        Service = quoteDtoDto.Service;
        Workflow = quoteDtoDto.Workflow;
        Specialization = quoteDtoDto.Specialization;
        LanguageCombinations = quoteDtoDto.LanguageCombinations.Select(lc => new LanguageCombinationResponse
        {
            SourceLanguage = lc.SourceLanguage.Symbol,
            TargetLanguage = lc.TargetLanguage.Symbol
        }).ToList();
        StartDate = quoteDtoDto.StartDate?.Formatted.ParseDate() ?? DateTime.MinValue;
        Deadline = quoteDtoDto.Deadline?.Formatted.ParseDate() ?? DateTime.MinValue;
        Office = quoteDtoDto.Office.Name;
        Status = quoteDtoDto.Status;
        SalesPersonId = quoteDtoDto.SalesPerson?.Id.ToString() ?? string.Empty;
        SalesPersonName = quoteDtoDto.SalesPerson?.Name ?? string.Empty;
    }
}

public class LanguageCombinationResponse
{
    [Display("Source language")] public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language")] public string TargetLanguage { get; set; } = string.Empty;
}