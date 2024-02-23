using Microsoft.EntityFrameworkCore;
using ProEventos.Domain.Entities;
using ProEventos.Persistence.Interfaces;
using ProEventos.Persistence.ProEventos;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProEventos.Persistence.Implementations
{
    public class ProEventosPersistence<T> : IProEventosPersistence<T> where T : class
    {
        private readonly ProEventosContext _context;
        private readonly DbSet<T> DbSet;

        public ProEventosPersistence(ProEventosContext context)
        {
            _context = context;
            DbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void DeleteRange(T entity)
        {
            DbSet.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public IQueryable<T> GetWithFilter(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes)
        {
            var query = DbSet.AsNoTracking();

            query = query.Where(predicado);

            if (inclusoes != null)
            {
                foreach (var item in inclusoes.Where(i => i != null))
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public IQueryable<T> GetWithFilterFull(Expression<Func<T, bool>> predicado)
        {
            return DbSet.Where(predicado);
        }

        public IQueryable<T> GetWithFilterWithoutAsNoTracking(Expression<Func<T, bool>> predicado, params Expression<Func<T, object>>[] inclusoes)
        {
            var query = DbSet.AsNoTracking().Where(predicado);

            if (inclusoes != null)
            {
                foreach (var item in inclusoes.Where(i => i != null))
                {
                    query = query.Include(item);
                }
            }

            return query;
        }
    }
}
