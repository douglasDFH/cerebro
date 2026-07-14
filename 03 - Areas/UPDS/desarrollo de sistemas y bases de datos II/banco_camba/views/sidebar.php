<div class="sidebar">
    <ul class="menu">
        <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'dashboard' ? 'active' : ''; ?>">
            <a href="index.php?controller=dashboard&action=index">Dashboard</a>
        </li>
        <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'cliente' ? 'active' : ''; ?>">
            <a href="index.php?controller=cliente&action=listar">Clientes</a>
        </li>
        <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'cuenta' ? 'active' : ''; ?>">
            <a href="index.php?controller=cuenta&action=listar">Cuentas</a>
        </li>
        <li class="<?php echo isset($_GET['controller']) && $_GET['controller'] == 'transaccion' ? 'active' : ''; ?>">
            <a href="index.php?controller=transaccion&action=listar">Transacciones</a>
        </li>
    </ul>
</div>