using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class StaffBankAssignmentMapping
    {
        public int Id { get; set; }
        public string StaffBankTrust { get; set; }
        public string StaffBankAssignment { get; set; }
        public string HealthRosterTrust { get; set; }
        public string HealthRosterGrade { get; set; }
    }
}
