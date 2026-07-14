<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Proveedor extends Model
{
    protected $fillable = [
        'nombre', 'nit', 'contacto', 'telefono', 'email', 'direccion',
    ];

    public function materiasPrimas()
    {
        return $this->hasMany(MateriaPrima::class);
    }
}
