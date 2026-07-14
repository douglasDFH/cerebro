<h1><?php echo $lang['transaction_history']; ?></h1>

<div class="card">
    <div class="card-header">
        <div class="d-flex justify-content-between align-items-center">
            <h3>
                <?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?> - 
                <?php echo $lang['account_number']; ?>: <?php echo htmlspecialchars($cuenta->nroCuenta); ?>
            </h3>
            <div>
                <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta->idCuenta; ?>" class="btn btn-secondary">
                    <i class="icon-arrow-left"></i> <?php echo $lang['back']; ?>
                </a>
            </div>
        </div>
    </div>
    <div class="card-body">
        <!-- Filtro por fechas -->
        <form method="POST" action="index.php?controller=cuenta&action=historial&id=<?php echo $cuenta->idCuenta; ?>" class="mb-4">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fechaInicio"><?php echo $lang['start_date']; ?></label>
                        <input type="date" class="form-control" id="fechaInicio" name="fechaInicio" value="<?php echo $fechaInicio; ?>" required>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="fechaFin"><?php echo $lang['end_date']; ?></label>
                        <input type="date" class="form-control" id="fechaFin" name="fechaFin" value="<?php echo $fechaFin; ?>" required>
                    </div>
                </div>
                <div class="col-md-4" style="display: flex; align-items: flex-end;">
                    <div class="form-group" style="width: 100%;">
                        <button type="submit" class="btn btn-primary">
                            <i class="icon-search"></i> <?php echo $lang['search']; ?>
                        </button>
                        <a href="index.php?controller=cuenta&action=imprimirHistorial&id=<?php echo $cuenta->idCuenta; ?>&inicio=<?php echo $fechaInicio; ?>&fin=<?php echo $fechaFin; ?>" target="_blank" class="btn btn-info">
                            <i class="icon-printer"></i> <?php echo $lang['print']; ?>
                        </a>
                    </div>
                </div>
            </div>
        </form>
        
        <!-- Resumen de transacciones -->
        <div class="row mb-4">
            <div class="col-md-6">
                <div class="card bg-light">
                    <div class="card-body text-center">
                        <h4><?php echo $lang['current_balance']; ?></h4>
                        <h2 style="color: #056f1f;">
                            <?php
                            $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                            echo $moneda . number_format($cuenta->saldo, 2);
                            ?>
                        </h2>
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card bg-light">
                    <div class="card-body text-center">
                        <h4><?php echo $lang['transactions_for_period']; ?></h4>
                        <h2><?php echo count($transacciones); ?></h2>
                        <p>
                            <?php echo $fechaInicio; ?> - <?php echo $fechaFin; ?>
                        </p>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Tabla de transacciones -->
        <?php if (count($transacciones) > 0): ?>
            <div class="table-responsive">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo $lang['transaction_type']; ?></th>
                            <th><?php echo $lang['amount']; ?></th>
                            <th><?php echo $lang['date']; ?></th>
                            <th><?php echo $lang['time']; ?></th>
                            <th><?php echo $lang['description']; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php 
                        // Calcular totales
                        $totalDepositos = 0;
                        $totalRetiros = 0;
                        
                        foreach ($transacciones as $transaccion): 
                            if ($transaccion['tipoTransaccion'] == 1) { // Retiro
                                $totalRetiros += $transaccion['monto'];
                            } else { // Depósito
                                $totalDepositos += $transaccion['monto'];
                            }
                        ?>
                            <tr>
                                <td><?php echo $transaccion['idTransaccion']; ?></td>
                                <td>
                                    <?php if ($transaccion['tipoTransaccion'] == 1): ?>
                                        <span class="badge" style="background-color: #dc3545;"><?php echo $lang['withdrawal']; ?></span>
                                    <?php else: ?>
                                        <span class="badge" style="background-color: #28a745;"><?php echo $lang['deposit']; ?></span>
                                    <?php endif; ?>
                                </td>
                                <td>
                                    <?php
                                    $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                                    echo $moneda . number_format($transaccion['monto'], 2);
                                    ?>
                                </td>
                                <td><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                                <td><?php echo $transaccion['hora']; ?></td>
                                <td><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
                            </tr>
                        <?php endforeach; ?>
                    </tbody>
                    <tfoot>
                        <tr>
                            <th colspan="2"><?php echo $lang['totals']; ?></th>
                            <th>
                                <span style="color: #28a745;">
                                    <?php 
                                    $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                                    echo $moneda . number_format($totalDepositos, 2); 
                                    ?>
                                </span>
                                <br>
                                <span style="color: #dc3545;">
                                    <?php 
                                    echo $moneda . number_format($totalRetiros, 2); 
                                    ?>
                                </span>
                            </th>
                            <th colspan="3"></th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        <?php else: ?>
            <div class="alert alert-info">
                <?php echo $lang['no_transactions_found']; ?>
            </div>
        <?php endif; ?>
    </div>
</div>