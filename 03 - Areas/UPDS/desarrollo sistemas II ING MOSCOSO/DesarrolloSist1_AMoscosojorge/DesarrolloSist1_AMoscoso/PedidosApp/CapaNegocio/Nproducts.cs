using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class Nproducts
    {
        //Metodo Insertar que llama al metodo Insertar de la clase DArticulo de la capa datos
        public static string Insertar(string product_name, int model_year, decimal price, 
            byte[] imagen, int category_id)
        {
            Dproducts Obj = new Dproducts();
            Obj.Product_name = product_name;
            Obj.Model_year = model_year;
            Obj.Price = price;
            Obj.Imagen = imagen;
            Obj.Category_id = category_id;
            return Obj.Insertar(Obj);
        }
        public static string Editar(int product_id, string product_name, int model_year, 
            decimal price, byte[] imagen, int category_id)
        {
            Dproducts Obj = new Dproducts();
            Obj.Product_id = product_id;
            Obj.Product_name = product_name;
            Obj.Model_year = model_year;
            Obj.Price = price;
            Obj.Imagen = imagen;
            Obj.Category_id = category_id;
            return Obj.Editar(Obj);
        }
        public static string Eliminar(int product_id)
        {
            Dproducts Obj = new Dproducts();
            Obj.Product_id = product_id;
            return Obj.Eliminar(Obj);
        }
        public static DataTable Mostrar()
        {
            return new Dproducts().Mostrar();
        }
        public static DataTable BuscarNombre(string textobuscar)
        {
            Dproducts Obj = new Dproducts();
            Obj.TextoBuscar = textobuscar;
            return Obj.BuscarNombre(Obj);
        }
    }
}
