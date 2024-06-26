using Apps.XtrfCustomerPortal.Invocables;
using Apps.XtrfCustomerPortal.Models.Dtos;
using Apps.XtrfCustomerPortal.Models.Identifiers;
using Apps.XtrfCustomerPortal.Models.Requests;
using Apps.XtrfCustomerPortal.Models.Responses.Projects;
using Apps.XtrfCustomerPortal.Utilities.Extensions;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;

namespace Apps.XtrfCustomerPortal.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search projects", Description = "Search projects")]
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
                new DateTimeOffset(searchProjectsRequest.CreatedOnFrom.Value).ToUnixTimeMilliseconds();
            endpoint = endpoint.AddQueryParameter("startDateFrom", createdOnFromMilliseconds.ToString());
        }

        if (searchProjectsRequest.CreatedOnTo.HasValue)
        {
            var createdOnToMilliseconds =
                new DateTimeOffset(searchProjectsRequest.CreatedOnTo.Value).ToUnixTimeMilliseconds();
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

    [Action("Get project", Description = "Get project based on project identifier")]
    public async Task<ProjectResponse> GetProject([ActionParameter] ProjectIdentifier projectIdentifier)
    {
        var projectDto =
            await Client.ExecuteRequestAsync<ProjectDto>($"/projects/{projectIdentifier.ProjectId}", Method.Get, null);
        return new ProjectResponse(projectDto);
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