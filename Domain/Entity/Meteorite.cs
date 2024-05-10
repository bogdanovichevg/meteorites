using Newtonsoft.Json;

namespace Domain.Entity
{
    public class Meteorite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        [JsonProperty("recclass")]
        public string Class { get; set; }
        public decimal Mass { get; set; }
        public string Fall { get; set; }
        public short Year { get; set; }
        public string GeoLocation { get; set; }
    }
}
