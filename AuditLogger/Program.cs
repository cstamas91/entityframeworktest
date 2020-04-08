using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwoWayRelation.Data;
using TwoWayRelation.Data.Models;

namespace TwoWayRelation
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var dbFactory = new MigrationHelper();
            using var db = dbFactory.CreateDbContext(Array.Empty<string>());
            var reg = new Registration
            {
                Email = "test@test.com",
                PaymentInfo = new PaymentInfo
                {
                    PaymentMethod = PaymentMethod.Wire
                },
                Preferences = new Preferences
                {
                    NSFWContent = false,
                    RecieveLocalNewsAlerts = false,
                    RegionPreferences = new List<RegionPreference>
                    {
                        new RegionPreference
                        {
                            Region = Region.HU
                        }
                    }
                },
                Settings = new Settings
                {
                    HideMenu = false,
                    Theme = Theme.Dark
                }
            };
            db.Registrations.Add(reg);
            db.SaveChanges();

            foreach (var log in reg.AuditLogRecords)
            {
                Console.WriteLine(log.Content);
            }

            Console.ReadKey();
        }
    }
}
