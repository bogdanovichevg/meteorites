using Application.DTO;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface IMeteoriteServices
    {
        public Task<int> LoadAsync();
        public Task<IEnumerable<GroupedMeteorites>> GetByFiltersAsync(ReqFilterMeteorites parms);
        public Task<IEnumerable<string>> GetAllClassesAsync();
    }
}
