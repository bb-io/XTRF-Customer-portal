using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Quotes;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Actions;

[ActionList]
public class QuoteActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search quotes", Description = "Search quotes based on the provided criteria")]
    public async Task<GetQuotesResponse> SearchQuotes([ActionParameter] SearchQuotesRequest searchQuotesRequest)
    {
        var endpoint = "/quotes?limit=50";
        if (!string.IsNullOrEmpty(searchQuotesRequest.Status))
        {
            endpoint = endpoint.AddQueryParameter("status", searchQuotesRequest.Status);
        }
        
        if (!string.IsNullOrEmpty(searchQuotesRequest.Search))
        {
            endpoint = endpoint.AddQueryParameter("search", searchQuotesRequest.Search);
        }
        
        var quotes = await FetchQuotesWithPagination(endpoint);

        quotes.Quotes = quotes.Quotes
            .Where(x => searchQuotesRequest.CreatedOnFrom == null || x.StartDate >= searchQuotesRequest.CreatedOnFrom)
            .Where(x => searchQuotesRequest.CreatedOnTo == null || x.StartDate <= searchQuotesRequest.CreatedOnTo)
            .Where(x => searchQuotesRequest.ExpirationFrom == null || x.Deadline >= searchQuotesRequest.ExpirationFrom)
            .Where(x => searchQuotesRequest.ExpirationTo == null || x.Deadline <= searchQuotesRequest.ExpirationTo)
            .ToList();
        
        return quotes;
    }
    
    [Action("Get quote", Description = "Get a specific quote by Quote ID")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var quoteDto = await Client.ExecuteRequestAsync<QuoteDto>($"/quotes/{quoteIdentifier.QuoteId}", Method.Get, null);
        return new(quoteDto);
    }
    
    [Action("Create quote", Description = "Create a new quote based on the provided data")]
    public async Task<QuoteResponse> CreateQuote([ActionParameter] QuoteCreateRequest request)
    {
        var obj = new
        {
            name = request.QuoteName,
            customerProjectNumber = request.CustomerProjectNumber,
            serviceId = int.Parse(request.ServiceId),
            sourceLanguageId = int.Parse(request.SourceLanguageId),
            targetLanguageIds = request.TargetLanguageIds == null 
                ? new List<int>() 
                : request.TargetLanguageIds.Select(int.Parse).ToList(),
            specializationId = int.Parse(request.SpecializationId),
            deliveryDate = new
            {
                time = request.DeliveryDate.HasValue 
                    ? new DateTimeOffset(request.DeliveryDate.Value).ToUnixTimeMilliseconds() 
                    : DateTime.Now.AddDays(7).ToUnixTimeMilliseconds()
            },
            notes = string.Empty,
            priceProfileId = int.Parse(request.PriceProfileId),
            personId = int.Parse(request.PersonId),
            sendBackToId = request.SendBackToId == null ? int.Parse(request.PersonId) : int.Parse(request.SendBackToId),
            additionalPersonIds = request.AdditionalPersonIds == null 
                ? new List<int>() 
                : request.AdditionalPersonIds.Select(int.Parse).ToList(),
            files = request.Files == null 
                ? new List<FileUploadDto>() 
                : await UploadFilesAsync(request.Files, fileManagementClient),
            referenceFiles = request.ReferenceFiles == null 
                ? new List<FileUploadDto>() 
                : await UploadFilesAsync(request.ReferenceFiles, fileManagementClient),
            customFields = new List<string>(),
            officeId = request.OfficeId != null 
                ? int.Parse(request.OfficeId) 
                : (await GetDefaultOffice()).Id,
            budgetCode = request.BudgetCode ?? string.Empty,
            catToolType = request.CatToolType ?? "TRADOS"
        };
        
        var quoteDto = await Client.ExecuteRequestAsync<QuoteDto>("/v2/quotes", Method.Post, obj);
        return new(quoteDto);
    }
    
    private async Task<GetQuotesResponse> FetchQuotesWithPagination(string endpoint)
    {
        var allQuotes = new List<QuoteDto>();
        int start = 0;

        while (true)
        {
            var paginatedEndpoint = $"{endpoint}&start={start}";
            var quotes = await Client.ExecuteRequestAsync<List<QuoteDto>>(paginatedEndpoint, Method.Get, null);

            if (quotes == null || quotes.Count == 0)
            {
                break;
            }

            allQuotes.AddRange(quotes);
            start += quotes.Count;

            if (quotes.Count < 50)
            {
                break;
            }
        }

        return new GetQuotesResponse(allQuotes);
    }
}