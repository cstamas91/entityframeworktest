using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwoWayRelation.Data.Models
{

    public class Registration : IAuditableEntityRoot<Registration>
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PaymentInfo) + "Id")]
        public PaymentInfo PaymentInfo { get; set; }

        public Settings Settings { get; set; }
        public Preferences Preferences { get; set; }
        public string Email { get; set; }

        public virtual ICollection<AuditLogRecord> AuditLogRecords { get; set; }

        public void Visit(IAuditableVisitor<Registration> logger, DbContext dbContext)
        {
            Visit(logger, dbContext.Entry(this));
            PaymentInfo?.Visit(logger, dbContext, "Fizetési mód");
            Preferences?.Visit(logger, dbContext, "Preferenciák");
            Settings?.Visit(logger, dbContext, "Beállítások");
        }

        private void Visit(IAuditableVisitor<Registration> logger, EntityEntry entityEntry)
        {
            if (entityEntry.State == EntityState.Added)
            {
                logger.LogChange("Email", Email);
            }

            if (entityEntry.State == EntityState.Modified)
            {
                AuditHelpers.LogAttributeIfChanged(logger, entityEntry, "Email", nameof(Email), Email);
            }
        }
    }
}
