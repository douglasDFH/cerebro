<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class RecetaDetalle extends Model
{
    public $incrementing = false;

    protected $primaryKey = ['receta_id', 'materia_prima_id'];

    protected $fillable = [
        'receta_id', 'materia_prima_id', 'cantidad_por_kg', 'porcentaje',
    ];

    protected function setKeysForSaveQuery($query)
    {
        return $query->where('receta_id', $this->receta_id)
            ->where('materia_prima_id', $this->materia_prima_id);
    }

    public function receta()
    {
        return $this->belongsTo(Receta::class);
    }

    public function materiaPrima()
    {
        return $this->belongsTo(MateriaPrima::class);
    }
}
