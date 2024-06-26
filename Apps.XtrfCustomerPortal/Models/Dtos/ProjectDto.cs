namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class ProjectDto
{
    public int Id { get; set; }
    
    public string IdNumber { get; set; }
    
    public string? RefNumber { get; set; }
    
    public string Name { get; set; }
    
    public AmountDetail TotalAgreed { get; set; }
    
    public AmountDetail TmSavings { get; set; }
    
    public string Service { get; set; }
    
    public string? Workflow { get; set; }
    
    public string Specialization { get; set; }
    
    public List<LanguageCombinationDto> LanguageCombinations { get; set; }
    
    public DateDto StartDate { get; set; }
    
    public DateDto? Deadline { get; set; }
    
    public OfficeDto Office { get; set; }
    
    public string CustomerNotes { get; set; }
    
    public string Status { get; set; }
    
    public ProjectManagerDto ProjectManager { get; set; }
    
    public bool IsProject { get; set; }
    
    public string? BudgetCode { get; set; }
    
    public bool ProjectConfirmationAvailable { get; set; }
    
    public DateTime? ActualDeliveryDate { get; set; }
    
    public bool HasInputWorkfiles { get; set; }
    
    public bool HasInputResources { get; set; }
    
    public bool HasOutputFiles { get; set; }
    
    public bool AwaitingCustomerReview { get; set; }
}

public class ProjectManagerDto
{
    public int Id { get; set; }
    
    public string Type { get; set; }
    
    public string Name { get; set; }
    
    public string Position { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public ContactDto Contact { get; set; }
}