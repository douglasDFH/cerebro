using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DAnimal : DbConnection
    {
        private int _idAnimal;
        private int _idPropietario;
        private string _nombre;
        private string _tipo;
        private string _raza;
        private string _color;
        private char? _sexo;
        private DateTime? _fechaNacimiento;
        private int? _edad;
        private decimal? _peso;
        private decimal? _altura;
        private string _microchip;
        private string _numPedigree;
        private bool _esterilizado;
        private bool _vacunado;
        private bool _estado;
        private string _observaciones;
        private string _textoBuscar;

        public int IdAnimal { get => _idAnimal; set => _idAnimal = value; }
        public int IdPropietario { get => _idPropietario; set => _idPropietario = value; }
        public string Nombre { get => _nombre; set => _nombre = value; }
        public string Tipo { get => _tipo; set => _tipo = value; }
        public string Raza { get => _raza; set => _raza = value; }
        public string Color { get => _color; set => _color = value; }
        public char? Sexo { get => _sexo; set => _sexo = value; }
        public DateTime? FechaNacimiento { get => _fechaNacimiento; set => _fechaNacimiento = value; }
        public int? Edad { get => _edad; set => _edad = value; }
        public decimal? Peso { get => _peso; set => _peso = value; }
        public decimal? Altura { get => _altura; set => _altura = value; }
        public string Microchip { get => _microchip; set => _microchip = value; }
        public string NumPedigree { get => _numPedigree; set => _numPedigree = value; }
        public bool Esterilizado { get => _esterilizado; set => _esterilizado = value; }
        public bool Vacunado { get => _vacunado; set => _vacunado = value; }
        public bool Estado { get => _estado; set => _estado = value; }
        public string Observaciones { get => _observaciones; set => _observaciones = value; }
        public string TextoBuscar { get => _textoBuscar; set => _textoBuscar = value; }

        public DAnimal() { }

        public string Insertar(DAnimal animal)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_InsertarAnimal", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@IdPropietario", animal.IdPropietario);
                        command.Parameters.AddWithValue("@Nombre", animal.Nombre);
                        command.Parameters.AddWithValue("@Tipo", animal.Tipo);
                        command.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Color", animal.Color ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Sexo", animal.Sexo?.ToString() ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Peso", animal.Peso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Microchip", animal.Microchip ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Observaciones", animal.Observaciones ?? (object)DBNull.Value);

                        var result = command.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int idAnimal))
                        {
                            animal.IdAnimal = idAnimal;
                            rpta = "OK";
                        }
                        else
                        {
                            rpta = "No se insertó el registro";
                        }
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Editar(DAnimal animal)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Animal 
                        SET Nombre = @Nombre,
                            Tipo = @Tipo,
                            Raza = @Raza,
                            Color = @Color,
                            Sexo = @Sexo,
                            FechaNacimiento = @FechaNacimiento,
                            Edad = CASE 
                                WHEN @FechaNacimiento IS NOT NULL 
                                THEN DATEDIFF(YEAR, @FechaNacimiento, GETDATE())
                                ELSE @Edad
                            END,
                            Peso = @Peso,
                            Altura = @Altura,
                            Microchip = @Microchip,
                            NumPedigree = @NumPedigree,
                            Esterilizado = @Esterilizado,
                            Vacunado = @Vacunado,
                            Observaciones = @Observaciones,
                            FechaModificacion = GETDATE()
                        WHERE IdAnimal = @IdAnimal";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);
                        command.Parameters.AddWithValue("@Nombre", animal.Nombre);
                        command.Parameters.AddWithValue("@Tipo", animal.Tipo);
                        command.Parameters.AddWithValue("@Raza", animal.Raza ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Color", animal.Color ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Sexo", animal.Sexo?.ToString() ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@FechaNacimiento", animal.FechaNacimiento ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Edad", animal.Edad ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Peso", animal.Peso ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Altura", animal.Altura ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Microchip", animal.Microchip ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@NumPedigree", animal.NumPedigree ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Esterilizado", animal.Esterilizado);
                        command.Parameters.AddWithValue("@Vacunado", animal.Vacunado);
                        command.Parameters.AddWithValue("@Observaciones", animal.Observaciones ?? (object)DBNull.Value);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se actualizó el registro";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public string Eliminar(DAnimal animal)
        {
            string rpta = "";
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        UPDATE Animal 
                        SET Estado = 0,
                            FechaModificacion = GETDATE()
                        WHERE IdAnimal = @IdAnimal";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@IdAnimal", animal.IdAnimal);

                        rpta = command.ExecuteNonQuery() == 1 ? "OK" : "No se eliminó el registro";
                    }
                }
                catch (Exception ex)
                {
                    rpta = ex.Message;
                }
            }
            return rpta;
        }

        public DataTable Mostrar()
        {
            DataTable dtResultado = new DataTable("Animal");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    // Intentar usar la vista primero
                    string sql = @"
                        SELECT * FROM VW_AnimalesConPropietarios
                        ORDER BY FechaRegistro DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    // Si la vista no existe, usar consulta directa
                    try
                    {
                        string sqlDirecto = @"
                            SELECT 
                                a.IdAnimal,
                                a.Nombre AS NombreAnimal,
                                a.Tipo,
                                a.Raza,
                                a.Color,
                                a.Sexo,
                                a.FechaNacimiento,
                                a.Edad,
                                a.Peso,
                                a.Altura,
                                a.Microchip,
                                a.NumPedigree,
                                a.Esterilizado,
                                a.Vacunado,
                                a.Observaciones,
                                p.IdPersona AS IdPropietario,
                                CASE 
                                    WHEN p.TipoPersona = 'F' THEN p.Nombre + ' ' + ISNULL(p.Apellidos, '')
                                    ELSE p.RazonSocial
                                END AS NombrePropietario,
                                p.Telefono,
                                p.Email,
                                a.FechaRegistro
                            FROM Animal a
                            INNER JOIN Persona p ON a.IdPropietario = p.IdPersona
                            WHERE a.Estado = 1 AND p.Estado = 1
                            ORDER BY a.FechaRegistro DESC";

                        using (SqlCommand command = new SqlCommand(sqlDirecto, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dtResultado);
                            }
                        }
                    }
                    catch
                    {
                        dtResultado = null;
                    }
                }
            }
            return dtResultado;
        }

        public DataTable BuscarPorPropietario(int idPropietario)
        {
            DataTable dtResultado = new DataTable("Animal");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand("SP_BuscarAnimalesPorPropietario", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@IdPropietario", idPropietario);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable BuscarTexto(DAnimal animal)
        {
            DataTable dtResultado = new DataTable("Animal");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT * FROM VW_AnimalesConPropietarios
                        WHERE NombreAnimal LIKE '%' + @TextoBuscar + '%'
                           OR Tipo LIKE '%' + @TextoBuscar + '%'
                           OR Raza LIKE '%' + @TextoBuscar + '%'
                           OR NombrePropietario LIKE '%' + @TextoBuscar + '%'
                           OR Microchip LIKE '%' + @TextoBuscar + '%'
                        ORDER BY FechaRegistro DESC";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@TextoBuscar", animal.TextoBuscar ?? "");

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }

        public DataTable BuscarPorTipo(string tipo)
        {
            DataTable dtResultado = new DataTable("Animal");
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                try
                {
                    string sql = @"
                        SELECT * FROM VW_AnimalesConPropietarios
                        WHERE Tipo = @Tipo
                        ORDER BY NombreAnimal";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Tipo", tipo);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dtResultado);
                        }
                    }
                }
                catch
                {
                    dtResultado = null;
                }
            }
            return dtResultado;
        }
    }
}