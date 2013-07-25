using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    using ESBInfrastructureLibrary;

    public class ToolbarAView : IToolbarAView
    {
        public IViewModel ViewModel { get; set; }
    }
}
