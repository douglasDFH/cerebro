<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Receta extends Model
{
    protected $fillable = [
        'producto_id', 'nombre_receta', 'version', 'rendimiento_porcentual',
        'activo', 'aprobada_por', 'fecha_aprobacion',
    ];

    protected $casts = [
        'fecha_aprobacion' => 'date',
        'activo' => 'boolean',
    ];

    public function producto()
    {
        return $this->belongsTo(Producto::class);
    }

    public function detalles()
    {
        return $this->hasMany(RecetaDetalle::class);
    }

    public function producciones()
    {
        return $this->hasMany(Produccion::class);
    }
}
