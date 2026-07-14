<h1><?php echo $lang['client_list']; ?></h1>

<div class="card">
    <div class="card-header">
        <div class="row">
            <div class="col-md-6">
                <form method="GET" action="index.php" class="search-form">
                    <input type="hidden" name="controller" value="cliente">
                    <input type="hidden" name="action" value="listar">
                    <div class="input-group">
                        <input type="text" class="form-control" name="busqueda" placeholder="<?php echo $lang['search']; ?>..." value="<?php echo isset($_GET['busqueda']) ? htmlspecialchars($_GET['busqueda']) : ''; ?>">
                        <button type="submit" class="btn btn-primary"><?php echo $lang['search']; ?></button>
                    </div>
                </form>
            </div>
            <div class="col-md-6 text-right">
                <a href="index.php?controller=cliente&action=crear" class="btn btn-success">
                    <i class="icon-plus"></i> <?php echo $lang['new_client']; ?>
                </a>
            </div>
        </div>
    </div>
    <div class="card-body">
        <?php if (count($clientes) > 0): ?>
            <div class="table-responsive">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo $lang['name']; ?></th>
                            <th><?php echo $lang['paternal_last_name']; ?></th>
                            <th><?php echo $lang['maternal_last_name']; ?></th>
                            <th><?php echo $lang['id_number']; ?></th>
                            <th><?php echo $lang['phone']; ?></th>
                            <th><?php echo $lang['email']; ?></th>
                            <th><?php echo $lang['branches']; ?></th>
                            <th><?php echo $lang['actions']; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($clientes as $cliente): ?>
                            <tr>
                                <td><?php echo $cliente['idPersona']; ?></td>
                                <td><?php echo htmlspecialchars($cliente['nombre']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['apellidoPaterno']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['apellidoMaterno']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['ci']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['telefono']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['email']); ?></td>
                                <td><?php echo htmlspecialchars($cliente['oficina_nombre']); ?></td>
                                <td>
                                    <div class="btn-group" role="group">
                                        <a href="index.php?controller=cliente&action=ver&id=<?php echo $cliente['idPersona']; ?>" class="btn btn-sm btn-info" title="<?php echo $lang['view']; ?>">
                                            <i class="icon-eye"></i>
                                        </a>
                                        <a href="index.php?controller=cliente&action=editar&id=<?php echo $cliente['idPersona']; ?>" class="btn btn-sm btn-warning" title="<?php echo $lang['edit']; ?>">
                                            <i class="icon-edit"></i>
                                        </a>
                                        <a href="index.php?controller=cliente&action=eliminar&id=<?php echo $cliente['idPersona']; ?>" class="btn btn-sm btn-danger delete-button" title="<?php echo $lang['delete']; ?>" onclick="return confirm('<?php echo $lang['confirm_delete']; ?>')">
                                            <i class="icon-trash"></i>
                                        </a>
                                    </div>
                                </td>
                            </tr>
                        <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        <?php else: ?>
            <div class="alert alert-info">
                <?php echo $lang['no_clients']; ?>
            </div>
        <?php endif; ?>
    </div>
</div>