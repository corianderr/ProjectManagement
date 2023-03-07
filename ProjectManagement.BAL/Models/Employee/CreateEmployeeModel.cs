namespace ProjectManagement.BAL.Models.Employee;

public class CreateEmployeeModel : BaseResponseModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
}