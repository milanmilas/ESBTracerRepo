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
    using Microsoft.Practices.Prism.Commands;

    class FilterAViewViewModel : IFilterAViewViewModel, INotifyPropertyChanged, IFilterableService<Log>
    {
        public IView View { get; set; }

        private bool dateFilter;

        public bool DateFilter
        {
            get { return dateFilter; }
            set { dateFilter = value; this.OnPropertyChanged(); }
        }

        private DateTime dateFrom;

        public DateTime DateFrom
        {
            get { return dateFrom; }
            set { dateFrom = value; this.OnPropertyChanged(); }
        }


        private DateTime dateTo;

        public DateTime DateTo
        {
            get { return dateTo; }
            set { dateTo = value; this.OnPropertyChanged(); }
        }

        private bool exceptionFilter;

        public bool ExceptionFilter
        {
            get { return exceptionFilter; }
            set { exceptionFilter = value; this.OnPropertyChanged(); }
        }
        

        public ICompositeFilterService FilterService { get; set; }

        public DelegateCommand ClearFiltersCommand { get; set; }

        public FilterAViewViewModel(IFilterA view)
        {
            View = view;
            view.ViewModel = this;
            DateFrom = DateTime.Today;
            DateTo = DateTime.Now;

            ClearFiltersCommand = new DelegateCommand(ClearFilters);
        }

        public FilterAViewViewModel(IFilterA view, ICompositeFilterService filterService)
            : this(view)
        {
            FilterService = filterService;
            filterService.RegisterFilter<Log>(this);
        }

        private void ClearFilters()
        {
            DateFilter = false;
            ExceptionFilter = false;
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


        public System.Linq.Expressions.Expression<Func<Log, bool>> GetFilter()
        {
            System.Linq.Expressions.Expression<Func<Log, bool>> func = l => true;
            if (dateFilter)
            {
                System.Linq.Expressions.Expression<Func<Log, bool>> func2 = l => l.DatePersisted >= this.DateFrom && l.DatePersisted <= this.DateTo;

                func = PredicateBuilder.And(func, func2);
            }
            if (exceptionFilter)
            {
                System.Linq.Expressions.Expression<Func<Log, bool>> func2 = l => l.LogMessage != null && l.LogMessage.Equals("Exception has occured");

                func = PredicateBuilder.And(func, func2);
            }
            return func;
        }
    }
}
