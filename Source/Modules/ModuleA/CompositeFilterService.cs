using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    public class CompositeFilterService : ICompositeFilterService
    {
        readonly Dictionary<Type,List<Object>> filterDictionary  = new Dictionary<Type, List<Object>>();
 
        public void RegisterFilter<T>(IFilterableService<T> filterableService)
        {
            if (filterDictionary.ContainsKey(typeof(T)))
            {
                filterDictionary.Add(typeof(T), new List<Object>());
            }
            filterDictionary[typeof(T)].Add(filterableService.GetFilter());
        }

        public List<Object> GetFilter<T>()
        {
            if (filterDictionary.ContainsKey(typeof(T)))
            {
                return filterDictionary[typeof(T)];
            }
            return null;
        }
    }
}
