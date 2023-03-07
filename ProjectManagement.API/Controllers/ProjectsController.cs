using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services.Interfaces;

namespace ProjectManagement.API.Controllers;

public class ProjectsController : ApiController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    // GET: api/Projects
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(ApiResult<IEnumerable<ProjectResponseModel>>.Success(await _projectService.GetAllAsync()));
    }

    // GET: api/Projects/GetById/5
    [HttpGet("GetById/{id:int}", Name = "Get")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(ApiResult<ProjectResponseModel>.Success(await _projectService.GetByIdAsync(id)));
    }

    // POST: api/Projects
    [HttpPost]
    public async Task<IActionResult> Post(CreateProjectModel createProjectModel)
    {
        return Ok(ApiResult<CreateProjectModel>.Success(await _projectService.CreateAsync(createProjectModel)));
    }

    // POST: api/Projects/AddEmployeeByProjectId/3
    [HttpPut("AddEmployee/{projectId:int}")]
    public async Task<IActionResult> AddEmployeeToProjectByIdAsync(int projectId, int employeeId)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(
            await _projectService.AddEmployeeToProjectByIdAsync(employeeId, projectId)));
    }

    // PUT: api/Projects/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateProjectModel updateProjectModel)
    {
        return Ok(ApiResult<UpdateProjectModel>.Success(await _projectService.UpdateAsync(id, updateProjectModel)));
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _projectService.DeleteAsync(id)));
    }
}