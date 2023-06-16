using System.Linq.Expressions;
using AutoMapper;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services.Interfaces;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services;

public class ProjectService : IProjectService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync(Expression<Func<Project, bool>> predicate)
    {
        var projects = await _projectRepository.GetAll(predicate);
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(projects);
    }
    
    public IEnumerable<ProjectResponseModel> GetAllFilteredAndSorted(int id, string name, int priority,
        DateTime startDateFrom, DateTime startDateTo, string orderBy, CancellationToken cancellationToken = default)
    {
        var projects = _projectRepository.GetAllFilteredBy(id, name, priority, startDateFrom, startDateTo);
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(_projectRepository.SortBy(orderBy, projects).ToList());
    }

    public async Task<ProjectResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(p => p.Id == id);
        return _mapper.Map<ProjectResponseModel>(project);
    }

    public async Task<BaseResponseModel> CreateAsync(CreateProjectModel createProjectModel,
        CancellationToken cancellationToken = default)
    {
        var project = _mapper.Map<Project>(createProjectModel);
        return new BaseResponseModel
        {
            Id = (await _projectRepository.AddAsync(project)).Id
        };
    }

    public async Task<BaseResponseModel> AddEmployeeToProjectByIdAsync(int employeeId, int projectId,
        CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetFirstAsync(e => e.Id == employeeId);
        var project = await _projectRepository.GetFirstAsync(p => p.Id == projectId);
        await _projectRepository.AddEmployeeToProject(employee, project);
        return new BaseResponseModel
        {
            Id = projectId
        };
    }

    public async Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(p => p.Id == id);
        return new BaseResponseModel
        {
            Id = (await _projectRepository.DeleteAsync(project)).Id
        };
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllByManagerIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(
            await _projectRepository.GetAll(p => p.ManagerId == id));
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllByEmployeeIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(
            await _projectRepository.GetAll(p => p.ExecutiveEmployees.Any(e => e.Id == id)));
    }

    public async Task<BaseResponseModel> UpdateAsync(int id, UpdateProjectModel updateProjectModel,
        CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateProjectModel, project);
        return new BaseResponseModel
        {
            Id = (await _projectRepository.UpdateAsync(project)).Id
        };
    }
}