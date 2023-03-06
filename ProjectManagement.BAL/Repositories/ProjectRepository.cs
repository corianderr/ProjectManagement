using ProjectManagement.BAL.Data;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    protected ProjectRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Project> AddEmployeeToProject(Employee employee, Project project)
    {
        project.ExecutiveEmployees.Add(employee);
        await Context.SaveChangesAsync();
        return project;
    }
}