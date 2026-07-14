<?php

namespace Database\Seeders;

use App\Models\Maquina;
use App\Models\TipoMaquina;
use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;
use Spatie\Permission\Models\Role;

class FabricaCompletaSeeder extends Seeder
{
    public function run(): void
    {
        // 1. Crear roles
        Role::firstOrCreate(['name' => 'Administrador']);
        Role::firstOrCreate(['name' => 'Gerente']);
        Role::firstOrCreate(['name' => 'EncargadoPlanta']);
        Role::firstOrCreate(['name' => 'Operador']);

        // 2. Crear usuario administrador
        $admin = User::updateOrCreate(
            ['email' => 'admin@fabrica.com'],
            [
                'name' => 'Administrador',
                'password' => Hash::make('12345678'), // cambia después
                'activo' => true,
            ]
        );
        $admin->assignRole('Administrador');

        // 3. Tipos de máquina
        $tipos = ['Inyección', 'Extrusión', 'Soplado', 'Termoformado', 'Granulador'];
        foreach ($tipos as $tipo) {
            TipoMaquina::firstOrCreate(['nombre' => $tipo]);
        }

        // 4. TUS 5 MÁQUINAS REALES
        Maquina::firstOrCreate(
            ['codigo' => 'ARB320'],
            [
                'nombre' => 'Arburg 320C',
                'tipo_maquina_id' => 1,
                'estado' => 'Operativa',
            ]
        );

        Maquina::firstOrCreate(
            ['codigo' => 'KMD75'],
            [
                'nombre' => 'KraussMaffei KMD 75-36',
                'tipo_maquina_id' => 2,
                'estado' => 'Operativa',
            ]
        );

        Maquina::firstOrCreate(
            ['codigo' => 'SID24'],
            [
                'nombre' => 'Sidel SBO 24',
                'tipo_maquina_id' => 3,
                'estado' => 'Operativa',
            ]
        );

        Maquina::firstOrCreate(
            ['codigo' => 'ILL54'],
            [
                'nombre' => 'Illig RDM 54K',
                'tipo_maquina_id' => 4,
                'estado' => 'Operativa',
            ]
        );

        Maquina::firstOrCreate(
            ['codigo' => 'RAP150'],
            [
                'nombre' => 'Rapid 150',
                'tipo_maquina_id' => 5,
                'estado' => 'Operativa',
            ]
        );

        // 5. Crear un registro en maquinas_estado_vivo para cada máquina (para que Reverb funcione desde el inicio)
        foreach (Maquina::all() as $maquina) {
            \App\Models\MaquinaEstadoVivo::updateOrCreate(
                ['maquina_id' => $maquina->id],
                ['estado' => 'Parada', 'kg_producidos' => 0]
            );
        }

        $this->command->info('¡Fábrica biodegradable lista! Usuario: admin@fabrica.com / Contraseña: 12345678');
    }
}
