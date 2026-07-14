<?php

require_once 'vendor/autoload.php';

$app = require_once 'bootstrap/app.php';
$app->make(Illuminate\Contracts\Console\Kernel::class)->bootstrap();

// Crear producto de prueba si no existe
$producto = App\Models\Producto::first();
if (!$producto) {
    $producto = new App\Models\Producto();
    $producto->codigo = 'PROD-TEST';
    $producto->nombre = 'Producto de Prueba';
    $producto->descripcion = 'Producto para testing';
    $producto->unidad_medida = 'kg';
    $producto->save();
    echo "Producto de prueba creado\n";
}

// Crear receta de prueba si no existe
$receta = App\Models\Receta::first();
if (!$receta) {
    $receta = new App\Models\Receta();
    $receta->producto_id = $producto->id;
    $receta->nombre_receta = 'Receta de Prueba';
    $receta->version = 1;
    $receta->rendimiento_porcentual = 98.5;
    $receta->activo = true;
    $receta->save();
    echo "Receta de prueba creada\n";
}

$maquina = App\Models\Maquina::first();
$operador = App\Models\User::first();

if ($maquina && $receta && $operador) {
    // Crear producción manualmente
    $produccion = new App\Models\Produccion();
    $produccion->numero_orden = 'TEST-' . time();
    $produccion->maquina_id = $maquina->id;
    $produccion->operador_id = $operador->id;
    $produccion->receta_id = $receta->id;
    $produccion->fecha_inicio = now();
    $produccion->fecha_fin = now();
    $produccion->turno = 'Diurno';
    $produccion->cantidad_producida_kg = 100;
    $produccion->estado = 'Finalizada';
    $produccion->oee_actual = 85.5;
    $produccion->velocidad_actual = 90.2;
    $produccion->save();

    echo "Producción registrada para máquina: " . $maquina->nombre . "\n";
    echo "Receta: " . $receta->nombre_receta . "\n";
    echo "Operador: " . $operador->name . "\n";
} else {
    echo "Faltan datos requeridos:\n";
    echo "Máquina: " . ($maquina ? 'OK' : 'FALTA') . "\n";
    echo "Receta: " . ($receta ? 'OK' : 'FALTA') . "\n";
    echo "Usuario: " . ($operador ? 'OK' : 'FALTA') . "\n";
}