using System;
using System.Text;
using System.Collections.Generic;
using EticaretSite.DataAccess.IRepository;
using System.Linq.Expressions;
using System.Linq;
using EticaretSite.Data;
using Microsoft.EntityFrameworkCore;

namespace EticaretSite.DataAccess.MainRepository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        // get all fonksiyonuna bak
        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            
            if(filter!=null)
            {
                query = query.Where(filter);
            }

            if(includeProperties!=null)
            {
                foreach(var item in includeProperties.Split(new char[]
                {','},StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(item);
                }
            }

            if(orderBy != null)
            {

                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;


            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[]
                {','}, StringSplitOptions.RemoveEmptyEntries))
                {

                    query = query.Include(item);
                }
            }

            
            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entity = dbSet.Find(id);
            Remove(entity);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
