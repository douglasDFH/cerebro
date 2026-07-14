<?php
include("bd.php");

try {
    echo "<h2>Verificación de tablas en la base de datos appbike_store</h2>";
    
    // Mostrar todas las tablas
    $tables = $conexion->query("SHOW TABLES");
    echo "<h3>Tablas disponibles:</h3>";
    echo "<ul>";
    foreach ($tables as $table) {
        echo "<li>" . $table[0] . "</li>";
    }
    echo "</ul>";
    
    // Verificar específicamente la tabla categories
    $check_categories = $conexion->query("SHOW TABLES LIKE 'categories'");
    if ($check_categories->rowCount() > 0) {
        echo "<p style='color: green;'>✓ La tabla 'categories' existe</p>";
        
        // Mostrar estructura de categories
        $desc_categories = $conexion->query("DESCRIBE categories");
        echo "<h4>Estructura de la tabla categories:</h4>";
        echo "<table border='1'>";
        echo "<tr><th>Campo</th><th>Tipo</th><th>Null</th><th>Key</th><th>Default</th><th>Extra</th></tr>";
        foreach ($desc_categories as $col) {
            echo "<tr>";
            echo "<td>" . $col['Field'] . "</td>";
            echo "<td>" . $col['Type'] . "</td>";
            echo "<td>" . $col['Null'] . "</td>";
            echo "<td>" . $col['Key'] . "</td>";
            echo "<td>" . $col['Default'] . "</td>";
            echo "<td>" . $col['Extra'] . "</td>";
            echo "</tr>";
        }
        echo "</table>";
    } else {
        echo "<p style='color: red;'>✗ La tabla 'categories' NO existe</p>";
    }
    
} catch (PDOException $e) {
    echo "<p style='color: red;'>Error: " . $e->getMessage() . "</p>";
}
?>