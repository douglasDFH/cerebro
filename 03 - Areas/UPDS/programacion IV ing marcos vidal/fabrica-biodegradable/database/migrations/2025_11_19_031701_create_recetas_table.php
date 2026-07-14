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
        Schema::create('recetas', function (Blueprint $table) {
            $table->id();
            $table->foreignId('producto_id')->constrained('productos');
            $table->string('nombre_receta');
            $table->integer('version')->default(1);
            $table->decimal('rendimiento_porcentual', 5, 2)->default(98.50);
            $table->boolean('activo')->default(true);
            $table->foreignId('aprobada_por')->nullable()->constrained('users');
            $table->date('fecha_aprobacion')->nullable();
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('recetas');
    }
};
