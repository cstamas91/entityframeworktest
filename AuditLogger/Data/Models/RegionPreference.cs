using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TwoWayRelation.Data.Models
{
    public enum Region
    {
        HU
    }

    public class RegionPreference : IAuditableEntityNode<Registration>
    {
        public int Id { get; set; }
        public Region Region { get; set; }
        [Required]
        public Preferences Preferences { get; set; }

        public void Visit(IAuditableVisitor<Registration> logger, DbContext dbContext, string navigation)
        {
            var entry = dbContext.Entry(this);
            if (entry.State == EntityState.Added)
            {
                logger.LogChange(navigation, $"{Region.ToFriendlyString()} hozzáadva");
            }
            else if (entry.State == EntityState.Deleted)
            {
                logger.LogChange(navigation, $"{Region.ToFriendlyString()} törölve");
            }
        }
    }
}