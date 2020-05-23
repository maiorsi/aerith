using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerith.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        AerithContext GetContext();
        IQueryable<TEntity> GetQueryable();
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> Get(int id);
        Task<TEntity> Add(TEntity item);
        Task<TEntity> Update(TEntity item);
        Task<bool> Remove(int id);
    }
}