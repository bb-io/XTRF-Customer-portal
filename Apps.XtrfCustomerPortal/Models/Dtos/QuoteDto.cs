namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class QuoteDto
{
    public int Id { get; set; }
    public string IdNumber { get; set; } = string.Empty;
    public string RefNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public AmountDetail TotalAgreed { get; set; } = new();
    public AmountDetail TmSavings { get; set; } = new();
    public string Service { get; set; } = string.Empty;
    public string Workflow { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public List<LanguageCombinationDto> LanguageCombinations { get; set; } = new();
    public Date StartDate { get; set; } = new();
    public Date Deadline { get; set; } = new();
    public Office Office { get; set; } = new();
    public string CustomerNotes { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public SalesPerson SalesPerson { get; set; } = new();
}

public class AmountDetail
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string FormattedAmount { get; set; } = string.Empty;
}

public class LanguageCombinationDto
{
    public Language SourceLanguage { get; set; } = new();
    public Language TargetLanguage { get; set; } = new();
    public bool HasAssociatedTask { get; set; }
}

public class Language
{
    public int? Id { get; set; }
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
}

public class Date
{
    public string Formatted { get; set; } = string.Empty;
    public long MillisGMT { get; set; }
}

public class Office
{
    public string Name { get; set; } = string.Empty;
}

public class SalesPerson
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public ContactQuote Contact { get; set; } = new();
}

public class ContactQuote
{
    public List<string> Phones { get; set; } = new();
    public string Mobile { get; set; } = string.Empty;
    public bool SmsEnabled { get; set; } 
    public string Fax { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Www { get; set; } = string.Empty;
    public List<SocialMediaContactDto> SocialMediaContacts { get; set; } = new();
}

public class SocialMediaContactDto
{
    public int Id { get; set; }
    public SocialMedia SocialMedia { get; set; } = new();
    public string Contact { get; set; } = string.Empty;
}

public class SocialMedia
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string AddressPrefix { get; set; } = string.Empty;
    public string AddressSuffix { get; set; } = string.Empty;
}