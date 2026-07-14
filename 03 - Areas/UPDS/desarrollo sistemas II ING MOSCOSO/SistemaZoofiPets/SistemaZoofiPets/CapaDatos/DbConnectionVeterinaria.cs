using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public abstract class DbConnectionVeterinaria
    {
        public static string cn = "server=(localdb)\\MSSQLLocalDB;database=SistemaVeterinario;integrated security=true;TrustServerCertificate=true;";
        private readonly string _connectionString;
        
        public DbConnectionVeterinaria()
        {
            _connectionString = cn;
        }
        
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}