<?php

use App\Http\Controllers\Planta\MonitorMaquinaController;
use App\Http\Controllers\DashboardController;
use Illuminate\Support\Facades\Route;

Route::get('/', function () {
    return redirect('/dashboard');
});

Route::get('/dashboard', [DashboardController::class, 'index'])
    ->name('dashboard');

Route::get('/dashboard-data', [DashboardController::class, 'getData'])
    ->name('dashboard.data');

Route::get('/planta/monitor-maquina', [MonitorMaquinaController::class, 'index'])
    ->name('planta.monitor-maquina.index');

Route::get('/planta/monitor-maquina/{maquina}', [MonitorMaquinaController::class, 'show'])
    ->name('planta.monitor-maquina.show');

Route::get('/api/maquina/{maquina}/estado', [MonitorMaquinaController::class, 'getEstado']);
