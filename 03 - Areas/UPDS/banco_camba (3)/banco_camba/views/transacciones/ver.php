<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleVerTransaccion.css">
<!-- Contenedor principal sin márgenes laterales -->
<div class="content-wrapper">
    <!-- Encabezado con título y badge de modo visualización -->
    <div class="header-container">
        <h2 class="page-title"><?php echo isset($lang['transaction_details']) ? $lang['transaction_details'] : 'Detalles de Transacción'; ?></h2>
        <div class="mode-visualizacion">
            <span class="mode-icon">👁️</span> <?php echo isset($lang['view_mode']) ? $lang['view_mode'] : 'Modo Visualización'; ?>
        </div>
    </div>

    <!-- Resumen de transacción -->
    <div class="transaction-summary">
        <div class="transaction-icon-box">
            <?php 
            $iconClass = '';
            $transactionLabel = '';
            switch($this->model->tipoTransaccion) {
                case 1:
                    $iconClass = 'withdraw-icon';
                    $transactionLabel = isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro';
                    break;
                case 2:
                    $iconClass = 'deposit-icon';
                    $transactionLabel = isset($lang['deposit']) ? $lang['deposit'] : 'Depósito';
                    break;
                case 3:
                    $iconClass = 'transfer-in-icon';
                    $transactionLabel = isset($lang['transfer_received']) ? $lang['transfer_received'] : 'Transferencia Recibida';
                    break;
                case 4:
                    $iconClass = 'transfer-out-icon';
                    $transactionLabel = isset($lang['transfer_sent']) ? $lang['transfer_sent'] : 'Transferencia Enviada';
                    break;
                default:
                    $iconClass = 'other-icon';
                    $transactionLabel = isset($lang['other_operation']) ? $lang['other_operation'] : 'Otra Operación';
            }
            ?>
            <div class="transaction-icon <?php echo $iconClass; ?>">
                <?php 
                switch($this->model->tipoTransaccion) {
                    case 1:
                        echo '<i class="fas fa-arrow-up"></i>';
                        break;
                    case 2:
                        echo '<i class="fas fa-arrow-down"></i>';
                        break;
                    case 3:
                        echo '<i class="fas fa-arrow-circle-down"></i>';
                        break;
                    case 4:
                        echo '<i class="fas fa-arrow-circle-up"></i>';
                        break;
                    default:
                        echo '<i class="fas fa-exchange-alt"></i>';
                }
                ?>
            </div>
        </div>
        <div class="transaction-info">
            <h2 class="transaction-id"><?php echo isset($lang['transaction_id_with_number']) ? sprintf($lang['transaction_id_with_number'], $this->model->idTransaccion) : 'Transacción #'.$this->model->idTransaccion; ?></h2>
            <p class="transaction-type"><?php echo $transactionLabel; ?></p>
        </div>
        <div class="transaction-amount-box">
            <div class="transaction-amount <?php echo in_array($this->model->tipoTransaccion, [1, 4]) ? 'negative' : 'positive'; ?>">
                <?php 
                $prefix = in_array($this->model->tipoTransaccion, [1, 4]) ? '-' : '+';
                if ($cuenta->tipoMoneda == 1) {
                    echo $prefix . ' Bs. ' . number_format($this->model->monto, 2);
                } else {
                    echo $prefix . ' $ ' . number_format($this->model->monto, 2);
                }
                ?>
            </div>
            <div class="transaction-date-time">
                <?php 
                $dateFormatted = date('d/m/Y', strtotime($this->model->fecha));
                $timeFormatted = date('H:i:s', strtotime($this->model->hora));
                echo isset($lang['date_time_format']) ? sprintf($lang['date_time_format'], $dateFormatted, $timeFormatted) : $dateFormatted . ' a las ' . $timeFormatted; 
                ?>
            </div>
        </div>
    </div>

    <!-- Detalles de la transacción -->
    <div class="details-container">
        <div class="details-column">
            <h3 class="details-section-title"><?php echo isset($lang['transaction_information']) ? $lang['transaction_information'] : 'Información de la Transacción'; ?></h3>
            
            <div class="details-box">
                <div class="details-row">
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['transaction_id']) ? $lang['transaction_id'] : 'ID Transacción'; ?></label>
                        <div class="detail-value"><?php echo $this->model->idTransaccion; ?></div>
                    </div>
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?></label>
                        <div class="detail-value">
                            <?php 
                            switch($this->model->tipoTransaccion) {
                                case 1:
                                    echo '<span class="transaction-badge withdrawal">' . (isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro') . '</span>';
                                    break;
                                case 2:
                                    echo '<span class="transaction-badge deposit">' . (isset($lang['deposit']) ? $lang['deposit'] : 'Depósito') . '</span>';
                                    break;
                                case 3:
                                    echo '<span class="transaction-badge transfer-in">' . (isset($lang['transfer_received']) ? $lang['transfer_received'] : 'Transferencia Recibida') . '</span>';
                                    break;
                                case 4:
                                    echo '<span class="transaction-badge transfer-out">' . (isset($lang['transfer_sent']) ? $lang['transfer_sent'] : 'Transferencia Enviada') . '</span>';
                                    break;
                                default:
                                    echo '<span class="transaction-badge other">' . (isset($lang['other']) ? $lang['other'] : 'Otro') . '</span>';
                            }
                            ?>
                        </div>
                    </div>
                </div>

                <div class="details-row">
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></label>
                        <div class="detail-value"><?php echo date('d/m/Y', strtotime($this->model->fecha)); ?></div>
                    </div>
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></label>
                        <div class="detail-value"><?php echo date('H:i:s', strtotime($this->model->hora)); ?></div>
                    </div>
                </div>

                <div class="details-row">
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></label>
                        <div class="detail-value highlight-value">
                            <?php 
                            if ($cuenta->tipoMoneda == 1) {
                                echo 'Bs. ' . number_format($this->model->monto, 2);
                            } else {
                                echo '$ ' . number_format($this->model->monto, 2);
                            }
                            ?>
                        </div>
                    </div>
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?></label>
                        <div class="detail-value">
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

                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></label>
                        <div class="detail-value"><?php echo htmlspecialchars($this->model->descripcion); ?></div>
                    </div>
                </div>

                <?php if (!empty($this->model->cuentaOrigen)): ?>
                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['source_account']) ? $lang['source_account'] : 'Cuenta Origen'; ?></label>
                        <div class="detail-value"><?php echo htmlspecialchars($this->model->cuentaOrigen); ?></div>
                    </div>
                </div>
                <?php endif; ?>

                <?php if (!empty($this->model->cuentaDestino)): ?>
                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['destination_account']) ? $lang['destination_account'] : 'Cuenta Destino'; ?></label>
                        <div class="detail-value"><?php echo htmlspecialchars($this->model->cuentaDestino); ?></div>
                    </div>
                </div>
                <?php endif; ?>
            </div>
        </div>

        <div class="details-column">
            <h3 class="details-section-title"><?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de la Cuenta'; ?></h3>
            
            <div class="details-box">
                <div class="details-row">
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></label>
                        <div class="detail-value account-number"><?php echo htmlspecialchars($cuenta->nroCuenta); ?></div>
                    </div>
                    <div class="details-cell">
                        <label class="detail-label"><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?></label>
                        <div class="detail-value">
                            <?php echo $cuenta->tipoCuenta == 1 ? (isset($lang['savings']) ? $lang['savings'] : 'Ahorro') : (isset($lang['checking']) ? $lang['checking'] : 'Corriente'); ?>
                        </div>
                    </div>
                </div>

                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?></label>
                        <div class="detail-value">
                            <?php echo $cuenta->tipoMoneda == 1 ? (isset($lang['bolivianos']) ? $lang['bolivianos'] : 'Bolivianos (Bs)') : (isset($lang['dollars']) ? $lang['dollars'] : 'Dólares ($)'); ?>
                        </div>
                    </div>
                </div>
            </div>

            <h3 class="details-section-title"><?php echo isset($lang['client_information']) ? $lang['client_information'] : 'Información del Cliente'; ?></h3>
            
            <div class="details-box">
                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['full_name']) ? $lang['full_name'] : 'Nombre Completo'; ?></label>
                        <div class="detail-value">
                            <?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?>
                        </div>
                    </div>
                </div>

                <div class="details-row">
                    <div class="details-cell full-width">
                        <label class="detail-label"><?php echo isset($lang['identity_document']) ? $lang['identity_document'] : 'CI / Documento'; ?></label>
                        <div class="detail-value"><?php echo htmlspecialchars($cliente->ci); ?></div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Botones de acción -->
    <div class="actions-container">
        <a href="index.php?controller=transaccion&action=comprobante&id=<?php echo $this->model->idTransaccion; ?>" class="action-main print-btn">
            <i class="fas fa-print"></i> <?php echo isset($lang['print_receipt']) ? $lang['print_receipt'] : 'Imprimir Comprobante'; ?>
        </a>
        
        <a href="index.php?controller=transaccion&action=listar" class="action-main back-btn">
            <i class="fas fa-arrow-left"></i> <?php echo isset($lang['back_to_list']) ? $lang['back_to_list'] : 'Volver a la Lista'; ?>
        </a>
    </div>
</div>