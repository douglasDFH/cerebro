<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleCrear.css">

<div class="client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['new_account']) ? $lang['new_account'] : 'Nueva Cuenta'; ?></h2>
    </div>
    <div class="card-body">
        <form id="form-cuenta" method="POST" action="index.php?controller=cuenta&action=guardar" class="needs-validation" novalidate>
            <!-- Campo: Cliente -->
            <div class="form-group">
                <label for="idPersona" class="form-label"><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?><span class="required">*</span></label>
                <?php if ($cliente): ?>
                    <!-- Cliente ya seleccionado -->
                    <input type="hidden" name="idPersona" value="<?php echo $cliente['idPersona']; ?>">
                    <div class="form-control disabled">
                        <?php echo $cliente['nombre'] . ' ' . $cliente['apellidoPaterno'] . ' ' . $cliente['apellidoMaterno'] . ' - ' . $cliente['ci']; ?>
                    </div>
                <?php else: ?>
                    <!-- Selector de cliente -->
                    <select id="idPersona" name="idPersona" class="form-control" required>
                        <option value=""><?php echo isset($lang['select_client']) ? $lang['select_client'] : 'Seleccione un cliente'; ?></option>
                        <?php foreach ($clientes as $cliente): ?>
                        <option value="<?php echo $cliente['idPersona']; ?>">
                            <?php echo $cliente['nombre'] . ' ' . $cliente['apellidoPaterno'] . ' ' . $cliente['apellidoMaterno'] . ' - ' . $cliente['ci']; ?>
                        </option>
                        <?php endforeach; ?>
                    </select>
                    <div class="invalid-feedback">
                        <?php echo isset($lang['client_required']) ? $lang['client_required'] : 'El cliente es requerido'; ?>
                    </div>
                <?php endif; ?>
            </div>
            
            <!-- Campo: Tipo de Cuenta -->
            <div class="form-group">
                <label for="tipoCuenta" class="form-label"><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?><span class="required">*</span></label>
                <select id="tipoCuenta" name="tipoCuenta" class="form-control" required>
                    <option value=""><?php echo isset($lang['select_option']) ? $lang['select_option'] : 'Seleccione una opción'; ?></option>
                    <option value="1"><?php echo isset($lang['savings_account']) ? $lang['savings_account'] : 'Cuenta de Ahorro'; ?></option>
                    <option value="2"><?php echo isset($lang['checking_account']) ? $lang['checking_account'] : 'Cuenta Corriente'; ?></option>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['account_type_required']) ? $lang['account_type_required'] : 'El tipo de cuenta es requerido'; ?>
                </div>
            </div>
            
            <!-- Campo: Tipo de Moneda -->
            <div class="form-group">
                <label for="tipoMoneda" class="form-label"><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?><span class="required">*</span></label>
                <select id="tipoMoneda" name="tipoMoneda" class="form-control" required>
                    <option value=""><?php echo isset($lang['select_option']) ? $lang['select_option'] : 'Seleccione una opción'; ?></option>
                    <option value="1"><?php echo isset($lang['bolivianos']) ? $lang['bolivianos'] : 'Bolivianos'; ?></option>
                    <option value="2"><?php echo isset($lang['dollars']) ? $lang['dollars'] : 'Dólares'; ?></option>
                </select>
                <div class="invalid-feedback">
                    <?php echo isset($lang['currency_required']) ? $lang['currency_required'] : 'La moneda es requerida'; ?>
                </div>
            </div>
            
            <!-- Campo: Saldo Inicial -->
            <div class="form-group">
                <label for="saldoInicial" class="form-label"><?php echo isset($lang['initial_balance']) ? $lang['initial_balance'] : 'Saldo Inicial'; ?></label>
                <div class="input-group">
                    <div class="input-group-prepend">
                        <span class="input-group-text" id="moneda-symbol">Bs.</span>
                    </div>
                    <input type="number" id="saldoInicial" name="saldoInicial" class="form-control" step="0.01" min="0" value="0">
                </div>
                <small class="form-text text-muted"><?php echo isset($lang['initial_balance_help']) ? $lang['initial_balance_help'] : 'Saldo con el que se iniciará la cuenta'; ?></small>
            </div>
            
            <div class="form-actions">
                <button type="submit" class="btn btn-primary"><?php echo isset($lang['save']) ? $lang['save'] : 'Guardar'; ?></button>
                <?php if (isset($cliente) && $cliente): ?>
                    <a href="index.php?controller=cliente&action=ver&id=<?php echo $cliente['idPersona']; ?>" class="btn btn-secondary"><?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?></a>
                <?php else: ?>
                    <a href="index.php?controller=cuenta&action=listar" class="btn btn-secondary"><?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?></a>
                <?php endif; ?>
            </div>
        </form>
    </div>
</div>

<script>
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form-cuenta');
    const tipoMoneda = document.getElementById('tipoMoneda');
    const monedaSymbol = document.getElementById('moneda-symbol');
    
    // Cambiar el símbolo de moneda según el tipo seleccionado
    tipoMoneda.addEventListener('change', function() {
        if (this.value === '1') {
            monedaSymbol.textContent = 'Bs.';
        } else if (this.value === '2') {
            monedaSymbol.textContent = '$';
        } else {
            monedaSymbol.textContent = '';
        }
    });
    
    // Validación del formulario
    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        
        form.classList.add('was-validated');
    });
});
</script>