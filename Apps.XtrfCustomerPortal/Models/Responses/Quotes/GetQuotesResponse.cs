using Apps.XtrfCustomerPortal.Models.Dtos;

namespace Apps.XtrfCustomerPortal.Models.Responses.Quotes;

public class GetQuotesResponse
{
    public List<QuoteResponse> Quotes { get; set; }

    public GetQuotesResponse(List<QuoteDto> quotes)
    {
        Quotes = quotes.Select(q => new QuoteResponse(q)).ToList();
    }
}