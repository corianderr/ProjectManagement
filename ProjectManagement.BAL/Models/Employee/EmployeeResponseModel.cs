using System.ComponentModel.DataAnnotations;
using ProjectManagement.BAL.Models.Project;

namespace ProjectManagement.BAL.Models.Employee;

public class EmployeeResponseModel : BaseResponseModel
{
    [Required(ErrorMessage = "Please enter your name using less than 50 characters."), StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter your surname using less than 50 characters."), StringLength(50)]
    public string Surname { get; set; }
    [Required(ErrorMessage = "Please enter your patronymic using less than 50 characters."), StringLength(50)]
    public string Patronymic { get; set; }
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    [Required(ErrorMessage = "Please enter your e-mail address using less than 50 characters."), StringLength(50)]
    public string Email { get; set; }
}