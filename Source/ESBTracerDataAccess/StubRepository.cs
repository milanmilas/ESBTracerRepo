using ESBInfrastructureLibrary;
using ESBTracerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBTracerDataAccess
{
    public class StubRepository<T> : IRepository<T>  where T : class
    {
        readonly IList<T> _context;

        public StubRepository(IList<T> context){
            _context = context;
        }

        public IQueryable<T> Fetch()
        {
            return _context.AsQueryable<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _context;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _context.Where(predicate);
        }

        public T Single(Func<T, bool> predicate)
        {
            return _context.Where(predicate).Single();
        }

        public T First(Func<T, bool> predicate)
        {
            return _context.Where(predicate).FirstOrDefault();
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }

    }
}
