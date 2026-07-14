<?php

use App\Http\Controllers\Api\SimulacionController;
use App\Http\Controllers\Planta\MonitorMaquinaController;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/user', function (Request $request) {
    return $request->user();
})->middleware('auth:sanctum');

Route::post('/simular-produccion', [SimulacionController::class, 'simularProduccion']);
Route::get('/maquina/{maquina}/estado', [MonitorMaquinaController::class, 'getEstado']);
