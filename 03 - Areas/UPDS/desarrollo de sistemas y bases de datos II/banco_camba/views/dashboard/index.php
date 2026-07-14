<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <title>Dashboard -Sistema Banco Mercantil</title>
    <link rel="stylesheet" href="assets/css/styles.css">
</head>
<body>
    <div class="header">
        <div class="logo">Banco Mercantil</div>
        <div class="user-info">
            <div class="user-name">Usuario : <?php echo isset($_SESSION['nombre']) ? $_SESSION['nombre'] : 'Usuario'; ?></div>
            <a href="index.php?controller=usuario&action=cerrarSesion" class="btn btn-secondary">Cerrar Sesión</a>
        </div>
    </div>
    
    <div class="main-container">
        <div class="sidebar">
            <ul class="sidebar-menu">
                <li class="sidebar-menu-item active">Dashboard</li>
                <li class="sidebar-menu-item">Clientes</li>
                <li class="sidebar-menu-item">Cuentas</li>
                <li class="sidebar-menu-item">Transacciones</li>
            </ul>
        </div>
        
        <div class="content">
            <h1>Bienvenido al sistema Banco Mercantil</h1>
            
            <div class="dashboard">
                <div class="card">
                    <div class="card-header">Clientes</div>
                    <div class="card-body">
                        <p>Total de clientes: <span id="total-clients"><?php echo isset($totalClientes) ? $totalClientes : 0; ?></span></p>
                        <a href="index.php?controller=cliente&action=crear" class="btn btn-primary">Nuevo Cliente</a>
                    </div>
                </div>
                
                <div class="card">
                    <div class="card-header">Cuentas</div>
                    <div class="card-body">
                        <p>Total de cuentas: <span id="total-accounts"><?php echo isset($totalCuentas) ? $totalCuentas : 0; ?></span></p>
                        <a href="index.php?controller=cuenta&action=crear" class="btn btn-primary">Nueva Cuenta</a>
                    </div>
                </div>
                
                <div class="card">
                    <div class="card-header">Transacciones</div>
                    <div class="card-body">
                        <p>Transacciones hoy: <span id="today-transactions"><?php echo isset($transaccionesHoy) ? $transaccionesHoy : 0; ?></span></p>
                        <a href="index.php?controller=transaccion&action=crear" class="btn btn-primary">Nueva Transacción</a>
                    </div>
                </div>
            </div>
            
            <div class="card">
                <div class="card-header">Transacciones Recientes</div>
                <div class="card-body">
                    <p>No hay transacciones recientes.</p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
