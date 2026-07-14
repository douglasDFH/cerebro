using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace SistemVeterinario.Navigation
{
    public partial class BaseModulos : UserControl
    {
        #region Propiedades

        protected int IdSeleccionado { get; set; }
        protected bool ModoEdicion { get; set; }
        protected string[] ColumnasPersonalizadas { get; set; }

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
            
            // Limpiar cualquier columna de acción duplicada existente
            LimpiarColumnasDuplicadas();
            ColumnasPersonalizadas = new string[0]; // Inicializar array vacío
        }

        private void SearchBase_Load(object sender, EventArgs e)
        {
            // Este evento se ejecuta cuando se carga el control
            OnLoad();
        }

        private void DgvDatos_DataSourceChanged(object sender, EventArgs e)
        {
            // Este evento se ejecuta cuando cambia el DataSource
            // Solo agregar columnas si hay datos y no existen ya las columnas de acción
            if (dgvDatos.DataSource != null && 
                dgvDatos.Columns["btnEditar"] == null && 
                dgvDatos.Columns["btnEliminar"] == null)
            {
                AgregarColumnasAccion();
                ConfigurarEventosColumnas();
            }
        }

        private void ConfigurarEventosColumnas()
        {
            // Agregar evento de clic secundario en headers
            dgvDatos.ColumnHeaderMouseClick += DgvDatos_ColumnHeaderMouseClick;
        }

        private void LimpiarColumnasDuplicadas()
        {
            // Limpiar cualquier columna de botones duplicada al inicializar
            var columnasAEliminar = new List<DataGridViewColumn>();
            
            foreach (DataGridViewColumn col in dgvDatos.Columns)
            {
                if (col is DataGridViewButtonColumn && 
                    (col.HeaderText == "EDITAR" || col.Name == "btnEditar" ||
                     col.HeaderText == "ELIMINAR" || col.Name == "btnEliminar" ||
                     col.HeaderText == "Editar" || col.HeaderText == "Eliminar" ||
                     (col.HeaderText != null && (col.HeaderText.Contains("Editar") || col.HeaderText.Contains("EDITAR"))) ||
                     (col.HeaderText != null && (col.HeaderText.Contains("Eliminar") || col.HeaderText.Contains("ELIMINAR")))))
                {
                    columnasAEliminar.Add(col);
                }
            }
            
            foreach (DataGridViewColumn column in columnasAEliminar)
            {
                dgvDatos.Columns.Remove(column);
            }
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
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var row = dgvDatos.Rows[e.RowIndex];
                var column = dgvDatos.Columns[e.ColumnIndex];
                var columnName = dgvDatos.Columns[e.ColumnIndex].Name;

                // Verificar si es una columna de botón y que sea la correcta
                if (column is DataGridViewButtonColumn)
                {
                    // Verificar si se hizo click en el botón Editar (solo el primero que encuentre)
                    if (column.Name == "btnEditar" || (column.HeaderText == "EDITAR" && column.Name.Contains("Editar")))
                    {
                        OnEditar(row);
                    }
                    // Verificar si se hizo click en el botón Eliminar (solo el primero que encuentre)
                    else if (column.Name == "btnEliminar" || (column.HeaderText == "ELIMINAR" && column.Name.Contains("Eliminar")))
                    {
                        OnEliminarFila(row);
                    }
                    // Verificar si se hizo click en una columna personalizada
                    else if (ColumnasPersonalizadas.Contains(columnName))
                    {
                        OnAccionPersonalizada(row, columnName);
                    }
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

        protected virtual void OnAccionPersonalizada(DataGridViewRow row, string accion)
        {
            // Implementar en clase hija para manejar acciones de columnas personalizadas
        }

        #endregion

        #region Métodos Auxiliares

        public void CargarDatos(DataTable datos)
        {
            // Limpiar columnas duplicadas antes de cargar nuevos datos
            LimpiarColumnasDuplicadas();
            
            dgvDatos.DataSource = datos;

            // Ocultar columna ID si existe
            if (dgvDatos.Columns["id"] != null)
            {
                dgvDatos.Columns["id"].Visible = false;
            }
        }


        private void AgregarColumnasAccion()
        {
            // Limpiar cualquier columna de botones duplicada existente
            LimpiarColumnasDuplicadas();

            // Verificar una vez más que no existen para evitar duplicados
            if (dgvDatos.Columns["btnEditar"] != null || dgvDatos.Columns["btnEliminar"] != null)
            {
                return; // Si ya existen, no agregar más
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
            this.AddBtnCustom();

        }
        public virtual void AddBtnCustom()
        {
            // Implementar lógica para agregar botones personalizados
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
                    var inColumnasPersonalizadas = ColumnasPersonalizadas.Contains(column.Name);
                    if (column.Name != "btnEditar" && column.Name != "btnEliminar" && column.Name != "id" && !inColumnasPersonalizadas)
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
                var inColumnasPersonalizadas = ColumnasPersonalizadas.Contains(column.Name);
                if (column.Name != "btnEditar" && column.Name != "btnEliminar" && column.Name != "id" && !inColumnasPersonalizadas)
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
