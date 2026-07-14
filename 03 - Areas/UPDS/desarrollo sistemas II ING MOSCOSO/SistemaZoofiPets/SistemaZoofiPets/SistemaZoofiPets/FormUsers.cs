using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;

namespace SistemaZoofiPets
{
    public partial class FormUsers : Form
    {
        ConexionSQLN cn = new ConexionSQLN();
        public FormUsers()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            try
            {
                DataTable usuarios = cn.ConsultarUsuarios();
                
                if (usuarios != null && usuarios.Rows.Count > 0)
                {
                    dataGridView1.DataSource = usuarios;
                    
                    // Configurar columnas del DataGridView con validación
                    try
                    {
                        if (dataGridView1.Columns["id"] != null)
                            dataGridView1.Columns["id"].HeaderText = "ID";
                        
                        if (dataGridView1.Columns["nombre"] != null)
                            dataGridView1.Columns["nombre"].HeaderText = "Nombre";
                        
                        if (dataGridView1.Columns["apellido"] != null)
                            dataGridView1.Columns["apellido"].HeaderText = "Apellido";
                        
                        if (dataGridView1.Columns["email"] != null)
                            dataGridView1.Columns["email"].HeaderText = "Email";
                        
                        if (dataGridView1.Columns["usuario"] != null)
                            dataGridView1.Columns["usuario"].HeaderText = "Usuario";
                        
                        if (dataGridView1.Columns["telefono"] != null)
                            dataGridView1.Columns["telefono"].HeaderText = "Teléfono";
                        
                        if (dataGridView1.Columns["direccion"] != null)
                            dataGridView1.Columns["direccion"].HeaderText = "Dirección";
                        
                        if (dataGridView1.Columns["fecha_creacion"] != null)
                            dataGridView1.Columns["fecha_creacion"].HeaderText = "Fecha Creación";
                        
                        // Ajustar ancho de columnas
                        dataGridView1.AutoResizeColumns();
                        dataGridView1.ReadOnly = true;
                        dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    }
                    catch (Exception exCol)
                    {
                        // Si hay error con las columnas, al menos mostrar los datos
                        MessageBox.Show($"Advertencia al configurar columnas: {exCol.Message}", "Advertencia", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontraron usuarios en el sistema", "Sin Datos", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar usuarios:\n{ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // En caso de error, crear una tabla vacía para evitar errores posteriores
                DataTable tablaVacia = new DataTable();
                tablaVacia.Columns.Add("Mensaje", typeof(string));
                tablaVacia.Rows.Add("Error al cargar datos");
                dataGridView1.DataSource = tablaVacia;
            }
        }

        private void Btn_nuevousuario_Click(object sender, EventArgs e)
        {
            try
            {
                int resultado = cn.InsertarUsuario(txt_nombre.Text, txt_apellido.Text, txt_email.Text, 
                                                 txt_usuario.Text, txt_contrasena.Text, txt_telefono.Text, txt_direccion.Text);
                
                if (resultado > 0)
                {
                    MessageBox.Show("Usuario creado exitosamente", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se pudo crear el usuario", "Error", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos Inválidos", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear usuario:\n{ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_modusuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_usuario.Text))
                {
                    MessageBox.Show("Seleccione un usuario para modificar", "Usuario Requerido", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int resultado = cn.ModificarUsuario(txt_nombre.Text, txt_apellido.Text, txt_email.Text, 
                                                  txt_usuario.Text, txt_contrasena.Text, txt_telefono.Text, txt_direccion.Text);
                
                if (resultado > 0)
                {
                    MessageBox.Show("Usuario modificado exitosamente", "Éxito", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarCampos();
                    CargarUsuarios();
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario a modificar", "Usuario No Encontrado", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Datos Inválidos", 
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al modificar usuario:\n{ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Btn_eliminarusuario_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_usuario.Text))
                {
                    MessageBox.Show("Seleccione un usuario para eliminar", "Usuario Requerido", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirmacion = MessageBox.Show(
                    $"¿Está seguro que desea eliminar el usuario '{txt_usuario.Text}'?", 
                    "Confirmar Eliminación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    int resultado = cn.EliminarUsuario(txt_usuario.Text);
                    
                    if (resultado > 0)
                    {
                        MessageBox.Show("Usuario eliminado exitosamente", "Éxito", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el usuario a eliminar", "Usuario No Encontrado", 
                                      MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar usuario:\n{ex.Message}", "Error", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txt_nombre.Clear();
            txt_apellido.Clear();
            txt_email.Clear();
            txt_usuario.Clear();
            txt_contrasena.Clear();
            txt_telefono.Clear();
            txt_direccion.Clear();
            txt_nombre.Focus();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void FormUsers_Load(object sender, EventArgs e)
        {

        }
    }
}
