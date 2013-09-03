using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

    public interface IToolbarAViewViewModel : IViewModel
    {
        DelegateCommand<object> StartStopRefreshingCommand { get; set; }

        DelegateCommand<object> PopUpCommand { get; set; }

        IInteractionRequest ConfirmCancelInteractionRequest { get; }

        bool Recording { get; set; }
    }
}
