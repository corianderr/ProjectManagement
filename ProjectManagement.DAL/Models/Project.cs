using ProjectManagement.DAL.Models.Common;

namespace ProjectManagement.DAL.Models;

public class Project : BaseEntity, IDateFixEntity
{
    public string Name { get; set; }
    public string ClientCompanyName { get; set; }
    public string ExecutorCompanyName { get; set; }
    public int Priority { get; set; }
    public int ManagerId { get; set; }
    public Employee Manager { get; set; }
    public List<Employee> ExecutiveEmployees { get; } = new();
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}