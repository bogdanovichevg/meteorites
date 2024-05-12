namespace Infrastructure.DTO
{
    public class GeoLocation
    {
        public string Type { get; set; }
        public IEnumerable<decimal> Coordinates { get; set; }
    }
}
