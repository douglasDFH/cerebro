<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleVerCuenta.css">
<!-- Contenedor principal sin márgenes laterales -->
<div class="content-wrapper">
    <!-- Encabezado con título y badge de modo visualización -->
    <div class="header-container">
        <h2 class="page-title"><?php echo isset($lang['account_details']) ? $lang['account_details'] : 'Detalles de Cuenta'; ?></h2>
        <div class="mode-visualizacion">
            <span class="mode-icon">👁️</span> <?php echo isset($lang['view_mode']) ? $lang['view_mode'] : 'Modo Visualización'; ?>
        </div>
    </div>

    <!-- Resumen de cuenta -->
    <div class="account-summary">
        <div class="avatar-box">
            <div class="account-avatar">
                <i class="fas fa-credit-card"></i>
            </div>
        </div>
        <div class="account-info">
            <h2 class="account-number"><?php echo htmlspecialchars($this->model->nroCuenta); ?></h2>
            <div class="account-balance">
                <?php 
                if ($this->model->tipoMoneda == 1) {
                    echo '<span class="currency-symbol">Bs.</span> ';
                    echo '<span class="balance-amount">' . number_format($this->model->saldo, 2) . '</span>';
                } else {
                    echo '<span class="currency-symbol">$</span> ';
                    echo '<span class="balance-amount">' . number_format($this->model->saldo, 2) . '</span>';
                }
                ?>
            </div>
        </div>
    </div>

    <!-- Acciones rápidas para cuentas activas -->
    <?php if ($this->model->estado == 1): ?>
    <div class="quick-actions">
        <a href="index.php?controller=transaccion&action=depositar&idCuenta=<?php echo $this->model->idCuenta; ?>" class="quick-action-btn deposit">
            <i class="fas fa-arrow-down"></i> <?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depositar'; ?>
        </a>
        <a href="index.php?controller=transaccion&action=retirar&idCuenta=<?php echo $this->model->idCuenta; ?>" class="quick-action-btn withdraw">
            <i class="fas fa-arrow-up"></i> <?php echo isset($lang['withdraw']) ? $lang['withdraw'] : 'Retirar'; ?>
        </a>
        <a href="index.php?controller=transaccion&action=transferir&idCuentaOrigen=<?php echo $this->model->idCuenta; ?>" class="quick-action-btn transfer">
            <i class="fas fa-exchange-alt"></i> <?php echo isset($lang['transfer']) ? $lang['transfer'] : 'Transferir'; ?>
        </a>
        <a href="index.php?controller=cuenta&action=extracto&id=<?php echo $this->model->idCuenta; ?>" class="quick-action-btn statement">
            <i class="fas fa-file-alt"></i> <?php echo isset($lang['statement']) ? $lang['statement'] : 'Extracto'; ?>
        </a>
    </div>
    <?php endif; ?>

    <!-- Información de la cuenta en dos columnas -->
    <div class="details-container">
        <div class="details-column">
            <div class="details-section">
                <h3 class="section-title">
                    <i class="fas fa-info-circle"></i> <?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de la Cuenta'; ?>
                </h3>
                <div class="details-grid">
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></div>
                        <div class="detail-value"><?php echo htmlspecialchars($this->model->nroCuenta); ?></div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?></div>
                        <div class="detail-value">
                            <?php echo $this->model->tipoCuenta == 1 ? (isset($lang['savings_account']) ? $lang['savings_account'] : 'Cuenta de Ahorro') : (isset($lang['checking_account']) ? $lang['checking_account'] : 'Cuenta Corriente'); ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?></div>
                        <div class="detail-value">
                            <?php echo $this->model->tipoMoneda == 1 ? (isset($lang['bolivianos']) ? $lang['bolivianos'] : 'Bolivianos (Bs)') : (isset($lang['dollars']) ? $lang['dollars'] : 'Dólares ($)'); ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['current_balance']) ? $lang['current_balance'] : 'Saldo Actual'; ?></div>
                        <div class="detail-value highlight-value">
                            <?php 
                            if ($this->model->tipoMoneda == 1) {
                                echo 'Bs. ' . number_format($this->model->saldo, 2);
                            } else {
                                echo '$ ' . number_format($this->model->saldo, 2);
                            }
                            ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Apertura'; ?></div>
                        <div class="detail-value">
                            <?php echo date('d/m/Y', strtotime($this->model->fechaApertura)); ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></div>
                        <div class="detail-value">
                            <?php if ($this->model->estado == 1): ?>
                            <span class="status-badge active"><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></span>
                            <?php else: ?>
                            <span class="status-badge inactive"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></span>
                            <?php endif; ?>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="details-column">
            <div class="details-section">
                <h3 class="section-title">
                    <i class="fas fa-user"></i> <?php echo isset($lang['client_information']) ? $lang['client_information'] : 'Información del Cliente'; ?>
                </h3>
                <div class="details-grid">
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?></div>
                        <div class="detail-value"><?php echo htmlspecialchars($clienteModel->nombre); ?></div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['surnames']) ? $lang['surnames'] : 'Apellidos'; ?></div>
                        <div class="detail-value">
                            <?php echo htmlspecialchars($clienteModel->apellidoPaterno . ' ' . $clienteModel->apellidoMaterno); ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['identity_document']) ? $lang['identity_document'] : 'CI / Documento'; ?></div>
                        <div class="detail-value"><?php echo htmlspecialchars($clienteModel->ci); ?></div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></div>
                        <div class="detail-value">
                            <?php echo !empty($clienteModel->telefono) ? htmlspecialchars($clienteModel->telefono) : '—'; ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['email']) ? $lang['email'] : 'Email'; ?></div>
                        <div class="detail-value">
                            <?php echo !empty($clienteModel->email) ? htmlspecialchars($clienteModel->email) : '—'; ?>
                        </div>
                    </div>
                    
                    <div class="detail-item">
                        <div class="detail-label"><?php echo isset($lang['view_client']) ? $lang['view_client'] : 'Ver Cliente'; ?></div>
                        <div class="detail-value">
                            <a href="index.php?controller=cliente&action=ver&id=<?php echo $clienteModel->idPersona; ?>" class="view-client-btn">
                                <i class="fas fa-external-link-alt"></i> <?php echo isset($lang['view_client_details']) ? $lang['view_client_details'] : 'Ver detalles del cliente'; ?>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <!-- Sección de transacciones recientes -->
    <div class="transactions-section">
        <div class="section-header">
            <h3 class="section-title">
                <i class="fas fa-exchange-alt"></i> <?php echo isset($lang['recent_transactions']) ? $lang['recent_transactions'] : 'Transacciones Recientes'; ?>
            </h3>
            <a href="index.php?controller=cuenta&action=extracto&id=<?php echo $this->model->idCuenta; ?>" class="view-all-btn">
                <i class="fas fa-file-alt"></i> <?php echo isset($lang['view_full_statement']) ? $lang['view_full_statement'] : 'Ver Extracto Completo'; ?>
            </a>
        </div>
        
        <?php if (empty($transacciones)): ?>
        <div class="no-transactions">
            <div class="empty-state">
                <i class="fas fa-receipt empty-icon"></i>
                <p><?php echo isset($lang['no_transactions_account']) ? $lang['no_transactions_account'] : 'No hay transacciones registradas para esta cuenta.'; ?></p>
            </div>
        </div>
        <?php else: ?>
        <div class="table-container">
            <table class="data-table">
                <thead>
                    <tr>
                        <th><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></th>
                        <th><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></th>
                        <th><?php echo isset($lang['type']) ? $lang['type'] : 'Tipo'; ?></th>
                        <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                        <th><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></th>
                        <th><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?></th>
                    </tr>
                </thead>
                <tbody>
                    <?php 
                    $recentTransactions = array_slice($transacciones, 0, 10);
                    foreach ($recentTransactions as $transaccion): 
                    ?>
                    <tr>
                        <td><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                        <td><?php echo date('H:i:s', strtotime($transaccion['hora'])); ?></td>
                        <td>
                            <?php 
                            switch($transaccion['tipoTransaccion']) {
                                case 1:
                                    echo '<span class="transaction-type withdrawal">' . (isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro') . '</span>';
                                    break;
                                case 2:
                                    echo '<span class="transaction-type deposit">' . (isset($lang['deposit']) ? $lang['deposit'] : 'Depósito') . '</span>';
                                    break;
                                case 3:
                                    echo '<span class="transaction-type transfer-in">' . (isset($lang['received_transfer']) ? $lang['received_transfer'] : 'Transferencia Recibida') . '</span>';
                                    break;
                                case 4:
                                    echo '<span class="transaction-type transfer-out">' . (isset($lang['sent_transfer']) ? $lang['sent_transfer'] : 'Transferencia Enviada') . '</span>';
                                    break;
                                default:
                                    echo '<span class="transaction-type other">' . (isset($lang['other']) ? $lang['other'] : 'Otro') . '</span>';
                            }
                            ?>
                        </td>
                        <td><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
                        <td class="amount-cell <?php echo in_array($transaccion['tipoTransaccion'], [1, 4]) ? 'negative' : 'positive'; ?>">
                            <?php 
                            $prefix = in_array($transaccion['tipoTransaccion'], [1, 4]) ? '-' : '+';
                            if ($this->model->tipoMoneda == 1) {
                                echo $prefix . ' Bs. ' . number_format($transaccion['monto'], 2);
                            } else {
                                echo $prefix . ' $ ' . number_format($transaccion['monto'], 2);
                            }
                            ?>
                        </td>
                        <td class="balance-cell">
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
        
        <?php if (count($transacciones) > 10): ?>
        <div class="view-more-container">
            <a href="index.php?controller=cuenta&action=extracto&id=<?php echo $this->model->idCuenta; ?>" class="view-more-btn">
                <?php echo isset($lang['view_all_transactions']) ? $lang['view_all_transactions'] : 'Ver todas las transacciones'; ?>
            </a>
        </div>
        <?php endif; ?>
        <?php endif; ?>
    </div>

    <!-- Botones de acción -->
    <div class="actions-container">
        <a href="index.php?controller=cuenta&action=editar&id=<?php echo $this->model->idCuenta; ?>" class="action-main edit-btn">
            <i class="fas fa-edit"></i> <?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>
        </a>
        
        <?php if ($this->model->estado == 1): ?>
        <a href="index.php?controller=cuenta&action=cerrar&id=<?php echo $this->model->idCuenta; ?>" class="action-main close-btn" onclick="return confirm('<?php echo isset($lang['confirm_close_account']) ? $lang['confirm_close_account'] : '¿Está seguro que desea cerrar esta cuenta?'; ?>');">
            <i class="fas fa-times-circle"></i> <?php echo isset($lang['close_account']) ? $lang['close_account'] : 'Cerrar Cuenta'; ?>
        </a>
        <?php else: ?>
        <a href="index.php?controller=cuenta&action=reactivar&id=<?php echo $this->model->idCuenta; ?>" class="action-main activate-btn" onclick="return confirm('<?php echo isset($lang['confirm_reactivate_account']) ? $lang['confirm_reactivate_account'] : '¿Está seguro que desea reactivar esta cuenta?'; ?>');">
            <i class="fas fa-check-circle"></i> <?php echo isset($lang['reactivate_account']) ? $lang['reactivate_account'] : 'Reactivar Cuenta'; ?>
        </a>
        <?php endif; ?>
        
        <a href="index.php?controller=cuenta&action=listar" class="action-main back-btn">
            <i class="fas fa-arrow-left"></i> <?php echo isset($lang['back_to_list']) ? $lang['back_to_list'] : 'Volver a la Lista'; ?>
        </a>
    </div>
</div>