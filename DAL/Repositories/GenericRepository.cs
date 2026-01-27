using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GenericRepository<T> where T : class
    {
        private readonly Fa25realEstateDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(Fa25realEstateDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public List<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null
        )
        {
            IQueryable<T> query = _dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                int skip = (pageIndex.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return query.ToList();
        }

        public T? GetById(object id) => _dbSet.Find(id);
        public void Add(T entity)
        {
            _dbSet.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void Delete(object id)
        {
            T? entity = _dbSet.Find(id);
            if (entity != null) {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public int Count(Expression<Func<T, bool>>? filter = null)
        {
            return filter == null ? _dbSet.Count() : _dbSet.Count(filter);
        }
    }
}
