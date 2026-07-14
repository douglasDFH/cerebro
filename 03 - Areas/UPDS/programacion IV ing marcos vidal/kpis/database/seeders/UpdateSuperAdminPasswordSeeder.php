<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Hash;
use App\Models\User;

class UpdateSuperAdminPasswordSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        // Crear o actualizar SuperAdmin
        $user = User::where('email', 'admin@ecoplast.com')->first();
        
        if (!$user) {
            // Crear si no existe
            User::create([
                'name' => 'SuperAdmin',
                'email' => 'admin@ecoplast.com',
                'password' => Hash::make('123456'),
            ]);
            $this->command->info('✅ SuperAdmin creado exitosamente');
        } else {
            // Actualizar si existe
            $user->update([
                'password' => Hash::make('123456'),
                'updated_at' => now()
            ]);
            $this->command->info('✅ Contraseña del SuperAdmin actualizada');
        }
        
        $this->command->info('📧 Email: admin@ecoplast.com');
        $this->command->info('🔑 Contraseña: 123456');
    }
}
