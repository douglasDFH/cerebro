<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/StyleVerOficina.css">
<!-- Contenedor principal sin márgenes laterales -->
<div class="content-wrapper">
    <!-- Encabezado con título y badge de modo visualización -->
    <div class="header-container">
        <h2 class="page-title"><?php echo isset($lang['branch_details']) ? $lang['branch_details'] : 'Detalles de Oficina'; ?></h2>
        <div class="mode-visualizacion">
            <span class="mode-icon">👁️</span> <?php echo isset($lang['view_mode']) ? $lang['view_mode'] : 'Modo Visualización'; ?>
        </div>
    </div>

    <!-- Resumen de oficina -->
    <div class="client-summary">
        <div class="avatar-box">
            <div class="client-avatar">
                <i class="fas fa-building"></i>
            </div>
        </div>
        <div class="client-info">
            <h2 class="client-name"><?php echo htmlspecialchars($this->model->nombre); ?></h2>
            <p class="client-identification"><?php echo isset($lang['id']) ? $lang['id'] : 'ID'; ?>: <?php echo $this->model->idOficina; ?></p>
        </div>
    </div>

    <!-- Campos principales de información -->
    <div class="details-container">
        <div class="details-row">
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['id']) ? $lang['id'] : 'ID Oficina'; ?></label>
                <div class="detail-value"><?php echo $this->model->idOficina; ?></div>
            </div>
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></label>
                <div class="detail-value"><?php echo !empty($this->model->telefono) ? htmlspecialchars($this->model->telefono) : '—'; ?></div>
            </div>
        </div>
        
        <div class="details-row">
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?></label>
                <div class="detail-value"><?php echo htmlspecialchars($this->model->nombre); ?></div>
            </div>
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['office_hours']) ? $lang['office_hours'] : 'Horario de Atención'; ?></label>
                <div class="detail-value"><?php echo !empty($this->model->horarioAtencion) ? htmlspecialchars($this->model->horarioAtencion) : '—'; ?></div>
            </div>
        </div>
        
        <div class="details-row">
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['address']) ? $lang['address'] : 'Dirección'; ?></label>
                <div class="detail-value"><?php echo htmlspecialchars($this->model->direccion); ?></div>
            </div>
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['manager']) ? $lang['manager'] : 'Gerente Encargado'; ?></label>
                <div class="detail-value"><?php echo !empty($this->model->gerenteEncargado) ? htmlspecialchars($this->model->gerenteEncargado) : '—'; ?></div>
            </div>
        </div>
        
        <div class="details-row">
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['city']) ? $lang['city'] : 'Ciudad'; ?></label>
                <div class="detail-value"><?php echo htmlspecialchars($this->model->ciudad); ?></div>
            </div>
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Inauguración'; ?></label>
                <div class="detail-value">
                    <?php if (!empty($this->model->fechaInauguracion)): ?>
                        <?php echo date('d/m/Y', strtotime($this->model->fechaInauguracion)); ?>
                    <?php else: ?>
                        —
                    <?php endif; ?>
                </div>
            </div>
        </div>
        
        <div class="details-row">
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['state']) ? $lang['state'] : 'Departamento'; ?></label>
                <div class="detail-value highlight-value"><?php echo htmlspecialchars($this->model->departamento); ?></div>
            </div>
            <div class="details-cell">
                <label class="detail-label"><?php echo isset($lang['country']) ? $lang['country'] : 'País'; ?></label>
                <div class="detail-value"><?php echo htmlspecialchars($this->model->pais); ?></div>
            </div>
        </div>
    </div>

    <!-- Tipo de Oficina y Estado en la misma fila -->
    <div class="option-row-container">
        <div class="option-section">
            <label class="option-label"><?php echo isset($lang['branch_type']) ? $lang['branch_type'] : 'Tipo de Oficina'; ?></label>
            <div class="option-buttons">
                <div class="option-button <?php echo $this->model->central == 1 ? 'selected' : ''; ?>">
                    <i class="fas fa-building"></i> <?php echo isset($lang['central']) ? $lang['central'] : 'Central'; ?>
                </div>
                <div class="option-button <?php echo $this->model->central == 0 ? 'selected' : ''; ?>">
                    <i class="fas fa-store"></i> <?php echo isset($lang['branch']) ? $lang['branch'] : 'Sucursal'; ?>
                </div>
            </div>
        </div>

        <div class="option-section">
            <label class="option-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></label>
            <div class="option-buttons">
                <div class="option-button status-active <?php echo $this->model->estado == 'activa' ? 'selected' : ''; ?>">
                    <i class="fas fa-check-circle"></i> <?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?>
                </div>
                <div class="option-button status-inactive <?php echo $this->model->estado != 'activa' ? 'selected' : ''; ?>">
                    <i class="fas fa-times-circle"></i> <?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?>
                </div>
            </div>
        </div>
    </div>

    <!-- Sección de empleados -->
    <div class="employees-section">
        <div class="section-header">
            <h3 class="section-title"><?php echo isset($lang['employees']) ? $lang['employees'] : 'Empleados'; ?></h3>
            <a href="index.php?controller=empleado&action=crear&idOficina=<?php echo $this->model->idOficina; ?>" class="add-button">
                <i class="fas fa-plus-circle"></i> <?php echo isset($lang['new_employee']) ? $lang['new_employee'] : 'Nuevo Empleado'; ?>
            </a>
        </div>
        
        <?php if (empty($empleados)): ?>
        <div class="no-employees">
            <i class="fas fa-users-slash empty-icon"></i>
            <p><?php echo isset($lang['no_employees']) ? $lang['no_employees'] : 'No hay empleados asignados a esta oficina.'; ?></p>
            <a href="index.php?controller=empleado&action=crear&idOficina=<?php echo $this->model->idOficina; ?>" class="add-employee-btn">
                <i class="fas fa-plus-circle"></i> <?php echo isset($lang['add_employee']) ? $lang['add_employee'] : 'Agregar Empleado'; ?>
            </a>
        </div>
        <?php else: ?>
        <div class="table-container">
            <table class="data-table">
                <thead>
                    <tr>
                        <th><?php echo isset($lang['id']) ? $lang['id'] : 'ID'; ?></th>
                        <th><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?></th>
                        <th><?php echo isset($lang['position']) ? $lang['position'] : 'Cargo'; ?></th>
                        <th><?php echo isset($lang['email']) ? $lang['email'] : 'Correo electrónico'; ?></th>
                        <th><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?></th>
                        <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?></th>
                        <th><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($empleados as $empleado): ?>
                    <tr>
                        <td><?php echo $empleado['idEmpleado']; ?></td>
                        <td class="employee-name">
                            <?php echo htmlspecialchars($empleado['nombre'] . ' ' . $empleado['apellidoPaterno'] . ' ' . $empleado['apellidoMaterno']); ?>
                        </td>
                        <td><?php echo htmlspecialchars($empleado['cargo']); ?></td>
                        <td><a href="mailto:<?php echo $empleado['email']; ?>"><?php echo htmlspecialchars($empleado['email']); ?></a></td>
                        <td><?php echo htmlspecialchars($empleado['telefono']); ?></td>
                        <td>
                            <?php if ($empleado['estado'] == 'activo'): ?>
                            <span class="status-pill active"><?php echo isset($lang['active']) ? $lang['active'] : 'Activo'; ?></span>
                            <?php else: ?>
                            <span class="status-pill inactive"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactivo'; ?></span>
                            <?php endif; ?>
                        </td>
                        <td>
                            <div class="action-buttons">
                                <a href="index.php?controller=empleado&action=ver&id=<?php echo $empleado['idEmpleado']; ?>" class="action-btn view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                    <i class="fas fa-eye"></i>
                                </a>
                                <a href="index.php?controller=empleado&action=editar&id=<?php echo $empleado['idEmpleado']; ?>" class="action-btn edit" title="<?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>">
                                    <i class="fas fa-edit"></i>
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

    <!-- Botones de acción -->
    <div class="actions-container">
        <a href="index.php?controller=oficina&action=editar&id=<?php echo $this->model->idOficina; ?>" class="action-main edit-btn">
            <i class="fas fa-edit"></i> <?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>
        </a>
        
        <a href="index.php?controller=empleado&action=crear&idOficina=<?php echo $this->model->idOficina; ?>" class="action-main new-employee-btn">
            <i class="fas fa-user-plus"></i> <?php echo isset($lang['new_employee']) ? $lang['new_employee'] : 'Nuevo Empleado'; ?>
        </a>
        
        <?php if ($this->model->estado == 'activa'): ?>
        <a href="index.php?controller=oficina&action=desactivar&id=<?php echo $this->model->idOficina; ?>" class="action-main deactivate-btn" onclick="return confirm('<?php echo isset($lang['confirm_deactivate']) ? $lang['confirm_deactivate'] : '¿Está seguro que desea desactivar esta oficina?'; ?>')">
            <i class="fas fa-ban"></i> <?php echo isset($lang['deactivate']) ? $lang['deactivate'] : 'Desactivar'; ?>
        </a>
        <?php else: ?>
        <a href="index.php?controller=oficina&action=activar&id=<?php echo $this->model->idOficina; ?>" class="action-main activate-btn" onclick="return confirm('<?php echo isset($lang['confirm_activate']) ? $lang['confirm_activate'] : '¿Está seguro que desea activar esta oficina?'; ?>')">
            <i class="fas fa-check"></i> <?php echo isset($lang['activate']) ? $lang['activate'] : 'Activar'; ?>
        </a>
        <?php endif; ?>
        
        <a href="index.php?controller=oficina&action=listar" class="action-main back-btn">
            <i class="fas fa-arrow-left"></i> <?php echo isset($lang['back_to_list']) ? $lang['back_to_list'] : 'Volver a la Lista'; ?>
        </a>
    </div>
