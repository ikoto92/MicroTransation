using MicroTransation.Data;
using MicroTransation.Models;

namespace MicroTransation.Repositories
{
    public interface IRepository<TEntity>
     {
        Task<TEntity?> GetById(int id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Create(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
    }
    public interface ITokenRepository
    {
        Task AddToken(AuthToken token);
    }
}

