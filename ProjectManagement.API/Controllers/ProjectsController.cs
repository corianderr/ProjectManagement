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

    // // GET: api/Projects
    [HttpGet]
    public IActionResult GetAllFilteredAndSorted(int id, string? name, int priority,
        DateTime startDateFrom, DateTime startDateTo, string? orderBy)
    {
        return Ok(ApiResult<IEnumerable<ProjectResponseModel>>.Success(
            _projectService.GetAllFilteredAndSorted(id, name, priority, startDateFrom, startDateTo, orderBy)));
    }

    // GET: api/Projects/GetById/5
    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(ApiResult<ProjectResponseModel>.Success(await _projectService.GetByIdAsync(id)));
    }

    // POST: api/Projects
    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateProjectModel createProjectModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _projectService.CreateAsync(createProjectModel)));
    }

    // PUT: api/Projects/AddEmployee/3/1
    [HttpPut("AddEmployee/{projectId:int}/{employeeId:int}")]
    public async Task<IActionResult> AddEmployeeToProjectByIdAsync(int projectId, int employeeId)
    {
        try
        {
            return Ok(ApiResult<BaseResponseModel>.Success(
                await _projectService.AddEmployeeToProjectByIdAsync(employeeId, projectId)));
        }
        catch (ArgumentException ex)
        {
            return NotFound(ApiResult<ObjectResult>.Failure(new[] { ex.Message }));
        }
    }

    // PUT: api/Projects/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, UpdateProjectModel updateProjectModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _projectService.UpdateAsync(id, updateProjectModel)));
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _projectService.DeleteAsync(id)));
    }
}