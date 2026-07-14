<?php 
include("../../bd.php");

// Verificar si se ha recibido el parámetro 'txtID' por URL (es decir, el ID del producto que se quiere editar)
if(isset($_GET['txtID'])){
    $txtID = $_GET['txtID'];
    
    // Consultar los datos del producto
    $sentencia = $conexion->prepare("SELECT * FROM products WHERE product_id = :id");
    $sentencia->bindParam(":id", $txtID);
    $sentencia->execute();
    $producto = $sentencia->fetch(PDO::FETCH_ASSOC);

    // Si el producto no existe, redirigir al listado de productos
    if (!$producto) {
        header("Location:index.php?mensaje=Producto no encontrado");
        exit();
    }

    // Obtener los datos del producto para el formulario
    $product_name = $producto['product_name'];
    $foto = $producto['foto'];
    $model_year = $producto['model_year'];
    $price = $producto['price'];
}

// Actualizar el producto cuando el formulario se envíe
if ($_POST) {
    // Recolectar los datos del formulario
    $product_name = $_POST['product_name'];
    $foto = $_POST['foto'];  // URL de la foto
    $model_year = $_POST['model_year'];
    $price = $_POST['price'];

    // Preparar la consulta SQL para actualizar el producto
    $sentencia = $conexion->prepare("UPDATE products SET product_name = :product_name, foto = :foto, 
                                    model_year = :model_year, price = :price WHERE product_id = :id");
    $sentencia->bindParam(":product_name", $product_name);
    $sentencia->bindParam(":foto", $foto);
    $sentencia->bindParam(":model_year", $model_year);
    $sentencia->bindParam(":price", $price);
    $sentencia->bindParam(":id", $txtID);

    // Ejecutar la consulta de actualización
    $sentencia->execute();

    $mensaje = "Producto actualizado";
    header("Location:index.php?mensaje=".$mensaje);
}
?>

<?php include("../../templates/header.php"); ?>

<h2>Editar Producto</h2>
<div class="card">
    <div class="card-header">Datos del Producto</div>
    <div class="card-body">
        <form action="" method="post" enctype="multipart/form-data">
            <div class="mb-3">
                <label for="txtID" class="form-label">ID:</label>
                <input type="text" value="<?php echo $txtID; ?>" class="form-control" readonly name="txtID" id="txtID" 
                    aria-describedby="helpId" placeholder="ID">
            </div>
            <div class="mb-3">
                <label for="product_name" class="form-label">Nombre del Producto:</label>
                <input type="text" value="<?php echo $product_name; ?>" class="form-control" name="product_name" id="product_name" 
                    aria-describedby="helpId" placeholder="Nombre del Producto" required>
                <small id="helpId" class="form-text text-muted">Ingrese el nombre del producto</small>
            </div>
            <div class="mb-3">
                <label for="foto" class="form-label">Foto (URL):</label>
                <input type="text" value="<?php echo $foto; ?>" class="form-control" name="foto" id="foto" 
                    aria-describedby="helpId" placeholder="URL de la foto" required>
                <small id="helpId" class="form-text text-muted">Ingrese la URL de la foto del producto</small>
            </div>
            <div class="mb-3">
                <label for="model_year" class="form-label">Año de Modelo:</label>
                <input type="number" value="<?php echo $model_year; ?>" class="form-control" name="model_year" id="model_year" 
                    aria-describedby="helpId" placeholder="Año de Modelo" required>
                <small id="helpId" class="form-text text-muted">Ingrese el año de modelo del producto</small>
            </div>
            <div class="mb-3">
                <label for="price" class="form-label">Precio:</label>
                <div class="input-group">
                <span class="input-group-text">bs.</span>  
                <input type="number" step="0.01" value="<?php echo $price; ?>" class="form-control" name="price" id="price" 
                    aria-describedby="helpId" placeholder="Precio" required>
                </div>
                <small id="helpId" class="form-text text-muted">Ingrese el precio del producto</small>
            </div>

            <button type="submit" class="btn btn-outline-success">Actualizar Producto</button>
            <a href="index.php" class="btn btn-outline-primary" role="button">Cancelar</a>
        </form>
    </div>
</div>
<br>

<?php include("../../templates/footer.php"); ?> 
