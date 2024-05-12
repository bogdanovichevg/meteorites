using Newtonsoft.Json;

namespace Infrastructure.DTO
{
    public class NasaMeteorite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        [JsonProperty("recclass")]
        public string Class { get; set; }
        public decimal Mass { get; set; }
        public string Fall { get; set; }
        public DateTime Year { get; set; }
        public GeoLocation GeoLocation { get; set; }
    }
}
