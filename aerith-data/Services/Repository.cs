using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aerith.Common.Models;
using Aerith.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Aerith.Data.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity: MetaDbType
    {
        private readonly AerithContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AerithContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity item)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(_ => _.Id == id);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public AerithContext GetContext()
        {
            return _context;
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<bool> RemoveAsync(int id)
        {
            _dbSet.Remove(await _dbSet.FindAsync(id));
            
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<TEntity> UpdateAsync(TEntity item)
        {
            _dbSet.Update(item);

            await _context.SaveChangesAsync();

            return item;
        }
    }
}