using ProjectManagement.BAL.Data;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    protected EmployeeRepository(ApplicationContext context) : base(context)
    {
    }
}