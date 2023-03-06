using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProjectManagement.BAL.MappingProfiles;
using ProjectManagement.BAL.Services;
using ProjectManagement.BAL.Services.Interfaces;

namespace ProjectManagement.BAL;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);
        services.RegisterAutoMapper();
        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }
}