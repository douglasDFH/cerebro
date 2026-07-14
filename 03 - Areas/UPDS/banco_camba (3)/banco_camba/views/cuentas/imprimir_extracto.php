<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name']; ?> - <?php echo $lang['account_statement']; ?></title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 20px;
            line-height: 1.5;
            color: #333;
        }
        .report-header {
            text-align: center;
            margin-bottom: 20px;
            border-bottom: 1px solid #ddd;
            padding-bottom: 10px;
        }
        .report-logo {
            font-size: 24px;
            font-weight: bold;
            color: #056f1f;
            margin-bottom: 5px;
        }
        .report-title {
            font-size: 18px;
            margin: 10px 0;
        }
        .account-info {
            margin-bottom: 20px;
        }
        .account-info table {
            width: 100%;
            border-collapse: collapse;
        }
        .account-info th, .account-info td {
            padding: 5px;
            text-align: left;
            border-bottom: 1px solid #eee;
        }
        .summary {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
            flex-wrap: wrap;
        }
        .summary-box {
            border: 1px solid #ddd;
            padding: 10px;
            border-radius: 4px;
            width: 22%;
            margin-bottom: 10px;
            background-color: #f9f9f9;
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
        .transaction-type {
            padding: 3px 6px;
            border-radius: 3px;
            font-size: 12px;
        }
        .deposit {
            background-color: #e6f7e6;
            border: 1px solid #c3e6cb;
        }
        .withdrawal {
            background-color: #ffeeba;
            border: 1px solid #ffdf7e;
        }
        .transfer-sent {
            background-color: #cce5ff;
            border: 1px solid #b8daff;
        }
        .transfer-received {
            background-color: #d1ecf1;
            border: 1px solid #bee5eb;
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
                margin: 0;
            }
            .no-print {
                display: none;
            }
        }
    </style>
</head>
<body>
    <div class="report-header">
        <div class="report-logo"><?php echo $lang['app_name']; ?></div>
        <div class="report-title"><?php echo $lang['account_statement']; ?></div>
        <div><?php echo date('d/m/Y H:i:s'); ?></div>
    </div>
    
    <div class="account-info">
        <h3><?php echo $lang['account_information']; ?></h3>
        <table>
            <tr>
                <th><?php echo $lang['account_number']; ?>:</th>
                <td><?php echo $this->model->nroCuenta; ?></td>
                <th><?php echo $lang['client']; ?>:</th>
                <td><?php echo $clienteModel->nombre . ' ' . $clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno; ?></td>
            </tr>
            <tr>
                <th><?php echo $lang['account_type']; ?>:</th>
                <td><?php echo $this->model->tipoCuenta == 1 ? $lang['savings_account'] : $lang['checking_account']; ?></td>
                <th><?php echo $lang['currency']; ?>:</th>
                <td><?php echo $this->model->tipoMoneda == 1 ? $lang['bolivianos'] : $lang['dollars']; ?></td>
            </tr>
            <tr>
                <th><?php echo $lang['period']; ?>:</th>
                <td><?php echo date('d/m/Y', strtotime($fechaInicio)) . ' - ' . date('d/m/Y', strtotime($fechaFin)); ?></td>
                <th><?php echo $lang['current_balance']; ?>:</th>
                <td><strong><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->saldo, 2); ?></strong></td>
            </tr>
        </table>
    </div>
    
    <div class="summary">
        <div class="summary-box">
            <strong><?php echo $lang['deposits']; ?>:</strong>
            <div><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalDepositos, 2); ?></div>
        </div>
        <div class="summary-box">
            <strong><?php echo $lang['withdrawals']; ?>:</strong>
            <div><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalRetiros, 2); ?></div>
        </div>
        <div class="summary-box">
            <strong><?php echo $lang['transfers_received']; ?>:</strong>
            <div><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalTransferenciasEntrantes, 2); ?></div>
        </div>
        <div class="summary-box">
            <strong><?php echo $lang['transfers_sent']; ?>:</strong>
            <div><?php echo ($this->model->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($totalTransferenciasSalientes, 2); ?></div>
        </div>
    </div>
    
    <h3><?php echo $lang['transactions_list']; ?></h3>
    <?php if (empty($transacciones)): ?>
        <p><?php echo $lang['no_transactions_for_period']; ?></p>
    <?php else: ?>
        <table>
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
                                    echo '<span class="transaction-type withdrawal">' . $lang['withdrawal'] . '</span>';
                                    break;
                                case 2:
                                    echo '<span class="transaction-type deposit">' . $lang['deposit'] . '</span>';
                                    break;
                                case 3:
                                    echo '<span class="transaction-type transfer-received">' . $lang['transfer_received'] . '</span>';
                                    break;
                                case 4:
                                    echo '<span class="transaction-type transfer-sent">' . $lang['transfer_sent'] . '</span>';
                                    break;
                                default:
                                    echo '<span class="transaction-type">' . $lang['other'] . '</span>';
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
    <?php endif; ?>
    
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