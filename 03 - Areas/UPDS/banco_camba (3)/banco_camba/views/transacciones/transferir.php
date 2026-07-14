<div class="card">
    <div class="card-header">
        <h2><?php echo $lang['transfer_funds']; ?></h2>
    </div>
    <div class="card-body">
        <!-- Paso 1: Seleccionar cuenta de origen -->
        <?php if (!isset($cuentaOrigen)): ?>
            <div class="alert alert-info">
                <?php echo $lang['select_source_account']; ?>
            </div>
            
            <form method="POST" action="index.php?controller=transaccion&action=buscarCuentaOrigen" class="mb-4">
                <div class="form-group">
                    <label for="nroCuentaOrigen"><?php echo $lang['source_account_number']; ?> *</label>
                    <input type="text" id="nroCuentaOrigen" name="nroCuentaOrigen" class="form-control" required>
                </div>
                
                <button type="submit" class="btn btn-primary"><?php echo $lang['search']; ?></button>
                <a href="index.php?controller=transaccion&action=listar" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
            </form>
        
        <!-- Paso 2: Seleccionar cuenta de destino -->
        <?php elseif (!isset($cuentaDestino)): ?>
            <div class="row mb-4">
                <div class="col-md-12">
                    <h4><?php echo $lang['source_account']; ?></h4>
                    <table class="table">
                        <tr>
                            <th><?php echo $lang['account_number']; ?>:</th>
                            <td><?php echo $cuentaOrigen['nroCuenta']; ?></td>
                            <th><?php echo $lang['account_type']; ?>:</th>
                            <td>
                                <?php echo $cuentaOrigen['tipoCuenta'] == 1 ? $lang['savings_account'] : $lang['checking_account']; ?>
                            </td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['client']; ?>:</th>
                            <td><?php echo $cuentaOrigen['cliente_nombre']; ?></td>
                            <th><?php echo $lang['available_balance']; ?>:</th>
                            <td>
                                <strong>
                                    <?php 
                                    if ($cuentaOrigen['tipoMoneda'] == 1) {
                                        echo 'Bs. ' . number_format($cuentaOrigen['saldo'], 2);
                                    } else {
                                        echo '$ ' . number_format($cuentaOrigen['saldo'], 2);
                                    }
                                    ?>
                                </strong>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            
            <div class="alert alert-info">
                <?php echo $lang['select_destination_account']; ?>
            </div>
            
            <form method="POST" action="index.php?controller=transaccion&action=buscarCuentaDestino" class="mb-4">
                <input type="hidden" name="idCuentaOrigen" value="<?php echo $cuentaOrigen['idCuenta']; ?>">
                
                <div class="form-group">
                    <label for="nroCuentaDestino"><?php echo $lang['destination_account_number']; ?> *</label>
                    <input type="text" id="nroCuentaDestino" name="nroCuentaDestino" class="form-control" required>
                </div>
                
                <button type="submit" class="btn btn-primary"><?php echo $lang['search']; ?></button>
                <a href="index.php?controller=transaccion&action=transferir" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
            </form>
        
        <!-- Paso 3: Realizar transferencia -->
        <?php else: ?>
            <div class="row mb-4">
                <div class="col-md-6">
                    <h4><?php echo $lang['source_account']; ?></h4>
                    <table class="table">
                        <tr>
                            <th><?php echo $lang['account_number']; ?>:</th>
                            <td><?php echo $cuentaOrigen['nroCuenta']; ?></td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['client']; ?>:</th>
                            <td><?php echo $cuentaOrigen['cliente_nombre']; ?></td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['account_type']; ?>:</th>
                            <td>
                                <?php echo $cuentaOrigen['tipoCuenta'] == 1 ? $lang['savings_account'] : $lang['checking_account']; ?>
                            </td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['currency']; ?>:</th>
                            <td>
                                <?php echo $cuentaOrigen['tipoMoneda'] == 1 ? $lang['bolivianos'] : $lang['dollars']; ?>
                            </td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['available_balance']; ?>:</th>
                            <td>
                                <strong>
                                    <?php 
                                    if ($cuentaOrigen['tipoMoneda'] == 1) {
                                        echo 'Bs. ' . number_format($cuentaOrigen['saldo'], 2);
                                    } else {
                                        echo '$ ' . number_format($cuentaOrigen['saldo'], 2);
                                    }
                                    ?>
                                </strong>
                            </td>
                        </tr>
                    </table>
                </div>
                
                <div class="col-md-6">
                    <h4><?php echo $lang['destination_account']; ?></h4>
                    <table class="table">
                        <tr>
                            <th><?php echo $lang['account_number']; ?>:</th>
                            <td><?php echo $cuentaDestino['nroCuenta']; ?></td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['client']; ?>:</th>
                            <td><?php echo $cuentaDestino['cliente_nombre']; ?></td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['account_type']; ?>:</th>
                            <td>
                                <?php echo $cuentaDestino['tipoCuenta'] == 1 ? $lang['savings_account'] : $lang['checking_account']; ?>
                            </td>
                        </tr>
                        <tr>
                            <th><?php echo $lang['currency']; ?>:</th>
                            <td>
                                <?php echo $cuentaDestino['tipoMoneda'] == 1 ? $lang['bolivianos'] : $lang['dollars']; ?>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            
            <form method="POST" action="index.php?controller=transaccion&action=procesarTransferencia" class="needs-validation" novalidate>
                <input type="hidden" name="idCuentaOrigen" value="<?php echo $cuentaOrigen['idCuenta']; ?>">
                <input type="hidden" name="idCuentaDestino" value="<?php echo $cuentaDestino['idCuenta']; ?>">
                
                <div class="form-group">
                    <label for="monto"><?php echo $lang['amount']; ?> *</label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><?php echo $cuentaOrigen['tipoMoneda'] == 1 ? 'Bs.' : '$'; ?></span>
                        </div>
                        <input type="number" id="monto" name="monto" class="form-control" step="0.01" min="0.01" max="<?php echo $cuentaOrigen['saldo']; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo $lang['amount_required']; ?>
                        </div>
                    </div>
                    <small class="form-text text-muted">
                        <?php echo $lang['max_amount'] . ': ' . ($cuentaOrigen['tipoMoneda'] == 1 ? 'Bs. ' : '$ ') . number_format($cuentaOrigen['saldo'], 2); ?>
                    </small>
                </div>
                
                <div class="form-group">
                    <label for="descripcion"><?php echo $lang['description']; ?></label>
                    <textarea id="descripcion" name="descripcion" class="form-control" rows="3"><?php echo $lang['transfer']; ?></textarea>
                </div>
                
                <<div class="form-actions">
                    <button type="submit" class="btn btn-primary"><?php echo $lang['transfer']; ?></button>
                    <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuentaOrigen['idCuenta']; ?>" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
                </div>
            </form>
        <?php endif; ?>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('.needs-validation');
    if (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }
            
            const monto = parseFloat(document.getElementById('monto').value);
            const saldo = <?php echo isset($cuentaOrigen) ? $cuentaOrigen['saldo'] : 0; ?>;
            
            if (monto > saldo) {
                event.preventDefault();
                alert('<?php echo $lang['insufficient_funds']; ?>');
            }
            
            form.classList.add('was-validated');
        });
    }
});
</script>