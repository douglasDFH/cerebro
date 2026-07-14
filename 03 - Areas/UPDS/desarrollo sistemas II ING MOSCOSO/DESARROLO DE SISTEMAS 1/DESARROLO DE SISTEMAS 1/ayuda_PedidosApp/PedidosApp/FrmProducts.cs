using CapaNegocio;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Image = System.Drawing.Image;

namespace PedidosApp
{
    public partial class FrmProducts : Form
    {
        private bool EsNuevo = false;
        private bool EsEdita = false;
        private static FrmProducts _instancia;

        public static FrmProducts GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new FrmProducts();
            }
            return _instancia;
        }

        public void setCategoria(string categoria_id, string nombre)
        {
            txtIdCategoria.Text = categoria_id;
            txtCategoria.Text = nombre;
        }

        public FrmProducts()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtNombre, "Ingrese el nombre del producto");
            ttMensaje.SetToolTip(pxImagen, "Agregue imagen del producto");
            ttMensaje.SetToolTip(txtCategoria, "Seleccione la categoria del producto");
            txtIdproducto.Enabled = false;
            txtIdCategoria.Visible = false;

            // Suscribir evento CellFormatting para la columna imagen
            dataListado.CellFormatting += dataListado_CellFormatting;
        }

        // Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Mostrar Mensaje de Error
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Pedidos App", MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        private void Limpiar()
        {
            txtIdproducto.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtModelo_año.Text = string.Empty;
            txtPrecio.Text = string.Empty;
            txtCategoria.Text = string.Empty;
            pxImagen.Image = global::PedidosApp.Properties.Resources.imagen_vacia;
        }
        //habilitar botones
        private void Habilitar(bool valor)
        {
            txtIdproducto.ReadOnly = !valor;
            txtNombre.ReadOnly = !valor;
            txtModelo_año.ReadOnly = !valor;
            txtPrecio.ReadOnly = !valor;
            txtCategoria.ReadOnly = !valor;
            btnBuscarCategoria.Enabled = valor;
            btnCargar.Enabled = valor;
            btnLimpiar.Enabled = valor;
        }

        private void Botones()
        {
            if (EsNuevo || EsEdita)
            {
                Habilitar(true);
                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnEditar.Enabled = false;
                btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnEditar.Enabled = true;
                btnCancelar.Enabled = false;
            }
        }
        //habilitar columnas
        private void OcultarColumnas()
        {
            if (dataListado.RowCount > 0)
            {
                if (dataListado.Columns.Count > 0)
                    dataListado.Columns[0].Visible = false; // Columna checkbox eliminar

                if (dataListado.Columns.Contains("category_id"))
                    dataListado.Columns["category_id"].Visible = false;

                if (dataListado.Columns.Contains("category"))
                    dataListado.Columns["category"].Visible = false;
            }
        }

        private void Mostrar()
        {
            dataListado.DataSource = Nproducts.Mostrar();

            // Convertir la columna "imagen" a DataGridViewImageColumn para mostrar imágenes
            if (dataListado.Columns.Contains("imagen"))
            {
                DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dataListado.Columns["imagen"];
                imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + dataListado.Rows.Count.ToString();
            tabControl1.SelectedIndex = 0;
        }

        private void BuscarProducto()
        {
            dataListado.DataSource = Nproducts.BuscarNombre(txtBuscar.Text);
            OcultarColumnas();
            lblTotal.Text = "Registros encontrados: " + dataListado.Rows.Count.ToString();
        }

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            Mostrar();
            Habilitar(false);
            Botones();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                pxImagen.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            pxImagen.Image = global::PedidosApp.Properties.Resources.imagen_vacia;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            BuscarProducto();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            EsNuevo = true;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(true);
            txtNombre.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (txtNombre.Text == string.Empty || txtModelo_año.Text == string.Empty ||
                    txtPrecio.Text == string.Empty || txtCategoria.Text == string.Empty)
                {
                    MensajeError("Faltan ingresar algunos datos, serán marcados");
                    errorIcono.SetError(txtNombre, "Ingrese un producto");
                    errorIcono.SetError(txtModelo_año, "Ingrese un año");
                    errorIcono.SetError(txtPrecio, "Ingrese un valor");
                    errorIcono.SetError(txtCategoria, "Seleccione una categoria");
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    pxImagen.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] imagen = ms.ToArray();

                    if (EsNuevo)
                    {
                        rpta = Nproducts.Insertar(txtNombre.Text.Trim().ToUpper(),
                                    Convert.ToInt32(txtModelo_año.Text.Trim()),
                                    Convert.ToDecimal(txtPrecio.Text.Trim()), imagen,
                                    Convert.ToInt32(txtIdCategoria.Text));
                    }
                    else
                    {
                        rpta = Nproducts.Editar(Convert.ToInt32(txtIdproducto.Text),
                                    txtNombre.Text.Trim().ToUpper(),
                                    Convert.ToInt32(txtModelo_año.Text.Trim()),
                                    Convert.ToDecimal(txtPrecio.Text.Trim()), imagen,
                                    Convert.ToInt32(txtIdCategoria.Text));
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (EsNuevo) { MensajeOK("Inserción satisfactoria del registro"); }
                        else { MensajeOK("Actualización satisfactoria del registro"); }
                    }
                    else { MensajeError(rpta); }
                    EsNuevo = false;
                    EsEdita = false;
                    Botones();
                    Limpiar();
                    Mostrar();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdproducto.Text.Equals(""))
            {
                EsEdita = true;
                Botones();
                Habilitar(true);
            }
            else
            {
                MensajeError("Debe seleccionar con doble clic de la lista el registro");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            EsNuevo = false;
            EsEdita = false;
            Botones();
            Limpiar();
            Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell chkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            if (dataListado.CurrentRow != null)
            {
                txtIdproducto.Text = Convert.ToString(dataListado.CurrentRow.Cells["product_id"].Value);
                txtNombre.Text = Convert.ToString(dataListado.CurrentRow.Cells["product_name"].Value);
                txtModelo_año.Text = Convert.ToString(dataListado.CurrentRow.Cells["model_year"].Value);
                txtPrecio.Text = Convert.ToString(dataListado.CurrentRow.Cells["price"].Value);

                // ✅ Validar que la columna exista
                if (dataListado.Columns.Contains("category"))
                {
                    txtCategoria.Text = Convert.ToString(dataListado.CurrentRow.Cells["category"].Value);
                }
                else
                {
                    txtCategoria.Text = string.Empty; // o mostrar mensaje si es crítico
                }

                txtIdCategoria.Text = Convert.ToString(dataListado.CurrentRow.Cells["category_id"].Value);

                // Mostrar imagen
                if (dataListado.CurrentRow.Cells["imagen"].Value != DBNull.Value && dataListado.CurrentRow.Cells["imagen"].Value != null)
                {
                    byte[] imagenBuffer = (byte[])dataListado.CurrentRow.Cells["imagen"].Value;
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer))
                    {
                        pxImagen.Image = Image.FromStream(ms);
                    }
                    pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else
                {
                    pxImagen.Image = global::PedidosApp.Properties.Resources.imagen_vacia;
                }

                tabControl1.SelectedIndex = 1;
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            dataListado.Columns[0].Visible = chkEliminar.Checked;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("¿Está seguro de eliminar los registros?", "Pedidos App",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string rpta = string.Empty;
                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            int codigo = Convert.ToInt32(row.Cells["product_id"].Value);
                            rpta = Nproducts.Eliminar(codigo);
                            if (rpta.Equals("OK"))
                            {
                                MensajeOK("Se eliminó el registro seleccionado");
                            }
                            else
                            {
                                MensajeError(rpta);
                            }
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

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            FrmVistaCategoria_Producto form = new FrmVistaCategoria_Producto();
            form.ShowDialog();
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            // Implementa impresión si es necesario
        }

        private void FrmProducts_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        // Evento para mostrar imagen en el DataGridView
        private void dataListado_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataListado.Columns[e.ColumnIndex].Name == "imagen")
            {
                if (e.Value != null && e.Value != DBNull.Value)
                {
                    byte[] imgBytes = (byte[])e.Value;
                    using (var ms = new System.IO.MemoryStream(imgBytes))
                    {
                        e.Value = Image.FromStream(ms);
                    }
                }
            }
        }
    }
}
