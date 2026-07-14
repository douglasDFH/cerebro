using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class Dorder_items : DbConnection
    {
        //Variables 
        private int _order_item_id;
        private int _order_id;
        private int _product_id;
        private int _quantity;
        private decimal _price;
        private decimal _discount;
        //Propiedades
        public int Order_item_id { get => _order_item_id; set => _order_item_id = value; }
        public int Order_id { get => _order_id; set => _order_id = value; }
        public int Product_id { get => _product_id; set => _product_id = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public decimal Price { get => _price; set => _price = value; }
        public decimal Discount { get => _discount; set => _discount = value; }
        //Constructores
        public Dorder_items() { }
        public Dorder_items(int order_item_id, int order_id, int product_id, int quantity, decimal price, decimal discount)
        {
            Order_item_id = order_item_id;
            Order_id = order_id;
            Product_id = product_id;
            Quantity = quantity;
            Price = price;
            Discount = discount;
        }
        //Metodo Insertar
        //Metodo Insertar CORREGIDO
        public string Insertar(Dorder_items Order_items, ref SqlConnection sqlCon,
                                ref SqlTransaction sqlTra)
        {
            string rpta = string.Empty;
            try
            {
                SqlCommand command = new SqlCommand();

                // ⭐ USAR LA CONEXIÓN Y TRANSACCIÓN QUE TE PASAN
                command.Connection = sqlCon;  // No crear nueva conexión
                command.Transaction = sqlTra;  // Usar la transacción existente
                command.CommandTimeout = 300;  // ⭐ AGREGAR TIMEOUT
                command.CommandText = "spinsertar_order_items";
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter ParOrder_item_id = new SqlParameter();
                ParOrder_item_id.ParameterName = "@order_item_id";
                ParOrder_item_id.SqlDbType = SqlDbType.Int;
                ParOrder_item_id.Direction = ParameterDirection.Output;
                command.Parameters.Add(ParOrder_item_id);

                SqlParameter ParOrder_id = new SqlParameter();
                ParOrder_id.ParameterName = "@order_id";
                ParOrder_id.SqlDbType = SqlDbType.Int;
                ParOrder_id.Value = Order_items.Order_id;
                command.Parameters.Add(ParOrder_id);

                SqlParameter ParProduct_id = new SqlParameter();
                ParProduct_id.ParameterName = "@product_id";
                ParProduct_id.SqlDbType = SqlDbType.Int;
                ParProduct_id.Value = Order_items.Product_id;
                command.Parameters.Add(ParProduct_id);

                SqlParameter ParQuantity = new SqlParameter();
                ParQuantity.ParameterName = "@quantity";
                ParQuantity.SqlDbType = SqlDbType.Int;
                ParQuantity.Value = Order_items.Quantity;
                command.Parameters.Add(ParQuantity);

                SqlParameter ParPrice = new SqlParameter();
                ParPrice.ParameterName = "@price";
                ParPrice.SqlDbType = SqlDbType.Decimal;
                ParPrice.Value = Order_items.Price;
                command.Parameters.Add(ParPrice);

                SqlParameter ParDiscount = new SqlParameter();
                ParDiscount.ParameterName = "@discount";
                ParDiscount.SqlDbType = SqlDbType.Decimal;
                ParDiscount.Value = Order_items.Discount;
                command.Parameters.Add(ParDiscount);

                //Ejecutamos el comando
                rpta = command.ExecuteNonQuery() == 1 ? "OK" : "NO SE INGRESO EL REGISTRO";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }

            return rpta;
        }
    }
}
