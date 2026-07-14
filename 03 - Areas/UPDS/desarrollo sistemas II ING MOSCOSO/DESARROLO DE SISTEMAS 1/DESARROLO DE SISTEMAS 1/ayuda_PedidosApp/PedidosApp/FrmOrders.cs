using CapaNegocio;
using PedidosApp.Reportes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PedidosApp
{
    public partial class FrmOrders : Form
    {
        private bool EsNuevo = false;
        public string Usuario_id = "1"; // Usuario por defecto, se actualiza desde FrmPrincipal
        public string Nombre = string.Empty;
        private DataTable dtDetalle = null;
        private decimal totalPagado = 0;
        private static FrmOrders _instancia;
        public static FrmOrders GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FrmOrders();
            }
            return _instancia;
        }
        public void setCliente(string idcliente, string nombre)
        {
            txtCliente_id.Text = idcliente;
            txtCliente.Text = nombre;
        }
        public void setProducto(string idproducto, string nombre, string precio)
        {
            txtProducto_id.Text = idproducto;
            txtProducto.Text = nombre;
            txtPrecio.Text = precio;

            // Opcional: establecer valores por defecto para facilitar al usuario
            txtCantidad.Text = "1";
            txtDescuento.Text = "0";

            // Enfocar el campo cantidad para que el usuario pueda escribir directamente
            txtCantidad.Focus();
            txtCantidad.SelectAll();
        }
        public FrmOrders()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtCliente, "Seleccione un Cliente");
            txtCliente_id.Visible = false;
            txtProducto_id.Visible = false;
            txtCliente.ReadOnly = true;
            txtProducto.ReadOnly = true;
        }
        //Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        //Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        private void Limpiar()
        {
            txtIdPedido.Text = string.Empty;
            txtCliente_id.Text = string.Empty;
            txtCliente.Text = string.Empty;
            dtpFechaPedido.Value = DateTime.Now;
            lblUsuario.Text = string.Empty;
            nUpDtotal.Value = 0;
        }
        private void LimpiarDetalle()
        {
            txtProducto_id.Text = string.Empty;
            txtProducto.Text = string.Empty;
            txtCantidad.Text = "1";
            txtPrecio.Text = "0";
            txtDescuento.Text = "0";
        }
        private void Habilitar(bool valor)
        {
            txtIdPedido.ReadOnly = !valor;
            txtCantidad.ReadOnly = !valor;
            txtPrecio.ReadOnly = !valor;
            txtDescuento.ReadOnly = !valor;
            btnBuscarCliente.Enabled = valor;
            btnBuscarProducto.Enabled = valor;
            btnAgregar.Enabled = valor;
            btnQuitar.Enabled = valor;
        }
        //Habilitar los botones
        private void Botones()
        {
            if (EsNuevo)
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }
        //Metodo para ocultar columnas
        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                dataListado.Columns[0].Visible = false;
                dataListado.Columns[1].Visible = false;
                dataListado.Columns[4].Visible = false;
            }
        }
        //Metodo Mostrar o Poblar de datos
        private void Mostrar()
        {
            dataListado.DataSource = Norders.Mostrar();
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
            tabControl1.SelectedIndex = 0;
        }
        private void BuscarFechas()
        {
            dataListado.DataSource = Norders.BuscarFecha(dtFecha1.Value.ToString("dd/MM/yyyy"),
                dtFecha2.Value.ToString("dd/MM/yyyy"));
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void MostrarDetalle()
        {
            dataListadoDetalle.DataSource = Norders.MostrarDetalle(txtIdPedido.Text);
        }
        private void crearTabla()
        {
            dtDetalle = new DataTable("Detalle");
            dtDetalle.Columns.Add("product_id", System.Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("producto", System.Type.GetType("System.String"));
            dtDetalle.Columns.Add("quantity", System.Type.GetType("System.Int32"));
            dtDetalle.Columns.Add("price", System.Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("discount", System.Type.GetType("System.Decimal"));
            dtDetalle.Columns.Add("subtotal", System.Type.GetType("System.Decimal"));
            //Relacionar dataListadoDetalle con el DataTable
            dataListadoDetalle.DataSource = dtDetalle;
        }
        private void FrmOrders_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar(false);
            Botones();
            crearTabla();
            
            // Mostrar información del usuario actual
            if (!string.IsNullOrEmpty(Nombre))
            {
                lblUsuario.Text = $"Usuario: {Nombre} (ID: {Usuario_id})";
            }
        }
        private void FrmOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }
        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            FrmVistaCliente_Orden vista = new FrmVistaCliente_Orden();
            vista.ShowDialog();
        }
        private void btnBuscarProducto_Click(object sender, EventArgs e)
        {
            FrmVistaProducto_Orden vista = new FrmVistaProducto_Orden();
            vista.ShowDialog();
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarFechas();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente Desea Eliminar los Registros", "Pedidos App",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo = 0;
                    string rpta = string.Empty;
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            rpta = Norders.Eliminar(codigo);
                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Se eliminó el registro seleccionado");
                            }
                            else { MensajeError(rpta); }
                        }
                    }
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            txtIdPedido.Text = Convert.ToString(dataListado.CurrentRow.Cells["order_id"].Value);
            txtCliente_id.Text = Convert.ToString(dataListado.CurrentRow.Cells["customer_id"].Value);
            txtCliente.Text = Convert.ToString(dataListado.CurrentRow.Cells["Cliente"].Value);
            lblUsuario.Text = Convert.ToString(dataListado.CurrentRow.Cells["usuario"].Value);
            nUpDtotal.Value = Convert.ToDecimal(dataListado.CurrentRow.Cells["Total"].Value);
            MostrarDetalle();
            tabControl1.SelectedIndex = 1;
        }
        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }
        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
            Botones();
            Limpiar();
            LimpiarDetalle();
            Habilitar(true);
            txtCliente.Focus();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (txtCliente_id.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán marcados");
                    errorIcono.SetError(txtCliente, "Seleccione un cliente");
                }
                else
                {
                    if (EsNuevo)
                    {
                        // Debug temporal
                        MessageBox.Show($"Iniciando inserción...\nCliente: {txtCliente_id.Text}\nProductos: {dtDetalle.Rows.Count}");

                        var startTime = DateTime.Now;
                        rpta = Norders.Insertar(Convert.ToInt32(txtCliente_id.Text),
                                    Convert.ToInt32(Usuario_id), dtDetalle);
                        var endTime = DateTime.Now;

                        MessageBox.Show($"Inserción completada en: {(endTime - startTime).TotalSeconds} segundos\nResultado: {rpta}");
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (EsNuevo) { MensajeOK("Inserción satisfactoria del registro"); }
                    }
                    else { MensajeError(rpta); }
                    EsNuevo = false;
                    Botones();
                    Limpiar();
                    LimpiarDetalle();
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completo: " + ex.Message + "\n\nStack Trace: " + ex.StackTrace);
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EsNuevo = false;
            Botones();
            Limpiar();
            LimpiarDetalle();
            Habilitar(false);
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProducto_id.Text == string.Empty || txtCantidad.Text == string.Empty ||
                    txtPrecio.Text == string.Empty || txtDescuento.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán marcados");
                    errorIcono.SetError(txtProducto, "Seleccione un producto");
                    errorIcono.SetError(txtCantidad, "Ingrese una cantidad");
                    errorIcono.SetError(txtPrecio, "Ingrese un valor");
                    errorIcono.SetError(txtDescuento, "Ingrese un descuento");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["product_id"]) == Convert.ToInt32(txtProducto_id.Text))
                        {
                            registrar = false;
                            MensajeError("No puede agregar productos repetidos al detalle");
                        }
                    }
                    if (registrar)
                    {
                        decimal subTotal = Convert.ToDecimal(txtCantidad.Text) * Convert.ToDecimal(txtPrecio.Text) -
                            Convert.ToDecimal(txtDescuento.Text);
                        totalPagado += subTotal;
                        nUpDtotal.Value = totalPagado;
                        //Agregar el detalle al datalistadoDetalle
                        DataRow row = dtDetalle.NewRow();
                        row["product_id"] = Convert.ToInt32(txtProducto_id.Text);
                        row["producto"] = txtProducto.Text;
                        row["quantity"] = txtCantidad.Text;
                        row["price"] = Convert.ToDecimal(txtPrecio.Text);
                        row["discount"] = Convert.ToDecimal(txtDescuento.Text);
                        row["subtotal"] = subTotal;
                        dtDetalle.Rows.Add(row);
                        LimpiarDetalle();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = dataListadoDetalle.CurrentCell.RowIndex;
                DataRow row = dtDetalle.Rows[indiceFila];
                //Restar el totalPagado
                totalPagado = totalPagado - Convert.ToDecimal(row["subtotal"].ToString());
                nUpDtotal.Value = totalPagado;
                //Removemos la fila
                dtDetalle.Rows.Remove(row);
            }
            catch (Exception)
            {
                MensajeError("El Grid esta vacio");
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dataListado.CurrentRow == null)
                {
                    MessageBox.Show("Por favor seleccione una orden para imprimir la factura.",
                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (this.dataListado.CurrentRow.Cells["order_id"].Value == null)
                {
                    MessageBox.Show("No se puede obtener el ID de la orden seleccionada.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                FrmReporteFactura frm = new FrmReporteFactura();
                frm.Order_id = Convert.ToInt32(this.dataListado.CurrentRow.Cells["order_id"].Value);
                frm.ShowDialog();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error al convertir el ID de la orden: " + ex.Message,
                    "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el reporte de factura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
