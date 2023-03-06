using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<ProjectResponseModel>>
        GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<ProjectResponseModel>>
        GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CreateProjectModel> CreateAsync(CreateProjectModel createProjectModel,
        CancellationToken cancellationToken = default);
    
    Task<ProjectResponseModel> AddEmployeeByProjectIdAsync(int employeeId, int projectId,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProjectResponseModel>>
        GetAllByManagerIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<IEnumerable<ProjectResponseModel>>
        GetAllByEmployeeIdAsync(int id, CancellationToken cancellationToken = default);

    Task<UpdateProjectModel> UpdateAsync(int id, UpdateProjectModel updateProjectModel,
        CancellationToken cancellationToken = default);
}