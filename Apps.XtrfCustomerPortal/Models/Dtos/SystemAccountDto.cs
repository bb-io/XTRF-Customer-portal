using Newtonsoft.Json;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class SystemAccountDto
{
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("email")]
    public string Email { get; set; }
}