<?php

require_once "conexion.php";

// Recibiendo los valores de los campos del formulario
$Mascota_Cliente = $_POST['Mascota_Cliente'];        // Nombre de la mascota del cliente
$Diagnostico = $_POST['Diagnostico'];                // Diagnóstico de la mascota
$Tratamiento = $_POST['Tratamiento'];                // Tratamiento aplicado a la mascota
$Especialidades = $_POST['Especialidades'];          // Especialidades relacionadas con el tratamiento
$Pronostico_Final = $_POST['Pronostico_Final'];      // Pronóstico final de la mascota
$Cod_Mascotas = $_POST['Cod_Mascotas'];              // Código de la mascota (FK)

// Inserción en la base de datos
$sql = "INSERT INTO Historial 
        (Mascota_Cliente, Diagnostico, Tratamiento, Especialidades, Pronostico_Final, Cod_Mascotas) 
        VALUES 
        ('$Mascota_Cliente', '$Diagnostico', '$Tratamiento', '$Especialidades', '$Pronostico_Final', '$Cod_Mascotas')";

$conex->query($sql);

// Redirigir al index después de realizar la operación
header('location: index.html');

?>
