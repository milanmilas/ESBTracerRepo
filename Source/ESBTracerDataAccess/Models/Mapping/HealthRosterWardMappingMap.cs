using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class HealthRosterWardMappingMap : EntityTypeConfiguration<HealthRosterWardMapping>
    {
        public HealthRosterWardMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("HealthRosterWardMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.HealthRosterTrust).HasColumnName("HealthRosterTrust");
            this.Property(t => t.HealthRosterWard).HasColumnName("HealthRosterWard");
            this.Property(t => t.StaffBankTrust).HasColumnName("StaffBankTrust");
            this.Property(t => t.StaffBankLocation).HasColumnName("StaffBankLocation");
            this.Property(t => t.StaffBankWard).HasColumnName("StaffBankWard");
        }
    }
}
