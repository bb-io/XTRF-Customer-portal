using Newtonsoft.Json;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

public class FileUploadDto
{
    [JsonProperty("id")]
    public string Id { get; set; }
    
    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("size")]
    public int Size { get; set; }
    
    [JsonProperty("fileStats")]
    public FileStats FileStats { get; set; }
    
    [JsonProperty("url")]
    public string Url { get; set; }
    
    [JsonProperty("delete_url")]
    public string DeleteUrl { get; set; }
    
    [JsonProperty("delete_type")]
    public string DeleteType { get; set; }
}

public class FileStats
{
    [JsonProperty("charactersWithSpaces")]
    public int CharactersWithSpaces { get; set; }
    
    [JsonProperty("charactersWithoutSpaces")]
    public int CharactersWithoutSpaces { get; set; }
    
    [JsonProperty("words")]
    public int Words { get; set; }
    
    [JsonProperty("lines")]
    public int Lines { get; set; }
    
    [JsonProperty("pages")]
    public int Pages { get; set; }
}