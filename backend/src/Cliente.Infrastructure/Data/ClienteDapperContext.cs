using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Cliente.Infrastructure.Data
{
    public interface IClienteDapperContext
    {
        IDbConnection CreateConnection(); 
    }

    public class ClienteDapperContext(IConfiguration configuration) : IClienteDapperContext
    {
        public IDbConnection CreateConnection() => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
