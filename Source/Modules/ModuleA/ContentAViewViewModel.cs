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
    using Microsoft.Practices.Prism.Commands;

    public class ContentAViewViewModel : IContentAViewModel
    {
        public IView View { get; set; }

        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;

            Refresh = new DelegateCommand(ExecuteMethod);

            GlobalCommands.RefreshCommand.RegisterCommand(Refresh);
        }

        public string Message { get; set; }

        public ObservableCollection<Log> Logs { get; set; }

        public AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext ctx { get; set; }

        public DelegateCommand Refresh { get; set; }

        private void ExecuteMethod()
        {
            Logs.Clear();
            ctx.Logs.OrderByDescending(x=> x.LogId).Take(10).ToList().ForEach(x => Logs.Add(x));
        }
    }
}
