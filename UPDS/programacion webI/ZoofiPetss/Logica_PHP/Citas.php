<?php

require_once "conexion.php";

// Recibiendo los valores de los campos del formulario
$Motivo_Consulta = $_POST['Motivo_Consulta'];               // Motivo de la consulta
$Cobro_Total = $_POST['Cobro_Total'];                       // Cobro total de la cita
$Metodo_Pago = $_POST['Metodo_Pago'];                       // Método de pago
$Fecha_Cita = $_POST['Fecha_Cita'];                         // Fecha de la cita
$Tratamiento = $_POST['Tratamiento'];                       // Tratamiento prescrito
$Enfermedades_Base = $_POST['Enfermedades_Base'];           // Enfermedades base del paciente
$Alergias = $_POST['Alergias'];                             // Alergias del paciente
$Diagnostico = $_POST['Diagnostico'];                       // Diagnóstico realizado
$Cod_Mascotas = $_POST['Cod_Mascotas'];                     // Código de la mascota
$Cod_Clientes = $_POST['Cod_Clientes'];                     // Código del cliente
$Cod_Trabajador = $_POST['Cod_Trabajador'];                 // Código del trabajador
$Cod_Servicios = $_POST['Cod_Servicios'];                   // Código del servicio
$Cod_Historial = $_POST['Cod_Historial'];                   // Código del historial

// Inserción en la base de datos
$sql = "INSERT INTO Citas 
        (Motivo_Consulta, Cobro_Total, Metodo_Pago, Fecha_Cita, Tratamiento, Enfermedades_Base, Alergias, Diagnostico, Cod_Mascotas, Cod_Clientes, Cod_Trabajador, Cod_Servicios, Cod_Historial)
        VALUES 
        ('$Motivo_Consulta', '$Cobro_Total', '$Metodo_Pago', '$Fecha_Cita', '$Tratamiento', '$Enfermedades_Base', '$Alergias', '$Diagnostico', '$Cod_Mascotas', '$Cod_Clientes', '$Cod_Trabajador', '$Cod_Servicios', '$Cod_Historial')";

$conex->query($sql);

// Redirigir al index después de realizar la operación
header('location: index.html');

?>
