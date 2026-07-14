<?php

namespace App\Http\Controllers;

use App\Models\Produccion;
use App\Models\Maquina;
use App\Models\MaquinaEstadoVivo;
use App\Services\Contracts\ProduccionServiceInterface;
use Illuminate\Http\Request;
use Carbon\Carbon;
use Inertia\Inertia;

class DashboardController extends Controller
{
    protected ProduccionServiceInterface $produccionService;

    public function __construct(ProduccionServiceInterface $produccionService)
    {
        $this->produccionService = $produccionService;
    }

    public function index()
    {
        $estadisticas = $this->produccionService->getEstadisticasDia();
        $produccionPorMaquina = $this->produccionService->getProduccionPorMaquina();

        // Estado actual de máquinas
        $estadosMaquinas = MaquinaEstadoVivo::with('maquina')->get();

        return Inertia::render('Dashboard', [
            'estadisticas' => $estadisticas,
            'produccionPorMaquina' => $produccionPorMaquina,
            'estadosMaquinas' => $estadosMaquinas,
        ]);
    }

    public function getData()
    {
        $estadisticas = $this->produccionService->getEstadisticasDia();
        $produccionPorMaquina = $this->produccionService->getProduccionPorMaquina();

        // Estado actual de máquinas
        $estadosMaquinas = MaquinaEstadoVivo::with('maquina')->get();

        return response()->json([
            'estadisticas' => $estadisticas,
            'produccionPorMaquina' => $produccionPorMaquina,
            'estadosMaquinas' => $estadosMaquinas,
        ]);
    }
}
