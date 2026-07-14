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
        Schema::create('mantenimientos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('maquina_id')->constrained('maquinas');
            $table->enum('tipo', ['Preventivo', 'Correctivo']);
            $table->date('fecha_programada')->nullable();
            $table->date('fecha_realizada')->nullable();
            $table->text('descripcion');
            $table->foreignId('realizado_por')->nullable()->constrained('users');
            $table->decimal('costo', 12, 2)->nullable();
            $table->integer('horas_parada')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('mantenimientos');
    }
};
