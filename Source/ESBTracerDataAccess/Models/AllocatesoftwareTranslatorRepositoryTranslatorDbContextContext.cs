using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using ESBTracerDataAccess.Models.Mapping;

namespace ESBTracerDataAccess.Models
{
    public partial class AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext : DbContext
    {
        static AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext()
        {
            Database.SetInitializer<AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext>(null);
        }

        public AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext()
            : base("Name=AllocatesoftwareTranslatorRepositoryTranslatorDbContextContext")
        {
        }

        public DbSet<HealthRosterAssignmentMapping> HealthRosterAssignmentMappings { get; set; }
        public DbSet<HealthRosterAssignmentSkillsMapping> HealthRosterAssignmentSkillsMappings { get; set; }
        public DbSet<HealthRosterJobCodeMapping> HealthRosterJobCodeMappings { get; set; }
        public DbSet<HealthRosterTrustConfigurationDetail> HealthRosterTrustConfigurationDetails { get; set; }
        public DbSet<HealthRosterWardMapping> HealthRosterWardMappings { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<StaffBankAssignmentMapping> StaffBankAssignmentMappings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new HealthRosterAssignmentMappingMap());
            modelBuilder.Configurations.Add(new HealthRosterAssignmentSkillsMappingMap());
            modelBuilder.Configurations.Add(new HealthRosterJobCodeMappingMap());
            modelBuilder.Configurations.Add(new HealthRosterTrustConfigurationDetailMap());
            modelBuilder.Configurations.Add(new HealthRosterWardMappingMap());
            modelBuilder.Configurations.Add(new LogMap());
            modelBuilder.Configurations.Add(new StaffBankAssignmentMappingMap());
        }
    }
}
