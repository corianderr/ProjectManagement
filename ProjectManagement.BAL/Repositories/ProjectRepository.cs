using ProjectManagement.BAL.Data;
using ProjectManagement.DAL.Contracts;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class ProjectRepository : BaseRepository<Project>, IProjectRepository
{
    protected ProjectRepository(ApplicationContext context) : base(context)
    {
    }
}