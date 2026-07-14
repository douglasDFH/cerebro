<?php 
include("../../bd.php");

// Verificar si el ID de usuario es recibido por GET
if (isset($_GET['txtID'])) {
    $txtID = $_GET['txtID'];

    // Consulta para obtener los datos del usuario
    $sentencia = $conexion->prepare("SELECT * FROM usuarios WHERE id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    $usuario = $sentencia->fetch(PDO::FETCH_ASSOC);

    // Si no existe el usuario, redirigir al listado
    if (!$usuario) {
        header("Location:index.php?mensaje=Usuario no encontrado");
        exit();
    }

    // Variables para llenar el formulario con los datos del usuario
    $id = $usuario['id'];
    $usuario_name = $usuario['usuario'];
    $password = $usuario['password'];  
    $correo = $usuario['correo'];

    // Procesar el formulario de actualización
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        // Obtener los datos del formulario
        $usuario_name = $_POST['usuario'];
        $correo = $_POST['correo'];

        // Si el password se está cambiando
        if (!empty($_POST['password'])) {
            
            $password = password_hash($_POST['password'], PASSWORD_DEFAULT);
        }

        // Actualizar los datos del usuario
        $sentencia_update = $conexion->prepare("UPDATE usuarios SET usuario = :usuario, password = :password, correo = :correo WHERE id = :id");
        $sentencia_update->bindParam(":usuario", $usuario_name);
        $sentencia_update->bindParam(":password", $password);
        $sentencia_update->bindParam(":correo", $correo);
        $sentencia_update->bindParam(":id", $id);
        $sentencia_update->execute();

        // Redirigir con un mensaje de éxito
        $mensaje = "Usuario actualizado correctamente";
        header("Location:index.php?mensaje=" . $mensaje);
        exit();
    }
}
?>

<?php include("../../templates/header.php"); ?>

<h2>Editar Usuario</h2>
<div class="card">
    <div class="card-header">Datos del Usuario</div>
    <div class="card-body">
        <form action="" method="post">
            <div class="mb-3">
                <label for="usuario" class="form-label">Usuario:</label>
                <input type="text" value="<?php echo $usuario_name; ?>"
                    class="form-control" name="usuario" id="usuario" 
                    aria-describedby="helpId" placeholder="Usuario">
                <small id="helpId" class="form-text text-muted">Ingrese el nombre de usuario</small>
            </div>
            <div class="mb-3">
                <label for="password" class="form-label">Contraseña:</label>
                <input type="password" value="<?php echo $password; ?>"
                    class="form-control" name="password" id="password" 
                    aria-describedby="helpId" placeholder="Contraseña (dejar en blanco para no cambiar)">
                <small id="helpId" class="form-text text-muted">Ingrese una nueva contraseña o deje en blanco para mantener la actual</small>
            </div>
            <div class="mb-3">
                <label for="correo" class="form-label">Correo:</label>
                <input type="email" value="<?php echo $correo; ?>"
                    class="form-control" name="correo" id="correo" 
                    aria-describedby="helpId" placeholder="Correo">
                <small id="helpId" class="form-text text-muted">Ingrese el correo del usuario</small>
            </div>
            <button type="submit" class="btn btn-outline-success">Actualizar registro</button>
            <a name="" id="" class="btn btn-outline-primary" href="index.php" role="button">Cancelar</a>
        </form>
    </div>
</div>

<?php include("../../templates/footer.php"); ?>
