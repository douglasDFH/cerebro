<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleListar.css">

<div class="card client-card">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['reports']) ? $lang['reports'] : 'Reportes'; ?></h2>
        <div class="header-buttons">
            <a href="javascript:window.print();" class="btn-new-client">
                <i class="fa fa-print"></i> <?php echo isset($lang['print']) ? $lang['print'] : 'Imprimir'; ?>
            </a>
        </div>
    </div>
    <div class="card-body">
        <div class="alert alert-mercantil">
            <strong><?php echo isset($lang['note']) ? $lang['note'] : 'Nota'; ?>:</strong> <?php echo isset($lang['reports_note']) ? $lang['reports_note'] : 'Los reportes se generan en tiempo real con datos actualizados del sistema.'; ?>
        </div>
        
        <!-- Sección: Reportes de Transacciones y Estadísticas -->
        <div class="section-header">
            <h3><?php echo isset($lang['transactions_stats_reports']) ? $lang['transactions_stats_reports'] : 'Reportes de Transacciones y Estadísticas'; ?></h3>
        </div>
        
        <div class="table-container">
            <table class="table table-mercantil">
                <thead>
                    <tr>
                        <th><?php echo isset($lang['report_name']) ? $lang['report_name'] : 'Nombre del Reporte'; ?></th>
                        <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                        <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Fila 1 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-calendar report-icon"></i>
                                <?php echo isset($lang['transactions_by_date']) ? $lang['transactions_by_date'] : 'Transacciones por Fecha'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['transactions_by_date_desc']) ? $lang['transactions_by_date_desc'] : 'Informe detallado de transacciones realizadas en un rango de fechas seleccionado.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=transaccionesPorFecha" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 2 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-user report-icon"></i>
                                <?php echo isset($lang['transactions_by_client']) ? $lang['transactions_by_client'] : 'Transacciones por Cliente'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['transactions_by_client_desc']) ? $lang['transactions_by_client_desc'] : 'Historial de transacciones agrupadas por cliente para un análisis detallado de actividad.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=transaccionesPorCliente" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 3 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-credit-card report-icon"></i>
                                <?php echo isset($lang['atm_statistics']) ? $lang['atm_statistics'] : 'Estadísticas de ATM'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['atm_statistics_desc']) ? $lang['atm_statistics_desc'] : 'Análisis de uso de cajeros automáticos, transacciones y disponibilidad.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=estadisticasATM" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <!-- Sección: Reportes de Saldos y Actividad -->
        <div class="section-header">
            <h3><?php echo isset($lang['balances_activity_reports']) ? $lang['balances_activity_reports'] : 'Reportes de Saldos y Actividad'; ?></h3>
        </div>
        
        <div class="table-container">
            <table class="table table-mercantil">
                <thead>
                    <tr>
                        <th><?php echo isset($lang['report_name']) ? $lang['report_name'] : 'Nombre del Reporte'; ?></th>
                        <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                        <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Fila 1 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-building report-icon"></i>
                                <?php echo isset($lang['balances_by_branch']) ? $lang['balances_by_branch'] : 'Saldos por Oficina'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['balances_by_branch_desc']) ? $lang['balances_by_branch_desc'] : 'Reporte de saldos consolidados por oficina y tipo de cuenta.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=saldosPorOficina" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 2 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-file-alt report-icon"></i>
                                <?php echo isset($lang['account_activity']) ? $lang['account_activity'] : 'Actividad de Cuenta'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['account_activity_desc']) ? $lang['account_activity_desc'] : 'Historial detallado de actividades para todas las cuentas en un período seleccionado.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=actividadCuentas" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 3 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-chart-line report-icon"></i>
                                <?php echo isset($lang['executive_summary']) ? $lang['executive_summary'] : 'Resumen Ejecutivo'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['executive_summary_desc']) ? $lang['executive_summary_desc'] : 'Resumen general de estadísticas e indicadores clave para la gerencia.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=resumenEjecutivo" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
        <!-- Sección: Reportes de Rendimiento y Auditoría -->
        <div class="section-header">
            <h3><?php echo isset($lang['performance_audit_reports']) ? $lang['performance_audit_reports'] : 'Reportes de Rendimiento y Auditoría'; ?></h3>
        </div>
        
        <div class="table-container">
            <table class="table table-mercantil">
                <thead>
                    <tr>
                        <th><?php echo isset($lang['report_name']) ? $lang['report_name'] : 'Nombre del Reporte'; ?></th>
                        <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                        <th class="actions-column"><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                    </tr>
                </thead>
                <tbody>
                    <!-- Fila 1 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-cube report-icon"></i>
                                <?php echo isset($lang['product_performance']) ? $lang['product_performance'] : 'Rendimiento por Producto'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['product_performance_desc']) ? $lang['product_performance_desc'] : 'Análisis de rendimiento para cada producto financiero ofrecido por el banco.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=rendimientoProductos" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 2 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-shield-alt report-icon"></i>
                                <?php echo isset($lang['system_audit']) ? $lang['system_audit'] : 'Auditoría del Sistema'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['system_audit_desc']) ? $lang['system_audit_desc'] : 'Registro de todas las operaciones y cambios realizados en el sistema por usuario.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=auditoriaSistema" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Fila 3 -->
                    <tr class="animate-row">
                        <td class="text-left">
                            <div class="report-name">
                                <i class="fa fa-star report-icon"></i>
                                <?php echo isset($lang['service_metrics']) ? $lang['service_metrics'] : 'Métricas de Servicio'; ?>
                            </div>
                        </td>
                        <td class="text-left"><?php echo isset($lang['service_metrics_desc']) ? $lang['service_metrics_desc'] : 'Estadísticas de calidad de servicio y satisfacción del cliente en todas las sucursales.'; ?></td>
                        <td class="text-center actions-cell">
                            <div class="action-buttons">
                                <a href="index.php?controller=reporte&action=metricasServicio" class="btn-action btn-view" title="<?php echo isset($lang['view_report']) ? $lang['view_report'] : 'Ver Reporte'; ?>">
                                    <i class="fa fa-chart-bar"></i>
                                </a>
                            </div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</div>


