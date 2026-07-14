using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Ncategories
    {
        //Metodo Insertar que llama al metodo Insertar de la clase Dcategories de la capa datos
        public static string Insertar(string category_name)
        {
            Dcategories Obj = new Dcategories();
            Obj.Category_name = category_name;
            return Obj.Insertar(Obj);
        }
        public static string Editar(int category_id, string category_name)
        {
            Dcategories Obj = new Dcategories();
            Obj.Category_id = category_id;
            Obj.Category_name = category_name;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int category_id)
        {
            Dcategories Obj = new Dcategories();
            Obj.Category_id = category_id;
            return Obj.Eliminar(Obj);
        }
        public static DataTable Mostrar()
        {
            return new Dcategories().Mostrar();
        }
        public static DataTable BuscarNombre(string textobuscar)
        {
            Dcategories Obj = new Dcategories();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
