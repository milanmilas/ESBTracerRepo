using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    using Microsoft.Practices.Prism.Commands;

    public interface IToolbarAViewViewModel : IViewModel
    {
        DelegateCommand<object> StartStopRefreshingCommand { get; set; }

        bool Recording { get; set; }
    }
}
