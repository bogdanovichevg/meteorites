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
        public async Task<IEnumerable<GroupedMeteorites>> GetAllByParamAsync(FiltersMeteoritesInfo filters)
        {
            using var connection = _provider.GetConnection();
            var param = new { filters.FromYear, filters.ToYear, filters.ClassName, Name = $"%{filters.MeteoriteName}%", filters.Take, filters.Skip };
            var sql = new StringBuilder();

            sql.Append($@"
                SELECT COUNT(1) OVER() as totalCount, year, count(name) as count, sum(mass) as mass FROM {TableName}
                WHERE year >= @FromYear AND year <= @ToYear AND class = @ClassName AND name LIKE @Name
                GROUP BY year"
            );
            sql.Append($" ORDER BY {filters.SortField.ToLower()} {(filters.IsDesc ? SortingDirection.DESC.ToString() : SortingDirection.ASC.ToString())}");
            sql.Append($" LIMIT @Take OFFSET @Skip ");
            
            return await connection.QueryAsync<GroupedMeteorites>(sql.ToString(), param);
        }

        public async Task<IEnumerable<string>> GetAllClassesAsync()
        {
            using var connection = _provider.GetConnection();
            var sql = $"SELECT DISTINCT class FROM {TableName}";
            return await connection.QueryAsync<string>(sql);
        }

        public async Task<int> CreateRangeAsync(IEnumerable<Meteorite> entity)
        {
            using var connection = _provider.GetConnection();
            var sql = $@"INSERT INTO {TableName} (id, name, nametype, class, mass, fall, year, geolocation)
                         VALUES (@Id, @Name, @NameType, @Class, @Mass, @Fall, @Year, @GeoLocation::jsonb)
                         ON CONFLICT (id) DO NOTHING";
            return await connection.ExecuteAsync(sql, entity);
        }
    }
}
