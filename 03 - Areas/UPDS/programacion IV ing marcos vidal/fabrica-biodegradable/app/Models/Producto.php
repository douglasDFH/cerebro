<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Producto extends Model
{
    protected $fillable = [
        'codigo', 'nombre', 'descripcion', 'unidad_medida',
        'stock_actual', 'precio_venta_promedio', 'activo',
    ];

    protected $casts = [
        'activo' => 'boolean',
    ];

    public function recetas()
    {
        return $this->hasMany(Receta::class);
    }
}
