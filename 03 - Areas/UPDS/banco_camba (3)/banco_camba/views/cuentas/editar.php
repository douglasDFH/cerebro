<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleEditar.css">
<div class="client-card edit-mode">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['edit_account']) ? $lang['edit_account'] : 'Editar Cuenta'; ?></h2>
        <div class="edit-badge">
            <span class="edit-icon">✏️</span>
            <span class="edit-text"><?php echo isset($lang['editing_mode']) ? $lang['editing_mode'] : 'Modo Edición'; ?></span>
        </div>
    </div>
    <div class="card-body">
        <div class="client-info-summary">
            <div class="client-avatar">
                <i class="fas fa-credit-card"></i>
            </div>
            <div class="client-summary">
                <h3><?php echo $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno; ?></h3>
                <p class="client-id"><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?>: <?php echo $this->model->nroCuenta; ?></p>
            </div>
        </div>
        
        <form id="form-cuenta" method="POST" action="index.php?controller=cuenta&action=actualizar" class="needs-validation" novalidate>
            <!-- Campo oculto para el ID de la cuenta -->
            <input type="hidden" name="idCuenta" value="<?php echo $this->model->idCuenta; ?>">
            
            <div class="form-grid">
                <!-- Columna izquierda -->
                <div class="form-column">
                    <!-- Información del cliente (solo lectura) -->
                    <div class="form-group">
                        <label for="cliente" class="form-label"><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></label>
                        <input type="text" id="cliente" class="form-control" value="<?php echo $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno; ?>" readonly>
                    </div>
                    
                    <!-- Número de cuenta (solo lectura) -->
                    <div class="form-group">
                        <label for="nroCuenta" class="form-label"><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></label>
                        <input type="text" id="nroCuenta" class="form-control" value="<?php echo $this->model->nroCuenta; ?>" readonly>
                    </div>
                    
                    <!-- Tipo de moneda (solo lectura) -->
                    <div class="form-group">
                        <label for="tipoMoneda" class="form-label"><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?></label>
                        <input type="text" id="tipoMoneda" class="form-control" value="<?php echo $this->model->tipoMoneda == 1 ? (isset($lang['bolivianos']) ? $lang['bolivianos'] : 'Bolivianos') : (isset($lang['dollars']) ? $lang['dollars'] : 'Dólares'); ?>" readonly>
                    </div>
                </div>
                
                <!-- Columna derecha -->
                <div class="form-column">
                    <!-- Saldo (solo lectura) -->
                    <div class="form-group">
                        <label for="saldo" class="form-label"><?php echo isset($lang['balance']) ? $lang['balance'] : 'Saldo'; ?></label>
                        <input type="text" id="saldo" class="form-control" value="<?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->saldo, 2); ?>" readonly>
                    </div>
                    
                    <!-- Fecha de apertura (solo lectura) -->
                    <div class="form-group">
                        <label for="fechaApertura" class="form-label"><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Apertura'; ?></label>
                        <input type="text" id="fechaApertura" class="form-control" value="<?php echo date('d/m/Y', strtotime($this->model->fechaApertura)); ?>" readonly>
                    </div>
                    
                    <!-- Campo: Tipo de Cuenta (editable) -->
                    <div class="form-group">
                        <label for="tipoCuenta" class="form-label"><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?><span class="required">*</span></label>
                        <select id="tipoCuenta" name="tipoCuenta" class="form-control" required>
                            <option value="1" <?php echo $this->model->tipoCuenta == 1 ? 'selected' : ''; ?>><?php echo isset($lang['savings_account']) ? $lang['savings_account'] : 'Cuenta de Ahorro'; ?></option>
                            <option value="2" <?php echo $this->model->tipoCuenta == 2 ? 'selected' : ''; ?>><?php echo isset($lang['checking_account']) ? $lang['checking_account'] : 'Cuenta Corriente'; ?></option>
                        </select>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['account_type_required']) ? $lang['account_type_required'] : 'El tipo de cuenta es requerido'; ?>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Campo: Estado (editable) - A ancho completo -->
            <div class="form-group full-width">
                <label for="estado" class="form-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?><span class="required">*</span></label>
                <select id="estado" name="estado" class="form-control" required>
                    <option value="1" <?php echo $this->model->estado == 1 ? 'selected' : ''; ?>><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></option>
                    <option value="2" <?php echo $this->model->estado == 2 ? 'selected' : ''; ?>><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></option>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['status_required']) ? $lang['status_required'] : 'El estado es requerido'; ?>
                </div>
            </div>
            
            <div class="form-actions">
                <button type="submit" class="btn btn-primary">
                    <i class="fas fa-save"></i> <?php echo isset($lang['save']) ? $lang['save'] : 'Guardar'; ?>
                </button>
                <a href="index.php?controller=cuenta&action=ver&id=<?php echo $this->model->idCuenta; ?>" class="btn btn-secondary">
                    <i class="fas fa-times"></i> <?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?>
                </a>
            </div>
        </form>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form-cuenta');
    
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
        // No aplicar efectos a campos de solo lectura
        if (input.hasAttribute('readonly')) {
            input.classList.add('read-only');
            return;
        }
        
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
        // Solo detectar cambios en campos editables
        if (!input.hasAttribute('readonly')) {
            input.addEventListener('change', () => {
                formModified = true;
            });
        }
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