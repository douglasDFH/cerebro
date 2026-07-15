<?php  
require_once "../conexion.php";


try {
    // Obtener y sanitizar entradas
    $nombreUsuario = filter_var($_POST['Nombre_Usuario'], FILTER_SANITIZE_STRING);
    $correo = filter_var($_POST['Email'], FILTER_SANITIZE_EMAIL);
    $contrasena = $_POST['Contraseña'];
    $confirmarContrasena = $_POST['Confirmar_Contraseña'];
    $estado = $_POST['Estado'];
    $fechaRegistro = $_POST['Fecha_Registro'];
    $telefonos = $_POST['Telefonos']; // Array de teléfonos
    $emails = $_POST['Emails']; // Array de emails

    // Validar formato de correo electrónico
    if (!filter_var($correo, FILTER_VALIDATE_EMAIL)) {
        throw new Exception('Formato de correo inválido');
    }
    
    // Validar si las contraseñas coinciden
    if ($contrasena !== $confirmarContrasena) {
        throw new Exception('Las contraseñas no coinciden');
    }
    
    // Validar longitud de la contraseña
    if (strlen($contrasena) < 8) {
        throw new Exception('La contraseña debe tener al menos 8 caracteres');
    }

    // Comprobar si el correo ya existe
    $stmt = $conex->prepare("SELECT Cod_Usuario FROM Usuario WHERE Correo_Institucional = ?");
    $stmt->bind_param("s", $correo);
    $stmt->execute();
    if ($stmt->get_result()->num_rows > 0) {
        throw new Exception('El correo ya está registrado');
    }


    // Insertar usuario con la sentencia preparada
$stmt = $conex->prepare("INSERT INTO Usuario (
    Nombre_Usuario, 
    Correo_Institucional, 
    Contrasena_Institucional, 
    Estado_Usuario, 
    Fecha_Creacion
) VALUES (?, ?, ?, ?, ?)");  // 5 placeholders para 5 columnas

$hashedPassword = password_hash($contrasena, PASSWORD_BCRYPT);
$stmt->bind_param("sssss",  // 5 parámetros
    $nombreUsuario, 
    $correo, 
    $hashedPassword, 
    $estado, 
    $fechaRegistro
);
    if (!$stmt->execute()) {
        throw new Exception('Error al registrar usuario');
    }

    // Obtener el ID del usuario recién insertado
    $codUsuario = $stmt->insert_id;

    // Insertar los correos electrónicos asociados al usuario
    foreach ($emails as $email) {
        $email = filter_var($email, FILTER_SANITIZE_EMAIL);
        if (filter_var($email, FILTER_VALIDATE_EMAIL)) {
            $stmt = $conex->prepare("INSERT INTO Emails_Usuario (Cod_Usuario, Email) VALUES (?, ?)");
            $stmt->bind_param("is", $codUsuario, $email);
            if (!$stmt->execute()) {
                throw new Exception('Error al registrar email');
            }
        }
    }

    // Insertar los teléfonos asociados al usuario
    foreach ($telefonos as $telefono) {
        $telefono = filter_var($telefono, FILTER_SANITIZE_STRING);
        $stmt = $conex->prepare("INSERT INTO Telefonos_Usuario (Cod_Usuario, Telefono) VALUES (?, ?)");
        $stmt->bind_param("is", $codUsuario, $telefono);
        if (!$stmt->execute()) {
            throw new Exception('Error al registrar teléfono');
        }
    }

    echo json_encode(['success' => true, 'message' => 'Usuario registrado exitosamente']);

} catch (Exception $e) {
    http_response_code(400);
    echo json_encode(['success' => false, 'message' => $e->getMessage()]);
}
