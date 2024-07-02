using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Responses.Quotes;
using Blackbird.Applications.Sdk.Common;
using Apps.XtrfCustomerPortal.Utilities.Extensions;

namespace Apps.XtrfCustomerPortal.Models.Responses.Projects;

public class ProjectResponse
{
    [Display("Project ID")]
    public string ProjectId { get; set; }
    
    [Display("Project name")]
    public string ProjectName { get; set; }

    [Display("ID number")]
    public string IdNumber { get; set; }
    
    [Display("Reference number")]
    public string? RefNumber { get; set; }
    
    [Display("Total agreed amount")]
    public double TotalAgreed { get; set; }
    
    [Display("Total agreed currency")]
    public string Currency { get; set; }
    
    [Display("Total agreed formatted amount")]
    public string FormattedAmount { get; set; }

    public string Service { get; set; }
    
    public string? Workflow { get; set; }
    
    public string Specialization { get; set; }
    
    [Display("Language combinations")]
    public List<LanguageCombinationResponse> LanguageCombinations { get; set; }
    
    [Display("Start date")]
    public DateTime StartDate { get; set; }
    
    public DateTime Deadline { get; set; }
    
    public string Status { get; set; }

    [Display("Is project")]
    public bool IsProject { get; set; }
    
    [Display("Budget code")]
    public string? BudgetCode { get; set; }
    
    [Display("Has output files")]
    public bool HasOutputFiles { get; set; }
    
    [Display("Awaiting customer review")]
    public bool AwaitingCustomerReview { get; set; }

    public ProjectResponse(ProjectDto dto)
    {
        ProjectId = dto.Id.ToString();
        ProjectName = dto.Name;
        IdNumber = dto.IdNumber;
        RefNumber = dto.RefNumber;
        TotalAgreed = (double)dto.TotalAgreed.Amount;
        Currency = dto.TotalAgreed.Currency;
        FormattedAmount = dto.TotalAgreed.FormattedAmount;
        Service = dto.Service;
        Workflow = dto.Workflow;
        Specialization = dto.Specialization;
        LanguageCombinations = dto.LanguageCombinations.Select(lc => new LanguageCombinationResponse
        {
            SourceLanguage = lc.SourceLanguage.Symbol,
            TargetLanguage = lc.TargetLanguage.Symbol
        }).ToList();
        StartDate = dto.StartDate?.Formatted.ParseDate() ?? DateTime.MinValue;
        Deadline = dto.Deadline?.Formatted.ParseDate() ?? DateTime.MinValue;
        Status = dto.Status;
        IsProject = dto.IsProject;
        BudgetCode = dto.BudgetCode;
        HasOutputFiles = dto.HasOutputFiles;
        AwaitingCustomerReview = dto.AwaitingCustomerReview;
    }
}