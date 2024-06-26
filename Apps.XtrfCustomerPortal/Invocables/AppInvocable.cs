using Apps.XtrfCustomerPortal.Api;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Invocables;

public class AppInvocable : BaseInvocable
{
    protected AuthenticationCredentialsProvider[] Creds =>
        InvocationContext.AuthenticationCredentialsProviders.ToArray();

    protected ApiClient Client { get; }
    
    public AppInvocable(InvocationContext invocationContext) : base(invocationContext)
    {
        Client = new(Creds.ToList());
    }
    
    protected async Task<List<FileUploadDto>> UploadFilesAsync(IEnumerable<FileReference> files, IFileManagementClient fileManagementClient)
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

    public async Task<FullOfficeDto> GetDefaultOffice()
    {
        var officeDto = await Client.ExecuteRequestAsync<FullOfficeDto>($"/offices/default", Method.Get, null);
        return officeDto;
    }
    
    public async Task<FullOfficeDto> GetOfficeById(string officeId)
    {
        var officeDto = await Client.ExecuteRequestAsync<FullOfficeDto>($"/offices/{officeId}", Method.Get, null);
        return officeDto;
    }
}