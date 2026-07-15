# 📦 ECOPLAST SRL - TODAS LAS MIGRATIONS DE LARAVEL 12

## Total de Migrations: 27 archivos

A continuación encontrarás todas las migrations organizadas en orden.
Copialas a tu proyecto Laravel en: `database/migrations/`

---

## MÓDULO 1: USUARIOS Y AUTENTICACIÓN (4 migrations)

### 2025_01_01_000001_create_roles_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('roles', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_rol', 50)->unique();
            $table->text('descripcion')->nullable();
            $table->enum('nivel_acceso', ['basico', 'intermedio', 'avanzado', 'total'])->default('basico');
            $table->timestamp('created_at')->useCurrent();
            
            $table->index('nombre_rol');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('roles');
    }
};
```

### 2025_01_01_000002_create_usuarios_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('usuarios', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_completo', 100);
            $table->string('email', 100)->unique();
            $table->string('password');
            $table->foreignId('rol_id')->constrained('roles')->onDelete('restrict');
            $table->string('telefono', 20)->nullable();
            $table->string('foto_perfil')->nullable();
            $table->boolean('activo')->default(true);
            $table->timestamp('ultimo_acceso')->nullable();
            $table->rememberToken();
            $table->timestamps();
            
            $table->index('email');
            $table->index('activo');
            $table->index('rol_id');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('usuarios');
    }
};
```

### 2025_01_01_000003_create_turnos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('turnos', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_turno', 50);
            $table->time('hora_inicio');
            $table->time('hora_fin');
            $table->boolean('activo')->default(true);
            
            $table->index('activo');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('turnos');
    }
};
```

### 2025_01_01_000004_create_asignacion_turnos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('asignacion_turnos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('usuario_id')->constrained('usuarios')->onDelete('cascade');
            $table->foreignId('turno_id')->constrained('turnos')->onDelete('restrict');
            $table->date('fecha_asignacion');
            $table->text('observaciones')->nullable();
            
            $table->unique(['usuario_id', 'fecha_asignacion']);
            $table->index('fecha_asignacion');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('asignacion_turnos');
    }
};
```

---

## MÓDULO 2: MATERIAS PRIMAS (3 migrations)

### 2025_01_01_000010_create_categorias_insumos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('categorias_insumos', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_categoria', 100);
            $table->text('descripcion')->nullable();
            $table->boolean('es_biodegradable')->default(true);
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('categorias_insumos');
    }
};
```

### 2025_01_01_000011_create_insumos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('insumos', function (Blueprint $table) {
            $table->id();
            $table->string('codigo_insumo', 50)->unique();
            $table->string('nombre_insumo', 150);
            $table->foreignId('categoria_id')->constrained('categorias_insumos')->onDelete('restrict');
            $table->enum('tipo_material', ['PLA', 'PHA', 'PBS', 'PBAT', 'Almidon', 'Celulosa', 'Aditivo', 'Pigmento', 'Otro']);
            $table->enum('unidad_medida', ['kg', 'ton', 'litro', 'unidad'])->default('kg');
            $table->decimal('densidad', 6, 3)->nullable()->comment('g/cm³');
            $table->decimal('temperatura_fusion', 5, 1)->nullable()->comment('°C');
            $table->string('certificacion_biodegradable', 100)->nullable();
            $table->string('proveedor', 150)->nullable();
            $table->decimal('precio_unitario', 10, 2);
            $table->decimal('stock_minimo', 10, 2);
            $table->decimal('stock_actual', 10, 2)->default(0);
            $table->date('fecha_caducidad_lote')->nullable();
            $table->boolean('activo')->default(true);
            $table->timestamps();
            
            $table->index('codigo_insumo');
            $table->index('activo');
            $table->index('stock_actual');
            $table->index('tipo_material');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('insumos');
    }
};
```

