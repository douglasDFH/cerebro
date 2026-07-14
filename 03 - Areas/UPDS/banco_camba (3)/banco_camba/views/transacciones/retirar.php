<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro de Fondos'; ?> - <?php echo isset($lang['bank_name']) ? $lang['bank_name'] : 'Banco Mercantil'; ?></title>
    <!-- Enlace a la hoja de estilos principal -->
    <link rel="stylesheet" href="assets/css/StyleRetirar.css">
    <!-- Bootstrap CSS -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.1.1/css/all.min.css" rel="stylesheet">
    <!-- Animate.css -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" rel="stylesheet">
    
</head>
<body>
    <!-- Page header -->
    <div class="page-header">
        <h2><i class="fas fa-money-bill-wave"></i> <?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro de Fondos'; ?></h2>
    </div>
    
    <?php
    // Determine which view to show based on if cuenta is set
    $showSearchForm = !isset($cuenta);
    ?>
    
    <!-- Alert information (only show in search view) -->
    <?php if ($showSearchForm): ?>
    <div class="alert-box alert-info">
        <i class="fas fa-info-circle"></i> 
        <?php echo isset($lang['enter_account_number_to_withdraw']) ? $lang['enter_account_number_to_withdraw'] : 'Ingrese el número de cuenta del cual desea realizar el retiro.'; ?>
    </div>
    <?php endif; ?>
    
    <!-- Form container -->
    <div class="form-container">
        <div class="form-section">
            <?php if ($showSearchForm): ?>
            <!-- Formulario para buscar cuenta -->
            <form method="POST" action="index.php?controller=transaccion&action=buscarCuentaRetiro" id="searchForm">
                <div class="input-group">
                    <label for="nroCuenta" class="form-label">
                        <?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?> 
                        <span class="required">*</span>
                    </label>
                    <i class="fas fa-university input-icon"></i>
                    <input type="text" id="nroCuenta" name="nroCuenta" class="form-control" 
                           required placeholder="<?php echo isset($lang['enter_account_number']) ? $lang['enter_account_number'] : 'Ingrese el número de cuenta'; ?>" autofocus>
                    <div class="scan-animation"></div>
                </div>
                
                <div class="form-actions">
                    <button type="submit" class="btn btn-primary" id="searchButton">
                        <i class="fas fa-search"></i> 
                        <?php echo isset($lang['search']) ? $lang['search'] : 'Buscar'; ?>
                    </button>
                    <a href="index.php?controller=transaccion&action=listar" class="btn btn-secondary">
                        <i class="fas fa-times"></i> 
                        <?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?>
                    </a>
                </div>
            </form>
            <?php else: ?>
            <!-- Formulario para realizar retiro -->
            <div class="row mb-4">
                <div class="col-md-6">
                    <div class="account-info-card">
                        <h4 class="info-card-title">
                            <i class="fas fa-university"></i> 
                            <?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de la Cuenta'; ?>
                        </h4>
                        <table class="table">
                            <tr>
                                <th width="40%"><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?>:</th>
                                <td><?php echo $cuenta->nroCuenta; ?></td>
                            </tr>
                            <tr>
                                <th><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?>:</th>
                                <td>
                                    <?php 
                                    if (isset($lang['savings_account']) && isset($lang['checking_account'])) {
                                        echo $cuenta->tipoCuenta == 1 ? $lang['savings_account'] : $lang['checking_account'];
                                    } else {
                                        echo $cuenta->tipoCuenta == 1 ? 'Cuenta de Ahorros' : 'Cuenta Corriente';
                                    }
                                    ?>
                                </td>
                            </tr>
                            <tr>
                                <th><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?>:</th>
                                <td>
                                    <?php 
                                    if (isset($lang['bolivianos']) && isset($lang['dollars'])) {
                                        echo $cuenta->tipoMoneda == 1 ? $lang['bolivianos'] : $lang['dollars'];
                                    } else {
                                        echo $cuenta->tipoMoneda == 1 ? 'Bolivianos' : 'Dólares';
                                    }
                                    ?>
                                </td>
                            </tr>
                            <tr>
                                <th><?php echo isset($lang['available_balance']) ? $lang['available_balance'] : 'Saldo Disponible'; ?>:</th>
                                <td class="balance-text">
                                    <?php 
                                    if ($cuenta->tipoMoneda == 1) {
                                        echo 'Bs. ' . number_format($cuenta->saldo, 2);
                                    } else {
                                        echo '$ ' . number_format($cuenta->saldo, 2);
                                    }
                                    ?>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div class="col-md-6">
                    <div class="client-info-card">
                        <h4 class="info-card-title">
                            <i class="fas fa-user"></i> 
                            <?php echo isset($lang['client_information']) ? $lang['client_information'] : 'Información del Cliente'; ?>
                        </h4>
                        <table class="table">
                            <tr>
                                <th width="40%"><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?>:</th>
                                <td><?php echo $cliente->nombre; ?></td>
                            </tr>
                            <tr>
                                <th><?php echo isset($lang['last_name']) ? $lang['last_name'] : 'Apellido'; ?>:</th>
                                <td><?php echo $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno; ?></td>
                            </tr>
                            <tr>
                                <th><?php echo isset($lang['id_number']) ? $lang['id_number'] : 'Número de Identificación'; ?>:</th>
                                <td><?php echo $cliente->ci; ?></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            
            <form method="POST" action="index.php?controller=transaccion&action=procesarRetiro" class="needs-validation" novalidate id="withdrawForm">
                <input type="hidden" name="idCuenta" value="<?php echo $cuenta->idCuenta; ?>">
                
                <div class="form-group mb-4">
                    <label for="monto" class="form-label">
                        <?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?> <span class="required">*</span>
                    </label>
                    <div class="input-group">
                        <div class="input-group-prepend">
                            <span class="input-group-text"><?php echo $cuenta->tipoMoneda == 1 ? 'Bs.' : '$'; ?></span>
                        </div>
                        <input type="number" id="monto" name="monto" class="form-control" step="0.01" min="0.01" max="<?php echo $cuenta->saldo; ?>" required>
                        <div class="invalid-feedback">
                            <?php echo isset($lang['amount_required']) ? $lang['amount_required'] : 'Por favor ingrese un monto válido.'; ?>
                        </div>
                    </div>
                    <small class="form-text text-muted">
                        <?php 
                        $maxText = isset($lang['max_amount']) ? $lang['max_amount'] : 'Monto máximo';
                        echo $maxText . ': ' . ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') . number_format($cuenta->saldo, 2); 
                        ?>
                    </small>
                </div>
                
                <div class="form-group mb-4">
                    <label for="descripcion" class="form-label">
                        <?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?>
                    </label>
                    <textarea id="descripcion" name="descripcion" class="form-control" rows="3"><?php echo isset($lang['cash_withdrawal']) ? $lang['cash_withdrawal'] : 'Retiro de efectivo'; ?></textarea>
                </div>
                
                <div class="form-actions">
                    <button type="submit" class="btn btn-warning">
                        <i class="fas fa-money-bill-wave"></i> 
                        <?php echo isset($lang['withdraw']) ? $lang['withdraw'] : 'Retirar'; ?>
                    </button>
                    <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta->idCuenta; ?>" class="btn btn-secondary">
                        <i class="fas fa-times"></i> 
                        <?php echo isset($lang['cancel']) ? $lang['cancel'] : 'Cancelar'; ?>
                    </a>
                </div>
            </form>
            <?php endif; ?>
        </div>
    </div>
    
    <?php if ($showSearchForm): ?>
    <!-- Dynamic help cards (only in search view) -->
    <div class="help-cards">
        <div class="help-card" onclick="showHelp('account')">
            <div class="help-card-icon"><i class="fas fa-id-card"></i></div>
            <div class="help-card-title"><?php echo isset($lang['where_find_account_number']) ? $lang['where_find_account_number'] : '¿Dónde encontrar mi número de cuenta?'; ?></div>
            <div class="help-card-text"><?php echo isset($lang['account_number_locations']) ? $lang['account_number_locations'] : 'Su número aparece en la libreta, tarjeta de débito o app móvil'; ?></div>
        </div>
        
        <div class="help-card" onclick="showHelp('balance')">
            <div class="help-card-icon"><i class="fas fa-coins"></i></div>
            <div class="help-card-title"><?php echo isset($lang['verify_available_balance']) ? $lang['verify_available_balance'] : 'Verificar saldo disponible'; ?></div>
            <div class="help-card-text"><?php echo isset($lang['ensure_sufficient_funds']) ? $lang['ensure_sufficient_funds'] : 'Asegúrese de tener fondos suficientes antes de retirar'; ?></div>
        </div>
        
        <div class="help-card" onclick="showHelp('authorization')">
            <div class="help-card-icon"><i class="fas fa-user-check"></i></div>
            <div class="help-card-title"><?php echo isset($lang['account_authorization']) ? $lang['account_authorization'] : 'Autorización de cuentas'; ?></div>
            <div class="help-card-text"><?php echo isset($lang['account_authorization_info']) ? $lang['account_authorization_info'] : 'Solo puede retirar de cuentas donde es titular o tiene autorización'; ?></div>
        </div>
        
        <div class="help-card" onclick="showHelp('limits')">
            <div class="help-card-icon"><i class="fas fa-chart-line"></i></div>
            <div class="help-card-title"><?php echo isset($lang['withdrawal_limits']) ? $lang['withdrawal_limits'] : 'Límites de retiro'; ?></div>
            <div class="help-card-text"><?php echo isset($lang['max_amount_depends']) ? $lang['max_amount_depends'] : 'El monto máximo depende del tipo de cuenta y saldo disponible'; ?></div>
        </div>
    </div>
    
    <!-- Example accounts section (only in search view) -->
    <div class="example-accounts">
        <div class="accounts-title">
            <i class="fas fa-credit-card"></i> <?php echo isset($lang['example_accounts']) ? $lang['example_accounts'] : 'Ejemplos de Cuentas'; ?>
        </div>
        <div class="account-cards">
            <div class="account-card" onclick="fillAccountNumber('1234567890')">
                <div class="account-card-chip"></div>
                <div class="account-card-type"><?php echo isset($lang['savings']) ? $lang['savings'] : 'Ahorros'; ?></div>
                <div class="account-card-number">1234 5678 90</div>
                <div class="account-card-name"><?php echo isset($lang['personal_account']) ? $lang['personal_account'] : 'Cuenta Personal'; ?></div>
            </div>
            
            <div class="account-card" onclick="fillAccountNumber('0987654321')">
                <div class="account-card-chip"></div>
                <div class="account-card-type"><?php echo isset($lang['checking']) ? $lang['checking'] : 'Corriente'; ?></div>
                <div class="account-card-number">0987 6543 21</div>
                <div class="account-card-name"><?php echo isset($lang['business_account']) ? $lang['business_account'] : 'Cuenta Empresarial'; ?></div>
            </div>
            
            <div class="account-card" onclick="fillAccountNumber('5678901234')">
                <div class="account-card-chip"></div>
                <div class="account-card-type"><?php echo isset($lang['payroll']) ? $lang['payroll'] : 'Nómina'; ?></div>
                <div class="account-card-number">5678 9012 34</div>
                <div class="account-card-name"><?php echo isset($lang['salary_account']) ? $lang['salary_account'] : 'Cuenta Salario'; ?></div>
            </div>
        </div>
    </div>
    <?php endif; ?>
    
    <!-- JavaScript para la funcionalidad -->
    <script>
    function fillAccountNumber(number) {
        document.getElementById('nroCuenta').value = number;
    }
    
    function showHelp(type) {
        // Implementar función para mostrar ayuda según el tipo
        console.log("Mostrar ayuda para: " + type);
        // Aquí se podría mostrar un modal o expandir información
    }
    </script>
    
    <!-- Bootstrap Bundle with Popper -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
