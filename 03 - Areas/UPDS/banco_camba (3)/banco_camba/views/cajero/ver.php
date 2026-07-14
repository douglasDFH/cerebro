<link rel="stylesheet" href="assets/css/styleVer.css">
<div class="card client-card">
    <div class="card-header d-flex justify-content-between align-items-center">
        <h2 class="title-with-line"><?php echo isset($lang['atm_transaction_details']) ? $lang['atm_transaction_details'] : 'Detalles de Transacción ATM'; ?></h2>
        <div>
            <!-- Botón para generar comprobante -->
            <a href="index.php?controller=transaccionatm&action=comprobante&id=<?php echo $this->model->idTransaccionATM; ?>" class="btn-primary">
                <i class="fa fa-file-text"></i> <?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Imprimir Comprobante'; ?>
            </a>
            
            <!-- Botón para volver a la lista -->
            <a href="index.php?controller=transaccionatm&action=listar" class="btn-secondary">
                <i class="fa fa-arrow-left"></i> <?php echo isset($lang['back']) ? $lang['back'] : 'Volver'; ?>
            </a>
        </div>
    </div>
    <div class="card-body">
        <div class="info-section">
            <!-- Información básica de la transacción -->
            <div class="row transaction-basic-info">
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['transaction_id']) ? $lang['transaction_id'] : 'ID Transacción'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['resulting_balance']) ? $lang['resulting_balance'] : 'Saldo Resultante'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo $this->model->idTransaccionATM; ?></p>
                    <p>
                        <?php
                        switch($this->model->tipoTransaccion) {
                            case 'retiro':
                                echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro';
                                break;
                            case 'deposito':
                                echo isset($lang['deposit']) ? $lang['deposit'] : 'Depósito';
                                break;
                            case 'consulta':
                                echo isset($lang['inquiry']) ? $lang['inquiry'] : 'Consulta';
                                break;
                            default:
                                echo isset($lang['other']) ? $lang['other'] : 'Otro';
                        }
                        ?>
                    </p>
                    <p><?php echo date('d/m/Y', strtotime($this->model->fecha)); ?></p>
                    <p><?php echo date('H:i:s', strtotime($this->model->hora)); ?></p>
                    <p><strong><?php echo ($cuenta && property_exists($cuenta, 'tipoMoneda') ? ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') : '') . number_format($this->model->monto, 2); ?></strong></p>
                    <p><?php echo ($cuenta && property_exists($cuenta, 'tipoMoneda') ? ($cuenta->tipoMoneda == 1 ? 'Bs. ' : '$ ') : '') . number_format($this->model->saldoResultante, 2); ?></p>
                    <p><?php echo htmlspecialchars($this->model->descripcion); ?></p>
                </div>
            </div>
            
            <hr>
            
            <?php if (isset($cuenta) && $cuenta): ?>
            <!-- Información de la cuenta -->
            <h3 class="section-title"><?php echo isset($lang['account_information']) ? $lang['account_information'] : 'Información de la Cuenta'; ?></h3>
            <div class="row account-info">
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['account_type']) ? $lang['account_type'] : 'Tipo de Cuenta'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['currency']) ? $lang['currency'] : 'Moneda'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo $cuenta->nroCuenta; ?></p>
                    <p><?php echo property_exists($cuenta, 'tipoCuenta') ? ($cuenta->tipoCuenta == 1 ? 
                        (isset($lang['savings_account']) ? $lang['savings_account'] : 'Cuenta de Ahorro') : 
                        (isset($lang['checking_account']) ? $lang['checking_account'] : 'Cuenta Corriente')) : '-'; ?></p>
                    <p><?php echo property_exists($cuenta, 'tipoMoneda') ? ($cuenta->tipoMoneda == 1 ? 
                        (isset($lang['bolivianos']) ? $lang['bolivianos'] : 'Bolivianos') : 
                        (isset($lang['dollars']) ? $lang['dollars'] : 'Dólares')) : '-'; ?></p>
                </div>
            </div>
            
            <hr>
            <?php endif; ?>
            
            <?php if (isset($cliente) && $cliente): ?>
            <!-- Información del cliente -->
            <h3 class="section-title"><?php echo isset($lang['client_information']) ? $lang['client_information'] : 'Información del Cliente'; ?></h3>
            <div class="row client-info">
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['name']) ? $lang['name'] : 'Nombre'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['last_name']) ? $lang['last_name'] : 'Apellido'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['id_number']) ? $lang['id_number'] : 'Cédula de Identidad'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo property_exists($cliente, 'nombre') ? htmlspecialchars($cliente->nombre) : '-'; ?></p>
                    <p><?php 
                        $apellido = '';
                        if (property_exists($cliente, 'apellidoPaterno')) {
                            $apellido .= htmlspecialchars($cliente->apellidoPaterno);
                        }
                        if (property_exists($cliente, 'apellidoMaterno')) {
                            $apellido .= ' ' . htmlspecialchars($cliente->apellidoMaterno);
                        }
                        echo $apellido ? $apellido : '-'; 
                    ?></p>
                    <p><?php 
                        if (property_exists($cliente, 'ci')) {
                            echo htmlspecialchars($cliente->ci);
                        } elseif (property_exists($cliente, 'numeroDocumento')) {
                            echo htmlspecialchars($cliente->numeroDocumento);
                        } else {
                            echo '-';
                        }
                    ?></p>
                </div>
            </div>
            <?php endif; ?>
            
            <?php if (!empty($this->model->cuentaOrigen) || !empty($this->model->cuentaDestino)): ?>
            <hr>
            
            <!-- Información adicional de cuentas origen/destino -->
            <h3 class="section-title"><?php echo isset($lang['additional_information']) ? $lang['additional_information'] : 'Información Adicional'; ?></h3>
            <div class="row additional-info">
                <?php if (property_exists($this->model, 'cuentaOrigen') && !empty($this->model->cuentaOrigen)): ?>
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['source_account']) ? $lang['source_account'] : 'Cuenta Origen'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo htmlspecialchars($this->model->cuentaOrigen); ?></p>
                </div>
                <?php endif; ?>
                
                <?php if (property_exists($this->model, 'cuentaDestino') && !empty($this->model->cuentaDestino)): ?>
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['destination_account']) ? $lang['destination_account'] : 'Cuenta Destino'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo htmlspecialchars($this->model->cuentaDestino); ?></p>
                </div>
                <?php endif; ?>
            </div>
            <?php endif; ?>
            
            <?php if (isset($cajero) && $cajero): ?>
            <hr>
            
            <!-- Información del cajero ATM -->
            <h3 class="section-title"><?php echo isset($lang['atm_information']) ? $lang['atm_information'] : 'Información del Cajero Automático'; ?></h3>
            <div class="row atm-info">
                <div class="col-md-4">
                    <p><strong><?php echo isset($lang['atm_name']) ? $lang['atm_name'] : 'Nombre del Cajero'; ?>:</strong></p>
                    <p><strong><?php echo isset($lang['location']) ? $lang['location'] : 'Ubicación'; ?>:</strong></p>
                </div>
                <div class="col-md-8">
                    <p><?php echo property_exists($cajero, 'nombre') ? htmlspecialchars($cajero->nombre) : '-'; ?></p>
                    <p><?php echo property_exists($cajero, 'ubicacion') ? htmlspecialchars($cajero->ubicacion) : '-'; ?></p>
                </div>
            </div>
            <?php endif; ?>
        </div>
    </div>
