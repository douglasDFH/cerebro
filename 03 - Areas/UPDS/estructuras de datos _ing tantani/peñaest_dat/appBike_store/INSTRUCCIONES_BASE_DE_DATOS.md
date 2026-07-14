# 📋 INSTRUCCIONES PARA CONFIGURAR LA BASE DE DATOS

## 🚀 Pasos para la instalación

### 1. Acceder a phpMyAdmin
- Abre tu navegador web
- Ve a: `http://localhost/phpmyadmin`
- Ingresa con tus credenciales de MySQL (generalmente usuario: `root`, sin contraseña en XAMPP)

### 2. Crear la base de datos
- Haz clic en "SQL" en el menú superior
- Copia y pega el contenido completo del archivo `bike_store_database.sql`
- Haz clic en "Continuar" para ejecutar el script

### 3. Insertar el usuario administrador
- Ejecuta el script `insert_admin_user.sql` para crear el usuario admin
- O ejecuta manualmente:
```sql
USE bike_store;
INSERT INTO usuarios (usuario, password, email, rol_id) VALUES
('admin', '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi', 'admin@bikestore.com', 1);
```

### 4. Verificar la instalación
Ejecuta estas consultas para verificar que todo esté correcto:

```sql
-- Verificar tablas creadas
SHOW TABLES;

-- Verificar datos iniciales
SELECT COUNT(*) as total_roles FROM roles;
SELECT COUNT(*) as total_usuarios FROM usuarios;
SELECT COUNT(*) as total_customers FROM customers;
SELECT COUNT(*) as total_products FROM products;
SELECT COUNT(*) as total_orders FROM orders;

-- Verificar usuario admin
SELECT u.usuario, u.email, r.nombre_rol 
FROM usuarios u 
INNER JOIN roles r ON u.rol_id = r.rol_id 
WHERE u.usuario = 'admin';
```

## 🔐 Credenciales de acceso

**Usuario administrador:**
- Usuario: `admin`
- Contraseña: `admin123`
- Email: `admin@bikestore.com`

> ⚠️ **IMPORTANTE**: Cambiar la contraseña después del primer login por seguridad.

## 📊 Estructura de la base de datos

### Tablas principales:
1. **roles** - Roles de usuario del sistema
2. **usuarios** - Usuarios administrativos
3. **customers** - Clientes de la tienda
4. **products** - Catálogo de productos (bicicletas)
5. **orders** - Pedidos realizados
6. **order_items** - Productos en cada pedido

### Datos de ejemplo incluidos:
- ✅ 3 roles (administrador, vendedor, supervisor)
- ✅ 5 clientes de ejemplo
- ✅ 6 productos de bicicletas
- ✅ 5 pedidos con sus respectivos items

## 🛠️ Configuración del proyecto

### Verificar archivo de conexión
Asegúrate de que el archivo `bd.php` tenga la configuración correcta:

```php
<?php
$servidor = "localhost";
$basededatos = "bike_store";
$usuario = "root";
$contrasenia = "";
// ... resto del código
?>
```

### Crear carpetas necesarias
Crear la carpeta para imágenes de productos:
```
uploads/
  └── products/
```

## 🧪 Pruebas del sistema

1. **Probar conexión**: Ejecuta `index.php` y verifica que no haya errores de conexión
2. **Login de usuario**: Prueba el login con las credenciales del admin
3. **CRUD básico**: 
   - Crear un nuevo cliente
   - Agregar un producto
   - Crear un pedido
   - Ver reportes

## 📈 Características adicionales incluidas

### Vistas creadas:
- `v_pedidos_completos` - Pedidos con información completa
- `v_productos_ventas` - Productos con estadísticas de ventas

### Procedimientos almacenados:
- `sp_calcular_total_pedido` - Calcula totales de un pedido
- `sp_productos_bajo_stock` - Productos con stock bajo

### Triggers:
- Actualización automática de stock al crear/eliminar items de pedido

## 🔧 Solución de problemas comunes

### Error de conexión:
- Verificar que XAMPP/WAMP esté ejecutándose
- Verificar credenciales en `bd.php`
- Verificar que la base de datos `bike_store` exista

### Error de permisos:
- Verificar permisos de la carpeta `uploads/`
- Configurar permisos de escritura en el servidor web

### Problemas con caracteres especiales:
- La base de datos usa `utf8mb4` para soporte completo de Unicode
- Verificar que PHP tenga configurado UTF-8

## 📞 Soporte

Si encuentras algún problema:
1. Verificar los logs de error de PHP
2. Revisar la consola de desarrollador del navegador
3. Verificar que todas las tablas se hayan creado correctamente
4. Comprobar que los datos de ejemplo se insertaron

---
**¡Listo para usar! Tu sistema de tienda de bicicletas está configurado y operativo.** 🚴‍♂️🚴‍♀️