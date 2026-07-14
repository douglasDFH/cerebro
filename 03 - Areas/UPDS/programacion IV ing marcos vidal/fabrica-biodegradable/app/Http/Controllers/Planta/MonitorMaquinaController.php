<?php

namespace App\Http\Controllers\Planta;

use App\Http\Controllers\Controller;
use App\Models\Maquina;
use Illuminate\Http\Request;
use Inertia\Inertia;

class MonitorMaquinaController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $maquinas = Maquina::with('tipo', 'estadoVivo')->get();

        return Inertia::render('Planta/MonitorMaquinaIndex', [
            'maquinas' => $maquinas,
        ]);
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Maquina $maquina)
    {
        $maquina->load('tipo', 'estadoVivo');

        return Inertia::render('Planta/MonitorMaquinaShow', [
            'maquina' => $maquina,
        ]);
    }

    /**
     * Get the current state of a machine for API polling.
     */
    public function getEstado(Maquina $maquina)
    {
        $estado = $maquina->estadoVivo;

        if (! $estado) {
            return response()->json(null);
        }

        return response()->json([
            'kg_producidos' => $estado->kg_producidos,
            'oee_actual' => $estado->oee_actual,
            'velocidad_actual' => $estado->velocidad_actual,
        ]);
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Maquina $maquina)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, Maquina $maquina)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Maquina $maquina)
    {
        //
    }
}