</body>
</html>

    <!-- Bootstrap JS Bundle with Popper -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap/5.1.3/js/bootstrap.bundle.min.js"></script>
    
    <script>
/* ---------- MANEJO DEL FORMULARIO DE BÚSQUEDA DE CUENTA ---------- */
document.addEventListener('DOMContentLoaded', function () {
    // Manejo del formulario de búsqueda de cuenta
    const searchForm = document.getElementById('searchForm');
    const accountInput = document.getElementById('nroCuenta');
    
    if (searchForm && accountInput) {
        // Enfocar el campo de entrada de número de cuenta
        accountInput.focus();
        
        // Formatear el número de cuenta (solo números)
        accountInput.addEventListener('input', function(e) {
            // Eliminar caracteres no numéricos
            let value = this.value.replace(/\D/g, '');
            
            // Limitar a 10 dígitos
            if (value.length > 10) {
                value = value.slice(0, 10);
            }
            
            this.value = value;
            
            // Mostrar animación de escaneo al escribir
            const scanAnimation = document.querySelector('.scan-animation');
            if (scanAnimation) {
                if (value.length > 0) {
                    scanAnimation.style.opacity = '1';
                } else {
                    scanAnimation.style.opacity = '0';
                }
            }
        });
        
        // Validación del formulario
        searchForm.addEventListener('submit', function(event) {
            const accountNumber = accountInput.value.replace(/\s/g, '');
            
            if (accountNumber.length !== 10 || !/^\d+$/.test(accountNumber)) {
                event.preventDefault();
                
                // Agregar animación de sacudida para entrada inválida
                accountInput.classList.add('shake');
                setTimeout(() => {
                    accountInput.classList.remove('shake');
                }, 500);
                
                showToast('Por favor ingrese un número de cuenta válido de 10 dígitos.');
                accountInput.focus();
            } else {
                // Mostrar indicador de carga
                showLoadingIndicator();
            }
        });
    }

/* ---------- MANEJO DEL FORMULARIO DE RETIRO ---------- */
    const withdrawForm = document.getElementById('withdrawForm');
    const montoInput = document.getElementById('monto');
    
    if (withdrawForm && montoInput) {
        // Validación del formulario
        withdrawForm.addEventListener('submit', function(event) {
            if (!this.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
                
                // Agregar animación de sacudida para entrada inválida
                if (!montoInput.checkValidity()) {
                    montoInput.classList.add('shake');
                    setTimeout(() => {
                        montoInput.classList.remove('shake');
                    }, 500);
                    
                    showToast('Por favor ingrese un monto válido.');
                }
            } else {
                // Verificar si el monto excede el saldo
                const monto = parseFloat(montoInput.value);
                const saldo = parseFloat(montoInput.getAttribute('max'));
                
                if (monto > saldo) {
                    event.preventDefault();
                    montoInput.classList.add('shake');
                    setTimeout(() => {
                        montoInput.classList.remove('shake');
                    }, 500);
                    
                    showToast('El monto excede el saldo disponible.');
                    montoInput.focus();
                } else {
                    // Mostrar indicador de carga
                    showLoadingIndicator('Procesando retiro...');
                }
            }
            
            withdrawForm.classList.add('was-validated');
        });
        
        // Validar el monto mientras el usuario escribe
        montoInput.addEventListener('input', function() {
            const max = parseFloat(this.getAttribute('max'));
            const value = parseFloat(this.value || 0);
            
            if (value > max) {
                this.setCustomValidity('El monto excede el saldo disponible.');
            } else if (value <= 0) {
                this.setCustomValidity('El monto debe ser mayor que cero.');
            } else {
                this.setCustomValidity('');
            }
        });
    }
});

