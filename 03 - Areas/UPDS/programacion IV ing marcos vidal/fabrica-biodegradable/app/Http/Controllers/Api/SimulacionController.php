<?php

namespace App\Http\Controllers\Api;

use App\Events\MaquinaEstadoActualizado;
use App\Http\Controllers\Controller;
use App\Models\Maquina;
use App\Models\MaquinaEstadoVivo;
use App\Models\Produccion;
use App\Services\Contracts\ProduccionServiceInterface;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Log;

class SimulacionController extends Controller
{
    protected ProduccionServiceInterface $produccionService;

    public function __construct(ProduccionServiceInterface $produccionService)
    {
        $this->produccionService = $produccionService;
    }

    public function simularProduccion(Request $request)
    {
        Log::info('Datos recibidos en simularProduccion:', $request->all());

        try {
            $request->validate([
                'producciones' => 'required|array',
                'producciones.*.maquina_id' => 'required|integer|exists:maquinas,id',
                'producciones.*.kg_incremento' => 'required|numeric|min:0',
                'producciones.*.oee' => 'required|numeric|min:0|max:100',
                'producciones.*.velocidad' => 'required|numeric|min:0',
            ]);
        } catch (\Illuminate\Validation\ValidationException $e) {
            Log::error('Errores de validación:', $e->errors());

            return response()->json(['errors' => $e->errors()], 422);
        }

        $resultados = [];

        foreach ($request->producciones as $prod) {
            $resultado = $this->produccionService->registrarProduccion(
                $prod['maquina_id'],
                $prod['kg_incremento'],
                $prod['oee'],
                $prod['velocidad']
            );
            $resultados[] = $resultado;
        }

        return response()->json([
            'success' => true,
            'message' => 'Producciones simuladas exitosamente',
            'resultados' => $resultados,
        ]);
    }
}
