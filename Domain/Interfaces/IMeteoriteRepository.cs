using Domain.Entity;
using Infrastructure.DTO;

namespace Domain.Interfaces
{
    public interface IMeteoriteRepository : IBaseRepository<Meteorite>
    {
        Task<IEnumerable<MeteoriteGroupRow>> GetFiltered(MeteoritesFilters filters);
        Task<IEnumerable<string>> GetAllClassesAsync();
    }
}
