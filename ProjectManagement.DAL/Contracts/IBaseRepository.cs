using System.Linq.Expressions;
using ProjectManagement.DAL.Models.Common;

namespace ProjectManagement.DAL.Contracts;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> GetAll();
    Task<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}