### 2025_01_01_000012_create_movimientos_inventario_insumos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('movimientos_inventario_insumos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('insumo_id')->constrained('insumos')->onDelete('restrict');
            $table->enum('tipo_movimiento', ['entrada', 'salida', 'ajuste', 'desperdicio']);
            $table->decimal('cantidad', 10, 2);
            $table->string('lote', 50)->nullable();
            $table->date('fecha_vencimiento')->nullable();
            $table->decimal('costo_unitario', 10, 2)->nullable();
            $table->foreignId('usuario_id')->constrained('usuarios')->onDelete('restrict');
            $table->text('motivo')->nullable();
            $table->timestamp('fecha_movimiento')->useCurrent();
            
            $table->index('fecha_movimiento');
            $table->index('insumo_id');
            $table->index('tipo_movimiento');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('movimientos_inventario_insumos');
    }
};
```

---

## MÓDULO 3: FORMULACIONES (2 migrations)

### 2025_01_01_000020_create_formulaciones_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('formulaciones', function (Blueprint $table) {
            $table->id();
            $table->string('codigo_formula', 50)->unique();
            $table->string('nombre_formula', 150);
            $table->text('descripcion')->nullable();
            $table->string('version', 20)->default('1.0');
            $table->string('tipo_producto_destino', 100)->nullable();
            $table->decimal('temperatura_procesamiento_min', 5, 1)->nullable()->comment('°C');
            $table->decimal('temperatura_procesamiento_max', 5, 1)->nullable()->comment('°C');
            $table->integer('tiempo_degradacion_estimado')->nullable()->comment('días');
            $table->text('certificaciones')->nullable();
            $table->boolean('aprobado')->default(false);
            $table->timestamp('fecha_aprobacion')->nullable();
            $table->foreignId('usuario_aprueba_id')->nullable()->constrained('usuarios')->onDelete('set null');
            $table->boolean('activo')->default(true);
            $table->timestamp('created_at')->useCurrent();
            
            $table->index('codigo_formula');
            $table->index('aprobado');
            $table->index('activo');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('formulaciones');
    }
};
```

### 2025_01_01_000021_create_componentes_formulacion_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('componentes_formulacion', function (Blueprint $table) {
            $table->id();
            $table->foreignId('formulacion_id')->constrained('formulaciones')->onDelete('cascade');
            $table->foreignId('insumo_id')->constrained('insumos')->onDelete('restrict');
            $table->decimal('porcentaje', 5, 2)->comment('Porcentaje en peso');
            $table->decimal('cantidad_base', 10, 3)->comment('Cantidad para 100kg');
            $table->tinyInteger('orden_adicion')->default(1);
            $table->text('notas')->nullable();
            
            $table->unique(['formulacion_id', 'insumo_id']);
            $table->index('formulacion_id');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('componentes_formulacion');
    }
};
```

---

## MÓDULO 4: MAQUINARIA (4 migrations)

### 2025_01_01_000030_create_tipos_maquina_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('tipos_maquina', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_tipo', 100);
            $table->text('descripcion')->nullable();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('tipos_maquina');
    }
};
```

### 2025_01_01_000031_create_maquinas_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('maquinas', function (Blueprint $table) {
            $table->id();
            $table->string('codigo_maquina', 50)->unique();
            $table->string('nombre_maquina', 150);
            $table->foreignId('tipo_maquina_id')->constrained('tipos_maquina')->onDelete('restrict');
            $table->string('marca', 100)->nullable();
            $table->string('modelo', 100)->nullable();
            $table->year('año_fabricacion')->nullable();
            $table->decimal('capacidad_produccion', 10, 2)->nullable()->comment('unidades o kg por hora');
            $table->string('unidad_capacidad', 20)->default('unidades/hora');
            $table->decimal('consumo_energia_kwh', 8, 2)->nullable();
            $table->decimal('temp_min_operacion', 5, 1)->nullable()->comment('°C');
            $table->decimal('temp_max_operacion', 5, 1)->nullable()->comment('°C');
            $table->decimal('presion_max_bar', 6, 2)->nullable()->comment('Bar');
            $table->decimal('velocidad_max_rpm', 8, 2)->nullable()->comment('RPM');
            $table->decimal('fuerza_cierre_ton', 8, 2)->nullable()->comment('Toneladas');
            $table->decimal('diametro_husillo_mm', 6, 2)->nullable()->comment('mm');
            $table->date('fecha_instalacion')->nullable();
            $table->integer('vida_util_años')->default(15);
            $table->string('ubicacion', 100)->nullable();
            $table->enum('estado_actual', ['operativa', 'mantenimiento', 'parada', 'averia'])->default('operativa');
            $table->boolean('activo')->default(true);
            $table->timestamps();
            
            $table->index('codigo_maquina');
            $table->index('estado_actual');
            $table->index('activo');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('maquinas');
    }
};
```

### 2025_01_01_000032_create_mantenimientos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('mantenimientos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('maquina_id')->constrained('maquinas')->onDelete('restrict');
            $table->enum('tipo_mantenimiento', ['preventivo', 'correctivo', 'predictivo']);
            $table->text('descripcion');
            $table->dateTime('fecha_programada');
            $table->dateTime('fecha_inicio')->nullable();
            $table->dateTime('fecha_fin')->nullable();
            $table->decimal('duracion_horas', 6, 2)->nullable();
            $table->decimal('costo', 10, 2)->nullable();
            $table->foreignId('tecnico_id')->nullable()->constrained('usuarios')->onDelete('set null');
            $table->enum('estado', ['programado', 'en_proceso', 'completado', 'cancelado'])->default('programado');
            $table->enum('prioridad', ['baja', 'media', 'alta', 'critica'])->default('media');
            $table->text('observaciones')->nullable();
            $table->timestamp('created_at')->useCurrent();
            
            $table->index('maquina_id');
            $table->index('fecha_programada');
            $table->index('estado');
            $table->index('tipo_mantenimiento');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('mantenimientos');
    }
};
```

