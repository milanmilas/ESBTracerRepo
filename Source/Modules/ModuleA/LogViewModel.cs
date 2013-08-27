using ESBTracerDataAccess.Models;
using ModuleA.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ModuleA
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private Log log;

        public LogViewModel(Log log)
        {
            this.log = log;
        }

        private int logId;

        public int LogId
        {
            get
            {
                return log.LogId;
            }
            set
            {
                log.LogId = value;
                this.OnPropertyChanged("LogId");
            }
        }

        public System.DateTime DatePersisted
        {
            get
            {
                return log.DatePersisted;
            }
            set
            {
                log.DatePersisted = value;
                this.OnPropertyChanged("DatePersisted");
            }
        }

        public string RouteId
        {
            get
            {
                return log.RouteId;
            }
            set
            {
                log.RouteId = value;
                this.OnPropertyChanged("RouteId");
            }
        }

        public string LogMessage
        {
            get
            {
                return log.LogMessage;
            }
            set
            {
                log.LogMessage = value;
                this.OnPropertyChanged("LogMessage");
            }
        }

        public string Header
        {
            get
            {
                return log.Header;
            }
            set
            {
                log.Header = value;
                this.OnPropertyChanged("Header");
            }
        }

        public string Body
        {
            get
            {
                return log.Body;
            }
            set
            {
                log.Body = value;
                this.OnPropertyChanged("Body");
            }
        }

        public string MessageId
        {
            get
            {
                return log.MessageId;
            }
            set
            {
                log.MessageId = value;
                this.OnPropertyChanged("MessageId");
            }
        }

        public string ExchangeId
        {
            get
            {
                return log.ExchangeId;
            }
            set
            {
                log.ExchangeId = value;
                this.OnPropertyChanged("ExchangeId");
            }
        }

        public string ExceptionMessage
        {
            get
            {
                return log.ExceptionMessage;
            }
            set
            {
                log.ExceptionMessage = value;
                this.OnPropertyChanged("ExceptionMessage");
            }
        }

        public string ExceptionStackTrace
        {
            get
            {
                return log.ExceptionStackTrace;
            }
            set
            {
                log.ExceptionStackTrace = value;
                this.OnPropertyChanged("ExceptionStackTrace");
            }
        }

        public string BreadcrumbId
        {
            get
            {
                return log.BreadcrumbId;
            }
            set
            {
                log.BreadcrumbId = value;
                this.OnPropertyChanged("BreadcrumbId");
            }
        }

        public string ContextId
        {
            get
            {
                return log.ContextId;
            }
            set
            {
                log.ContextId = value;
                this.OnPropertyChanged("ContextId");
            }
        }

        public string CorrelationId
        {
            get
            {
                return log.CorrelationId;
            }
            set
            {
                log.CorrelationId = value;
                this.OnPropertyChanged("CorrelationId");
            }
        }

        public string TransactionKey
        {
            get
            {
                return log.TransactionKey;
            }
            set
            {
                log.TransactionKey = value;
                this.OnPropertyChanged("TransactionKey");
            }
        }

        public string TagData
        {
            get
            {
                return log.TagData;
            }
            set
            {
                log.TagData = value;
                this.OnPropertyChanged("TagData");
            }
        }

        public string TagMessage
        {
            get
            {
                return log.TagMessage;
            }
            set
            {
                log.TagMessage = value;
                this.OnPropertyChanged("TagMessage");
            }
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
