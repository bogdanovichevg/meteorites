using System.Data;

namespace Infrastructure.Interfaces
{
    public interface IPostgreSqlProvider
    {
        public IDbConnection GetConnection();
    }
}
