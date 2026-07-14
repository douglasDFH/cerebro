<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class ProduccionConsumo extends Model
{
    protected $fillable = [
        'produccion_id', 'lote_materia_prima_id', 'cantidad_consumida_kg',
    ];

    public function produccion()
    {
        return $this->belongsTo(Produccion::class);
    }

    public function loteMateriaPrima()
    {
        return $this->belongsTo(LoteMateriaPrima::class);
    }
}
