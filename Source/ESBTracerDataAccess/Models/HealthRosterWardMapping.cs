using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class HealthRosterWardMapping
    {
        public int Id { get; set; }
        public string HealthRosterTrust { get; set; }
        public string HealthRosterWard { get; set; }
        public string StaffBankTrust { get; set; }
        public string StaffBankLocation { get; set; }
        public string StaffBankWard { get; set; }
    }
}
