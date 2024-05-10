using Domain.Entity;
using Infrastructure.DTO;

namespace Domain.Interfaces
{
    public interface IMeteoriteRepository : IBaseRepository<Meteorite>
    {
        Task<IEnumerable<GroupedMeteorites>> GetAllByParamAsync(FiltersMeteoritesInfo filters);
        Task<IEnumerable<string>> GetAllClassesAsync();
    }
}
