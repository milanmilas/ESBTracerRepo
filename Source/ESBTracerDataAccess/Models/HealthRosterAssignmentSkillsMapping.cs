using System;
using System.Collections.Generic;

namespace ESBTracerDataAccess.Models
{
    public partial class HealthRosterAssignmentSkillsMapping
    {
        public int Id { get; set; }
        public string SkillsId { get; set; }
        public Nullable<int> HealthRosterAssignmentMapping_Id { get; set; }
        public virtual HealthRosterAssignmentMapping HealthRosterAssignmentMapping { get; set; }
    }
}
