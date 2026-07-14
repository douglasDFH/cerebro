using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocio;

namespace PedidosApp
{
    public partial class FrmVistaProducto_Orden : Form
    {
        public FrmVistaProducto_Orden()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtBuscar, "Ej. Productos que inicia con letra A");
            ttMensaje.SetToolTip(dataListado, "Para seleccionar un Producto con Doble Click");
        }

        private void Mostrar()
        {
            try
            {
                dataListado.DataSource = Nproducts.Mostrar();
                MostrarTodasLasColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al mostrar productos: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuscarNombre()
        {
            try
            {
                dataListado.DataSource = Nproducts.BuscarNombre(txtBuscar.Text.Trim());
                MostrarTodasLasColumnas();
                lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar productos: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MostrarTodasLasColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                foreach (DataGridViewColumn column in dataListado.Columns)
                {
                    column.Visible = true;
                }
                
                ConfigurarColumnas();
            }
        }

        private void ConfigurarColumnas()
        {
            try
            {
                if (dataListado.Columns.Contains("product_id"))
                    dataListado.Columns["product_id"].HeaderText = "ID";
                
                if (dataListado.Columns.Contains("product_name"))
                    dataListado.Columns["product_name"].HeaderText = "Producto";
                
                if (dataListado.Columns.Contains("model_year"))
                    dataListado.Columns["model_year"].HeaderText = "Año Modelo";
                
                if (dataListado.Columns.Contains("price"))
                {
                    dataListado.Columns["price"].HeaderText = "Precio";
                    dataListado.Columns["price"].DefaultCellStyle.Format = "C2";
                    dataListado.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dataListado.Columns.Contains("category_id"))
                    dataListado.Columns["category_id"].HeaderText = "ID Categoría";
                
                if (dataListado.Columns.Contains("brand_id"))
                    dataListado.Columns["brand_id"].HeaderText = "ID Marca";
                
                dataListado.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataListado.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataListado.MultiSelect = false;
                dataListado.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al configurar columnas: " + ex.Message, "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                if (dataListado.Columns.Count > 0) dataListado.Columns[0].Visible = false;
                if (dataListado.Columns.Count > 2) dataListado.Columns[2].Visible = false;
                if (dataListado.Columns.Count > 3) dataListado.Columns[3].Visible = false;
                if (dataListado.Columns.Count > 4) dataListado.Columns[4].Visible = false;
                if (dataListado.Columns.Count > 5) dataListado.Columns[5].Visible = false;
                if (dataListado.Columns.Count > 6) dataListado.Columns[6].Visible = false;
            }
        }

        private void FrmVistaProducto_Orden_Load(object sender, EventArgs e)
        {
            Mostrar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                Mostrar();
            }
            else
            {
                BuscarNombre();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnBuscar_Click(sender, e);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataListado.CurrentRow != null)
                {
                    FrmOrders form = FrmOrders.GetInstancia();
                    string par1, par2, par3;
                    par1 = Convert.ToString(dataListado.CurrentRow.Cells["product_id"].Value);
                    par2 = Convert.ToString(dataListado.CurrentRow.Cells["product_name"].Value);
                    par3 = Convert.ToString(dataListado.CurrentRow.Cells["price"].Value); // Agregar el precio
                    form.setProducto(par1, par2, par3); // Pasar los 3 parámetros
                    Hide();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al seleccionar producto: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            Mostrar();
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            dataListado_DoubleClick(sender, e);
        }

        private void FrmVistaProducto_Orden_FormClosed(object sender, FormClosedEventArgs e)
        {
            dataListado.DataSource = null;
        }

        private void dataListado_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (dataListado.Columns[e.ColumnIndex].Name == "price" && e.Value != null)
                {
                    decimal price;
                    if (decimal.TryParse(e.Value.ToString(), out price))
                    {
                        e.Value = price.ToString("C2");
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error en formato de celda: " + ex.Message);
            }
        }

        private void dataListado_SelectionChanged(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                try
                {
                    string productInfo = $"Producto: {dataListado.CurrentRow.Cells["product_name"].Value} - " +
                                       $"Precio: ${dataListado.CurrentRow.Cells["price"].Value}";
                    this.Text = $"Vista Productos - {productInfo}";
                }
                catch
                {
                    this.Text = "Vista Productos";
                }
            }
        }
    }
}