<script>
document.addEventListener('DOMContentLoaded', function() {
    // Asignar retrasos de animación a las filas
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        const sectionIndex = Math.floor(index / 3);
        const rowInSection = index % 3;
        row.style.animationDelay = `${0.3 + (sectionIndex * 0.2) + (rowInSection * 0.1)}s`;
    });
    
    // Efectos de hover para las filas
    rows.forEach(row => {
        row.addEventListener('mouseenter', function() {
            this.style.transition = 'transform 0.2s ease, box-shadow 0.2s ease';
            this.style.zIndex = '1';
        });
        
        row.addEventListener('mouseleave', function() {
            this.style.zIndex = 'auto';
        });
    });
    
    // Animar la alerta al cargar
    const alertBox = document.querySelector('.alert-mercantil');
    if (alertBox) {
        alertBox.style.animation = 'fadeInUp 0.5s ease forwards';
    }
    
    // Animar los encabezados de sección
    const sectionHeaders = document.querySelectorAll('.section-header');
    sectionHeaders.forEach((header, index) => {
        header.style.opacity = '0';
        header.style.animation = `fadeInUp 0.5s ease ${0.3 + (index * 0.1)}s forwards`;
    });
    
    // Efecto ripple para botones
    const actionButtons = document.querySelectorAll('.btn-action');
    actionButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            const x = e.clientX - e.target.getBoundingClientRect().left;
            const y = e.clientY - e.target.getBoundingClientRect().top;
            
            const ripple = document.createElement('span');
            ripple.style.position = 'absolute';
            ripple.style.backgroundColor = 'rgba(255, 255, 255, 0.7)';
            ripple.style.borderRadius = '50%';
            ripple.style.width = '5px';
            ripple.style.height = '5px';
            ripple.style.top = y + 'px';
            ripple.style.left = x + 'px';
            ripple.style.transform = 'scale(0)';
            ripple.style.animation = 'ripple 0.6s linear';
            
            this.appendChild(ripple);
            
            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
    });
});

</script>