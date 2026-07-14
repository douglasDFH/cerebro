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
        Schema::create('produccion_consumos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('produccion_id')->constrained('producciones')->cascadeOnDelete();
            $table->foreignId('lote_materia_prima_id')->constrained('lotes_materia_prima');
            $table->decimal('cantidad_consumida_kg', 12, 3);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('produccion_consumos');
    }
};
