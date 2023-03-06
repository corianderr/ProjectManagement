using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponseModel>>
        GetAllAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<EmployeeResponseModel>>
        GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<CreateEmployeeModel> CreateAsync(CreateEmployeeModel createEmployeeModel,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<EmployeeResponseModel>>
        GetAllByProjectIdAsync(int id, CancellationToken cancellationToken = default);
    
    Task<EmployeeResponseModel>
        GetManagerByProjectIdAsync(int id, CancellationToken cancellationToken = default);

    Task<UpdateEmployeeModel> UpdateAsync(int id, UpdateEmployeeModel updateEmployeeModel,
        CancellationToken cancellationToken = default);
}