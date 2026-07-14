
<?php include("../../bd.php");
if($_POST){
    //Recolectar los datos del metodo POST
    $first_name=(isset($_POST["first_name"])?$_POST["first_name"]:"");
    $last_name=(isset($_POST["last_name"])?$_POST["last_name"]:"");
    $phone=(isset($_POST["phone"])?$_POST["phone"]:"");
    $email=(isset($_POST["email"])?$_POST["email"]:"");
    $street=(isset($_POST["street"])?$_POST["street"]:"");
    $city=(isset($_POST["city"])?$_POST["city"]:"");
    $state=(isset($_POST["state"])?$_POST["state"]:"");

    //Preparar la insercion de los datos
    $sentencia=$conexion->prepare("INSERT INTO customers(customer_id,first_name,last_name,
    phone,email,street,city,state) VALUES (null,:first_name,:last_name,
    :phone,:email,:street,:city,:state)");
    //Asignamos los valores que tienen uso de :variable
    $sentencia->bindParam(":first_name",$first_name);
    $sentencia->bindParam(":last_name",$last_name);
    $sentencia->bindParam(":phone",$phone);
    $sentencia->bindParam(":email",$email);
    $sentencia->bindParam(":street",$street);
    $sentencia->bindParam(":city",$city);
    $sentencia->bindParam(":state",$state);
    $sentencia->execute();
    $mensaje="Registro agregado";
    //Redireccion al arcivo index.php
    header("Location:index.php?mensaje=".$mensaje);
}

?>
<?php include("../../templates/header.php");?>
<br>
<div class="card">
    <div class="card-header">Datos del cliente</div>
    <div class="card-body">
        <form action=" " method="post" enctype="multipart/form-data">
            <div class="mb-3">
                <input type="text" class="form-control" name="first_name" id="first_name" 
                aria-describedby="helpid" placeholder="Nombres">
                <small id="helpid" class="form-text text-muted">Ingrese los nombres del cliente</small>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" name="last_name" id="last_name" 
                aria-describedby="helpid" placeholder="Apellidos">
                <small id="helpid" class="form-text text-muted">Ingrese los apellidos del cliente</small>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" name="phone" id="phone" 
                aria-describedby="helpid" placeholder="Telefonos">
                <small id="helpid" class="form-text text-muted">Ingrese el n&uacute;mero de telefono del cliente</small>
            </div>
            <div class="mb-3">
                <input type="email" class="form-control" name="email" id="email" 
                aria-describedby="helpid" placeholder="Correo">
                <small id="helpid" class="form-text text-muted">Ingrese el correo electr&oacute;onico del cliente</small>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" name="street" id="street" 
                aria-describedby="helpid" placeholder="Calle">
                <small id="helpid" class="form-text text-muted">Ingrese el nombre de la calle del cliente</small>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" name="city" id="city" 
                aria-describedby="helpid" placeholder="Ciudad">
                <small id="helpid" class="form-text text-muted">Ingrese el nombre de la ciudad del cliente</small>
            </div>
            <div class="mb-3">
                <input type="text" class="form-control" name="state" id="state" 
                aria-describedby="helpid" placeholder="Departamento">
                <small id="helpid" class="form-text text-muted">Ingrese el departamento del cliente</small>
            </div>
            <button type="submit" class="btn btn-outline-success">Agregar regsitro</button>
            <a name="" id="" class="btn btn-outline-primary" href="index.php" role="button">Cancelar</a>
        </form>

    </div>
</div>
<br>

<?php include("../../templates/footer.php");?>