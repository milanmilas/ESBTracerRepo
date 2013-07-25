using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    public class ToolbarAViewViewModel : IToolbarAViewViewModel
    {
        public IView View { get; set; }
    }
}
