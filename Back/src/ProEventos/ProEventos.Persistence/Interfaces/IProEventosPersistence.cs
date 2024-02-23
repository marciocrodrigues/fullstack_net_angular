using ProEventos.Domain.Entities;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Interfaces
{
    public interface IProEventosPersistence<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(T entity);
        Task<bool> SaveChangesAsync();

        IQueryable<T> GetWithFilterFull(Expression<Func<T, bool>> predicado);
        IQueryable<T> GetWithFilter(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes);

        IQueryable<T> GetWithFilterWithoutAsNoTracking(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes);
    }
}
