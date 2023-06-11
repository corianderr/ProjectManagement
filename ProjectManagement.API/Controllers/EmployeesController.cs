using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BAL.Models;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services.Interfaces;

namespace ProjectManagement.API.Controllers;

public class EmployeesController : ApiController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    // GET: api/Employees
    [HttpGet]
    public IActionResult GetAllFilteredAndSortedAsync(int id, string name,
        string surname, string email, string orderBy)
    {
        return Ok(ApiResult<IEnumerable<EmployeeResponseModel>>.Success(_employeeService.GetAllFilteredAndSorted(id, name, surname, email, orderBy)));
    }

    // GET: api/Employees/GetById/5
    [HttpGet("GetById/{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(ApiResult<EmployeeResponseModel>.Success(await _employeeService.GetByIdAsync(id)));
    }
    
    // GET: api/Employees/GetExecutorsByProjectId/5
    [HttpGet("GetExecutorsByProjectId/{id:int}")]
    public async Task<IActionResult> GetAllByProjectIdAsync(int id)
    {
        return Ok(ApiResult<IEnumerable<EmployeeResponseModel>>.Success(await _employeeService.GetAllByProjectIdAsync(id)));
    }
    
    // GET: api/Employees/GetManagerByProjectId/5
    [HttpGet("GetManagerByProjectId/{id:int}")]
    public async Task<IActionResult> GetManagerByProjectIdAsync(int id)
    {
        return Ok(ApiResult<EmployeeResponseModel>.Success(await _employeeService.GetManagerByProjectIdAsync(id)));
    }

    // POST: api/Employees
    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateEmployeeModel createEmployeeModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _employeeService.CreateAsync(createEmployeeModel)));
    }

    // PUT: api/Employees/5
    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, UpdateEmployeeModel updateEmployeeModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _employeeService.UpdateAsync(id, updateEmployeeModel)));
    }

    // DELETE: api/Employees/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(await _employeeService.DeleteAsync(id)));
    }
}