<?php

require_once "conexion.php";

// Recibiendo los valores de los campos del formulario
$Nombre = $_POST['Nombre'];                // Nombre de la mascota
$Especie = $_POST['Especie'];              // Especie de la mascota
$Raza = $_POST['Raza'];                    // Raza de la mascota
$Fecha_Nacimiento = $_POST['Fecha_Nacimiento']; // Fecha de nacimiento de la mascota
$Edad = $_POST['Edad'];                    // Edad de la mascota
$Cod_Clientes = $_POST['Cod_Clientes'];    // Código del cliente relacionado

// Inserción en la base de datos
$sql = "INSERT INTO Mascotas 
        (Nombre, Especie, Raza, Fecha_Nacimiento, Edad, Cod_Clientes) 
        VALUES 
        ('$Nombre', '$Especie', '$Raza', '$Fecha_Nacimiento', '$Edad', '$Cod_Clientes')";

// Ejecutar la consulta
$conex->query($sql);

// Redirigir al index después de realizar la operación
header('location: index.html');

?>
