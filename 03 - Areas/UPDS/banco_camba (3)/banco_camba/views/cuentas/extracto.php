<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h2><?php echo $lang['account_statement']; ?></h2>
        <a href="index.php?controller=cuenta&action=ver&id=<?php echo $this->model->idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['back']; ?></a>
    </div>
    <div class="card-body">
        <div class="account-info mb-4">
            <div class="row">
                <div class="col-md-6">
                    <h4><?php echo $lang['account_information']; ?></h4>
                    <p><strong><?php echo $lang['client']; ?>:</strong> <?php echo $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno; ?></p>
                    <p><strong><?php echo $lang['account_number']; ?>:</strong> <?php echo $this->model->nroCuenta; ?></p>
                    <p><strong><?php echo $lang['account_type']; ?>:</strong> <?php echo $this->model->tipoCuenta == 1 ? $lang['savings_account'] : $lang['checking_account']; ?></p>
                </div>
                <div class="col-md-6">
                    <h4><?php echo $lang['statement_details']; ?></h4>
                    <p><strong><?php echo $lang['currency']; ?>:</strong> <?php echo $this->model->tipoMoneda == 1 ? $lang['bolivianos'] : $lang['dollars']; ?></p>
                    <p><strong><?php echo $lang['current_balance']; ?>:</strong> <?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->saldo, 2); ?></p>
                    <p><strong><?php echo $lang['period']; ?>:</strong> <?php echo date('d/m/Y', strtotime($fechaInicio)) . ' - ' . date('d/m/Y', strtotime($fechaFin)); ?></p>
                </div>
            </div>
        </div>
        
        <!-- Filtro de fechas -->
        <div class="filter-section mb-4">
            <form method="GET" action="index.php" class="row">
                <input type="hidden" name="controller" value="cuenta">
                <input type="hidden" name="action" value="extracto">
                <input type="hidden" name="id" value="<?php echo $this->model->idCuenta; ?>">
                
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fechaInicio"><?php echo $lang['start_date']; ?></label>
                        <input type="date" id="fechaInicio" name="fechaInicio" class="form-control" value="<?php echo $fechaInicio; ?>">
                    </div>
                </div>
                
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fechaFin"><?php echo $lang['end_date']; ?></label>
                        <input type="date" id="fechaFin" name="fechaFin" class="form-control" value="<?php echo $fechaFin; ?>">
                    </div>
                </div>
                
                <div class="col-md-4">
                    <div class="form-group">
                        <label>&nbsp;</label>
                        <button type="submit" class="btn btn-primary form-control"><?php echo $lang['filter']; ?></button>
                    </div>
                </div>
            </form>
        </div>
        
        <!-- Resumen de transacciones -->
        <div class="summary-section mb-4">
            <h4><?php echo $lang['transactions_summary']; ?></h4>
            <?php
            // Calcular totales
            $totalDepositos = 0;
            $totalRetiros = 0;
            $totalTransferenciasSalientes = 0;
            $totalTransferenciasEntrantes = 0;
            
            foreach ($transacciones as $transaccion) {
                switch($transaccion['tipoTransaccion']) {
                    case 1: // Retiro
                        $totalRetiros += $transaccion['monto'];
                        break;
                    case 2: // Depósito
                        $totalDepositos += $transaccion['monto'];
                        break;
                    case 3: // Transferencia entrante
                        $totalTransferenciasEntrantes += $transaccion['monto'];
                        break;
                    case 4: // Transferencia saliente
                        $totalTransferenciasSalientes += $transaccion['monto'];
                        break;
                }
            }
            ?>
            
            <div class="row">
                <div class="col-md-3">
                    <div class="card bg-success text-white">
                        <div class="card-body">
                            <h5 class="card-title"><?php echo $lang['deposits']; ?></h5>
                            <p class="card-text"><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalDepositos, 2); ?></p>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="card bg-warning text-dark">
                        <div class="card-body">
                            <h5 class="card-title"><?php echo $lang['withdrawals']; ?></h5>
                            <p class="card-text"><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalRetiros, 2); ?></p>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="card bg-info text-white">
                        <div class="card-body">
                            <h5 class="card-title"><?php echo $lang['transfers_received']; ?></h5>
                            <p class="card-text"><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalTransferenciasEntrantes, 2); ?></p>
                        </div>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="card bg-primary text-white">
                        <div class="card-body">
                            <h5 class="card-title"><?php echo $lang['transfers_sent']; ?></h5>
                            <p class="card-text"><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalTransferenciasSalientes, 2); ?></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Lista de transacciones -->
        <div class="transactions-section">
            <h4><?php echo $lang['transactions_list']; ?></h4>
            
            <?php if (empty($transacciones)): ?>
                <div class="alert alert-info"><?php echo $lang['no_transactions_for_period']; ?></div>
            <?php else: ?>
                <div class="table-responsive">
                    <table class="table table-striped">
                        <thead>
                            <tr>
                                <th><?php echo $lang['date']; ?></th>
                                <th><?php echo $lang['time']; ?></th>
                                <th><?php echo $lang['transaction_type']; ?></th>
                                <th><?php echo $lang['description']; ?></th>
                                <th><?php echo $lang['amount']; ?></th>
                                <th><?php echo $lang['balance']; ?></th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach ($transacciones as $transaccion): ?>
                                <tr>
                                    <td><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                                    <td><?php echo date('H:i:s', strtotime($transaccion['hora'])); ?></td>
                                    <td>
                                        <?php 
                                        switch($transaccion['tipoTransaccion']) {
                                            case 1:
                                                echo '<span class="badge bg-warning">' . $lang['withdrawal'] . '</span>';
                                                break;
                                            case 2:
                                                echo '<span class="badge bg-success">' . $lang['deposit'] . '</span>';
                                                break;
                                            case 3:
                                                echo '<span class="badge bg-info">' . $lang['transfer_received'] . '</span>';
                                                break;
                                            case 4:
                                                echo '<span class="badge bg-primary">' . $lang['transfer_sent'] . '</span>';
                                                break;
                                            default:
                                                echo '<span class="badge bg-secondary">' . $lang['other'] . '</span>';
                                        }
                                        ?>
                                    </td>
                                    <td><?php echo $transaccion['descripcion']; ?></td>
                                    <td>
                                        <?php 
                                        if ($this->model->tipoMoneda == 1) {
                                            echo 'Bs. ' . number_format($transaccion['monto'], 2);
                                        } else {
                                            echo '$ ' . number_format($transaccion['monto'], 2);
                                        }
                                        ?>
                                    </td>
                                    <td>
                                        <?php 
                                        if ($this->model->tipoMoneda == 1) {
                                            echo 'Bs. ' . number_format($transaccion['saldoResultante'], 2);
                                        } else {
                                            echo '$ ' . number_format($transaccion['saldoResultante'], 2);
                                        }
                                        ?>
                                    </td>
                                </tr>
                            <?php endforeach; ?>
                        </tbody>
                    </table>
                </div>
                
                <!-- Botones de acción -->
                <div class="text-center mt-4">
                    <button type="button" class="btn btn-primary" onclick="window.print();"><?php echo $lang['print']; ?></button>
                    <a href="index.php?controller=cuenta&action=ver&id=<?php echo $this->model->idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['back']; ?></a>
                </div>
            <?php endif; ?>
        </div>
    </div>
</div>

<style>
/* Estilos para impresión */
@media print {
    .header, .footer, .sidebar, .filter-section, button, .btn {
        display: none !important;
    }
    .card {
        border: none !important;
        box-shadow: none !important;
    }
    body {
        font-size: 12pt;
    }
}
</style>