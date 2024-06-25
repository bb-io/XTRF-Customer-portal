namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class InvoiceDto
{
    public long Id { get; set; }
    public string FinalNumber { get; set; } = string.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal TotalBrutto { get; set; }
    public decimal TotalNetto { get; set; }
    public string FormattedTotalBrutto { get; set; } = string.Empty;
    public string FormattedTotalNetto { get; set; } = string.Empty;
    public List<ChargeDto> Charges { get; set; } = new();
    public ExpectedFullyPaidDateDto? ExpectedFullyPaidDate { get; set; }
    public string PaymentState { get; set; } = string.Empty;
    public bool Overdue { get; set; }
    public bool NotPaid { get; set; }
    
    public List<DocumentDto> Documents { get; set; } = new();
    
    public OfficeDto Office { get; set; } = new();
}

public class ChargeDto
{
    public decimal Value { get; set; }
    public decimal PaidValue { get; set; }
    public DateDto DueDate { get; set; }
}

public class DateDto
{
    public string Formatted { get; set; }
    public long MillisGMT { get; set; }
}

public class ExpectedFullyPaidDateDto
{
    public string Formatted { get; set; }
    public long MillisGMT { get; set; }
}

public class DocumentDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Format { get; set; }
}

public class OfficeDto
{
    public string Name { get; set; }
}