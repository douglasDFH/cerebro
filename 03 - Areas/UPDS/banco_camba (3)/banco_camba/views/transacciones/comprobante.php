<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo isset($lang['transaction_receipt']) ? $lang['transaction_receipt'] : 'Comprobante de Transacción'; ?> - <?php echo isset($lang['bank_name']) ? $lang['bank_name'] : 'Banco Mercantil'; ?></title>
    <link rel="stylesheet" href="assets/css/StyleComprobante.css">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    
</head>
<body>
    <div class="container">
        <div class="receipt-container">
            <!-- Marca de agua -->
            <div class="watermark"><?php echo isset($lang['receipt']) ? $lang['receipt'] : 'COMPROBANTE'; ?></div>
            
            <!-- Encabezado del comprobante -->
            <div class="receipt-header">
                <div class="bank-logo">
                    <img src="assets/img/logo.png" alt="<?php echo isset($lang['bank_name']) ? $lang['bank_name'] : 'Banco Mercantil'; ?> Logo">
                    <div class="bank-name"><?php echo isset($lang['bank_name']) ? $lang['bank_name'] : 'Banco Mercantil'; ?></div>
                </div>
                <h1 class="receipt-title"><?php echo isset($lang['transaction_receipt_title']) ? $lang['transaction_receipt_title'] : 'COMPROBANTE DE TRANSACCIÓN'; ?></h1>
                <!-- Información de transacción en el encabezado -->
                <div class="header-transaction-info">
                    <div class="header-transaction-row">
                        <div class="header-transaction-item">
                            <span class="header-label"><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?>: </span>
                            <span class="header-value"><?php echo date('d/m/Y', strtotime($this->model->fecha)); ?></span>
                        </div>
                        <div class="header-transaction-item">
                            <span class="header-label"><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?>: </span>
                            <span class="header-value"><?php echo date('H:i:s', strtotime($this->model->hora)); ?></span>
                        </div>
                        <div class="header-transaction-item">
                            <span class="header-label"><?php echo isset($lang['id']) ? $lang['id'] : 'ID'; ?>: </span>
                            <span class="header-value"><?php echo $this->model->idTransaccion; ?></span>
                        </div>
                    </div>
                </div>
                <!-- Subtítulo dinámico según el tipo de transacción -->
                <?php 
                $subtitleClass = '';
                switch($this->model->tipoTransaccion) {
                    case 1:
                        $receiptType = isset($lang['withdrawal_receipt']) ? $lang['withdrawal_receipt'] : 'COMPROBANTE DE RETIRO';
                        $transactionClass = 'withdrawal';
                        $transactionName = isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro';
                        break;
                    case 2:
                        $receiptType = isset($lang['deposit_receipt']) ? $lang['deposit_receipt'] : 'COMPROBANTE DE DEPÓSITO';
                        $transactionClass = 'deposit';
                        $transactionName = isset($lang['deposit']) ? $lang['deposit'] : 'Depósito';
                        break;
                    case 3:
                        $receiptType = isset($lang['transfer_received_receipt']) ? $lang['transfer_received_receipt'] : 'COMPROBANTE DE TRANSFERENCIA RECIBIDA';
                        $transactionClass = 'transfer-in';
                        $transactionName = isset($lang['transfer_received']) ? $lang['transfer_received'] : 'Transferencia Recibida';
                        break;
                    case 4:
                        $receiptType = isset($lang['transfer_sent_receipt']) ? $lang['transfer_sent_receipt'] : 'COMPROBANTE DE TRANSFERENCIA ENVIADA';
                        $transactionClass = 'transfer-out';
                        $transactionName = isset($lang['transfer_sent']) ? $lang['transfer_sent'] : 'Transferencia Enviada';
                        break;
                    default:
                        $receiptType = isset($lang['transaction_receipt']) ? $lang['transaction_receipt'] : 'COMPROBANTE DE TRANSACCIÓN';
                        $transactionClass = '';
                        $transactionName = isset($lang['other']) ? $lang['other'] : 'Otro';
                }
                ?>
                <p class="receipt-subtitle"><?php echo $receiptType; ?></p>
            </div>
            
            <!-- Cuerpo del comprobante -->
            <div class="receipt-body">
               
                <!-- Información de seguridad -->
                <div class="security-info">
                    <p>
                        <i class="fas fa-shield-alt"></i>
                        <?php echo isset($lang['receipt_security_message']) ? $lang['receipt_security_message'] : 'Este documento es un comprobante oficial de Banco Mercantil. Verifique los datos antes de finalizar.'; ?>
                    </p>
                </div>
                
                <!-- Detalles de la transacción -->
                <div class="receipt-details">
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-university"></i>
                            <span><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $cuenta->nroCuenta; ?></div>
                    </div>
                    
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-user"></i>
                            <span><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno; ?></div>
                    </div>
                    
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-exchange-alt"></i>
                            <span><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $transactionName; ?></div>
                    </div>
                    
                    <?php if (!empty($this->model->cuentaOrigen)): ?>
                        <div class="detail-row">
                            <div class="detail-label">
                                <i class="fas fa-sign-out-alt"></i>
                                <span><?php echo isset($lang['source_account']) ? $lang['source_account'] : 'Cuenta Origen'; ?></span>
                            </div>
                            <div class="detail-value"><?php echo $this->model->cuentaOrigen; ?></div>
                        </div>
                    <?php endif; ?>
                    
                    <?php if (!empty($this->model->cuentaDestino)): ?>
                        <div class="detail-row">
                            <div class="detail-label">
                                <i class="fas fa-sign-in-alt"></i>
                                <span><?php echo isset($lang['destination_account']) ? $lang['destination_account'] : 'Cuenta Destino'; ?></span>
                            </div>
                            <div class="detail-value"><?php echo $this->model->cuentaDestino; ?></div>
                        </div>
                    <?php endif; ?>
                    
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-align-left"></i>
                            <span><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $this->model->descripcion; ?></div>
                    </div>
                    
                    <!-- Información adicional - Sucursal -->
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-map-marker-alt"></i>
                            <span><?php echo isset($lang['branch']) ? $lang['branch'] : 'Sucursal'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $cuenta->sucursal ?? (isset($lang['head_office']) ? $lang['head_office'] : 'Casa Matriz'); ?></div>
                    </div>
                    
                    <!-- Cajero/Atendido por -->
                    <div class="detail-row">
                        <div class="detail-label">
                            <i class="fas fa-user-tie"></i>
                            <span><?php echo isset($lang['attended_by']) ? $lang['attended_by'] : 'Atendido por'; ?></span>
                        </div>
                        <div class="detail-value"><?php echo $this->model->usuario ?? (isset($lang['online_transaction']) ? $lang['online_transaction'] : 'Transacción en línea'); ?></div>
                    </div>
                </div>
                
                <!-- Sección de montos -->
                <div class="amount-section">
                    <div class="amount-row">
                        <div class="amount-label">
                            <i class="fas fa-money-bill-wave"></i>
                            <span><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></span>
                        </div>
                        <div class="amount-value">
                            <?php 
                            if ($cuenta->tipoMoneda == 1) {
                                echo 'Bs. ' . number_format($this->model->monto, 2);
                            } else {
                                echo '$ ' . number_format($this->model->monto, 2);
                            }
                            ?>
                        </div>
                    </div>
                    
                    <div class="amount-row">
                        <div class="amount-label">
                            <i class="fas fa-wallet"></i>
                            <span><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?></span>
                        </div>
                        <div class="amount-value result">
                            <?php 
                            if ($cuenta->tipoMoneda == 1) {
                                echo 'Bs. ' . number_format($this->model->saldoResultante, 2);
                            } else {
                                echo '$ ' . number_format($this->model->saldoResultante, 2);
                            }
                            ?>
                        </div>
                    </div>
                </div>
                
                <!-- Validación con QR corregido -->
                <div class="receipt-validation">
                    <div class="qr-container">
                        <div class="qr-code">
                            <img src="assets/img/QR.png" alt="<?php echo isset($lang['verification_qr_code']) ? $lang['verification_qr_code'] : 'Código QR de verificación'; ?>">
                        </div>
                    </div>
                    <p><?php echo isset($lang['scan_qr_code']) ? $lang['scan_qr_code'] : 'Escanee el código QR para verificar la autenticidad'; ?></p>
                    <div class="hash-reference">
                        <small><?php echo isset($lang['transaction_reference']) ? $lang['transaction_reference'] : 'Referencia'; ?>: <?php echo $this->model->hash; ?></small>
                    </div>
                </div>
                
                <!-- Pie del comprobante -->
                <div class="receipt-footer">
                    <p><strong><?php echo isset($lang['receipt_thank_you']) ? $lang['receipt_thank_you'] : '¡Gracias por confiar en Banco Mercantil!'; ?></strong></p>
                    <p><?php echo isset($lang['receipt_valid_without_signature']) ? $lang['receipt_valid_without_signature'] : 'Este comprobante es válido sin firma ni sello.'; ?></p>
                    <p><?php echo isset($lang['receipt_contact_info']) ? $lang['receipt_contact_info'] : 'Para cualquier consulta, comuníquese al 0800-BANCO (22626)'; ?></p>
                    <p class="timestamp"><?php echo isset($lang['generated_on']) ? $lang['generated_on'] : 'Generado el'; ?> <?php echo date('d/m/Y H:i:s'); ?></p>
                </div>
            </div>
            
            <!-- Botones de acción -->
            <div class="actions-footer">
                <button type="button" class="btn btn-primary" onclick="window.print();">
                    <i class="fas fa-print"></i> <?php echo isset($lang['print']) ? $lang['print'] : 'Imprimir'; ?>
                </button>
                
                <a href="index.php?controller=transaccion&action=ver&id=<?php echo $this->model->idTransaccion; ?>" class="btn btn-secondary">
                    <i class="fas fa-arrow-left"></i> <?php echo isset($lang['back']) ? $lang['back'] : 'Volver'; ?>
                </a>
                
                <button type="button" class="btn btn-info" onclick="downloadPDF()">
                    <i class="fas fa-file-pdf"></i> <?php echo isset($lang['download_pdf']) ? $lang['download_pdf'] : 'Descargar PDF'; ?>
                </button>
                
                <button type="button" class="btn btn-outline" onclick="sendByEmail()">
                    <i class="fas fa-envelope"></i> <?php echo isset($lang['send_by_email']) ? $lang['send_by_email'] : 'Enviar por Email'; ?>
                </button>
                
                <button type="button" class="btn btn-warning" onclick="reportIssue()">
                    <i class="fas fa-exclamation-triangle"></i> <?php echo isset($lang['report_issue']) ? $lang['report_issue'] : 'Reportar'; ?>
                </button>
            </div>
            
            <!-- Sello de verificación -->
            <img class="verification-seal" src="/api/placeholder/100/100" alt="<?php echo isset($lang['verification_seal']) ? $lang['verification_seal'] : 'Sello de verificación'; ?>">
        </div>
    </div>
    
    <!-- Notificación de éxito (oculta por defecto) -->
    <div id="successNotification" class="success-notification">
        <i class="fas fa-check-circle"></i>
        <span id="notificationMessage"></span>
    </div>

    <script>
        // Función para descargar PDF (simulada)
        function downloadPDF() {
            const notificationMessage = document.getElementById('notificationMessage');
            notificationMessage.textContent = '<?php echo isset($lang['pdf_download_started']) ? $lang['pdf_download_started'] : 'Descarga de PDF iniciada...'; ?>';
            showNotification();
        }

        // Función para enviar por email (simulada)
        function sendByEmail() {
            const emailPrompt = '<?php echo isset($lang['enter_email']) ? $lang['enter_email'] : 'Por favor, ingrese su dirección de correo electrónico:'; ?>';
            const email = prompt(emailPrompt);
            if (email) {
                const notificationMessage = document.getElementById('notificationMessage');
                notificationMessage.textContent = '<?php echo isset($lang['email_sent']) ? $lang['email_sent'] : 'Comprobante enviado al correo electrónico.'; ?>';
                showNotification();
            }
        }

        // Función para reportar un problema (simulada)
        function reportIssue() {
            const issuePrompt = '<?php echo isset($lang['describe_issue']) ? $lang['describe_issue'] : 'Por favor, describa el problema:'; ?>';
            const issue = prompt(issuePrompt);
            if (issue) {
                const notificationMessage = document.getElementById('notificationMessage');
                notificationMessage.textContent = '<?php echo isset($lang['issue_reported']) ? $lang['issue_reported'] : 'Problema reportado. Gracias por su retroalimentación.'; ?>';
                showNotification();
            }
        }

        // Mostrar notificación
        function showNotification() {
            const notification = document.getElementById('successNotification');
            notification.classList.add('show');
            setTimeout(() => {
                notification.classList.remove('show');
            }, 3000);
        }
    </script>
</body>
</html>