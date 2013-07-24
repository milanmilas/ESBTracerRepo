using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string MessageId { get; set; }
        public string ExchangeId { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string RouteId { get; set; }
        public string BreadcrumbId { get; set; }
        public string ContextId { get; set; }
        public string CorrelationId { get; set; }
        public string TransactionKey { get; set; }
        public string TagData { get; set; }
        public string TagMessage { get; set; }
        public string LogMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
        public System.DateTime DatePersisted { get; set; }
    }
}