</div>

<style>
.client-card {
    box-shadow: 0 0 15px rgba(0,0,0,0.1);
    border-radius: 5px;
    overflow: hidden;
    margin-bottom: 20px;
}
.card-header {
    background-color: #006634;
    color: white;
    padding: 15px;
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.title-with-line {
    font-size: 1.5rem;
    margin: 0;
    position: relative;
    display: inline-block;
}
.btn-primary {
    background-color: #006634;
    color: white;
    border: none;
    padding: 8px 15px;
    border-radius: 3px;
    text-decoration: none;
    margin-left: 10px;
}
.btn-secondary {
    background-color: #6c757d;
    color: white;
    border: none;
    padding: 8px 15px;
    border-radius: 3px;
    text-decoration: none;
}
.card-body {
    padding: 20px;
    background-color: #f8f9fa;
}
.info-section {
    background-color: white;
    border-radius: 5px;
    padding: 20px;
    box-shadow: 0 0 5px rgba(0,0,0,0.05);
}
.section-title {
    color: #006634;
    font-size: 1.2rem;
    margin-bottom: 15px;
}
hr {
    margin: 20px 0;
    border-top: 1px solid #ddd;
}
.row p {
    margin-bottom: 10px;
}
</style>

<script>
document.addEventListener('DOMContentLoaded', function() {
    // Script para asegurar que la página cargue correctamente
    console.log('Página de detalles de transacción ATM cargada correctamente.');
});
</script>