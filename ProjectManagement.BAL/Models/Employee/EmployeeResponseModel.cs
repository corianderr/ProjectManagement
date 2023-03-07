using ProjectManagement.BAL.Models.Project;

namespace ProjectManagement.BAL.Models.Employee;

public class EmployeeResponseModel : BaseResponseModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
}