using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class HealthRosterTrustConfigurationDetail
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public System.DateTime LastProcessedDateTime { get; set; }
        public string TrustCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Session { get; set; }
    }
}
