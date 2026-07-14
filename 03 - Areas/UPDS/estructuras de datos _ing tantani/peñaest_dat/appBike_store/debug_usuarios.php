<?php
// Script de depuración para verificar usuarios
include("bd.php");

echo "<h2>🔍 Depuración de usuarios en la base de datos</h2>";
echo "<hr>";

try {
    // Verificar conexión a la base de datos
    echo "<h3>✅ Conexión a la base de datos</h3>";
    echo "Servidor: localhost<br>";
    echo "Base de datos: appbike_store<br>";
    echo "Estado: Conectado correctamente<br><br>";
    
    // Listar todos los usuarios
    echo "<h3>👥 Usuarios en la base de datos</h3>";
    $stmt = $conexion->prepare("SELECT user_id, usuario, email, rol_id, activo FROM usuarios ORDER BY user_id");
    $stmt->execute();
    $usuarios = $stmt->fetchAll(PDO::FETCH_ASSOC);
    
    if (count($usuarios) > 0) {
        echo "<table border='1' style='border-collapse: collapse; width: 100%;'>";
        echo "<tr style='background-color: #f0f0f0;'>";
        echo "<th style='padding: 8px;'>ID</th>";
        echo "<th style='padding: 8px;'>Usuario</th>";
        echo "<th style='padding: 8px;'>Email</th>";
        echo "<th style='padding: 8px;'>Rol ID</th>";
        echo "<th style='padding: 8px;'>Activo</th>";
        echo "</tr>";
        
        foreach ($usuarios as $usuario) {
            echo "<tr>";
            echo "<td style='padding: 8px;'>" . $usuario['user_id'] . "</td>";
            echo "<td style='padding: 8px;'><strong>" . $usuario['usuario'] . "</strong></td>";
            echo "<td style='padding: 8px;'>" . $usuario['email'] . "</td>";
            echo "<td style='padding: 8px;'>" . $usuario['rol_id'] . "</td>";
            echo "<td style='padding: 8px;'>" . ($usuario['activo'] ? 'Sí' : 'No') . "</td>";
            echo "</tr>";
        }
        echo "</table><br>";
    } else {
        echo "<p style='color: red;'>❌ No hay usuarios en la base de datos</p>";
    }
    
    // Verificar el hash de la contraseña del usuario superadmin
    echo "<h3>🔐 Verificación de contraseñas</h3>";
    $stmt_superadmin = $conexion->prepare("SELECT usuario, password FROM usuarios WHERE usuario = 'superadmin'");
    $stmt_superadmin->execute();
    $superadmin = $stmt_superadmin->fetch(PDO::FETCH_ASSOC);
    
    if ($superadmin) {
        echo "Usuario encontrado: <strong>superadmin</strong><br>";
        echo "Hash en BD: " . substr($superadmin['password'], 0, 30) . "...<br>";
        
        // Verificar si la contraseña 'admin123' coincide con el hash
        $password_test = 'admin123';
        $hash_correcto = '$2y$10$92IXUNpkjO0rOQ5byMi.Ye4oKoEa3Ro9llC/.og/at2.uheWG/igi';
        
        echo "Contraseña a probar: <strong>admin123</strong><br>";
        
        $verificacion1 = password_verify($password_test, $superadmin['password']);
        $verificacion2 = password_verify($password_test, $hash_correcto);
        
        echo "Verificación con hash de BD: " . ($verificacion1 ? '✅ CORRECTA' : '❌ INCORRECTA') . "<br>";
        echo "Verificación con hash esperado: " . ($verificacion2 ? '✅ CORRECTA' : '❌ INCORRECTA') . "<br>";
        
        // Generar un nuevo hash para comparar
        $nuevo_hash = password_hash($password_test, PASSWORD_DEFAULT);
        echo "Nuevo hash generado: " . substr($nuevo_hash, 0, 30) . "...<br>";
        
        $verificacion3 = password_verify($password_test, $nuevo_hash);
        echo "Verificación con nuevo hash: " . ($verificacion3 ? '✅ CORRECTA' : '❌ INCORRECTA') . "<br>";
        
    } else {
        echo "<p style='color: red;'>❌ Usuario 'superadmin' no encontrado</p>";
    }
    
    // Verificar roles
    echo "<h3>🏷️ Roles disponibles</h3>";
    $stmt_roles = $conexion->prepare("SELECT * FROM roles ORDER BY rol_id");
    $stmt_roles->execute();
    $roles = $stmt_roles->fetchAll(PDO::FETCH_ASSOC);
    
    if (count($roles) > 0) {
        echo "<table border='1' style='border-collapse: collapse; width: 100%;'>";
        echo "<tr style='background-color: #f0f0f0;'>";
        echo "<th style='padding: 8px;'>ID</th>";
        echo "<th style='padding: 8px;'>Nombre</th>";
        echo "<th style='padding: 8px;'>Descripción</th>";
        echo "</tr>";
        
        foreach ($roles as $rol) {
            echo "<tr>";
            echo "<td style='padding: 8px;'>" . $rol['rol_id'] . "</td>";
            echo "<td style='padding: 8px;'>" . $rol['nombre_rol'] . "</td>";
            echo "<td style='padding: 8px;'>" . $rol['descripcion'] . "</td>";
            echo "</tr>";
        }
        echo "</table><br>";
    } else {
        echo "<p style='color: red;'>❌ No hay roles en la base de datos</p>";
    }
    
    // Simulación de login
    echo "<h3>🧪 Prueba de autenticación</h3>";
    $usuario_prueba = 'superadmin';
    $password_prueba = 'admin123';
    
    $stmt_login = $conexion->prepare("SELECT u.user_id, u.usuario, u.password, u.email, r.nombre_rol, r.descripcion 
                                     FROM usuarios u 
                                     LEFT JOIN roles r ON u.rol_id = r.rol_id 
                                     WHERE u.usuario = :usuario");
    $stmt_login->bindParam(':usuario', $usuario_prueba);
    $stmt_login->execute();
    
    $usuario_db = $stmt_login->fetch(PDO::FETCH_ASSOC);
    
    if ($usuario_db) {
        echo "Usuario encontrado: ✅<br>";
        echo "ID: " . $usuario_db['user_id'] . "<br>";
        echo "Email: " . $usuario_db['email'] . "<br>";
        echo "Rol: " . $usuario_db['nombre_rol'] . "<br>";
        
        if (password_verify($password_prueba, $usuario_db['password'])) {
            echo "<p style='color: green; font-weight: bold;'>🎉 AUTENTICACIÓN EXITOSA - El login debería funcionar</p>";
        } else {
            echo "<p style='color: red; font-weight: bold;'>❌ FALLO EN AUTENTICACIÓN - La contraseña no coincide</p>";
        }
    } else {
        echo "<p style='color: red;'>❌ Usuario no encontrado en la consulta de login</p>";
    }
    
} catch (Exception $e) {
    echo "<h3 style='color: red;'>❌ Error de conexión</h3>";
    echo "Error: " . $e->getMessage();
}
?>