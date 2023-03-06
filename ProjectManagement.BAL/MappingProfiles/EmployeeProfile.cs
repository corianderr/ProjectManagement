using AutoMapper;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<CreateEmployeeModel, Employee>();
        CreateMap<UpdateEmployeeModel, Employee>();
        CreateMap<Employee, EmployeeResponseModel>();
        
    }
}