### 2025_01_01_000033_create_paros_maquina_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('paros_maquina', function (Blueprint $table) {
            $table->id();
            $table->foreignId('maquina_id')->constrained('maquinas')->onDelete('restrict');
            $table->enum('tipo_paro', ['averia', 'mantenimiento', 'cambio_molde', 'falta_material', 'falta_personal', 'ajuste_calidad', 'otros']);
            $table->dateTime('fecha_inicio');
            $table->dateTime('fecha_fin')->nullable();
            $table->integer('duracion_minutos')->nullable();
            $table->text('descripcion')->nullable();
            $table->text('causa_raiz')->nullable();
            $table->text('accion_correctiva')->nullable();
            $table->foreignId('operador_id')->constrained('usuarios')->onDelete('restrict');
            $table->decimal('impacto_produccion', 10, 2)->nullable()->comment('unidades no producidas');
            $table->decimal('costo_estimado', 10, 2)->nullable();
            
            $table->index('maquina_id');
            $table->index('fecha_inicio');
            $table->index('tipo_paro');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('paros_maquina');
    }
};
```

---

## MÓDULO 5: PRODUCTOS (3 migrations)

### 2025_01_01_000040_create_categorias_productos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('categorias_productos', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_categoria', 100);
            $table->text('descripcion')->nullable();
            $table->string('aplicacion', 200)->nullable();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('categorias_productos');
    }
};
```

### 2025_01_01_000041_create_productos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('productos', function (Blueprint $table) {
            $table->id();
            $table->string('codigo_producto', 50)->unique();
            $table->string('nombre_producto', 150);
            $table->foreignId('categoria_producto_id')->constrained('categorias_productos')->onDelete('restrict');
            $table->text('descripcion')->nullable();
            $table->enum('material_principal', ['PLA', 'PHA', 'PBS', 'PBAT', 'Almidon', 'Mixto']);
            $table->string('certificacion_compostable', 200)->nullable();
            $table->integer('tiempo_compostaje_dias')->nullable();
            $table->decimal('capacidad_carga_kg', 8, 2)->nullable();
            $table->decimal('peso_unitario_gramos', 8, 2);
            $table->string('dimensiones', 100)->nullable();
            $table->string('color', 50)->default('natural');
            $table->integer('espesor_micras')->nullable();
            $table->foreignId('formulacion_id')->nullable()->constrained('formulaciones')->onDelete('set null');
            $table->integer('tiempo_ciclo_segundos')->nullable();
            $table->integer('piezas_por_ciclo')->default(1);
            $table->decimal('temperatura_proceso', 5, 1)->nullable();
            $table->decimal('precio_venta', 10, 2);
            $table->enum('unidad_venta', ['unidad', 'paquete', 'caja', 'kg'])->default('unidad');
            $table->integer('unidades_por_paquete')->default(1);
            $table->integer('stock_minimo')->default(0);
            $table->integer('stock_actual')->default(0);
            $table->string('imagen_producto')->nullable();
            $table->boolean('activo')->default(true);
            $table->timestamps();
            
            $table->index('codigo_producto');
            $table->index('activo');
            $table->index('stock_actual');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('productos');
    }
};
```

### 2025_01_01_000042_create_movimientos_inventario_productos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('movimientos_inventario_productos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('producto_id')->constrained('productos')->onDelete('restrict');
            $table->enum('tipo_movimiento', ['entrada_produccion', 'salida_venta', 'ajuste', 'merma', 'devolucion']);
            $table->integer('cantidad');
            $table->string('lote_produccion', 50)->nullable();
            $table->date('fecha_fabricacion')->nullable();
            $table->date('fecha_vencimiento')->nullable();
            $table->foreignId('usuario_id')->constrained('usuarios')->onDelete('restrict');
            $table->string('referencia', 100)->nullable();
            $table->text('motivo')->nullable();
            $table->timestamp('fecha_movimiento')->useCurrent();
            
            $table->index('producto_id');
            $table->index('fecha_movimiento');
            $table->index('tipo_movimiento');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('movimientos_inventario_productos');
    }
};
```

