<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleEditar.css">

<!-- Barra de título principal -->

<div class="client-card edit-mode">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['edit_office']) ? $lang['edit_office'] : 'Editar Oficina'; ?></h2>
        <div class="edit-badge">
            <span class="edit-icon">✏️</span>
            <span class="edit-text"><?php echo isset($lang['editing_mode']) ? $lang['editing_mode'] : 'Modo Edición'; ?></span>
        </div>
    </div>
    <div class="card-body">
        <div class="client-info-summary">
            <div class="client-avatar">
                <i class="fas fa-building"></i>
            </div>
            <div class="client-summary">
                <h3><?php echo $this->model->nombre; ?></h3>
                <p class="client-id"><?php echo isset($lang['city']) ? $lang['city'] : 'Ciudad'; ?>: <?php echo $this->model->ciudad; ?></p>
            </div>
        </div>
        
        <form id="form-oficina" method="POST" action="index.php?controller=oficina&action=actualizar" class="needs-validation" novalidate>
            <!-- Campo oculto para el ID de la oficina -->
            <input type="hidden" name="idOficina" value="<?php echo $this->model->idOficina; ?>">
            <input type="hidden" name="hash" value="<?php echo $this->model->hash; ?>">
            
            <div class="form-grid">
                <!-- Columna izquierda -->
                <div class="form-column">
                    <!-- Campo: Nombre -->
                    <div class="form-group">
                        <label for="nombre" class="form-label"><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?><span class="required">*</span></label>
                        <input type="text" id="nombre" name="nombre" class="form-control" value="<?php echo $this->model->nombre; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['name_required']) ? $lang['name_required'] : 'El nombre es requerido'; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Dirección -->
                    <div class="form-group">
                        <label for="direccion" class="form-label"><?php echo isset($lang['address']) ? $lang['address'] : 'Dirección'; ?><span class="required">*</span></label>
                        <input type="text" id="direccion" name="direccion" class="form-control" value="<?php echo $this->model->direccion; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['address_required']) ? $lang['address_required'] : 'La dirección es requerida'; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Ciudad -->
                    <div class="form-group">
                        <label for="ciudad" class="form-label"><?php echo isset($lang['city']) ? $lang['city'] : 'Ciudad'; ?><span class="required">*</span></label>
                        <input type="text" id="ciudad" name="ciudad" class="form-control" value="<?php echo $this->model->ciudad; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['city_required']) ? $lang['city_required'] : 'La ciudad es requerida'; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: Departamento -->
                    <div class="form-group">
                        <label for="departamento" class="form-label"><?php echo isset($lang['state']) ? $lang['state'] : 'Departamento'; ?><span class="required">*</span></label>
                        <input type="text" id="departamento" name="departamento" class="form-control" value="<?php echo $this->model->departamento; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['state_required']) ? $lang['state_required'] : 'El departamento es requerido'; ?>
                        </div>
                    </div>
                    
                    <!-- Campo: País -->
                    <div class="form-group">
                        <label for="pais" class="form-label"><?php echo isset($lang['country']) ? $lang['country'] : 'País'; ?><span class="required">*</span></label>
                        <input type="text" id="pais" name="pais" class="form-control" value="<?php echo $this->model->pais; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['country_required']) ? $lang['country_required'] : 'El país es requerido'; ?>
                        </div>
                    </div>
                </div>
                
                <!-- Columna derecha -->
                <div class="form-column">
                    <!-- Campo: Teléfono -->
                    <div class="form-group">
                        <label for="telefono" class="form-label"><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></label>
                        <input type="text" id="telefono" name="telefono" class="form-control" value="<?php echo $this->model->telefono; ?>">
                    </div>
                    
                    <!-- Campo: Horario de Atención -->
                    <div class="form-group">
                        <label for="horarioAtencion" class="form-label"><?php echo isset($lang['office_hours']) ? $lang['office_hours'] : 'Horario de Atención'; ?></label>
                        <input type="text" id="horarioAtencion" name="horarioAtencion" class="form-control" value="<?php echo $this->model->horarioAtencion; ?>" placeholder="<?php echo isset($lang['office_hours_placeholder']) ? $lang['office_hours_placeholder'] : 'Ej: Lunes a Viernes 8:00 - 16:00'; ?>">
                    </div>
                    
                    <!-- Campo: Gerente Encargado -->
                    <div class="form-group">
                        <label for="gerenteEncargado" class="form-label"><?php echo isset($lang['manager']) ? $lang['manager'] : 'Gerente Encargado'; ?></label>
                        <input type="text" id="gerenteEncargado" name="gerenteEncargado" class="form-control" value="<?php echo $this->model->gerenteEncargado; ?>">
                    </div>
                    
                    <!-- Campo: Fecha de Inauguración -->
                    <div class="form-group">
                        <label for="fechaInauguracion" class="form-label"><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Inauguración'; ?></label>
                        <input type="date" id="fechaInauguracion" name="fechaInauguracion" class="form-control" value="<?php echo $this->model->fechaInauguracion; ?>">
                    </div>
                    
                    <!-- Campo: Tipo de Oficina -->
                    <div class="form-group">
                        <label class="form-label"><?php echo isset($lang['branch_type']) ? $lang['branch_type'] : 'Tipo de Oficina'; ?><span class="required">*</span></label>
                        <div class="radio-options">
                            <div class="radio-option <?php echo $this->model->central == 1 ? 'selected' : ''; ?>">
                                <input class="form-check-input" type="radio" name="central" id="tipoCentral" value="1" <?php echo $this->model->central == 1 ? 'checked' : ''; ?>>
                                <label class="form-check-label" for="tipoCentral">
                                    <?php echo isset($lang['central']) ? $lang['central'] : 'Central'; ?>
                                </label>
                            </div>
                            <div class="radio-option <?php echo $this->model->central == 0 ? 'selected' : ''; ?>">
                                <input class="form-check-input" type="radio" name="central" id="tipoAgencia" value="0" <?php echo $this->model->central == 0 ? 'checked' : ''; ?>>
                                <label class="form-check-label" for="tipoAgencia">
                                    <?php echo isset($lang['branch']) ? $lang['branch'] : 'Sucursal'; ?>
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Campo: Estado - A ancho completo -->
            <div class="form-group full-width">
                <label class="form-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?><span class="required">*</span></label>
                <div class="radio-options status-options">
                    <div class="radio-option status-active <?php echo $this->model->estado == 'activa' ? 'selected' : ''; ?>">
                        <input class="form-check-input" type="radio" name="estado" id="estadoActivo" value="activa" <?php echo $this->model->estado == 'activa' ? 'checked' : ''; ?>>
                        <label class="form-check-label" for="estadoActivo">
                            <i class="fas fa-check-circle"></i> <?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?>
                        </label>
                    </div>
                    <div class="radio-option status-inactive <?php echo $this->model->estado == 'inactiva' ? 'selected' : ''; ?>">
                        <input class="form-check-input" type="radio" name="estado" id="estadoInactivo" value="inactiva" <?php echo $this->model->estado == 'inactiva' ? 'checked' : ''; ?>>
                        <label class="form-check-label" for="estadoInactivo">
                            <i class="fas fa-times-circle"></i> <?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?>
                        </label>
                    </div>
                </div>
            </div>
            
            <div class="form-actions">
                <button type="submit" class="btn btn-primary">
                    <i class="fas fa-save"></i> <?php echo isset($lang['save']) ? $lang['save'] : 'Guardar'; ?>
                </button>
                <a href="index.php?controller=oficina&action=ver&id=<?php echo $this->model->idOficina; ?>" class="btn btn-secondary">
                    <i class="fas fa-times"></i> <?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?>
                </a>
            </div>
        </form>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form-oficina');
    
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
    
    // Control visual para los radio buttons
    const radioInputs = document.querySelectorAll('.form-check-input[type="radio"]');
    radioInputs.forEach(radio => {
        // Marcar el contenedor padre cuando cambia la selección
        radio.addEventListener('change', () => {
            // Obtener todos los radios con el mismo nombre
            const name = radio.getAttribute('name');
            const siblings = document.querySelectorAll(`.form-check-input[name="${name}"]`);
            
            // Quitar clase selected de todos los contenedores hermanos
            siblings.forEach(sibling => {
                sibling.closest('.radio-option').classList.remove('selected');
            });
            
            // Añadir clase selected al contenedor del radio seleccionado
            radio.closest('.radio-option').classList.add('selected');
        });
    });
    
    // Mostrar alerta de confirmación al salir sin guardar cambios
    let formModified = false;
    
    // Detectar cambios en inputs de texto
    inputs.forEach(input => {
        input.addEventListener('change', () => {
            formModified = true;
        });
    });
    
    // Detectar cambios en radios
    radioInputs.forEach(radio => {
        radio.addEventListener('change', () => {
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