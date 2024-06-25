using Apps.XtrfCustomerPortal.Models.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.XtrfCustomerPortal.Models.Responses.Invoices;

/*
 * [
  {
    "id": 5,
    "draftNumber": null,
    "finalNumber": "2024/1",
    "creditNoteNumber": null,
    "currency": "€",
    "totalBrutto": 0,
    "totalNetto": 0,
    "formattedTotalBrutto": "€0.00",
    "formattedTotalNetto": "€0.00",
    "charges": [
      {
        "value": 0,
        "paidValue": 0,
        "dueDate": {
          "formatted": "2024-07-02",
          "millisGMT": 1719914400000
        }
      }
    ],
    "expectedFullyPaidDate": {
      "formatted": "2024-07-02",
      "millisGMT": 1719914400000
    },
    "paymentState": "Fully Paid",
    "overdue": false,
    "notPaid": false,
    "documents": [
      {
        "id": 820,
        "name": "Built-in-Client Invoice (PDF) (en)",
        "format": "pdf"
      }
    ],
    "office": {
      "name": "Vitalii"
    }
  }
]
 */
public class InvoiceResponse
{
    [Display("Invoice ID")]
    public string InvoiceId { get; set; }
    
    [Display("Final number")] 
    public string FinalNumber { get; set; }

    public string Currency { get; set; }
    
    [Display("Total brutto")]
    public double TotalBrutto { get; set; }
    
    [Display("Total netto")]
    public double TotalNetto { get; set; }
    
    [Display("Formatted total brutto")]
    public string FormattedTotalBrutto { get; set; }
    
    [Display("Formatted total netto")]
    public string FormattedTotalNetto { get; set; }
    
    public List<ChargeResponse> Charges { get; set; }
    
    [Display("Expected fully paid date")]
    public DateTime ExpectedFullyPaidDate { get; set; }
    
    [Display("Payment state")]
    public string PaymentState { get; set; }
    
    public bool Overdue { get; set; }
    
    [Display("Not paid")]
    public bool NotPaid { get; set; }

    public InvoiceResponse(InvoiceDto dto)
    {
        InvoiceId = dto.Id.ToString();
        FinalNumber = dto.FinalNumber;
        Currency = dto.Currency;
        TotalBrutto = (double)dto.TotalBrutto;
        TotalNetto = (double)dto.TotalNetto;
        FormattedTotalBrutto = dto.FormattedTotalBrutto;
        FormattedTotalNetto = dto.FormattedTotalNetto;
        Charges = dto.Charges?.Select(c => new ChargeResponse
        {
            Value = (double)c.Value,
            PaidValue = (double)c.PaidValue,
            DueDate = DateTime.Parse(c.DueDate.Formatted)
        }).ToList() ?? new List<ChargeResponse>();
        ExpectedFullyPaidDate = dto.ExpectedFullyPaidDate != null 
          ? DateTime.Parse(dto.ExpectedFullyPaidDate.Formatted)
          : DateTime.MinValue;
        PaymentState = dto.PaymentState;
        Overdue = dto.Overdue;
        NotPaid = dto.NotPaid;
    }
}

public class ChargeResponse
{
    public double Value { get; set; }
    
    [Display("Paid value")]
    public double PaidValue { get; set; }
    
    [Display("Due date")]
    public DateTime DueDate { get; set; }
}