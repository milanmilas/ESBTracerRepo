using ESBInfrastructureLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESBTracerDataAccess.Models;
using System.Collections.ObjectModel;

namespace ModuleA
{
    public class ContentAViewViewModel : IContentAViewModel
    {
        public IView View { get; set; }

        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;
        }

        public string Message { get; set; }

        public ObservableCollection<Log> Logs { get; set; }
    }
}
