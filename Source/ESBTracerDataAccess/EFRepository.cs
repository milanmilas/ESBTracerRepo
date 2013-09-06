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
    using LinqKit;

    public class EFRepository<T> : IRepository<T>  where T : class
    {
        readonly DbContext _context;

        public EFRepository(DbContext context){
            _context = context;
        }

        public IQueryable<T> Fetch()
        {
            return _context.Set<T>().AsExpandable();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public T Single(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).Single();
        }

        public T First(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate).FirstOrDefault();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
