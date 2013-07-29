using ESBInfrastructureLibrary;
using ESBTracerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBTracerDataAccess
{
    public class EFRepository<T> : IRepository<T>  where T : class
    {
        readonly DbContext _context;

        public IQueryable<T> Fetch()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T Single(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public T First(Func<T, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
