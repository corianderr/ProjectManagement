using AutoMapper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using ProjectManagement.BAL.MappingProfiles;
using ProjectManagement.DAL.Contracts;

namespace ProjectManagement.UnitTests.Services;

public class BaseServiceTestConfiguration
{
    protected readonly IConfiguration Configuration;
    protected readonly IEmployeeRepository EmployeeRepository;
    protected readonly IMapper Mapper;
    protected readonly IProjectRepository ProjectRepository;

    protected BaseServiceTestConfiguration()
    {
        Mapper = new MapperConfiguration(cfg => { cfg.AddMaps(typeof(ProjectProfile)); }).CreateMapper();
        Configuration = new ConfigurationBuilder().Build();
        ProjectRepository = Substitute.For<IProjectRepository>();
        EmployeeRepository = Substitute.For<IEmployeeRepository>();
    }
}