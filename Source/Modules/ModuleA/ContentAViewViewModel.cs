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
/*    using System.Threading;
    using System.Windows.Threading;*/

    using System.Windows.Threading;

    using Microsoft.Practices.Prism.Commands;

    public class ContentAViewViewModel : IContentAViewModel
    {
        public IView View { get; set; }

        private int maxLodId = 0;

        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;

            Refresh = new DelegateCommand<object>(RefreshLogsMethod);

            Clear = new DelegateCommand(ClearLogsMethod);

            AppendNewlyAdded = new DelegateCommand(AppendNewlyAddedLogsMehod);

            GlobalCommands.RefreshCommand.RegisterCommand(Refresh);
            GlobalCommands.ClearLogsCommand.RegisterCommand(Clear);
            GlobalCommands.AppendNewlyAddedCommand.RegisterCommand(AppendNewlyAdded);
        }

        public string Message { get; set; }

        public ObservableCollection<Log> Logs { get; set; }

        public AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext ctx { get; set; }

        public DelegateCommand<object> Refresh { get; set; }

        public DelegateCommand Clear { get; set; }

        public DelegateCommand AppendNewlyAdded { get; set; }

        private void RefreshLogsMethod(object numRecords)
        {
            this.ClearLogsMethod();
            ctx.Logs.OrderByDescending(x => x.LogId).Take(int.Parse(numRecords.ToString())).ToList().ForEach(x => Logs.Add(x));
        }

        private void ClearLogsMethod()
        {
            Logs.Clear();
            maxLodId = 0;
        }

        private void AppendNewlyAddedLogsMehod()
        {
            if (maxLodId == 0) maxLodId = ctx.Logs.Max(x => x.LogId);

            var logs = ctx.Logs.Where(x => x.LogId > maxLodId).OrderBy(x => x.LogId).ToList();

            if (logs.Count > 0) maxLodId = logs.Max(i => i.LogId);

            foreach (var log in logs)
            {
                Logs.Add(log);
                Logs.Move(Logs.IndexOf(log), 0);
            }
        }
    }
}
