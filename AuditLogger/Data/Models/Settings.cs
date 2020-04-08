using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TwoWayRelation.Data.Models
{
    public class Settings : IAuditableEntityNode<Registration>
    {
        public int Id { get; set; }
        public Theme Theme { get; set; }
        public bool HideMenu { get; set; }

        public void Visit(IAuditableVisitor<Registration> logger, DbContext dbContext, string navigation)
        {
            var huCulture = CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(c => c.Name == "hu");
            var entry = dbContext.Entry(this);
            if (entry.State == EntityState.Added)
            {
                logger.LogChange($"{navigation} Téma", Theme.ToString());
                logger.LogChange($"{navigation} Menü megjelenítése", HideMenu.ToString(huCulture));
            }
            
            if (entry.State == EntityState.Modified)
            {
                AuditHelpers.LogAttributeIfChanged(logger, entry, $"{navigation} Téma", nameof(Theme), Theme);
                AuditHelpers.LogAttributeIfChanged(logger, entry, $"{navigation} Menü megjelenítése", nameof(HideMenu), HideMenu);
            }
        }
    }
}
