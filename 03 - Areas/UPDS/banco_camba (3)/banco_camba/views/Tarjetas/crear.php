<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleCrear.css">

<div class="client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['new_card']) ? $lang['new_card'] : 'Nueva Tarjeta'; ?></h2>
    </div>
    <div class="card-body">
        <form id="form-tarjeta" method="POST" action="index.php?controller=tarjeta&action=guardar" class="needs-validation" novalidate>


            <!-- Campo: Cuenta asociada (selector) -->
            <div class="form-group">
                <label for="idCuenta" class="form-label"><?php echo isset($lang['account']) ? $lang['account'] : 'Cuenta'; ?> <span class="required">*</span></label>
                <input type="text" id="cuenta-display" class="form-control" value="- 1234567903 ($)" name="idCuenta" list="cuentas" required>
                <datalist id="cuentas">
                    <?php foreach ($cuentas as $cuenta) : ?>
                        <option value="<?php echo $cuenta->idCuenta; ?>"><?php echo $cuenta->nroCuenta; ?></option>
                    <?php endforeach; ?>
                </datalist>
            </div>

            <!-- Título de sección -->
            <div class="section-title">
                <h3><?php echo isset($lang['card_information']) ? $lang['card_information'] : 'Información de la Tarjeta'; ?></h3>
            </div>

            <!-- Campo: Tipo de Tarjeta -->
            <div class="form-group">
                <label for="tipoTarjeta" class="form-label"><?php echo isset($lang['type']) ? $lang['type'] : 'Tipo'; ?> <span class="required">*</span></label>
                <select id="tipoTarjeta" name="tipoTarjeta" class="form-control" required>
                    <option value=""><?php echo isset($lang['select_option']) ? $lang['select_option'] : 'Seleccione una opción'; ?></option>
                    <option value="debito"><?php echo isset($lang['debit']) ? $lang['debit'] : 'Débito'; ?></option>
                    <option value="credito"><?php echo isset($lang['credit']) ? $lang['credit'] : 'Crédito'; ?></option>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['select_card_type']) ? $lang['select_card_type'] : 'Seleccione el tipo de tarjeta'; ?>
                </div>
            </div>

            <!-- Campo: Número de Tarjeta - MODIFICADO: quitado readonly -->
            <div class="form-group">
                <label for="nroTarjeta" class="form-label"><?php echo isset($lang['number']) ? $lang['number'] : 'Número'; ?> <span class="required">*</span></label>
                <div class="input-group">
                    <input type="text" id="nroTarjeta" name="nroTarjeta" class="form-control" value="<?php echo isset($numeroTarjeta) ? $numeroTarjeta : '4532XXXXXXXX1234'; ?>" pattern="[0-9]{16}" required>
                    <div class="input-group-append">
                        <span class="input-group-text"><i class="fas fa-credit-card"></i></span>
                    </div>
                </div>
                <div class="invalid-feedback">
                    <?php echo isset($lang['card_number_required']) ? $lang['card_number_required'] : 'El número de tarjeta es requerido'; ?>
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['card_number_format_help']) ? $lang['card_number_format_help'] : 'Ingrese los 16 dígitos de la tarjeta'; ?></small>
            </div>

            <!-- Campo: CVV - MODIFICADO: quitado readonly -->
            <div class="form-group">
                <label for="cvv" class="form-label">CVV <span class="required">*</span></label>
                <div class="input-group">
                    <input type="text" id="cvv" name="cvv" class="form-control" value="<?php echo isset($cvv) ? $cvv : '123'; ?>" pattern="[0-9]{3}" maxlength="3" required>
                    <div class="input-group-append">
                        <span class="input-group-text"><i class="fas fa-lock"></i></span>
                    </div>
                </div>
                <div class="invalid-feedback">
                    <?php echo isset($lang['cvv_required']) ? $lang['cvv_required'] : 'El CVV es requerido'; ?>
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['cvv_help']) ? $lang['cvv_help'] : 'Código de seguridad de 3 dígitos'; ?></small>
            </div>

            <!-- Campo: Fecha de Expiración - MODIFICADO: quitado readonly -->
            <div class="form-group">
                <label for="fechaExpiracion" class="form-label"><?php echo isset($lang['validity']) ? $lang['validity'] : 'Vigencia'; ?> <span class="required">*</span></label>
                <div class="input-group">
                    <input type="text" id="fechaExpiracion" name="fechaExpiracion" class="form-control" value="<?php echo isset($fechaExpiracion) ? $fechaExpiracion : '12/28'; ?>" pattern="(0[1-9]|1[0-2])\/[0-9]{2}" maxlength="5" required>
                    <div class="input-group-append">
                        <span class="input-group-text"><i class="fas fa-calendar"></i></span>
                    </div>
                </div>
                <div class="invalid-feedback">
                    <?php echo isset($lang['expiration_date_required']) ? $lang['expiration_date_required'] : 'La fecha de vencimiento es requerida'; ?>
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['format_mm_yy']) ? $lang['format_mm_yy'] : 'Formato: MM/AA'; ?></small>
            </div>

            <!-- Campo: PIN -->
            <div class="form-group">
                <label for="pin" class="form-label">PIN <span class="required">*</span></label>
                <div class="input-group">
                    <input type="password" id="pin" name="pin" class="form-control" pattern="[0-9]{4}" maxlength="4" required>
                    <div class="input-group-append">
                        <button type="button" id="togglePin" class="btn btn-outline-secondary" tabindex="-1">
                            <i class="fas fa-eye"></i>
                        </button>
                    </div>
                </div>
                <div class="invalid-feedback" id="pin-feedback">
                    <?php echo isset($lang['pin_required']) ? $lang['pin_required'] : 'El PIN es requerido'; ?>
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['pin_must_be_4_digits']) ? $lang['pin_must_be_4_digits'] : 'Debe contener 4 dígitos numéricos'; ?></small>
            </div>

            <!-- Campo: Estado -->
            <div class="form-group">
                <label for="estado" class="form-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?> <span class="required">*</span></label>
                <select id="estado" name="estado" class="form-control" required>
                    <option value="activa" selected><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></option>
                    <option value="inactiva"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></option>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['select_status']) ? $lang['select_status'] : 'Seleccione un estado'; ?>
                </div>
            </div>

            <!-- Campo oculto para el hash -->
            <input type="hidden" id="hash" name="hash" value="<?php echo isset($hash) ? $hash : md5(uniqid(rand(), true)); ?>">

            <!-- Título de sección -->
            <div class="section-title">
                <h3><?php echo isset($lang['additional_information']) ? $lang['additional_information'] : 'Información Adicional'; ?></h3>
            </div>

            <!-- Campo: Límite de Crédito (solo visible si es tarjeta de crédito) -->
            <div class="form-group" id="limite-credito-group" style="display: none;">
                <label for="limiteCredito" class="form-label"><?php echo isset($lang['credit_limit']) ? $lang['credit_limit'] : 'Límite de Crédito'; ?> <span class="required">*</span></label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text">$</span>
                    </div>
                    <input type="number" id="limiteCredito" name="limiteCredito" class="form-control" step="0.01" min="0" value="1000">
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['max_limit_credit_cards']) ? $lang['max_limit_credit_cards'] : 'Límite máximo para tarjetas de crédito'; ?></small>
            </div>

            <!-- Campo: Notas -->
            <div class="form-group">
                <label for="notas" class="form-label"><?php echo isset($lang['notes']) ? $lang['notes'] : 'Notas'; ?></label>
                <textarea id="notas" name="notas" class="form-control" rows="3"></textarea>
                <small class="form-text text-muted"><?php echo isset($lang['additional_card_info']) ? $lang['additional_card_info'] : 'Información adicional sobre la tarjeta (opcional)'; ?></small>
            </div>

            <div class="form-actions">
                <button type="submit" class="btn btn-primary"><?php echo isset($lang['save']) ? $lang['save'] : 'Guardar'; ?></button>
                <a href="index.php?controller=tarjeta&action=listar" class="btn btn-secondary"><?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?></a>
            </div>
        </form>
    </div>