/* ---------- FUNCIÓN PARA LLENAR EL NÚMERO DE CUENTA DESDE UN EJEMPLO ---------- */
function fillAccountNumber(number) {
    const input = document.getElementById('nroCuenta');
    if (input) {
        input.value = number;
        input.focus();
        
        // Agregar animación al botón de búsqueda
        const searchBtn = document.getElementById('searchButton');
        if (searchBtn) {
            searchBtn.classList.add('animate__animated', 'animate__heartBeat');
            setTimeout(() => {
                searchBtn.classList.remove('animate__animated', 'animate__heartBeat');
            }, 1000);
        }
        
        // Actualizar animación de escaneo
        const scanAnimation = document.querySelector('.scan-animation');
        if (scanAnimation) {
            scanAnimation.style.opacity = '1';
        }
    }
}

/* ---------- FUNCIÓN PARA MOSTRAR AYUDA EN TOAST ---------- */
function showHelp(topic) {
    let message = '';
    switch(topic) {
        case 'account':
            message = 'El número de cuenta de 10 dígitos se encuentra en su libreta bancaria, tarjeta de débito, o en la sección "Mis Cuentas" de la aplicación móvil.';
            break;
        case 'balance':
            message = 'Antes de realizar un retiro, verifique que su cuenta tenga saldo disponible. Puede consultarlo en cajeros automáticos, por banca telefónica o en línea.';
            break;
        case 'authorization':
            message = 'Solo puede retirar fondos de cuentas donde usted es el titular o tiene una autorización registrada. Para autorizar a terceros, visite su sucursal.';
            break;
        case 'limits':
            message = 'Los límites de retiro varían según el tipo de cuenta y el canal utilizado. En ventanilla puede retirar hasta el total de su saldo disponible.';
            break;
        default:
            message = 'Para más información, contacte a nuestro centro de atención al cliente.';
    }
    
    showToast(message);
}

