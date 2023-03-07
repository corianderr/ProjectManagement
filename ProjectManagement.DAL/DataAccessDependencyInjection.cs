using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Repositories;

namespace ProjectManagement.DAL;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccessRepos(this IServiceCollection services)
    {
        services.AddRepositories();
        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }
}