using System;
using System.Collections.Generic;
using System.Text;

namespace TwoWayRelation.Data.Models
{
    public class Preferences
    {
        public int Id { get; set; }
        public bool NSFWContent { get; set; }
        public bool RecieveLocalNewsAlerts { get; set; }
        public ICollection<RegionPreference> RegionPreferences { get; set; }
    }
}
