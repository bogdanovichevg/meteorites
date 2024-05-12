namespace Infrastructure.DTO
{
    public interface INasaApiService
    {
        public Task<IEnumerable<NasaMeteorite>> GetMeteoriteAsync(HttpClient httpClient);
    }
}
