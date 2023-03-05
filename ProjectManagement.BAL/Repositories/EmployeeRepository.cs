using ProjectManagement.BAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class EmployeeRepository : BaseRepository<Employee>
{
    protected EmployeeRepository(ApplicationContext context) : base(context)
    {
    }
}