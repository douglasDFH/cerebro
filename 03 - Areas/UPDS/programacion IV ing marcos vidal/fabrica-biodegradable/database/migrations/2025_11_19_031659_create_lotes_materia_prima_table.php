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
        Schema::create('lotes_materia_prima', function (Blueprint $table) {
            $table->id();
            $table->foreignId('materia_prima_id')->constrained('materias_primas');
            $table->string('numero_lote_proveedor');
            $table->date('fecha_ingreso');
            $table->date('fecha_vencimiento')->nullable();
            $table->decimal('cantidad_ingresada', 12, 3);
            $table->decimal('cantidad_actual', 12, 3);
            $table->boolean('activo')->default(true);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('lotes_materia_prima');
    }
};
