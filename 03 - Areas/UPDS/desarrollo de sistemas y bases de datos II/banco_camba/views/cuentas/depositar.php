<h1><?php echo $lang['deposit']; ?></h1>

<div class="card">
    <div class="card-header">
        <h5><?php echo $lang['account']; ?></h5>
    </div>
    <div class="card-body">
        <?php if (!isset($cuenta)): ?>
        <form method="POST" action="index.php?controller=transaccion&action=buscarCuentaDeposito">
            <div class="form-group">
                <label for="nroCuenta"><?php echo $lang['account_number']; ?>:</label>
                <input type="text" class="form-control" id="nroCuenta" name="nroCuenta" required>
            </div>
            <button type="submit" class="btn btn-primary">
                <i class="icon-search"></i> <?php echo $lang['search_account']; ?>
            </button>
        </form>
        <?php endif; ?>
    </div>
</div>

<?php if (isset($cuenta)): ?>
<div class="card mt-4">
    <div class="card-header">
        <h5><?php echo $lang['account_information']; ?></h5>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-6">
                <h6><?php echo $lang['account_details']; ?></h6>
                <table class="table table-bordered">
                    <tr>
                        <th><?php echo $lang['account_number']; ?>:</th>
                        <td><?php echo htmlspecialchars($cuenta['nroCuenta']); ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['account_type']; ?>:</th>
                        <td>
                            <?php
                            if ($cuenta['tipoCuenta'] == 1) {
                                echo $lang['savings_account'];
                            } else {
                                echo $lang['checking_account'];
                            }
                            ?>
                        </td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['currency']; ?>:</th>
                        <td>
                            <?php
                            if ($cuenta['tipoMoneda'] == 1) {
                                echo $lang['bolivianos'];
                            } else {
                                echo $lang['dollars'];
                            }
                            ?>
                        </td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['current_balance']; ?>:</th>
                        <td>
                            <?php 
                            $moneda = ($cuenta['tipoMoneda'] == 1) ? 'Bs. ' : '$ ';
                            echo $moneda . number_format($cuenta['saldo'], 2); 
                            ?>
                        </td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['status']; ?>:</th>
                        <td>
                            <?php if ($cuenta['estado'] == 1): ?>
                                <span class="badge" style="background-color: #28a745;"><?php echo $lang['active']; ?></span>
                            <?php else: ?>
                                <span class="badge" style="background-color: #dc3545;"><?php echo $lang['inactive']; ?></span>
                            <?php endif; ?>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-6">
                <h6><?php echo $lang['client_information']; ?></h6>
                <table class="table table-bordered">
                    <tr>
                        <th><?php echo $lang['name']; ?>:</th>
                        <td><?php echo htmlspecialchars($cuenta['cliente_nombre']); ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['id_number']; ?>:</th>
                        <td><?php echo htmlspecialchars($cuenta['cliente_ci']); ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['phone']; ?>:</th>
                        <td><?php echo htmlspecialchars($cuenta['cliente_telefono']); ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['email']; ?>:</th>
                        <td><?php echo htmlspecialchars($cuenta['cliente_email']); ?></td>
                    </tr>
                </table>
            </div>
        </div>
        
        <?php if ($cuenta['estado'] == 1): ?>
            <div class="deposit-form mt-4">
                <h5><?php echo $lang['make_deposit']; ?></h5>
                <form method="POST" action="index.php?controller=transaccion&action=procesarDeposito">
                    <input type="hidden" name="idCuenta" value="<?php echo $cuenta['idCuenta']; ?>">
                    <input type="hidden" name="tipoMoneda" value="<?php echo $cuenta['tipoMoneda']; ?>">
                    
                    <div class="form-group">
                        <label for="monto"><?php echo $lang['amount']; ?>:</label>
                        <div class="input-group">
                            <div class="input-group-prepend">
                                <span class="input-group-text">
                                    <?php echo ($cuenta['tipoMoneda'] == 1) ? 'Bs.' : '$'; ?>
                                </span>
                            </div>
                            <input type="number" class="form-control" id="monto" name="monto" min="1" step="0.01" required>
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <label for="descripcion"><?php echo $lang['description']; ?>:</label>
                        <textarea class="form-control" id="descripcion" name="descripcion" rows="3"></textarea>
                    </div>
                    
                    <div class="form-group">
                        <button type="submit" class="btn btn-success">
                            <i class="icon-check"></i> <?php echo $lang['confirm_deposit']; ?>
                        </button>
                        <a href="index.php?controller=transaccion&action=depositar" class="btn btn-secondary">
                            <i class="icon-x"></i> <?php echo $lang['cancel']; ?>
                        </a>
                    </div>
                </form>
            </div>
        <?php else: ?>
            <div class="alert alert-danger mt-4">
                <i class="icon-alert-triangle"></i> <?php echo $lang['inactive_account_deposit_error']; ?>
            </div>
        <?php endif; ?>
    </div>
</div>
<?php endif; ?>