<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LoteMateriaPrima extends Model
{
    protected $fillable = [
        'materia_prima_id', 'numero_lote_proveedor', 'fecha_ingreso',
        'fecha_vencimiento', 'cantidad_ingresada', 'cantidad_actual', 'activo',
    ];

    protected $casts = [
        'fecha_ingreso' => 'date',
        'fecha_vencimiento' => 'date',
        'activo' => 'boolean',
    ];

    public function materiaPrima()
    {
        return $this->belongsTo(MateriaPrima::class);
    }

    public function consumos()
    {
        return $this->hasMany(ProduccionConsumo::class);
    }
}
