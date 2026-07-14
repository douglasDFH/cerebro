using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CapaNegocio;
using SistemVeterinario.Navigation;

namespace SistemVeterinario.Forms
{
    public partial class ConsultaModule : BaseModulos
    {
        #region Propiedades y Variables
        private NDiagnostico nDiagnostico;
        private DataTable dtDiagnosticos;
        private bool isLoadingData = false;
        private string categoriaSeleccionada = "";
        
        // Enum para los modos específicos del módulo
        public enum ModoOperacion
        {
            Consulta = 1,
            NuevoDiagnostico = 2,
            EditarDiagnostico = 3,
            VerDetalles = 4
        }
        #endregion

        #region Constructor y Inicialización
        public ConsultaModule()
        {
            InitializeComponent();
            nDiagnostico = new NDiagnostico();
            ConfigurarModulo();
            ConfigurarEventos();
            CargarDatosIniciales();
        }

        private void ConfigurarModulo()
        {
            // Configurar modos específicos
            ConfigurarModos();
            
            // Configurar controles específicos
            ConfigurarControles();
            
            // Aplicar tema visual
            AplicarTemaVisual();
            
            // Configurar dgvDatos para que ocupe todo el ancho
            ConfigurarDataGridView();
        }

        private void ConfigurarModos()
        {
            // Limpiar modos existentes y agregar los específicos
            cmbModo.Items.Clear();
            cmbModo.Items.Add("Consulta");
            cmbModo.Items.Add("Nuevo Diagnóstico");
            cmbModo.Items.Add("Editar Diagnóstico");
            cmbModo.Items.Add("Ver Detalles");
            cmbModo.SelectedIndex = 0;
        }

        private void ConfigurarControles()
        {
            // Configurar ComboBox de categorías
            CargarCategorias();
            
            // Configurar NumericUpDown
            nudPrecioBase.Minimum = 0;
            nudPrecioBase.Maximum = 999999;
            nudPrecioBase.DecimalPlaces = 2;
            nudPrecioBase.Increment = 10;
            
            // Configurar TextBox multilínea
            txtDescripcion.AcceptsReturn = true;
            txtDescripcion.AcceptsTab = true;
            txtDescripcion.WordWrap = true;
            
            // Configurar validaciones
            txtNombre.MaxLength = 200;
            txtCodigo.MaxLength = 20;
            txtDescripcion.MaxLength = 1000;
        }

        private void AplicarTemaVisual()
        {
            // Personalizar colores específicos del módulo
            this.BackColor = Color.FromArgb(248, 249, 250);
            grpDatosDiagnostico.BackColor = Color.FromArgb(248, 249, 250);
            tableLayoutDiagnostico.BackColor = Color.White;
            
            // Aplicar efectos hover a botones especiales
            AplicarEfectoHover(btnGenerarCodigo, Color.FromArgb(52, 152, 219), Color.FromArgb(41, 128, 185));
            AplicarEfectoHover(btnNuevaCategoria, Color.FromArgb(46, 204, 113), Color.FromArgb(39, 174, 96));
            AplicarEfectoHover(btnVerPorCategoria, Color.FromArgb(155, 89, 182), Color.FromArgb(142, 68, 173));
        }

        private void AplicarEfectoHover(Button boton, Color colorNormal, Color colorHover)
        {
            boton.MouseEnter += (s, e) => boton.BackColor = colorHover;
            boton.MouseLeave += (s, e) => boton.BackColor = colorNormal;
        }

        private void ConfigurarDataGridView()
        {
            // Ajustar el dgvDatos para que ocupe todo el ancho como en MascotasModule
            dgvDatos.Size = new Size(1121, 400);
            dgvDatos.Location = new Point(10, 150);
            
            // Configurar alineación central para encabezados
            dgvDatos.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Mejorar la apariencia
            dgvDatos.AllowUserToResizeColumns = true;
            dgvDatos.AllowUserToResizeRows = false;
            
            // Configurar el estilo de las celdas para mejor presentación
            dgvDatos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgvDatos.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            
            if (dgvDatos.Columns.Count == 0)
            {
                // Configurar columnas del DataGridView
                dgvDatos.AutoGenerateColumns = false;
                dgvDatos.Columns.Clear();

                // Columna ID (oculta)
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Id",
                    DataPropertyName = "id_diagnostico",
                    Visible = false
                });

