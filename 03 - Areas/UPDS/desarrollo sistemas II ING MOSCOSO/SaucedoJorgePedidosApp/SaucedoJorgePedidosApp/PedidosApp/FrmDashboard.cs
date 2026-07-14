using CapaDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PedidosApp
{
    public partial class FrmDashboard : Form
    {
        private SqlConnection connection;
        private string usuario;

        public FrmDashboard()
        {
            InitializeComponent();
            connection = new SqlConnection(DbConnection.cn); // Cambia CadenaConexion por cn
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
        }

        private void FrmDashboard_Load(object sender, EventArgs e)
        {
            lblUsuario.Text = $"Bienvenido: {usuario}";
            lblFecha.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy");

            CargarEstadisticas();
            CargarGraficoVentas();
            CargarGraficoProductos();
            CargarUltimosPedidos();
        }

        private void CargarEstadisticas()
        {
            try
            {
                connection.Open();

                // Total de pedidos
                string query = "SELECT COUNT(*) FROM orders";
                SqlCommand cmd = new SqlCommand(query, connection);
                lblTotalPedidos.Text = cmd.ExecuteScalar().ToString();

                // Total de clientes
                query = "SELECT COUNT(*) FROM customers";
                cmd = new SqlCommand(query, connection);
                lblTotalClientes.Text = cmd.ExecuteScalar().ToString();

                // Total de productos
                query = "SELECT COUNT(*) FROM products";
                cmd = new SqlCommand(query, connection);
                lblTotalProductos.Text = cmd.ExecuteScalar().ToString();

                // Ventas totales
                query = "SELECT SUM(quantity * price) FROM order_items";
                cmd = new SqlCommand(query, connection);
                object result = cmd.ExecuteScalar();
                decimal ventasTotales = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                lblVentasTotales.Text = ventasTotales.ToString("C");

                // Productos con stock bajo
                query = "SELECT COUNT(*) FROM stocks WHERE quantity < 10";
                cmd = new SqlCommand(query, connection);
                lblStockBajo.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar estadísticas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void CargarGraficoVentas()
        {
            try
            {
                connection.Open();
                string query = @"SELECT DATENAME(MONTH, order_date) + ' ' + DATENAME(YEAR, order_date) as Mes, 
                                SUM(oi.quantity * oi.price) as Total
                                FROM orders o
                                JOIN order_items oi ON o.order_id = oi.order_id
                                WHERE o.order_date >= DATEADD(MONTH, -6, GETDATE())
                                GROUP BY DATENAME(MONTH, order_date) + ' ' + DATENAME(YEAR, order_date)
                                ORDER BY MIN(o.order_date)";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                chartVentas.Series.Clear();
                chartVentas.Titles.Clear();

                chartVentas.Titles.Add("Ventas últimos 6 meses");
                Series series = new Series("Ventas");
                series.ChartType = SeriesChartType.Column;
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "C";

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["Mes"].ToString(), Convert.ToDecimal(row["Total"]));
                }

                chartVentas.Series.Add(series);
                chartVentas.ChartAreas[0].AxisX.LabelStyle.Angle = -45;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráfico de ventas: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void CargarGraficoProductos()
        {
            try
            {
                connection.Open();
                string query = @"SELECT TOP 5 p.product_name, SUM(oi.quantity) as TotalVendido
                                FROM order_items oi
                                JOIN products p ON oi.product_id = p.product_id
                                GROUP BY p.product_name
                                ORDER BY TotalVendido DESC";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                chartProductos.Series.Clear();
                chartProductos.Titles.Clear();

                chartProductos.Titles.Add("Top 5 productos más vendidos");
                Series series = new Series("Productos");
                series.ChartType = SeriesChartType.Pie;
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "#,##0";

                foreach (DataRow row in dt.Rows)
                {
                    series.Points.AddXY(row["product_name"].ToString(), Convert.ToInt32(row["TotalVendido"]));
                }

                chartProductos.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráfico de productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void CargarUltimosPedidos()
        {
            try
            {
                connection.Open();
                string query = @"SELECT TOP 10 o.order_id, c.first_name + ' ' + c.last_name as Cliente, 
                                 o.order_date, SUM(oi.quantity * oi.price) as Total
                                 FROM orders o
                                 JOIN customers c ON o.customer_id = c.customer_id
                                 JOIN order_items oi ON o.order_id = oi.order_id
                                 GROUP BY o.order_id, c.first_name + ' ' + c.last_name, o.order_date
                                 ORDER BY o.order_date DESC";

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                dgvUltimosPedidos.DataSource = dt;
                dgvUltimosPedidos.Columns["order_id"].HeaderText = "N° Pedido";
                dgvUltimosPedidos.Columns["Cliente"].HeaderText = "Cliente";
                dgvUltimosPedidos.Columns["order_date"].HeaderText = "Fecha";
                dgvUltimosPedidos.Columns["Total"].HeaderText = "Total";
                dgvUltimosPedidos.Columns["Total"].DefaultCellStyle.Format = "C";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar últimos pedidos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
