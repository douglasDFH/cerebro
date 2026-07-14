<div class="card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h2><?php echo $lang['atm_transaction_receipt'] ?? 'Comprobante de Transacción ATM'; ?></h2>
        <div>
            <!-- Botón para imprimir -->
            <button onclick="window.print();" class="btn btn-primary">
                <i class="fa fa-print"></i> <?php echo $lang['print'] ?? 'Imprimir'; ?>
            </button>
            
            <!-- Botón para volver a la vista de detalles -->
            <a href="index.php?controller=transaccionatm&action=ver&id=<?php echo $this->model->idTransaccionATM; ?>" class="btn btn-info">
                <i class="fa fa-eye"></i> <?php echo $lang['view_details'] ?? 'Ver Detalles'; ?>
            </a>
            
            <!-- Botón para volver a la lista -->
            <a href="index.php?controller=transaccionatm&action=listar" class="btn btn-secondary">
                <i class="fa fa-arrow-left"></i> <?php echo $lang['back'] ?? 'Volver'; ?>
            </a>
        </div>
    </div>
    <div class="card-body">
        <div class="receipt-container">
            <!-- Encabezado del comprobante -->
            <div class="receipt-header text-center">
                <h3><?php echo $lang['bank_name'] ?? 'Banco CAMBA'; ?></h3>
                <p><?php echo htmlspecialchars($atm->nombre); ?></p>
                <p><?php echo htmlspecialchars($atm->ubicacion); ?></p>
                <hr>
                <h4>
                    <?php
                    switch($this->model->tipoTransaccion) {
                        case 'retiro':
                            echo $lang['withdrawal_receipt'] ?? 'Comprobante de Retiro';
                            break;
                        case 'deposito':
                            echo $lang['deposit_receipt'] ?? 'Comprobante de Depósito';
                            break;
                        case 'consulta':
                            echo $lang['inquiry_receipt'] ?? 'Comprobante de Consulta';
                            break;
                        default:
                            echo $lang['transaction_receipt'] ?? 'Comprobante de Transacción';
                    }
                    ?>
                </h4>
                <p><?php echo $lang['transaction_id'] ?? 'ID Transacción'; ?>: <?php echo $this->model->idTransaccionATM; ?></p>
                <p><?php echo date('d/m/Y H:i:s', strtotime($this->model->fecha . ' ' . $this->model->hora)); ?></p>
            </div>
            
            <!-- Cuerpo del comprobante -->
            <div class="receipt-body">
                <table class="table receipt-table">
                    <tr>
                        <th><?php echo $lang['account_number'] ?? 'Número de Cuenta'; ?>:</th>
                        <td><?php echo $cuenta->numeroCuenta; ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['client'] ?? 'Cliente'; ?>:</th>
                        <td><?php echo htmlspecialchars($cliente->nombres . ' ' . $cliente->apellidos); ?></td>
                    </tr>
                    <?php if ($this->model->tipoTransaccion == 'retiro'): ?>
                    <tr>
                        <th><?php echo $lang['withdrawal_amount'] ?? 'Monto Retirado'; ?>:</th>
                        <td><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->monto, 2); ?></td>
                    </tr>
                    <?php elseif ($this->model->tipoTransaccion == 'deposito'): ?>
                    <tr>
                        <th><?php echo $lang['deposit_amount'] ?? 'Monto Depositado'; ?>:</th>
                        <td><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->monto, 2); ?></td>
                    </tr>
                    <?php endif; ?>
                    <tr>
                        <th><?php echo $lang['resulting_balance'] ?? 'Saldo Resultante'; ?>:</th>
                        <td><?php echo ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($this->model->saldoResultante, 2); ?></td>
                    </tr>
                    <tr>
                        <th><?php echo $lang['card_number'] ?? 'Número de Tarjeta'; ?>:</th>
                        <td><?php echo substr($tarjeta->numeroTarjeta, 0, 4) . ' **** **** ' . substr($tarjeta->numeroTarjeta, -4); ?></td>
                    </tr>
                    <?php if (!empty($this->model->descripcion)): ?>
                    <tr>
                        <th><?php echo $lang['description'] ?? 'Descripción'; ?>:</th>
                        <td><?php echo htmlspecialchars($this->model->descripcion); ?></td>
                    </tr>
                    <?php endif; ?>
                </table>
            </div>
            
            <!-- Pie del comprobante -->
            <div class="receipt-footer text-center">
                <hr>
                <p class="reference"><?php echo $lang['reference_code'] ?? 'Código de Referencia'; ?>: <?php echo substr($this->model->hash, 0, 8); ?></p>
                <p class="thank-you"><?php echo $lang['thank_you'] ?? 'Gracias por su preferencia'; ?></p>
                <p class="legal-text"><?php echo $lang['receipt_legal_text'] ?? 'Este comprobante es un documento válido de su transacción.'; ?></p>
                <p class="contact"><?php echo $lang['contact_info'] ?? 'Para cualquier consulta, contáctenos al: 0800-0000000'; ?></p>
            </div>
        </div>
    </div>
</div>

<style>
/* Estilos para impresión */
@media print {
    body * {
        visibility: hidden;
    }
    .receipt-container, .receipt-container * {
        visibility: visible;
    }
    .receipt-container {
        position: absolute;
        left: 0;
        top: 0;
        width: 100%;
    }
    .btn, .card-header, .navbar {
        display: none !important;
    }
}

/* Estilos para el comprobante */
.receipt-container {
    max-width: 800px;
    margin: 0 auto;
    padding: 20px;
    border: 1px solid #ddd;
    background-color: #fff;
    box-shadow: 0 0 10px rgba(0,0,0,0.1);
}

.receipt-header, .receipt-footer {
    padding: 10px 0;
}

.receipt-body {
    padding: 20px 0;
}

.receipt-table {
    width: 100%;
    border-collapse: collapse;
}

.receipt-table th, .receipt-table td {
    padding: 8px;
    border-bottom: 1px solid #eee;
}

.receipt-table th {
    text-align: left;
    width: 40%;
}

.reference {
    font-family: monospace;
    font-size: 14px;
}

.thank-you {
    font-size: 18px;
    font-weight: bold;
    margin: 10px 0;
}

.legal-text {
    font-size: 12px;
    font-style: italic;
    color: #666;
}

.contact {
    font-size: 14px;
    margin-top: 10px;
}
</style>