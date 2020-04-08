using System;

namespace TwoWayRelation.Data.Models
{
    public class AuditLogRecord
    {
        public int Id { get; set; }
        public string Attribute { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime RecordedAt { get; set; }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(To))
            {
                return $" {RecordedAt} {Attribute}: {From}";
            }
            else
            {
                return $" {RecordedAt} {Attribute}: {From} -> {To}";
            }            
        }
    }
}