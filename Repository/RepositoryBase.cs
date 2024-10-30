using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Repository;

public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected RepositoryContext RepositoryContext { get; set; }

    protected RepositoryBase(RepositoryContext context)
    {
        RepositoryContext = context;
    }

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
    {
        if (trackChanges)
        {
            return RepositoryContext.Set<T>().Where(expression);
        }
        else
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        }
    }

    public IQueryable<T> FindAll(bool trackChanges)
    {
        return FindByCondition(_ => true, trackChanges);
    }

    public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);

    public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

    public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
}
