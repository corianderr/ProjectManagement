using ProjectManagement.DAL.Models.Common;

namespace ProjectManagement.DAL.Models;

public class Employee : BaseEntity
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Email { get; set; }
    public List<Project> Projects { get; } = new List<Project>();
}