using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SistemVeterinario.Navigation
{
    public partial class BaseModulos : UserControl
    {
        #region Propiedades

        protected int IdSeleccionado { get; set; }
        protected bool ModoEdicion { get; set; }

        // Diccionario para recordar el estado de visibilidad de las columnas
        private Dictionary<string, bool> _columnasVisibles = new Dictionary<string, bool>();

        #endregion

        #region Constructor

        public BaseModulos()
        {
            InitializeComponent();
            InicializarControlBase();
        }

        #endregion

        #region Inicialización

        private void InicializarControlBase()
        {
            // Configuración inicial
            cmbModo.SelectedIndex = 0;
            IdSeleccionado = 0;
            ModoEdicion = false;
        }

        private void SearchBase_Load(object sender, EventArgs e)
        {
            // Este evento se ejecuta cuando se carga el control
            OnLoad();
        }

        private void DgvDatos_DataSourceChanged(object sender, EventArgs e)
        {
            // Este evento se ejecuta cuando cambia el DataSource
            PersonalizarColumnas();
            AgregarColumnasAccion();
            ConfigurarEventosColumnas();
        }

        private void ConfigurarEventosColumnas()
        {
            // Agregar evento de clic secundario en headers
            dgvDatos.ColumnHeaderMouseClick += DgvDatos_ColumnHeaderMouseClick;
        }

        #endregion

        #region Eventos del Designer

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            OnBuscar();
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            OnNuevo();
        }

        private void TxtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                OnBuscar();
            }
        }

        private void DgvDatos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDatos.Rows[e.RowIndex];

                // Verificar si se hizo click en el botón Editar
                if (e.ColumnIndex == dgvDatos.Columns["btnEditar"]?.Index)
                {
                    OnEditar(row);
                }
                // Verificar si se hizo click en el botón Eliminar
                else if (e.ColumnIndex == dgvDatos.Columns["btnEliminar"]?.Index)
                {
                    OnEliminarFila(row);
                }
            }
        }

        private void CmbModo_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool esEdicion = cmbModo.SelectedIndex == 1;
            txtId.Enabled = esEdicion;
            txtId.BackColor = esEdicion ? Color.White : System.Drawing.SystemColors.Control;
            btnEliminar.Visible = esEdicion;

            ModoEdicion = esEdicion;

            OnCambioModo(esEdicion);
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            OnGuardar();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            OnCancelar();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            OnEliminar();
        }

        private void ChkMostrarTodo_CheckedChanged(object sender, EventArgs e)
        {
            AlternarVisibilidadTodasLasColumnas();
        }

        private void DgvDatos_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Solo procesar clic secundario (botón derecho)
            if (e.Button == MouseButtons.Right)
            {
                // Obtener la columna clickeada
                var columna = dgvDatos.Columns[e.ColumnIndex];

                // No permitir ocultar las columnas de botones de acción
                if (columna.Name == "btnEditar" || columna.Name == "btnEliminar")
                {
                    return;
                }
                chkMostrarTodo.Checked = false;
                // Alternar visibilidad de la columna
                AlternarVisibilidadColumna(columna);
            }
        }

        #endregion

        #region Métodos Virtuales - Para ser implementados por las clases heredadas

        protected virtual void OnLoad()
        {
            // Implementar en clase hija
        }

        protected virtual void OnBuscar()
        {
            // Implementar en clase hija
        }

        protected virtual void OnNuevo()
        {
            cmbModo.SelectedIndex = 0;
            IdSeleccionado = 0;
            tabControlPrincipal.SelectedTab = tabConfiguraciones;
            LimpiarFormulario();
        }

        protected virtual void OnEditar(DataGridViewRow row)
        {
            // Obtener ID de la fila seleccionada
            if (row.DataBoundItem is DataRowView dataRow)
            {
                IdSeleccionado = Convert.ToInt32(dataRow["id"]);
                txtId.Text = IdSeleccionado.ToString();
                cmbModo.SelectedIndex = 1;
                tabControlPrincipal.SelectedTab = tabConfiguraciones;
                CargarDatosParaEdicion(IdSeleccionado);
            }
        }

        protected virtual void OnGuardar()
        {
            // Implementar en clase hija
        }

        protected virtual void OnCancelar()
        {
            tabControlPrincipal.SelectedTab = tabInicio;
            LimpiarFormulario();
        }

        protected virtual void OnEliminar()
        {
            // Implementar en clase hija
        }

        protected virtual void OnEliminarFila(DataGridViewRow row)
        {
            // Obtener ID de la fila seleccionada
            if (row.DataBoundItem is DataRowView dataRow)
            {
                int id = Convert.ToInt32(dataRow["id"]);
                string nombre = dataRow["nombre_completo"]?.ToString() ?? "registro";

                var resultado = MostrarConfirmacion(
                    $"¿Está seguro que desea eliminar el registro '{nombre}'?",
                    "Confirmar eliminación"
                );

                if (resultado == DialogResult.Yes)
                {
                    EliminarRegistro(id);
                    OnBuscar(); // Refrescar la lista
                }
            }
        }

        protected virtual void EliminarRegistro(int id)
        {
            // Implementar en clase hija - lógica específica de eliminación
        }

        protected virtual void OnCambioModo(bool esEdicion)
        {
            // Implementar en clase hija si es necesario
        }

        protected virtual void LimpiarFormulario()
        {
            // Implementar en clase hija
        }

        protected virtual void CargarDatosParaEdicion(int id)
        {
            // Implementar en clase hija
        }

        #endregion

        #region Métodos Auxiliares

        public void CargarDatos(DataTable datos)
        {
            dgvDatos.DataSource = datos;

            // Ocultar columna ID si existe
            if (dgvDatos.Columns["id"] != null)
            {
                dgvDatos.Columns["id"].Visible = false;
            }
        }

        private void PersonalizarColumnas()
        {
            // Diccionario para mapear nombres de columna a headers en español con formato correcto
            var headerMapping = new Dictionary<string, (string header, int width)>
            {
                {"tipo", ("Tipo", 80)},
                {"email", ("Email", 200)},
                {"direccion", ("Dirección", 250)},
                {"telefono", ("Teléfono", 120)},
                {"activo", ("Activo", 60)},
                {"ci", ("CI", 100)},
                {"nombre", ("Nombre", 150)},
                {"apellido", ("Apellido", 150)},
                {"fecha_nacimiento", ("F. Nacimiento", 120)},
                {"genero", ("Género", 80)},
                {"razon_social", ("Razón Social", 120)},
                {"nit", ("NIT", 120)},
                {"nombre_completo", ("Nombre Completo", 250)},
                {"representante_legal", ("Representante", 200)},
                {"giro", ("Giro", 150)},
                {"sector", ("Sector", 120)}
            };

            // Aplicar personalización a cada columna existente
            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                if (headerMapping.TryGetValue(column.Name.ToLower(), out var config))
                {
                    column.HeaderText = config.header;
                    column.Width = config.width;
                }

                // Configurar para que no se ajuste automáticamente
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;

                // Inicializar el estado de visibilidad (todas visibles por defecto)
                if (column.Name != "btnEditar" && column.Name != "btnEliminar" && column.Name != "id")
                {
                    if (!_columnasVisibles.ContainsKey(column.Name))
                    {
                        _columnasVisibles[column.Name] = true;
                    }
                    column.Visible = _columnasVisibles[column.Name];
                }
            }

            // Actualizar el estado del checkbox
            ActualizarEstadoCheckboxMostrarTodo();
        }

        private void AgregarColumnasAccion()
        {
            // Verificar si ya existen las columnas para no duplicarlas
            if (dgvDatos.Columns["btnEditar"] != null)
            {
                dgvDatos.Columns.Remove("btnEditar");
            }
            if (dgvDatos.Columns["btnEliminar"] != null)
            {
                dgvDatos.Columns.Remove("btnEliminar");
            }

            // Agregar columna de botón Editar
            DataGridViewButtonColumn btnEditar = new DataGridViewButtonColumn();
            btnEditar.Name = "btnEditar";
            btnEditar.HeaderText = "EDITAR";
            btnEditar.Text = "✏️ Editar";
            btnEditar.UseColumnTextForButtonValue = true;
            btnEditar.Width = 100;
            btnEditar.FlatStyle = FlatStyle.Flat;
            btnEditar.DefaultCellStyle.BackColor = Color.FromArgb(0, 120, 215);
            btnEditar.DefaultCellStyle.ForeColor = Color.White;
            btnEditar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 100, 195);
            btnEditar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDatos.Columns.Add(btnEditar);

            // Agregar columna de botón Eliminar
            DataGridViewButtonColumn btnEliminar = new DataGridViewButtonColumn();
            btnEliminar.Name = "btnEliminar";
            btnEliminar.HeaderText = "ELIMINAR";
            btnEliminar.Text = "🗑️ Eliminar";
            btnEliminar.UseColumnTextForButtonValue = true;
            btnEliminar.Width = 100;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.DefaultCellStyle.BackColor = Color.FromArgb(220, 53, 69);
            btnEliminar.DefaultCellStyle.ForeColor = Color.White;
            btnEliminar.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 33, 49);
            btnEliminar.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgvDatos.Columns.Add(btnEliminar);
        }

        public void MostrarMensaje(string mensaje, string titulo = "Información", MessageBoxIcon icono = MessageBoxIcon.Information)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, icono);
        }

        public DialogResult MostrarConfirmacion(string mensaje, string titulo = "Confirmación")
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        #endregion

        #region Gestión de Visibilidad de Columnas

        private void AlternarVisibilidadTodasLasColumnas()
        {
            bool mostrarTodo = chkMostrarTodo.Checked;
            if (mostrarTodo)
            {
                // Mostrar todas las columnas
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    column.Visible = true;
                    _columnasVisibles[column.Name] = true;
                }
            }
            else
            {
                // Ocultar todas las columnas excepto las de acción y ID
                foreach (DataGridViewColumn column in dgvDatos.Columns)
                {
                    if (column.Name != "btnEditar" && column.Name != "btnEliminar" && column.Name != "id")
                    {
                        column.Visible = false;
                        _columnasVisibles[column.Name] = false;
                    }
                }
            }
        }

        private void AlternarVisibilidadColumna(DataGridViewColumn columna)
        {
            // Alternar la visibilidad de la columna específica
            columna.Visible = !columna.Visible;
            _columnasVisibles[columna.Name] = columna.Visible;

            // Actualizar el estado del checkbox si es necesario
            ActualizarEstadoCheckboxMostrarTodo();
        }

        private void ActualizarEstadoCheckboxMostrarTodo()
        {
            // Verificar si todas las columnas visibles están mostradas
            bool todasVisibles = true;
            bool algunaVisible = false;

            foreach (DataGridViewColumn column in dgvDatos.Columns)
            {
                if (column.Name != "btnEditar" && column.Name != "btnEliminar" && column.Name != "id")
                {
                    if (!column.Visible)
                        todasVisibles = false;
                    else
                        algunaVisible = true;
                }
            }

            // Actualizar el checkbox sin disparar el evento
            chkMostrarTodo.CheckedChanged -= ChkMostrarTodo_CheckedChanged;

            if (todasVisibles)
            {
                chkMostrarTodo.CheckState = CheckState.Checked;
                chkMostrarTodo.Text = "Mostrar todas las columnas";
            }
            else if (algunaVisible)
            {
                chkMostrarTodo.CheckState = CheckState.Indeterminate;
                chkMostrarTodo.Text = "Algunas columnas ocultas";
            }
            else
            {
                chkMostrarTodo.CheckState = CheckState.Unchecked;
                chkMostrarTodo.Text = "Todas las columnas ocultas";
            }

            chkMostrarTodo.CheckedChanged += ChkMostrarTodo_CheckedChanged;
        }

        #endregion
    }
}
