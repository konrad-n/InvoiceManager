using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InvoiceManager.Data;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManager.Repositories
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationDbContext DbContext { get; set; }

        public RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await DbContext.Set<T>().Where(expression).AsNoTracking().ToListAsync();
        }

        public async Task<T> FindByIdAsync(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
        }

        public Task UpdateAsync(T entity)
        {
            DbContext.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
