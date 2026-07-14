/**
 * Funciones de impresión mejoradas para Banco Mercantil
 * Fecha: 08/03/2025
 * Autor: Departamento de Sistemas
 * Descripción: Provee funcionalidad mejorada para la impresión de documentos
 */

/**
 * Función mejorada para imprimir documentos
 * @param {string} elementId - ID del elemento a imprimir (opcional)
 * @param {string} titulo - Título del documento a imprimir
 */
function imprimirDocumento(elementId, titulo) {
    // Guardar el estado actual
    const estadoOriginal = document.body.innerHTML;
    
    try {
        // Si se proporciona un ID específico, solo imprimir ese elemento
        if (elementId) {
            const elemento = document.getElementById(elementId);
            if (!elemento) {
                console.error('Elemento no encontrado:', elementId);
                return;
            }
            
            // Crear una ventana nueva con el contenido
            const ventanaImpresion = window.open('', '_blank');
            if (!ventanaImpresion) {
                alert('Por favor, permita las ventanas emergentes para imprimir.');
                return;
            }
            
            // Título por defecto si no se proporciona
            const tituloDoc = titulo || 'Banco Mercantil - Documento';
            
            // Agregar estilos y contenido
            ventanaImpresion.document.write(`
                <!DOCTYPE html>
                <html lang="es">
                <head>
                    <meta charset="UTF-8">
                    <title>${tituloDoc}</title>
                    <style>
                        body { 
                            font-family: Arial, sans-serif;
                            padding: 20px;
                            margin: 0;
                            color: #333;
                            line-height: 1.5;
                        }
                        .print-header {
                            text-align: center;
                            margin-bottom: 20px;
                            border-bottom: 1px solid #ddd;
                            padding-bottom: 10px;
                        }
                        .print-header h1 {
                            font-size: 20px;
                            margin: 0 0 5px 0;
                            color: #056f1f;
                        }
                        .print-header p {
                            font-size: 12px;
                            margin: 0;
                            color: #666;
                        }
                        .print-footer {
                            text-align: center;
                            margin-top: 20px;
                            border-top: 1px solid #ddd;
                            padding-top: 10px;
                            font-size: 10px;
                            color: #666;
                        }
                        table {
                            width: 100%;
                            border-collapse: collapse;
                            margin-bottom: 20px;
                        }
                        th, td {
                            padding: 8px;
                            text-align: left;
                            border: 1px solid #ddd;
                        }
                        th {
                            background-color: #f3f3f3;
                        }
                        .badge {
                            padding: 3px 6px;
                            border-radius: 4px;
                            font-size: 12px;
                            font-weight: normal;
                            border: 1px solid #ddd;
                            background-color: transparent;
                        }
                        /* Ocultar elementos que no deben imprimirse */
                        .no-print, button, .btn, .btn-group {
                            display: none !important;
                        }
                        .container, .card {
                            width: 100%;
                            margin: 0;
                            padding: 0;
                            border: none;
                            box-shadow: none;
                        }
                        .card-header {
                            background-color: #f3f3f3;
                            padding: 10px;
                            margin-bottom: 15px;
                            border-bottom: 1px solid #ddd;
                        }
                        .card-body {
                            padding: 10px 0;
                        }
                        h2, h3, h4 {
                            color: #056f1f;
                            margin-top: 20px;
                            margin-bottom: 10px;
                        }
                        .row {
                            display: block;
                            margin: 0;
                        }
                        .col-md-6, .col {
                            width: 100%;
                            float: none;
                            margin-bottom: 15px;
                        }
                    </style>
                </head>
                <body>
                    <div class="print-header">
                        <h1>${tituloDoc}</h1>
                        <p>Fecha de impresión: ${new Date().toLocaleDateString()} ${new Date().toLocaleTimeString()}</p>
                    </div>
                    <div class="print-content">
                        ${elemento.innerHTML}
                    </div>
                    <div class="print-footer">
                        <p>&copy; ${new Date().getFullYear()} Banco Mercantil Santa Cruz S.A.</p>
                        <p>Este documento es una impresión del sistema y no tiene validez sin sello y firma autorizada.</p>
                    </div>
                </body>
                </html>
            `);
            
            ventanaImpresion.document.close();
            
            // Esperar a que cargue todo el contenido
            ventanaImpresion.onload = function() {
                setTimeout(function() {
                    ventanaImpresion.focus();
                    ventanaImpresion.print();
                    // No cerrar la ventana para permitir múltiples impresiones
                }, 500);
            };
        } else {
            // Si no se proporciona un ID, aplicar estilos de impresión al documento actual
            
            // Crear encabezado y pie de página de impresión
            const printHeader = document.createElement('div');
            printHeader.className = 'print-header d-none d-print-block';
            printHeader.innerHTML = `
                <h1>Banco Mercantil</h1>
                <p>Fecha de impresión: ${new Date().toLocaleDateString()} ${new Date().toLocaleTimeString()}</p>
            `;
            
            const printFooter = document.createElement('div');
            printFooter.className = 'print-footer d-none d-print-block';
            printFooter.innerHTML = `
                <p>&copy; ${new Date().getFullYear()} Banco Mercantil Santa Cruz S.A.</p>
                <p>Este documento es una impresión del sistema y no tiene validez sin sello y firma autorizada.</p>
            `;
            
            // Agregar al documento
            document.body.insertBefore(printHeader, document.body.firstChild);
            document.body.appendChild(printFooter);
            
            // Ejecutar la impresión
            window.print();
            
            // Eliminar los elementos agregados
            document.body.removeChild(printHeader);
            document.body.removeChild(printFooter);
        }
    } catch (error) {
        console.error('Error al imprimir:', error);
        alert('Ocurrió un error al intentar imprimir. Por favor, inténtelo de nuevo.');
    }
}

