<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class LoteProducto extends Model
{
    protected $fillable = [
        'produccion_id', 'numero_lote', 'fecha_produccion', 'cantidad_kg',
        'calidad', 'ubicacion_almacen', 'qr_code', 'meses_vencimiento',
    ];

    protected $casts = [
        'fecha_produccion' => 'date',
    ];

    public function produccion()
    {
        return $this->belongsTo(Produccion::class);
    }
}
