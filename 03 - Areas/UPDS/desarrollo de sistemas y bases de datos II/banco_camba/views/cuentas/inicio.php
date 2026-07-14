<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Inicio - Sistema Bancario</title>
    <link rel="stylesheet" href="<?php echo BASE_URL; ?>assets/css/styles.css">
</head>
<body>
    <?php include 'views/includes/header.php'; ?>
    
    <div class="container">
        <div class="welcome-section">
            <h1>Bienvenido al Sistema de Gestión Bancaria</h1>
            <p class="subtitle">Una plataforma completa para gestionar clientes, cuentas y transacciones bancarias</p>
        </div>
        
        <div class="dashboard">
            <div class="dashboard-card">
                <div class="card-icon">
                    <i class="fas fa-users"></i>
                </div>
                <div class="card-content">
                    <h2>Gestión de Clientes</h2>
                    <p>Administre información de clientes, registre nuevos clientes y actualice sus datos.</p>
                    <a href="<?php echo BASE_URL; ?>clientes/listar" class="btn btn-primary">Ir a Clientes</a>
                </div>
            </div>
            
            <div class="dashboard-card">
                <div class="card-icon">
                    <i class="fas fa-wallet"></i>
                </div>
                <div class="card-content">
                    <h2>Gestión de Cuentas</h2>
                    <p>Cree y administre cuentas bancarias, consulte saldos y vea movimientos.</p>
                    <a href="<?php echo BASE_URL; ?>cuentas/listar" class="btn btn-primary">Ir a Cuentas</a>
                </div>
            </div>
            
            <div class="dashboard-card">
                <div class="card-icon">
                    <i class="fas fa-exchange-alt"></i>
                </div>
                <div class="card-content">
                    <h2>Transacciones</h2>
                    <p>Realice depósitos, retiros y transferencias entre cuentas de manera rápida y segura.</p>
                    <div class="btn-group">
                        <a href="<?php echo BASE_URL; ?>cuentas/depositar" class="btn btn-success">Depósito</a>
                        <a href="<?php echo BASE_URL; ?>cuentas/retirar" class="btn btn-warning">Retiro</a>
                        <a href="<?php echo BASE_URL; ?>cuentas/transferir" class="btn btn-info">Transferencia</a>
                    </div>
                </div>
            </div>
            
            <div class="dashboard-card">
                <div class="card-icon">
                    <i class="fas fa-search-dollar"></i>
                </div>
                <div class="card-content">
                    <h2>Consultas</h2>
                    <p>Consulte saldos y movimientos de cuentas de forma rápida y sencilla.</p>
                    <a href="<?php echo BASE_URL; ?>cuentas/consultarSaldo" class="btn btn-primary">Consultar Saldo</a>
                </div>
            </div>
        </div>
        
        <div class="quick-access">
            <h2>Acceso Rápido</h2>
            <div class="quick-links">
                <a href="<?php echo BASE_URL; ?>clientes/crear" class="quick-link">
                    <i class="fas fa-user-plus"></i>
                    <span>Nuevo Cliente</span>
                </a>
                <a href="<?php echo BASE_URL; ?>cuentas/crear" class="quick-link">
                    <i class="fas fa-plus-circle"></i>
                    <span>Nueva Cuenta</span>
                </a>
                <a href="<?php echo BASE_URL; ?>cuentas/depositar" class="quick-link">
                    <i class="fas fa-hand-holding-usd"></i>
                    <span>Depósito</span>
                </a>
                <a href="<?php echo BASE_URL; ?>cuentas/retirar" class="quick-link">
                    <i class="fas fa-money-bill-wave"></i>
                    <span>Retiro</span>
                </a>
                <a href="<?php echo BASE_URL; ?>cuentas/transferir" class="quick-link">
                    <i class="fas fa-exchange-alt"></i>
                    <span>Transferencia</span>
                </a>
                <a href="<?php echo BASE_URL; ?>cuentas/consultarSaldo" class="quick-link">
                    <i class="fas fa-search"></i>
                    <span>Consultar Saldo</span>
                </a>
            </div>
        </div>
    </div>
    
    <?php include 'views/includes/footer.php'; ?>
    <script src="<?php echo BASE_URL; ?>assets/js/scripts.js"></script>
</body>
</html>