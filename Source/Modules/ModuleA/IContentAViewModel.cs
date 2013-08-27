using ESBInfrastructureLibrary;
using ESBTracerDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using Microsoft.Practices.Prism.Commands;

    interface IContentAViewModel : IViewModel
    {
        string Message { get; set; }
        ObservableCollection<LogViewModel> Logs { get; set; }
    }
}
