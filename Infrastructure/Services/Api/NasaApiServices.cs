using Infrastructure.DTO;
using Newtonsoft.Json;

namespace Infrastructure.Services.Api
{
    public class NasaApiServices : INasaApiService
    {
        public NasaApiServices() { }

        public async Task<IEnumerable<NasaMeteorite>> GetMeteoriteAsync(HttpClient httpClient)
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://data.nasa.gov/resource/y77d-th95.json");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception();
            }
            string json = await response.Content.ReadAsStringAsync();
            var meteorites = JsonConvert.DeserializeObject<IEnumerable<NasaMeteorite>>(json);
            return meteorites!;
        }
    }
}
