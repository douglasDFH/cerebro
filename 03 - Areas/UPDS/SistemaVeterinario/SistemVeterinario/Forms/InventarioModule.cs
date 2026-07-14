using CapaNegocio;
using SistemVeterinario.Navigation;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SistemVeterinario.Forms
{
    public partial class InventarioModule : BaseModulos
    {
        public InventarioModule()
        {
            InitializeComponent();
            ConfigurarModulo();
        }

        private void ConfigurarModulo()
        {
            ConfigurarComboTipoMovimiento();
            dtpFechaInicio.Value = DateTime.Now.AddDays(-30);
            dtpFechaFin.Value = DateTime.Now;
        }

        private void ConfigurarComboTipoMovimiento()
        {
            cmbTipoMovimiento.Items.Clear();
            cmbTipoMovimiento.Items.AddRange(new string[] { "ENTRADA", "SALIDA" });
            cmbTipoMovimiento.SelectedIndex = 0;
        }

        protected override void OnLoad()
        {
            CargarInventario();
            CargarUbicaciones();
        }

        private void CargarInventario()
        {
            try
            {
                DataTable datos = NInventario.MostrarInventario();
                CargarDatos(datos);
                VerificarStockBajo();
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar inventario: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void CargarUbicaciones()
        {
            try
            {
                DataTable ubicaciones = NInventario.ObtenerUbicacionesAlmacen();
                cmbUbicacion.DataSource = ubicaciones;
                cmbUbicacion.DisplayMember = "nombre";
                cmbUbicacion.ValueMember = "id";
                cmbUbicacion.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar ubicaciones: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void VerificarStockBajo()
        {
            try
            {
                DataTable stockBajo = NInventario.ObtenerProductosStockBajo();
                if (stockBajo.Rows.Count > 0)
                {
                    string productos = string.Join(", ", stockBajo.AsEnumerable()
                        .Select(r => r["nombre"].ToString()));
                    MostrarMensaje($"¡ALERTA! Los siguientes productos tienen stock bajo: {productos}",
                        "Alerta de Stock", MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al verificar stock: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnBuscar()
        {
            try
            {
                string textoBuscar = txtBuscar.Text.Trim();
                DataTable datos = NInventario.BuscarProductoInventario(textoBuscar);
                CargarDatos(datos);
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al buscar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void OnNuevo()
        {
            base.OnNuevo();
            LimpiarFormulario();
        }

        protected override void OnGuardar()
        {
            try
            {
                if (!ValidarCampos())
                    return;

                string resultado;

                if (ModoEdicion)
                {
                    resultado = NInventario.ActualizarProductoInventario(
                        int.Parse(txtId.Text),
                        txtNombreProducto.Text.Trim(),
                        txtDescripcion.Text.Trim(),
                        decimal.Parse(txtPrecio.Text),
                        int.Parse(txtStockMinimo.Text),
                        int.Parse(txtStockMaximo.Text)
                    );
                }
                else
                {
                    resultado = NInventario.InsertarProductoInventario(
                            txtNombreProducto.Text.Trim(),
                        txtDescripcion.Text.Trim(),
                        decimal.Parse(txtPrecio.Text),
                        int.Parse(txtStockActual.Text),
                        int.Parse(txtStockMinimo.Text),
                        int.Parse(txtStockMaximo.Text)
                    );
                }

                if (resultado == "OK")
                {
                    MostrarMensaje("Producto guardado exitosamente");
                    LimpiarFormulario();
                    CargarInventario();
                    tabControlPrincipal.SelectedTab = tabInicio;
                }
                else
                {
                    MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al guardar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void EliminarRegistro(int id)
        {
            try
            {
                string resultado = NInventario.EliminarProducto(id);
                if (resultado == "OK")
                {
                    MostrarMensaje("Producto eliminado exitosamente");
                }
                else
                {
                    MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al eliminar: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void CargarDatosParaEdicion(int id)
        {
            try
            {
                DataTable datos = NInventario.MostrarInventario();
                DataRow[] filas = datos.Select($"id = {id}");

                if (filas.Length > 0)
                {
                    DataRow fila = filas[0];
                    txtNombreProducto.Text = fila["nombre"].ToString();
                    txtDescripcion.Text = fila["descripcion"].ToString();
                    txtPrecio.Text = fila["precio"].ToString();
                    txtStockActual.Text = fila["stock_actual"].ToString();
                    txtStockMinimo.Text = fila["stock_minimo"].ToString();
                    txtStockMaximo.Text = fila["stock_maximo"].ToString();
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar datos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        protected override void LimpiarFormulario()
        {
            txtId.Clear();
            txtNombreProducto.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            txtStockActual.Clear();
            txtStockMinimo.Clear();
            txtStockMaximo.Clear();
            txtCantidadMovimiento.Clear();
            txtMotivoMovimiento.Clear();
            cmbTipoMovimiento.SelectedIndex = 0;
            cmbUbicacion.SelectedIndex = 0;
        }

        protected override void OnCambioModo(bool esEdicion)
        {
            if (txtStockActual != null)
            {
                txtStockActual.Enabled = !esEdicion;
                txtStockActual.Visible = !esEdicion;
            }
            if (lblStockActual != null)
            {
                lblStockActual.Visible = !esEdicion;
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
            {
                MostrarMensaje("El nombre del producto es obligatorio", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MostrarMensaje("Ingrese un precio válido", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtStockMinimo.Text, out int stockMinimo) || stockMinimo < 0)
            {
                MostrarMensaje("Ingrese un stock mínimo válido", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtStockMaximo.Text, out int stockMaximo) || stockMaximo < stockMinimo)
            {
                MostrarMensaje("El stock máximo debe ser mayor al mínimo", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (!ModoEdicion)
            {
                if (!int.TryParse(txtStockActual.Text, out int stockActual) || stockActual < 0)
                {
                    MostrarMensaje("Ingrese un stock actual válido", "Validación", MessageBoxIcon.Warning);
                    return false;
                }
            }

            return true;
        }

        private void BtnRegistrarMovimiento_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarMovimiento())
                    return;

                int idProducto = int.Parse(txtId.Text);
                string tipoMovimiento = cmbTipoMovimiento.Text;
                int cantidad = int.Parse(txtCantidadMovimiento.Text);
                string motivo = txtMotivoMovimiento.Text.Trim();
                int idUbicacion = (int)cmbUbicacion.SelectedValue;

                string resultado = NInventario.RegistrarMovimientoInventario(idProducto, tipoMovimiento, cantidad, motivo, idUbicacion);

                if (resultado == "OK")
                {
                    MostrarMensaje("Movimiento registrado exitosamente");
                    txtCantidadMovimiento.Clear();
                    txtMotivoMovimiento.Clear();
                    CargarInventario();
                }
                else
                {
                    MostrarMensaje(resultado, "Error", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al registrar movimiento: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private bool ValidarMovimiento()
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || txtId.Text == "0")
            {
                MostrarMensaje("Seleccione un producto para registrar el movimiento", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(txtCantidadMovimiento.Text, out int cantidad) || cantidad <= 0)
            {
                MostrarMensaje("Ingrese una cantidad válida", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtMotivoMovimiento.Text))
            {
                MostrarMensaje("Ingrese el motivo del movimiento", "Validación", MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void BtnVerMovimientos_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaInicio = dtpFechaInicio.Value.Date;
                DateTime fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                DataTable movimientos = NInventario.ObtenerMovimientosInventario(fechaInicio, fechaFin);
                dgvMovimientos.DataSource = movimientos;
                tabControlPrincipal.SelectedTab = tabMovimientos;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar movimientos: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }

        private void BtnStockBajo_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable stockBajo = NInventario.ObtenerProductosStockBajo();
                dgvStockBajo.DataSource = stockBajo;
                tabControlPrincipal.SelectedTab = tabAlertas;
            }
            catch (Exception ex)
            {
                MostrarMensaje($"Error al cargar productos con stock bajo: {ex.Message}", "Error", MessageBoxIcon.Error);
            }
        }
    }
}