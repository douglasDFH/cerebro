<?php
// Script para actualizar la contraseña del usuario superadmin
include("bd.php");

// Generar un nuevo hash para la contraseña 'admin123'
$nueva_contraseña = 'admin123';
$nuevo_hash = password_hash($nueva_contraseña, PASSWORD_DEFAULT);

echo "<h2>🔧 Actualización de contraseña</h2>";
echo "<hr>";

try {
    // Actualizar la contraseña del usuario superadmin
    $stmt = $conexion->prepare("UPDATE usuarios SET password = :password WHERE usuario = 'superadmin'");
    $stmt->bindParam(':password', $nuevo_hash);
    $result = $stmt->execute();
    
    if ($result) {
        echo "<p style='color: green;'>✅ Contraseña actualizada correctamente</p>";
        echo "Usuario: <strong>superadmin</strong><br>";
        echo "Contraseña: <strong>admin123</strong><br>";
        echo "Nuevo hash: " . substr($nuevo_hash, 0, 50) . "...<br><br>";
        
        // Verificar que funciona
        $verificacion = password_verify($nueva_contraseña, $nuevo_hash);
        echo "Verificación del nuevo hash: " . ($verificacion ? '✅ CORRECTO' : '❌ ERROR') . "<br><br>";
        
        // Verificar en la base de datos
        $stmt_check = $conexion->prepare("SELECT password FROM usuarios WHERE usuario = 'superadmin'");
        $stmt_check->execute();
        $hash_db = $stmt_check->fetchColumn();
        
        $verificacion_db = password_verify($nueva_contraseña, $hash_db);
        echo "Verificación desde BD: " . ($verificacion_db ? '✅ CORRECTO' : '❌ ERROR') . "<br>";
        
        if ($verificacion_db) {
            echo "<p style='color: green; font-weight: bold; background-color: #d4edda; padding: 10px; border-radius: 5px;'>";
            echo "🎉 ¡LISTO! Ahora puedes hacer login con:<br>";
            echo "Usuario: superadmin<br>";
            echo "Contraseña: admin123";
            echo "</p>";
        }
        
    } else {
        echo "<p style='color: red;'>❌ Error al actualizar la contraseña</p>";
    }
    
} catch (Exception $e) {
    echo "<p style='color: red;'>❌ Error: " . $e->getMessage() . "</p>";
}

echo "<br><hr>";
echo "<a href='login.php' style='background-color: #007bff; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>🔐 Ir al Login</a>";
echo "<br><br>";
echo "<a href='debug_usuarios.php' style='background-color: #28a745; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>🔍 Ver Depuración</a>";
?>