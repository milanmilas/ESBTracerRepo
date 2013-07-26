using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESBInfrastructureLibrary
{
    using Microsoft.Practices.Prism.Commands;

    public static class GlobalCommands
    {
        public static CompositeCommand RefreshCommand = new CompositeCommand();

        public static CompositeCommand ClearLogsCommand = new CompositeCommand();

        public static CompositeCommand AppendNewlyAddedCommand = new CompositeCommand();
    }
}
