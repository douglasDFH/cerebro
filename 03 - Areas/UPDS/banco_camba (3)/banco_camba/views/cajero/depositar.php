<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleCrear.css">
<link rel="stylesheet" href="assets/css/StyleDepositar.css">
<div class="client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo $lang['atm_deposit'] ?? 'Depósito en ATM'; ?></h2>
    </div>
    <div class="card-body">
        <?php if (!isset($cuenta)): ?>
            <!-- Formulario para buscar cuenta para depósito -->
            <div class="search-account-form">
                <h3><?php echo $lang['search_account'] ?? 'Buscar Cuenta'; ?></h3>
                <form id="form-buscar-cuenta" method="POST" action="index.php?controller=transaccionatm&action=buscarCuentaDeposito" class="needs-validation" novalidate>
                    <div class="row">
                        <div class="col-md-8">
                            <div class="form-group">
                                <label for="nroCuenta" class="form-label"><?php echo $lang['account_number'] ?? 'Número de Cuenta'; ?><span class="required">*</span></label>
                                <input type="text" id="nroCuenta" name="nroCuenta" class="form-control" required>
                                <div class="invalid-feedback">
                                    <?php echo $lang['account_number_required'] ?? 'El número de cuenta es obligatorio'; ?>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 d-flex align-items-end">
                            <button type="submit" class="btn btn-primary"><?php echo $lang['search'] ?? 'Buscar'; ?></button>
                        </div>
                    </div>
                </form>
            </div>
        <?php else: ?>
            <!-- Formulario de depósito -->
            <form id="form-deposito" method="POST" action="index.php?controller=transaccionatm&action=procesarDeposito" class="needs-validation" novalidate>
                <input type="hidden" name="idCuenta" value="<?php echo $cuenta->idCuenta; ?>">
                
                <div class="row">
                    <div class="col-md-6">
                        <!-- Información de la cuenta -->
                        <div class="card account-info">
                            <div class="card-header">
                                <?php echo $lang['account_information'] ?? 'Información de la Cuenta'; ?>
                            </div>
                            <div class="card-body">
                                <p><strong><?php echo $lang['account_number'] ?? 'Número de Cuenta'; ?>:</strong> <?php echo $cuenta->numeroCuenta; ?></p>
                                <p><strong><?php echo $lang['account_type'] ?? 'Tipo de Cuenta'; ?>:</strong> <?php echo $cuenta->tipoCuenta == 1 ? ($lang['savings_account'] ?? 'Cuenta de Ahorro') : ($lang['checking_account'] ?? 'Cuenta Corriente'); ?></p>
                                <p><strong><?php echo $lang['currency'] ?? 'Moneda'; ?>:</strong> <?php echo $cuenta->tipoMoneda == 1 ? 'Bs.' : 'USD'; ?></p>
                                <p><strong><?php echo $lang['current_balance'] ?? 'Saldo Actual'; ?>:</strong> <?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($cuenta->saldo, 2); ?></p>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <!-- Información del cliente -->
                        <div class="card client-info">
                            <div class="card-header">
                                <?php echo $lang['client_information'] ?? 'Información del Cliente'; ?>
                            </div>
                            <div class="card-body">
                                <p><strong><?php echo $lang['name'] ?? 'Nombre'; ?>:</strong> <?php echo $cliente->nombres . ' ' . $cliente->apellidos; ?></p>
                                <p><strong><?php echo $lang['document'] ?? 'Documento'; ?>:</strong> <?php echo $cliente->tipoDocumento . ': ' . $cliente->numeroDocumento; ?></p>
                                <p><strong><?php echo $lang['email'] ?? 'Correo'; ?>:</strong> <?php echo $cliente->correo; ?></p>
                                <p><strong><?php echo $lang['phone'] ?? 'Teléfono'; ?>:</strong> <?php echo $cliente->telefono; ?></p>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row mt-4">
                    <div class="col-md-6">
                        <!-- Selección de ATM -->
                        <div class="form-group">
                            <label for="idATM" class="form-label"><?php echo $lang['atm'] ?? 'Cajero ATM'; ?><span class="required">*</span></label>
                            <select id="idATM" name="idATM" class="form-control" required>
                                <option value=""><?php echo $lang['select_atm'] ?? 'Seleccione un ATM'; ?></option>
                                <?php foreach ($atms as $atm): ?>
                                    <option value="<?php echo $atm['idATM']; ?>">
                                        <?php echo htmlspecialchars($atm['nombre'] . ' (' . $atm['ubicacion'] . ')'); ?>
                                    </option>
                                <?php endforeach; ?>
                            </select>
                            <div class="invalid-feedback">
                                <?php echo $lang['atm_required'] ?? 'Debe seleccionar un ATM'; ?>
                            </div>
                        </div>
                        
                        <!-- Selección de Tarjeta -->
                        <div class="form-group">
                            <label for="idTarjeta" class="form-label"><?php echo $lang['card'] ?? 'Tarjeta'; ?><span class="required">*</span></label>
                            <select id="idTarjeta" name="idTarjeta" class="form-control" required>
                                <option value=""><?php echo $lang['select_card'] ?? 'Seleccione una Tarjeta'; ?></option>
                                <?php foreach ($tarjetas as $tarjeta): ?>
                                    <option value="<?php echo $tarjeta['idTarjeta']; ?>">
                                        <?php echo htmlspecialchars($tarjeta['numeroTarjeta'] . ' (' . $tarjeta['tipoTarjeta'] . ')'); ?>
                                    </option>
                                <?php endforeach; ?>
                            </select>
                            <div class="invalid-feedback">
                                <?php echo $lang['card_required'] ?? 'Debe seleccionar una tarjeta'; ?>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <!-- Monto a depositar -->
                        <div class="form-group">
                            <label for="monto" class="form-label"><?php echo $lang['amount_to_deposit'] ?? 'Monto a Depositar'; ?><span class="required">*</span></label>
                            <input type="number" id="monto" name="monto" min="0.01" step="0.01" class="form-control" required>
                            <div class="invalid-feedback">
                                <?php echo $lang['amount_required'] ?? 'El monto es obligatorio y debe ser mayor a cero'; ?>
                            </div>
                        </div>
                        
                        <!-- Descripción -->
                        <div class="form-group">
                            <label for="descripcion" class="form-label"><?php echo $lang['description'] ?? 'Descripción'; ?></label>
                            <textarea id="descripcion" name="descripcion" class="form-control" rows="3"></textarea>
                        </div>
                    </div>
                </div>
                
                <div class="form-actions">
                    <button type="submit" class="btn btn-primary"><?php echo $lang['deposit'] ?? 'Depositar'; ?></button>
                    <a href="index.php?controller=transaccionatm&action=listar" class="btn btn-secondary"><?php echo $lang['cancel'] ?? 'Cancelar'; ?></a>
                </div>
            </form>
        <?php endif; ?>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const formBuscarCuenta = document.getElementById('form-buscar-cuenta');
    const formDeposito = document.getElementById('form-deposito');
    
    if (formBuscarCuenta) {
        formBuscarCuenta.addEventListener('submit', function (event) {
            if (!formBuscarCuenta.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            
            formBuscarCuenta.classList.add('was-validated');
        });
    }
    
    if (formDeposito) {
        formDeposito.addEventListener('submit', function (event) {
            if (!formDeposito.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            
            formDeposito.classList.add('was-validated');
        });
        
        // Validación adicional para el monto
        const montoInput = document.getElementById('monto');
        if (montoInput) {
            montoInput.addEventListener('input', function() {
                const value = parseFloat(this.value);
                if (isNaN(value) || value <= 0) {
                    this.setCustomValidity('El monto debe ser mayor a cero');
                } else {
                    this.setCustomValidity('');
                }
            });
        }
    }
    
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