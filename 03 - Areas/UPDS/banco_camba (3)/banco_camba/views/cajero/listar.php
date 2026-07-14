<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">
<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['atm_transaction_list']) ? $lang['atm_transaction_list'] : 'Listado de Transacciones ATM'; ?></h2>
        <div class="header-buttons">
            <a href="index.php?controller=transaccionatm&action=buscar" class="btn-new-client">
                <i class="fa fa-search"></i> <?php echo isset($lang['search_atm_transactions']) ? $lang['search_atm_transactions'] : 'Buscar Transacciones ATM'; ?>
            </a>
            <a href="index.php?controller=transaccion&action=retirar" class="btn-new-client">
                <i class="fa fa-minus-circle"></i> <?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro'; ?>
            </a>
            <a href="index.php?controller=transaccion&action=depositar" class="btn-new-client">
                <i class="fa fa-plus-circle"></i> <?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depósito'; ?>
            </a>
        </div>
    </div>
    <div class="card-body">
        <?php if (empty($transaccionesATM)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_transactions']) ? $lang['no_transactions'] : 'No hay transacciones registradas en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></th>
                            <th><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></th>
                            <th><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo'; ?></th>
                            <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                            <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Nro. Cuenta'; ?></th>
                            <th><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></th>
                            <th><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?></th>
                            <th><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></th>
                            <?php if (isset($transaccionesATM[0]['nombreATM'])): ?>
                            <th><?php echo isset($lang['atm']) ? $lang['atm'] : 'Cajero'; ?></th>
                            <?php endif; ?>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($transaccionesATM as $transaccion): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo $transaccion['idTransaccionATM']; ?></td>
                                <td class="text-center"><?php echo date('d/m/Y', strtotime($transaccion['fecha'])); ?></td>
                                <td class="text-center"><?php echo date('H:i:s', strtotime($transaccion['hora'])); ?></td>
                                <td class="text-center">
                                    <?php 
                                    switch($transaccion['tipoTransaccion']) {
                                        case 'retiro':
                                            echo '<span class="badge bg-warning">' . (isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro') . '</span>';
                                            break;
                                        case 'deposito':
                                            echo '<span class="badge bg-success">' . (isset($lang['deposit']) ? $lang['deposit'] : 'Depósito') . '</span>';
                                            break;
                                        case 'consulta':
                                            echo '<span class="badge bg-info">' . (isset($lang['inquiry']) ? $lang['inquiry'] : 'Consulta') . '</span>';
                                            break;
                                        default:
                                            echo '<span class="badge bg-secondary">' . (isset($lang['other']) ? $lang['other'] : 'Otro') . '</span>';
                                    }
                                    ?>
                                </td>
                                <td class="text-center"><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
                                <td class="text-center"><?php echo isset($transaccion['nroCuenta']) ? $transaccion['nroCuenta'] : '-'; ?></td>
                                <td class="text-center">
                                    <?php 
                                    try {
                                        echo number_format($transaccion['monto'], 2);
                                    } catch (Exception $e) {
                                        echo '-';
                                    }
                                    ?>
                                </td>
                                <td class="text-center">
                                    <?php 
                                    if (isset($transaccion['saldoResultante'])) {
                                        try {
                                            echo number_format($transaccion['saldoResultante'], 2);
                                        } catch (Exception $e) {
                                            echo '-';
                                        }
                                    } else {
                                        echo '-';
                                    }
                                    ?>
                                </td>
                                <td class="text-center"><?php echo isset($transaccion['cliente_nombre']) ? $transaccion['cliente_nombre'] : '-'; ?></td>
                                <?php if (isset($transaccionesATM[0]['nombreATM'])): ?>
                                <td class="text-center"><?php echo isset($transaccion['nombreATM']) ? ($transaccion['nombreATM'] . ' (' . $transaccion['ubicacion'] . ')') : '-'; ?></td>
                                <?php endif; ?>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <!-- Botón para ver detalles de la transacción - Link optimizado -->
                                        <a href="index.php?controller=transaccionatm&action=ver&id=<?php echo $transaccion['idTransaccionATM']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>" onclick="event.preventDefault(); window.location.href='index.php?controller=transaccionatm&action=ver&id=<?php echo $transaccion['idTransaccionATM']; ?>';">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <!-- Botón para ver el comprobante -->
                                        <a href="index.php?controller=transaccionatm&action=comprobante&id=<?php echo $transaccion['idTransaccionATM']; ?>" class="btn-action btn-edit" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fa fa-file-text"></i>
                                        </a>
                                        <!-- Botón para imprimir -->
                                        <a href="javascript:void(0);" onclick="window.print();" class="btn-action btn-info" title="<?php echo isset($lang['print']) ? $lang['print'] : 'Imprimir'; ?>">
                                            <i class="fa fa-print"></i>
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
/* Estilos para los botones de la cabecera */
.card-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
}

.header-buttons {
    display: flex;
    gap: 10px;
}

.btn-new-client:hover {
    background-color: rgb(0, 106, 255);
}

/* Estilo para el botón de imprimir en acciones */
.btn-info {
    background-color: rgb(0, 106, 255);
    color: white;
}

/* Estilos para los botones de acción */
.btn-action {
    margin-right: 5px;
    cursor: pointer;
}
</style>

<script>
// Animación para las filas de la tabla
document.addEventListener('DOMContentLoaded', function() {
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        row.style.animationDelay = `${index * 0.05}s`;
    });
    
    // Asegurar que los enlaces de acción funcionen correctamente
    document.querySelectorAll('.btn-view').forEach(function(button) {
        button.addEventListener('click', function(e) {
            const href = this.getAttribute('href');
            window.location.href = href;
        });
    });
});
</script>