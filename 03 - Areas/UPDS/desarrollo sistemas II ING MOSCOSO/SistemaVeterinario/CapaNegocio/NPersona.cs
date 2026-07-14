using CapaDatos;
using System;
using System.Data;
using System.Linq;

namespace CapaNegocio
{
    public class NPersona
    {
        public static string InsertarPersonaFisica(string dni, string nombre, string apellidos, char genero,
            string email = null, string direccion = null, string telefono = null, string observaciones = null)
        {
            if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos))
                return "DNI, nombre y apellidos son obligatorios para personas físicas";

            DPersona obj = new DPersona();
            obj.TipoPersona = 'F';
            obj.DNI = dni;
            obj.Nombre = nombre;
            obj.Apellidos = apellidos;
            obj.Genero = genero;
            obj.Email = email;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Observaciones = observaciones;
            obj.Estado = true;

            return obj.InsertarPersonaFisica(obj);
        }

        public static string InsertarPersonaJuridica(string cif, string razonSocial,
            string email = null, string direccion = null, string telefono = null, string observaciones = null)
        {
            if (string.IsNullOrWhiteSpace(cif) || string.IsNullOrWhiteSpace(razonSocial))
                return "CIF y razón social son obligatorios para personas jurídicas";

            DPersona obj = new DPersona();
            obj.TipoPersona = 'J';
            obj.CIF = cif;
            obj.RazonSocial = razonSocial;
            obj.Nombre = razonSocial; // El nombre se usa como razón social
            obj.Email = email;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Observaciones = observaciones;
            obj.Estado = true;

            return obj.InsertarPersonaJuridica(obj);
        }

        public static string Editar(int idPersona, string email = null, string direccion = null, 
            string telefono = null, string telefonoAlternativo = null, string observaciones = null)
        {
            if (idPersona <= 0)
                return "ID de persona no válido";

            DPersona obj = new DPersona();
            obj.IdPersona = idPersona;
            obj.Email = email;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.TelefonoAlternativo = telefonoAlternativo;
            obj.Observaciones = observaciones;

            return obj.Editar(obj);
        }

        public static string CambiarTipoPersona(int idPersona, char nuevoTipo, string dni = null, 
            string nombre = null, string apellidos = null, char genero = 'F', string cif = null, 
            string razonSocial = null, string email = null, string direccion = null, 
            string telefono = null, string telefonoAlternativo = null, string observaciones = null)
        {
            if (idPersona <= 0)
                return "ID de persona no válido";

            if (nuevoTipo == 'F')
            {
                if (string.IsNullOrWhiteSpace(dni) || string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellidos))
                    return "DNI, nombre y apellidos son obligatorios para personas físicas";

                if (!ValidarDNI(dni))
                    return "El formato del DNI no es válido";
            }
            else if (nuevoTipo == 'J')
            {
                if (string.IsNullOrWhiteSpace(cif) || string.IsNullOrWhiteSpace(razonSocial))
                    return "CIF y razón social son obligatorios para personas jurídicas";

                if (!ValidarCIF(cif))
                    return "El formato del CIF no es válido";
            }
            else
            {
                return "Tipo de persona no válido";
            }

            DPersona obj = new DPersona();
            obj.IdPersona = idPersona;
            obj.TipoPersona = nuevoTipo;
            obj.DNI = dni;
            obj.CIF = cif;
            obj.Nombre = nombre;
            obj.Apellidos = apellidos;
            obj.RazonSocial = razonSocial;
            obj.Genero = genero;
            obj.Email = email;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.TelefonoAlternativo = telefonoAlternativo;
            obj.Observaciones = observaciones;

            return obj.CambiarTipoPersona(obj);
        }

        public static string Eliminar(int idPersona)
        {
            if (idPersona <= 0)
                return "ID de persona no válido";

            DPersona obj = new DPersona();
            obj.IdPersona = idPersona;
            return obj.Eliminar(obj);
        }

        public static DataTable Mostrar()
        {
            return new DPersona().Mostrar();
        }

        public static DataTable BuscarTexto(string textoBuscar)
        {
            DPersona obj = new DPersona();
            obj.TextoBuscar = textoBuscar;
            return obj.BuscarTexto(obj);
        }

        public static DataTable BuscarPersonasFisicas()
        {
            return new DPersona().BuscarPorTipo('F');
        }

        public static DataTable BuscarPersonasJuridicas()
        {
            return new DPersona().BuscarPorTipo('J');
        }

        public static bool ValidarDNI(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni) || dni.Length < 8)
                return false;

            // Verificar que tenga al menos números y letras
            bool tieneNumeros = dni.Any(char.IsDigit);
            bool tieneLetras = dni.Any(char.IsLetter);
            
            if (!tieneNumeros || !tieneLetras)
                return false;

            // Si tiene exactamente 9 caracteres, validar formato español tradicional
            if (dni.Length == 9)
            {
                string letras = "TRWAGMYFPDXBNJZSQVHLCKE";
                string numeros = dni.Substring(0, 8);
                char letra = dni[8];

                if (int.TryParse(numeros, out int numero))
                {
                    return letras[numero % 23] == char.ToUpper(letra);
                }
            }

            // Para otros formatos, validar que tenga caracteres alfanuméricos válidos
            return dni.All(c => char.IsLetterOrDigit(c));
        }

        public static bool ValidarCIF(string cif)
        {
            if (string.IsNullOrWhiteSpace(cif) || cif.Length != 9)
                return false;

            char primeraLetra = char.ToUpper(cif[0]);
            string letrasValidas = "ABCDEFGHJNPQRSUVW";

            return letrasValidas.Contains(primeraLetra.ToString());
        }
    }
}