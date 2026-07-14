using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemVeterinario
{
    /// <summary>
    /// Enumeración para definir los diferentes tipos de vista/acción
    /// </summary>
    public enum ViewType
    {
        Index,      // Lista principal
        Create,     // Crear nuevo
        Edit,       // Editar existente
        View,       // Ver detalles (solo lectura)
        Delete      // Confirmación de eliminación
    }

    /// <summary>
    /// Representa un elemento en la navegación breadcrumb
    /// </summary>
    public class BreadcrumbItem
    {
        public string Text { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public ViewType ViewType { get; set; }
        public object? Data { get; set; }
        public bool IsActive { get; set; }

        public BreadcrumbItem(string module, ViewType viewType, string text, object? data = null, bool isActive = false)
        {
            Module = module;
            ViewType = viewType;
            Text = text;
            Data = data;
            IsActive = isActive;
        }
    }

    /// <summary>
    /// Gestor de navegación para el sistema modular
    /// Maneja breadcrumbs y navegación entre vistas
    /// </summary>
    public class NavigationManager
    {
        private readonly Panel _contentPanel;
        private readonly Panel _breadcrumbPanel;
        private readonly List<BreadcrumbItem> _breadcrumbs;
        private UserControl? _currentView;

        public event Action<string, ViewType, object?>? NavigationRequested;

        public NavigationManager(Panel contentPanel)
        {
            _contentPanel = contentPanel ?? throw new ArgumentNullException(nameof(contentPanel));
            _breadcrumbs = new List<BreadcrumbItem>();
            
            // Crear panel de breadcrumbs
            _breadcrumbPanel = CreateBreadcrumbPanel();
            _contentPanel.Controls.Add(_breadcrumbPanel);
        }

        /// <summary>
        /// Navega a una vista específica
        /// </summary>
        public void NavigateTo(string module, ViewType viewType, UserControl view, object? data = null)
        {
            try
            {
                // Limpiar vista actual
                ClearCurrentView();

                // Actualizar breadcrumbs
                UpdateBreadcrumbs(module, viewType, data);

                // Configurar nueva vista
                _currentView = view;
                _currentView.Dock = DockStyle.Fill;
                
                // Agregar al panel de contenido
                _contentPanel.Controls.Add(_currentView);
                _currentView.BringToFront();

                // Forzar actualización de breadcrumbs
                RefreshBreadcrumbs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en navegación: {ex.Message}", "Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Actualiza los breadcrumbs según la navegación
        /// </summary>
        private void UpdateBreadcrumbs(string module, ViewType viewType, object? data)
        {
            // Obtener texto para el breadcrumb
            string viewText = GetViewText(viewType, data);
            
            // Limpiar breadcrumbs y agregar nuevo
            _breadcrumbs.Clear();

            // Agregar breadcrumb del módulo (siempre inicio)
            _breadcrumbs.Add(new BreadcrumbItem(module, ViewType.Index, "Inicio", null, viewType == ViewType.Index));

            // Si no es Index, agregar breadcrumb específico
            if (viewType != ViewType.Index)
            {
                _breadcrumbs.Add(new BreadcrumbItem(module, viewType, viewText, data, true));
            }
        }

        /// <summary>
        /// Obtiene el texto descriptivo para cada tipo de vista
        /// </summary>
        private string GetViewText(ViewType viewType, object? data)
        {
            return viewType switch
            {
                ViewType.Index => "Inicio",
                ViewType.Create => "Nuevo",
                ViewType.Edit => $"Editando",
                ViewType.View => "Detalles",
                ViewType.Delete => "Eliminar",
                _ => "Desconocido"
            };
        }

        /// <summary>
        /// Crea el panel de breadcrumbs
        /// </summary>
        private Panel CreateBreadcrumbPanel()
        {
            var panel = new Panel
            {
                Height = 50,
                Dock = DockStyle.Top,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            return panel;
        }

        /// <summary>
        /// Refresca la visualización de breadcrumbs
        /// </summary>
        private void RefreshBreadcrumbs()
        {
            _breadcrumbPanel.Controls.Clear();

            if (!_breadcrumbs.Any()) return;

            var currentX = 10;
            var currentModule = "";

            for (int i = 0; i < _breadcrumbs.Count; i++)
            {
                var breadcrumb = _breadcrumbs[i];
                
                // Agregar nombre del módulo si cambió
                if (breadcrumb.Module != currentModule)
                {
                    var moduleLabel = CreateModuleLabel(breadcrumb.Module);
                    moduleLabel.Location = new Point(currentX, 15);
                    _breadcrumbPanel.Controls.Add(moduleLabel);
                    currentX += moduleLabel.Width + 5;
                    currentModule = breadcrumb.Module;

                    // Agregar separador si no es el primer elemento
                    if (i > 0 || breadcrumb.ViewType != ViewType.Index)
                    {
                        var separator = CreateSeparator();
                        separator.Location = new Point(currentX, 17);
                        _breadcrumbPanel.Controls.Add(separator);
                        currentX += separator.Width + 5;
                    }
                }

                // Agregar breadcrumb de vista si no es Index
                if (breadcrumb.ViewType != ViewType.Index)
                {
                    var viewLabel = CreateBreadcrumbLabel(breadcrumb);
                    viewLabel.Location = new Point(currentX, 15);
                    _breadcrumbPanel.Controls.Add(viewLabel);
                    currentX += viewLabel.Width + 5;
                }
            }
        }

        /// <summary>
        /// Crea el label del módulo (ej: "Cliente")
        /// </summary>
        private Label CreateModuleLabel(string module)
        {
            var label = new Label
            {
                Text = module,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                AutoSize = true,
                Cursor = Cursors.Hand,
                Tag = new { Module = module, ViewType = ViewType.Index }
            };

            // Agregar evento click
            label.Click += BreadcrumbLabel_Click;
            
            return label;
        }

        /// <summary>
        /// Crea el separador entre breadcrumbs
        /// </summary>
        private Label CreateSeparator()
        {
            return new Label
            {
                Text = "?",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true
            };
        }

        /// <summary>
        /// Crea el label del breadcrumb
        /// </summary>
        private Label CreateBreadcrumbLabel(BreadcrumbItem breadcrumb)
        {
            var label = new Label
            {
                Text = breadcrumb.Text,
                Font = new Font("Segoe UI", 10F, 
                    breadcrumb.IsActive ? FontStyle.Regular : FontStyle.Regular),
                ForeColor = breadcrumb.IsActive ? 
                    Color.FromArgb(52, 73, 94) : Color.FromArgb(149, 165, 166),
                AutoSize = true,
                Tag = breadcrumb
            };

            // Solo hacer clickeable si no está activo
            if (!breadcrumb.IsActive)
            {
                label.Cursor = Cursors.Hand;
                label.Click += BreadcrumbLabel_Click;
            }

            return label;
        }

        /// <summary>
        /// Maneja el click en breadcrumbs
        /// </summary>
        private void BreadcrumbLabel_Click(object? sender, EventArgs e)
        {
            if (sender is Label label && label.Tag != null)
            {
                // Manejar tanto BreadcrumbItem como objetos anónimos
                if (label.Tag is BreadcrumbItem breadcrumb)
                {
                    NavigationRequested?.Invoke(breadcrumb.Module, breadcrumb.ViewType, breadcrumb.Data);
                }
                else if (label.Tag is { } tagObj)
                {
                    // Usar reflexión para objetos anónimos
                    var moduleProperty = tagObj.GetType().GetProperty("Module");
                    var viewTypeProperty = tagObj.GetType().GetProperty("ViewType");
                    
                    if (moduleProperty != null && viewTypeProperty != null)
                    {
                        var module = moduleProperty.GetValue(tagObj)?.ToString() ?? "";
                        var viewType = (ViewType)(viewTypeProperty.GetValue(tagObj) ?? ViewType.Index);
                        NavigationRequested?.Invoke(module, viewType, null);
                    }
                }
            }
        }

        /// <summary>
        /// Limpia la vista actual
        /// </summary>
        private void ClearCurrentView()
        {
            if (_currentView != null)
            {
                _contentPanel.Controls.Remove(_currentView);
                _currentView.Dispose();
                _currentView = null;
            }
        }

        /// <summary>
        /// Navega de vuelta al índice del módulo
        /// </summary>
        public void NavigateToIndex(string module)
        {
            NavigationRequested?.Invoke(module, ViewType.Index, null);
        }

        /// <summary>
        /// Obtiene el breadcrumb activo current
        /// </summary>
        public BreadcrumbItem? GetActiveBreadcrumb()
        {
            return _breadcrumbs.FirstOrDefault(b => b.IsActive);
        }

        /// <summary>
        /// Libera recursos
        /// </summary>
        public void Dispose()
        {
            ClearCurrentView();
            _breadcrumbs.Clear();
        }
    }
}