</div>

<style>
    .section-title {
        margin-top: 1.5rem;
        margin-bottom: 1rem;
        border-bottom: 1px solid #e0e0e0;
        padding-bottom: 0.5rem;
    }

    .section-title h3 {
        font-size: 1.2rem;
        color: #006633;
        margin: 0;
    }

    .form-group {
        margin-bottom: 1.5rem;
    }

    .form-control:read-only {
        background-color: #f9f9f9;
    }

    .input-group-text {
        background-color: #f4f4f4;
        border-color: #ced4da;
    }

    .field-focus {
        box-shadow: 0 0 0 0.2rem rgba(0, 102, 51, 0.25);
    }

    .btn-primary {
        background-color: #006633;
        border-color: #006633;
    }

    .btn-primary:hover {
        background-color: #005529;
        border-color: #005529;
    }

    .required {
        color: #dc3545;
        margin-left: 3px;
    }
</style>

<script>
    // Validación del formulario con JavaScript
    document.addEventListener('DOMContentLoaded', function() {
        const form = document.getElementById('form-tarjeta');

        // Formatear número de tarjeta en grupos de 4 dígitos
        const cardInput = document.getElementById('nroTarjeta');
        cardInput.addEventListener('input', function(e) {
            // Eliminar espacios y caracteres no numéricos
            let value = this.value.replace(/\D/g, '');
            
            // Limitar a 16 dígitos
            value = value.substring(0, 16);
            
            // Formatear en grupos de 4
            let formattedValue = '';
            for (let i = 0; i < value.length; i++) {
                if (i > 0 && i % 4 === 0) {
                    formattedValue += ' ';
                }
                formattedValue += value[i];
            }
            
            this.value = formattedValue;
        });

        // Formatear fecha de expiración
        const expirationInput = document.getElementById('fechaExpiracion');
        expirationInput.addEventListener('input', function(e) {
            // Eliminar caracteres no numéricos y '/'
            let value = this.value.replace(/[^\d/]/g, '');
            
            // Formatear como MM/YY
            if (value.length > 2 && !value.includes('/')) {
                value = value.substring(0, 2) + '/' + value.substring(2);
            }
            
            // Limitar a formato MM/YY
            if (value.includes('/')) {
                const parts = value.split('/');
                if (parts[0].length > 2) {
                    parts[0] = parts[0].substring(0, 2);
                }
                if (parts[1] && parts[1].length > 2) {
                    parts[1] = parts[1].substring(0, 2);
                }
                value = parts.join('/');
            }
            
            this.value = value;
        });

        // Validar que el PIN tenga exactamente 4 dígitos
        const pinInput = document.getElementById('pin');
        pinInput.addEventListener('input', function() {
            this.value = this.value.replace(/[^0-9]/g, '').substring(0, 4);

            // Actualizar mensaje de validación personalizado
            if (this.value.length !== 4) {
                this.setCustomValidity('El PIN debe tener 4 dígitos');
                document.getElementById('pin-feedback').textContent = '<?php echo isset($lang["pin_must_be_4_digits"]) ? $lang["pin_must_be_4_digits"] : "El PIN debe tener 4 dígitos"; ?>';
            } else {
                this.setCustomValidity('');
                document.getElementById('pin-feedback').textContent = '<?php echo isset($lang["pin_required"]) ? $lang["pin_required"] : "El PIN es requerido"; ?>';
            }
        });

        // Mostrar/ocultar PIN
        const togglePin = document.getElementById('togglePin');
        togglePin.addEventListener('click', function() {
            const type = pinInput.getAttribute('type') === 'password' ? 'text' : 'password';
            pinInput.setAttribute('type', type);
            this.querySelector('i').classList.toggle('fa-eye');
            this.querySelector('i').classList.toggle('fa-eye-slash');
        });

        // Mostrar/ocultar campo de límite de crédito según el tipo de tarjeta
        const tipoTarjetaSelect = document.getElementById('tipoTarjeta');
        const limiteCreditoGroup = document.getElementById('limite-credito-group');

        tipoTarjetaSelect.addEventListener('change', function() {
            if (this.value === 'credito') {
                limiteCreditoGroup.style.display = 'block';
                document.getElementById('limiteCredito').setAttribute('required', 'required');
            } else {
                limiteCreditoGroup.style.display = 'none';
                document.getElementById('limiteCredito').removeAttribute('required');
            }
        });

        form.addEventListener('submit', function(event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }

            // Validación adicional para el PIN
            if (pinInput.value.length !== 4 || !/^\d{4}$/.test(pinInput.value)) {
                pinInput.setCustomValidity('El PIN debe tener 4 dígitos');
                document.getElementById('pin-feedback').textContent = '<?php echo isset($lang["pin_must_be_4_digits"]) ? $lang["pin_must_be_4_digits"] : "El PIN debe tener 4 dígitos"; ?>';
                event.preventDefault();
                event.stopPropagation();
            } else {
                pinInput.setCustomValidity('');
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