                // Configurar distribución proporcional para ocupar exactamente el 100% del ancho
                int totalWidth = 1121;

                // Columna Código
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Codigo",
                    DataPropertyName = "codigo",
                    HeaderText = "🏷️ Código",
                    Width = (int)(totalWidth * 0.14), // 14%
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                // Columna Nombre
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Nombre",
                    DataPropertyName = "nombre",
                    HeaderText = "📝 Nombre del Diagnóstico",
                    Width = (int)(totalWidth * 0.32), // 32%
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                // Columna Categoría
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Categoria",
                    DataPropertyName = "categoria",
                    HeaderText = "📋 Categoría",
                    Width = (int)(totalWidth * 0.16), // 16%
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                // Columna Precio
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "PrecioBase",
                    DataPropertyName = "precio_base",
                    HeaderText = "💰 Precio Base",
                    Width = (int)(totalWidth * 0.14), // 14%
                    ReadOnly = true,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Alignment = DataGridViewContentAlignment.MiddleRight,
                        Format = "C2"
                    },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                // Columna Descripción
                dgvDatos.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Descripcion",
                    DataPropertyName = "descripcion",
                    HeaderText = "📄 Descripción",
                    Width = (int)(totalWidth * 0.16), // 16%
                    ReadOnly = true,
                    DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleLeft },
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });

                // Columna Estado
                dgvDatos.Columns.Add(new DataGridViewCheckBoxColumn
                {
                    Name = "Activo",
                    DataPropertyName = "activo",
                    HeaderText = "✅ Activo",
                    Width = (int)(totalWidth * 0.08), // 8%
                    ReadOnly = true,
                    HeaderCell = { Style = { Alignment = DataGridViewContentAlignment.MiddleCenter } }
                });
            }
        }
        #endregion

        #region Configuración de Eventos
        private void ConfigurarEventos()
        {
            // Eventos específicos del módulo
            btnGenerarCodigo.Click += BtnGenerarCodigo_Click;
            btnNuevaCategoria.Click += BtnNuevaCategoria_Click;
            btnVerPorCategoria.Click += BtnVerPorCategoria_Click;
            cmbCategoriaFiltro.SelectedIndexChanged += CmbCategoriaFiltro_SelectedIndexChanged;
            
            // Eventos de validación
            txtNombre.TextChanged += ValidarCamposRequeridos;
            nudPrecioBase.ValueChanged += ValidarCamposRequeridos;
            
            // Eventos de formato
            txtCodigo.TextChanged += TxtCodigo_TextChanged;
            txtNombre.KeyPress += TxtNombre_KeyPress;
            
            // Sobrescribir eventos del módulo base
            this.Load += ConsultaModule_Load;
        }

        private void ConsultaModule_Load(object sender, EventArgs e)
        {
            CargarDatosGrid();
            EstablecerModo(ModoOperacion.Consulta);
        }
        #endregion

        #region Carga de Datos
        private void CargarDatosGrid()
        {
            try
            {
                isLoadingData = true;
                
                // Cargar diagnósticos según filtros
                if (!string.IsNullOrEmpty(categoriaSeleccionada) && categoriaSeleccionada != "Todas")
                {
                    dtDiagnosticos = nDiagnostico.ListarDiagnosticosPorCategoria(categoriaSeleccionada);
                }
                else
                {
                    dtDiagnosticos = nDiagnostico.ListarDiagnosticos();
                }

                // Configurar y cargar el DataGridView
                ConfigurarDataGridView();
                dgvDatos.DataSource = dtDiagnosticos;
                
                // Actualizar contador
                ActualizarContador();
                
                // Aplicar filtros de búsqueda si existen
                if (!string.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    AplicarFiltroBusqueda();
                }

                isLoadingData = false;
            }
            catch (Exception ex)
            {
                isLoadingData = false;
                MostrarError("Error al cargar los diagnósticos", ex.Message);
            }
        }

        private void CargarCategorias()
        {
            try
            {
                // Cargar categorías en ambos ComboBox
                var categorias = nDiagnostico.ObtenerCategorias();
                
                // ComboBox del formulario
                cmbCategoria.Items.Clear();
                cmbCategoria.Items.Add("Seleccionar categoría...");
                foreach (string categoria in categorias)
                {
                    cmbCategoria.Items.Add(categoria);
                }
                cmbCategoria.SelectedIndex = 0;

                // ComboBox de filtro
                cmbCategoriaFiltro.Items.Clear();
                cmbCategoriaFiltro.Items.Add("Todas");
                foreach (string categoria in categorias)
                {
                    cmbCategoriaFiltro.Items.Add(categoria);
                }
                cmbCategoriaFiltro.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar categorías", ex.Message);
            }
        }

        private void CargarDatosIniciales()
        {
            CargarCategorias();
            CargarDatosGrid();
        }
        #endregion

        #region Métodos de Búsqueda y Filtrado
        private void BuscarDatos()
        {
            if (isLoadingData) return;

            try
            {
                string terminoBusqueda = txtBuscar.Text.Trim();
                
                if (string.IsNullOrEmpty(terminoBusqueda))
                {
                    CargarDatosGrid();
                    return;
                }

                // Realizar búsqueda
                DataTable dtResultados = nDiagnostico.BuscarDiagnosticos(terminoBusqueda);
                
                // Aplicar filtro de categoría si está seleccionado
                if (!string.IsNullOrEmpty(categoriaSeleccionada) && categoriaSeleccionada != "Todas")
                {
                    var filasFiltradas = dtResultados.AsEnumerable()
                        .Where(row => row.Field<string>("categoria") == categoriaSeleccionada);
                    
                    if (filasFiltradas.Any())
                    {
                        dtResultados = filasFiltradas.CopyToDataTable();
                    }
                    else
                    {
                        dtResultados = dtResultados.Clone(); // Tabla vacía con la misma estructura
                    }
                }

                dgvDatos.DataSource = dtResultados;
                ActualizarContador();
                
                // Resaltar términos encontrados
                ResaltarResultadosBusqueda(terminoBusqueda);
            }
            catch (Exception ex)
            {
                MostrarError("Error en la búsqueda", ex.Message);
            }
        }

        private void AplicarFiltroBusqueda()
        {
            if (dtDiagnosticos == null || dtDiagnosticos.Rows.Count == 0) return;

            string termino = txtBuscar.Text.Trim().ToLower();
            var filasFiltradas = dtDiagnosticos.AsEnumerable()
                .Where(row => 
                    row.Field<string>("codigo").ToLower().Contains(termino) ||
                    row.Field<string>("nombre").ToLower().Contains(termino) ||
                    row.Field<string>("categoria").ToLower().Contains(termino) ||
                    (row.Field<string>("descripcion") ?? "").ToLower().Contains(termino)
                );

            if (filasFiltradas.Any())
            {
                dgvDatos.DataSource = filasFiltradas.CopyToDataTable();
            }
            else
            {
                dgvDatos.DataSource = dtDiagnosticos.Clone();
            }
            
            ActualizarContador();
        }

        private void ResaltarResultadosBusqueda(string termino)
        {
            // Implementar resaltado de texto en el DataGridView
            foreach (DataGridViewRow fila in dgvDatos.Rows)
            {
                if (fila.Cells["Nombre"].Value.ToString().ToLower().Contains(termino.ToLower()) ||
                    fila.Cells["Codigo"].Value.ToString().ToLower().Contains(termino.ToLower()))
                {
                    fila.DefaultCellStyle.BackColor = Color.FromArgb(255, 248, 220); // Color amarillo suave
                }
            }
        }

        private void ActualizarContador()
        {
            int total = dgvDatos.Rows.Count;
            lblContador.Text = $"📊 Total: {total} registro{(total != 1 ? "s" : "")}";
            
            // Cambiar color según la cantidad
            if (total == 0)
                lblContador.ForeColor = Color.FromArgb(231, 76, 60); // Rojo
            else if (total < 10)
                lblContador.ForeColor = Color.FromArgb(230, 126, 34); // Naranja
            else
                lblContador.ForeColor = Color.FromArgb(46, 204, 113); // Verde
        }
        #endregion

        #region Operaciones CRUD
        private bool GuardarDatos()
        {
            try
            {
                if (!ValidarDatos()) return false;

                // Crear objeto con los datos del formulario
                var datosDiagnostico = new
                {
                    codigo = txtCodigo.Text.Trim(),
                    nombre = txtNombre.Text.Trim(),
                    categoria = cmbCategoria.Text == "Seleccionar categoría..." ? "" : cmbCategoria.Text,
                    descripcion = txtDescripcion.Text.Trim(),
                    precio_base = nudPrecioBase.Value,
                    requiere_equipamiento = chkRequiereEquipamiento.Checked,
                    activo = chkActivo.Checked
                };

                bool resultado = false;
                string mensaje = "";

                if (EsModoNuevo())
                {
                    resultado = nDiagnostico.InsertarDiagnostico(
                        datosDiagnostico.codigo,
                        datosDiagnostico.nombre,
                        datosDiagnostico.categoria,
                        datosDiagnostico.descripcion,
                        datosDiagnostico.precio_base,
                        datosDiagnostico.requiere_equipamiento,
                        datosDiagnostico.activo
                    );
                    mensaje = "Diagnóstico creado exitosamente";
                }
                else if (EsModoEdicion())
                {
                    int id = Convert.ToInt32(txtId.Text);
                    resultado = nDiagnostico.ActualizarDiagnostico(
                        id,
                        datosDiagnostico.codigo,
                        datosDiagnostico.nombre,
                        datosDiagnostico.categoria,
                        datosDiagnostico.descripcion,
                        datosDiagnostico.precio_base,
                        datosDiagnostico.requiere_equipamiento,
                        datosDiagnostico.activo
                    );
                    mensaje = "Diagnóstico actualizado exitosamente";
                }

                if (resultado)
                {
                    MostrarExito(mensaje);
                    LimpiarFormulario();
                    CargarDatosGrid();
                    EstablecerModo(ModoOperacion.Consulta);
                    return true;
                }
                else
                {
                    MostrarError("Error", "No se pudo guardar el diagnóstico");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al guardar", ex.Message);
                return false;
            }
        }

        private bool EliminarDatos()
        {
            try
            {
                if (dgvDatos.CurrentRow == null)
                {
                    MostrarAdvertencia("Seleccione un diagnóstico para eliminar");
                    return false;
                }

                int id = Convert.ToInt32(dgvDatos.CurrentRow.Cells["Id"].Value);
                string nombre = dgvDatos.CurrentRow.Cells["Nombre"].Value.ToString();

                var confirmacion = MessageBox.Show(
                    $"¿Está seguro de eliminar el diagnóstico '{nombre}'?\n\n" +
                    "Esta acción no se puede deshacer.",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (confirmacion == DialogResult.Yes)
                {
                    bool resultado = nDiagnostico.EliminarDiagnostico(id);
                    
                    if (resultado)
                    {
                        MostrarExito("Diagnóstico eliminado exitosamente");
                        CargarDatosGrid();
                        LimpiarFormulario();
                        EstablecerModo(ModoOperacion.Consulta);
                        return true;
                    }
                    else
                    {
                        MostrarError("Error", "No se pudo eliminar el diagnóstico");
                        return false;
                    }
                }
                
                return false;
            }
            catch (Exception ex)
            {
                MostrarError("Error al eliminar", ex.Message);
                return false;
            }
        }

        private void CargarDatosEnFormulario()
        {
            if (dgvDatos.CurrentRow == null) return;

            try
            {
                // Cargar datos del registro seleccionado
                DataGridViewRow fila = dgvDatos.CurrentRow;
                
                txtId.Text = fila.Cells["Id"].Value.ToString();
                txtCodigo.Text = fila.Cells["Codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                
                // Seleccionar categoría en ComboBox
                string categoria = fila.Cells["Categoria"].Value.ToString();
                for (int i = 0; i < cmbCategoria.Items.Count; i++)
                {
                    if (cmbCategoria.Items[i].ToString() == categoria)
                    {
                        cmbCategoria.SelectedIndex = i;
                        break;
                    }
                }

                nudPrecioBase.Value = Convert.ToDecimal(fila.Cells["PrecioBase"].Value);
                chkRequiereEquipamiento.Checked = Convert.ToBoolean(fila.Cells["RequiereEquipamiento"].Value);
                chkActivo.Checked = Convert.ToBoolean(fila.Cells["Activo"].Value);

                // Cargar descripción desde la base de datos
                int id = Convert.ToInt32(txtId.Text);
                var detalles = nDiagnostico.ObtenerDiagnosticoPorId(id);
                if (detalles != null && detalles.Rows.Count > 0)
                {
                    txtDescripcion.Text = detalles.Rows[0]["descripcion"].ToString();
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error al cargar datos", ex.Message);
            }
        }
        #endregion

        #region Validaciones
        private bool ValidarDatos()
        {
            List<string> errores = new List<string>();

            // Validar campos requeridos
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
                errores.Add("• El nombre del diagnóstico es obligatorio");

            if (nudPrecioBase.Value <= 0)
                errores.Add("• El precio base debe ser mayor a 0");

            // Validar código único (solo en modo nuevo o si cambió el código)
            string codigoActual = txtCodigo.Text.Trim();
            if (!string.IsNullOrWhiteSpace(codigoActual))
            {
                bool codigoExiste = false;
                
                if (EsModoNuevo())
                {
                    codigoExiste = nDiagnostico.ExisteCodigoDiagnostico(codigoActual);
                }
                else if (EsModoEdicion())
                {
                    // En modo edición, validar solo si el código cambió
                    DataGridViewRow filaSeleccionada = dgvDatos.CurrentRow;
                    if (filaSeleccionada != null)
                    {
                        string codigoOriginal = filaSeleccionada.Cells["Codigo"].Value.ToString();
                        if (codigoActual != codigoOriginal)
                        {
                            codigoExiste = nDiagnostico.ExisteCodigoDiagnostico(codigoActual);
                        }
                    }
                }

                if (codigoExiste)
                    errores.Add("• El código ingresado ya existe");
            }

            // Validar longitud de campos
            if (txtNombre.Text.Length > 200)
                errores.Add("• El nombre no puede exceder 200 caracteres");

            if (txtCodigo.Text.Length > 20)
                errores.Add("• El código no puede exceder 20 caracteres");

            if (txtDescripcion.Text.Length > 1000)
                errores.Add("• La descripción no puede exceder 1000 caracteres");

            // Mostrar errores si existen
            if (errores.Count > 0)
            {
                string mensajeError = "Por favor corrija los siguientes errores:\n\n" + string.Join("\n", errores);
                MostrarError("Errores de validación", mensajeError);
                return false;
            }

            return true;
        }

        private void ValidarCamposRequeridos(object sender, EventArgs e)
        {
            // Cambiar color de borde según validación
            bool nombreValido = !string.IsNullOrWhiteSpace(txtNombre.Text);
            bool precioValido = nudPrecioBase.Value > 0;

            txtNombre.BackColor = nombreValido ? Color.White : Color.FromArgb(254, 242, 242);
            nudPrecioBase.BackColor = precioValido ? Color.White : Color.FromArgb(254, 242, 242);
        }
        #endregion

        #region Eventos Específicos del Módulo
        private void BtnGenerarCodigo_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoGenerado = nDiagnostico.GenerarCodigoAutomatico();
                txtCodigo.Text = codigoGenerado;
                
                // Mostrar notificación
                MostrarInformacion($"Código generado: {codigoGenerado}");
            }
            catch (Exception ex)
            {
                MostrarError("Error al generar código", ex.Message);
            }
        }

        private void BtnNuevaCategoria_Click(object sender, EventArgs e)
        {
            // Crear un formulario simple para input
            Form inputForm = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Nueva Categoría de Diagnóstico",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblPrompt = new Label() 
            { 
                Left = 20, 
                Top = 20, 
                Width = 350, 
                Text = "Ingrese el nombre de la nueva categoría:" 
            };
            
            TextBox txtInput = new TextBox() 
            { 
                Left = 20, 
                Top = 50, 
                Width = 340, 
                MaxLength = 50 
            };
            
            Button btnOk = new Button() 
            { 
                Text = "Aceptar", 
                Left = 200, 
                Width = 80, 
                Top = 90,
                DialogResult = DialogResult.OK
            };
            
            Button btnCancel = new Button() 
            { 
                Text = "Cancelar", 
                Left = 290, 
                Width = 80, 
                Top = 90,
                DialogResult = DialogResult.Cancel
            };

            btnOk.Click += (s, ev) => { inputForm.Close(); };
            btnCancel.Click += (s, ev) => { inputForm.Close(); };
            
            inputForm.Controls.Add(lblPrompt);
            inputForm.Controls.Add(txtInput);
            inputForm.Controls.Add(btnOk);
            inputForm.Controls.Add(btnCancel);
            inputForm.AcceptButton = btnOk;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog(this) == DialogResult.OK)
            {
                string nuevaCategoria = txtInput.Text.Trim();
                
                if (!string.IsNullOrWhiteSpace(nuevaCategoria))
                {
                    // Agregar a los ComboBox si no existe
                    bool existe = false;
                    for (int i = 0; i < cmbCategoria.Items.Count; i++)
                    {
                        if (cmbCategoria.Items[i].ToString().ToLower() == nuevaCategoria.ToLower())
                        {
                            existe = true;
                            break;
                        }
                    }

                    if (!existe)
                    {
                        cmbCategoria.Items.Add(nuevaCategoria);
                        cmbCategoriaFiltro.Items.Add(nuevaCategoria);
                        cmbCategoria.SelectedItem = nuevaCategoria;
                        
                        MostrarExito($"Categoría '{nuevaCategoria}' agregada exitosamente");
                    }
                    else
                    {
                        MostrarAdvertencia("Esta categoría ya existe");
                    }
                }
            }

            inputForm.Dispose();
        }

        private void BtnVerPorCategoria_Click(object sender, EventArgs e)
        {
            if (cmbCategoriaFiltro.SelectedItem != null)
            {
                categoriaSeleccionada = cmbCategoriaFiltro.SelectedItem.ToString();
                CargarDatosGrid();
                
                if (categoriaSeleccionada != "Todas")
                {
                    MostrarInformacion($"Mostrando diagnósticos de la categoría: {categoriaSeleccionada}");
                }
            }
        }

        private void CmbCategoriaFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoadingData && cmbCategoriaFiltro.SelectedItem != null)
            {
                categoriaSeleccionada = cmbCategoriaFiltro.SelectedItem.ToString();
                CargarDatosGrid();
            }
        }

        private void TxtCodigo_TextChanged(object sender, EventArgs e)
        {
            // Convertir a mayúsculas automáticamente
            if (txtCodigo.Text != txtCodigo.Text.ToUpper())
            {
                int cursorPos = txtCodigo.SelectionStart;
                txtCodigo.Text = txtCodigo.Text.ToUpper();
                txtCodigo.SelectionStart = cursorPos;
            }
        }

        private void TxtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Permitir solo letras, números, espacios y algunos caracteres especiales
            if (!char.IsLetterOrDigit(e.KeyChar) && 
                e.KeyChar != ' ' && 
                e.KeyChar != '-' && 
                e.KeyChar != '_' && 
                e.KeyChar != '/' &&
                e.KeyChar != '(' &&
                e.KeyChar != ')' &&
                !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        #endregion

        #region Métodos de Control de Modo
        private void EstablecerModo(ModoOperacion modo)
        {
            cmbModo.SelectedIndex = (int)modo - 1;
            OnCambioModo(modo != ModoOperacion.Consulta);
        }

        protected override void OnCambioModo(bool esEdicion)
        {
            ModoOperacion modoActual = (ModoOperacion)(cmbModo.SelectedIndex + 1);

            switch (modoActual)
            {
                case ModoOperacion.Consulta:
                    ConfigurarModoConsulta();
                    break;
                case ModoOperacion.NuevoDiagnostico:
                    ConfigurarModoNuevo();
                    break;
                case ModoOperacion.EditarDiagnostico:
                    ConfigurarModoEdicion();
                    break;
                case ModoOperacion.VerDetalles:
                    ConfigurarModoDetalles();
                    break;
            }

            base.OnCambioModo(esEdicion);
        }

        private void ConfigurarModoConsulta()
        {
            DeshabilitarFormulario();
            tabControlPrincipal.SelectedTab = tabInicio;
        }

        private void ConfigurarModoNuevo()
        {
            LimpiarFormulario();
            HabilitarFormulario();
            tabControlPrincipal.SelectedTab = tabConfiguraciones;
            txtNombre.Focus();
            
            // Establecer valores por defecto
            chkActivo.Checked = true;
            chkRequiereEquipamiento.Checked = false;
            nudPrecioBase.Value = 50; // Precio base por defecto
        }

        private void ConfigurarModoEdicion()
        {
            if (dgvDatos.CurrentRow == null)
            {
                MostrarAdvertencia("Seleccione un diagnóstico para editar");
                EstablecerModo(ModoOperacion.Consulta);
                return;
            }

            CargarDatosEnFormulario();
            HabilitarFormulario();
            tabControlPrincipal.SelectedTab = tabConfiguraciones;
            txtNombre.Focus();
        }

        private void ConfigurarModoDetalles()
        {
            if (dgvDatos.CurrentRow == null)
            {
                MostrarAdvertencia("Seleccione un diagnóstico para ver detalles");
                EstablecerModo(ModoOperacion.Consulta);
                return;
            }

            CargarDatosEnFormulario();
            DeshabilitarFormulario();
            tabControlPrincipal.SelectedTab = tabConfiguraciones;
        }

        private bool EsModoNuevo()
        {
            return cmbModo.SelectedIndex == (int)ModoOperacion.NuevoDiagnostico - 1;
        }

        private bool EsModoEdicion()
        {
            return cmbModo.SelectedIndex == (int)ModoOperacion.EditarDiagnostico - 1;
        }
        #endregion

        #region Sobrescribir Métodos del Base
        protected override void OnLoad()
        {
            CargarDatosGrid();
            EstablecerModo(ModoOperacion.Consulta);
        }

        protected override void OnBuscar()
        {
            BuscarDatos();
        }

        protected override void OnNuevo()
        {
            EstablecerModo(ModoOperacion.NuevoDiagnostico);
        }

        protected override void OnEditar(DataGridViewRow row)
        {
            EstablecerModo(ModoOperacion.EditarDiagnostico);
        }

        protected override void OnGuardar()
        {
            GuardarDatos();
        }

        protected override void OnEliminar()
        {
            EliminarDatos();
        }

        protected override void LimpiarFormulario()
        {
            txtId.Clear();
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            cmbCategoria.SelectedIndex = 0;
            nudPrecioBase.Value = 0;
            chkRequiereEquipamiento.Checked = false;
            chkActivo.Checked = true;
            
            // Restaurar colores normales
            txtNombre.BackColor = Color.White;
            nudPrecioBase.BackColor = Color.White;
        }
        #endregion

        #region Métodos de Utilidad

        private void HabilitarFormulario()
        {
            try
            {
                if (grpDatosDiagnostico != null)
                    grpDatosDiagnostico.Enabled = true;
                
                if (btnGuardar != null)
                    btnGuardar.Visible = true;
                
                if (btnEliminar != null)
                    btnEliminar.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarError("Error al habilitar formulario", $"Error: {ex.Message}");
            }
        }

        private void DeshabilitarFormulario()
        {
            try
            {
                if (grpDatosDiagnostico != null)
                    grpDatosDiagnostico.Enabled = false;
                
                if (btnGuardar != null)
                    btnGuardar.Visible = false;
                
                if (btnEliminar != null)
                    btnEliminar.Visible = true;
            }
            catch (Exception ex)
            {
                MostrarError("Error al deshabilitar formulario", $"Error: {ex.Message}");
            }
        }

        // Métodos de notificación
        private void MostrarError(string titulo, string mensaje)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MostrarExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Operación Exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MostrarAdvertencia(string mensaje)
        {
            MessageBox.Show(mensaje, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void MostrarInformacion(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion
    }
}
