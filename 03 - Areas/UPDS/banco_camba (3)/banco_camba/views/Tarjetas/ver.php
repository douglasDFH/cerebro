<!DOCTYPE html>
<html lang="<?php echo $this->session->getLanguage(); ?>">
<head>
    <!-- Enlace a la hoja de estilos principal -->
    <link rel="stylesheet" href="assets/css/StyleTarjeta.css">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo $lang['app_name']; ?> - <?php echo isset($lang['card_details']) ? $lang['card_details'] : 'Detalles de Tarjeta'; ?></title>
</head>
<body>
        
    <!-- Contenedor de detalles de la cuenta asociada a la tarjeta -->
    <div class="card">
        <div class="card-header">
            <h2><?php echo isset($lang['card_details']) ? $lang['card_details'] : 'Detalles de Tarjeta'; ?></h2>
            <div class="action-buttons-container">
                <a href="#" class="btn btn-warning">
                    <i class="fa fa-edit"></i> <?php echo isset($lang['edit']) ? $lang['edit'] : 'Editar'; ?>
                </a>
                <a href="#" class="btn btn-warning">
                    <i class="fa fa-ban"></i> <?php echo isset($lang['deactivate']) ? $lang['deactivate'] : 'Desactivar'; ?>
                </a>
                <a href="#" class="btn btn-warning">
                    <i class="fa fa-arrow-left"></i> <?php echo isset($lang['back']) ? $lang['back'] : 'Volver'; ?>
                </a>
            </div>
        </div>
        
        <!-- Contenedor de la tarjeta visual -->
        <div class="card-visual-container">
            <div class="card-visual-header">
                <h2><?php echo $lang['app_name']; ?></h2>
            </div>
            
            <!-- Representación visual de la tarjeta -->
            <div class="card-visual">
                <div class="card-logo">
                    <img src="assets/img/logo.png" alt="<?php echo $lang['app_name']; ?>" />
                    <span style="margin-left: 10px; font-weight: bold; letter-spacing: 1px;"><?php echo $lang['app_name']; ?></span>
                </div>
                <div class="chip"></div>
                <div class="card-number-display">**** **** **** 3470</div>
                <div class="card-expiry-display">
                    <span style="font-size: 15px; opacity: 0.8;"><?php echo isset($lang['valid_until']) ? $lang['valid_until'] : 'VÁLIDA HASTA'; ?></span><br>
                    12/39
                </div>
                <div class="card-holder-display">Admin Sistema Banco</div>
                <div style="position: absolute; bottom: 20px; right: 20px; display: flex; flex-direction: column; align-items: flex-end;">
                    <span style="margin-left: 15px; font-weight: bold; letter-spacing: 1px;"><?php echo isset($lang['debit']) ? $lang['debit'] : 'DÉBITO'; ?></span>
                </div>
            </div>
            
            <!-- Alerta informativa -->
            <div class="alert-mercantil">
                <i class="fa fa-info-circle" style="margin-right: 15px;"></i>
                <?php echo isset($lang['sensitive_data_message']) ? $lang['sensitive_data_message'] : 'Los datos sensibles como el número de tarjeta, CVV y PIN están enmascarados por seguridad. Haga clic en los iconos de ojo para revelar la información.'; ?>
            </div>
        </div>
        
        <!-- Contenedor de detalles de la cuenta asociada a la tarjeta -->
        <div class="card">
            <div class="card-header">
                <h2><?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de Cuenta'; ?></h2>
            </div>
            
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <h3 class="title-with-line"><?php echo isset($lang['card_information']) ? $lang['card_information'] : 'Información de Tarjeta'; ?></h3>
                        <div class="table-container">
                            <table class="table">
                                <tr>
                                    <th><?php echo isset($lang['card_number']) ? $lang['card_number'] : 'Número de Tarjeta'; ?>:</th>
                                    <td>
                                        <div class="d-flex align-items-center">
                                            <span class="masked-card-number">**** **** **** 3470</span>
                                            <button type="button" class="btn-action btn-view ml-2" id="showCardNumber">
                                                <i class="fa fa-eye"></i>
                                            </button>
                                            <span class="full-card-number d-none">1234 5678 9012 3470</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['card_type']) ? $lang['card_type'] : 'Tipo de Tarjeta'; ?>:</th>
                                    <td><?php echo isset($lang['debit']) ? $lang['debit'] : 'Débito'; ?></td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['expiration_date']) ? $lang['expiration_date'] : 'Fecha de Expiración'; ?>:</th>
                                    <td>12/39</td>
                                </tr>
                                <tr>
                                    <th>CVV:</th>
                                    <td>
                                        <div class="d-flex align-items-center">
                                            <span class="masked-cvv">***</span>
                                            <button type="button" class="btn-action btn-view ml-2" id="showCVV">
                                                <i class="fa fa-eye"></i>
                                            </button>
                                            <span class="full-cvv d-none">137</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>PIN:</th>
                                    <td>
                                        <div class="d-flex align-items-center">
                                            <span class="masked-pin">****</span>
                                            <button type="button" class="btn-action btn-view ml-2" id="showPIN">
                                                <i class="fa fa-eye"></i>
                                            </button>
                                            <span class="full-pin d-none">1248</span>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?>:</th>
                                    <td>
                                        <span class="badge bg-danger"><?php echo isset($lang['inactive']) ? $lang['inactive'] : 'Inactiva'; ?></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <h3 class="title-with-line"><?php echo isset($lang['associated_account']) ? $lang['associated_account'] : 'Cuenta Asociada'; ?></h3>
                        <div class="table-container">
                            <table class="table">
                                <tr>
                                    <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?>:</th>
                                    <td>1234567904</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?>:</th>
                                    <td><?php echo isset($lang['checking_account']) ? $lang['checking_account'] : 'Cuenta Corriente'; ?></td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?>:</th>
                                    <td><?php echo isset($lang['dollars']) ? $lang['dollars'] : 'Dólares'; ?></td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['balance']) ? $lang['balance'] : 'Saldo'; ?>:</th>
                                    <td>$ 7,500.00</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['opening_date']) ? $lang['opening_date'] : 'Fecha de Apertura'; ?>:</th>
                                    <td>15/01/2023</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['status']) ? $lang['status'] : 'Estado'; ?>:</th>
                                    <td>
                                        <span class="badge bg-success"><?php echo isset($lang['active']) ? $lang['active'] : 'Activa'; ?></span>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="action-buttons" style="margin-top: 15px;">
                            <a href="#" class="btn btn-info">
                                <i class="fa fa-bank"></i> <?php echo isset($lang['view_account']) ? $lang['view_account'] : 'Ver Cuenta'; ?>
                            </a>
                            <a href="#" class="btn btn-primary">
                                <i class="fa fa-history"></i> <?php echo isset($lang['transaction_history']) ? $lang['transaction_history'] : 'Historial de Transacciones'; ?>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Contenedor de información del cliente -->
        <div class="card">
            <div class="card-header">
                <h2><?php echo isset($lang['client_information']) ? $lang['client_information'] : 'Información del Cliente'; ?></h2>
            </div>
            
            <div class="card-body">
                <div class="row">
                    <div class="col-md-6">
                        <h3 class="title-with-line"><?php echo isset($lang['personal_information']) ? $lang['personal_information'] : 'Datos Personales'; ?></h3>
                        <div class="table-container">
                            <table class="table">
                                <tr>
                                    <th><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?>:</th>
                                    <td>Admin Sistema</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['paternal_last_name']) ? $lang['paternal_last_name'] : 'Apellido Paterno'; ?>:</th>
                                    <td>Banco</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['maternal_last_name']) ? $lang['maternal_last_name'] : 'Apellido Materno'; ?>:</th>
                                    <td>Mercantil</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['id_number']) ? $lang['id_number'] : 'Número de Identificación'; ?>:</th>
                                    <td>12345678</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['birth_date']) ? $lang['birth_date'] : 'Fecha de Nacimiento'; ?>:</th>
                                    <td>01/01/1990</td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                    <div class="col-md-6">
                        <h3 class="title-with-line"><?php echo isset($lang['contact_information']) ? $lang['contact_information'] : 'Información de Contacto'; ?></h3>
                        <div class="table-container">
                            <table class="table">
                                <tr>
                                    <th><?php echo isset($lang['address']) ? $lang['address'] : 'Dirección'; ?>:</th>
                                    <td>Av. Principal #123, Ciudad</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['phone']) ? $lang['phone'] : 'Teléfono'; ?>:</th>
                                    <td>+00 123 456 7890</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['email']) ? $lang['email'] : 'Correo Electrónico'; ?>:</th>
                                    <td>admin@bancomercantil.com</td>
                                </tr>
                                <tr>
                                    <th><?php echo isset($lang['branch']) ? $lang['branch'] : 'Sucursal'; ?>:</th>
                                    <td>Central</td>
                                </tr>
                            </table>
                        </div>

                        <div class="action-buttons" style="margin-top: 15px;">
                            <a href="#" class="btn btn-primary">
                                <i class="fa fa-user"></i> <?php echo isset($lang['view_full_profile']) ? $lang['view_full_profile'] : 'Ver Perfil Completo'; ?>
                            </a>
                            <a href="#" class="btn btn-info">
                                <i class="fa fa-file-text"></i> <?php echo isset($lang['view_history']) ? $lang['view_history'] : 'Ver Historial'; ?>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <script>
    document.addEventListener('DOMContentLoaded', function() {
        // Animación de entrada para elementos
        const animateElements = document.querySelectorAll('.card, .table, h3, .card-visual-container');
        animateElements.forEach((element, index) => {
            element.style.opacity = '0';
            element.style.transform = 'translateY(20px)';
            element.style.transition = 'opacity 0.5s ease, transform 0.5s ease';
            element.style.transitionDelay = (index * 0.1) + 's';
            
            setTimeout(() => {
                element.style.opacity = '1';
                element.style.transform = 'translateY(0)';
            }, 100);
        });
        
        // Añadir clase animate-row a las filas de la tabla
        const tableRows = document.querySelectorAll('tbody tr');
        tableRows.forEach((row, index) => {
            row.classList.add('animate-row');
        });
        
        // Funcionalidad para mostrar/ocultar número de tarjeta completo
        const btnShowCardNumber = document.getElementById('showCardNumber');
        const maskedCardNumber = document.querySelector('.masked-card-number');
        const fullCardNumber = document.querySelector('.full-card-number');
        
        if (btnShowCardNumber) {
            btnShowCardNumber.addEventListener('click', function() {
                if (maskedCardNumber.classList.contains('d-none')) {
                    maskedCardNumber.classList.remove('d-none');
                    fullCardNumber.classList.add('d-none');
                    this.innerHTML = '<i class="fa fa-eye"></i>';
                } else {
                    maskedCardNumber.classList.add('d-none');
                    fullCardNumber.classList.remove('d-none');
                    fullCardNumber.classList.add('fadeIn');
                    this.innerHTML = '<i class="fa fa-eye-slash"></i>';
                }
            });
        }
        
        // Funcionalidad para mostrar/ocultar CVV
        const btnShowCVV = document.getElementById('showCVV');
        const maskedCVV = document.querySelector('.masked-cvv');
        const fullCVV = document.querySelector('.full-cvv');
        
        if (btnShowCVV) {
            btnShowCVV.addEventListener('click', function() {
                if (maskedCVV.classList.contains('d-none')) {
                    maskedCVV.classList.remove('d-none');
                    fullCVV.classList.add('d-none');
                    this.innerHTML = '<i class="fa fa-eye"></i>';
                } else {
                    maskedCVV.classList.add('d-none');
                    fullCVV.classList.remove('d-none');
                    fullCVV.classList.add('fadeIn');
                    this.innerHTML = '<i class="fa fa-eye-slash"></i>';
                }
            });
        }
        
        // Funcionalidad para mostrar/ocultar PIN
        const btnShowPIN = document.getElementById('showPIN');
        const maskedPIN = document.querySelector('.masked-pin');
        const fullPIN = document.querySelector('.full-pin');
        
        if (btnShowPIN) {
            btnShowPIN.addEventListener('click', function() {
                if (maskedPIN.classList.contains('d-none')) {
                    maskedPIN.classList.remove('d-none');
                    fullPIN.classList.add('d-none');
                    this.innerHTML = '<i class="fa fa-eye"></i>';
                } else {
                    maskedPIN.classList.add('d-none');
                    fullPIN.classList.remove('d-none');
                    fullPIN.classList.add('fadeIn');
                    this.innerHTML = '<i class="fa fa-eye-slash"></i>';
                }
            });
        }
        
        // Añadir efecto hover a la tarjeta visual
        const cardVisual = document.querySelector('.card-visual');
        if (cardVisual) {
            cardVisual.addEventListener('mousemove', function(e) {
                const rect = this.getBoundingClientRect();
                const x = e.clientX - rect.left;
                const y = e.clientY - rect.top;
                
                // Cálculo para efecto 3D suave
                const centerX = rect.width / 2;
                const centerY = rect.height / 2;
                
                const rotateY = ((x - centerX) / centerX) * 5;
                const rotateX = -((y - centerY) / centerY) * 5;
                
                this.style.transform = `perspective(1000px) rotateX(${rotateX}deg) rotateY(${rotateY}deg) translateY(-5px)`;
            });
            
            cardVisual.addEventListener('mouseleave', function() {
                this.style.transform = 'perspective(1000px) rotateX(0) rotateY(0) translateY(0)';
            });
        }
    });
    </script>
</body>
</html>