<h1><?php echo $lang['request_card']; ?></h1>

<div class="card">
    <div class="card-header">
        <h3><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno . ' ' . $cliente->apellidoMaterno); ?></h3>
        <p><?php echo $lang['account_number']; ?>: <strong><?php echo htmlspecialchars($cuenta->nroCuenta); ?></strong></p>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-6">
                <form method="POST" action="index.php?controller=cuenta&action=crearTarjeta&id=<?php echo $cuenta->idCuenta; ?>" class="needs-validation">
                    <div class="form-group">
                        <label for="pin" class="form-label"><?php echo $lang['pin']; ?> <span class="text-danger">*</span></label>
                        <input type="password" class="form-control" id="pin" name="pin" pattern="[0-9]{4}" maxlength="4" required>
                        <small class="form-text text-muted"><?php echo $lang['pin_must_be_4_digits']; ?></small>
                    </div>
                    
                    <div class="form-group">
                        <label for="pin_confirmacion" class="form-label"><?php echo $lang['confirm_pin']; ?> <span class="text-danger">*</span></label>
                        <input type="password" class="form-control" id="pin_confirmacion" name="pin_confirmacion" pattern="[0-9]{4}" maxlength="4" required>
                    </div>
                    
                    <div class="alert alert-info mt-3">
                        <strong><?php echo $lang['important']; ?>:</strong> <?php echo $lang['card_pin_info']; ?>
                    </div>
                    
                    <div class="form-group mt-3">
                        <button type="submit" class="btn btn-primary"><?php echo $lang['request_card']; ?></button>
                        <a href="index.php?controller=cuenta&action=ver&id=<?php echo $cuenta->idCuenta; ?>" class="btn btn-secondary"><?php echo $lang['cancel']; ?></a>
                    </div>
                </form>
            </div>
            <div class="col-md-6">
                <div class="card-preview" style="background: linear-gradient(45deg, #056f1f, #0a4d14); color: white; padding: 20px; border-radius: 10px; margin-bottom: 20px; min-height: 200px;">
                    <div style="font-size: 1.2rem; margin-bottom: 20px; letter-spacing: 2px;">
                        **** **** **** ****
                    </div>
                    <div style="display: flex; justify-content: space-between;">
                        <div>
                            <div style="font-size: 0.8rem; text-transform: uppercase;"><?php echo $lang['cardholder']; ?></div>
                            <div><?php echo htmlspecialchars($cliente->nombre . ' ' . $cliente->apellidoPaterno); ?></div>
                        </div>
                        <div>
                            <div style="font-size: 0.8rem; text-transform: uppercase;"><?php echo $lang['expiration_date']; ?></div>
                            <div>--/--</div>
                        </div>
                    </div>
                </div>
                
                <div class="card bg-light">
                    <div class="card-body">
                        <h5><?php echo $lang['card_benefits']; ?></h5>
                        <ul>
                            <li><?php echo $lang['card_benefit_1']; ?></li>
                            <li><?php echo $lang['card_benefit_2']; ?></li>
                            <li><?php echo $lang['card_benefit_3']; ?></li>
                            <li><?php echo $lang['card_benefit_4']; ?></li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>
document.addEventListener('DOMContentLoaded', function() {
    // Validación del formulario
    const form = document.querySelector('.needs-validation');
    form.addEventListener('submit', function(event) {
        const pin = document.getElementById('pin').value;
        const pinConfirmacion = document.getElementById('pin_confirmacion').value;
        
        if (pin !== pinConfirmacion) {
            event.preventDefault();
            alert('<?php echo $lang['pins_dont_match']; ?>');
            return false;
        }
        
        if (!form.checkValidity()) {
            event.preventDefault();
            event.stopPropagation();
        }
        form.classList.add('was-validated');
    }, false);
    
    // Restringir entrada a solo números
    const pinInputs = document.querySelectorAll('input[type="password"]');
    pinInputs.forEach(input => {
        input.addEventListener('input', function(e) {
            this.value = this.value.replace(/[^0-9]/g, '');
            if (this.value.length > 4) {
                this.value = this.value.slice(0, 4);
            }
        });
        
        input.addEventListener('keypress', function(e) {
            if (!/[0-9]/.test(e.key)) {
                e.preventDefault();
            }
        });
    });
});
</script>