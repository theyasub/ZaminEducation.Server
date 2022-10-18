using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Microservice.Data.DbContexts;
using Users.Microservice.Data.IRepositories;

namespace Users.Microservice.Data.Repositories
{
    public class Repository<TSource> : IRepository<TSource> where TSource : class
    {
        protected readonly UserDbContext dbContext;
        protected readonly DbSet<TSource> dbSet;

        public Repository(UserDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TSource>();
        }

        public async ValueTask<TSource> AddAsync(TSource entity)
        {
            var entry = await dbSet.AddAsync(entity);

            return entry.Entity;
        }

        public void Delete(TSource entity)
            => dbSet.Remove(entity);

        public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string[] includes = null, bool isTracking = true)
        {
            IQueryable<TSource> query = expression is null ? dbSet : dbSet.Where(expression);

            if (includes is not null)
                foreach (var include in includes)
                    query = query.Include(include);

            if (!isTracking)
                query = query.AsNoTracking();

            return query;
        }

        public async ValueTask<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string[] includes = null)
            => await GetAll(expression, includes).FirstOrDefaultAsync();

        public TSource Update(TSource entity)
            => dbSet.Update(entity).Entity;

        public async ValueTask SaveChangesAsync()
            => await dbContext.SaveChangesAsync();
    }
}
