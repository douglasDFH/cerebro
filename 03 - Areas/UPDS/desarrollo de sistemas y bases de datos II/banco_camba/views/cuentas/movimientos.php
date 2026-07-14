<?php
// Verificar si hay mensajes de error o éxito para mostrar
$errorMsg = isset($_SESSION['error']) ? $_SESSION['error'] : null;
$successMsg = isset($_SESSION['success']) ? $_SESSION['success'] : null;

// Limpiar mensajes de sesión después de obtenerlos
if (isset($_SESSION['error'])) {
    unset($_SESSION['error']);
}
if (isset($_SESSION['success'])) {
    unset($_SESSION['success']);
}

// Obtener información de la cuenta y sus movimientos
$cuenta = $data['cuenta'] ?? null;
$movimientos = $data['movimientos'] ?? [];
?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Movimientos de Cuenta - Sistema Bancario</title>
    <link rel="stylesheet" href="<?php echo BASE_URL; ?>assets/css/styles.css">
</head>
<body>
    <?php include 'views/includes/header.php'; ?>
    
    <div class="container">
        <div class="header-actions">
            <h1>Movimientos de Cuenta</h1>
            <a href="<?php echo BASE_URL; ?>cuentas/listar" class="btn btn-secondary">Volver a Cuentas</a>
        </div>
        
        <?php if ($errorMsg): ?>
            <div class="alert alert-error">
                <?php echo $errorMsg; ?>
            </div>
        <?php endif; ?>
        
        <?php if ($successMsg): ?>
            <div class="alert alert-success">
                <?php echo $successMsg; ?>
            </div>
        <?php endif; ?>
        
        <?php if ($cuenta): ?>
            <div class="card">
                <div class="card-header">
                    <h2>Información de la Cuenta</h2>
                </div>
                <div class="card-body">
                    <div class="info-group">
                        <p><strong>Número de Cuenta:</strong> <?php echo $cuenta['numero_cuenta']; ?></p>
                        <p><strong>Tipo de Cuenta:</strong> <?php echo $cuenta['tipo_cuenta']; ?></p>
                        <p><strong>Titular:</strong> <?php echo $cuenta['nombre_cliente'] . ' ' . $cuenta['apellido_cliente']; ?></p>
                        <p><strong>Saldo Actual:</strong> $<?php echo number_format($cuenta['saldo'], 2, '.', ','); ?></p>
                        <p><strong>Estado:</strong> 
                            <span class="badge <?php echo $cuenta['estado'] == 1 ? 'badge-success' : 'badge-danger'; ?>">
                                <?php echo $cuenta['estado'] == 1 ? 'Activa' : 'Inactiva'; ?>
                            </span>
                        </p>
                        <p><strong>Fecha de Apertura:</strong> <?php echo date('d/m/Y', strtotime($cuenta['fecha_apertura'])); ?></p>
                    </div>
                </div>
            </div>
            
            <div class="card mt-20">
                <div class="card-header">
                    <h2>Historial de Movimientos</h2>
                    <div class="filter-form">
                        <form action="<?php echo BASE_URL; ?>cuentas/movimientos/<?php echo $cuenta['id']; ?>" method="GET">
                            <div class="form-row">
                                <div class="form-group">
                                    <label for="fecha_desde">Desde:</label>
                                    <input type="date" id="fecha_desde" name="fecha_desde" 
                                           value="<?php echo isset($_GET['fecha_desde']) ? $_GET['fecha_desde'] : ''; ?>">
                                </div>
                                <div class="form-group">
                                    <label for="fecha_hasta">Hasta:</label>
                                    <input type="date" id="fecha_hasta" name="fecha_hasta" 
                                           value="<?php echo isset($_GET['fecha_hasta']) ? $_GET['fecha_hasta'] : ''; ?>">
                                </div>
                                <div class="form-group">
                                    <label for="tipo">Tipo:</label>
                                    <select id="tipo" name="tipo">
                                        <option value="">Todos</option>
                                        <option value="Depósito" <?php echo (isset($_GET['tipo']) && $_GET['tipo'] == 'Depósito') ? 'selected' : ''; ?>>Depósito</option>
                                        <option value="Retiro" <?php echo (isset($_GET['tipo']) && $_GET['tipo'] == 'Retiro') ? 'selected' : ''; ?>>Retiro</option>
                                        <option value="Transferencia Recibida" <?php echo (isset($_GET['tipo']) && $_GET['tipo'] == 'Transferencia Recibida') ? 'selected' : ''; ?>>Transferencia Recibida</option>
                                        <option value="Transferencia Enviada" <?php echo (isset($_GET['tipo']) && $_GET['tipo'] == 'Transferencia Enviada') ? 'selected' : ''; ?>>Transferencia Enviada</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <button type="submit" class="btn btn-primary">Filtrar</button>
                                    <a href="<?php echo BASE_URL; ?>cuentas/movimientos/<?php echo $cuenta['id']; ?>" class="btn btn-secondary">Limpiar</a>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <div class="card-body">
                    <?php if (empty($movimientos)): ?>
                        <p>No hay movimientos registrados para esta cuenta o que coincidan con los filtros aplicados.</p>
                    <?php else: ?>
                        <div class="table-responsive">
                            <table class="table">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Fecha</th>
                                        <th>Tipo</th>
                                        <th>Monto</th>
                                        <th>Saldo Después</th>
                                        <th>Descripción</th>
                                        <th>Origen/Destino</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <?php foreach ($movimientos as $movimiento): ?>
                                        <tr>
                                            <td><?php echo $movimiento['id']; ?></td>
                                            <td><?php echo date('d/m/Y H:i', strtotime($movimiento['fecha'])); ?></td>
                                            <td><?php echo $movimiento['tipo']; ?></td>
                                            <td class="<?php echo in_array($movimiento['tipo'], ['Depósito', 'Transferencia Recibida']) ? 'text-success' : 'text-danger'; ?>">
                                                <?php echo in_array($movimiento['tipo'], ['Depósito', 'Transferencia Recibida']) ? '+' : '-'; ?>
                                                $<?php echo number_format($movimiento['monto'], 2, '.', ','); ?>
                                            </td>
                                            <td>$<?php echo number_format($movimiento['saldo_resultante'], 2, '.', ','); ?></td>
                                            <td><?php echo $movimiento['descripcion'] ?: 'N/A'; ?></td>
                                            <td>
                                                <?php 
                                                if ($movimiento['tipo'] == 'Transferencia Recibida' && !empty($movimiento['cuenta_origen'])) {
                                                    echo 'De: ' . $movimiento['cuenta_origen'];
                                                } elseif ($movimiento['tipo'] == 'Transferencia Enviada' && !empty($movimiento['cuenta_destino'])) {
                                                    echo 'Para: ' . $movimiento['cuenta_destino'];
                                                } else {
                                                    echo 'N/A';
                                                }
                                                ?>
                                            </td>
                                        </tr>
                                    <?php endforeach; ?>
                                </tbody>
                            </table>
                        </div>
                    <?php endif; ?>
                </div>
            </div>
        <?php else: ?>
            <div class="alert alert-error">
                <p>No se encontró la cuenta especificada.</p>
            </div>
        <?php endif; ?>
    </div>
    
    <?php include 'views/includes/footer.php'; ?>
    <script src="<?php echo BASE_URL; ?>assets/js/scripts.js"></script>
</body>
</html>