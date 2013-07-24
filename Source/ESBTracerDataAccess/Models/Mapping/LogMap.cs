using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ESBTracerDataAccess.Models.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            // Primary Key
            this.HasKey(t => t.LogId);

            // Properties
            this.Property(t => t.MessageId)
                .HasMaxLength(100);

            this.Property(t => t.ExchangeId)
                .HasMaxLength(100);

            this.Property(t => t.RouteId)
                .HasMaxLength(254);

            this.Property(t => t.ContextId)
                .HasMaxLength(100);

            this.Property(t => t.CorrelationId)
                .HasMaxLength(100);

            this.Property(t => t.TransactionKey)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Logs");
            this.Property(t => t.LogId).HasColumnName("LogId");
            this.Property(t => t.MessageId).HasColumnName("MessageId");
            this.Property(t => t.ExchangeId).HasColumnName("ExchangeId");
            this.Property(t => t.Header).HasColumnName("Header");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.RouteId).HasColumnName("RouteId");
            this.Property(t => t.BreadcrumbId).HasColumnName("BreadcrumbId");
            this.Property(t => t.ContextId).HasColumnName("ContextId");
            this.Property(t => t.CorrelationId).HasColumnName("CorrelationId");
            this.Property(t => t.TransactionKey).HasColumnName("TransactionKey");
            this.Property(t => t.TagData).HasColumnName("TagData");
            this.Property(t => t.TagMessage).HasColumnName("TagMessage");
            this.Property(t => t.LogMessage).HasColumnName("LogMessage");
            this.Property(t => t.ExceptionMessage).HasColumnName("ExceptionMessage");
            this.Property(t => t.ExceptionStackTrace).HasColumnName("ExceptionStackTrace");
            this.Property(t => t.DatePersisted).HasColumnName("DatePersisted");
        }
    }
}
