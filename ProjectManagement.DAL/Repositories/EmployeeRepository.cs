using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Repositories;

public class EmployeeRepository : BaseRepository<Employee>
{
    protected EmployeeRepository(ApplicationContext context) : base(context)
    {
    }
}