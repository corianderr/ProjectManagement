using ProjectManagement.BAL.Data;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BAL.Repositories;

public class ProjectRepository : BaseRepository<Project>
{
    protected ProjectRepository(ApplicationContext context) : base(context)
    {
    }
}