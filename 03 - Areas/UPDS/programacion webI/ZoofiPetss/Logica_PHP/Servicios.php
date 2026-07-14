<?php

require_once "conexion.php";

// Recibiendo los valores de los campos del formulario
$Especialidades = $_POST['Especialidades'];      // Especialidad del servicio
$Especialista = $_POST['Especialista'];          // Nombre del especialista
$Precio = $_POST['Precio'];                      // Precio del servicio
$Duracion_Estimada = $_POST['Duracion_Estimada']; // Duración estimada del servicio
$Categoria = $_POST['Categoria'];                // Categoría del servicio
$Turno = $_POST['Turno'];                        // Turno en que se realiza el servicio
$Cod_Historial = $_POST['Cod_Historial'];        // Código del historial médico de la mascota

// Inserción en la base de datos
$sql = "INSERT INTO Servicios 
        (Especialidades, Especialista, Precio, Duracion_Estimada, Categoria, Turno, Cod_Historial) 
        VALUES 
        ('$Especialidades', '$Especialista', '$Precio', '$Duracion_Estimada', '$Categoria', '$Turno', '$Cod_Historial')";

// Ejecutando la consulta
$conex->query($sql);

// Redirigir al index después de realizar la operación
header('location: index.html');

?>
