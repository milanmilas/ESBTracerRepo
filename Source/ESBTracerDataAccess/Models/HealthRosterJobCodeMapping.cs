using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class HealthRosterJobCodeMapping
    {
        public int Id { get; set; }
        public string HealthRosterTrust { get; set; }
        public string HealthRosterJobCode { get; set; }
        public string StaffBankJobCode { get; set; }
    }
}
