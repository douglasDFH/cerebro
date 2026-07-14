<?php

require_once "conexion.php"; // Incluye la conexión a la base de datos

// Verificar si se ha enviado el formulario para asignar permisos a un rol
if ($_SERVER["REQUEST_METHOD"] == "POST") {
    $Cod_Roles = $_POST['Cod_Roles']; // ID del rol
    $Cod_Permisos = $_POST['Cod_Permisos']; // ID del permiso

    // Validación para evitar que los campos estén vacíos
    if (empty($Cod_Roles) || empty($Cod_Permisos)) {
        echo "Debe seleccionar un rol y un permiso.";
        exit;
    }

    // Escapar los valores para evitar inyección SQL
    $Cod_Roles = $conex->real_escape_string($Cod_Roles);
    $Cod_Permisos = $conex->real_escape_string($Cod_Permisos);

    // Insertar el permiso asignado al rol
    $sql = "INSERT INTO Roles_Permisos (Cod_Roles, Cod_Permisos) 
            VALUES ('$Cod_Roles', '$Cod_Permisos')";

    if ($conex->query($sql) === TRUE) {
        echo "Permiso asignado correctamente al rol.";
    } else {
        echo "Error al asignar el permiso: " . $conex->error;
    }
}

// Si deseas obtener los roles y permisos para mostrarlos en otro momento o para hacer más operaciones, puedes hacerlo con estas consultas:
$sql_roles = "SELECT Cod_Roles, Nombre_Rol FROM Roles";
$result_roles = $conex->query($sql_roles);

$sql_permisos = "SELECT Cod_Permisos, Nombre_Permiso FROM Permisos";
$result_permisos = $conex->query($sql_permisos);

// Aquí puedes trabajar con los resultados de las consultas o seguir con cualquier otra lógica de tu aplicación.

?>
