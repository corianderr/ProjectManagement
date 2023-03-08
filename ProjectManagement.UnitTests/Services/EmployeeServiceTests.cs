using System.Linq.Expressions;
using AutoMapper;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ProjectManagement.BAL.Models.Employee;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.UnitTests.Services;

public class EmployeeServiceTests : BaseServiceTestConfiguration
{
    private readonly EmployeeService _sut;

    public EmployeeServiceTests()
    {
        _sut = new EmployeeService(EmployeeRepository, Mapper);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Todo_Lists()
    {
        //Arrange
        var employees = Builder<Employee>.CreateListOfSize(10).Build().ToList();

        EmployeeRepository.GetAllAsync(Arg.Any<Expression<Func<Employee, bool>>>()).Returns(employees);

        //Act
        var result = await _sut.GetAllAsync(p => true);

        //Assert
        result.Should().HaveCount(10);
        await EmployeeRepository.Received().GetAllAsync(Arg.Any<Expression<Func<Employee, bool>>>());
    }
    
    [Fact]
    public async Task CreateAsync_Should_Add_New_Entity_To_Database()
    {
        //Arrange
        var createEmployeeModel = Builder<CreateEmployeeModel>.CreateNew().Build();
        var employee = Mapper.Map<Employee>(createEmployeeModel);
        employee.Id = 1;

        EmployeeRepository.AddAsync(Arg.Any<Employee>()).Returns(employee);

        //Act
        var result = await _sut.CreateAsync(createEmployeeModel);

        //Assert
        result.Id.Should().Be(employee.Id);
        await EmployeeRepository
            .Received().AddAsync(Arg.Any<Employee>());
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_Entity_From_DatabaseAsync()
    {
        //Arrange
        var updateEmployeeModel = Builder<UpdateEmployeeModel>.CreateNew().Build();
        var employeeId = 1;
        var employee = Builder<Employee>.CreateNew().With(tl => tl.Id = employeeId).Build();

        EmployeeRepository.GetFirstAsync(Arg.Any<Expression<Func<Employee, bool>>>()).Returns(employee);
        EmployeeRepository.UpdateAsync(Arg.Any<Employee>()).Returns(employee);

        //Act
        var result = await _sut.UpdateAsync(employeeId, updateEmployeeModel);

        //Assert
        result.Id.Should().Be(employeeId);
        await EmployeeRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Employee, bool>>>());
        await EmployeeRepository.Received().UpdateAsync(Arg.Any<Employee>());
    }
    
    [Fact]
    public async Task DeleteAsync_Should_Delete_Entity_From_Database()
    {
        // Arrange
        var employeeId = 1;
        var employee = Builder<Employee>.CreateNew().With(ti => ti.Id = employeeId).Build();

        EmployeeRepository.GetFirstAsync(Arg.Any<Expression<Func<Employee, bool>>>()).Returns(employee);
        EmployeeRepository.DeleteAsync(Arg.Any<Employee>()).Returns(employee);
        
        //Act
        var result = await _sut.DeleteAsync(employeeId);

        // Assert
        result.Id.Should().Be(employee.Id);
        await EmployeeRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Employee, bool>>>());
        await EmployeeRepository.Received().DeleteAsync(Arg.Any<Employee>());
    }
}