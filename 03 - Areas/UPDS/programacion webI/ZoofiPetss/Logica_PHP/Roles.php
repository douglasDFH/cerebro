<?php

// Incluir el archivo de conexión a la base de datos
require_once "../conexion.php";

// Recibir los datos del formulario
$Nombre_Rol = $_POST['Nombre_Rol'];  // Nombre del rol

// Insertar los datos en la tabla 'Roles'
$sql = "INSERT INTO Roles (Nombre_Rol) VALUES ('$Nombre_Rol')";

// Ejecutar la consulta
if ($conex->query($sql) === TRUE) {
    // Si la inserción es exitosa, redirigir al usuario a otra página
    header('Location: ../Formularios_html/FORM_Roles.html');  // O la página que desees
    exit();
} else {
    // Si hay un error en la consulta, mostrar un mensaje de error
    echo "Error: " . $sql . "<br>" . $conex->error;
}

// Cerrar la conexión
$conex->close();

?>S
