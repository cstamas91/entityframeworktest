using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace TwoWayRelation.Data.Models
{
    public class Preferences : IAuditableEntityNode<Registration>
    {
        public int Id { get; set; }
        public bool NSFWContent { get; set; }
        public bool RecieveLocalNewsAlerts { get; set; }
        public ICollection<RegionPreference> RegionPreferences { get; set; }

        public void Visit(IAuditableVisitor<Registration> logger, DbContext dbContext, string navigation)
        {
            var entry = dbContext.Entry(this);
            if (entry.State == EntityState.Added)
            {
                logger.LogChange($"{navigation} Felnőtt tartalom megjelenítése", NSFWContent);
                logger.LogChange($"{navigation} Helyi hír értesítések", RecieveLocalNewsAlerts);
            }

            if (entry.State == EntityState.Modified)
            {
                AuditHelpers.LogAttributeIfChanged(logger, entry, $"{navigation} Felnőtt tartalom megjelenítése", nameof(NSFWContent), NSFWContent);
                AuditHelpers.LogAttributeIfChanged(logger, entry, $"{navigation} Helyi hír értesítések", nameof(RecieveLocalNewsAlerts), RecieveLocalNewsAlerts);
            }

            foreach (var regionPref in RegionPreferences)
            {
                regionPref.Visit(logger, dbContext, "Régió preferenciák");
            }

            var removed = dbContext.ChangeTracker
                .Entries<RegionPreference>()
                .Where(e => e.State == EntityState.Deleted &&
                            (int)e.Property(nameof(RegionPreference.Preferences) + "Id").OriginalValue == Id)
                .Select(e => e.Entity)
                .ToList();

            foreach (var regionPref in removed)
            {
                regionPref.Visit(logger, dbContext, "Régió preferenciák");
            }
        }
    }
}
