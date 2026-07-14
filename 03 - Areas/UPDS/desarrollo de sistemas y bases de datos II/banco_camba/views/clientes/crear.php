<h1><?php echo $lang['new_client']; ?></h1>

<div class="card">
    <div class="card-header"><?php echo $lang['client_details']; ?></div>
    <div class="card-body">
        <form method="POST" action="index.php?controller=cliente&action=crear" class="needs-validation">
            <div class="row">
                <!-- Datos personales -->
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="nombre" class="form-label"><?php echo $lang['name']; ?> <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="nombre" name="nombre" required>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="apellidoPaterno" class="form-label"><?php echo $lang['paternal_last_name']; ?> <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="apellidoPaterno" name="apellidoPaterno" required>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="apellidoMaterno" class="form-label"><?php echo $lang['maternal_last_name']; ?></label>
                        <input type="text" class="form-control" id="apellidoMaterno" name="apellidoMaterno">
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="ci" class="form-label"><?php echo $lang['id_number']; ?> <span class="text-danger">*</span></label>
                        <input type="text" class="form-control" id="ci" name="ci" required>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="fechaNacimiento" class="form-label"><?php echo $lang['birth_date']; ?></label>
                        <input type="date" class="form-control" id="fechaNacimiento" name="fechaNacimiento">
                    </div>
                </div>
                
                <!-- Datos de contacto -->
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="direccion" class="form-label"><?php echo $lang['address']; ?></label>
                        <input type="text" class="form-control" id="direccion" name="direccion">
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="telefono" class="form-label"><?php echo $lang['phone']; ?></label>
                        <input type="tel" class="form-control" id="telefono" name="telefono" pattern="[0-9]{1,3}-[0-9]{7,8}">
                        <small class="form-text text-muted">Formato: 3-3456789 o 70-1234567</small>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="email" class="form-label"><?php echo $lang['email']; ?></label>
                        <input type="email" class="form-control" id="email" name="email">
                    </div>
                </div>
                
                <!-- Oficina asignada -->
                <div class="col-md-6">
                    <div class="form-group">
                        <label for="idOficina" class="form-label"><?php echo $lang['branches']; ?> <span class="text-danger">*</span></label>
                        <select class="form-control" id="idOficina" name="idOficina" required>
                            <option value=""><?php echo $lang['select_option']; ?></option>
                            <?php foreach ($oficinas as $oficina): ?>
                                <option value="<?php echo $oficina['idOficina']; ?>">
                                    <?php echo $oficina['nombre']; ?>
                                    <?php if ($oficina['central']): ?> 
                                        (<?php echo $lang['central_office']; ?>)
                                    <?php endif; ?>
                                </option>
                            <?php endforeach; ?>
                        </select>
                    </div>
                </div>
            </div>
            
            <div class="form-group mt-3 text-center">
                <button type="submit" class="btn btn-primary"><?php echo $lang['save']; ?></button>
                <a href="index.php?controller=cliente&action=listar" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
            </div>
        </form>
    </div>
</div>

<script>
document.addEventListener('DOMContentLoaded', function() {
    // Validación del formulario
    const form = document.querySelector('.needs-validation');
    form.addEventListener('submit', function(event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        form.classList.add('was-validated');
    }, false);
    
    // Formateo automático del número de teléfono
    const telefonoInput = document.getElementById('telefono');
    if (telefonoInput) {
        telefonoInput.addEventListener('input', function(e) {
            let value = e.target.value.replace(/\D/g, '');
            if (value.length > 0) {
                if (value.length <= 8) {
                    value = value.replace(/^(\d{1,3})(\d{0,})$/, function(match, p1, p2) {
                        return p2 ? p1 + '-' + p2 : p1;
                    });
                } else {
                    value = value.replace(/^(\d{2})(\d{0,})$/, function(match, p1, p2) {
                        return p2 ? p1 + '-' + p2 : p1;
                    });
                }
            }
            e.target.value = value;
        });
    }
});
</script>