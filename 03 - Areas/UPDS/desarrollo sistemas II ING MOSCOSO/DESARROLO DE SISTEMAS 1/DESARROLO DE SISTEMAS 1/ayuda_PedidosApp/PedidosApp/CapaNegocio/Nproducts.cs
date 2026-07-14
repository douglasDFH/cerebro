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

        //Metodos para manejo de stock
        public static DataTable VerificarStock(int productId, int cantidad)
        {
            Dproducts Obj = new Dproducts();
            return Obj.VerificarStock(productId, cantidad);
        }

        public static string ActualizarStock(int productId, int cantidad, string operacion)
        {
            Dproducts Obj = new Dproducts();
            return Obj.ActualizarStock(productId, cantidad, operacion);
        }

        public static DataTable ProductosStockBajo()
        {
            return new Dproducts().ProductosStockBajo();
        }

        //Metodo para validar si hay stock suficiente antes de agregar a pedido
        public static bool ValidarStockDisponible(int productId, int cantidadSolicitada)
        {
            try
            {
                DataTable stock = VerificarStock(productId, cantidadSolicitada);
                if (stock != null && stock.Rows.Count > 0)
                {
                    string disponibilidad = stock.Rows[0]["disponibilidad"].ToString();
                    return disponibilidad == "DISPONIBLE";
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
