<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">
<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['search_atm_transactions']) ? $lang['search_atm_transactions'] : 'Buscar Transacciones ATM'; ?></h2>
        <a href="index.php?controller=transaccionatm&action=listar" class="btn-secondary">
            <i class="fa fa-arrow-left"></i> <?php echo isset($lang['back']) ? $lang['back'] : 'Volver'; ?>
        </a>
    </div>
    <div class="card-body">
        <!-- Formulario de búsqueda -->
        <div class="search-form">
            <form action="index.php" method="GET" class="row">
                <input type="hidden" name="controller" value="transaccionatm">
                <input type="hidden" name="action" value="buscar">
                
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fechaInicio"><?php echo isset($lang['start_date']) ? $lang['start_date'] : 'Fecha Inicio'; ?></label>
                        <input type="date" id="fechaInicio" name="fechaInicio" class="form-control" value="<?php echo $fechaInicio; ?>">
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fechaFin"><?php echo isset($lang['end_date']) ? $lang['end_date'] : 'Fecha Fin'; ?></label>
                        <input type="date" id="fechaFin" name="fechaFin" class="form-control" value="<?php echo $fechaFin; ?>">
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="tipoTransaccion"><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?></label>
                        <select id="tipoTransaccion" name="tipoTransaccion" class="form-control">
                            <option value=""><?php echo isset($lang['all']) ? $lang['all'] : 'Todos'; ?></option>
                            <option value="deposito" <?php echo $tipoTransaccion == 'deposito' ? 'selected' : ''; ?>><?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depósito'; ?></option>
                            <option value="retiro" <?php echo $tipoTransaccion == 'retiro' ? 'selected' : ''; ?>><?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro'; ?></option>
                            <option value="consulta" <?php echo $tipoTransaccion == 'consulta' ? 'selected' : ''; ?>><?php echo isset($lang['inquiry']) ? $lang['inquiry'] : 'Consulta'; ?></option>
                        </select>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="idATM"><?php echo isset($lang['atm']) ? $lang['atm'] : 'Cajero ATM'; ?></label>
                        <select id="idATM" name="idATM" class="form-control">
                            <option value="0"><?php echo isset($lang['all']) ? $lang['all'] : 'Todos'; ?></option>
                            <?php foreach ($atms as $atm): ?>
                                <option value="<?php echo $atm['idATM']; ?>" <?php echo $idATM == $atm['idATM'] ? 'selected' : ''; ?>>
                                    <?php echo htmlspecialchars($atm['nombre'] . ' (' . $atm['ubicacion'] . ')'); ?>
                                </option>
                            <?php endforeach; ?>
                        </select>
                    </div>
                </div>
                
                <div class="col-12 mt-3">
                    <button type="submit" class="btn-primary">
                        <i class="fa fa-search"></i> <?php echo isset($lang['search']) ? $lang['search'] : 'Buscar'; ?>
                    </button>
                    <a href="index.php?controller=transaccionatm&action=buscar" class="btn-secondary">
                        <i class="fa fa-refresh"></i> <?php echo isset($lang['reset']) ? $lang['reset'] : 'Reiniciar'; ?>
                    </a>
                </div>
            </form>
        </div>
        
        <!-- Resultados de la búsqueda -->
        <div class="search-results mt-4">
            <h3><?php echo isset($lang['search_results']) ? $lang['search_results'] : 'Resultados de la Búsqueda'; ?></h3>
            
            <?php if (empty($transaccionesATM)): ?>
                <div class="alert alert-info"><?php echo isset($lang['no_transactions_found']) ? $lang['no_transactions_found'] : 'No se encontraron transacciones con los criterios especificados'; ?></div>
            <?php else: ?>
                <div class="table-container">
                    <table class="table table-mercantil">
                        <thead>
                            <tr>
                                <th><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></th>
                                <th><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></th>
                                <th><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo'; ?></th>
                                <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Nro. Cuenta'; ?></th>
                                <th><?php echo isset($lang['destination_account']) ? $lang['destination_account'] : 'Cuenta Destino'; ?></th>
                                <th><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></th>
                                <?php if (isset($transaccionesATM[0]['nombreATM'])): ?>
                                <th><?php echo isset($lang['atm']) ? $lang['atm'] : 'Cajero'; ?></th>
                                <?php endif; ?>
                                <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                                <th><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></th>
                                <th><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?></th>
                                <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                            </tr>
                        </thead>
                        <tbody>
                            <?php foreach ($transaccionesATM as $transaccion): ?>
                                <tr class="animate-row">
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
                                    <td class="text-center"><?php echo isset($transaccion['nroCuenta']) ? $transaccion['nroCuenta'] : '-'; ?></td>
                                    <td class="text-center"><?php echo isset($transaccion['cuentaDestino']) && !empty($transaccion['cuentaDestino']) ? $transaccion['cuentaDestino'] : '-'; ?></td>
                                    <td class="text-center"><?php echo isset($transaccion['cliente_nombre']) ? $transaccion['cliente_nombre'] : '-'; ?></td>
                                    <?php if (isset($transaccionesATM[0]['nombreATM'])): ?>
                                    <td class="text-center"><?php echo isset($transaccion['nombreATM']) ? ($transaccion['nombreATM'] . ' (' . $transaccion['ubicacion'] . ')') : '-'; ?></td>
                                    <?php endif; ?>
                                    <td class="text-center"><?php echo htmlspecialchars($transaccion['descripcion']); ?></td>
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
                                    <td class="text-center actions-cell">
                                        <div class="action-buttons">
                                            <a href="index.php?controller=transaccionatm&action=ver&id=<?php echo $transaccion['idTransaccionATM']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                                <i class="fa fa-eye"></i>
                                            </a>
                                            <a href="index.php?controller=transaccionatm&action=comprobante&id=<?php echo $transaccion['idTransaccionATM']; ?>" class="btn-action btn-print" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
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
</div>

<script>
// Animación para las filas de la tabla
document.addEventListener('DOMContentLoaded', function() {
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        row.style.animationDelay = `${index * 0.05}s`;
    });
});
</script>