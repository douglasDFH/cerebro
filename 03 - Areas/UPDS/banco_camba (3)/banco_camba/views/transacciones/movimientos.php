<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h2><?php echo $lang['account_movements']; ?></h2>
        <div>
            <a href="index.php?controller=cuenta&action=ver&id=<?php echo $idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['back']; ?></a>
        </div>
    </div>
    <div class="card-body">
        <!-- Información de la cuenta -->
        <div class="row mb-4">
            <div class="col-md-6">
                <h4><?php echo $lang['account_information']; ?></h4>
                <table class="table">
                    <tr>
                        <th><?php echo $lang['account_number']; ?>:</th>
                        <td><?php echo $cuenta->nroCuenta; ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['account_type']; ?>:</th>
                        <td>
                            <?php echo $cuenta->tipoCuenta == 1 ? $lang['savings_account'] : $lang['checking_account']; ?>
                        </td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['currency']; ?>:</th>
                        <td>
                            <?php echo $cuenta->tipoMoneda == 1 ? $lang['bolivianos'] : $lang['dollars']; ?>
                        </td>
                    </tr>
                </table>
            </div>
            
            <div class="col-md-6">
                <h4><?php echo $lang['client_information']; ?></h4>
                <table class="table">
                    <tr>
                        <th><?php echo $lang['name']; ?>:</th>
                        <td><?php echo $cliente->nombre; ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['last_name']; ?>:</th>
                        <td><?php echo $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno; ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['id_number']; ?>:</th>
                        <td><?php echo $cliente->ci; ?></td>
                    </tr>
                </table>
            </div>
        </div>
        
        <!-- Filtro de fechas -->
        <div class="row mb-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h5><?php echo $lang['filter_by_date']; ?></h5>
                    </div>
                    <div class="card-body">
                        <form method="GET" action="index.php" class="row">
                            <input type="hidden" name="controller" value="transaccion">
                            <input type="hidden" name="action" value="movimientos">
                            <input type="hidden" name="idCuenta" value="<?php echo $idCuenta; ?>">
                            
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
                </div>
            </div>
        </div>
        
        <!-- Resumen de transacciones -->
        <div class="row mb-4">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-header">
                        <h5><?php echo $lang['account_summary']; ?></h5>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="card bg-light">
                                    <div class="card-body">
                                        <h6 class="card-title"><?php echo $lang['initial_balance']; ?></h6>
                                        <p class="card-text">
                                            <?php
                                            $monedaSymbol = $cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ';
                                            echo $monedaSymbol . number_format($saldoInicial, 2);
                                            ?>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-3">
                                <div class="card bg-success text-white">
                                    <div class="card-body">
                                        <h6 class="card-title"><?php echo $lang['total_deposits']; ?></h6>
                                        <p class="card-text">
                                            <?php echo $monedaSymbol . number_format($totalIngresos, 2); ?>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-3">
                                <div class="card bg-danger text-white">
                                    <div class="card-body">
                                        <h6 class="card-title"><?php echo $lang['total_withdrawals']; ?></h6>
                                        <p class="card-text">
                                            <?php echo $monedaSymbol . number_format($totalEgresos, 2); ?>
                                        </p>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="col-md-3">
                                <div class="card bg-primary text-white">
                                    <div class="card-body">
                                        <h6 class="card-title"><?php echo $lang['current_balance']; ?></h6>
                                        <p class="card-text">
                                            <?php echo $monedaSymbol . number_format($saldoFinal, 2); ?>
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Tabla de movimientos -->
        <div class="card">
            <div class="card-header">
                <h5><?php echo $lang['movement_details']; ?></h5>
            </div>
            <div class="card-body">
                <?php if (empty($movimientos)): ?>
                    <div class="alert alert-info"><?php echo $lang['no_movements_for_period']; ?></div>
                <?php else: ?>
                    <div class="table-responsive">
                        <table class="table table-striped table-hover">
                            <thead>
                                <tr>
                                    <th><?php echo $lang['date']; ?></th>
                                    <th><?php echo $lang['time']; ?></th>
                                    <th><?php echo $lang['description']; ?></th>
                                    <th><?php echo $lang['reference']; ?></th>
                                    <th><?php echo $lang['debit']; ?></th>
                                    <th><?php echo $lang['credit']; ?></th>
                                    <th><?php echo $lang['balance']; ?></th>
                                </tr>
                            </thead>
                            <tbody>
                                <!-- Saldo inicial -->
                                <tr class="table-secondary">
                                    <td><?php echo date('d/m/Y', strtotime($fechaInicio)); ?></td>
                                    <td>00:00:00</td>
                                    <td><strong><?php echo $lang['initial_balance']; ?></strong></td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>
                                        <strong>
                                            <?php echo $monedaSymbol . number_format($saldoInicial, 2); ?>
                                        </strong>
                                    </td>
                                </tr>
                                
                                <!-- Movimientos -->
                                <?php 
                                $saldoActual = $saldoInicial;
                                foreach ($movimientos as $movimiento): 
                                    // Actualizar saldo actual según el tipo de transacción
                                    if ($movimiento['tipoTransaccion'] == 1 || $movimiento['tipoTransaccion'] == 4) {
                                        // Retiro o transferencia saliente (resta)
                                        $saldoActual -= $movimiento['monto'];
                                    } else {
                                        // Depósito o transferencia entrante (suma)
                                        $saldoActual += $movimiento['monto'];
                                    }
                                ?>
                                    <tr>
                                        <td><?php echo date('d/m/Y', strtotime($movimiento['fecha'])); ?></td>
                                        <td><?php echo date('H:i:s', strtotime($movimiento['hora'])); ?></td>
                                        <td>
                                            <?php 
                                            echo $movimiento['descripcion'];
                                            // Añadir información adicional para transferencias
                                            if ($movimiento['tipoTransaccion'] == 3 && !empty($movimiento['cuentaOrigen'])) {
                                                echo ' (' . $lang['from'] . ' ' . $movimiento['cuentaOrigen'] . ')';
                                            } elseif ($movimiento['tipoTransaccion'] == 4 && !empty($movimiento['cuentaDestino'])) {
                                                echo ' (' . $lang['to'] . ' ' . $movimiento['cuentaDestino'] . ')';
                                            }
                                            ?>
                                        </td>
                                        <td>
                                            <a href="index.php?controller=transaccion&action=ver&id=<?php echo $movimiento['idTransaccion']; ?>" class="btn btn-sm btn-info">
                                                <?php echo $movimiento['idTransaccion']; ?>
                                            </a>
                                        </td>
                                        <td>
                                            <?php 
                                            // Mostrar monto en columna de débito para retiros y transferencias salientes
                                            if ($movimiento['tipoTransaccion'] == 1 || $movimiento['tipoTransaccion'] == 4) {
                                                echo $monedaSymbol . number_format($movimiento['monto'], 2);
                                            } else {
                                                echo '-';
                                            }
                                            ?>
                                        </td>
                                        <td>
                                            <?php 
                                            // Mostrar monto en columna de crédito para depósitos y transferencias entrantes
                                            if ($movimiento['tipoTransaccion'] == 2 || $movimiento['tipoTransaccion'] == 3) {
                                                echo $monedaSymbol . number_format($movimiento['monto'], 2);
                                            } else {
                                                echo '-';
                                            }
                                            ?>
                                        </td>
                                        <td>
                                            <?php 
                                            // Saldo actualizado
                                            echo $monedaSymbol . number_format($saldoActual, 2);
                                            ?>
                                        </td>
                                    </tr>
                                <?php endforeach; ?>
                                
                                <!-- Saldo final -->
                                <tr class="table-primary">
                                    <td><?php echo date('d/m/Y', strtotime($fechaFin)); ?></td>
                                    <td>23:59:59</td>
                                    <td><strong><?php echo $lang['ending_balance']; ?></strong></td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>-</td>
                                    <td>
                                        <strong>
                                            <?php echo $monedaSymbol . number_format($saldoFinal, 2); ?>
                                        </strong>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                <?php endif; ?>
            </div>
        </div>
        
        <!-- Botones de acción -->
        <div class="text-center mt-4">
            <button type="button" class="btn btn-primary" onclick="imprimirMovimientos(<?php echo $idCuenta; ?>, '<?php echo $fechaInicio; ?>', '<?php echo $fechaFin; ?>');">
                <i class="fa fa-print"></i> <?php echo $lang['print']; ?>
            </button>
            <a href="index.php?controller=cuenta&action=ver&id=<?php echo $idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['back']; ?></a>
        </div>
    </div>
</div>

<script>
function imprimirMovimientos(idCuenta, fechaInicio, fechaFin) {
    var url = 'index.php?controller=transaccion&action=imprimirMovimientos&idCuenta=' + idCuenta + '&fechaInicio=' + fechaInicio + '&fechaFin=' + fechaFin;
    window.open(url, '_blank', 'width=800,height=600');
}
</script>