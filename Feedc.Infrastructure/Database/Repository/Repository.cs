using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Feedc.Infrastructure.Database.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        #region Fields

        private readonly FeedcContext _context;

        #endregion

        #region Ctor

        public Repository()
        {
            _context = new FeedcContext();
        }

        #endregion

        #region Methods

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Set<T>().Add(entity);
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual void Insert(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    _context.Set<T>().Add(entity);

                _context.SaveChangesAsync();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Set<T>().Update(entity);
                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual void Update(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    _context.Set<T>().Update(entity);

                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));

                _context.Set<T>().Remove(entity);

                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual void Delete(IEnumerable<T> entities)
        {
            try
            {
                if (entities == null)
                    throw new ArgumentNullException(nameof(entities));

                foreach (var entity in entities)
                    _context.Set<T>().Remove(entity);

                _context.SaveChanges();
            }
            catch (Exception dbEx)
            {
                throw new Exception(dbEx.Message);
            }
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>().AsNoTracking();
        }

        #endregion
    }
}
