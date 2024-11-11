using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Auto.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }


    public class GenericRepository<T> : IGenericRepository<T> where T : class
        {
            protected readonly AutoDbContext _context;
            private readonly DbSet<T> _dbSet;

            public GenericRepository(AutoDbContext context)
            {
                _context = context;
                _dbSet = context.Set<T>();
            }

            public async Task<IList<T>> GetAllAsync()
            {
                return await _dbSet.ToListAsync();
            }
        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
        public async Task<T?> GetByIdAsync(int id)
            {
                return await _dbSet.FindAsync(id);
            }

            public async Task AddAsync(T entity)
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            public async Task UpdateAsync(T entity)
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
            }

            public async Task DeleteAsync(int id)
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                    await _context.SaveChangesAsync();
                }
            }
        }
    

}