/**
 * Función para imprimir un comprobante específico
 * @param {number} idTransaccion - ID de la transacción
 */
function imprimirComprobante(idTransaccion) {
    window.open(`index.php?controller=transaccion&action=comprobante&id=${idTransaccion}&print=true`, '_blank');
}

/**
 * Función para imprimir un reporte de transacciones por fecha
 * @param {string} fechaInicio - Fecha de inicio (YYYY-MM-DD)
 * @param {string} fechaFin - Fecha de fin (YYYY-MM-DD)
 */
function imprimirReporteTransacciones(fechaInicio, fechaFin) {
    window.open(`index.php?controller=reporte&action=imprimirTransaccionesFecha&inicio=${fechaInicio}&fin=${fechaFin}`, '_blank');
}

/**
 * Función para imprimir un extracto de cuenta
 * @param {number} idCuenta - ID de la cuenta
 * @param {string} fechaInicio - Fecha de inicio (YYYY-MM-DD)
 * @param {string} fechaFin - Fecha de fin (YYYY-MM-DD)
 */
function imprimirExtractoCuenta(idCuenta, fechaInicio, fechaFin) {
    let url = `index.php?controller=cuenta&action=imprimirExtracto&id=${idCuenta}`;
    if (fechaInicio && fechaFin) {
        url += `&fechaInicio=${fechaInicio}&fechaFin=${fechaFin}`;
    }
    window.open(url, '_blank');
}

// Detectar cuando la página se carga completamente
document.addEventListener('DOMContentLoaded', function() {
    // Buscar todos los botones de impresión y agregarles la funcionalidad
    const botonesImprimir = document.querySelectorAll('.btn-print, .print-button, button[onclick*="print"]');
    
    botonesImprimir.forEach(boton => {
        // Reemplazar el evento onclick existente
        boton.onclick = function(e) {
            e.preventDefault();
            
            // Determinar qué elemento imprimir basado en el contexto
            let elementoImprimir = null;
            let titulo = 'Banco Mercantil - Documento';
            
            // Buscar el contenedor padre más cercano
            const card = this.closest('.card');
            if (card) {
                // Priorizar un div con ID específico para impresión
                const printSection = card.querySelector('[id$="-print"], [id$="_print"], .print-section');
                if (printSection) {
                    elementoImprimir = printSection.id;
                    // Extraer título del encabezado de la tarjeta si existe
                    const cardHeader = card.querySelector('.card-header');
                    if (cardHeader) {
                        titulo = cardHeader.textContent.trim();
                    }
                } else {
                    // Usar el cuerpo de la tarjeta
                    const cardBody = card.querySelector('.card-body');
                    if (cardBody && cardBody.id) {
                        elementoImprimir = cardBody.id;
                    } else if (card.id) {
                        elementoImprimir = card.id;
                    }
                }
            }
            
            // Si no se encontró un ID específico, imprimir toda la página
            imprimirDocumento(elementoImprimir, titulo);
        };
    });
});