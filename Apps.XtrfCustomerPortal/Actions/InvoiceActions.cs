using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Invoices;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Actions;

[ActionList("Invoices")]
public class InvoiceActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search invoices", Description = "Search invoices based on the provided criteria")]
    public async Task<GetInvoicesResponse> SearchInvoices([ActionParameter] SearchInvoicesRequest request)
    {
        var endpoint = "/invoices?limit=50";
    
        if (!string.IsNullOrEmpty(request.View))
        {
            endpoint = endpoint.AddQueryParameter("view", request.View);
        }
    
        if (!string.IsNullOrEmpty(request.Search))
        {
            endpoint = endpoint.AddQueryParameter("search", request.Search);
        }
    
        var invoices = await FetchInvoicesWithPagination(endpoint);
        invoices.Invoices = invoices.Invoices
            .Where(i => request.InvoiceDateFrom == null || i.ExpectedFullyPaidDate >= request.InvoiceDateFrom)
            .Where(i => request.InvoiceDateTo == null || i.ExpectedFullyPaidDate <= request.InvoiceDateTo)
            .ToList();
        
        return invoices;
    }
    
    [Action("Get invoice", Description = "Get a specific invoice by ID")]
    public async Task<InvoiceResponse> GetInvoice([ActionParameter] InvoiceIdentifier invoiceIdentifier)
    {
        var invoices = await SearchInvoices(new SearchInvoicesRequest());
        return invoices.Invoices.FirstOrDefault(i => i.InvoiceId == invoiceIdentifier.InvoiceId) 
            ?? throw new Exception($"Invoice with ID {invoiceIdentifier.InvoiceId} not found");
    }
    
    [Action("Download invoice", Description = "Download a specific invoice as a PDF")]
    public async Task<DownloadInvoiceResponse> DownloadInvoiceAsPdf([ActionParameter] InvoiceIdentifier invoiceIdentifier)
    {
        var invoicePdf = await Client.ExecuteRequestAsync($"/invoices/{invoiceIdentifier.InvoiceId}/document", Method.Get, null, "application/pdf");
        var rawBytes = invoicePdf.RawBytes!;
        var fileName = $"{invoiceIdentifier.InvoiceId}.pdf";
        
        var stream = new MemoryStream(rawBytes);
        stream.Position = 0;
        
        var fileReference = await fileManagementClient.UploadAsync(stream, "application/pdf", fileName);
        return new DownloadInvoiceResponse
        {
            File = fileReference
        };
    }
    
    private async Task<GetInvoicesResponse> FetchInvoicesWithPagination(string endpoint)
    {
        var allInvoices = new List<InvoiceDto>();
        int start = 0;

        while (true)
        {
            var paginatedEndpoint = $"{endpoint}&start={start}";
            var invoices = await Client.ExecuteRequestAsync<List<InvoiceDto>>(paginatedEndpoint, Method.Get, null);

            if (invoices == null || invoices.Count == 0)
            {
                break;
            }

            allInvoices.AddRange(invoices);
            start += invoices.Count;

            if (invoices.Count < 50)
            {
                break;
            }
        }

        return new GetInvoicesResponse(allInvoices);
    }
}