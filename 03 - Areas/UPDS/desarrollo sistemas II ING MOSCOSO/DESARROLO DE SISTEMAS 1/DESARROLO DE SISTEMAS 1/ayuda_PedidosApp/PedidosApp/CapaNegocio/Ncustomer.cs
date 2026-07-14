using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Ncustomer
    {
        // Método Insertar que llama al método Insertar de la clase Dcustomer de la capa de datos
        public static string Insertar(string first_name, string last_name, string phone,
            string email, string street, string city, string state)
        {
            Dcustomer Obj = new Dcustomer
            {
                First_name = first_name,
                Last_name = last_name,
                Phone = phone,
                Email = email,
                Street = street,
                City = city,
                State = state
            };
            return Obj.Insertar(Obj);
        }

        // Método Editar
        public static string Editar(int customer_id, string first_name, string last_name,
            string phone, string email, string street, string city, string state)
        {
            Dcustomer Obj = new Dcustomer
            {
                Customer_id = customer_id,
                First_name = first_name,
                Last_name = last_name,
                Phone = phone,
                Email = email,
                Street = street,
                City = city,
                State = state
            };
            return Obj.Editar(Obj);
        }

        // Método Eliminar
        public static string Eliminar(int customer_id)
        {
            Dcustomer Obj = new Dcustomer
            {
                Customer_id = customer_id
            };
            return Obj.Eliminar(Obj);
        }

        // Método Mostrar
        public static DataTable Mostrar()
        {
            return new Dcustomer().Mostrar();
        }

        // Método Buscar por nombre
        public static DataTable BuscarNombre(string textobuscar)
        {
            Dcustomer Obj = new Dcustomer
            {
                Textobuscar = textobuscar
            };
            return Obj.BuscarNombre(Obj);
        }
    }
}
