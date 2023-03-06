using ProjectManagement.BAL.Models.Employee;

namespace ProjectManagement.BAL.Models.Project;

public class ProjectResponseModel : BaseResponseModel
{
    public string? Name { get; set; }
    public string? ClientCompanyName { get; set; }
    public string? ExecutorCompanyName { get; set; }
    public int Priority { get; set; }
    public int ManagerId { get; set; }
    public List<EmployeeResponseModel> ExecutiveEmployees { get; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}