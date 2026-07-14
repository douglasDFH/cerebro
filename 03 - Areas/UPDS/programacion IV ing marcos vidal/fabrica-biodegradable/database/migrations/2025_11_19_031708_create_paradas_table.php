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
        Schema::create('paradas', function (Blueprint $table) {
            $table->id();
            $table->foreignId('maquina_id')->constrained('maquinas');
            $table->foreignId('produccion_id')->nullable()->constrained('producciones');
            $table->dateTime('inicio');
            $table->dateTime('fin')->nullable();
            $table->string('motivo');
            $table->integer('tiempo_minutos')->virtualAs('TIMESTAMPDIFF(MINUTE, inicio, COALESCE(fin, NOW()))');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('paradas');
    }
};
