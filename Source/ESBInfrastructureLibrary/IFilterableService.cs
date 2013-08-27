using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESBInfrastructureLibrary
{
    public interface IFilterableService<TFilterType>
    {
        Expression<Func<TFilterType,bool>> GetFilter();
    }
}
