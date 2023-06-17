using System.Linq.Expressions;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseModel>> GetAllAsync(Expression<Func<Project, bool>> predicate);

    IEnumerable<ProjectResponseModel> GetAllFilteredAndSorted(int id, string name = "", int priority = 0,
        DateTime startDateFrom = default, DateTime startDateTo = default, string orderBy = "nameAsc",
        CancellationToken cancellationToken = default);

    Task<ProjectResponseModel>
        GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> CreateAsync(CreateProjectModel createProjectModel,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> AddEmployeeToProjectByIdAsync(int employeeId, int projectId,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UpdateAsync(int id, UpdateProjectModel updateProjectModel,
        CancellationToken cancellationToken = default);
}