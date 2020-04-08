using AuditLogger.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TwoWayRelation.Data.Models;

namespace TwoWayRelation.Services.Audit
{
    public class RegistrationAuditLogger : IAuditableVisitor<Registration>
    {
        private readonly List<AuditLogRecord> changes = new List<AuditLogRecord>();

        public void Audit(Registration auditableRoot, DbContext dbContext)
        {
            auditableRoot.Visit(this, dbContext);
        }
        protected readonly CultureInfo huCulture = 
            CultureInfo.GetCultures(CultureTypes.SpecificCultures).FirstOrDefault(c => c.Name == "hu");
        public void LogChange<TAttrib>(string attribute, TAttrib from)
        {
            switch (from)
            {
                case int f:
                    LogChange(attribute, f.ToString(huCulture));
                    return;
                case bool f:
                    LogChange(attribute, f.ToString(huCulture));
                    return;
                case string f:
                    LogChange(attribute, f);
                    return;
                case decimal f:
                    LogChange(attribute, f.ToString(huCulture));
                    return;
                case long f:
                    LogChange(attribute, f.ToString(huCulture));
                    return;
                case DateTime f:
                    LogChange(attribute, f.ToString(huCulture));
                    return;
            }
        }

        public void LogChange<TAttrib>(string attribute, TAttrib from, TAttrib to)
        {
            switch (from, to)
            {
                case (int f, int t):
                    LogChange(attribute, f.ToString(huCulture), t.ToString(huCulture));
                    return;
                case (bool f, bool t):
                    LogChange(attribute, f.ToString(huCulture), t.ToString(huCulture));
                    return;
                case (string f, string t):
                    LogChange(attribute, f, t);
                    return;
                case (decimal f, decimal t):
                    LogChange(attribute, f.ToString(huCulture), t.ToString(huCulture));
                    return;
                case (long f, long t):
                    LogChange(attribute, f.ToString(huCulture), t.ToString(huCulture));
                    return;
                case (DateTime f, DateTime t):
                    LogChange(attribute, f.ToString(huCulture), t.ToString(huCulture));
                    return;
                case (PaymentMethod f, PaymentMethod t):
                    LogChange(attribute, f.ToFriendlyString(), t.ToFriendlyString());
                    return;
            }
        }
        protected void LogChange(string a, string f)
        {
            changes.Add(new AuditLogRecord
            {
                Attribute = a,
                From = f,
                To = string.Empty,
                RecordedAt = DateTime.Now
            });
        }

        protected void LogChange(string a, string f, string t)
        {
            changes.Add(new AuditLogRecord
            {
                Attribute = a,
                From = f,
                To = t,
                RecordedAt = DateTime.Now
            });
        }

        public IEnumerable<AuditLogRecord> GetLogRecords()
        {
            return changes;
        }
    }
}
