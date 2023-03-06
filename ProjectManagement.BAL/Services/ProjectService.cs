using AutoMapper;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services.Interfaces;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Services;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _projectRepository = projectRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var projects = await _projectRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(projects);
    }

    public async Task<ProjectResponseModel> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(p => p.Id == id);
        return _mapper.Map<ProjectResponseModel>(project);
    }

    public async Task<CreateProjectModel> CreateAsync(CreateProjectModel createProjectModel,
        CancellationToken cancellationToken = default)
    {
        var project = _mapper.Map<Project>(createProjectModel);
        return new CreateProjectModel()
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
        return new BaseResponseModel()
        {
            Id = projectId
        };
    }

    public async Task<BaseResponseModel> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(p => p.Id == id);
        return new BaseResponseModel()
        {
            Id = (await _projectRepository.DeleteAsync(project)).Id
        };
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllByManagerIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(
            await _projectRepository.GetAllAsync(p => p.ManagerId == id));
    }

    public async Task<IEnumerable<ProjectResponseModel>> GetAllByEmployeeIdAsync(int id,
        CancellationToken cancellationToken = default)
    {
        return _mapper.Map<IEnumerable<ProjectResponseModel>>(
            await _projectRepository.GetAllAsync(p => p.ExecutiveEmployees.Any(e => e.Id == id)));
    }

    public async Task<UpdateProjectModel> UpdateAsync(int id, UpdateProjectModel updateProjectModel,
        CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetFirstAsync(e => e.Id == id);
        _mapper.Map(updateProjectModel, project);
        return new UpdateProjectModel()
        {
            Id = (await _projectRepository.UpdateAsync(project)).Id
        };
    }
}