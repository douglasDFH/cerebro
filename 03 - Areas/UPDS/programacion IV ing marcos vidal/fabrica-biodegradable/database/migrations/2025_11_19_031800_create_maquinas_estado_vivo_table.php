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
        Schema::create('maquinas_estado_vivo', function (Blueprint $table) {
            $table->foreignId('maquina_id')->primary()->constrained('maquinas');
            $table->foreignId('produccion_id')->nullable()->constrained('producciones');
            $table->decimal('kg_producidos', 12, 3)->default(0);
            $table->decimal('piezas_producidas', 12, 0)->default(0);
            $table->decimal('metros_producidos', 12, 3)->default(0);
            $table->decimal('velocidad_actual', 10, 2)->default(0);
            $table->decimal('velocidad_fast', 10, 2)->default(2000);
            $table->decimal('velocidad_slow', 10, 2)->default(400);
            $table->decimal('tiempo_ciclo_actual', 8, 3)->default(0);
            $table->decimal('tiempo_ciclo_teorico', 8, 3)->default(0);
            $table->decimal('oee_actual', 5, 2)->default(0);
            $table->enum('estado', ['Parada', 'Corriendo', 'Alarma', 'Mantenimiento'])->default('Parada');
            $table->dateTime('ultima_actualizacion')->useCurrent()->useCurrentOnUpdate();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('maquinas_estado_vivo');
    }
};
