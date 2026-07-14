<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleCrear.css">
<div class="client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['new_client']) ? $lang['new_client'] : 'Nuevo Cliente'; ?></h2>
    </div>
    <div class="card-body">
        <form id="form-cliente" method="POST" action="index.php?controller=cliente&action=guardar" class="needs-validation" novalidate>
            <!-- Campo: Nombre -->
            <div class="form-group">
                <label for="nombre" class="form-label"><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?><span class="required">*</span></label>
                <input type="text" id="nombre" name="nombre" class="form-control" required>
                <div class="invalid-feedback">
                    <?php echo isset($lang['name_required']) ? $lang['name_required'] : 'El nombre es requerido'; ?>
                </div>
            </div>
            
            <!-- Campo: Apellido Paterno -->
            <div class="form-group">
                <label for="apellidoPaterno" class="form-label"><?php echo isset($lang['paternal_last_name']) ? $lang['paternal_last_name'] : 'Apellido Paterno'; ?><span class="required">*</span></label>
                <input type="text" id="apellidoPaterno" name="apellidoPaterno" class="form-control" required>
                <div class="invalid-feedback">
                    <?php echo isset($lang['paternal_last_name_required']) ? $lang['paternal_last_name_required'] : 'El apellido paterno es requerido'; ?>
                </div>
            </div>
            
            <!-- Campo: Apellido Materno -->
            <div class="form-group">
                <label for="apellidoMaterno" class="form-label"><?php echo isset($lang['maternal_last_name']) ? $lang['maternal_last_name'] : 'Apellido Materno'; ?></label>
                <input type="text" id="apellidoMaterno" name="apellidoMaterno" class="form-control">
            </div>
            
            <!-- Campo: Dirección -->
            <div class="form-group">
                <label for="direccion" class="form-label"><?php echo isset($lang['address']) ? $lang['address'] : 'Dirección'; ?></label>
                <input type="text" id="direccion" name="direccion" class="form-control">
            </div>
            
            <!-- Campo: Teléfono -->
            <div class="form-group">
                <label for="telefono" class="form-label"><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></label>
                <input type="text" id="telefono" name="telefono" class="form-control">
            </div>
            
            <!-- Campo: Email -->
            <div class="form-group">
                <label for="email" class="form-label"><?php echo isset($lang['email']) ? $lang['email'] : 'Email'; ?></label>
                <input type="email" id="email" name="email" class="form-control">
                <div class="invalid-feedback">
                    <?php echo isset($lang['valid_email_required']) ? $lang['valid_email_required'] : 'Ingrese un email válido'; ?>
                </div>
            </div>
            
            <!-- Campo: Fecha de Nacimiento -->
            <div class="form-group">
                <label for="fechaNacimiento" class="form-label"><?php echo isset($lang['birth_date']) ? $lang['birth_date'] : 'Fecha de Nacimiento'; ?></label>
                <input type="date" id="fechaNacimiento" name="fechaNacimiento" class="form-control">
            </div>
            
            <!-- Campo: CI (Cédula de Identidad) -->
            <div class="form-group">
                <label for="ci" class="form-label"><?php echo isset($lang['id_number']) ? $lang['id_number'] : 'Cédula de Identidad'; ?><span class="required">*</span></label>
                <input type="text" id="ci" name="ci" class="form-control" required>
                <div class="invalid-feedback">
                    <?php echo isset($lang['id_number_required']) ? $lang['id_number_required'] : 'La cédula de identidad es requerida'; ?>
                </div>
            </div>
            
            <!-- Campo: Oficina -->
            <div class="form-group">
                <label for="idOficina" class="form-label"><?php echo isset($lang['branch']) ? $lang['branch'] : 'Oficina'; ?><span class="required">*</span></label>
                <select id="idOficina" name="idOficina" class="form-control" required>
                    <option value=""><?php echo isset($lang['select_option']) ? $lang['select_option'] : 'Seleccione una opción'; ?></option>
                    <?php foreach ($oficinas as $oficina): ?>
                    <option value="<?php echo $oficina['idOficina']; ?>">
                        <?php echo $oficina['nombre']; ?>
                    </option>
                    <?php endforeach; ?>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['branch_required']) ? $lang['branch_required'] : 'La oficina es requerida'; ?>
                </div>
            </div>
            
            <div class="form-actions">
                <button type="submit" class="btn btn-primary"><?php echo isset($lang['save']) ? $lang['save'] : 'Guardar'; ?></button>
                <a href="index.php?controller=cliente&action=listar" class="btn btn-secondary"><?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?></a>
            </div>
        </form>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form-cliente');
    
    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        
        form.classList.add('was-validated');
    });
    
    // Mejorar la experiencia de usuario en los campos
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(input => {
        // Añadir clase cuando el campo recibe el foco
        input.addEventListener('focus', () => {
            input.parentElement.classList.add('field-focus');
        });
        
        // Quitar la clase cuando pierde el foco
        input.addEventListener('blur', () => {
            input.parentElement.classList.remove('field-focus');
        });
    });
});
</script>