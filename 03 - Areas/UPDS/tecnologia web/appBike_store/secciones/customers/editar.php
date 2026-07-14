




<?php include("../../templates/header.php");?>
<h2>Editar cliente</h2>
<div class="card">
    <div class="card-header">Datos del cliente</div>
    <div class="card-body">
        <form action="" method="post" enctype="multipart/form-data">
            <div class="mb-3">
                <label for="txtID" class="form-label">ID:</label>
                <input type="text" value="<?php echo $txtID; ?>"
                    class="form-control" readonly name="txtID" id="txtID" 
                    aria-describedby="helpId" placeholder="ID">
            </div>
            <div class="mb-3">
                <label for="first_name" class="form-label">Nombres:</label>
                <input type="text" value="<?php echo $first_name; ?>"
                    class="form-control" name="first_name" id="first_name" 
                    aria-describedby="helpId" placeholder="Nombres">
                <small id="helpId" class="form-text text-muted">Ingrese los nombres de cliente</small>
            </div>
            <div class="mb-3">
                <label for="last_name" class="form-label">Apellidos:</label>
                <input type="text" value="<?php echo $last_name; ?>"
                    class="form-control" name="last_name" id="last_name" 
                    aria-describedby="helpId" placeholder="Apellidos">
                <small id="helpId" class="form-text text-muted">Ingrese los apellidos de cliente</small>
            </div>
            <div class="mb-3">
                <label for="phone" class="form-label">Telefonos:</label>
                <input type="text" value="<?php echo $phone; ?>"
                    class="form-control" name="phone" id="phone" 
                    aria-describedby="helpId" placeholder="Telefonos">
                <small id="helpId" class="form-text text-muted">Ingrese los telefonos de cliente</small>
            </div>
            <div class="mb-3">
                <label for="email" class="form-label">Correo:</label>
                <input type="email" value="<?php echo $email; ?>"
                    class="form-control" name="email" id="email" 
                    aria-describedby="helpId" placeholder="Correos">
                <small id="helpId" class="form-text text-muted">Ingrese los correos de cliente</small>
            </div>
            <div class="mb-3">
                <label for="street" class="form-label">Calle:</label>
                <input type="text" value="<?php echo $street; ?>"
                    class="form-control" name="street" id="street" 
                    aria-describedby="helpId" placeholder="Calle">
                <small id="helpId" class="form-text text-muted">Ingrese la calle del cliente</small>
            </div>
            <div class="mb-3">
                <label for="city" class="form-label">Ciudad:</label>
                <select class="form-select form-select-sm" name="city" id="city">
                    <?php foreach($lista_city as $registro) { ?>
                        <option <?php echo ($customer_id==$registro['customer_id'])?"selected":
                        "";?>value="<?php echo $registro['customer_id']?>">
                        <?php echo $registro['city']?>   
                        </option>
                    <?php } ?>
                </select>
            </div>
            <button type="submit" class="btn btn-outline-success">Actualizar regsitro</button>
            <a name="" id="" class="btn btn-outline-primary" href="index.php" role="button">Cancelar</a>
            
        </form>
    </div>
    
</div>
<br>




<?php include("../../templates/footer.php");?>