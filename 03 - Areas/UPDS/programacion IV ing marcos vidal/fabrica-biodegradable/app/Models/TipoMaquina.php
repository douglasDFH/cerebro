<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class TipoMaquina extends Model
{
    protected $table = 'tipos_maquinas';

    protected $fillable = ['nombre', 'descripcion'];

    public function maquinas()
    {
        return $this->hasMany(Maquina::class);
    }
}
