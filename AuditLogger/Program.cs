using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var reg = db.Registrations
                .Include(r => r.Preferences)
                .ThenInclude(p => p.RegionPreferences)                
                .FirstOrDefault(r => r.Preferences.RegionPreferences.Any());

            var from = DateTime.Now;
            var pref = reg.Preferences.RegionPreferences.FirstOrDefault();
            reg.Preferences.RegionPreferences.Remove(pref);
            db.SaveChanges();

            //foreach (var log in reg.AuditLogRecords.Where(r => r.RecordedAt > from))
            //{
            //    Console.WriteLine(log.ToString());
            //}

            Console.ReadKey();
        }
    }
}
