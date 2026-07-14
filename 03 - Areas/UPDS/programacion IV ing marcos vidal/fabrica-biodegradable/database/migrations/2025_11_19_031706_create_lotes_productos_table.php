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
        Schema::create('lotes_productos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('produccion_id')->constrained('producciones');
            $table->string('numero_lote')->unique();
            $table->date('fecha_produccion');
            $table->decimal('cantidad_kg', 12, 3);
            $table->enum('calidad', ['Aprobado', 'Rechazado', 'Reproceso'])->default('Aprobado');
            $table->string('ubicacion_almacen')->nullable();
            $table->string('qr_code')->nullable();
            $table->integer('meses_vencimiento')->default(24);
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('lotes_productos');
    }
};
