<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MaquinaEstadoVivo extends Model
{
    public $incrementing = false; // PK es maquina_id

    protected $primaryKey = 'maquina_id';

    protected $table = 'maquinas_estado_vivo';

    protected $fillable = [
        'maquina_id', 'produccion_id', 'kg_producidos', 'piezas_producidas',
        'metros_producidos', 'velocidad_actual', 'velocidad_fast', 'velocidad_slow',
        'tiempo_ciclo_actual', 'tiempo_ciclo_teorico', 'oee_actual', 'estado',
    ];

    protected $casts = [
        'ultima_actualizacion' => 'datetime',
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
