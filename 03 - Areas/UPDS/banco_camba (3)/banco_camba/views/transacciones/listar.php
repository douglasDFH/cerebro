<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">

<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['transaction_list']) ? $lang['transaction_list'] : 'Lista de Transacciones'; ?></h2>
        <div class="header-buttons">
            <a href="index.php?controller=transaccion&action=buscar" class="btn-action-header btn-search">
                <i class="fa fa-search"></i> <?php echo isset($lang['search_transactions']) ? $lang['search_transactions'] : 'Buscar Transacciones'; ?>
            </a>
            <a href="index.php?controller=transaccion&action=retirar" class="btn-action-header btn-withdraw">
                <i class="fa fa-minus-circle"></i> <?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro'; ?>
            </a>
            <a href="index.php?controller=transaccion&action=depositar" class="btn-action-header btn-deposit">
                <i class="fa fa-plus-circle"></i> <?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depósito'; ?>
            </a>
            <a href="javascript:window.print();" class="btn-action-header btn-print">
                <i class="fa fa-print"></i> <?php echo isset($lang['print']) ? $lang['print'] : 'Imprimir'; ?>
            </a>
        </div>
    </div>
    <div class="card-body">
        <?php if (empty($transacciones)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_transactions']) ? $lang['no_transactions'] : 'No hay transacciones registradas en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></th>
                            <th><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></th>
                            <th><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?></th>
                            <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></th>
                            <th><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></th>
                            <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                            <th><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></th>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($transacciones as $transaccion): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                                <td class="text-center"><?php echo date('H:i:s', strtotime($transaccion['hora'])); ?></td>
                                <td class="text-center">
                                    <?php 
                                    switch($transaccion['tipoTransaccion']) {
                                        case 1:
                                            echo '<span class="badge badge-withdraw">' . (isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro') . '</span>';
                                            break;
                                        case 2:
                                            echo '<span class="badge badge-deposit">' . (isset($lang['deposit']) ? $lang['deposit'] : 'Depósito') . '</span>';
                                            break;
                                        case 3:
                                            echo '<span class="badge badge-transfer-in">' . (isset($lang['transfer_received']) ? $lang['transfer_received'] : 'Transferencia Recibida') . '</span>';
                                            break;
                                        case 4:
                                            echo '<span class="badge badge-transfer-out">' . (isset($lang['transfer_sent']) ? $lang['transfer_sent'] : 'Transferencia Enviada') . '</span>';
                                            break;
                                        default:
                                            echo '<span class="badge badge-other">' . (isset($lang['other']) ? $lang['other'] : 'Otro') . '</span>';
                                    }
                                    ?>
                                </td>
                                <td class="text-center"><?php echo htmlspecialchars($transaccion['nroCuenta']); ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($transaccion['cliente_nombre']); ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
                                <td class="text-center amount-cell">
                                    <?php 
                                    // Determinar moneda según la cuenta
                                    $cuenta = new Cuenta($this->db);
                                    $cuenta->idCuenta = $transaccion['idCuenta'];
                                    $cuenta->obtenerUna();
                                    
                                    if ($cuenta->tipoMoneda == 1) {
                                        echo '<span class="bs-currency">Bs.</span> ' . number_format($transaccion['monto'], 2);
                                    } else {
                                        echo '<span class="usd-currency">$</span> ' . number_format($transaccion['monto'], 2);
                                    }
                                    ?>
                                </td>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <a href="index.php?controller=transaccion&action=ver&id=<?php echo $transaccion['idTransaccion']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <a href="index.php?controller=transaccion&action=comprobante&id=<?php echo $transaccion['idTransaccion']; ?>" class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fa fa-file-text"></i>
                                        </a>
                                    </div>
                                </td>
                            </tr>
                        <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        <?php endif; ?>
    </div>
</div>

<style>
/* Estilos específicos para la vista de transacciones */

/* Estilos para los botones de la cabecera */
.header-buttons {
    display: flex;
    gap: 10px;
}

.btn-action-header {
    padding: 8px 16px;
    border-radius: 4px;
    font-weight: 600;
    text-decoration: none;
    transition: all 0.3s ease;
    display: inline-flex;
    align-items: center;
    gap: 8px;
}

.btn-search {
    background-color: #17a2b8;
    color: white;
}

.btn-search:hover {
    background-color: #138496;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.btn-withdraw {
    background-color: #ffc107;
    color: #212529;
}

.btn-withdraw:hover {
    background-color: #e0a800;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.btn-deposit {
    background-color: #28a745;
    color: white;
}

.btn-deposit:hover {
    background-color: #218838;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.btn-print {
    background-color: #6c757d;
    color: white;
}

.btn-print:hover {
    background-color: #5a6268;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

/* Estilos para las badges de tipo de transacción */
.badge {
    padding: 6px 10px;
    border-radius: 4px;
    font-weight: 600;
    font-size: 0.8rem;
    text-transform: uppercase;
    letter-spacing: 0.5px;
    display: inline-block;
}

.badge-withdraw {
    background-color: #ffc107;
    color: #212529;
}

.badge-deposit {
    background-color: #28a745;
    color: white;
}

.badge-transfer-in {
    background-color: #17a2b8;
    color: white;
}

.badge-transfer-out {
    background-color: #007bff;
    color: white;
}

.badge-other {
    background-color: #6c757d;
    color: white;
}

/* Estilos para la celda de monto */
.amount-cell {
    font-weight: 600;
    font-family: 'Courier New', monospace;
}

.bs-currency {
    color: #0a6c0a;
    font-weight: bold;
}

.usd-currency {
    color: #007bff;
    font-weight: bold;
}

/* Botón de recibo */
.btn-receipt {
    background-color: #007bff;
    color: white;
}

.btn-receipt:hover {
    background-color: #0069d9;
    transform: translateY(-2px);
}

/* Ajuste para la tabla de transacciones */
.table-mercantil td {
    max-width: 200px;
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
}

/* Media queries para responsividad */
@media (max-width: 992px) {
    .header-buttons {
        flex-wrap: wrap;
    }
    
    .btn-action-header {
        flex: 1 0 calc(50% - 5px);
    }
}

@media (max-width: 768px) {
    .btn-action-header {
        flex: 1 0 100%;
        margin-bottom: 5px;
    }
}

/* Animación mejorada para aparecer filas */
@keyframes fadeInUp {
    from {
        opacity: 0;
        transform: translateY(10px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}

.animate-row {
    animation: fadeInUp 0.4s ease forwards;
}
</style>

<script>
// Animación para las filas de la tabla
document.addEventListener('DOMContentLoaded', function() {
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        row.style.animationDelay = `${index * 0.05}s`;
    });
    
    // Destacar fila al hacer hover
    rows.forEach(row => {
        row.addEventListener('mouseenter', function() {
            this.style.transition = 'transform 0.2s ease';
            this.style.transform = 'scale(1.01)';
            this.style.boxShadow = '0 0 15px rgba(10, 108, 10, 0.1)';
            this.style.zIndex = '1';
        });
        
        row.addEventListener('mouseleave', function() {
            this.style.transform = 'scale(1)';
            this.style.boxShadow = 'none';
            this.style.zIndex = 'auto';
        });
    });
});
</script>