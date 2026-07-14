<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">
<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['branch_list']) ? $lang['branch_list'] : 'Lista de Oficinas'; ?></h2>
        <a href="index.php?controller=oficina&action=crear" class="btn-new-client">
            <i class="fa fa-plus-circle"></i> <?php echo isset($lang['new_branch']) ? $lang['new_branch'] : 'Nueva Oficina'; ?>
        </a>
    </div>
    <div class="card-body">
        <?php if (empty($oficinas)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_branches_found']) ? $lang['no_branches_found'] : 'No hay oficinas registradas en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?></th>
                            <th><?php echo isset($lang['address']) ? $lang['address'] : 'Dirección'; ?></th>
                            <th><?php echo isset($lang['city']) ? $lang['city'] : 'Ciudad'; ?></th>
                            <th><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></th>
                            <th><?php echo isset($lang['type']) ? $lang['type'] : 'Tipo'; ?></th>
                            <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></th>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($oficinas as $oficina): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo $oficina['idOficina']; ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($oficina['nombre']); ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($oficina['direccion']); ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($oficina['ciudad']); ?></td>
                                <td class="text-center"><?php echo htmlspecialchars($oficina['telefono']); ?></td>
                                <td class="text-center">
                                    <?php if ($oficina['central'] == 1): ?>
                                        <span class="badge bg-success"><?php echo isset($lang['central']) ? $lang['central'] : 'Central'; ?></span>
                                    <?php else: ?>
                                        <span class="badge bg-info"><?php echo isset($lang['branch']) ? $lang['branch'] : 'Agencia'; ?></span>
                                    <?php endif; ?>
                                </td>
                                <td class="text-center">
                                    <?php if ($oficina['estado'] == 'activa'): ?>
                                        <span class="badge bg-success"><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></span>
                                    <?php else: ?>
                                        <span class="badge bg-danger"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></span>
                                    <?php endif; ?>
                                </td>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <a href="index.php?controller=oficina&action=ver&id=<?php echo $oficina['idOficina']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <a href="index.php?controller=oficina&action=editar&id=<?php echo $oficina['idOficina']; ?>" class="btn-action btn-edit" title="<?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                        <?php if ($oficina['estado'] == 'activa'): ?>
                                            <a href="index.php?controller=oficina&action=desactivar&id=<?php echo $oficina['idOficina']; ?>" class="btn-action btn-info" title="<?php echo isset($lang['deactivate']) ? $lang['deactivate'] : 'Desactivar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_deactivate_branch']) ? $lang['confirm_deactivate_branch'] : '¿Está seguro que desea desactivar esta oficina?'; ?>')">
                                                <i class="fa fa-times"></i>
                                            </a>
                                        <?php else: ?>
                                            <a href="index.php?controller=oficina&action=activar&id=<?php echo $oficina['idOficina']; ?>" class="btn-action btn-info" title="<?php echo isset($lang['activate']) ? $lang['activate'] : 'Activar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_activate_branch']) ? $lang['confirm_activate_branch'] : '¿Está seguro que desea activar esta oficina?'; ?>')">
                                                <i class="fa fa-check"></i>
                                            </a>
                                        <?php endif; ?>
                                        <a href="index.php?controller=oficina&action=eliminar&id=<?php echo $oficina['idOficina']; ?>" class="btn-action btn-delete delete-button" title="<?php echo isset($lang['delete']) ? $lang['delete'] : 'Eliminar'; ?>" onclick="return confirm('<?php echo isset($lang['confirm_delete_branch']) ? $lang['confirm_delete_branch'] : '¿Está seguro que desea eliminar esta oficina? Esta acción no se puede deshacer.'; ?>')">
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