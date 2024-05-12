using Dapper;
using Domain.Consts;
using Domain.Entity;
using Domain.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using System.Text;

namespace Infrastructure.Services.Repository
{
    public class MeteoriteRepository : IMeteoriteRepository
    {
        public string TableName => "meteorite";
        private IPostgreSqlProvider _provider;

        public MeteoriteRepository(IPostgreSqlProvider provider)
        {
            _provider = provider;
        }

        public async Task<IEnumerable<MeteoriteGroupRow>> GetFiltered(MeteoritesFilters filters)
        {
            var param = new { filters.FromYear, filters.ToYear, filters.MeteoriteClass, Name = $"%{filters.MeteoriteName}%", filters.Take, filters.Skip };
            var sql = new StringBuilder();

            sql.Append($@"
                SELECT COUNT(1) OVER() as totalCount, year, count(name) as count, sum(mass) as mass FROM {TableName}
                WHERE year >= @FromYear AND year <= @ToYear AND class = @MeteoriteClass AND name ILIKE @Name
                GROUP BY year"
            );
            sql.Append($" ORDER BY {filters.SortableField.ToLower()} {(filters.IsDesc ? SortingDirection.DESC.ToString() : SortingDirection.ASC.ToString())}");
            sql.Append($" LIMIT @Take OFFSET @Skip ");

            using var connection = _provider.GetConnection();
            return await connection.QueryAsync<MeteoriteGroupRow>(sql.ToString(), param);
        }

        public async Task<IEnumerable<string>> GetAllClassesAsync()
        {
            var sql = $"SELECT DISTINCT class FROM {TableName} ORDER BY class";

            using var connection = _provider.GetConnection();
            return await connection.QueryAsync<string>(sql);
        }

        public async Task<int> AddRangeAsync(IEnumerable<Meteorite> entity)
        {
            var sql = $@"INSERT INTO {TableName} (id, name, nametype, class, mass, fall, year, geolocation)
                         VALUES (@Id, @Name, @NameType, @Class, @Mass, @Fall, @Year, @GeoLocation::jsonb)
                         ON CONFLICT (id) DO NOTHING";

            using var connection = _provider.GetConnection();
            return await connection.ExecuteAsync(sql, entity);
        }
    }
}
