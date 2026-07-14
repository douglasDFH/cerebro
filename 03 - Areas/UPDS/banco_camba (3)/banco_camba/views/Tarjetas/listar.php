<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">
<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['card_list']) ? $lang['card_list'] : 'Lista de Tarjetas'; ?></h2>
        <a href="index.php?controller=tarjeta&action=crear" class="btn-new-client">
            <i class="fa fa-plus-circle"></i> <?php echo isset($lang['new_card']) ? $lang['new_card'] : 'Nueva Tarjeta'; ?>
        </a>
    </div>
    <div class="card-body">
        <?php if (empty($tarjetas)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_cards']) ? $lang['no_cards'] : 'No hay tarjetas registradas en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo isset($lang['card_number']) ? $lang['card_number'] : 'Número de Tarjeta'; ?></th>
                            <th><?php echo isset($lang['card_type']) ? $lang['card_type'] : 'Tipo de Tarjeta'; ?></th>
                            <th><?php echo isset($lang['expiration_date']) ? $lang['expiration_date'] : 'Fecha de Expiración'; ?></th>
                            <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></th>
                            <th><?php echo isset($lang['client_name']) ? $lang['client_name'] : 'Nombre del Cliente'; ?></th>
                            <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></th>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($tarjetas as $tarjeta): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo $tarjeta['idTarjeta']; ?></td>
                                <td class="text-center">
                                    <?php 
                                    // Mostrar solo los últimos 4 dígitos de la tarjeta por seguridad
                                    $maskedNumber = preg_replace('/\d{4} \d{4} \d{4} (\d{4})/', '**** **** **** $1', $tarjeta['nroTarjeta']); 
                                    echo $maskedNumber;
                                    ?>
                                </td>
                                <td class="text-center">
                                    <?php 
                                    // Mostrar el tipo de tarjeta (débito o crédito)
                                    if ($tarjeta['tipoTarjeta'] == 'debito') {
                                        echo isset($lang['debit']) ? $lang['debit'] : 'Débito';
                                    } else {
                                        echo isset($lang['credit']) ? $lang['credit'] : 'Crédito';
                                    }
                                    ?>
                                </td>
                                <td class="text-center"><?php echo $tarjeta['fechaExpiracion']; ?></td>
                                <td class="text-center"><?php echo $tarjeta['nroCuenta']; ?></td>
                                <td class="text-center">
                                    <?php 
                                    echo $tarjeta['nombre'] . ' ' . $tarjeta['apellidoPaterno'] . ' ' . $tarjeta['apellidoMaterno']; 
                                    ?>
                                </td>
                                <td class="text-center">
                                    <?php 
                                    if ($tarjeta['estado'] == 'activa') {
                                        echo '<span class="badge bg-success">' . (isset($lang['active']) ? $lang['active'] : 'Activa') . '</span>';
                                    } else {
                                        echo '<span class="badge bg-danger">' . (isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva') . '</span>';
                                    }
                                    ?>
                                </td>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <a href="index.php?controller=tarjeta&action=ver&id=<?php echo $tarjeta['idTarjeta']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <a href="index.php?controller=tarjeta&action=editar&id=<?php echo $tarjeta['idTarjeta']; ?>" class="btn-action btn-edit" title="<?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                        <?php if ($tarjeta['estado'] == 'activa'): ?>
                                            <a href="index.php?controller=tarjeta&action=editar&id=<?php echo $tarjeta['idTarjeta']; ?>&cambiarEstado=inactiva&return=listar" class="btn-action btn-info" title="<?php echo isset($lang['deactivate']) ? $lang['deactivate'] : 'Desactivar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_deactivate_card']) ? $lang['confirm_deactivate_card'] : '¿Está seguro que desea desactivar esta tarjeta?'; ?>')">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        <?php else: ?>
                                            <a href="index.php?controller=tarjeta&action=editar&id=<?php echo $tarjeta['idTarjeta']; ?>&cambiarEstado=activa&return=listar" class="btn-action btn-info" title="<?php echo isset($lang['activate']) ? $lang['activate'] : 'Activar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_activate_card']) ? $lang['confirm_activate_card'] : '¿Está seguro que desea activar esta tarjeta?'; ?>')">
                                                <i class="fa fa-check"></i>
                                            </a>
                                        <?php endif; ?>
                                        <a href="index.php?controller=tarjeta&action=eliminar&id=<?php echo $tarjeta['idTarjeta']; ?>" class="btn-action btn-delete delete-button" title="<?php echo isset($lang['delete']) ? $lang['delete'] : 'Eliminar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_delete_card']) ? $lang['confirm_delete_card'] : '¿Está seguro que desea eliminar esta tarjeta? Esta acción no se puede deshacer.'; ?>')">
                                            <i class="fa fa-trash"></i>
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
    background-color:rgb(0, 106, 255);
}

/* Estilo para el botón de imprimir en acciones */
.btn-info {
    background-color:rgb(0, 106, 255);
    color: white;
}
</style>

<script>
// Animación para las filas de la tabla
document.addEventListener('DOMContentLoaded', function() {
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        row.style.animationDelay = `${index * 0.05}s`;
    });
});
</script>