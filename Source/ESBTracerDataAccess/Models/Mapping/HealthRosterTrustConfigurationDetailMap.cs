using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class HealthRosterTrustConfigurationDetailMap : EntityTypeConfiguration<HealthRosterTrustConfigurationDetail>
    {
        public HealthRosterTrustConfigurationDetailMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TrustCode)
                .HasMaxLength(20);

            this.Property(t => t.UserName)
                .HasMaxLength(50);

            this.Property(t => t.Password)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("HealthRosterTrustConfigurationDetails");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Url).HasColumnName("Url");
            this.Property(t => t.LastProcessedDateTime).HasColumnName("LastProcessedDateTime");
            this.Property(t => t.TrustCode).HasColumnName("TrustCode");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Session).HasColumnName("Session");
        }
    }
}
