using ESBInfrastructureLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
