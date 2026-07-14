using System;
using System.Data;
using System.Text.RegularExpressions;
using CapaDatos.Repositorios;
using CapaDatos.Entidades;

namespace CapaNegocios.Servicios
{
    public class ClienteServicio
    {
        private readonly ClienteRepositorio clienteRepo;

        public ClienteServicio()
        {
            clienteRepo = new ClienteRepositorio();
        }

        public DataTable ConsultarClientes()
        {
            try
            {
                return clienteRepo.ObtenerClientes();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar clientes: {ex.Message}");
            }
        }

        public PersonaFisica ObtenerCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                    throw new ArgumentException("ID de cliente no válido");

                return clienteRepo.ObtenerClientePorId(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener cliente: {ex.Message}");
            }
        }

        public int RegistrarCliente(string dni, string nombre, string apellido, string email, 
            string telefono, string direccion, DateTime? fechaNacimiento = null)
        {
            try
            {
                ValidarDatosCliente(dni, nombre, apellido, email);

                var cliente = new PersonaFisica
                {
                    DNI = dni.Trim(),
                    Nombre = nombre.Trim(),
                    Apellido = apellido.Trim(),
                    Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim(),
                    Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
                    FechaNacimiento = fechaNacimiento,
                    TipoPersona = "Fisica",
                    Activo = true,
                    FechaCreacion = DateTime.Now
                };

                return clienteRepo.CrearCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al registrar cliente: {ex.Message}");
            }
        }

        public bool ModificarCliente(int idCliente, string dni, string nombre, string apellido, 
            string email, string telefono, string direccion, DateTime? fechaNacimiento = null)
        {
            try
            {
                if (idCliente <= 0)
                    throw new ArgumentException("ID de cliente no válido");

                ValidarDatosCliente(dni, nombre, apellido, email);

                var cliente = new PersonaFisica
                {
                    IdPersona = idCliente,
                    DNI = dni.Trim(),
                    Nombre = nombre.Trim(),
                    Apellido = apellido.Trim(),
                    Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(telefono) ? null : telefono.Trim(),
                    Direccion = string.IsNullOrWhiteSpace(direccion) ? null : direccion.Trim(),
                    FechaNacimiento = fechaNacimiento
                };

                return clienteRepo.ActualizarCliente(cliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al modificar cliente: {ex.Message}");
            }
        }

        public bool EliminarCliente(int idCliente)
        {
            try
            {
                if (idCliente <= 0)
                    throw new ArgumentException("ID de cliente no válido");

                return clienteRepo.EliminarCliente(idCliente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar cliente: {ex.Message}");
            }
        }

        public DataTable BuscarClientes(string criterio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterio))
                    return ConsultarClientes();

                return clienteRepo.BuscarClientes(criterio.Trim());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar clientes: {ex.Message}");
            }
        }

        private void ValidarDatosCliente(string dni, string nombre, string apellido, string email)
        {
            if (string.IsNullOrWhiteSpace(dni))
                throw new ArgumentException("El DNI es requerido");

            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre es requerido");

            if (string.IsNullOrWhiteSpace(apellido))
                throw new ArgumentException("El apellido es requerido");

            // Validar formato de DNI (ejemplo: 8 dígitos + 1 letra)
            if (!Regex.IsMatch(dni.Trim(), @"^\d{8}[A-Za-z]$"))
                throw new ArgumentException("El formato del DNI no es válido (8 dígitos + 1 letra)");

            // Validar nombre y apellido (solo letras y espacios)
            if (!Regex.IsMatch(nombre.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ArgumentException("El nombre solo puede contener letras");

            if (!Regex.IsMatch(apellido.Trim(), @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$"))
                throw new ArgumentException("El apellido solo puede contener letras");

            // Validar email si se proporciona
            if (!string.IsNullOrWhiteSpace(email) && !EsEmailValido(email.Trim()))
                throw new ArgumentException("El formato del email no es válido");
        }

        private bool EsEmailValido(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}