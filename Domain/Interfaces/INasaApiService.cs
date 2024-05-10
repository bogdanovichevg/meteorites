using Infrastructure.DTO;

namespace Domain.Interfaces
{
    public interface INasaApiService
    {
        public Task<IEnumerable<MeteoriteDTO>> GetMeteoriteInfoAsync(HttpClient httpClient);
    }
}
