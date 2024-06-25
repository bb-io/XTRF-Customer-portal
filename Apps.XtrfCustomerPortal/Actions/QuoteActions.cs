using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Quotes;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Actions;

[ActionList]
public class QuoteActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search quotes", Description = "Search quotes")]
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
        
        if (searchQuotesRequest.CreatedOnFrom.HasValue)
        {
            var createdOnFromMilliseconds = new DateTimeOffset(searchQuotesRequest.CreatedOnFrom.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("createdOnFrom", createdOnFromMilliseconds.ToString());
        }

        if (searchQuotesRequest.CreatedOnTo.HasValue)
        {
            var createdOnToMilliseconds = new DateTimeOffset(searchQuotesRequest.CreatedOnTo.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("createdOnTo", createdOnToMilliseconds.ToString());
        }

        if (searchQuotesRequest.ExpirationFrom.HasValue)
        {
            var expirationFromMilliseconds = new DateTimeOffset(searchQuotesRequest.ExpirationFrom.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("expirationDateFrom", expirationFromMilliseconds.ToString());
        }

        if (searchQuotesRequest.ExpirationTo.HasValue)
        {
            var expirationToMilliseconds = new DateTimeOffset(searchQuotesRequest.ExpirationTo.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("expirationDateTo", expirationToMilliseconds.ToString());
        }
        
        var quotes = await FetchQuotesWithPagination(endpoint);
        return quotes;
    }
    
    [Action("Get quote", Description = "Get a specific quote")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] QuoteIdentifier quoteIdentifier)
    {
        var quoteDto = await Client.ExecuteRequestAsync<QuoteDto>($"/quotes/{quoteIdentifier.QuoteId}", Method.Get, null);
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
    
    private async Task<List<FileUploadDto>> UploadFilesAsync(IEnumerable<FileReference> files)
    {
        var fileUploadDtos = new List<FileUploadDto>();
        foreach (var file in files)
        {
            var stream = await fileManagementClient.DownloadAsync(file);
            var bytes = await stream.GetByteData();

            var response = await Client.UploadFileAsync<List<FileUploadDto>>("/system/session/files", bytes, file.Name);
            fileUploadDtos.AddRange(response);
        }
        
        return fileUploadDtos;
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
            targetLanguageIds = request.TargetLanguageIds.Select(int.Parse).ToList(),
            specializationId = int.Parse(request.SpecializationId),
            deliveryDate = new
            {
                time = request.DeliveryDate.HasValue 
                    ? new DateTimeOffset(request.DeliveryDate.Value).ToUnixTimeMilliseconds() 
                    : DateTime.Now.AddDays(7).ToUnixTimeMilliseconds()
            },
            notes = request.Note ?? string.Empty,
            priceProfileId = int.Parse(request.PriceProfileId),
            personId = int.Parse(request.PersonId),
            sendBackToId = request.SendBackToId == null ? int.Parse(request.PersonId) : int.Parse(request.SendBackToId),
            additionalPersonIds = request.AdditionalPersonIds == null 
                ? new List<int>() 
                : request.AdditionalPersonIds.Select(int.Parse).ToList(),
            files = await UploadFilesAsync(request.Files),
            referenceFiles = request.ReferenceFiles == null 
                ? new List<FileUploadDto>() 
                : await UploadFilesAsync(request.ReferenceFiles),
            customFields = new List<string>(),
            officeId = int.Parse(request.OfficeId),
            budgetCode = request.BudgetCode ?? string.Empty,
            catToolType = request.CatToolType ?? "TRADOS",
        };
        
        var quoteDto = await Client.ExecuteRequestAsync<QuoteDto>("/v2/quotes", Method.Post, obj);
        return new(quoteDto);
    }
}