using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class GenericService<T> where T : class 
    {
        private readonly GenericRepository<T> _genericRepository;
        public GenericService(GenericRepository<T> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<T> GetAll(
            System.Linq.Expressions.Expression<Func<T, bool>>? filter = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null
        ) => _genericRepository.GetAll(filter, includeProperties, pageIndex, pageSize);

        public T? GetById(object id) => _genericRepository.GetById(id);
        public void Add(T entity) => _genericRepository.Add(entity);
        public void Update(T entity) => _genericRepository.Update(entity);
        public void Delete(object id) => _genericRepository.Delete(id);
        public int Count(Expression<Func<T, bool>>? filter = null) => _genericRepository.Count(filter);
    }
}
