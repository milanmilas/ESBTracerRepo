using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class HealthRosterAssignmentMappingMap : EntityTypeConfiguration<HealthRosterAssignmentMapping>
    {
        public HealthRosterAssignmentMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("HealthRosterAssignmentMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HealthRosterTrust).HasColumnName("HealthRosterTrust");
            this.Property(t => t.HealthRosterWard).HasColumnName("HealthRosterWard");
            this.Property(t => t.HealthRosterGradeType).HasColumnName("HealthRosterGradeType");
            this.Property(t => t.HealthRosterGradeCategory).HasColumnName("HealthRosterGradeCategory");
            this.Property(t => t.StaffBankAssignment).HasColumnName("StaffBankAssignment");
        }
    }
}
