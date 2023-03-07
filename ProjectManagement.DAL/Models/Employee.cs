using ProjectManagement.DAL.Models.Common;

namespace ProjectManagement.DAL.Models;

public class Employee : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Email { get; set; }
    public List<Project> ManagedProjects { get; } = new();
    public List<Project> WorkedOnProjects { get; } = new();
}