/* ---------- FUNCIÓN PARA MOSTRAR INDICADOR DE CARGA ---------- */
function showLoadingIndicator(message = 'Buscando cuenta...') {
    // Crear el overlay de carga
    const overlay = document.createElement('div');
    overlay.className = 'loading-overlay';
    
    // Crear el spinner
    const spinner = document.createElement('div');
    spinner.className = 'spinner';
    overlay.appendChild(spinner);
    
    // Crear el mensaje
    const text = document.createElement('div');
    text.className = 'loading-text';
    text.textContent = message;
    overlay.appendChild(text);
    
    // Agregar al cuerpo del documento
    document.body.appendChild(overlay);
}

/* ---------- FUNCIÓN PARA MOSTRAR NOTIFICACIONES TOAST ---------- */
function showToast(message) {
    // Eliminar toast existente si hay uno
    const existingToast = document.querySelector('.toast-notification');
    if (existingToast) {
        document.body.removeChild(existingToast);
    }
    
    // Crear nuevo toast
    const toast = document.createElement('div');
    toast.className = 'toast-notification';
    toast.textContent = message;
    
    // Agregar al documento
    document.body.appendChild(toast);
    
    // Eliminar automáticamente después de un tiempo
    setTimeout(() => {
        if (document.body.contains(toast)) {
            toast.style.animation = 'fadeInUp 0.3s ease reverse';
            setTimeout(() => {
                if (document.body.contains(toast)) {
                    document.body.removeChild(toast);
                }
            }, 300);
        }
    }, 4000);
}
</script>
</body>
</html>
