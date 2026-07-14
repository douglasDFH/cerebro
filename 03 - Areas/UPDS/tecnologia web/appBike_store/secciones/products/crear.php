<?php 
include("../../bd.php");

if ($_POST) {
    // Recolectar los datos del método POST
    $product_name = (isset($_POST["product_name"]) ? $_POST["product_name"] : "");
    $foto = (isset($_POST["foto"]) ? $_POST["foto"] : ""); // URL de la foto
    $model_year = (isset($_POST["model_year"]) ? $_POST["model_year"] : "");
    $price = (isset($_POST["price"]) ? $_POST["price"] : "");

    // Preparar la inserción de los datos en la tabla 'products'
    $sentencia = $conexion->prepare("INSERT INTO products (product_id, product_name, foto, model_year, price) 
                                    VALUES (null, :product_name, :foto, :model_year, :price)");
    
    // Asignamos los valores que tienen uso de :variable
    $sentencia->bindParam(":product_name", $product_name);
    $sentencia->bindParam(":foto", $foto);
    $sentencia->bindParam(":model_year", $model_year);
    $sentencia->bindParam(":price", $price);

    // Ejecutar la sentencia
    $sentencia->execute();

    $mensaje = "Producto agregado";
    
    // Redireccionar al archivo index.php con el mensaje
    header("Location:index.php?mensaje=".$mensaje);
}
?>

<?php include("../../templates/header.php"); ?>
<br>
<div class="card">
    <div class="card-header">Datos del Producto</div>
    <div class="card-body">
        <form action=" " method="post" enctype="multipart/form-data">
            <div class="mb-3">
                <label for="product_name" class="form-label">Nombre del Producto</label>
                <input type="text" class="form-control" name="product_name" id="product_name" 
                aria-describedby="helpId" placeholder="Nombre del Producto" required>
                <small id="helpId" class="form-text text-muted">Ingrese el nombre del producto</small>
            </div>
            <div class="mb-3">
                <label for="foto" class="form-label">Foto (URL)</label>
                <input type="text" class="form-control" name="foto" id="foto" 
                aria-describedby="helpId" placeholder="URL de la foto" required>
                <small id="helpId" class="form-text text-muted">Ingrese la URL de la foto del producto</small>
            </div>
            <div class="mb-3">
                <label for="model_year" class="form-label">Año de Modelo</label>
                <input type="number" class="form-control" name="model_year" id="model_year" 
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
            <button type="submit" class="btn btn-outline-success">Agregar Producto</button>
            <a href="index.php" class="btn btn-outline-primary" role="button">Cancelar</a>
        </form>
    </div>
</div>
<br>

<?php include("../../templates/footer.php"); ?> 
