using Apps.XtrfCustomerPortal.Models.Dtos;

namespace Apps.XtrfCustomerPortal.Models.Responses.Invoices;

public class GetInvoicesResponse
{
    public List<InvoiceResponse> Invoices { get; set; }
    
    public GetInvoicesResponse(List<InvoiceDto> invoices)
    {
        Invoices = invoices.Select(i => new InvoiceResponse(i)).ToList();
    }
}