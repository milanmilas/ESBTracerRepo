using ESBInfrastructureLibrary;
using Microsoft.Practices.Prism.Commands;

namespace ModuleA
{
    using System;
    using System.Threading;
    using System.Timers;

    using Timer = System.Timers.Timer;

    public class ToolbarAViewViewModel : IToolbarAViewViewModel
    {
        public IView View { get; set; }

        public bool Recording { get; set; }

        public DelegateCommand<object> StartStopRefreshingCommand { get; set; }

        private Timer backbroudWorder = new Timer();

        public ToolbarAViewViewModel(IToolbarAView view)
        {
            View = view;
            view.ViewModel = this;

            StartStopRefreshingCommand = new DelegateCommand<object>(StartRecording);
        }

        private void StartRecording(object refreshTime)
        {
            if (!Recording)
            {
                Recording = true;

                backbroudWorder.Interval = int.Parse(refreshTime.ToString())*1000;

                backbroudWorder.Elapsed += BackbroudWorderOnElapsed;

                backbroudWorder.Start();
            }
            else
            {
                Recording = false;

                backbroudWorder.Stop();
            }
        }

        private void BackbroudWorderOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal, method: (Action)this.RefreshAppendLogs);
        }

        private void RefreshAppendLogs()
        {
/*            backbroudWorder.Stop();*/
            GlobalCommands.AppendNewlyAddedCommand.RegisteredCommands[0].Execute(null);
        }

    }
}
