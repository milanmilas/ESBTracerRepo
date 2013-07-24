using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class HealthRosterJobCodeMappingMap : EntityTypeConfiguration<HealthRosterJobCodeMapping>
    {
        public HealthRosterJobCodeMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("HealthRosterJobCodeMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HealthRosterTrust).HasColumnName("HealthRosterTrust");
            this.Property(t => t.HealthRosterJobCode).HasColumnName("HealthRosterJobCode");
            this.Property(t => t.StaffBankJobCode).HasColumnName("StaffBankJobCode");
        }
    }
}
