<?php

require_once "conexion.php"; // Incluye la conexión a la base de datos

// Verificar si se ha enviado el formulario para asignar servicios a un trabajador
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $Cod_Trabajador = $_POST['Cod_Trabajador']; // ID del trabajador
    $Cod_Servicios = $_POST['Cod_Servicios']; // ID del servicio

    // Validación para evitar que los campos estén vacíos
    if (empty($Cod_Trabajador) || empty($Cod_Servicios)) {
        echo "Debe seleccionar un trabajador y un servicio.";
        exit;
    }

    // Escapar los valores para evitar inyección SQL
    $Cod_Trabajador = $conex->real_escape_string($Cod_Trabajador);
    $Cod_Servicios = $conex->real_escape_string($Cod_Servicios);

    // Insertar el servicio asignado al trabajador
    $sql = "INSERT INTO Trabajadores_Servicios (Cod_Trabajador, Cod_Servicios) 
            VALUES ('$Cod_Trabajador', '$Cod_Servicios')";

    if ($conex->query($sql) === TRUE) {
        echo "Servicio asignado correctamente al trabajador.";
    } else {
        echo "Error al asignar el servicio: " . $conex->error;
    }
}

// Si deseas obtener los trabajadores y servicios para mostrarlos en otro momento o para hacer más operaciones, puedes hacerlo con estas consultas:
$sql_trabajadores = "SELECT Cod_Trabajador, Nombre_Trabajador FROM Trabajadores";
$result_trabajadores = $conex->query($sql_trabajadores);

$sql_servicios = "SELECT Cod_Servicios, Nombre_Servicio FROM Servicios";
$result_servicios = $conex->query($sql_servicios);

// Aquí puedes trabajar con los resultados de las consultas o seguir con cualquier otra lógica de tu aplicación.

?>
