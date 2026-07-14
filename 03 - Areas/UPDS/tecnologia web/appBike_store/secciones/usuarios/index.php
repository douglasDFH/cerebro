<?php include("../../bd.php");
// Envío de parámetros en la URL y con el método GET
if(isset($_GET['txtID'])){
    $txtID = (isset($_GET['txtID'])) ? $_GET['txtID'] : "";
    
    // Borra los datos del usuario
    $sentencia = $conexion->prepare("DELETE FROM usuarios WHERE id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    
    $mensaje = "Registro eliminado";
    header("Location:index.php?mensaje=" . $mensaje);
}

// Consulta para obtener los datos del usuario
$sentencia = $conexion->prepare("SELECT * FROM usuarios");
$sentencia->execute();
$lista_usuarios = $sentencia->fetchAll(PDO::FETCH_ASSOC);
?>

<?php include("../../templates/header.php"); ?>

<h2>Lista de Usuarios</h2>
<div class="card">
    <div class="card-header">
        <a name="" id="" class="btn btn-outline-primary" href="crear.php" role="button">Nuevo</a>
    </div>
    <div class="card-body">
        <div class="table-responsive-sm">
            <table class="table table-primary" id="tabla_id">
                <thead>
                    <tr>
                        <th scope="col">ID</th>
                        <th scope="col">Usuario</th>
                        <th scope="col">Correo</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach($lista_usuarios as $registro) { ?>
                    <tr>
                        <td scope="row"><?php echo $registro['id']; ?></td>
                        <td><?php echo $registro['usuario']; ?></td>
                        <td><?php echo $registro['correo']; ?></td>
                        <td>
                            <a class="btn btn-outline-primary" href="editar.php?txtID=<?php echo $registro['id']; ?>" role="button">Editar</a>
                            <a class="btn btn-outline-danger" href="index.php?txtID=<?php echo $registro['id']; ?>" role="button" onclick="return confirm('¿Estás seguro de eliminar este usuario?');">Eliminar</a>
                        </td>
                    </tr>
                    <?php } ?>
                </tbody>
            </table>
        </div>       
    </div>
</div>

<?php include("../../templates/footer.php"); ?>
