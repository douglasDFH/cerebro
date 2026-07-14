<?php

require __DIR__.'/vendor/autoload.php';
$app = require_once __DIR__.'/bootstrap/app.php';
$app->make(Illuminate\Contracts\Console\Kernel::class)->bootstrap();

$users = App\Models\User::all();

echo "=== USUARIOS EN BASE DE DATOS ===\n\n";

if ($users->count() === 0) {
    echo "❌ NO HAY USUARIOS\n";
} else {
    foreach ($users as $user) {
        echo "👤 {$user->name}\n";
        echo "   📧 Email: {$user->email}\n";
        echo "   🔑 Password Hash: " . substr($user->password, 0, 20) . "...\n\n";
    }
}

echo "=== CONTRASEÑA POR DEFECTO ===\n";
echo "⚠️  Para TODOS los usuarios: password\n";
