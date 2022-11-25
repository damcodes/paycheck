using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using PaycheckBackend.Repositories.Interfaces;
using PaycheckBackend.Db;

namespace PaycheckBackend.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected AppDbContext RepositoryContext { get; set; }
        public BaseRepository(AppDbContext appDbContext)
        {
            RepositoryContext = appDbContext;
        }

        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);
    }
}