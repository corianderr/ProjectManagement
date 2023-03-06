namespace ProjectManagement.BAL.Models.Project;

public class CreateProjectModel
{
    public string? Name { get; set; }
    public string? ClientCompanyName { get; set; }
    public string? ExecutorCompanyName { get; set; }
    public int Priority { get; set; }
    public int ManagerId { get; set; }
}