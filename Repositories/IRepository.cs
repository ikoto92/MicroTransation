using MicroTransation.Data;
using MicroTransation.Models;

namespace MicroTransation.Repositories
{
    public interface IRepository
    {
        public interface IRepository<TEntity>
        {
            TEntity GetById(int id);
            IEnumerable<TEntity> GetAll();
            TEntity Create(TEntity entity);
            void Delete(TEntity entity);
        }
    }
}
