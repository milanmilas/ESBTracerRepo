using ESBInfrastructureLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    interface IContentAViewModel : IViewModel
    {
        string Message { get; set; }
    }
}
