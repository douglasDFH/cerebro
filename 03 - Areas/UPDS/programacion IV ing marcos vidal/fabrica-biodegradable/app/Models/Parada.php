<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Parada extends Model
{
    protected $fillable = [
        'maquina_id', 'produccion_id', 'inicio', 'fin', 'motivo',
    ];

    protected $casts = [
        'inicio' => 'datetime',
        'fin' => 'datetime',
    ];

    public function maquina()
    {
        return $this->belongsTo(Maquina::class);
    }

    public function produccion()
    {
        return $this->belongsTo(Produccion::class);
    }
}
