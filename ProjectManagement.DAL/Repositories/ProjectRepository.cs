using ProjectManagement.DAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL.Repositories;

public class ProjectRepository : BaseRepository<Project>
{
    protected ProjectRepository(ApplicationContext context) : base(context)
    {
    }
}