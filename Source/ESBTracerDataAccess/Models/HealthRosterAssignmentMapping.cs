using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class HealthRosterAssignmentMapping
    {
        public HealthRosterAssignmentMapping()
        {
            this.HealthRosterAssignmentSkillsMappings = new List<HealthRosterAssignmentSkillsMapping>();
        }

        public int Id { get; set; }
        public string HealthRosterTrust { get; set; }
        public string HealthRosterWard { get; set; }
        public string HealthRosterGradeType { get; set; }
        public string HealthRosterGradeCategory { get; set; }
        public string StaffBankAssignment { get; set; }
        public virtual ICollection<HealthRosterAssignmentSkillsMapping> HealthRosterAssignmentSkillsMappings { get; set; }
    }
}
