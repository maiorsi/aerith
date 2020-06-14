using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerith.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        AerithContext GetContext();
        IQueryable<TEntity> GetQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync(bool eager = false);
        Task<TEntity> GetAsync(long id, bool eager = false);
        Task<TEntity> AddAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<bool> RemoveAsync(long id);
    }
}