using System.Linq.Expressions;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services.Interfaces;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeResponseModel>> GetAllAsync(Expression<Func<Employee, bool>> predicate);
    IEnumerable<EmployeeResponseModel> GetAllFilteredAndSorted(int id, string name,
        string surname, string email, string orderBy, CancellationToken cancellationToken = default);

    Task<EmployeeResponseModel>
        GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> CreateAsync(CreateEmployeeModel createEmployeeModel,
        CancellationToken cancellationToken = default);

    Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default);

    Task<IEnumerable<EmployeeResponseModel>>
        GetAllByProjectIdAsync(int id, CancellationToken cancellationToken = default);

    Task<EmployeeResponseModel>
        GetManagerByProjectIdAsync(int id, CancellationToken cancellationToken = default);

    Task<BaseResponseModel> UpdateAsync(int id, UpdateEmployeeModel updateEmployeeModel,
        CancellationToken cancellationToken = default);
}