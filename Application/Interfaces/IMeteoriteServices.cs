using Application.DTO;
using Domain.Entity;

namespace Application.Interfaces
{
    public interface IMeteoriteServices
    {
        public Task<int> LoadAsync();
        public Task<IEnumerable<MeteoriteGroupRow>> GetFiltered(MeteoritesFiltersReq parms);
        public Task<IEnumerable<string>> GetAllClassesAsync();
    }
}
