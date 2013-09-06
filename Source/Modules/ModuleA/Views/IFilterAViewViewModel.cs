using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModuleA
{
    using ESBInfrastructureLibrary;
    using Microsoft.Practices.Prism.Commands;

    public interface IFilterAViewViewModel : IViewModel
    {
        DelegateCommand ClearFiltersCommand { get; set; }
    }
}
