<?php

require_once "../conexion.php"; // Incluye el archivo de conexión a la base de datos

// Recibiendo los valores de los campos del formulario
$Nombre = $_POST['Nombre'];                // Nombre del cliente
$Apellido = $_POST['Apellido'];            // Apellido del cliente
$Genero = $_POST['Genero'];                // Genero del cliente
$Fecha_Nacimiento = $_POST['Fecha_Nacimiento']; // Fecha de nacimiento del cliente
$Edad = $_POST['Edad'];                    // Edad del cliente
$Direccion = $_POST['Direccion'];          // Dirección del cliente
$CI = $_POST['CI'];                        // Cédula de Identidad del cliente
$Fecha_Registro = $_POST['Fecha_Registro']; // Fecha de registro del cliente
$Preferencias_Contactos = $_POST['Preferencias_Contactos']; // Preferencias de contacto del cliente

// Datos de teléfono y email, que pueden ser múltiples
$Telefonos = $_POST['Telefonos'];          // Arreglo de teléfonos del cliente
$Emails = $_POST['Emails'];                // Arreglo de emails del cliente


// Preparar los datos para pasar de vuelta
$datosCliente = [
    'nombre' => $Nombre,
    'apellido' => $Apellido,
    'genero' => $Genero,
    'fechaNacimiento' => $Fecha_Nacimiento,
    'edad' => $Edad,
    'direccion' => $Direccion,
    'ci' => $CI,
    'fechaRegistro' => $Fecha_Registro,
    'preferenciaContacto' => $Preferencias_Contactos,
    'telefonos' => $Telefonos,
    'emails' => $Emails
];

// Inserción en la tabla Clientes
$sql_cliente = "INSERT INTO Clientes 
    (Nombre, Apellido, Genero, Fecha_Nacimiento, Edad, Direccion, CI, Fecha_Registro, Preferencias_Contactos) 
    VALUES 
    ('$Nombre', '$Apellido', '$Genero', '$Fecha_Nacimiento', '$Edad', '$Direccion', '$CI', '$Fecha_Registro', '$Preferencias_Contactos')";

// Ejecutando la inserción en la tabla Clientes
if ($conex->query($sql_cliente) === TRUE) {
    $Cod_Clientes = $conex->insert_id;  // Obtenemos el ID del cliente recién insertado

    // Inserción en la tabla Telefono_Clientes
    foreach ($Telefonos as $Telefono) {
        $sql_telefono = "INSERT INTO Telefono_Clientes (Cod_Clientes, Telefono) VALUES ('$Cod_Clientes', '$Telefono')";
        $conex->query($sql_telefono);
    }

    // Inserción en la tabla Email_Clientes
    foreach ($Emails as $Email) {
        $sql_email = "INSERT INTO Email_Clientes (Cod_Clientes, Email) VALUES ('$Cod_Clientes', '$Email')";
        $conex->query($sql_email);
    }

    // Convertir los datos a JSON y codificar para URL
    $datosJson = urlencode(json_encode($datosCliente));

    // Redirigir al formulario con los datos
    header("Location: ../Formularios_HTML/FORM_Clientes.html?datos=$datosJson");
    exit;
} else {
    // Si ocurre un error al insertar en la tabla Clientes
    echo "Error: " . $sql_cliente . "<br>" . $conex->error;
}
?>
