<!DOCTYPE html>
<html lang="<?php echo isset($_SESSION['lang']) ? $_SESSION['lang'] : 'es'; ?>">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?php echo isset($lang['transaction_search']) ? $lang['transaction_search'] : 'Búsqueda de Transacciones'; ?> - <?php echo isset($lang['bank_name']) ? $lang['bank_name'] : 'Banco Mercantil'; ?></title>
    <link rel="stylesheet" href="assets/css/StyleBuscar.css">
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet">
    
</head>
<body>
    <div class="main-content">
        <div class="search-card">
            <div class="search-header">
                <i class="fas fa-search"></i>
                <h2><?php echo isset($lang['transaction_search']) ? $lang['transaction_search'] : 'Búsqueda de Transacciones'; ?></h2>
            </div>
            
            <div class="search-body">
                <!-- Formulario de búsqueda -->
                <form method="GET" action="index.php" class="search-form">
                    <input type="hidden" name="controller" value="transaccion">
                    <input type="hidden" name="action" value="buscar">
                    
                    <div class="form-row">
                        <!-- Selector de cuenta -->
                        <div class="form-group">
                            <label for="idCuenta"><i class="fas fa-wallet"></i> <?php echo isset($lang['account']) ? $lang['account'] : 'Cuenta'; ?></label>
                            <select id="idCuenta" name="idCuenta">
                                <option value="0"><?php echo isset($lang['all_accounts']) ? $lang['all_accounts'] : 'Todas las cuentas'; ?></option>
                                <option value="1">1234567901 - Jorge Martínez</option>
                                <option value="2">1234567902 - Lucía Rodríguez</option>
                                <option value="3">1234567903 - Mario Sánchez</option>
                                <option value="4">1234567904 - Admin Sistema</option>
                            </select>
                        </div>
                        
                        <!-- Selector de tipo de transacción -->
                        <div class="form-group">
                            <label for="tipoTransaccion"><i class="fas fa-exchange-alt"></i> <?php echo isset($lang['transaction_type']) ? $lang['transaction_type'] : 'Tipo de Transacción'; ?></label>
                            <select id="tipoTransaccion" name="tipoTransaccion">
                                <option value="0"><?php echo isset($lang['all_types']) ? $lang['all_types'] : 'Todos los tipos'; ?></option>
                                <option value="1"><?php echo isset($lang['withdrawal']) ? $lang['withdrawal'] : 'Retiro'; ?></option>
                                <option value="2"><?php echo isset($lang['deposit']) ? $lang['deposit'] : 'Depósito'; ?></option>
                                <option value="3"><?php echo isset($lang['transfer_received']) ? $lang['transfer_received'] : 'Transferencia recibida'; ?></option>
                                <option value="4"><?php echo isset($lang['transfer_sent']) ? $lang['transfer_sent'] : 'Transferencia enviada'; ?></option>
                            </select>
                        </div>
                        
                        <!-- Fechas -->
                        <div class="form-group">
                            <label for="fechaInicio"><i class="far fa-calendar-alt"></i> <?php echo isset($lang['start_date']) ? $lang['start_date'] : 'Fecha Inicio'; ?></label>
                            <input type="date" id="fechaInicio" name="fechaInicio">
                        </div>
                        
                        <div class="form-group">
                            <label for="fechaFin"><i class="far fa-calendar-alt"></i> <?php echo isset($lang['end_date']) ? $lang['end_date'] : 'Fecha Fin'; ?></label>
                            <input type="date" id="fechaFin" name="fechaFin">
                        </div>
                        
                        <!-- Botón de búsqueda -->
                        <div class="form-group">
                            <button type="submit" class="btn-search">
                                <i class="fas fa-search"></i> <?php echo isset($lang['search']) ? $lang['search'] : 'Buscar'; ?>
                            </button>
                        </div>
                    </div>
                    
                    <!-- Opciones adicionales de búsqueda (opcional) -->
                    <div class="advanced-options">
                        <a href="#" id="toggleAdvanced"><?php echo isset($lang['advanced_options']) ? $lang['advanced_options'] : 'Opciones avanzadas'; ?> <i class="fas fa-chevron-down"></i></a>
                        
                        <div class="advanced-options-content" style="display: none;">
                            <div class="form-row">
                                <!-- Monto mínimo -->
                                <div class="form-group">
                                    <label for="montoMin"><i class="fas fa-dollar-sign"></i> <?php echo isset($lang['min_amount']) ? $lang['min_amount'] : 'Monto mínimo'; ?></label>
                                    <input type="number" id="montoMin" name="montoMin" min="0" step="0.01">
                                </div>
                                
                                <!-- Monto máximo -->
                                <div class="form-group">
                                    <label for="montoMax"><i class="fas fa-dollar-sign"></i> <?php echo isset($lang['max_amount']) ? $lang['max_amount'] : 'Monto máximo'; ?></label>
                                    <input type="number" id="montoMax" name="montoMax" min="0" step="0.01">
                                </div>
                                
                                <!-- Búsqueda en descripción -->
                                <div class="form-group">
                                    <label for="descripcion"><i class="fas fa-align-left"></i> <?php echo isset($lang['description_contains']) ? $lang['description_contains'] : 'Descripción contiene'; ?></label>
                                    <input type="text" id="descripcion" name="descripcion">
                                </div>
                            </div>
                        </div>
                    </div>
                </form>
                
                <!-- Resultados de la búsqueda -->
                <h3 class="results-title"><i class="fas fa-list"></i> <?php echo isset($lang['search_results']) ? $lang['search_results'] : 'Resultados de la Búsqueda'; ?></h3>
                
                <div class="table-container">
                    <table class="transactions-table">
                        <thead>
                            <tr>
                                <th><?php echo isset($lang['date']) ? $lang['date'] : 'Fecha'; ?></th>
                                <th><?php echo isset($lang['time']) ? $lang['time'] : 'Hora'; ?></th>
                                <th><?php echo isset($lang['type']) ? $lang['type'] : 'Tipo'; ?></th>
                                <th><?php echo isset($lang['account_number']) ? $lang['account_number'] : 'Número de Cuenta'; ?></th>
                                <th><?php echo isset($lang['client']) ? $lang['client'] : 'Cliente'; ?></th>
                                <th><?php echo isset($lang['description']) ? $lang['description'] : 'Descripción'; ?></th>
                                <th><?php echo isset($lang['amount']) ? $lang['amount'] : 'Monto'; ?></th>
                                <th><?php echo isset($lang['actions']) ? $lang['actions'] : 'Acciones'; ?></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>15/01/2023</td>
                                <td>00:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567904</td>
                                <td>Admin Sistema Banco</td>
                                <td><?php echo isset($lang['atm_withdrawal']) ? $lang['atm_withdrawal'] : 'Retiro en cajero'; ?></td>
                                <td class="amount">$ 450.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>14/01/2023</td>
                                <td>23:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567903</td>
                                <td>Mario Sánchez Pérez</td>
                                <td><?php echo isset($lang['counter_deposit']) ? $lang['counter_deposit'] : 'Depósito en ventanilla'; ?></td>
                                <td class="amount">$ 800.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>13/01/2023</td>
                                <td>22:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567902</td>
                                <td>Lucía Rodríguez Sánchez</td>
                                <td><?php echo isset($lang['atm_withdrawal']) ? $lang['atm_withdrawal'] : 'Retiro en cajero'; ?></td>
                                <td class="amount">$ 400.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>12/01/2023</td>
                                <td>21:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567901</td>
                                <td>Jorge Martínez Rodríguez</td>
                                <td><?php echo isset($lang['counter_deposit']) ? $lang['counter_deposit'] : 'Depósito en ventanilla'; ?></td>
                                <td class="amount">$ 700.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>11/01/2023</td>
                                <td>20:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567900</td>
                                <td>Carmen García Martínez</td>
                                <td><?php echo isset($lang['atm_withdrawal']) ? $lang['atm_withdrawal'] : 'Retiro en cajero'; ?></td>
                                <td class="amount">$ 350.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>10/01/2023</td>
                                <td>19:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567899</td>
                                <td>Diego López García</td>
                                <td><?php echo isset($lang['counter_deposit']) ? $lang['counter_deposit'] : 'Depósito en ventanilla'; ?></td>
                                <td class="amount">$ 600.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>09/01/2023</td>
                                <td>18:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567898</td>
                                <td>Sofía Gómez López</td>
                                <td><?php echo isset($lang['atm_withdrawal']) ? $lang['atm_withdrawal'] : 'Retiro en cajero'; ?></td>
                                <td class="amount">$ 300.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>08/01/2023</td>
                                <td>17:00:00</td>
                                <td><span class="badge badge-secondary"><?php echo isset($lang['other']) ? $lang['other'] : 'OTRO'; ?></span></td>
                                <td>1234567897</td>
                                <td>Pedro Fernández Gómez</td>
                                <td><?php echo isset($lang['counter_deposit']) ? $lang['counter_deposit'] : 'Depósito en ventanilla'; ?></td>
                                <td class="amount">$ 500.00</td>
                                <td>
                                    <div class="action-buttons">
                                        <button class="btn-action btn-view" title="<?php echo isset($lang['view']) ? $lang['view'] : 'Ver'; ?>">
                                            <i class="fas fa-eye"></i>
                                        </button>
                                        <button class="btn-action btn-receipt" title="<?php echo isset($lang['receipt']) ? $lang['receipt'] : 'Comprobante'; ?>">
                                            <i class="fas fa-file-alt"></i>
                                        </button>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                
                <!-- Paginación -->
                <div class="pagination-container">
                    <div class="pagination">
                        <a href="#" class="pagination-btn disabled" title="<?php echo isset($lang['previous_page']) ? $lang['previous_page'] : 'Página anterior'; ?>"><i class="fas fa-chevron-left"></i></a>
                        <span class="page-info"><?php echo isset($lang['page']) ? $lang['page'] : 'Página'; ?> 1 <?php echo isset($lang['of']) ? $lang['of'] : 'de'; ?> 5</span>
                        <a href="#" class="pagination-btn" title="<?php echo isset($lang['next_page']) ? $lang['next_page'] : 'Página siguiente'; ?>"><i class="fas fa-chevron-right"></i></a>
                    </div>
                </div>
                
                <!-- Botones de acción -->
                <div class="footer-actions">
                    <button type="button" class="btn-footer btn-print" onclick="window.print();">
                        <i class="fas fa-print"></i> <?php echo isset($lang['print']) ? $lang['print'] : 'Imprimir'; ?>
                    </button>
                    <a href="index.php?controller=transaccion&action=listar" class="btn-footer btn-back">
                        <i class="fas fa-arrow-left"></i> <?php echo isset($lang['back']) ? $lang['back'] : 'Volver'; ?>
                    </a>
                    <button type="button" class="btn-footer btn-export" onclick="exportResults()">
                        <i class="fas fa-file-export"></i> <?php echo isset($lang['export']) ? $lang['export'] : 'Exportar'; ?>
                    </button>
                </div>
            </div>
        </div>
    </div>

    <script>
        // Toggle para opciones avanzadas
        document.addEventListener('DOMContentLoaded', function() {
            const toggleAdvanced = document.getElementById('toggleAdvanced');
            const advancedOptions = document.querySelector('.advanced-options-content');
            
            toggleAdvanced.addEventListener('click', function(e) {
                e.preventDefault();
                
                if (advancedOptions.style.display === 'none') {
                    advancedOptions.style.display = 'block';
                    toggleAdvanced.innerHTML = '<?php echo isset($lang['hide_advanced']) ? $lang['hide_advanced'] : 'Ocultar opciones avanzadas'; ?> <i class="fas fa-chevron-up"></i>';
                } else {
                    advancedOptions.style.display = 'none';
                    toggleAdvanced.innerHTML = '<?php echo isset($lang['advanced_options']) ? $lang['advanced_options'] : 'Opciones avanzadas'; ?> <i class="fas fa-chevron-down"></i>';
                }
            });
        });

        // Función para exportar resultados (simulada)
        function exportResults() {
            // Crear menú de opciones de exportación
            const exportOptions = document.createElement('div');
            exportOptions.className = 'export-options';
            exportOptions.innerHTML = `
                <div class="export-menu">
                    <div class="export-title"><?php echo isset($lang['export_as']) ? $lang['export_as'] : 'Exportar como'; ?>:</div>
                    <button class="export-option" data-type="excel">
                        <i class="fas fa-file-excel"></i> <?php echo isset($lang['excel']) ? $lang['excel'] : 'Excel'; ?>
                    </button>
                    <button class="export-option" data-type="pdf">
                        <i class="fas fa-file-pdf"></i> <?php echo isset($lang['pdf']) ? $lang['pdf'] : 'PDF'; ?>
                    </button>
                    <button class="export-option" data-type="csv">
                        <i class="fas fa-file-csv"></i> <?php echo isset($lang['csv']) ? $lang['csv'] : 'CSV'; ?>
                    </button>
                </div>
            `;
            
            // Añadir al DOM
            document.body.appendChild(exportOptions);
            
            // Posicionar el menú
            const btnExport = document.querySelector('.btn-export');
            const rect = btnExport.getBoundingClientRect();
            exportOptions.style.position = 'absolute';
            exportOptions.style.top = (rect.bottom + window.scrollY) + 'px';
            exportOptions.style.right = (document.body.clientWidth - rect.right) + 'px';
            
            // Añadir eventos a los botones
            const exportButtons = document.querySelectorAll('.export-option');
            exportButtons.forEach(button => {
                button.addEventListener('click', function() {
                    const type = this.getAttribute('data-type');
                    alert('<?php echo isset($lang['exporting']) ? $lang['exporting'] : 'Exportando'; ?> ' + type);
                    exportOptions.remove();
                });
            });
            
            // Cerrar el menú al hacer clic fuera
            document.addEventListener('click', function closeMenu(e) {
                if (!exportOptions.contains(e.target) && e.target !== btnExport) {
                    exportOptions.remove();
                    document.removeEventListener('click', closeMenu);
                }
            });
        }
    </script>
</body>
</html>