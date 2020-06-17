using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aerith.Data.Interfaces
{
    public interface IRepository<TEntity>
    {
        AerithContext GetContext();
        IQueryable<TEntity> GetQueryable();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetAsync(long id);
        Task<TEntity> AddAsync(TEntity item);
        Task<TEntity> UpdateAsync(TEntity item);
        Task<bool> RemoveAsync(long id);
    }
}