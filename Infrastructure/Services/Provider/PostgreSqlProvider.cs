using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infrastructure.Services.Provider
{
    public class PostgreSqlProvider : IPostgreSqlProvider
    {
        private readonly IConfiguration _configuration;
        public PostgreSqlProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection GetConnection()
        {
            string connectionString = _configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
            return new NpgsqlConnection(connectionString);
        }
    }
}
