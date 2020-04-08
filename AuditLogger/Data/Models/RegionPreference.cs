namespace TwoWayRelation.Data.Models
{
    public enum Region
    {
        HU
    }

    public class RegionPreference
    {
        public int Id { get; set; }
        public Region Region { get; set; }
    }
}