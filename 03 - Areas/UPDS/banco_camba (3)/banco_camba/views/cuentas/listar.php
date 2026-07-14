<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">

<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['account_list']) ? $lang['account_list'] : 'Lista de Cuentas'; ?></h2>
        <a href="index.php?controller=cuenta&action=crear" class="btn-new-client">
            <i class="fa fa-plus-circle"></i> <?php echo isset($lang['new_account']) ? $lang['new_account'] : 'Nueva Cuenta'; ?>
        </a>
    </div>
    <div class="card-body">
        <?php if (empty($cuentas)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_accounts_registered']) ? $lang['no_accounts_registered'] : 'No hay cuentas registradas en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></th>
                            <th><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></th>
                            <th><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?></th>
                            <th><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?></th>
                            <th><?php echo isset($lang['balance']) ? $lang['balance'] : 'Saldo'; ?></th>
                            <th><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Apertura'; ?></th>
                            <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></th>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($cuentas as $cuenta): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo $cuenta['nroCuenta']; ?></td>
                                <td class="text-center"><?php echo $cuenta['cliente_nombre']; ?></td>
                                <td class="text-center">
                                    <?php echo $cuenta['tipoCuenta'] == 1 ? (isset($lang['savings']) ? $lang['savings'] : 'Ahorro') : (isset($lang['checking']) ? $lang['checking'] : 'Corriente'); ?>
                                </td>
                                <td class="text-center">
                                    <?php echo $cuenta['tipoMoneda'] == 1 ? (isset($lang['bolivianos_short']) ? $lang['bolivianos_short'] : 'Bolivianos') : (isset($lang['dollars_short']) ? $lang['dollars_short'] : 'Dólares'); ?>
                                </td>
                                <td class="text-center">
                                    <?php 
                                    if ($cuenta['tipoMoneda'] == 1) {
                                        echo 'Bs. ' . number_format($cuenta['saldo'], 2);
                                    } else {
                                        echo '$ ' . number_format($cuenta['saldo'], 2);
                                    }
                                    ?>
                                </td>
                                <td class="text-center"><?php echo date('d/m/Y', strtotime($cuenta['fechaApertura'])); ?></td>
                                <td class="text-center">
                                    <?php if ($cuenta['estado'] == 1): ?>
                                        <span class="badge bg-success"><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></span>
                                    <?php else: ?>
                                        <span class="badge bg-danger"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></span>
                                    <?php endif; ?>
                                </td>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta['idCuenta']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view_details']) ? $lang['view_details'] : 'Ver detalles'; ?>">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <a href="index.php?controller=cuenta&action=editar&id=<?php echo $cuenta['idCuenta']; ?>" class="btn-action btn-edit" title="<?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                        <?php if ($cuenta['estado'] == 1): ?>
                                            <a href="index.php?controller=transaccion&action=depositar&idCuenta=<?php echo $cuenta['idCuenta']; ?>" class="btn-action btn-info" title="<?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depositar'; ?>">
                                                <i class="fa fa-plus"></i>
                                            </a>
                                            <a href="index.php?controller=transaccion&action=retirar&idCuenta=<?php echo $cuenta['idCuenta']; ?>" class="btn-action btn-warning" title="<?php echo isset($lang['withdraw']) ? $lang['withdraw'] : 'Retirar'; ?>">
                                                <i class="fa fa-minus"></i>
                                            </a>
                                        <?php endif; ?>
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