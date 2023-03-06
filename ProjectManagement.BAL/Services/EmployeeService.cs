using AutoMapper;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.BAL.Services.Interfaces;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProjectRepository _projectRepository;
    

    public EmployeeService(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper maper)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;
        _mapper = maper;
    }
    
    public async Task<IEnumerable<EmployeeResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var employees = await _employeeRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<EmployeeResponseModel>>(employees);
    }

    public async Task<EmployeeResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetFirstAsync(e => e.Id == id);
        return _mapper.Map<EmployeeResponseModel>(employee);
    }

    public async Task<CreateEmployeeModel> CreateAsync(CreateEmployeeModel createEmployeeModel, CancellationToken cancellationToken = default)
    {
        var employee = _mapper.Map<Employee>(createEmployeeModel);
        return new CreateEmployeeModel
        {
            Id = (await _employeeRepository.AddAsync(employee)).Id
        };
    }

    public async Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetFirstAsync(e => e.Id == id);
        return new BaseResponseModel()
        {
            Id = (await _employeeRepository.DeleteAsync(employee)).Id
        };
    }

    public async Task<IEnumerable<EmployeeResponseModel>> GetAllByProjectIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<EmployeeResponseModel>>(await _employeeRepository.GetAllAsync(e => e.WorkedOnProjects.Any(p => p.Id == id)));
    }

    public async Task<EmployeeResponseModel> GetManagerByProjectIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return _mapper.Map<EmployeeResponseModel>(await _employeeRepository.GetFirstAsync(e => e.ManagedProjects.Exists(p => p.Id == id)));
    }

    public async Task<UpdateEmployeeModel> UpdateAsync(int id, UpdateEmployeeModel updateEmployeeModel, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateEmployeeModel, employee);
        return new UpdateEmployeeModel()
        {
            Id = (await _employeeRepository.UpdateAsync(employee)).Id
        };
    }
}