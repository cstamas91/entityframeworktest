using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TwoWayRelation.Data.Models;

namespace TwoWayRelation.Services.Audit
{
    public static class AuditExtensions
    {
        private static void RecordIfChanged(AuditLogger logger, string prop, EntityEntry entry, int indent)
        {
            var from = entry.OriginalValues[prop].ToString();
            var to = entry.CurrentValues[prop].ToString();
            if (entry.State == EntityState.Added)
            {
                logger.Changed(prop, from, indent);
            }
            else if (entry.State == EntityState.Modified)
            {
                if (from != to)
                {
                    logger.Changed(prop, from, to, indent);
                }
            }
        }

        public static void Visit(
            this Registration registration,
            AuditLogger logger,
            DbContext db,
            int indent = 0)
        {
            var entry = db.Entry(registration);
            switch (entry.State)
            {
                case EntityState.Added:
                case EntityState.Modified:
                    logger.Changed($"({entry.State.ToString()}){nameof(Registration)}", string.Empty, indent);
                    break;
                default:
                    break;
            }

            RecordIfChanged(logger, nameof(registration.Email), entry, indent + 2);
            registration.PaymentInfo.Visit(logger, db, indent + 2);
            registration.Preferences.Visit(logger, db, indent + 2);
            registration.Settings.Visit(logger, db, indent + 2);
        }

        public static void Visit(
            this PaymentInfo paymentInfo,
            AuditLogger logger,
            DbContext db,
            int indent = 0)
        {
            var entry = db.Entry(paymentInfo);
            RecordIfChanged(logger, nameof(paymentInfo.PaymentMethod), entry, indent);
        }

        public static void Visit(
            this Preferences preferences,
            AuditLogger logger,
            DbContext db,
            int indent = 0)
        {
            var entry = db.Entry(preferences);
            RecordIfChanged(logger, nameof(preferences.NSFWContent), entry, indent);
            RecordIfChanged(logger, nameof(preferences.RecieveLocalNewsAlerts), entry, indent);
            foreach (var regionPreference in preferences.RegionPreferences)
                regionPreference.Visit(logger, db, indent + 2);
        }

        public static void Visit(
            this RegionPreference regionPreference,
            AuditLogger logger,
            DbContext db,
            int indent = 0)
        {
            var entry = db.Entry(regionPreference);
            switch (entry.State)
            {
                case EntityState.Added:
                    logger.Changed(
                        $"({EntityState.Added.ToString()}){nameof(RegionPreference)}",
                        string.Empty, 
                        indent);
                    logger.Changed(nameof(RegionPreference.Region), regionPreference.Region.ToString(), indent + 2);
                    break;
                case EntityState.Modified:
                    logger.Changed(
                        $"({EntityState.Modified.ToString()}){nameof(RegionPreference)}",
                        string.Empty, 
                        indent);
                    RecordIfChanged(logger, nameof(RegionPreference.Region), entry, indent + 2);
                    break;
                default:
                    break;
            }
        }

        public static void Visit(
            this Settings settings,
            AuditLogger logger,
            DbContext db,
            int indent = 0)
        {
            var entry = db.Entry(settings);
            RecordIfChanged(logger, nameof(settings.HideMenu), entry, indent);
            RecordIfChanged(logger, nameof(settings.Theme), entry, indent);
        }
    }
}
