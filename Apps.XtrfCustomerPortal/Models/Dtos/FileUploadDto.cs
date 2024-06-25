using Newtonsoft.Json;

namespace Apps.XtrfCustomerPortal.Models.Dtos;

/*
 * {
    "id": 0,
    "name": "string",
    "size": 0,
    "fileStats": {
      "charactersWithSpaces": 0,
      "charactersWithoutSpaces": 0,
      "words": 0,
      "lines": 0,
      "pages": 0
    },
    "url": "string",
    "delete_url": "string",
    "delete_type": "string"
  }
 */
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