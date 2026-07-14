<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name']; ?> - <?php echo $lang['transaction_history']; ?></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            color: #333;
        }
        .header {
            text-align: center;
            margin-bottom: 30px;
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
        }
        .logo {
            font-size: 24px;
            font-weight: bold;
            color: #056f1f;
            margin-bottom: 5px;
        }
        .bank-info {
            font-size: 14px;
            margin-bottom: 10px;
        }
        .report-title {
            font-size: 18px;
            margin-bottom: 5px;
        }
        .report-date {
            font-size: 12px;
            color: #666;
        }
        .account-info {
            margin-bottom: 20px;
        }
        .account-info table {
            width: 100%;
            border-collapse: collapse;
        }
        .account-info th, .account-info td {
            padding: 8px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        .transaction-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        .transaction-table th {
            background-color: #f2f2f2;
            font-weight: bold;
        }
        .transaction-table th, .transaction-table td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        .transaction-table tr:nth-child(even) {
            background-color: #f9f9f9;
        }
        .transaction-type {
            font-weight: bold;
        }
        .deposit {
            color: #28a745;
        }
        .withdrawal {
            color: #dc3545;
        }
        .summary {
            margin-top: 20px;
            border-top: 1px solid #ddd;
            padding-top: 10px;
        }
        .summary-table {
            width: 60%;
            margin: 0 auto;
        }
        .summary-table th, .summary-table td {
            padding: 8px;
            text-align: left;
        }
        .summary-table .amount {
            text-align: right;
            font-weight: bold;
        }
        .footer {
            margin-top: 30px;
            text-align: center;
            font-size: 12px;
            color: #666;
            border-top: 1px solid #ddd;
            padding-top: 10px;
        }
        .print-buttons {
            text-align: center;
            margin-top: 20px;
        }
        .btn {
            display: inline-block;
            padding: 8px 16px;
            background-color: #056f1f;
            color: white;
            text-decoration: none;
            border-radius: 4px;
            font-size: 14px;
            margin: 0 5px;
            cursor: pointer;
            border: none;
        }
        .btn-secondary {
            background-color:rgb(17, 7, 135);
        }
        @media print {
            .print-buttons {
                display: none;
            }
        }
    </style>
</head>
<body>
    <div class="header">
        <div class="logo"><?php echo $lang['app_name']; ?></div>
        <div class="bank-info"><?php echo $lang['bank_address']; ?></div>
        <div class="report-title"><?php echo $lang['transaction_history']; ?></div>
        <div class="report-date"><?php echo $lang['generated_on']; ?>: <?php echo date('d/m/Y H:i:s'); ?></div>
    </div>
    
    <div class="account-info">
        <table>
            <tr>
                <th><?php echo $lang['client']; ?>:</th>
                <td><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?></td>
                <th><?php echo $lang['id_number']; ?>:</th>
                <td><?php echo htmlspecialchars($cliente->ci); ?></td>
            </tr>
            <tr>
                <th><?php echo $lang['account_number']; ?>:</th>
                <td><?php echo htmlspecialchars($cuenta->nroCuenta); ?></td>
                <th><?php echo $lang['account_type']; ?>:</th>
                <td>
                    <?php
                    if ($cuenta->tipoCuenta == 1) {
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
                    if ($cuenta->tipoMoneda == 1) {
                        echo $lang['bolivianos'];
                    } else {
                        echo $lang['dollars'];
                    }
                    ?>
                </td>
                <th><?php echo $lang['current_balance']; ?>:</th>
                <td>
                    <?php
                    $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                    echo $moneda . number_format($cuenta->saldo, 2);
                    ?>
                </td>
            </tr>
            <tr>
                <th><?php echo $lang['period']; ?>:</th>
                <td colspan="3"><?php echo date('d/m/Y', strtotime($fechaInicio)); ?> - <?php echo date('d/m/Y', strtotime($fechaFin)); ?></td>
            </tr>
        </table>
    </div>
    
    <?php if (count($transacciones) > 0): ?>
        <table class="transaction-table">
            <thead>
                <tr>
                    <th><?php echo $lang['date']; ?></th>
                    <th><?php echo $lang['time']; ?></th>
                    <th><?php echo $lang['transaction_type']; ?></th>
                    <th><?php echo $lang['description']; ?></th>
                    <th><?php echo $lang['amount']; ?></th>
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
                        <td><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                        <td><?php echo $transaccion['hora']; ?></td>
                        <td class="transaction-type <?php echo ($transaccion['tipoTransaccion'] == 1) ? 'withdrawal' : 'deposit'; ?>">
                            <?php
                            if ($transaccion['tipoTransaccion'] == 1) {
                                echo $lang['withdrawal'];
                            } else {
                                echo $lang['deposit'];
                            }
                            ?>
                        </td>
                        <td><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
                        <td style="text-align: right;">
                            <?php
                            $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                            echo $moneda . number_format($transaccion['monto'], 2);
                            ?>
                        </td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
        
        <div class="summary">
            <table class="summary-table">
                <tr>
                    <th><?php echo $lang['total_transactions']; ?>:</th>
                    <td class="amount"><?php echo count($transacciones); ?></td>
                </tr>
                <tr>
                    <th><?php echo $lang['total_deposits']; ?>:</th>
                    <td class="amount" style="color: #28a745;">
                        <?php
                        $moneda = ($cuenta->tipoMoneda == 1) ? 'Bs. ' : '$ ';
                        echo $moneda . number_format($totalDepositos, 2);
                        ?>
                    </td>
                </tr>
                <tr>
                    <th><?php echo $lang['total_withdrawals']; ?>:</th>
                    <td class="amount" style="color: #dc3545;">
                        <?php
                        echo $moneda . number_format($totalRetiros, 2);
                        ?>
                    </td>
                </tr>
                <tr>
                    <th><?php echo $lang['net_movement']; ?>:</th>
                    <td class="amount" style="color: <?php echo ($totalDepositos - $totalRetiros >= 0) ? '#28a745' : '#dc3545'; ?>">
                        <?php
                        echo $moneda . number_format($totalDepositos - $totalRetiros, 2);
                        ?>
                    </td>
                </tr>
            </table>
        </div>
    <?php else: ?>
        <div style="text-align: center; padding: 20px; background-color: #f8f9fa; border-radius: 4px;">
            <p><?php echo $lang['no_transactions_found']; ?></p>
        </div>
    <?php endif; ?>
    
    <div class="footer">
        <p><?php echo $lang['app_name']; ?> &copy; <?php echo date('Y'); ?> - <?php echo $lang['transaction_report_footer']; ?></p>
    </div>
    
    <div class="print-buttons">
        <button onclick="window.print();" class="btn"><?php echo $lang['print']; ?></button>
        <button onclick="window.close();" class="btn btn-secondary"><?php echo $lang['close']; ?></button>
    </div>
</body>
</html>