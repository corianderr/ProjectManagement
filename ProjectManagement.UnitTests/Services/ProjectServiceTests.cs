using System.Linq.Expressions;
using FizzWare.NBuilder;
using FluentAssertions;
using NSubstitute;
using ProjectManagement.BAL.Models.Project;
using ProjectManagement.BAL.Services;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.UnitTests.Services;

public class ProjectServiceTests : BaseServiceTestConfiguration
{
    private readonly ProjectService _sut;

    public ProjectServiceTests()
    {
        _sut = new ProjectService(EmployeeRepository, ProjectRepository, Mapper);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Todo_Lists()
    {
        //Arrange
        var projects = Builder<Project>.CreateListOfSize(10).Build().ToList();

        ProjectRepository.GetAll(Arg.Any<Expression<Func<Project, bool>>>()).Returns(projects);

        //Act
        var result = await _sut.GetAllAsync(p => true);

        //Assert
        result.Should().HaveCount(10);
        await ProjectRepository.Received().GetAll(Arg.Any<Expression<Func<Project, bool>>>());
    }

    [Fact]
    public async Task CreateAsync_Should_Add_New_Entity_To_Database()
    {
        //Arrange
        var createProjectModel = Builder<CreateProjectModel>.CreateNew().Build();
        var project = Mapper.Map<Project>(createProjectModel);
        project.Id = 1;

        ProjectRepository.AddAsync(Arg.Any<Project>()).Returns(project);

        //Act
        var result = await _sut.CreateAsync(createProjectModel);

        //Assert
        result.Id.Should().Be(project.Id);
        await ProjectRepository
            .Received().AddAsync(Arg.Any<Project>());
    }

    [Fact]
    public async Task UpdateAsync_Should_Update_Existing_Entity_From_DatabaseAsync()
    {
        //Arrange
        var updateProjectModel = Builder<UpdateProjectModel>.CreateNew().Build();
        var projectId = 1;
        var project = Builder<Project>.CreateNew().With(tl => tl.Id = projectId).Build();

        ProjectRepository.GetFirstAsync(Arg.Any<Expression<Func<Project, bool>>>()).Returns(project);
        ProjectRepository.UpdateAsync(Arg.Any<Project>()).Returns(project);

        //Act
        var result = await _sut.UpdateAsync(projectId, updateProjectModel);

        //Assert
        result.Id.Should().Be(projectId);
        await ProjectRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Project, bool>>>());
        await ProjectRepository.Received().UpdateAsync(Arg.Any<Project>());
    }

    [Fact]
    public async Task DeleteAsync_Should_Delete_Entity_From_Database()
    {
        // Arrange
        var projectId = 1;
        var project = Builder<Project>.CreateNew().With(ti => ti.Id = projectId).Build();

        ProjectRepository.GetFirstAsync(Arg.Any<Expression<Func<Project, bool>>>()).Returns(project);
        ProjectRepository.DeleteAsync(Arg.Any<Project>()).Returns(project);

        //Act
        var result = await _sut.DeleteAsync(projectId);

        // Assert
        result.Id.Should().Be(project.Id);
        await ProjectRepository.Received().GetFirstAsync(Arg.Any<Expression<Func<Project, bool>>>());
        await ProjectRepository.Received().DeleteAsync(Arg.Any<Project>());
    }
}