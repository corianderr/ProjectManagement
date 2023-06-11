using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Contracts;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    IEnumerable<Employee> SortBy(string orderBy, IEnumerable<Employee> employees);
    IQueryable<Employee> GetAllFilteredBy(int id, string name, string surname, string email);
}