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
        Schema::create('producciones', function (Blueprint $table) {
            $table->id();
            $table->string('numero_orden')->unique();
            $table->foreignId('maquina_id')->constrained('maquinas');
            $table->foreignId('operador_id')->constrained('users');
            $table->foreignId('encargado_id')->nullable()->constrained('users');
            $table->foreignId('receta_id')->constrained('recetas');
            $table->dateTime('fecha_inicio')->nullable();
            $table->dateTime('fecha_fin')->nullable();
            $table->string('turno');
            $table->decimal('cantidad_objetivo_kg', 12, 3)->nullable();
            $table->decimal('cantidad_producida_kg', 12, 3)->default(0);
            $table->enum('estado', ['Planificada', 'EnCurso', 'Finalizada', 'Cancelada', 'Pausada'])->default('Planificada');
            $table->text('observaciones')->nullable();
            $table->decimal('oee_actual', 5, 2)->nullable();
            $table->decimal('velocidad_actual', 8, 2)->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('producciones');
    }
};
