using System.Linq.Expressions;

namespace Users.Microservice.Data.IRepositories
{
    public interface IRepository<TSource> where TSource : class
    {
        IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string[] includes = null, bool isTracking = true);
        ValueTask<TSource> AddAsync(TSource entity);
        ValueTask<TSource> GetAsync(Expression<Func<TSource, bool>> expression, string[] includes = null);
        TSource Update(TSource entity);
        void Delete(TSource entity);
        ValueTask SaveChangesAsync();
    }
}
