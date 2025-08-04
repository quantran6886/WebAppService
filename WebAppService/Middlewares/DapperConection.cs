using Microsoft.Data.SqlClient;
using System.Data;

namespace WebAppService.Middlewares
{
    public class DapperConnection
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DapperConnection(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
