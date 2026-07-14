using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PedidosApp
{
    public enum UserRole
    {
        Administrator = 1,
        Manager = 2,
        Employee = 3
    }

    public class UserSession
    {
        private static UserSession _instance;
        private static readonly object _lock = new object();

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public DateTime LoginTime { get; set; }
        public List<string> Permissions { get; set; }

        private UserSession()
        {
            Permissions = new List<string>();
        }

        public static UserSession Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new UserSession();
                    }
                }
                return _instance;
            }
        }

        public void SetUserData(int userId, string userName, string email, int roleId, string roleName, string roleDescription)
        {
            UserId = userId;
            UserName = userName;
            Email = email;
            Role = (UserRole)roleId;
            RoleName = roleName;
            RoleDescription = roleDescription;
            LoginTime = DateTime.Now;
            
            // Cargar permisos del usuario
            LoadUserPermissions();
        }

        private void LoadUserPermissions()
        {
            try
            {
                // Usar directamente la capa de datos para evitar referencia circular
                var dusers = new CapaDatos.Dusers();
                var dataTable = dusers.GetUserPermissions(UserId);
                Permissions.Clear();
                
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dataTable.Rows)
                    {
                        string permission = row["permission_name"].ToString();
                        if (!Permissions.Contains(permission))
                        {
                            Permissions.Add(permission);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar permisos: " + ex.Message);
            }
        }

        public bool HasPermission(string permissionName)
        {
            return Permissions.Contains(permissionName);
        }

        public bool IsAdministrator()
        {
            return Role == UserRole.Administrator;
        }

        public bool IsManager()
        {
            return Role == UserRole.Manager || Role == UserRole.Administrator;
        }

        public bool IsEmployee()
        {
            return Role == UserRole.Employee;
        }

        public void ClearSession()
        {
            UserId = 0;
            UserName = string.Empty;
            Email = string.Empty;
            Role = UserRole.Employee;
            RoleName = string.Empty;
            RoleDescription = string.Empty;
            LoginTime = DateTime.MinValue;
            Permissions.Clear();
        }

        public bool IsLoggedIn()
        {
            return UserId > 0 && !string.IsNullOrEmpty(UserName);
        }

        // Métodos de conveniencia para verificar permisos específicos
        public bool CanViewOrders() => HasPermission("VIEW_ORDERS");
        public bool CanCreateOrders() => HasPermission("CREATE_ORDERS");
        public bool CanEditOrders() => HasPermission("EDIT_ORDERS");
        public bool CanDeleteOrders() => HasPermission("DELETE_ORDERS");
        public bool CanManageOrderStatus() => HasPermission("MANAGE_ORDER_STATUS");

        public bool CanViewProducts() => HasPermission("VIEW_PRODUCTS");
        public bool CanCreateProducts() => HasPermission("CREATE_PRODUCTS");
        public bool CanEditProducts() => HasPermission("EDIT_PRODUCTS");
        public bool CanDeleteProducts() => HasPermission("DELETE_PRODUCTS");
        public bool CanManageStock() => HasPermission("MANAGE_STOCK");

        public bool CanViewCustomers() => HasPermission("VIEW_CUSTOMERS");
        public bool CanCreateCustomers() => HasPermission("CREATE_CUSTOMERS");
        public bool CanEditCustomers() => HasPermission("EDIT_CUSTOMERS");
        public bool CanDeleteCustomers() => HasPermission("DELETE_CUSTOMERS");

        public bool CanViewUsers() => HasPermission("VIEW_USERS");
        public bool CanCreateUsers() => HasPermission("CREATE_USERS");
        public bool CanEditUsers() => HasPermission("EDIT_USERS");
        public bool CanDeleteUsers() => HasPermission("DELETE_USERS");

        public bool CanViewReports() => HasPermission("VIEW_REPORTS");
        public bool CanViewAdvancedReports() => HasPermission("VIEW_ADVANCED_REPORTS");
        public bool CanExportReports() => HasPermission("EXPORT_REPORTS");

        public bool CanConfigureSystem() => HasPermission("SYSTEM_CONFIG");
        public bool CanBackupRestore() => HasPermission("BACKUP_RESTORE");

        public override string ToString()
        {
            return $"{UserName} ({RoleName})";
        }
    }
}