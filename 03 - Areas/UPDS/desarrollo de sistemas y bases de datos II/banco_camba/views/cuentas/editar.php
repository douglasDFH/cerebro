<h1><?php echo $lang['edit_account']; ?></h1>

<div class="card">
    <div class="card-header">
        <h3><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?></h3>
        <p><?php echo $lang['account_number']; ?>: <strong><?php echo htmlspecialchars($cuenta->nroCuenta); ?></strong></p>
    </div>
    <div class="card-body">
        <form method="POST" action="index.php?controller=cuenta&action=editar&id=<?php echo $cuenta->idCuenta; ?>" class="needs-validation">
            <div class="row">
                <!-- Tipo de cuenta -->
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="tipoCuenta" class="form-label"><?php echo $lang['account_type']; ?> <span class="text-danger">*</span></label>
                        <select class="form-control" id="tipoCuenta" name="tipoCuenta" required>
                            <option value=""><?php echo $lang['select_option']; ?></option>
                            <option value="1" <?php echo ($cuenta->tipoCuenta == 1) ? 'selected' : ''; ?>><?php echo $lang['savings_account']; ?></option>
                            <option value="2" <?php echo ($cuenta->tipoCuenta == 2) ? 'selected' : ''; ?>><?php echo $lang['checking_account']; ?></option>
                        </select>
                    </div>
                </div>
                
                <!-- Tipo de moneda -->
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="tipoMoneda" class="form-label"><?php echo $lang['currency']; ?> <span class="text-danger">*</span></label>
                        <select class="form-control" id="tipoMoneda" name="tipoMoneda" required>
                            <option value=""><?php echo $lang['select_option']; ?></option>
                            <option value="1" <?php echo ($cuenta->tipoMoneda == 1) ? 'selected' : ''; ?>><?php echo $lang['bolivianos']; ?></option>
                            <option value="2" <?php echo ($cuenta->tipoMoneda == 2) ? 'selected' : ''; ?>><?php echo $lang['dollars']; ?></option>
                        </select>
                    </div>
                </div>
                
                <!-- Estado de la cuenta -->
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="estado" class="form-label"><?php echo $lang['status']; ?> <span class="text-danger">*</span></label>
                        <select class="form-control" id="estado" name="estado" required>
                            <option value=""><?php echo $lang['select_option']; ?></option>
                            <option value="1" <?php echo ($cuenta->estado == 1) ? 'selected' : ''; ?>><?php echo $lang['active']; ?></option>
                            <option value="2" <?php echo ($cuenta->estado == 2) ? 'selected' : ''; ?>><?php echo $lang['inactive']; ?></option>
                        </select>
                    </div>
                </div>
                
                <!-- Información adicional -->
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-label"><?php echo $lang['opening_date']; ?>:</label>
                        <p class="form-control-static"><?php echo date('d/m/Y', strtotime($cuenta->fechaApertura)); ?></p>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="form-group">
                        <label class="form-label"><?php echo $lang['current_balance']; ?>:</label>
                        <p class="form-control-static">
                            <?php
                            $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                            echo $moneda . number_format($cuenta->saldo, 2);
                            ?>
                        </p>
                    </div>
                </div>
            </div>
            
            <div class="alert alert-warning">
                <strong><?php echo $lang['warning']; ?>:</strong> <?php echo $lang['account_edit_warning']; ?>
            </div>
            
            <div class="form-group mt-3 text-center">
                <button type="submit" class="btn btn-primary"><?php echo $lang['save']; ?></button>
                <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta->idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
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
});
</script>