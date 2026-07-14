<?php
// exportar_pdf.php
require_once('../vendor/tcpdf/TCPDF-main/tcpdf.php');
include("../../bd.php");

// Obtener el ID del pedido
$order_id = isset($_GET['order_id']) ? (int)$_GET['order_id'] : 0;

if ($order_id <= 0) {
    header("Location: index.php?error=" . urlencode("ID de pedido no válido"));
    exit();
}

// Obtener información del pedido y cliente
$stmt_order = $conexion->prepare("SELECT o.order_id, o.order_date, 
                                 c.customer_id, c.first_name, c.last_name, c.email, c.phone, c.city, c.state, c.street
                                 FROM orders o 
                                 INNER JOIN customers c ON o.customer_id = c.customer_id 
                                 WHERE o.order_id = :order_id");
$stmt_order->bindParam(':order_id', $order_id);
$stmt_order->execute();
$pedido_info = $stmt_order->fetch(PDO::FETCH_ASSOC);

if (!$pedido_info) {
    header("Location: index.php?error=" . urlencode("Pedido no encontrado"));
    exit();
}

// Obtener items del pedido
$stmt_items = $conexion->prepare("SELECT oi.order_item_id, oi.product_id, oi.quantity, oi.price, oi.discount,
                                 p.product_name, p.foto, p.model_year,
                                 (oi.quantity * oi.price) as subtotal,
                                 (oi.quantity * oi.price * oi.discount) as descuento_total,
                                 (oi.quantity * oi.price * (1 - oi.discount)) as total_item
                                 FROM order_items oi 
                                 INNER JOIN products p ON oi.product_id = p.product_id 
                                 WHERE oi.order_id = :order_id 
                                 ORDER BY oi.order_item_id");
$stmt_items->bindParam(':order_id', $order_id);
$stmt_items->execute();
$items_pedido = $stmt_items->fetchAll(PDO::FETCH_ASSOC);

// Calcular totales
$total_items = count($items_pedido);
$total_cantidad = 0;
$subtotal_pedido = 0;
$descuento_total_pedido = 0;
$total_pedido = 0;

foreach ($items_pedido as $item) {
    $total_cantidad += $item['quantity'];
    $subtotal_pedido += $item['subtotal'];
    $descuento_total_pedido += $item['descuento_total'];
    $total_pedido += $item['total_item'];
}

// Crear nuevo PDF
class PedidoPDF extends TCPDF {
    public function Header() {
        // Logo de la empresa (opcional)
        // $this->Image('../../assets/logo.png', 15, 10, 30, '', 'PNG', '', 'T', false, 300, '', false, false, 0, false, false, false);
        
        // Título
        $this->SetFont('helvetica', 'B', 20);
        $this->SetTextColor(0, 102, 204);
        $this->Cell(0, 15, 'FACTURA DE PEDIDO', 0, false, 'C', 0, '', 0, false, 'M', 'M');
        $this->Ln(10);
        
        // Línea separadora
        $this->SetDrawColor(0, 102, 204);
        $this->Line(15, 35, 195, 35);
    }
    
    public function Footer() {
        $this->SetY(-25);
        $this->SetFont('helvetica', 'I', 8);
        $this->SetTextColor(128, 128, 128);
        $this->Cell(0, 10, 'Página ' . $this->getAliasNumPage() . '/' . $this->getAliasNbPages(), 0, false, 'C', 0, '', 0, false, 'T', 'M');
        $this->Ln(5);
        $this->Cell(0, 10, 'Generado el: ' . date('d/m/Y H:i:s'), 0, false, 'C', 0, '', 0, false, 'T', 'M');
    }
}

// Crear instancia del PDF
$pdf = new PedidoPDF(PDF_PAGE_ORIENTATION, PDF_UNIT, PDF_PAGE_FORMAT, true, 'UTF-8', false);

// Configurar documento
$pdf->SetCreator('Sistema de Pedidos');
$pdf->SetAuthor('Tu Empresa');
$pdf->SetTitle('Pedido #' . str_pad($order_id, 4, '0', STR_PAD_LEFT));
$pdf->SetSubject('Factura de Pedido');

// Configurar márgenes
$pdf->SetMargins(15, 45, 15);
$pdf->SetHeaderMargin(5);
$pdf->SetFooterMargin(15);

// Agregar página
$pdf->AddPage();

// Información del pedido
$pdf->SetFont('helvetica', 'B', 16);
$pdf->SetTextColor(0, 0, 0);
$pdf->Cell(0, 10, 'PEDIDO #' . str_pad($order_id, 4, '0', STR_PAD_LEFT), 0, 1, 'L');
$pdf->Ln(5);

// Información en dos columnas
$pdf->SetFont('helvetica', '', 10);

// Columna izquierda - Información del cliente
$pdf->SetXY(15, 60);
$pdf->SetFont('helvetica', 'B', 12);
$pdf->SetFillColor(240, 240, 240);
$pdf->Cell(85, 8, 'INFORMACIÓN DEL CLIENTE', 1, 1, 'C', true);

$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(20, 6, 'Nombre:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(65, 6, $pedido_info['first_name'] . ' ' . $pedido_info['last_name'], 0, 1, 'L');

$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(20, 6, 'Email:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(65, 6, $pedido_info['email'], 0, 1, 'L');

$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(20, 6, 'Teléfono:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(65, 6, $pedido_info['phone'], 0, 1, 'L');

$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(20, 6, 'Dirección:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(65, 6, $pedido_info['street'], 0, 1, 'L');

$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(20, 6, 'Ciudad:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(65, 6, $pedido_info['city'] . ', ' . $pedido_info['state'], 0, 1, 'L');

// Columna derecha - Información del pedido
$pdf->SetXY(110, 60);
$pdf->SetFont('helvetica', 'B', 12);
$pdf->SetFillColor(240, 240, 240);
$pdf->Cell(85, 8, 'INFORMACIÓN DEL PEDIDO', 1, 1, 'C', true);

$pdf->SetXY(110, 68);
$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(30, 6, 'Fecha:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(55, 6, date('d/m/Y', strtotime($pedido_info['order_date'])), 0, 1, 'L');

$pdf->SetXY(110, 74);
$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(30, 6, 'Total Items:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(55, 6, $total_items, 0, 1, 'L');

$pdf->SetXY(110, 80);
$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(30, 6, 'Cantidad Total:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->Cell(55, 6, number_format($total_cantidad, 2), 0, 1, 'L');

$pdf->SetXY(110, 86);
$pdf->SetFont('helvetica', 'B', 10);
$pdf->Cell(30, 6, 'Estado:', 0, 0, 'L');
$pdf->SetFont('helvetica', '', 10);
$pdf->SetTextColor(0, 150, 0);
$pdf->Cell(55, 6, 'CONFIRMADO', 0, 1, 'L');

// Espacio
$pdf->Ln(15);

// Tabla de productos
$pdf->SetTextColor(0, 0, 0);
$pdf->SetFont('helvetica', 'B', 10);
$pdf->SetFillColor(0, 102, 204);
$pdf->SetTextColor(255, 255, 255);

// Cabecera de la tabla
$pdf->Cell(15, 8, 'ID', 1, 0, 'C', true);
$pdf->Cell(60, 8, 'PRODUCTO', 1, 0, 'C', true);
$pdf->Cell(20, 8, 'CANT.', 1, 0, 'C', true);
$pdf->Cell(25, 8, 'P. UNIT.', 1, 0, 'C', true);
$pdf->Cell(20, 8, 'DESC.', 1, 0, 'C', true);
$pdf->Cell(25, 8, 'SUBTOTAL', 1, 0, 'C', true);
$pdf->Cell(25, 8, 'TOTAL', 1, 1, 'C', true);

// Contenido de la tabla
$pdf->SetFont('helvetica', '', 9);
$pdf->SetTextColor(0, 0, 0);
$pdf->SetFillColor(245, 245, 245);

$fill = false;
foreach($items_pedido as $item) {
    $pdf->Cell(15, 8, $item['order_item_id'], 1, 0, 'C', $fill);
    $pdf->Cell(60, 8, substr($item['product_name'], 0, 30), 1, 0, 'L', $fill);
    $pdf->Cell(20, 8, number_format($item['quantity'], 2), 1, 0, 'C', $fill);
    $pdf->Cell(25, 8, 'Bs. ' . number_format($item['price'], 2), 1, 0, 'R', $fill);
    $pdf->Cell(20, 8, number_format($item['discount'] * 100, 1) . '%', 1, 0, 'C', $fill);
    $pdf->Cell(25, 8, 'Bs. ' . number_format($item['subtotal'], 2), 1, 0, 'R', $fill);
    $pdf->Cell(25, 8, 'Bs. ' . number_format($item['total_item'], 2), 1, 1, 'R', $fill);
    $fill = !$fill;
}

// Totales
$pdf->Ln(5);
$pdf->SetFont('helvetica', 'B', 10);

// Subtotal
$pdf->Cell(145, 8, 'SUBTOTAL:', 0, 0, 'R');
$pdf->Cell(25, 8, 'Bs. ' . number_format($subtotal_pedido, 2), 1, 1, 'R');

// Descuento
$pdf->SetTextColor(200, 0, 0);
$pdf->Cell(145, 8, 'DESCUENTO:', 0, 0, 'R');
$pdf->Cell(25, 8, '-Bs. ' . number_format($descuento_total_pedido, 2), 1, 1, 'R');

// Total final
$pdf->SetTextColor(0, 150, 0);
$pdf->SetFont('helvetica', 'B', 12);
$pdf->SetFillColor(240, 255, 240);
$pdf->Cell(145, 10, 'TOTAL FINAL:', 0, 0, 'R');
$pdf->Cell(25, 10, 'Bs. ' . number_format($total_pedido, 2), 1, 1, 'R', true);

// Notas adicionales
$pdf->Ln(10);
$pdf->SetTextColor(0, 0, 0);
$pdf->SetFont('helvetica', 'I', 9);
$pdf->Cell(0, 5, 'Notas:', 0, 1, 'L');
$pdf->Cell(0, 5, '• Este documento es una copia de su pedido.', 0, 1, 'L');
$pdf->Cell(0, 5, '• Conserve este documento para futuras referencias.', 0, 1, 'L');
$pdf->Cell(0, 5, '• Para cualquier consulta, contacte con nuestro servicio al cliente.', 0, 1, 'L');

// Generar PDF
$filename = 'Pedido_' . str_pad($order_id, 4, '0', STR_PAD_LEFT) . '_' . date('Ymd_His') . '.pdf';

// Limpiar buffer de salida
ob_clean();

// Configurar headers para descarga
header('Content-Type: application/pdf');
header('Content-Disposition: attachment; filename="' . $filename . '"');
header('Cache-Control: private, must-revalidate, post-check=0, pre-check=0, max-age=1');
header('Pragma: public');
header('Expires: Sat, 26 Jul 1997 05:00:00 GMT');
header('Last-Modified: ' . gmdate('D, d M Y H:i:s') . ' GMT');

// Salida del PDF
$pdf->Output($filename, 'D');
exit();
?>