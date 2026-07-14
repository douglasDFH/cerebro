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
        Schema::create('receta_detalles', function (Blueprint $table) {
            $table->foreignId('receta_id')->constrained('recetas')->cascadeOnDelete();
            $table->foreignId('materia_prima_id')->constrained('materias_primas');
            $table->decimal('cantidad_por_kg', 8, 4);
            $table->decimal('porcentaje', 6, 3)->nullable();
            $table->primary(['receta_id', 'materia_prima_id']);
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('receta_detalles');
    }
};
