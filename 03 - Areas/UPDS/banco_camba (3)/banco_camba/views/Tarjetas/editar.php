<!-- Enlace a la hoja de estilos principal -->
<link rel="stylesheet" href="assets/css/styleEditar.css">
<!-- Estilos adicionales específicos para tarjetas -->
<style>
/* Estilos base para la tarjeta y contenedores */
.client-card {
    border-radius: 8px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
    margin-bottom: 30px;
    background-color: #f8fcf8;
    border: none;
    overflow: hidden;
    transition: all 0.3s ease;
}

.client-card.edit-mode {
    border-left: 4px solid #ffd700;
}

.card-header {
    background-color: #0a6c0a;
    color: white;
    padding: 15px 20px;
    display: flex;
    justify-content: space-between;
    align-items: center;
    border-bottom: 3px solid #ffd700;
}

.title-with-line {
    position: relative;
    margin: 0;
    font-weight: 600;
    font-size: 1.4rem;
    padding-bottom: 5px;
}

.title-with-line:after {
    content: '';
    position: absolute;
    left: 0;
    bottom: 0;
    width: 60px;
    height: 3px;
    background-color: #ffd700;
}

.edit-badge {
    display: flex;
    align-items: center;
    background-color: #ffd700;
    color: #0a6c0a;
    padding: 5px 12px;
    border-radius: 20px;
    font-weight: 600;
    font-size: 0.9rem;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.edit-icon {
    margin-right: 5px;
    font-size: 1.1rem;
}

.card-body {
    padding: 20px;
}

/* Sección de resumen de tarjeta */
.card-info-summary {
    display: flex;
    flex-wrap: wrap;
    margin-bottom: 25px;
    padding: 15px;
    background-color: rgba(10, 108, 10, 0.05);
    border-radius: 8px;
    border-left: 3px solid #0a6c0a;
}

.card-preview {
    flex: 1;
    min-width: 300px;
    max-width: 450px;
    margin-right: 20px;
    margin-bottom: 15px;
}

.card-graphic {
    width: 360px;
    height: 180px;
    background: linear-gradient(135deg, #207cca 0%, #1e5799 100%);
    border-radius: 12px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
    padding: 20px;
    color: white;
    position: relative;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    transition: all 0.3s ease;
}

/* Tipos de tarjeta */
.card-graphic.debito {
    background: linear-gradient(135deg, #207cca 0%, #1e5799 100%);
}

.card-graphic.credito {
    background: linear-gradient(135deg, #8e0e00 0%, #1f1c18 100%);
}

/* Estado de tarjeta */
.card-graphic.blocked {
    filter: grayscale(0.8);
    opacity: 0.7;
}

.card-graphic.blocked::after {
    content: '✕';
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    font-size: 60px;
    color: rgba(255, 0, 0, 0.7);
    z-index: 2;
}

.card-logo {
    position: absolute;
    top: 15px;
    right: 15px;
    width: 70px;
    height: 35px;
    display: flex;
    justify-content: center;
    align-items: center;
    font-weight: bold;
    font-size: 13px;
    color: #0a6c0a;
    background-color: #ffd700;
    border-radius: 4px;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.2);
}

.card-bank-info {
    position: absolute;
    top: 60px;
    right: 15px;
    font-size: 10px;
    color: rgba(255, 255, 255, 0.8);
    text-align: right;
}

.card-chip {
    width: 40px;
    height: 30px;
    background: linear-gradient(to bottom, #bdb76b 0%, #ffd700 100%);
    border-radius: 4px;
    margin-bottom: 10px;
}

.card-number {
    font-family: 'Courier New', monospace;
    font-size: 18px;
    letter-spacing: 2px;
    margin-bottom: 20px;
}

.card-holder {
    font-size: 14px;
    font-family: 'Arial', sans-serif;
    margin-bottom: 10px;
    opacity: 0.9;
    text-transform: uppercase;
}

.card-expire {
    font-size: 14px;
    font-family: 'Courier New', monospace;
}

.card-expire small {
    font-size: 9px;
    opacity: 0.8;
    font-family: 'Arial', sans-serif;
    display: block;
    margin-bottom: 3px;
}

/* Información complementaria */
.card-summary {
    flex: 1;
    min-width: 200px;
    margin-right: 20px;
    margin-bottom: 15px;
}

.card-summary h3 {
    margin: 0 0 5px 0;
    color: #0a6c0a;
    font-size: 1.2rem;
}

.card-status {
    margin: 0 0 10px 0;
    font-size: 0.9rem;
    display: flex;
    align-items: center;
}

.card-status.activa {
    color: #28a745;
}

.card-status.inactiva {
    color: #dc3545;
}

.status-dot {
    display: inline-block;
    width: 10px;
    height: 10px;
    border-radius: 50%;
    margin-right: 6px;
}

.activa .status-dot {
    background-color: #28a745;
}

.inactiva .status-dot {
    background-color: #dc3545;
}

.hash-badge {
    display: inline-block;
    background-color: #f8f9fa;
    color: #6c757d;
    padding: 3px 8px;
    border-radius: 4px;
    font-size: 0.8rem;
    font-family: monospace;
    margin-left: 5px;
    margin-bottom: 10px;
}

/* Acciones rápidas */
.quick-actions {
    display: flex;
    flex-wrap: wrap;
    gap: 10px;
    margin-top: 15px;
}

.action-btn {
    flex: 1;
    min-width: 80px;
    padding: 10px;
    border-radius: 4px;
    text-align: center;
    font-size: 0.9rem;
    background-color: #f2f2f2;
    color: #0a6c0a;
    border: 1px solid #ddd;
    cursor: pointer;
    transition: all 0.2s ease;
}

.action-btn:hover {
    background-color: #e6f7e6;
    transform: translateY(-2px);
}

.action-btn i {
    display: block;
    font-size: 1.2rem;
    margin-bottom: 5px;
}

/* Información complementaria de la tarjeta */
.card-complementary-info {
    flex: 1;
    min-width: 200px;
    display: flex;
    flex-direction: column;
    justify-content: center;
    border-left: 1px dashed #ccc;
    padding-left: 20px;
    margin-bottom: 15px;
}

.info-item {
    display: flex;
    align-items: center;
    margin-bottom: 10px;
}

.info-item i {
    color: #0a6c0a;
    font-size: 16px;
    margin-right: 10px;
    width: 20px;
    text-align: center;
}

.info-title {
    font-weight: 600;
    margin-right: 8px;
    color: #666;
    min-width: 120px;
}

.info-value {
    font-weight: 600;
    color: #0a6c0a;
}

/* Secciones y títulos */
.section-title {
    color: #0a6c0a;
    font-size: 1.2rem;
    font-weight: 600;
    margin: 25px 0 15px 0;
    padding-bottom: 8px;
    border-bottom: 2px solid #e6f7e6;
}

.account-section {
    background-color: #f8fcf8;
    border: 1px solid #ddd;
    border-radius: 6px;
    padding: 15px;
    margin-bottom: 20px;
}

/* Formulario */
.form-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
    gap: 20px;
    margin-bottom: 25px;
}

.form-group {
    margin-bottom: 15px;
    position: relative;
}

.form-label {
    display: block;
    margin-bottom: 5px;
    font-weight: 600;
    color: #0a6c0a;
}

.required {
    color: #e74c3c;
    margin-left: 4px;
}

.form-control {
    display: block;
    width: 100%;
    padding: 10px 12px;
    font-size: 1rem;
    color: #495057;
    background-color: #fff;
    border: 1px solid #ced4da;
    border-radius: 4px;
    transition: all 0.15s ease-in-out;
}

.form-control:focus {
    border-color: #0a6c0a;
    outline: 0;
    box-shadow: 0 0 0 0.2rem rgba(10, 108, 10, 0.25);
}

.form-text {
    display: block;
    margin-top: 5px;
    font-size: 12px;
    color: #6c757d;
}

/* Campos seguros */
.secure-input-wrapper {
    position: relative;
    display: flex;
    align-items: center;
}

.secure-icon {
    position: absolute;
    right: 10px;
    color: #6c757d;
    font-size: 14px;
}

.toggle-password {
    background: none;
    border: none;
    cursor: pointer;
    font-size: 16px;
    padding: 0;
    margin-left: 10px;
}

/* Toggle de estado */
.status-toggle {
    display: flex;
    gap: 15px;
}

.status-toggle input[type="radio"] {
    display: none;
}

.status-option {
    display: flex;
    align-items: center;
    padding: 8px 15px;
    border-radius: 4px;
    cursor: pointer;
    border: 1px solid #ced4da;
    transition: all 0.2s ease;
}

.active-option {
    color: #0a6c0a;
}

.inactive-option {
    color: #dc3545;
}

.status-toggle input[type="radio"]:checked + .active-option {
    background-color: rgba(10, 108, 10, 0.1);
    border-color: #0a6c0a;
}

.status-toggle input[type="radio"]:checked + .inactive-option {
    background-color: rgba(220, 53, 69, 0.1);
    border-color: #dc3545;
}

.status-icon {
    font-size: 16px;
    margin-right: 8px;
}

/* Tooltip */
.tooltip-icon {
    display: inline-block;
    width: 16px;
    height: 16px;
    background-color: #0a6c0a;
    color: white;
    border-radius: 50%;
    text-align: center;
    line-height: 16px;
    font-size: 12px;
    margin-left: 5px;
    cursor: help;
}

.custom-tooltip {
    position: absolute;
    background-color: #333;
    color: #fff;
    padding: 5px 10px;
    border-radius: 4px;
    font-size: 12px;
    z-index: 1000;
    max-width: 200px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
}

.custom-tooltip::before {
    content: '';
    position: absolute;
    top: -5px;
    left: 10px;
    border-width: 0 5px 5px;
    border-style: solid;
    border-color: transparent transparent #333;
}

/* Validación */
.was-validated .form-control:invalid {
    border-color: #dc3545;
}

.invalid-feedback {
    display: none;
    width: 100%;
    margin-top: 0.25rem;
    font-size: 0.875rem;
    color: #dc3545;
}

.was-validated .form-control:invalid ~ .invalid-feedback {
    display: block;
}

/* Historial y seguridad */
.edit-history {
    margin: 25px 0;
    padding: 12px 15px;
    background-color: #f5f5f5;
    border-radius: 6px;
    font-size: 0.9rem;
}

.edit-history h4 {
    margin: 0 0 5px 0;
    color: #0a6c0a;
    font-weight: 600;
    font-size: 1rem;
}

.edit-history p {
    margin: 0;
    color: #666;
}

.security-notice {
    display: flex;
    align-items: flex-start;
    background-color: #e6f7e6;
    border-left: 3px solid #0a6c0a;
    padding: 12px 15px;
    margin: 20px 0;
    border-radius: 6px;
}

.security-icon {
    font-size: 24px;
    margin-right: 12px;
    color: #0a6c0a;
}

.security-text h4 {
    margin: 0 0 5px 0;
    font-size: 14px;
    font-weight: 600;
    color: #0a6c0a;
}

.security-text p {
    margin: 0;
    font-size: 13px;
    color: #333;
}

.additional-security-info {
    display: flex;
    flex-direction: column;
    gap: 15px;
    margin: 20px 0;
    background-color: #f8fcf8;
    border: 1px solid #ddd;
    border-radius: 6px;
    padding: 15px;
}

.additional-security-info h4 {
    margin: 0 0 10px 0;
    color: #0a6c0a;
    font-size: 1rem;
}

/* Botones */
.form-actions {
    display: flex;
    flex-wrap: wrap;
    gap: 12px;
    margin-top: 25px;
    padding-top: 15px;
    border-top: 1px solid #e6e6e6;
}

.btn {
    padding: 10px 20px;
    border-radius: 4px;
    font-weight: 600;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    gap: 8px;
    cursor: pointer;
    transition: all 0.3s ease;
    text-decoration: none;
}

.btn-primary {
    background-color: #0a6c0a;
    color: white;
    border: none;
}

.btn-primary:hover {
    background-color: #095809;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.btn-secondary {
    background-color: #f2f2f2;
    color: #333;
    border: 1px solid #ddd;
}

.btn-secondary:hover {
    background-color: #e6e6e6;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.btn-danger {
    background-color: #dc3545;
    color: white;
    border: none;
}

.btn-danger:hover {
    background-color: #c82333;
    transform: translateY(-2px);
    box-shadow: 0 4px 8px rgba(220, 53, 69, 0.3);
}

.btn-outline {
    background-color: transparent;
    color: #0a6c0a;
    border: 1px solid #0a6c0a;
}

.btn-outline:hover {
    background-color: rgba(10, 108, 10, 0.1);
    transform: translateY(-2px);
}

.history-btn {
    margin-left: auto;
}

/* Animaciones */
@keyframes slideIn {
    from {
        opacity: 0;
        transform: translateY(15px);
    }
    to {
        opacity: 1;
        transform: translateY(0);
    }
}

#form-tarjeta {
    animation: slideIn 0.4s ease forwards;
}

/* Responsive */
@media (max-width: 992px) {
    .card-info-summary {
        flex-direction: column;
    }
    
    .card-preview, .card-summary, .card-complementary-info {
        width: 100%;
        margin-right: 0;
        margin-bottom: 20px;
    }
    
    .card-complementary-info {
        border-left: none;
        border-top: 1px dashed #ccc;
        padding-left: 0;
        padding-top: 15px;
    }
}
</style>

<div class="client-card edit-mode">
    <div class="card-header">
        <h2 class="title-with-line"><?php echo isset($lang['edit_card_data']) ? $lang['edit_card_data'] : 'Editar Datos de la Tarjeta'; ?></h2>
        <div class="edit-badge">
            <span class="edit-icon">💳</span>
            <span class="edit-text"><?php echo isset($lang['editing_card']) ? $lang['editing_card'] : 'Editando Tarjeta'; ?> #16</span>
        </div>
    </div>
    <div class="card-body">
        <!-- Vista previa de la tarjeta -->
        
        <div class="card-info-summary">
            <!-- Tarjeta gráfica -->
            <div class="card-preview">
                <div class="card-graphic debito">
                    <div class="card-logo"><?php echo isset($lang['bank_short']) ? $lang['bank_short'] : 'BANCO M'; ?></div>
                    <div class="card-bank-info"><?php echo isset($lang['bank_full_name']) ? $lang['bank_full_name'] : 'MERCANTIL SANTA CRUZ'; ?></div>
                    <div class="card-chip"></div>
                    <div class="card-number">
                        <span>•••• •••• •••• </span>3470
                    </div>
                    <div class="card-holder"><?php echo isset($lang['authorized_client']) ? $lang['authorized_client'] : 'CLIENTE AUTORIZADO'; ?></div>
                    <div class="card-expire">
                        <small><?php echo isset($lang['valid_until']) ? $lang['valid_until'] : 'VÁLIDO HASTA'; ?></small> 12/39
                    </div>
                </div>
            </div>
            
            <!-- Resumen de la tarjeta -->
            <div class="card-summary">
                <h3><?php echo isset($lang['debit_card']) ? $lang['debit_card'] : 'Tarjeta de Débito'; ?></h3>
                <p class="card-status activa">
                    <span class="status-dot"></span>
                    <?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?>
                </p>
                <div><?php echo isset($lang['security_hash']) ? $lang['security_hash'] : 'Hash de seguridad'; ?>: <span class="hash-badge">a7f9d3e2c...</span></div>
                <div class="quick-actions">
                    <div class="action-btn" id="verify-btn">
                        <i class="fas fa-shield-alt"></i>
                        <?php echo isset($lang['verify']) ? $lang['verify'] : 'Verificar'; ?>
                    </div>
                    <div class="action-btn" id="limit-btn">
                        <i class="fas fa-sliders-h"></i>
                        <?php echo isset($lang['limits']) ? $lang['limits'] : 'Límites'; ?>
                    </div>
                    <div class="action-btn" id="block-btn">
                        <i class="fas fa-ban"></i>
                        <?php echo isset($lang['block']) ? $lang['block'] : 'Bloquear'; ?>
                    </div>
                </div>
            </div>
            
            <!-- Información complementaria -->
            <div class="card-complementary-info">
                <div class="info-item">
                    <i class="fas fa-calendar-check"></i>
                    <span class="info-title"><?php echo isset($lang['issue_date']) ? $lang['issue_date'] : 'Fecha de emisión'; ?>:</span>
                    <span class="info-value">05/03/2023</span>
                </div>
                <div class="info-item">
                    <i class="fas fa-university"></i>
                    <span class="info-title"><?php echo isset($lang['branch']) ? $lang['branch'] : 'Sucursal'; ?>:</span>
                    <span class="info-value"><?php echo isset($lang['head_office']) ? $lang['head_office'] : 'Casa Matriz'; ?></span>
                </div>
                <div class="info-item">
                    <i class="fas fa-globe"></i>
                    <span class="info-title"><?php echo isset($lang['international_use']) ? $lang['international_use'] : 'Uso internacional'; ?>:</span>
                    <span class="info-value"><?php echo isset($lang['enabled']) ? $lang['enabled'] : 'Habilitado'; ?></span>
                </div>
                <div class="info-item">
                    <i class="fas fa-lock"></i>
                    <span class="info-title"><?php echo isset($lang['security']) ? $lang['security'] : 'Seguridad'; ?>:</span>
                    <span class="info-value"><?php echo isset($lang['pci_dss_standard']) ? $lang['pci_dss_standard'] : 'Estándar PCI DSS'; ?></span>
                </div>
            </div>
        </div>
        
        <!-- Formulario de edición -->
        <form id="form-tarjeta" method="POST" action="index.php?controller=tarjeta&action=actualizar" class="needs-validation" novalidate>
            <!-- Campos ocultos -->
            <input type="hidden" name="idTarjeta" value="16">
            <input type="hidden" name="hash" value="a7f9d3e2c8b1f5e6d4c2b9a3f7e1d5c8">
            
            <!-- Sección: Información de cuenta -->
            <h3 class="section-title"><?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de la Cuenta'; ?></h3>
            
            <div class="account-section">
                <div class="form-group">
                    <label for="idCuenta" class="form-label"><?php echo isset($lang['associated_account']) ? $lang['associated_account'] : 'Cuenta Asociada'; ?><span class="required">*</span></label>
                    <select id="idCuenta" name="idCuenta" class="form-control" required>
                        <option value=""><?php echo isset($lang['select_account']) ? $lang['select_account'] : 'Seleccione una cuenta'; ?></option>
                        <option value="1" selected>Juan Pérez - 1234567904 ($)</option>
                        <option value="2">María García - 0987654321 (Bs.)</option>
                        <option value="3">Carlos Rodríguez - 5678901234 (Bs.)</option>
                    </select>
                    <div class="invalid-feedback">
                        <?php echo isset($lang['please_select_account']) ? $lang['please_select_account'] : 'Por favor seleccione una cuenta asociada.'; ?>
                    </div>
                    <small class="form-text text-muted"><?php echo isset($lang['account_card_association_info']) ? $lang['account_card_association_info'] : 'Esta cuenta será asociada a la tarjeta para transacciones'; ?></small>
                </div>
            </div>
            
            <!-- Sección: Información de la tarjeta -->
            <h3 class="section-title"><?php echo isset($lang['card_information']) ? $lang['card_information'] : 'Información de la Tarjeta'; ?></h3>
            
            <div class="form-grid">
                <!-- Columna izquierda -->
                <div class="form-column">
                    <!-- Tipo de Tarjeta -->
                    <div class="form-group">
                        <label for="tipoTarjeta" class="form-label"><?php echo isset($lang['card_type']) ? $lang['card_type'] : 'Tipo de Tarjeta'; ?><span class="required">*</span></label>
                        <select id="tipoTarjeta" name="tipoTarjeta" class="form-control" required>
                            <option value=""><?php echo isset($lang['select_option']) ? $lang['select_option'] : 'Seleccione una opción'; ?></option>
                            <option value="debito" selected><?php echo isset($lang['debit']) ? $lang['debit'] : 'Débito'; ?></option>
                            <option value="credito"><?php echo isset($lang['credit']) ? $lang['credit'] : 'Crédito'; ?></option>
                        </select>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['please_select_card_type']) ? $lang['please_select_card_type'] : 'Por favor seleccione el tipo de tarjeta.'; ?>
                        </div>
                    </div>
                    
                    <!-- Número de Tarjeta -->
                    <div class="form-group">
                        <label for="nroTarjeta" class="form-label"><?php echo isset($lang['card_number']) ? $lang['card_number'] : 'Número de Tarjeta'; ?><span class="required">*</span></label>
                        <input type="text" id="nroTarjeta" name="nroTarjeta" class="form-control" value="4532 7854 9632 3470" required placeholder="XXXX XXXX XXXX XXXX">
                        <div class="invalid-feedback">
                            <?php echo isset($lang['please_enter_valid_card_number']) ? $lang['please_enter_valid_card_number'] : 'Por favor ingrese un número de tarjeta válido.'; ?>
                        </div>
                        <small class="form-text text-muted"><?php echo isset($lang['card_number_format_help']) ? $lang['card_number_format_help'] : 'Ingrese los 16 dígitos de la tarjeta separados en grupos de 4'; ?></small>
                    </div>
                    
                    <!-- CVV -->
                    <div class="form-group">
                        <label for="cvv" class="form-label">
                            CVV<span class="required">*</span>
                            <span class="tooltip-icon" title="<?php echo isset($lang['cvv_tooltip']) ? $lang['cvv_tooltip'] : 'Código de seguridad de 3 dígitos ubicado en el reverso de la tarjeta'; ?>">?</span>
                        </label>
                        <div class="secure-input-wrapper">
                            <input type="text" id="cvv" name="cvv" class="form-control" value="123" maxlength="3" required placeholder="123">
                            <span class="secure-icon" title="<?php echo isset($lang['secure_field']) ? $lang['secure_field'] : 'Campo seguro'; ?>">🔒</span>
                        </div>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['cvv_validation']) ? $lang['cvv_validation'] : 'El CVV debe tener 3 dígitos.'; ?>
                        </div>
                        <small class="form-text text-muted"><?php echo isset($lang['cvv_help']) ? $lang['cvv_help'] : 'Código de seguridad de 3 dígitos'; ?></small>
                    </div>
                </div>
                
                <!-- Columna derecha -->
                <div class="form-column">
                    <!-- Fecha de Expiración -->
                    <div class="form-group">
                        <label for="fechaExpiracion" class="form-label"><?php echo isset($lang['expiration_date']) ? $lang['expiration_date'] : 'Fecha de Expiración'; ?><span class="required">*</span></label>
                        <input type="text" id="fechaExpiracion" name="fechaExpiracion" class="form-control" value="12/39" maxlength="7" required placeholder="MM/YY">
                        <div class="invalid-feedback">
                            <?php echo isset($lang['expiration_date_validation']) ? $lang['expiration_date_validation'] : 'Por favor ingrese una fecha válida (MM/YY).'; ?>
                        </div>
                        <small class="form-text text-muted"><?php echo isset($lang['expiration_date_format']) ? $lang['expiration_date_format'] : 'Formato: MM/YY (ejemplo: 12/25)'; ?></small>
                    </div>
                    
                    <!-- PIN -->
                    <div class="form-group">
                        <label for="pin" class="form-label">
                            PIN<span class="required">*</span>
                            <span class="tooltip-icon" title="<?php echo isset($lang['pin_tooltip']) ? $lang['pin_tooltip'] : 'Código secreto de 4 dígitos para usar en cajeros y comercios'; ?>">?</span>
                        </label>
                        <div class="secure-input-wrapper">
                            <input type="password" id="pin" name="pin" class="form-control" value="1234" maxlength="4" required placeholder="****">
                            <span class="secure-icon" title="<?php echo isset($lang['secure_field']) ? $lang['secure_field'] : 'Campo seguro'; ?>">🔒</span>
                            <button type="button" class="toggle-password" title="<?php echo isset($lang['show_hide_pin']) ? $lang['show_hide_pin'] : 'Mostrar/Ocultar PIN'; ?>">👁️</button>
                        </div>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['pin_validation']) ? $lang['pin_validation'] : 'El PIN debe tener 4 dígitos.'; ?>
                        </div>
                        <small class="form-text text-muted"><?php echo isset($lang['pin_help']) ? $lang['pin_help'] : 'Código numérico de 4 dígitos'; ?></small>
                    </div>
                    
                    <!-- Estado -->
                    <div class="form-group">
                        <label for="estado" class="form-label"><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?><span class="required">*</span></label>
                        <div class="status-toggle">
                            <input type="radio" id="estado-activa" name="estado" value="activa" checked required>
                            <label for="estado-activa" class="status-option active-option">
                                <span class="status-icon">✓</span>
                                <span class="status-text"><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></span>
                            </label>
                            
                            <input type="radio" id="estado-inactiva" name="estado" value="inactiva" required>
                            <label for="estado-inactiva" class="status-option inactive-option">
                                <span class="status-icon">✕</span>
                                <span class="status-text"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></span>
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            
            <!-- Información de seguridad -->
            <div class="additional-security-info">
                <h4><?php echo isset($lang['security_measures']) ? $lang['security_measures'] : 'Medidas de Seguridad'; ?></h4>
            </div>
            
            <!-- Historial de cambios -->
            <div class="edit-history">
                <h4><?php echo isset($lang['last_modification']) ? $lang['last_modification'] : 'Última Modificación'; ?></h4>
                <p>09/03/2025 06:56:49</p>
            </div>
            
            <!-- Aviso de seguridad -->
            <div class="security-notice">
                <div class="security-icon">🛡️</div>
                <div class="security-text">
                    <h4><?php echo isset($lang['security_notice']) ? $lang['security_notice'] : 'Aviso de Seguridad'; ?></h4>
                    <p><?php echo isset($lang['security_notice_text']) ? $lang['security_notice_text'] : 'Todo cambio en la información de la tarjeta quedará registrado y podría requerir verificación adicional. El cambio del PIN o bloqueo de la tarjeta se aplicará inmediatamente.'; ?></p>
                </div>
            </div>
            
            <!-- Botones de acción -->
            <div class="form-actions">
                <button type="submit" class="btn btn-primary">
                    <i class="fas fa-save"></i> <?php echo isset($lang['save_changes']) ? $lang['save_changes'] : 'Guardar Cambios'; ?>
                </button>
                <a href="index.php?controller=tarjeta&action=ver&id=16" class="btn btn-secondary">
                    <i class="fas fa-times"></i> <?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?>
                </a>
                <button type="button" class="btn btn-danger" id="block-card-btn">
                    <i class="fas fa-ban"></i> <?php echo isset($lang['block_card']) ? $lang['block_card'] : 'Bloquear Tarjeta'; ?>
                </button>
                <a href="index.php?controller=tarjeta&action=historial&id=16" class="btn btn-outline history-btn">
                    <i class="fas fa-history"></i> <?php echo isset($lang['view_history']) ? $lang['view_history'] : 'Ver Historial'; ?>
                </a>
            </div>
        </form>
    </div>
