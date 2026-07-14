<h1><?php echo $lang['reports']; ?> - <?php echo $lang['account_activity']; ?></h1>

<div class="card">
    <div class="card-header"><?php echo $lang['search_criteria']; ?></div>
    <div class="card-body">
        <form method="POST" action="index.php?controller=reporte&action=actividadCuentas">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <label for="idCuenta"><?php echo $lang['select_account']; ?></label>
                        <select class="form-control" id="idCuenta" name="idCuenta" required>
                            <option value=""><?php echo $lang['select_option']; ?></option>
                            <?php foreach ($cuentas as $cuenta): ?>
                                <option value="<?php echo $cuenta['idCuenta']; ?>" <?php echo (isset($_POST['idCuenta']) && $_POST['idCuenta'] == $cuenta['idCuenta']) ? 'selected' : ''; ?>>
                                    <?php echo $cuenta['nroCuenta']; ?> - <?php echo $cuenta['cliente_nombre']; ?>
                                </option>
                            <?php endforeach; ?>
                        </select>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fechaInicio"><?php echo $lang['start_date']; ?></label>
                        <input type="date" class="form-control" id="fechaInicio" name="fechaInicio" value="<?php echo $fechaInicio; ?>" required>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label for="fechaFin"><?php echo $lang['end_date']; ?></label>
                        <input type="date" class="form-control" id="fechaFin" name="fechaFin" value="<?php echo $fechaFin; ?>" required>
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="form-group" style="margin-top: 25px;">
                        <button type="submit" class="btn btn-primary"><?php echo $lang['generate_report']; ?></button>
                    </div>
                </div>
            </div>
        </form>
    </div>
</div>

<?php if ($_SERVER['REQUEST_METHOD'] == 'POST' && $cuentaSeleccionada): ?>
<div class="card mt-4">
    <div class="card-header">
        <?php echo $lang['results']; ?>: <?php echo $cuentaSeleccionada['nroCuenta']; ?> - <?php echo $cuentaSeleccionada['nombre'] . ' ' . $cuentaSeleccionada['apellidoPaterno'] . ' ' . $cuentaSeleccionada['apellidoMaterno']; ?>
        <a href="javascript:void(0);" onclick="imprimirActividadCuenta('<?php echo $cuentaSeleccionada['idCuenta']; ?>', '<?php echo $fechaInicio; ?>', '<?php echo $fechaFin; ?>');" class="btn btn-sm btn-info float-right">
            <i class="fa fa-print"></i> <?php echo $lang['print']; ?>
        </a>
    </div>
    <div class="card-body">
        <div class="row">
            <div class="col-md-6">
                <div class="alert alert-info">
                    <p><strong><?php echo $lang['balance']; ?>:</strong> <?php echo number_format($cuentaSeleccionada['saldo'], 2); ?> <?php echo $cuentaSeleccionada['tipoMoneda'] == 1 ? 'BOB' : 'USD'; ?></p>
                    <p><strong><?php echo $lang['open_date']; ?>:</strong> <?php echo date('d/m/Y', strtotime($cuentaSeleccionada['fechaApertura'])); ?></p>
                </div>
            </div>
            <div class="col-md-6">
                <?php if ($resumenActividad): ?>
                <div class="alert alert-success">
                    <p><strong><?php echo $lang['total_transactions']; ?>:</strong> <?php echo $resumenActividad['total_transacciones']; ?></p>
                    <p><strong><?php echo $lang['withdrawals']; ?>:</strong> <?php echo $resumenActividad['total_retiros']; ?> (<?php echo number_format($resumenActividad['monto_retiros'], 2); ?>)</p>
                    <p><strong><?php echo $lang['deposits']; ?>:</strong> <?php echo $resumenActividad['total_depositos']; ?> (<?php echo number_format($resumenActividad['monto_depositos'], 2); ?>)</p>
                </div>
                <?php endif; ?>
            </div>
        </div>
        
        <?php if (count($actividadDetalle) > 0): ?>
            <div class="mt-3">
                <canvas id="chartActividad" width="400" height="200"></canvas>
            </div>
            
            <div class="mt-3">
                <table class="table table-striped">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th><?php echo $lang['date']; ?></th>
                            <th><?php echo $lang['time']; ?></th>
                            <th><?php echo $lang['transaction_type']; ?></th>
                            <th><?php echo $lang['amount']; ?></th>
                            <th><?php echo $lang['resulting_balance']; ?></th>
                            <th><?php echo $lang['description']; ?></th>
                        </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($actividadDetalle as $actividad): ?>
                            <tr>
                                <td><?php echo $actividad['idTransaccion']; ?></td>
                                <td><?php echo date('d/m/Y', strtotime($actividad['fecha'])); ?></td>
                                <td><?php echo $actividad['hora']; ?></td>
                                <td>
                                    <?php if ($actividad['tipoTransaccion'] == 1): ?>
                                        <span class="badge" style="background-color: #dc3545;"><?php echo $lang['withdrawal']; ?></span>
                                    <?php else: ?>
                                        <span class="badge" style="background-color: #28a745;"><?php echo $lang['deposit']; ?></span>
                                    <?php endif; ?>
                                </td>
                                <td><?php echo number_format($actividad['monto'], 2); ?></td>
                                <td><?php echo number_format($actividad['saldoResultante'], 2); ?></td>
                                <td><?php echo $actividad['descripcion']; ?></td>
                            </tr>
                        <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        <?php else: ?>
            <div class="alert alert-warning">
                <?php echo $lang['no_transactions_found']; ?>
            </div>
        <?php endif; ?>
        
        <div class="mt-3 text-center">
            <a href="index.php?controller=reporte&action=index" class="btn btn-secondary"><?php echo $lang['back']; ?></a>
        </div>
    </div>
</div>

<!-- Script para generar gráfico -->
<script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
<script>
    document.addEventListener('DOMContentLoaded', function() {
        <?php if (count($actividadDetalle) > 0): ?>
        var ctx = document.getElementById('chartActividad').getContext('2d');
        
        // Preparar datos para el gráfico
        var fechas = [];
        var saldos = [];
        
        <?php
        // Revertir el array para que el gráfico muestre la evolución del saldo en orden cronológico
        $actividadGrafico = array_reverse($actividadDetalle);
        foreach ($actividadGrafico as $act):
        ?>
            fechas.push('<?php echo date('d/m/Y', strtotime($act['fecha'])) . ' ' . $act['hora']; ?>');
            saldos.push(<?php echo $act['saldoResultante']; ?>);
        <?php endforeach; ?>
        
        // Crear gráfico
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: fechas,
                datasets: [{
                    label: '<?php echo $lang['balance_evolution']; ?>',
                    data: saldos,
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',
                    borderColor: 'rgba(75, 192, 192, 1)',
                    borderWidth: 2,
                    tension: 0.1
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: false
                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: '<?php echo $lang['balance_evolution']; ?>'
                    }
                }
            }
        });
        <?php endif; ?>
    });
    
    // Función para imprimir reporte
    function imprimirActividadCuenta(idCuenta, fechaInicio, fechaFin) {
        var url = 'index.php?controller=reporte&action=imprimirActividadCuenta&cuenta=' + idCuenta + '&inicio=' + fechaInicio + '&fin=' + fechaFin;
        window.open(url, '_blank', 'width=800,height=600');
    }
</script>
<?php endif; ?>