<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">
<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['client_list']) ? $lang['client_list'] : 'Lista de Clientes'; ?></h2>
        <a href="index.php?controller=cliente&action=crear" class="btn-new-client">
            <i class="fa fa-plus-circle"></i> <?php echo isset($lang['new_client']) ? $lang['new_client'] : 'Nuevo Cliente'; ?>
        </a>
    </div>
    <div class="card-body">
        <?php if (empty($clientes)): ?>
            <div class="alert alert-mercantil"><?php echo isset($lang['no_clients_registered']) ? $lang['no_clients_registered'] : 'No hay clientes registrados en el sistema.'; ?></div>
        <?php else: ?>
            <div class="table-container">
                <table class="table table-mercantil">
                    <thead>
                        <tr>
                            <th><?php echo isset($lang['identity_card']) ? $lang['identity_card'] : 'Cédula de Identidad'; ?></th>
                            <th><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?></th>
                            <th><?php echo isset($lang['paternal_surname']) ? $lang['paternal_surname'] : 'Apellido Paterno'; ?></th>
                            <th><?php echo isset($lang['maternal_surname']) ? $lang['maternal_surname'] : 'Apellido Materno'; ?></th>
                            <th><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></th>
                            <th><?php echo isset($lang['office']) ? $lang['office'] : 'Oficina'; ?></th>
                            <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($clientes as $cliente): ?>
                            <tr class="animate-row">
                                <td class="text-center"><?php echo $cliente['ci']; ?></td>
                                <td class="text-center"><?php echo $cliente['nombre']; ?></td>
                                <td class="text-center"><?php echo $cliente['apellidoPaterno']; ?></td>
                                <td class="text-center"><?php echo $cliente['apellidoMaterno']; ?></td>
                                <td class="text-center"><?php echo $cliente['telefono']; ?></td>
                                <td class="text-center"><?php echo $cliente['oficina_nombre']; ?></td>
                                <td class="text-center actions-cell">
                                    <div class="action-buttons">
                                        <a href="index.php?controller=cliente&action=ver&id=<?php echo $cliente['idPersona']; ?>" class="btn-action btn-view" title="<?php echo isset($lang['view_details']) ? $lang['view_details'] : 'Ver detalles'; ?>">
                                            <i class="fa fa-eye"></i>
                                        </a>
                                        <a href="index.php?controller=cliente&action=editar&id=<?php echo $cliente['idPersona']; ?>" class="btn-action btn-edit" title="<?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>">
                                            <i class="fa fa-edit"></i>
                                        </a>
                                        <a href="index.php?controller=cliente&action=eliminar&id=<?php echo $cliente['idPersona']; ?>" class="btn-action btn-delete delete-button" title="<?php echo isset($lang['delete']) ? $lang['delete'] : 'Eliminar'; ?>">
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