using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.XtrfCustomerPortal.Models.Responses.Projects;

public class DownloadProjectFilesResponse
{
    public List<FileReference> Files { get; set; } = new();
}