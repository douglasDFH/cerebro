using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CapaDatos
{
    public abstract class DbConnection
    {
        public static string cn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Bike_Store;Integrated Security=True;TrustServerCertificate=True";
        private readonly string _connectionString;
        public DbConnection()
        {
            _connectionString = cn;
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
