using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class HealthRosterAssignmentSkillsMappingMap : EntityTypeConfiguration<HealthRosterAssignmentSkillsMapping>
    {
        public HealthRosterAssignmentSkillsMappingMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("HealthRosterAssignmentSkillsMappings");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.SkillsId).HasColumnName("SkillsId");
            this.Property(t => t.HealthRosterAssignmentMapping_Id).HasColumnName("HealthRosterAssignmentMapping_Id");

            // Relationships
            this.HasOptional(t => t.HealthRosterAssignmentMapping)
                .WithMany(t => t.HealthRosterAssignmentSkillsMappings)
                .HasForeignKey(d => d.HealthRosterAssignmentMapping_Id);

        }
    }
}
