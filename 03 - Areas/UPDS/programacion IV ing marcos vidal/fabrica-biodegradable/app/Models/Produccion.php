<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Produccion extends Model
{
    protected $table = 'producciones';

    protected $fillable = [
        'numero_orden', 'maquina_id', 'operador_id', 'encargado_id',
        'receta_id', 'fecha_inicio', 'fecha_fin', 'turno',
        'cantidad_objetivo_kg', 'cantidad_producida_kg', 'estado', 'observaciones',
        'oee_actual', 'velocidad_actual',
    ];

    protected $casts = [
        'fecha_inicio' => 'datetime',
        'fecha_fin' => 'datetime',
    ];

    public function maquina()
    {
        return $this->belongsTo(Maquina::class);
    }

    public function operador()
    {
        return $this->belongsTo(User::class, 'operador_id');
    }

    public function encargado()
    {
        return $this->belongsTo(User::class, 'encargado_id');
    }

    public function receta()
    {
        return $this->belongsTo(Receta::class);
    }

    public function consumos()
    {
        return $this->hasMany(ProduccionConsumo::class);
    }

    public function loteProducto()
    {
        return $this->hasOne(LoteProducto::class);
    }

    public function paradas()
    {
        return $this->hasMany(Parada::class);
    }
}