---

## MÓDULO 6: ÓRDENES DE PRODUCCIÓN (3 migrations)

### 2025_01_01_000050_create_ordenes_produccion_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('ordenes_produccion', function (Blueprint $table) {
            $table->id();
            $table->string('numero_orden', 50)->unique();
            $table->foreignId('producto_id')->constrained('productos')->onDelete('restrict');
            $table->integer('cantidad_planificada');
            $table->integer('cantidad_producida')->default(0);
            $table->integer('cantidad_conforme')->default(0);
            $table->integer('cantidad_defectuosa')->default(0);
            $table->foreignId('formulacion_id')->constrained('formulaciones')->onDelete('restrict');
            $table->foreignId('maquina_id')->constrained('maquinas')->onDelete('restrict');
            $table->foreignId('turno_id')->constrained('turnos')->onDelete('restrict');
            $table->dateTime('fecha_programada');
            $table->dateTime('fecha_inicio')->nullable();
            $table->dateTime('fecha_fin')->nullable();
            $table->foreignId('operador_id')->nullable()->constrained('usuarios')->onDelete('set null');
            $table->foreignId('supervisor_id')->nullable()->constrained('usuarios')->onDelete('set null');
            $table->enum('estado', ['pendiente', 'en_proceso', 'pausada', 'completada', 'cancelada'])->default('pendiente');
            $table->enum('prioridad', ['baja', 'normal', 'alta', 'urgente'])->default('normal');
            $table->text('notas_produccion')->nullable();
            $table->text('observaciones_calidad')->nullable();
            $table->foreignId('creado_por')->constrained('usuarios')->onDelete('restrict');
            $table->timestamps();
            
            $table->index('numero_orden');
            $table->index('estado');
            $table->index('fecha_programada');
            $table->index('maquina_id');
            $table->index('producto_id');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('ordenes_produccion');
    }
};
```

### 2025_01_01_000051_create_lotes_produccion_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('lotes_produccion', function (Blueprint $table) {
            $table->id();
            $table->string('numero_lote', 50)->unique();
            $table->foreignId('orden_id')->constrained('ordenes_produccion')->onDelete('restrict');
            $table->integer('cantidad');
            $table->dateTime('fecha_fabricacion');
            $table->date('fecha_vencimiento');
            $table->json('trazabilidad_insumos')->nullable();
            $table->enum('estado_lote', ['cuarentena', 'aprobado', 'rechazado', 'distribuido'])->default('cuarentena');
            $table->string('ubicacion_almacen', 100)->nullable();
            
            $table->index('numero_lote');
            $table->index('orden_id');
            $table->index('estado_lote');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('lotes_produccion');
    }
};
```

