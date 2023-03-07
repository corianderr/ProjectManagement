using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Contracts;

public interface IProjectRepository : IBaseRepository<Project>
{
    Task<Project> AddEmployeeToProject(Employee employee, Project project);
}