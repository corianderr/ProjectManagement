using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    public ProjectRepository(ApplicationContext context) : base(context)
    {
    }

    public IEnumerable<Project> SortBy(string orderBy, IEnumerable<Project> projects)
    {
        if (string.IsNullOrWhiteSpace(orderBy)) return projects;
        projects = orderBy switch
        {
            "nameAsc" => projects.OrderBy(p => p.Name).ToList(),
            "nameDesc" => projects.OrderByDescending(p => p.Name).ToList(),
            "priorityAsc" => projects.OrderBy(p => p.Priority).ToList(),
            "priorityDesc" => projects.OrderByDescending(p => p.Priority).ToList(),
            "startDateAsc" => projects.OrderBy(p => p.StartDate).ToList(),
            "startDateDesc" => projects.OrderByDescending(p => p.StartDate).ToList(),
            _ => projects
        };
        return projects;
    }

    public async Task<IEnumerable<Project>> GetAllFilteredBy(int id, string name, int priority, DateTime startDateFrom,
        DateTime startDateTo)
    {
        IEnumerable<Project> projects = await GetAllAsync();
        if (!string.IsNullOrWhiteSpace(name)) projects = projects.Where(p => p.Name == name);
        if (id != 0) projects = projects.Where(p => p.Id == id);
        if (priority != 0) projects = projects.Where(p => p.Priority == priority);
        if (!startDateFrom.Equals(DateTime.MinValue)) projects = projects.Where(p => p.StartDate >= startDateFrom);
        if (!startDateTo.Equals(DateTime.MinValue)) projects = projects.Where(p => p.StartDate <= startDateFrom);
        return projects;
    }

    public async Task<Project> AddEmployeeToProject(Employee employee, Project project)
    {
        project.ExecutiveEmployees.Add(employee);
        await UpdateAsync(project);
        await Context.SaveChangesAsync();
        return project;
    }
}