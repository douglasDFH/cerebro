<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name']; ?> - <?php echo $lang['account_activity']; ?></title>
    <link rel="stylesheet" href="assets/css/styles.css">
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
        }
        .report-header {
            text-align: center;
            margin-bottom: 20px;
        }
        .report-logo {
            font-size: 24px;
            font-weight: bold;
            color: #056f1f;
        }
        .report-title {
            font-size: 18px;
            margin: 10px 0;
        }
        .report-info {
            margin-bottom: 20px;
        }
        .account-info {
            border: 1px solid #ddd;
            padding: 10px;
            margin-bottom: 20px;
            border-radius: 4px;
            background-color: #f9f9f9;
        }
        .summary {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }
        .summary-box {
            border: 1px solid #ddd;
            padding: 10px;
            border-radius: 4px;
            width: 30%;
        }
        table {
            width: 100%;
            border-collapse: collapse;
        }
        th, td {
            border: 1px solid #ddd;
            padding: 8px;
            text-align: left;
        }
        th {
            background-color: #f2f2f2;
        }
        .print-footer {
            margin-top: 20px;
            text-align: center;
            font-size: 12px;
            color: #666;
        }
        @media print {
            .no-print {
                display: none;
            }
        }
    </style>
</head>
<body>
    <div class="report-header">
        <div class="report-logo"><?php echo $lang['app_name']; ?></div>
        <div class="report-title"><?php echo $lang['account_activity']; ?></div>
        <div><?php echo date('d/m/Y H:i:s'); ?></div>
    </div>
    
    <div class="account-info">
        <p><strong><?php echo $lang['account_number']; ?>:</strong> <?php echo $cuenta['nroCuenta']; ?></p>
        <p><strong><?php echo $lang['client']; ?>:</strong> <?php echo $cuenta['nombre'] . ' ' . $cuenta['apellidoPaterno'] . ' ' . $cuenta['apellidoMaterno']; ?></p>
        <p><strong><?php echo $lang['identification']; ?>:</strong> <?php echo $cuenta['ci']; ?></p>
        <p><strong><?php echo $lang['balance']; ?>:</strong> <?php echo number_format($cuenta['saldo'], 2); ?> <?php echo $cuenta['tipoMoneda'] == 1 ? 'BOB' : 'USD'; ?></p>
        <p><strong><?php echo $lang['open_date']; ?>:</strong> <?php echo date('d/m/Y', strtotime($cuenta['fechaApertura'])); ?></p>
    </div>
    
    <div class="report-info">
        <p><strong><?php echo $lang['period']; ?>:</strong> <?php echo date('d/m/Y', strtotime($fechaInicio)); ?> - <?php echo date('d/m/Y', strtotime($fechaFin)); ?></p>
    </div>
    
    <div class="summary">
        <div class="summary-box">
            <strong><?php echo $lang['total_transactions']; ?>:</strong> <?php echo $resumen['total_transacciones']; ?>
        </div>
        <div class="summary-box">
            <strong><?php echo $lang['total_deposits']; ?>:</strong> <?php echo $resumen['total_depositos']; ?> (<?php echo number_format($resumen['monto_depositos'], 2); ?>)
        </div>
        <div class="summary-box">
            <strong><?php echo $lang['total_withdrawals']; ?>:</strong> <?php echo $resumen['total_retiros']; ?> (<?php echo number_format($resumen['monto_retiros'], 2); ?>)
        </div>
    </div>
    
    <?php if (count($actividad) > 0): ?>
        <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th><?php echo $lang['date']; ?></th>
                    <th><?php echo $lang['time']; ?></th>
                    <th><?php echo $lang['transaction_type']; ?></th>
                    <th><?php echo $lang['amount']; ?></th>
                    <th><?php echo $lang['resulting_balance']; ?></th>
                    <th><?php echo $lang['description']; ?></th>
                </tr>
            </thead>
            <tbody>
                <?php foreach ($actividad as $a): ?>
                    <tr>
                        <td><?php echo $a['idTransaccion']; ?></td>
                        <td><?php echo date('d/m/Y', strtotime($a['fecha'])); ?></td>
                        <td><?php echo $a['hora']; ?></td>
                        <td>
                            <?php echo $a['tipoTransaccion'] == 1 ? $lang['withdrawal'] : $lang['deposit']; ?>
                        </td>
                        <td><?php echo number_format($a['monto'], 2); ?></td>
                        <td><?php echo number_format($a['saldoResultante'], 2); ?></td>
                        <td><?php echo $a['descripcion']; ?></td>
                    </tr>
                <?php endforeach; ?>
            </tbody>
        </table>
    <?php else: ?>
        <p><?php echo $lang['no_transactions_found']; ?></p>
    <?php endif; ?>
    
    <div class="print-footer">
        <?php echo $lang['app_name']; ?> &copy; <?php echo date('Y'); ?>
    </div>
    
    <div class="no-print" style="margin-top: 20px; text-align: center;">
        <button onclick="window.print()" class="btn btn-primary"><?php echo $lang['print']; ?></button>
        <button onclick="window.close()" class="btn btn-secondary"><?php echo $lang['close']; ?></button>
    </div>
</body>
</html>