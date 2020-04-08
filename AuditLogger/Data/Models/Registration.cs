using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TwoWayRelation.Data.Models
{

    public class Registration
    {
        public int Id { get; set; }
        [ForeignKey(nameof(PaymentInfo) + "Id")]
        public PaymentInfo PaymentInfo { get; set; }

        public Settings Settings { get; set; }
        public Preferences Preferences { get; set; }
        public string Email { get; set; }

        public virtual ICollection<AuditLogRecord> AuditLogRecords { get; set; }
    }
}
