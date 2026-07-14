<?php include("../../bd.php");

if ($_POST) {
    // Recolectar los datos del método POST
    $usuario = (isset($_POST["usuario"])) ? $_POST["usuario"] : "";  
    $password = (isset($_POST["password"])) ? $_POST["password"] : "";
    $correo = (isset($_POST["email"])) ? $_POST["email"] : "";

    // Cifrar la contraseña con password_hash
    $hashed_password = password_hash($password, PASSWORD_DEFAULT);

    // Preparar la inserción de los datos
    $sentencia = $conexion->prepare("INSERT INTO usuarios(id, usuario, password, correo) 
                                     VALUES (null, :usuario, :password, :correo)");
    // Asignamos los valores que tienen uso de :variable
    $sentencia->bindParam(":usuario", $usuario);  
    $sentencia->bindParam(":password", $hashed_password); 
    $sentencia->bindParam(":correo", $correo);  
    $sentencia->execute();

    $mensaje = "Usuario agregado";
    // Redirección al archivo index.php
    header("Location:index.php?mensaje=" . $mensaje);
}
?>

<?php include("../../templates/header.php"); ?>
<br>
<div class="card">
    <div class="card-header">Datos del Usuario</div>
    <div class="card-body">
        <form action=" " method="post" enctype="multipart/form-data">
            <div class="mb-3">
                <input type="text" class="form-control" name="usuario" id="usuario" 
                    aria-describedby="helpid" placeholder="Nombre de Usuario">  
                <small id="helpid" class="form-text text-muted">Ingrese el nombre de usuario</small>
            </div>
            <div class="mb-3">
                <input type="password" class="form-control" name="password" id="password" 
                    aria-describedby="helpid" placeholder="Contraseña">
                <small id="helpid" class="form-text text-muted">Ingrese la contraseña del usuario</small>
            </div>
            <div class="mb-3">
                <input type="email" class="form-control" name="email" id="email" 
                    aria-describedby="helpid" placeholder="Correo Electrónico">
                <small id="helpid" class="form-text text-muted">Ingrese el correo electrónico del usuario</small>
            </div>
            <button type="submit" class="btn btn-outline-success">Agregar Usuario</button>
            <a name="" id="" class="btn btn-outline-primary" href="index.php" role="button">Cancelar</a>
        </form>
    </div>
</div>
<br>

<?php include("../../templates/footer.php"); ?>
