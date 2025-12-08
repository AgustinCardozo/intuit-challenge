using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Cliente.Infrastructure.Data
{
    public class ClienteDapperContext(IConfiguration configuration)
    {
        public IDbConnection CreateConnection() => new NpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
    }
}
