<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name']; ?> - <?php echo $lang['account_movements']; ?></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            line-height: 1.5;
            color: #333;
        }
        .header {
            text-align: center;
            margin-bottom: 20px;
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
        }
        .logo {
            font-size: 24px;
            font-weight: bold;
            color: #056f1f;
            margin-bottom: 5px;
        }
        .title {
            font-size: 18px;
            margin: 10px 0;
        }
        .info-section {
            margin-bottom: 20px;
        }
        .info-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .info-table th, .info-table td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #eee;
        }
        .summary-box {
            margin-bottom: 20px;
            padding: 10px;
            background-color: #f9f9f9;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .summary-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 10px;
        }
        .summary-item {
            flex: 1;
            padding: 0 10px;
        }
        .summary-item-title {
            font-weight: bold;
            margin-bottom: 5px;
        }
        .table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .table th, .table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .table th {
            background-color: #f2f2f2;
        }
        .initial-row {
            background-color: #e9ecef;
        }
        .final-row {
            background-color: #cfe2ff;
        }
        .print-footer {
            margin-top: 20px;
            text-align: center;
            font-size: 12px;
            color: #666;
            border-top: 1px solid #ddd;
            padding-top: 10px;
        }
        @media print {
            body {
                padding: 0;
                margin: 10mm;
            }
            .no-print {
                display: none;
            }
        }
    </style>
</head>
<body>
    <div class="header">
        <div class="logo"><?php echo $lang['app_name']; ?></div>
        <div class="title"><?php echo $lang['account_movements']; ?></div>
        <div><?php echo date('d/m/Y H:i:s'); ?></div>
    </div>
    
    <div class="info-section">
        <h3><?php echo $lang['account_information']; ?></h3>
        <table class="info-table">
            <tr>
                <th><?php echo $lang['account_number']; ?>:</th>
                <td><?php echo $cuenta->nroCuenta; ?></td>
                <th><?php echo $lang['client']; ?>:</th>
                <td><?php echo $cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno; ?></td>
            </tr>
            <tr>
                <th><?php echo $lang['account_type']; ?>:</th>
                <td><?php echo $cuenta->tipoCuenta == 1 ? $lang['savings_account'] : $lang['checking_account']; ?></td>
                <th><?php echo $lang['currency']; ?>:</th>
                <td><?php echo $cuenta->tipoMoneda == 1 ? $lang['bolivianos'] : $lang['dollars']; ?></td>
            </tr>
            <tr>
                <th><?php echo $lang['period']; ?>:</th>
                <td colspan="3"><?php echo date('d/m/Y', strtotime($fechaInicio)) . ' - ' . date('d/m/Y', strtotime($fechaFin)); ?></td>
            </tr>
        </table>
    </div>
    
    <div class="summary-box">
        <h3><?php echo $lang['account_summary']; ?></h3>
        <div class="summary-row">
            <div class="summary-item">
                <div class="summary-item-title"><?php echo $lang['initial_balance']; ?></div>
                <div><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($saldoInicial, 2); ?></div>
            </div>
            <div class="summary-item">
                <div class="summary-item-title"><?php echo $lang['total_deposits']; ?></div>
                <div><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalIngresos, 2); ?></div>
            </div>
            <div class="summary-item">
                <div class="summary-item-title"><?php echo $lang['total_withdrawals']; ?></div>
                <div><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalEgresos, 2); ?></div>
            </div>
            <div class="summary-item">
                <div class="summary-item-title"><?php echo $lang['current_balance']; ?></div>
                <div><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($saldoFinal, 2); ?></div>
            </div>
        </div>
    </div>
    
    <div class="movements-section">
        <h3><?php echo $lang['movement_details']; ?></h3>
        <?php if (empty($movimientos)): ?>
            <p><?php echo $lang['no_movements_for_period']; ?></p>
        <?php else: ?>
            <table class="table">
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
                    <tr class="initial-row">
                        <td><?php echo date('d/m/Y', strtotime($fechaInicio)); ?></td>
                        <td>00:00:00</td>
                        <td><strong><?php echo $lang['initial_balance']; ?></strong></td>
                        <td>-</td>
                        <td>-</td>
                        <td>-</td>
                        <td>
                            <strong>
                                <?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($saldoInicial, 2); ?>
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
                            <td><?php echo $movimiento['idTransaccion']; ?></td>
                            <td>
                                <?php 
                                // Mostrar monto en columna de débito para retiros y transferencias salientes
                                if ($movimiento['tipoTransaccion'] == 1 || $movimiento['tipoTransaccion'] == 4) {
                                    echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($movimiento['monto'], 2);
                                } else {
                                    echo '-';
                                }
                                ?>
                            </td>
                            <td>
                                <?php 
                                // Mostrar monto en columna de crédito para depósitos y transferencias entrantes
                                if ($movimiento['tipoTransaccion'] == 2 || $movimiento['tipoTransaccion'] == 3) {
                                    echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($movimiento['monto'], 2);
                                } else {
                                    echo '-';
                                }
                                ?>
                            </td>
                            <td>
                                <?php 
                                // Saldo actualizado
                                echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($saldoActual, 2);
                                ?>
                            </td>
                        </tr>
                    <?php endforeach; ?>
                    
                    <!-- Saldo final -->
                    <tr class="final-row">
                        <td><?php echo date('d/m/Y', strtotime($fechaFin)); ?></td>
                        <td>23:59:59</td>
                        <td><strong><?php echo $lang['ending_balance']; ?></strong></td>
                        <td>-</td>
                        <td>-</td>
                        <td>-</td>
                        <td>
                            <strong>
                                <?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($saldoFinal, 2); ?>
                            </strong>
                        </td>
                    </tr>
                </tbody>
            </table>
        <?php endif; ?>
    </div>
    
    <div class="print-footer">
        <p><?php echo $lang['app_name']; ?> &copy; <?php echo date('Y'); ?></p>
        <p><?php echo $lang['print_disclaimer']; ?></p>
    </div>
    
    <div class="no-print" style="margin-top: 20px; text-align: center;">
        <button onclick="window.print()" class="btn-print"><?php echo $lang['print']; ?></button>
        <button onclick="window.close()" class="btn-close"><?php echo $lang['close']; ?></button>
    </div>
</body>
</html>