using Newtonsoft.Json;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class JsonErrorDto
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("errorMessage")]
    public string ErrorMessage { get; set; }
    
    [JsonProperty("detailedMessage")]
    public string DetailedMessage { get; set; }
}