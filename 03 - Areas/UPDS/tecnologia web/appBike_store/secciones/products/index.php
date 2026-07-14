<?php 
include("../../bd.php");

// Enviar parámetros por URL y con el método GET para eliminar un producto
if(isset($_GET['txtID'])){
    $txtID = (isset($_GET['txtID'])) ? $_GET['txtID'] : "";
    
    // Eliminar el producto de la base de datos
    $sentencia = $conexion->prepare("DELETE FROM products WHERE product_id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    
    $mensaje = "Producto eliminado";
    header("Location:index.php?mensaje=" . $mensaje);
}

// Consulta para obtener los productos
$sentencia = $conexion->prepare("SELECT * FROM products");
$sentencia->execute();
$lista_products = $sentencia->fetchAll(PDO::FETCH_ASSOC);
?>

<?php include("../../templates/header.php"); ?>

<h2>Lista de Productos</h2>
<div class="card">
    <div class="card-header">
        <a href="crear.php" class="btn btn-outline-primary" role="button">Nuevo Producto</a>
    </div>
    <div class="card-body">
        <div class="table-responsive-sm">
            <table class="table table-primary" id="tabla_id">
                <thead>
                    <tr>
                        <th scope="col">ID</th>
                        <th scope="col">Nombre del Producto</th>
                        <th scope="col">Foto</th>
                        <th scope="col">Año de Modelo</th>
                        <th scope="col">Precio</th>
                        <th scope="col">Acciones</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach($lista_products as $registro) { ?>
                    <tr>
                        <td><?php echo $registro['product_id']; ?></td>
                        <td><?php echo $registro['product_name']; ?></td>
                        <td><img src="<?php echo $registro['foto']; ?>" alt="Imagen del Producto" width="50" height="50"></td>
                        <td><?php echo $registro['model_year']; ?></td>
                        <td><?php echo "bs." . number_format($registro['price'], 2); ?></td>
                        <td>
                            <a class="btn btn-outline-primary" href="editar.php?txtID=<?php echo $registro['product_id']; ?>" role="button">Editar</a>
                            <a class="btn btn-outline-danger" href="index.php?txtID=<?php echo $registro['product_id']; ?>" role="button" onclick="return confirm('¿Estás seguro de eliminar este producto?');">Eliminar</a>
                        </td>
                    </tr>
                    <?php } ?>
                </tbody>
            </table>
        </div>       
    </div>
</div>

<?php include("../../templates/footer.php"); ?>
