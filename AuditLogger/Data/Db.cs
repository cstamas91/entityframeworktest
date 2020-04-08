using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using TwoWayRelation.Data.Models;
using TwoWayRelation.Services.Audit;

namespace TwoWayRelation.Data
{
    public class Db : DbContext
    {
        public Db(DbContextOptions<Db> options) : base(options)
        {

        }

        public DbSet<Registration> Registrations { get; set; }
        public DbSet<PaymentInfo> PaymentInfo { get; set; }
        public DbSet<Preferences> Preferences { get; set; }
        public DbSet<RegionPreference> RegionPreferences { get; set; }

        public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
        {
            return base.Remove(entity);
        }

        public override int SaveChanges()
        {
            foreach (var reg in Registrations.Local)
            {
                var logger = new RegistrationAuditLogger();
                reg.Visit(logger, this);
                if (reg.AuditLogRecords == null)
                    reg.AuditLogRecords = new List<AuditLogRecord>();

                foreach(var record in logger.GetLogRecords())
                    reg.AuditLogRecords.Add(record);
            }

            return base.SaveChanges();
        }
    }
}
