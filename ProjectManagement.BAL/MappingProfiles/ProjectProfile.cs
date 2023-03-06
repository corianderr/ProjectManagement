using AutoMapper;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.MappingProfiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<CreateProjectModel, Project>();
        CreateMap<UpdateProjectModel, Project>();
        CreateMap<Project, ProjectResponseModel>();
    }
}