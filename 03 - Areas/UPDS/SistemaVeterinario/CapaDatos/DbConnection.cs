using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{
    public class DbConnection
    {
        private static DbConnection _instance;
        private static readonly object _lock = new object();
        private SqlConnection _connection;
        private static readonly string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Sistema_Veterinario;Integrated Security=True;TrustServerCertificate=True;Connection Timeout=30;";

        private DbConnection()
        {
            try
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al conectar con la base de datos: {ex.Message}", ex);
            }
        }

        public static DbConnection Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new DbConnection();
                    }
                }
                return _instance;
            }
        }

        public SqlConnection GetConnection()
        {
            // Verificar si la conexión está cerrada o rota y reconectar si es necesario
            if (_connection == null || _connection.State != System.Data.ConnectionState.Open)
            {
                try
                {
                    _connection?.Close();
                    _connection?.Dispose();
                    _connection = new SqlConnection(connectionString);
                    _connection.Open();
                }
                catch (Exception ex)
                {
                    // Log del error si es necesario
                    throw new Exception($"Error al reconectar a la base de datos: {ex.Message}", ex);
                }
            }
            return _connection;
        }

        public void CloseConnection()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        public static void CloseInstance()
        {
            if (_instance != null)
            {
                _instance.CloseConnection();
                _instance = null;
            }
        }
        public static string GetConnectionString()
        {
            return connectionString;
        }
    }
}