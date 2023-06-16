using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.BAL.Models.Project;

public class CreateProjectModel
{
    [Required(ErrorMessage = "Please enter your name using less than 50 characters."), StringLength(50)]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter client company's name."), StringLength(50)]
    public string ClientCompanyName { get; set; }
    [Required(ErrorMessage = "Please enter executor company's name."), StringLength(50)]
    public string ExecutorCompanyName { get; set; }
    [Range(0, 3, ErrorMessage = "Priority value must be between 0 and 3")]
    [Required(ErrorMessage = "Please enter priority.")]
    public int Priority { get; set; }
    [Required(ErrorMessage = "Please enter manager's id.")]
    public int ManagerId { get; set; }
}