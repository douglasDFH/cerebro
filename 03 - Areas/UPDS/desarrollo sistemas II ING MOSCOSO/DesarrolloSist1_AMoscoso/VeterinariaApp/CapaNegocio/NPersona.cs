using CapaDatos;
using System;
using System.Data;

namespace CapaNegocio
{
    public class NPersona
    {
        // Método para insertar persona física
        public static string InsertarFisica(
            string nombre, string apellidos, string email, string direccion, string telefono,
            byte[] foto, string dni, DateTime? fechaNacimiento, char genero)
        {
            DPersona obj = new DPersona
            {
                Nombre = nombre,
                Apellidos = apellidos,
                Tipo_persona = 'F',
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Foto = foto,
                Dni = dni,
                Fecha_nacimiento = fechaNacimiento,
                Genero = genero
            };
            return obj.Insertar(obj);
        }

        // Método para insertar persona jurídica
        public static string InsertarJuridica(
            string nombre, string apellidos, string email, string direccion, string telefono,
            byte[] foto, string cif, string razonSocial, DateTime? fechaConstitucion, string actividadPrincipal)
        {
            DPersona obj = new DPersona
            {
                Nombre = nombre,
                Apellidos = apellidos,
                Tipo_persona = 'J',
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Foto = foto,
                Cif = cif,
                Razon_social = razonSocial,
                Fecha_constitucion = fechaConstitucion,
                Actividad_principal = actividadPrincipal
            };
            return obj.Insertar(obj);
        }

        // Método para editar persona física
        public static string EditarFisica(
            int personaId, string nombre, string apellidos, string email, string direccion,
            string telefono, byte[] foto, string dni, DateTime? fechaNacimiento, char genero)
        {
            DPersona obj = new DPersona
            {
                Persona_id = personaId,
                Nombre = nombre,
                Apellidos = apellidos,
                Tipo_persona = 'F',
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Foto = foto,
                Dni = dni,
                Fecha_nacimiento = fechaNacimiento,
                Genero = genero
            };
            return obj.Editar(obj);
        }

        // Método para editar persona jurídica
        public static string EditarJuridica(
            int personaId, string nombre, string apellidos, string email, string direccion,
            string telefono, byte[] foto, string cif, string razonSocial, DateTime? fechaConstitucion, string actividadPrincipal)
        {
            DPersona obj = new DPersona
            {
                Persona_id = personaId,
                Nombre = nombre,
                Apellidos = apellidos,
                Tipo_persona = 'J',
                Email = email,
                Direccion = direccion,
                Telefono = telefono,
                Foto = foto,
                Cif = cif,
                Razon_social = razonSocial,
                Fecha_constitucion = fechaConstitucion,
                Actividad_principal = actividadPrincipal
            };
            return obj.Editar(obj);
        }

        public static string Eliminar(int personaId, char tipoPersona)
        {
            DPersona obj = new DPersona
            {
                Persona_id = personaId,
                Tipo_persona = tipoPersona
            };
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DPersona().Mostrar();
        }

        public static DataTable BuscarPorNombre(string textoBuscar)
        {
            return new DPersona().BuscarPorNombre(textoBuscar);
        }

        public static DataTable BuscarPorDocumento(string textoBuscar, char tipoPersona)
        {
            return new DPersona().BuscarPorDocumento(textoBuscar, tipoPersona);
        }
    }
}