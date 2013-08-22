using ESBInfrastructureLibrary;
using Microsoft.Practices.Prism.Commands;

namespace ModuleA
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Timers;

    using ModuleA.Annotations;

    using Timer = System.Timers.Timer;

    public class ToolbarAViewViewModel : IToolbarAViewViewModel, INotifyPropertyChanged
    {
        public IView View { get; set; }

        public String RefreshRecordsNum { get; set; }

        public String RefreshSeconds { get; set; }
        

        private bool recording;

        public bool Recording {
            get
            {
                return recording;}
            set
            {
                recording = value;
                this.OnPropertyChanged("Recording");
            }
        }

        public DelegateCommand<object> StartStopRefreshingCommand { get; set; }

        private Timer backbroudWorder = new Timer();

        public ToolbarAViewViewModel(IToolbarAView view)
        {
            View = view;
            view.ViewModel = this;

            RefreshRecordsNum = "10";
            RefreshSeconds = "3";

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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
