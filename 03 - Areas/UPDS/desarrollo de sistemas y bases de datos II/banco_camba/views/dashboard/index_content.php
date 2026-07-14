<h1><?php echo $lang['dashboard']; ?></h1>
<div class="dashboard">
    <div class="card">
        <div class="card-header"><?php echo $lang['clients']; ?></div>
        <div class="card-body">
            <p><?php echo $lang['total_clients']; ?>: <span id="total-clients"><?php echo $totalClientes; ?></span></p>
            <a href="index.php?controller=cliente&action=crear" class="btn btn-primary"><?php echo $lang['new_client']; ?></a>
        </div>
    </div>
    
    <div class="card">
        <div class="card-header"><?php echo $lang['accounts']; ?></div>
        <div class="card-body">
            <p><?php echo $lang['total_accounts']; ?>: <span id="total-accounts"><?php echo $totalCuentas; ?></span></p>
            <a href="index.php?controller=cuenta&action=crear" class="btn btn-primary"><?php echo $lang['new_account']; ?></a>
        </div>
    </div>
    
    <div class="card">
        <div class="card-header"><?php echo $lang['transactions']; ?></div>
        <div class="card-body">
            <p><?php echo $lang['today_transactions']; ?>: <span id="today-transactions"><?php echo $transaccionesHoy; ?></span></p>
            <a href="index.php?controller=transaccion&action=crear" class="btn btn-primary"><?php echo $lang['new_transaction']; ?></a>
        </div>
    </div>
</div>

<div class="card">
    <div class="card-header"><?php echo $lang['recent_transactions']; ?></div>
    <div class="card-body">
        <?php if (count($transaccionesRecientes) > 0): ?>
            <table class="table table-striped">
                <thead>
                    <tr>
                        <th>ID</th>
                        <th><?php echo $lang['account_number']; ?></th>
                        <th><?php echo $lang['client_details']; ?></th>
                        <th><?php echo $lang['transaction_type']; ?></th>
                        <th><?php echo $lang['amount']; ?></th>
                        <th><?php echo $lang['date']; ?></th>
                        <th><?php echo $lang['time']; ?></th>
                        <th><?php echo $lang['actions']; ?></th>
                    </tr>
                </thead>
                <tbody id="recent-transactions">
                    <?php foreach ($transaccionesRecientes as $transaccion): ?>
                        <tr>
                            <td><?php echo $transaccion['idTransaccion']; ?></td>
                            <td><?php echo $transaccion['nroCuenta']; ?></td>
                            <td><?php echo $transaccion['cliente_nombre']; ?></td>
                            <td>
                                <?php if ($transaccion['tipoTransaccion'] == 1): ?>
                                    <span class="badge" style="background-color: #dc3545;"><?php echo $lang['withdrawal']; ?></span>
                                <?php else: ?>
                                    <span class="badge" style="background-color: #28a745;"><?php echo $lang['deposit']; ?></span>
                                <?php endif; ?>
                            </td>
                            <td><?php echo number_format($transaccion['monto'], 2); ?></td>
                            <td><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                            <td><?php echo $transaccion['hora']; ?></td>
                            <td>
                                <a href="index.php?controller=transaccion&action=ver&id=<?php echo $transaccion['idTransaccion']; ?>" class="btn btn-sm btn-info">
                                    <i class="icon-eye"></i> <?php echo $lang['view']; ?>
                                </a>
                            </td>
                        </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        <?php else: ?>
            <p><?php echo $lang['no_transactions']; ?></p>
        <?php endif; ?>
    </div>
</div>