</div>

<!-- Estilos CSS para la vista -->
<style>
/* Estilos generales */
.content-wrapper {
    max-width: 1200px;
    margin: 0 auto;
    padding: 20px;
    font-family: 'Roboto', Arial, sans-serif;
}

/* Encabezado */
.header-container {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 20px;
    border-bottom: 2px solid #0056b3;
    padding-bottom: 10px;
}

.page-title {
    font-size: 24px;
    color:rgb(255, 255, 255);
    margin: 0;
}

.mode-visualizacion {
    background-color: #e9f0f8;
    padding: 5px 10px;
    border-radius: 5px;
    display: flex;
    align-items: center;
    font-size: 14px;
    color: #0056b3;
}

.mode-icon {
    margin-right: 5px;
}

/* Resumen de oficina */
.client-summary {
    display: flex;
    background-color: #f9f9f9;
    border-radius: 8px;
    padding: 15px;
    margin-bottom: 20px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.avatar-box {
    margin-right: 20px;
}

.client-avatar {
    width: 80px;
    height: 80px;
    background-color: #0056b3;
    border-radius: 50%;
    display: flex;
    justify-content: center;
    align-items: center;
    color: white;
    font-size: 36px;
}

.client-info {
    flex: 1;
}

.client-name {
    font-size: 22px;
    margin: 0 0 5px 0;
    color: #333;
}

.client-identification {
    font-size: 16px;
    color: #666;
    margin: 0;
}

/* Contenedor de detalles */
.details-container {
    background-color: white;
    border-radius: 8px;
    padding: 20px;
    margin-bottom: 20px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.details-row {
    display: flex;
    margin-bottom: 15px;
}

.details-cell {
    flex: 1;
    padding-right: 20px;
}

.detail-label {
    display: block;
    font-size: 14px;
    color: #666;
    margin-bottom: 5px;
}

.detail-value {
    font-size: 16px;
    color: #333;
    padding: 5px 0;
}

.highlight-value {
    font-weight: 600;
    color: #0056b3;
}

/* Contenedor de opciones */
.option-row-container {
    display: flex;
    margin-bottom: 20px;
    gap: 20px;
}

.option-section {
    flex: 1;
    background-color: white;
    border-radius: 8px;
    padding: 15px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.option-label {
    display: block;
    font-size: 14px;
    color: #666;
    margin-bottom: 10px;
}

.option-buttons {
    display: flex;
    gap: 10px;
}

.option-button {
    flex: 1;
    padding: 10px;
    border-radius: 5px;
    text-align: center;
    background-color: #f0f0f0;
    color: #666;
    font-size: 14px;
    cursor: default;
}

.option-button.selected {
    background-color: #0056b3;
    color: white;
}

.status-active.selected {
    background-color: #28a745;
}

.status-inactive.selected {
    background-color: #dc3545;
}

/* Sección de empleados */
.employees-section {
    background-color: white;
    border-radius: 8px;
    padding: 20px;
    margin-bottom: 20px;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}

.section-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 15px;
}

.section-title {
    font-size: 18px;
    color:rgb(255, 255, 255);
    margin: 0;
}

.add-button {
    background-color: #28a745;
    color: white;
    padding: 8px 12px;
    border-radius: 4px;
    text-decoration: none;
    font-size: 14px;
    display: inline-flex;
    align-items: center;
}

.add-button i {
    margin-right: 5px;
}

.no-employees {
    text-align: center;
    padding: 30px;
    color: #666;
}

.empty-icon {
    font-size: 48px;
    color: #ccc;
    margin-bottom: 10px;
}

.add-employee-btn {
    display: inline-block;
    background-color: #28a745;
    color: white;
    padding: 8px 15px;
    border-radius: 4px;
    text-decoration: none;
    margin-top: 15px;
}

.table-container {
    overflow-x: auto;
}

.data-table {
    width: 100%;
    border-collapse: collapse;
}

.data-table th {
    background-color: #f0f4f8;
    padding: 10px;
    text-align: left;
    font-weight: 600;
    color: #333;
}

.data-table td {
    padding: 10px;
    border-top: 1px solid #ddd;
}

.employee-name {
    font-weight: 500;
}

.status-pill {
    display: inline-block;
    padding: 4px 8px;
    border-radius: 12px;
    font-size: 12px;
    font-weight: 500;
}

.status-pill.active {
    background-color: #d4edda;
    color: #155724;
}

.status-pill.inactive {
    background-color: #f8d7da;
    color: #721c24;
}

.action-buttons {
    display: flex;
    gap: 5px;
}

.action-btn {
    width: 30px;
    height: 30px;
    border-radius: 4px;
    display: inline-flex;
    justify-content: center;
    align-items: center;
    color: white;
}

.action-btn.view {
    background-color: #17a2b8;
}

.action-btn.edit {
    background-color: #0056b3;
}

/* Botones de acción */
.actions-container {
    display: flex;
    gap: 10px;
    margin-top: 30px;
}

.action-main {
    padding: 10px 20px;
    border-radius: 4px;
    display: inline-flex;
    align-items: center;
    text-decoration: none;
    font-weight: 500;
}

.action-main i {
    margin-right: 8px;
}

.edit-btn {
    background-color: #0056b3;
    color: white;
}

.new-employee-btn {
    background-color: #28a745;
    color: white;
}

.deactivate-btn {
    background-color: #ffc107;
    color: #212529;
}

.activate-btn {
    background-color: #28a745;
    color: white;
}

.back-btn {
    background-color: #6c757d;
    color: white;
}

/* Animaciones */
@keyframes fadeIn {
    from { opacity: 0; transform: translateY(10px); }
    to { opacity: 1; transform: translateY(0); }
}

.details-row, .option-section, .employees-section, .actions-container {
    animation: fadeIn 0.3s ease-in-out forwards;
}

.details-row:nth-child(1) { animation-delay: 0.1s; }
.details-row:nth-child(2) { animation-delay: 0.2s; }
.details-row:nth-child(3) { animation-delay: 0.3s; }
.details-row:nth-child(4) { animation-delay: 0.4s; }
.details-row:nth-child(5) { animation-delay: 0.5s; }
</style>

<!-- JavaScript para animaciones y funcionalidades -->
<script>
document.addEventListener('DOMContentLoaded', function() {
    // Animación para las filas de la tabla
    const tableRows = document.querySelectorAll('.data-table tbody tr');
    tableRows.forEach((row, index) => {
        row.style.opacity = '0';
        row.style.transform = 'translateY(10px)';
        row.style.transition = 'opacity 0.3s ease, transform 0.3s ease';
        row.style.transitionDelay = `${index * 0.05}s`;
        
        setTimeout(() => {
            row.style.opacity = '1';
            row.style.transform = 'translateY(0)';
        }, 100);
    });
    
    // Efecto hover para las filas de la tabla
    tableRows.forEach(row => {
        row.addEventListener('mouseenter', () => {
            row.style.backgroundColor = '#f5f9ff';
        });
        
        row.addEventListener('mouseleave', () => {
            row.style.backgroundColor = '';
        });
    });
});
</script>