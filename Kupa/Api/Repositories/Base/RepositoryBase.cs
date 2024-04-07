using Kupa.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Kupa.Api.Repositories.Base
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected DbContext _context;

        public RepositoryBase(DbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _context
                .Set<T>()
                .Where(expression)
                .AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> GetAllItemsAsync()
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(object id)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(entity => EF.Property<object>(entity, "Id").Equals(id));
        }

        public virtual async Task AddItemAsync(T entity)
        {
            await _context
                .Set<T>()
                .AddAsync(entity);
            await _context
                .SaveChangesAsync();
        }

        public virtual async Task UpdateItemAsync(T entity)
        {
            _context
                .Set<T>()
                .Update(entity);
            await _context
                .SaveChangesAsync();
        }

        public virtual async Task DeleteItemAsync(T entity)
        {
            _context
                .Set<T>()
                .Remove(entity);
            await _context
                .SaveChangesAsync();
        }

        public virtual async Task<bool> ExistsByIdAsync(object id)
        {
            var entity = await _context
                .Set<T>()
                .FindAsync(id);

            return entity != null;
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context
                .Set<T>()
                .AsNoTracking()
                .AnyAsync(expression);
        }
    }
}
