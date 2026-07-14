<?php

namespace App\Models;

use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
use Laravel\Sanctum\HasApiTokens;
use Spatie\Permission\Traits\HasRoles;
use Illuminate\Database\Eloquent\Factories\HasFactory;

class User extends Authenticatable
{
    use HasApiTokens, HasRoles, Notifiable, HasFactory;

    protected $fillable = [
        'name', 'email', 'password', 'activo', 'foto_perfil',
    ];

    protected $hidden = ['password', 'remember_token'];

    protected $casts = [
        'email_verified_at' => 'datetime',
        'activo' => 'boolean',
    ];

    // Relaciones
    public function produccionesOperador()
    {
        return $this->hasMany(Produccion::class, 'operador_id');
    }

    public function produccionesEncargado()
    {
        return $this->hasMany(Produccion::class, 'encargado_id');
    }
}
