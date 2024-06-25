using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Quotes;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
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
    public async Task<GetQuotesResponse> SearchQuotes(SearchQuotesRequest searchQuotesRequest)
    {
        var endpoint = "/quotes?limit=50";
        if (!string.IsNullOrEmpty(searchQuotesRequest.Status))
        {
            endpoint = endpoint.AddQueryParameter("status", searchQuotesRequest.Status);
        }
        
        var searchParameters = new List<string>();
        if (!string.IsNullOrEmpty(searchQuotesRequest.RefNumber))
        {
            searchParameters.Add(searchQuotesRequest.RefNumber);
        }

        if (!string.IsNullOrEmpty(searchQuotesRequest.IdNumber))
        {
            searchParameters.Add(searchQuotesRequest.IdNumber);
        }

        if (!string.IsNullOrEmpty(searchQuotesRequest.Name))
        {
            searchParameters.Add(searchQuotesRequest.Name);
        }

        if (searchParameters.Count > 1)
        {
            throw new ArgumentException("Please provide only one search parameter (RefNumber, IdNumber, or Name).");
        }
        
        if (searchParameters.Count == 1)
        {
            endpoint = endpoint.AddQueryParameter("search", searchParameters[0]);
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
    public async Task<QuoteDto> GetQuote(QuoteIdentifier quoteIdentifier)
    {
        var quote = await Client.ExecuteRequestAsync<QuoteDto>($"/quotes/{quoteIdentifier.QuoteId}", Method.Get, null);
        return quote;
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
    
    public async Task<List<FileUploadDto>> UploadFileAsync(FileReference file)
    {
        var stream = await fileManagementClient.DownloadAsync(file);
        var bytes = await stream.GetByteData();

        var response = await Client.UploadFileAsync<List<FileUploadDto>>("/system/session/files", bytes, file.Name);
        return response;
    }

    public async Task CreateQuote()
    {
        var files = await UploadFileAsync(new FileReference()
        {
            Name = "test.txt",
            ContentType = "text/plain",
        });

        var obj = new
        {
            name = "Test project #1",
            customerProjectNumber = "1207",
            serviceId = 5,
            sourceLanguageId = 74,
            targetLanguageIds = new[] { 240 },
            specializationId = 1,
            deliveryDate = new
            {
                time = DateTimeOffset.UtcNow.AddHours(24).ToUnixTimeMilliseconds(),
            },
            notes = "test-note",
            priceProfileId = 47,
            personId = 18,
            sendBackToId = 18,
            additionalPersonIds = new List<int>(),
            files = files,
            referenceFiles = new List<FileUploadDto>(),
            customFields = new[]
            {
                new
                {
                    key = "custom_field_1",
                    value = "custom_value_1"
                }
            },
            officeId = 47,
            budgetCode = "BUDGET2024",
            catToolType = "TRADOS"
        };
        
        var json = JsonConvert.SerializeObject(obj, Formatting.Indented);
        var response = await Client.ExecuteRequestAsync<object>("/v2/quotes", Method.Post, obj);

        await Task.Delay(1000);
    }
}