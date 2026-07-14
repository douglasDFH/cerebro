<?php

// Conexión a la base de datos
require_once "../conexion.php";

// Recibiendo los valores del formulario
$Nombre = $_POST['Nombre'];                      // Nombre del trabajador
$Apellido = $_POST['Apellido'];                  // Apellido del trabajador
$CI = $_POST['CI'];                              // Cédula de Identidad
$Fecha_Contratacion = $_POST['Fecha_Contratacion']; // Fecha de contratación
$Fecha_Nacimiento = $_POST['Fecha_Nacimiento'];   // Fecha de nacimiento
$Edad = $_POST['Edad'];                          // Edad del trabajador
$Direccion = $_POST['Direccion'];                // Dirección del trabajador
$Genero = $_POST['Género'];                      // Género del trabajador
$Sueldo = $_POST['Salario'];                     // Sueldo del trabajador
$Turnos = $_POST['Turnos'];                      // Turnos asignados
$Religion = $_POST['Religion'];                  // Religión del trabajador
$Cargo = $_POST['Cargo'];                        // Cargo del trabajador
$Estado_Civil = $_POST['Estado_Civil'];          // Estado civil del trabajador
$Telefono = $_POST['Telefonos'];                  // Teléfono del trabajador (puedes manejar como un array si es necesario)
$Email = $_POST['Emails'];                        // Email del trabajador (puedes manejar como un array si es necesario)

// Insertar datos en la tabla Trabajadores
$sql = "INSERT INTO Trabajadores 
        (Nombre, Apellido, CI, Fecha_Contratacion, Fecha_Nacimiento, Edad, Direccion, Genero, Sueldo, Turnos, Religion, Cargo, Estado_Civil) 
        VALUES 
        ('$Nombre', '$Apellido', '$CI', '$Fecha_Contratacion', '$Fecha_Nacimiento', '$Edad', '$Direccion', '$Genero', '$Sueldo', '$Turnos', '$Religion', '$Cargo', '$Estado_Civil')";

if ($conex->query($sql) === TRUE) {
    
    // Obtener el Cod_Trabajador del trabajador recién insertado
    $Cod_Trabajador = $conex->insert_id;

    // Insertar teléfono del trabajador
    foreach ($Telefono as $tel) {
        $sqlTelefono = "INSERT INTO Telefono_Trabajadores (Cod_Trabajador, Telefono) 
                        VALUES ('$Cod_Trabajador', '$tel')";
        $conex->query($sqlTelefono);
    }

    // Insertar email del trabajador
    foreach ($Email as $email) {
        $sqlEmail = "INSERT INTO Email_Trabajadores (Cod_Trabajador, Email) 
                     VALUES ('$Cod_Trabajador', '$email')";
        $conex->query($sqlEmail);
    }

    // Redirigir al index después de la operación
    header('Location: ../Formularios_html/FORM_Trabajadores.html');
    exit;
} else {
    echo "Error: " . $sql . "<br>" . $conex->error;
}

$conex->close();

?>