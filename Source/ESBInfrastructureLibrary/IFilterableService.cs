using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBInfrastructureLibrary
{
    public interface IFilterableService<in TFilterType>
    {
        Action<TFilterType,bool> GetFilter();
    }
}
