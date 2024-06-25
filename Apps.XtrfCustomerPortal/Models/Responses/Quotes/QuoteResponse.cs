using System.Globalization;
using Apps.XtrfCustomerPortal.Models.Dtos;
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
    
    [Display("Language combinations")] public List<LanguageCombinationQuoteResponse> LanguageCombinations { get; set; }
    
    [Display("Start date")] public DateTime StartDate { get; set; } = new();
    
    [Display("Deadline")] public DateTime Deadline { get; set; } = new();
    
    [Display("Office")] public string Office { get; set; }
    
    [Display("Customer notes")] public string CustomerNotes { get; set; } 
    
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
        LanguageCombinations = quoteDtoDto.LanguageCombinations.Select(lc => new LanguageCombinationQuoteResponse
        {
            SourceLanguage = lc.SourceLanguage.Symbol,
            TargetLanguage = lc.TargetLanguage.Symbol
        }).ToList();
        StartDate = ParseDate(quoteDtoDto.StartDate?.Formatted);
        Deadline = ParseDate(quoteDtoDto.Deadline?.Formatted); 
        Office = quoteDtoDto.Office.Name;
        CustomerNotes = quoteDtoDto.CustomerNotes;
        Status = quoteDtoDto.Status;
        SalesPersonId = quoteDtoDto.SalesPerson?.Id.ToString() ?? string.Empty;
        SalesPersonName = quoteDtoDto.SalesPerson?.Name ?? string.Empty;
    }
    
    // Example: 2023-12-20 08:30 CET
    private DateTime ParseDate(string? dateString)
    {
        if (string.IsNullOrEmpty(dateString))
        {
            return DateTime.MinValue;
        }

        const string format = "yyyy-MM-dd HH:mm 'CET'";
        if (DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
        {
            return parsedDate;
        }

        return DateTime.MinValue;
    }
}

public class LanguageCombinationQuoteResponse
{
    [Display("Source language")] public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language")] public string TargetLanguage { get; set; } = string.Empty;
}