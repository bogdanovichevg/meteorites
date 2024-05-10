using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Application.Services
{
    public class MeteoriteServices : IMeteoriteServices
    {
        private readonly IMeteoriteRepository _meteoriteRepository;
        private readonly INasaApiService _nasaApiServices;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        public MeteoriteServices(
            IMeteoriteRepository meteoriteRepository, INasaApiService nasaApiServices, IMapper mapper,
            IHttpClientFactory httpClientFactory, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _httpClientFactory = httpClientFactory;
            _meteoriteRepository = meteoriteRepository;
            _nasaApiServices = nasaApiServices;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GroupedMeteorites>> GetByFiltersAsync(ReqFilterMeteorites req)
        {
            var key = JsonConvert.SerializeObject(req);
            if (!_memoryCache.TryGetValue(key, out IEnumerable<GroupedMeteorites> cache))
            {
                var filters = _mapper.Map<FiltersMeteoritesInfo>(req);
                var meteorites = await _meteoriteRepository.GetAllByParamAsync(filters);
                _memoryCache.Set(key, meteorites, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(60)));
                return meteorites;
            }
            return cache;
        }

        public async Task<IEnumerable<string>> GetAllClassesAsync()
        {
            var key = "MeteoritesClass";
            if (!_memoryCache.TryGetValue(key, out IEnumerable<string> cache))
            {
                var meteoritesClasses = await _meteoriteRepository.GetAllClassesAsync();
                _memoryCache.Set(key, meteoritesClasses, new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(1)));
                return meteoritesClasses;
            }
            return cache;
        }

        public async Task<int> LoadAsync()
        {
            using HttpClient client = _httpClientFactory.CreateClient("MeteoriteClient");
            var meteoritesDTO = await _nasaApiServices.GetMeteoriteInfoAsync(client);
            var meteorites = _mapper.Map<IEnumerable<Meteorite>>(meteoritesDTO);
            return await _meteoriteRepository.CreateRangeAsync(meteorites);
        }
    }
}
