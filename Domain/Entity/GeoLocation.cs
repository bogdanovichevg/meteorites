namespace Domain.Entity
{
    public class GeoLocation
    {
        public string Type { get; set; }
        public IEnumerable<decimal> Coordinates { get; set; }
    }
}
