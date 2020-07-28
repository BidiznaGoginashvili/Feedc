using System.Linq;
using System.Collections.Generic;

namespace Feedc.Infrastructure.Database.Repository
{
    public interface IRepository<T> where T : class
    {
        void Insert(T entity);

        void Insert(IEnumerable<T> entities);

        void Update(T entity);

        void Update(IEnumerable<T> entities);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);

        T GetById(int id);

        IQueryable<T> GetAll();
    }
}
