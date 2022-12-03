using System.Data;
using System.Data.SqlClient;

namespace GymProject.Repository
{
    public class DapperContext : IDbContext
    {
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public DapperContext(IConfiguration config)
        {
            _config = config;
            _connectionString = _config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
