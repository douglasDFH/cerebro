<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MateriaPrima extends Model
{
    protected $fillable = [
        'codigo', 'nombre', 'proveedor_id', 'unidad_medida',
        'stock_actual', 'stock_minimo', 'precio_promedio_kg', 'activo',
    ];

    protected $casts = [
        'activo' => 'boolean',
    ];

    public function proveedor()
    {
        return $this->belongsTo(Proveedor::class);
    }

    public function lotes()
    {
        return $this->hasMany(LoteMateriaPrima::class);
    }

    public function recetaDetalles()
    {
        return $this->hasMany(RecetaDetalle::class);
    }
}
