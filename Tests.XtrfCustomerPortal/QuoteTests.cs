using Apps.XtrfCustomerPortal.Actions;
using Apps.XtrfCustomerPortal.Models.Requests;
using XtrfCustomerPortalTests.Base;

namespace Tests.XtrfCustomerPortal
{
    [TestClass]
    public class QuoteTests : TestBase
    {
        [TestMethod]
        public async Task SearchQuotes_IsSucces()
        {
            var action = new QuoteActions(InvocationContext,FileManager);

            var response = await action.SearchQuotes(new SearchQuotesRequest()
            {
                //Status = "New",
                //Search = "Test",
                //CreatedOnFrom = DateTime.Now.AddDays(-1),
                //CreatedOnTo = DateTime.Now.AddDays(1),
                //ExpirationFrom = DateTime.Now.AddDays(-1),
                //ExpirationTo = DateTime.Now.AddDays(1)
            });

            foreach (var quote in response.Quotes)
            {
                Console.WriteLine(quote.QuoteId);
                Assert.IsNotNull(quote.QuoteId);
            }
        }
    }
}

