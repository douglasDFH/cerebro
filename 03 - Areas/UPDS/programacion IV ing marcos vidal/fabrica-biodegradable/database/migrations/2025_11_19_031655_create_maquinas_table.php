<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('maquinas', function (Blueprint $table) {
            $table->id();
            $table->string('codigo')->unique(); // ARB320
            $table->string('nombre'); // Arburg 320C
            $table->foreignId('tipo_maquina_id')->constrained('tipos_maquinas');
            $table->date('fecha_compra')->nullable();
            $table->decimal('horas_trabajadas', 12, 2)->default(0);
            $table->enum('estado', ['Operativa', 'Mantenimiento', 'Parada', 'Baja'])->default('Operativa');
            $table->string('ubicacion_planta')->nullable();
            $table->string('foto')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('maquinas');
    }
};
