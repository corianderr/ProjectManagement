using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Project> AddEmployeeToProject(Employee employee, Project project)
    {
        project.ExecutiveEmployees.Add(employee);
        await UpdateAsync(project);
        await Context.SaveChangesAsync();
        return project;
    }
}