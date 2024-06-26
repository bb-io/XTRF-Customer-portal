using Apps.XtrfCustomerPortal.Models.Dtos;

namespace Apps.XtrfCustomerPortal.Models.Responses.Projects;

public class GetProjectsResponse
{
    public List<ProjectResponse> Projects { get; set; }
    
    public GetProjectsResponse(List<ProjectDto> dtos)
    {
        Projects = dtos.Select(dto => new ProjectResponse(dto)).ToList();
    }
}