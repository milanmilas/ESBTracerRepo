using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    using ESBInfrastructureLibrary;

    using ESBTracerDataAccess.Models;

    class FilterAViewViewModel : IFilterAViewViewModel, INotifyPropertyChanged, IFilterableService<Log>
    {
        public IView View { get; set; }

        public ICompositeFilterService FilterService { get; set; }

        public FilterAViewViewModel(IFilterA view)
        {
            View = view;
            view.ViewModel = this;
        }

        public FilterAViewViewModel(IFilterA view, ICompositeFilterService filterService)
        {
            View = view;
            view.ViewModel = this;
            FilterService = filterService;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public IQueryable<Log> GetFilter()
        {
            Func<Log, bool> deleg = l => l.LogId > 10;

            return new EnumerableQuery<Log>(deleg);
        }
    }
}