### 2025_01_01_000052_create_registros_produccion_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('registros_produccion', function (Blueprint $table) {
            $table->id();
            $table->foreignId('orden_id')->constrained('ordenes_produccion')->onDelete('restrict');
            $table->foreignId('maquina_id')->constrained('maquinas')->onDelete('restrict');
            $table->foreignId('operador_id')->constrained('usuarios')->onDelete('restrict');
            $table->dateTime('fecha_hora');
            $table->integer('piezas_producidas')->default(0);
            $table->integer('piezas_conformes')->default(0);
            $table->integer('piezas_defectuosas')->default(0);
            $table->string('tipo_defecto', 100)->nullable();
            $table->decimal('temperatura_zona1', 5, 1)->nullable()->comment('°C');
            $table->decimal('temperatura_zona2', 5, 1)->nullable()->comment('°C');
            $table->decimal('temperatura_zona3', 5, 1)->nullable()->comment('°C');
            $table->decimal('temperatura_zona4', 5, 1)->nullable()->comment('°C');
            $table->decimal('presion_inyeccion', 6, 2)->nullable()->comment('Bar');
            $table->decimal('velocidad_husillo', 7, 2)->nullable()->comment('RPM');
            $table->decimal('tiempo_ciclo_real', 6, 2)->nullable()->comment('segundos');
            $table->decimal('consumo_energia_kwh', 8, 3)->nullable();
            $table->decimal('consumo_material_kg', 10, 3)->nullable();
            $table->decimal('scrap_kg', 10, 3)->nullable();
            $table->text('observaciones')->nullable();
            $table->boolean('alerta_calidad')->default(false);
            
            $table->index('fecha_hora');
            $table->index(['orden_id', 'maquina_id']);
            $table->index('alerta_calidad');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('registros_produccion');
    }
};
```

---

## MÓDULO 7: CONTROL DE CALIDAD (3 migrations)

### 2025_01_01_000060_create_inspecciones_calidad_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('inspecciones_calidad', function (Blueprint $table) {
            $table->id();
            $table->foreignId('orden_id')->constrained('ordenes_produccion')->onDelete('restrict');
            $table->foreignId('lote_id')->nullable()->constrained('lotes_produccion')->onDelete('set null');
            $table->enum('tipo_inspeccion', ['primera_pieza', 'proceso', 'final', 'auditoria']);
            $table->dateTime('fecha_hora');
            $table->foreignId('inspector_id')->constrained('usuarios')->onDelete('restrict');
            $table->decimal('peso_promedio_gramos', 8, 3)->nullable();
            $table->decimal('desviacion_peso', 6, 3)->nullable();
            $table->decimal('espesor_promedio_micras', 7, 2)->nullable();
            $table->decimal('resistencia_traccion_mpa', 7, 2)->nullable();
            $table->boolean('test_biodegradacion')->nullable();
            $table->integer('dias_compostaje_prueba')->nullable();
            $table->integer('manchas')->default(0);
            $table->integer('deformaciones')->default(0);
            $table->integer('rebabas')->default(0);
            $table->integer('burbujas')->default(0);
            $table->integer('fisuras')->default(0);
            $table->text('otros_defectos')->nullable();
            $table->integer('piezas_inspeccionadas');
            $table->integer('piezas_aprobadas');
            $table->integer('piezas_rechazadas');
            $table->enum('resultado', ['aprobado', 'aprobado_condicional', 'rechazado']);
            $table->text('observaciones')->nullable();
            $table->text('acciones_correctivas')->nullable();
            
            $table->index('orden_id');
            $table->index('lote_id');
            $table->index('fecha_hora');
            $table->index('resultado');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('inspecciones_calidad');
    }
};
```

### 2025_01_01_000061_create_defectos_calidad_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('defectos_calidad', function (Blueprint $table) {
            $table->id();
            $table->string('codigo_defecto', 20)->unique();
            $table->string('nombre_defecto', 100);
            $table->text('descripcion')->nullable();
            $table->enum('severidad', ['critico', 'mayor', 'menor']);
            $table->boolean('activo')->default(true);
            
            $table->index('codigo_defecto');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('defectos_calidad');
    }
};
```

### 2025_01_01_000062_create_registro_defectos_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('registro_defectos', function (Blueprint $table) {
            $table->id();
            $table->foreignId('inspeccion_id')->constrained('inspecciones_calidad')->onDelete('cascade');
            $table->foreignId('defecto_id')->constrained('defectos_calidad')->onDelete('restrict');
            $table->integer('cantidad');
            $table->string('ubicacion_pieza', 100)->nullable();
            $table->string('imagen_evidencia')->nullable();
            
            $table->index('inspeccion_id');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('registro_defectos');
    }
};
```

---

## MÓDULO 8: KPIs (2 migrations)

