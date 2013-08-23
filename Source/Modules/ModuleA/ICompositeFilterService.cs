using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    public interface ICompositeFilterService
    {
        void RegisterFilter<T>(IFilterableService<T> querableFilter);

        List<Object> GetFilter<T>();
    }
}
