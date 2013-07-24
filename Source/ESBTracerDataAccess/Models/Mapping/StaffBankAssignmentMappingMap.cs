using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class StaffBankAssignmentMappingMap : EntityTypeConfiguration<StaffBankAssignmentMapping>
    {
        public StaffBankAssignmentMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("StaffBankAssignmentMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.StaffBankTrust).HasColumnName("StaffBankTrust");
            this.Property(t => t.StaffBankAssignment).HasColumnName("StaffBankAssignment");
            this.Property(t => t.HealthRosterTrust).HasColumnName("HealthRosterTrust");
            this.Property(t => t.HealthRosterGrade).HasColumnName("HealthRosterGrade");
        }
    }
}
