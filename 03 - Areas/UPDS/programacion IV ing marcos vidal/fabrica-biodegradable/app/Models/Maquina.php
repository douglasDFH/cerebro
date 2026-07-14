<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Maquina extends Model
{
    protected $fillable = [
        'codigo', 'nombre', 'tipo_maquina_id', 'fecha_compra',
        'horas_trabajadas', 'estado', 'ubicacion_planta', 'foto',
    ];

    public function tipo()
    {
        return $this->belongsTo(TipoMaquina::class, 'tipo_maquina_id');
    }

    public function producciones()
    {
        return $this->hasMany(Produccion::class);
    }

    public function estadoVivo()
    {
        return $this->hasOne(MaquinaEstadoVivo::class);
    }

    public function paradas()
    {
        return $this->hasMany(Parada::class);
    }

    public function mantenimientos()
    {
        return $this->hasMany(Mantenimiento::class);
    }
}
