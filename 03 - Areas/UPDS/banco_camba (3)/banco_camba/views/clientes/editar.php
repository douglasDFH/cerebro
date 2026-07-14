<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleEditar.css">
<div class="client-card edit-mode">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo $lang['edit_client']; ?></h2>
        <div class="edit-badge">
            <span class="edit-icon">✏️</span>
            <span class="edit-text"><?php echo $lang['editing_mode']; ?></span>
        </div>
    </div>
    <div class="card-body">
        <div class="client-info-summary">
            <div class="client-avatar">
                <i class="fas fa-user-edit"></i>
            </div>
            <div class="client-summary">
                <h3><?php echo $this->model->nombre . ' ' . $this->model->apellidoPaterno; ?></h3>
                <p class="client-id"><?php echo $lang['id_number']; ?>: <?php echo $this->model->ci; ?></p>
            </div>
        </div>
        
        <form id="form-cliente" method="POST" action="index.php?controller=cliente&action=actualizar" class="needs-validation" novalidate>
            <!-- Campo oculto para el ID de la persona -->
            <input type="hidden" name="idPersona" value="<?php echo $this->model->idPersona; ?>">
            
            <div class="form-grid">
                <!-- Columna izquierda -->
                <div class="form-column">
                    <!-- Campo: Nombre -->
                    <div class="form-group">
                        <label for="nombre" class="form-label"><?php echo $lang['name']; ?><span class="required">*</span></label>
                        <input type="text" id="nombre" name="nombre" class="form-control" value="<?php echo $this->model->nombre; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo $lang['name_required']; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Apellido Paterno -->
                    <div class="form-group">
                        <label for="apellidoPaterno" class="form-label"><?php echo $lang['paternal_last_name']; ?><span class="required">*</span></label>
                        <input type="text" id="apellidoPaterno" name="apellidoPaterno" class="form-control" value="<?php echo $this->model->apellidoPaterno; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo $lang['paternal_last_name_required']; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Apellido Materno -->
                    <div class="form-group">
                        <label for="apellidoMaterno" class="form-label"><?php echo $lang['maternal_last_name']; ?></label>
                        <input type="text" id="apellidoMaterno" name="apellidoMaterno" class="form-control" value="<?php echo $this->model->apellidoMaterno; ?>">
                    </div>
                    
                    <!-- Campo: CI (Cédula de Identidad) -->
                    <div class="form-group">
                        <label for="ci" class="form-label"><?php echo $lang['id_number']; ?><span class="required">*</span></label>
                        <input type="text" id="ci" name="ci" class="form-control" value="<?php echo $this->model->ci; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo $lang['id_number_required']; ?>
                        </div>
                    </div>
                </div>
                
                <!-- Columna derecha -->
                <div class="form-column">
                    <!-- Campo: Dirección -->
                    <div class="form-group">
                        <label for="direccion" class="form-label"><?php echo $lang['address']; ?></label>
                        <input type="text" id="direccion" name="direccion" class="form-control" value="<?php echo $this->model->direccion; ?>">
                    </div>
                    
                    <!-- Campo: Teléfono -->
                    <div class="form-group">
                        <label for="telefono" class="form-label"><?php echo $lang['phone']; ?></label>
                        <input type="text" id="telefono" name="telefono" class="form-control" value="<?php echo $this->model->telefono; ?>">
                    </div>
                    
                    <!-- Campo: Email -->
                    <div class="form-group">
                        <label for="email" class="form-label"><?php echo $lang['email']; ?></label>
                        <input type="email" id="email" name="email" class="form-control" value="<?php echo $this->model->email; ?>">
                        <div class="invalid-feedback">
                            <?php echo $lang['valid_email_required']; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Fecha de Nacimiento -->
                    <div class="form-group">
                        <label for="fechaNacimiento" class="form-label"><?php echo $lang['birth_date']; ?></label>
                        <input type="date" id="fechaNacimiento" name="fechaNacimiento" class="form-control" value="<?php echo $this->model->fechaNacimiento; ?>">
                    </div>
                </div>
            </div>
            
            <!-- Campo: Oficina - A ancho completo -->
            <div class="form-group full-width">
                <label for="idOficina" class="form-label"><?php echo $lang['branch']; ?><span class="required">*</span></label>
                <select id="idOficina" name="idOficina" class="form-control" required>
                    <option value=""><?php echo $lang['select_option']; ?></option>
                    <?php foreach ($oficinas as $oficina): ?>
                    <option value="<?php echo $oficina['idOficina']; ?>" <?php echo $oficina['idOficina'] == $this->model->idOficina ? 'selected' : ''; ?>>
                        <?php echo $oficina['nombre']; ?>
                    </option>
                    <?php endforeach; ?>
                </select>
                <div class="invalid-feedback">
                    <?php echo $lang['branch_required']; ?>
                </div>
            </div>

            
            <div class="form-actions">
                <button type="submit" class="btn btn-primary">
                    <i class="fas fa-save"></i> <?php echo $lang['save']; ?>
                </button>
                <a href="index.php?controller=cliente&action=ver&id=<?php echo $this->model->idPersona; ?>" class="btn btn-secondary">
                    <i class="fas fa-times"></i> <?php echo $lang['cancel']; ?>
                </a>
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
            
            // Añadir clase si el campo ha sido modificado
            if (input.value !== input.defaultValue) {
                input.classList.add('field-modified');
                input.parentElement.classList.add('has-changes');
            } else {
                input.classList.remove('field-modified');
                input.parentElement.classList.remove('has-changes');
            }
        });
    });
    
    // Mostrar alerta de confirmación al salir sin guardar cambios
    let formModified = false;
    
    inputs.forEach(input => {
        input.addEventListener('change', () => {
            formModified = true;
        });
    });
    
    window.addEventListener('beforeunload', function(e) {
        if (formModified) {
            e.preventDefault();
            e.returnValue = '';
        }
    });
    
    // Desactivar advertencia al enviar el formulario
    form.addEventListener('submit', () => {
        formModified = false;
    });
    
    // Desactivar advertencia al hacer clic en Cancelar
    document.querySelector('.btn-secondary').addEventListener('click', () => {
        formModified = false;
    });
});
</script>