### 2025_01_01_000070_create_kpis_diarios_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('kpis_diarios', function (Blueprint $table) {
            $table->id();
            $table->date('fecha');
            $table->foreignId('maquina_id')->constrained('maquinas')->onDelete('restrict');
            $table->foreignId('turno_id')->constrained('turnos')->onDelete('restrict');
            $table->integer('unidades_planificadas');
            $table->integer('unidades_producidas');
            $table->integer('unidades_conformes');
            $table->integer('unidades_defectuosas');
            $table->decimal('scrap_kg', 10, 2)->default(0);
            $table->integer('tiempo_planificado')->comment('minutos');
            $table->integer('tiempo_operacion')->comment('minutos');
            $table->integer('tiempo_paradas')->comment('minutos');
            $table->integer('tiempo_setup')->default(0)->comment('minutos');
            $table->decimal('disponibilidad', 5, 2)->comment('Tiempo operación / Tiempo planificado * 100');
            $table->decimal('rendimiento', 5, 2)->comment('Producción real / Producción teórica * 100');
            $table->decimal('calidad', 5, 2)->comment('Piezas conformes / Piezas producidas * 100');
            $table->decimal('oee', 5, 2)->comment('Disponibilidad * Rendimiento * Calidad / 100');
            $table->decimal('consumo_energia_kwh', 10, 2)->default(0);
            $table->decimal('consumo_material_kg', 10, 2)->default(0);
            $table->decimal('eficiencia_material', 5, 2)->nullable()->comment('%');
            $table->decimal('costo_produccion', 12, 2)->nullable();
            $table->decimal('tasa_defectos', 5, 2)->comment('PPM o %');
            $table->decimal('first_pass_yield', 5, 2)->nullable();
            $table->timestamp('calculado_en')->useCurrent();
            
            $table->unique(['fecha', 'maquina_id', 'turno_id']);
            $table->index('fecha');
            $table->index('maquina_id');
            $table->index('oee');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('kpis_diarios');
    }
};
```

### 2025_01_01_000071_create_kpis_mensuales_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('kpis_mensuales', function (Blueprint $table) {
            $table->id();
            $table->year('año');
            $table->tinyInteger('mes');
            $table->foreignId('maquina_id')->nullable()->constrained('maquinas')->onDelete('set null');
            $table->bigInteger('total_unidades_producidas');
            $table->bigInteger('total_unidades_conformes');
            $table->decimal('total_scrap_kg', 12, 2)->default(0);
            $table->decimal('oee_promedio', 5, 2);
            $table->decimal('disponibilidad_promedio', 5, 2);
            $table->decimal('rendimiento_promedio', 5, 2);
            $table->decimal('calidad_promedio', 5, 2);
            $table->decimal('mtbf', 10, 2)->nullable()->comment('horas');
            $table->decimal('mttr', 10, 2)->nullable()->comment('horas');
            $table->integer('numero_paros')->default(0);
            $table->decimal('tiempo_total_paros_horas', 10, 2)->default(0);
            $table->decimal('costo_total_produccion', 15, 2)->nullable();
            $table->decimal('costo_unitario', 10, 4)->nullable();
            $table->decimal('costo_energia', 12, 2)->nullable();
            $table->decimal('costo_material', 12, 2)->nullable();
            $table->decimal('costo_mantenimiento', 12, 2)->nullable();
            $table->decimal('porcentaje_material_biodegradable', 5, 2)->nullable();
            $table->decimal('cumplimiento_certificaciones', 5, 2)->nullable();
            $table->timestamp('calculado_en')->useCurrent();
            
            $table->unique(['año', 'mes', 'maquina_id']);
            $table->index(['año', 'mes']);
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('kpis_mensuales');
    }
};
```

---

## MÓDULO 9: ALERTAS (1 migration)

