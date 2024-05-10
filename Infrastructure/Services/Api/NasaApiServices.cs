using Domain.Interfaces;
using Infrastructure.DTO;
using Newtonsoft.Json;

namespace Infrastructure.Services.Api
{
    public class NasaApiServices : INasaApiService
    {
        public NasaApiServices() { }

        public async Task<IEnumerable<MeteoriteDTO>> GetMeteoriteInfoAsync(HttpClient httpClient)
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://data.nasa.gov/resource/y77d-th95.json");
            if (!response.IsSuccessStatusCode)
            {
                //log err
                throw new Exception();
            }
            string json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<MeteoriteDTO>>(json);
        }
    }
}
