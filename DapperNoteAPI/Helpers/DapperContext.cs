using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperNoteAPI.Helpers
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _conString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _conString = _configuration.GetConnectionString("DefaultConnection");
        }
        public IDbConnection CreateConnection() => new SqlConnection(_conString);
        
    }
}