</div>

<script>
// Validación del formulario con JavaScript
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('form-tarjeta');
    
    // Formateo y validación del número de tarjeta
    const nroTarjetaInput = document.getElementById('nroTarjeta');
    nroTarjetaInput.addEventListener('input', function() {
        let value = this.value.replace(/\D/g, '');
        if (value.length > 16) {
            value = value.substring(0, 16);
        }
        
        // Formato con espacios cada 4 dígitos
        let formattedValue = '';
        for (let i = 0; i < value.length; i++) {
            if (i > 0 && i % 4 === 0) {
                formattedValue += ' ';
            }
            formattedValue += value[i];
        }
        
        this.value = formattedValue;
        
        // Actualizar vista previa
        updateCardPreview();
    });
    
    // Validar CVV (3 dígitos)
    const cvvInput = document.getElementById('cvv');
    cvvInput.addEventListener('input', function() {
        this.value = this.value.replace(/[^0-9]/g, '').substring(0, 3);
    });
    
    // Formateo de fecha de expiración (MM/YY)
    const fechaExpInput = document.getElementById('fechaExpiracion');
    fechaExpInput.addEventListener('input', function() {
        let value = this.value.replace(/[^0-9]/g, '');
        
        if (value.length > 0) {
            // Validar mes (01-12)
            let month = parseInt(value.substring(0, 2));
            if (month > 12) {
                value = '12' + value.substring(2);
            } else if (month === 0 && value.length >= 2) {
                value = '01' + value.substring(2);
            }
        }
        
        if (value.length > 4) {
            value = value.substring(0, 4);
        }
        
        // Formato MM/YY
        if (value.length > 2) {
            this.value = value.substring(0, 2) + '/' + value.substring(2);
        } else {
            this.value = value;
        }
        
        // Actualizar vista previa
        updateCardPreview();
    });
    
    // Validar PIN (4 dígitos)
    const pinInput = document.getElementById('pin');
    pinInput.addEventListener('input', function() {
        this.value = this.value.replace(/[^0-9]/g, '').substring(0, 4);
    });
    
    // Cambiar tipo de tarjeta
    const tipoTarjetaSelect = document.getElementById('tipoTarjeta');
    tipoTarjetaSelect.addEventListener('change', function() {
        updateCardPreview();
    });
    
    // Cambiar estado de la tarjeta
    const estadoActiva = document.getElementById('estado-activa');
    const estadoInactiva = document.getElementById('estado-inactiva');
    
    estadoActiva.addEventListener('change', function() {
        if (this.checked) {
            document.querySelector('.card-graphic').classList.remove('blocked');
            document.querySelector('.card-status').className = 'card-status activa';
            document.querySelector('.card-status').innerHTML = '<span class="status-dot"></span>Activa';
        }
    });
    
    estadoInactiva.addEventListener('change', function() {
        if (this.checked) {
            document.querySelector('.card-graphic').classList.add('blocked');
            document.querySelector('.card-status').className = 'card-status inactiva';
            document.querySelector('.card-status').innerHTML = '<span class="status-dot"></span>Inactiva';
        }
    });
    
    // Actualizar vista previa de la tarjeta
    function updateCardPreview() {
        const cardPreview = document.querySelector('.card-graphic');
        const lastFour = document.querySelector('.card-number').lastElementChild;
        const expireDate = document.querySelector('.card-expire').lastChild;
        const cardTitle = document.querySelector('.card-summary h3');
        
        // Actualizar tipo de tarjeta
        cardPreview.className = 'card-graphic ' + tipoTarjetaSelect.value;
        
        // Añadir clase blocked si la tarjeta está inactiva
        if (document.getElementById('estado-inactiva').checked) {
            cardPreview.classList.add('blocked');
        }
        
        // Actualizar título según tipo de tarjeta
        cardTitle.textContent = tipoTarjetaSelect.value === 'debito' ? 
            'Tarjeta de Débito' : 
            'Tarjeta de Crédito';
        
        // Actualizar últimos 4 dígitos
        if (nroTarjetaInput.value.length >= 4) {
            const last4 = nroTarjetaInput.value.replace(/\s/g, '').slice(-4);
            lastFour.textContent = last4;
        }
        
        // Actualizar fecha de expiración
        if (fechaExpInput.value) {
            expireDate.textContent = fechaExpInput.value;
        }
    }
    
    // Mostrar/ocultar PIN
    const togglePasswordBtn = document.querySelector('.toggle-password');
    togglePasswordBtn.addEventListener('click', function() {
        const input = this.previousElementSibling.previousElementSibling;
        
        if (input.type === 'password') {
            input.type = 'text';
            this.textContent = '👁️‍🗨️';
            this.title = 'Ocultar PIN';
        } else {
            input.type = 'password';
            this.textContent = '👁️';
            this.title = 'Mostrar PIN';
        }
    });
    
    // Botón de bloqueo rápido
    document.getElementById('block-btn').addEventListener('click', function() {
        document.getElementById('estado-inactiva').checked = true;
        document.querySelector('.card-status').className = 'card-status inactiva';
        document.querySelector('.card-status').innerHTML = '<span class="status-dot"></span>Inactiva';
        document.querySelector('.card-graphic').classList.add('blocked');
    });
    
    // Bloquear tarjeta con botón principal
    document.getElementById('block-card-btn').addEventListener('click', function(e) {
        e.preventDefault();
        
        if (confirm('¿Está seguro que desea bloquear esta tarjeta? Esta acción no se puede deshacer fácilmente.')) {
            document.getElementById('estado-inactiva').checked = true;
            document.querySelector('.card-graphic').classList.add('blocked');
            document.querySelector('.card-status').className = 'card-status inactiva';
            document.querySelector('.card-status').innerHTML = '<span class="status-dot"></span>Inactiva';
        }
    });
    
    // Verificación y límites (simulados)
    document.getElementById('verify-btn').addEventListener('click', function() {
        alert('Iniciando proceso de verificación de la tarjeta. Por favor espere...');
    });
    
    document.getElementById('limit-btn').addEventListener('click', function() {
        alert('Configuración de límites de la tarjeta. Aquí podrá establecer límites diarios de retiro y compras.');
    });
    
    // Validación del formulario antes de enviar
    form.addEventListener('submit', function (event) {
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        
        // Validación adicional para el PIN
        if (pinInput.value.length !== 4) {
            pinInput.setCustomValidity('El PIN debe tener exactamente 4 dígitos.');
            event.preventDefault();
        } else {
            pinInput.setCustomValidity('');
        }
        
        // Validación adicional para el CVV
        if (cvvInput.value.length !== 3) {
            cvvInput.setCustomValidity('El CVV debe tener exactamente 3 dígitos.');
            event.preventDefault();
        } else {
            cvvInput.setCustomValidity('');
        }
        
        // Validación adicional para fecha de expiración
        const fechaExp = fechaExpInput.value;
        if (!/^\d{2}\/\d{2}$/.test(fechaExp)) {
            fechaExpInput.setCustomValidity('El formato debe ser MM/YY.');
            event.preventDefault();
        } else {
            const [month, year] = fechaExp.split('/');
            const currentYear = new Date().getFullYear() % 100;
            const currentMonth = new Date().getMonth() + 1;
            
            if (parseInt(year) < currentYear || (parseInt(year) === currentYear && parseInt(month) < currentMonth)) {
                fechaExpInput.setCustomValidity('La tarjeta ha expirado.');
                event.preventDefault();
            } else {
                fechaExpInput.setCustomValidity('');
            }
        }
        
        form.classList.add('was-validated');
    });
    
    // Mejorar experiencia con cambio de estilos al foco
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(input => {
        // Añadir clase al recibir foco
        input.addEventListener('focus', () => {
            input.parentElement.classList.add('field-focus');
            
            // Resaltar tarjeta al editar campo relacionado
            if (input.id === 'tipoTarjeta' || input.id === 'nroTarjeta' || input.id === 'fechaExpiracion') {
                document.querySelector('.card-graphic').classList.add('highlight');
            }
        });
        
        // Quitar clase al perder foco
        input.addEventListener('blur', () => {
            input.parentElement.classList.remove('field-focus');
            document.querySelector('.card-graphic').classList.remove('highlight');
            
            // Marcar campos modificados
            if (input.value !== input.defaultValue) {
                input.classList.add('field-modified');
            } else {
                input.classList.remove('field-modified');
            }
        });
    });
    
    // Alerta al salir sin guardar cambios
    let formModified = false;
    
    inputs.forEach(input => {
        input.addEventListener('change', () => {
            formModified = true;
        });
    });
    
    window.addEventListener('beforeunload', function(e) {
        if (formModified) {
            e.preventDefault();
            e.returnValue = '';
        }
    });
    
    // Desactivar advertencia al enviar o cancelar
    form.addEventListener('submit', () => {
        formModified = false;
    });
    
    document.querySelector('.btn-secondary').addEventListener('click', () => {
        formModified = false;
    });
    
    // Tooltips personalizados
    const tooltips = document.querySelectorAll('.tooltip-icon');
    tooltips.forEach(tooltip => {
        tooltip.addEventListener('mouseenter', function() {
            const title = this.getAttribute('title');
            if (!title) return;
            
            // Crear tooltip
            const tooltipEl = document.createElement('div');
            tooltipEl.className = 'custom-tooltip';
            tooltipEl.textContent = title;
            
            // Posicionar tooltip
            document.body.appendChild(tooltipEl);
            const rect = this.getBoundingClientRect();
            tooltipEl.style.left = rect.left + 'px';
            tooltipEl.style.top = (rect.bottom + 5) + 'px';
            
            // Almacenar título original y removerlo para evitar tooltip nativo
            this.setAttribute('data-title', title);
            this.removeAttribute('title');
            
            // Almacenar referencia al tooltip
            this.tooltip = tooltipEl;
        });
        
        tooltip.addEventListener('mouseleave', function() {
            if (this.tooltip) {
                this.tooltip.remove();
                this.setAttribute('title', this.getAttribute('data-title'));
                this.removeAttribute('data-title');
                this.tooltip = null;
            }
        });
    });
    
    // Inicializar vista previa
    updateCardPreview();
});
</script>