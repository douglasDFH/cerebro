<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Mantenimiento extends Model
{
    protected $fillable = [
        'maquina_id', 'tipo', 'fecha_programada', 'fecha_realizada',
        'descripcion', 'realizado_por', 'costo', 'horas_parada',
    ];

    protected $casts = [
        'fecha_programada' => 'date',
        'fecha_realizada' => 'date',
    ];

    public function maquina()
    {
        return $this->belongsTo(Maquina::class);
    }

    public function realizadoPor()
    {
        return $this->belongsTo(User::class, 'realizado_por');
    }
}
