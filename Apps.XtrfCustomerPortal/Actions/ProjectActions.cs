using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Projects;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search projects", Description = "Search projects based on search criteria")]
    public async Task<GetProjectsResponse> SearchProjects([ActionParameter] SearchProjectsRequest searchProjectsRequest)
    {
        var endpoint = "/projects?limit=50";

        if (searchProjectsRequest.Statuses != null && searchProjectsRequest.Statuses.Any())
        {
            foreach (var status in searchProjectsRequest.Statuses)
            {
                endpoint = endpoint.AddQueryParameter("status", status);
            }
        }

        if (!string.IsNullOrEmpty(searchProjectsRequest.SurveyStatus))
        {
            endpoint = endpoint.AddQueryParameter("surveyStatus", searchProjectsRequest.SurveyStatus);
        }

        if (!string.IsNullOrEmpty(searchProjectsRequest.Search))
        {
            endpoint = endpoint.AddQueryParameter("search", searchProjectsRequest.Search);
        }

        if (!string.IsNullOrEmpty(searchProjectsRequest.CustomerProjectNumber))
        {
            endpoint = endpoint.AddQueryParameter("customerProjectNumber", searchProjectsRequest.CustomerProjectNumber);
        }

        if (searchProjectsRequest.CreatedOnFrom.HasValue)
        {
            var createdOnFromMilliseconds =
                new DateTimeOffset(searchProjectsRequest.CreatedOnFrom.Value.ToDateTime(TimeOnly.MinValue)).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("startDateFrom", createdOnFromMilliseconds.ToString());
        }

        if (searchProjectsRequest.CreatedOnTo.HasValue)
        {
            var createdOnToMilliseconds =
                new DateTimeOffset(searchProjectsRequest.CreatedOnTo.Value.ToDateTime(TimeOnly.MinValue)).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("startDateTo", createdOnToMilliseconds.ToString());
        }

        if (searchProjectsRequest.ExpirationFrom.HasValue)
        {
            var expirationFromMilliseconds =
                new DateTimeOffset(searchProjectsRequest.ExpirationFrom.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("deadlineFrom", expirationFromMilliseconds.ToString());
        }

        if (searchProjectsRequest.ExpirationTo.HasValue)
        {
            var expirationToMilliseconds =
                new DateTimeOffset(searchProjectsRequest.ExpirationTo.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("deadlineTo", expirationToMilliseconds.ToString());
        }

        var projects = await FetchProjectsWithPagination(endpoint);
        return new GetProjectsResponse(projects);
    }

    [Action("Get project", Description = "Get project based on project ID")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var projectDto =
            await Client.ExecuteRequestAsync<ProjectDto>($"/projects/{projectIdentifier.ProjectId}", Method.Get, null);
        return new ProjectResponse(projectDto);
    }
    
    [Action("Create project", Description = "Create a new project")]
    public async Task<ProjectResponse> CreateProject([ActionParameter] CreateProjectRequest request)
    {
        var obj = new
        {
            name = request.ProjectName,
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
            notes = string.Empty,
            priceProfileId = int.Parse(request.PriceProfileId),
            personId = int.Parse(request.PersonId),
            sendBackToId = request.SendBackToId == null ? int.Parse(request.PersonId) : int.Parse(request.SendBackToId),
            additionalPersonIds = request.AdditionalPersonIds == null 
                ? new List<int>() 
                : request.AdditionalPersonIds.Select(int.Parse).ToList(),
            files = await UploadFilesAsync(request.Files, fileManagementClient),
            referenceFiles = request.ReferenceFiles == null 
                ? new List<FileUploadDto>() 
                : await UploadFilesAsync(request.ReferenceFiles, fileManagementClient),
            customFields = new List<string>(),
            officeId = request.OfficeId != null 
                ? int.Parse(request.OfficeId) 
                : (await GetDefaultOffice()).Id,
            budgetCode = request.BudgetCode ?? string.Empty,
            catToolType = request.CatToolType ?? "TRADOS"
        };

        var projectDto = await Client.ExecuteRequestAsync<ProjectDto>("/v2/projects", Method.Post, obj);
        return new ProjectResponse(projectDto);
    }
    
    [Action("Download project files", Description = "Download project translation files")]
    public async Task<DownloadProjectFilesResponse> DownloadProjectFiles([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var taskFilesDto = await Client.ExecuteRequestAsync<TaskFilesDto>($"/projects/{projectIdentifier.ProjectId}/files", Method.Get, null);
        var files = taskFilesDto.TasksFiles.SelectMany(x => x.Output?.Files ?? new List<TaskFilesDto.File>()).ToList();

        var fileReferences = new List<FileReference>();
        foreach (var file in files)
        {
            var invoicePdf = await Client.ExecuteRequestAsync($"/projects/files/{file.Id}", Method.Get, null, "application/octet-stream");
            var rawBytes = invoicePdf.RawBytes!;
            
            var stream = new MemoryStream(rawBytes);
            stream.Position = 0;
            
            var fileReference = await fileManagementClient.UploadAsync(stream, "application/octet-stream", file.Name);
            fileReferences.Add(fileReference);
        }
        
        return new DownloadProjectFilesResponse
        {
            Files = fileReferences
        };
    }
    
    private async Task<List<ProjectDto>> FetchProjectsWithPagination(string endpoint)
    {
        var allProjects = new List<ProjectDto>();
        int start = 0;

        while (true)
        {
            var paginatedEndpoint = $"{endpoint}&start={start}";
            var projects = await Client.ExecuteRequestAsync<List<ProjectDto>>(paginatedEndpoint, Method.Get, null);

            if (projects == null || projects.Count == 0)
            {
                break;
            }

            allProjects.AddRange(projects);
            start += projects.Count;

            if (projects.Count < 50)
            {
                break;
            }
        }

        return allProjects;
    }
}