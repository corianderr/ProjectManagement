using System.Collections.Generic;
using System.Linq;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(ApplicationContext context) : base(context)
    {
    }
    public IEnumerable<Employee> SortBy(string orderBy, IEnumerable<Employee> employees)
    {
        if (string.IsNullOrWhiteSpace(orderBy)) return employees;
        employees = orderBy switch
        {
            "nameAsc" => employees.OrderBy(p => p.Name).ToList(),
            "nameDesc" => employees.OrderByDescending(p => p.Name).ToList(),
            "surnameAsc" => employees.OrderBy(p => p.Surname).ToList(),
            "surnameDesc" => employees.OrderByDescending(p => p.Surname).ToList(),
            "emailAsc" => employees.OrderBy(p => p.Email).ToList(),
            "emailDesc" => employees.OrderByDescending(p => p.Email).ToList(),
            _ => employees
        };
        return employees;
    }

    public IQueryable<Employee> GetAllFilteredBy(int id, string name, string surname, string email)
    {
        var employees = GetAll();
        if (!string.IsNullOrWhiteSpace(name)) employees = employees.Where(p => p.Name.Contains(name));
        if (!string.IsNullOrWhiteSpace(surname)) employees = employees.Where(p => p.Surname.Contains(surname));
        if (!string.IsNullOrWhiteSpace(email)) employees = employees.Where(p => p.Email.Contains(email));
        if (id != 0) employees = employees.Where(p => p.Id == id);
        return employees;
    }
}