### 2025_01_01_000080_create_alertas_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('alertas', function (Blueprint $table) {
            $table->id();
            $table->enum('tipo_alerta', ['stock_bajo', 'maquina_parada', 'calidad_deficiente', 'mantenimiento_vencido', 'meta_no_cumplida', 'otro']);
            $table->enum('severidad', ['info', 'advertencia', 'critico']);
            $table->string('titulo', 200);
            $table->text('mensaje');
            $table->string('entidad_tipo', 50)->nullable();
            $table->unsignedBigInteger('entidad_id')->nullable();
            $table->foreignId('usuario_destino_id')->nullable()->constrained('usuarios')->onDelete('cascade');
            $table->boolean('leida')->default(false);
            $table->timestamp('fecha_lectura')->nullable();
            $table->text('accion_tomada')->nullable();
            $table->timestamp('created_at')->useCurrent();
            
            $table->index('usuario_destino_id');
            $table->index('leida');
            $table->index('severidad');
            $table->index('created_at');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('alertas');
    }
};
```

---

## MÓDULO 10: CERTIFICACIONES (2 migrations)

### 2025_01_01_000090_create_certificaciones_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('certificaciones', function (Blueprint $table) {
            $table->id();
            $table->string('nombre_certificacion', 150);
            $table->enum('tipo_certificacion', ['producto', 'proceso', 'empresa', 'ambiental']);
            $table->string('organismo_certificador', 150)->nullable();
            $table->string('numero_certificado', 100)->nullable();
            $table->date('fecha_emision');
            $table->date('fecha_vencimiento');
            $table->enum('estado', ['vigente', 'por_vencer', 'vencida', 'en_renovacion']);
            $table->text('alcance')->nullable();
            $table->string('documento_pdf')->nullable();
            $table->text('notas')->nullable();
            $table->timestamp('created_at')->useCurrent();
            
            $table->index('fecha_vencimiento');
            $table->index('estado');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('certificaciones');
    }
};
```

### 2025_01_01_000091_create_auditorias_table.php
```php
<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('auditorias', function (Blueprint $table) {
            $table->id();
            $table->enum('tipo_auditoria', ['interna', 'externa', 'certificacion', 'cliente']);
            $table->date('fecha_auditoria');
            $table->string('auditor', 150);
            $table->text('alcance');
            $table->text('hallazgos')->nullable();
            $table->integer('no_conformidades')->default(0);
            $table->integer('observaciones')->default(0);
            $table->integer('oportunidades_mejora')->default(0);
            $table->enum('resultado', ['satisfactorio', 'condicional', 'no_satisfactorio']);
            $table->text('plan_accion')->nullable();
            $table->string('documento_informe')->nullable();
            $table->foreignId('usuario_responsable_id')->constrained('usuarios')->onDelete('restrict');
            
            $table->index('fecha_auditoria');
            $table->index('tipo_auditoria');
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('auditorias');
    }
};
```

---

## 🎯 COMANDOS PARA EJECUTAR LAS MIGRATIONS

```bash
# Ver estado de migrations
php artisan migrate:status

# Ejecutar todas las migrations
php artisan migrate

# Ejecutar migrations con seeders
php artisan migrate --seed

# Reiniciar base de datos (¡CUIDADO! Borra todo)
php artisan migrate:fresh

# Reiniciar con seeders
php artisan migrate:fresh --seed

# Rollback última migración
php artisan migrate:rollback

# Rollback todas las migraciones
php artisan migrate:reset

# Ver SQL que se ejecutará sin ejecutarlo
php artisan migrate --pretend
```

---

## 📊 RESUMEN DE MIGRATIONS

| Módulo | # Migrations | Tablas |
|--------|-------------|--------|
| Usuarios | 4 | roles, usuarios, turnos, asignacion_turnos |
| Insumos | 3 | categorias_insumos, insumos, movimientos_inventario_insumos |
| Formulaciones | 2 | formulaciones, componentes_formulacion |
| Maquinaria | 4 | tipos_maquina, maquinas, mantenimientos, paros_maquina |
| Productos | 3 | categorias_productos, productos, movimientos_inventario_productos |
| Producción | 3 | ordenes_produccion, lotes_produccion, registros_produccion |
| Calidad | 3 | inspecciones_calidad, defectos_calidad, registro_defectos |
| KPIs | 2 | kpis_diarios, kpis_mensuales |
| Alertas | 1 | alertas |
| Certificaciones | 2 | certificaciones, auditorias |
| **TOTAL** | **27** | **27 tablas** |

---

## ✅ VERIFICACIÓN POST-MIGRACIÓN

```bash
# Ver todas las tablas creadas
php artisan db:show

# Ver estructura de una tabla
php artisan db:table usuarios

# Contar registros en todas las tablas
php artisan tinker
>>> DB::select("SHOW TABLES");
```

---

**¡27 Migrations Completas Listas para Usar!** 🎉

Copia cada migration a tu carpeta `database/migrations/` y ejecuta:
```bash
php artisan migrate
```