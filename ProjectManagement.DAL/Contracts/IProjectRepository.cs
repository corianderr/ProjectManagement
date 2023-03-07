using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Contracts;

public interface IProjectRepository : IBaseRepository<Project>
{
    IEnumerable<Project> SortBy(string orderBy, IEnumerable<Project> projects);
    Task<IEnumerable<Project>> GetAllFilteredBy(int id, string name, int priority, DateTime startDateFrom,
        DateTime startDateTo);
    Task<Project> AddEmployeeToProject(Employee employee, Project project);
}