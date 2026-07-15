# 🚀 ECOPLAST SRL - PROYECTO LARAVEL 12
## Guía Completa de Instalación y Estructura

---

## 📋 PASO 1: CREAR PROYECTO LARAVEL 12

### Opción A: Usando Laravel Installer (Recomendado)

```bash
# 1. Instalar Laravel Installer (si no lo tienes)
composer global require laravel/installer

# 2. Crear proyecto Laravel 12
laravel new ecoplast-srl

# 3. Entrar al directorio
cd ecoplast-srl
```

### Opción B: Usando Composer Directamente

```bash
# Crear proyecto Laravel 12
composer create-project laravel/laravel ecoplast-srl "^12.0"

# Entrar al directorio
cd ecoplast-srl
```

### Verificar Instalación

```bash
# Ver versión de Laravel
php artisan --version
# Debe mostrar: Laravel Framework 12.x.x

# Ver versión de PHP (debe ser >= 8.2)
php -v
# Debe mostrar: PHP 8.2.x o superior
```

---

## 🔧 PASO 2: CONFIGURACIÓN INICIAL

### 2.1 Configurar .env

```bash
# Copiar archivo de ejemplo
cp .env.example .env

# Generar key de aplicación
php artisan key:generate
```

### 2.2 Editar .env con Configuración de Ecoplast

```env
# Nombre de la aplicación
APP_NAME="Ecoplast SRL"
APP_ENV=local
APP_KEY=base64:tu_key_generada_aqui
APP_DEBUG=true
APP_TIMEZONE=America/La_Paz
APP_URL=http://localhost:8000
APP_LOCALE=es
APP_FALLBACK_LOCALE=es
APP_FAKER_LOCALE=es_ES

# Base de Datos
DB_CONNECTION=mysql
DB_HOST=127.0.0.1
DB_PORT=3306
DB_DATABASE=ecoplast_produccion
DB_USERNAME=root
DB_PASSWORD=tu_password

# Broadcasting (Pusher para empezar)
BROADCAST_CONNECTION=pusher

# Cache y Queue (Redis)
CACHE_STORE=redis
QUEUE_CONNECTION=redis
SESSION_DRIVER=redis

# Redis
REDIS_HOST=127.0.0.1
REDIS_PASSWORD=null
REDIS_PORT=6379

# Pusher (para WebSockets)
PUSHER_APP_ID=tu_app_id
PUSHER_APP_KEY=tu_app_key
PUSHER_APP_SECRET=tu_app_secret
PUSHER_HOST=
PUSHER_PORT=443
PUSHER_SCHEME=https
PUSHER_APP_CLUSTER=us2

# Mail
MAIL_MAILER=smtp
MAIL_HOST=smtp.gmail.com
MAIL_PORT=587
MAIL_USERNAME=tu_email@gmail.com
MAIL_PASSWORD=tu_password_app
MAIL_ENCRYPTION=tls
MAIL_FROM_ADDRESS=noreply@ecoplast.com
MAIL_FROM_NAME="Ecoplast SRL"

# Vite
VITE_APP_NAME="${APP_NAME}"
VITE_PUSHER_APP_KEY="${PUSHER_APP_KEY}"
VITE_PUSHER_HOST="${PUSHER_HOST}"
VITE_PUSHER_PORT="${PUSHER_PORT}"
VITE_PUSHER_SCHEME="${PUSHER_SCHEME}"
VITE_PUSHER_APP_CLUSTER="${PUSHER_APP_CLUSTER}"
```

### 2.3 Crear Base de Datos

```bash
# Opción 1: Desde MySQL CLI
mysql -u root -p
CREATE DATABASE ecoplast_produccion CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
EXIT;

# Opción 2: Usar el script SQL que ya tienes
mysql -u root -p < ecoplast_database_mysql.sql
```

---

## 📦 PASO 3: INSTALAR DEPENDENCIAS

### 3.1 Dependencias de PHP

```bash
# Instalar paquetes para Broadcasting
composer require pusher/pusher-php-server

# Instalar paquetes para Excel (opcional, para reportes)
composer require maatwebsite/excel

# Instalar Laravel Debugbar (desarrollo)
composer require barryvdh/laravel-debugbar --dev

# Instalar Laravel IDE Helper (desarrollo)
composer require --dev barryvdh/laravel-ide-helper

# Instalar Spatie Permission (gestión de roles)
composer require spatie/laravel-permission
```

### 3.2 Dependencias de JavaScript

```bash
# Instalar dependencias base
npm install

# Instalar Vue 3
npm install vue@^3.4 @vitejs/plugin-vue

# Instalar Laravel Echo y Pusher
npm install laravel-echo pusher-js

# Instalar Pinia (state management)
npm install pinia

# Instalar Vue Router
npm install vue-router@^4

# Instalar Chart.js para gráficos
npm install chart.js vue-chartjs

# Instalar Axios
npm install axios

# Instalar TailwindCSS
npm install -D tailwindcss@latest postcss@latest autoprefixer@latest
npx tailwindcss init -p
```

---

## 📁 PASO 4: ESTRUCTURA DEL PROYECTO

### 4.1 Crear Estructura de Directorios

```bash
# Crear directorios personalizados
mkdir -p app/Broadcasting
mkdir -p app/Events
mkdir -p app/Observers
mkdir -p app/Traits
mkdir -p app/Services
mkdir -p app/Repositories
mkdir -p resources/js/components/Dashboard
mkdir -p resources/js/components/Produccion
mkdir -p resources/js/components/Calidad
mkdir -p resources/js/stores
mkdir -p resources/js/composables
mkdir -p public/sounds
mkdir -p storage/app/certificates
mkdir -p storage/app/reports
```

### 4.2 Estructura Completa del Proyecto

```
ecoplast-srl/
├── app/
│   ├── Broadcasting/           # Canales de Broadcasting
│   │   ├── DashboardChannel.php
│   │   └── MaquinaChannel.php
│   ├── Console/
│   │   ├── Commands/          # Comandos artisan personalizados
│   │   │   ├── CalcularKPIsDiarios.php
│   │   │   └── GenerarReporteMensual.php
│   │   └── Kernel.php
│   ├── Events/                # Eventos de Broadcasting
│   │   ├── RegistroProduccionActualizado.php
│   │   ├── NuevaAlerta.php
│   │   ├── OrdenCompletada.php
│   │   ├── MaquinaParada.php
│   │   └── KPIsActualizados.php
│   ├── Exceptions/
│   │   └── Handler.php
│   ├── Http/
│   │   ├── Controllers/
│   │   │   ├── Api/
│   │   │   │   ├── DashboardController.php
│   │   │   │   ├── RegistroProduccionController.php
│   │   │   │   ├── OrdenProduccionController.php
│   │   │   │   ├── MaquinaController.php
│   │   │   │   ├── InsumoController.php
│   │   │   │   ├── ProductoController.php
│   │   │   │   ├── CalidadController.php
│   │   │   │   ├── MantenimientoController.php
│   │   │   │   └── KpiController.php
│   │   │   ├── Auth/
│   │   │   │   ├── LoginController.php
│   │   │   │   └── LogoutController.php
│   │   │   ├── DashboardController.php
│   │   │   ├── ProduccionController.php
│   │   │   ├── CalidadController.php
│   │   │   └── ReporteController.php
│   │   ├── Middleware/
│   │   │   ├── CheckRole.php
│   │   │   ├── LogActivity.php
│   │   │   └── EnsureMaquinaDisponible.php
│   │   └── Requests/
│   │       ├── StoreOrdenProduccionRequest.php
│   │       ├── StoreRegistroProduccionRequest.php
│   │       └── StoreInspeccionCalidadRequest.php
│   ├── Jobs/                  # Jobs para Queue
│   │   ├── ActualizarKPIsEnTiempoReal.php
│   │   ├── CalcularKPIsDiarios.php
│   │   ├── GenerarCertificadoLote.php
│   │   ├── EnviarAlertasEmail.php
│   │   └── ProcesarAnalisisPredictivo.php
│   ├── Listeners/             # Listeners de Eventos
│   │   ├── ActualizarOrdenListener.php
│   │   ├── GenerarAlertaListener.php
│   │   └── NotificarUsuariosListener.php
│   ├── Models/                # Modelos Eloquent
│   │   ├── Usuario.php
│   │   ├── Rol.php
│   │   ├── Turno.php
│   │   ├── AsignacionTurno.php
│   │   ├── CategoriaInsumo.php
│   │   ├── Insumo.php
│   │   ├── MovimientoInventarioInsumo.php
│   │   ├── Formulacion.php
│   │   ├── ComponenteFormulacion.php
│   │   ├── TipoMaquina.php
│   │   ├── Maquina.php
│   │   ├── Mantenimiento.php
│   │   ├── ParoMaquina.php
│   │   ├── CategoriaProducto.php
│   │   ├── Producto.php
│   │   ├── MovimientoInventarioProducto.php
│   │   ├── OrdenProduccion.php
│   │   ├── LoteProduccion.php
│   │   ├── RegistroProduccion.php
│   │   ├── InspeccionCalidad.php
│   │   ├── DefectoCalidad.php
│   │   ├── RegistroDefecto.php
│   │   ├── KpiDiario.php
│   │   ├── KpiMensual.php
│   │   ├── Alerta.php
│   │   ├── Certificacion.php
│   │   └── Auditoria.php
│   ├── Observers/             # Observers para Modelos
│   │   ├── RegistroProduccionObserver.php
│   │   ├── AlertaObserver.php
│   │   ├── OrdenProduccionObserver.php
│   │   ├── InsumoObserver.php
│   │   └── LoteProduccionObserver.php
│   ├── Providers/
│   │   ├── AppServiceProvider.php
│   │   ├── AuthServiceProvider.php
│   │   ├── BroadcastServiceProvider.php
│   │   ├── EventServiceProvider.php
│   │   └── RouteServiceProvider.php
│   ├── Repositories/          # Patrón Repository
│   │   ├── OrdenProduccionRepository.php
│   │   ├── KpiRepository.php
│   │   └── DashboardRepository.php
│   ├── Services/              # Lógica de Negocio
│   │   ├── KpiCalculatorService.php
│   │   ├── OEECalculatorService.php
│   │   ├── AlertService.php
│   │   ├── ReportGeneratorService.php
│   │   └── TrazabilidadService.php
│   └── Traits/                # Traits reutilizables
│       ├── HasOEE.php
│       ├── LogsActivity.php
│       └── GeneratesCodes.php
├── bootstrap/
├── config/
│   ├── app.php
│   ├── broadcasting.php       # Configuración de Broadcasting
│   ├── cache.php
│   ├── database.php
│   ├── queue.php
│   └── ecoplast.php          # Configuraciones personalizadas
├── database/
│   ├── factories/
│   │   ├── UsuarioFactory.php
│   │   ├── MaquinaFactory.php
│   │   ├── ProductoFactory.php
│   │   └── OrdenProduccionFactory.php
│   ├── migrations/
│   │   ├── 2025_01_01_000001_create_roles_table.php
│   │   ├── 2025_01_01_000002_create_usuarios_table.php
│   │   ├── 2025_01_01_000003_create_turnos_table.php
│   │   ├── 2025_01_01_000004_create_asignacion_turnos_table.php
│   │   ├── 2025_01_01_000010_create_categorias_insumos_table.php
│   │   ├── 2025_01_01_000011_create_insumos_table.php
│   │   ├── 2025_01_01_000012_create_movimientos_inventario_insumos_table.php
│   │   ├── 2025_01_01_000020_create_formulaciones_table.php
│   │   ├── 2025_01_01_000021_create_componentes_formulacion_table.php
│   │   ├── 2025_01_01_000030_create_tipos_maquina_table.php
│   │   ├── 2025_01_01_000031_create_maquinas_table.php
│   │   ├── 2025_01_01_000032_create_mantenimientos_table.php
│   │   ├── 2025_01_01_000033_create_paros_maquina_table.php
│   │   ├── 2025_01_01_000040_create_categorias_productos_table.php
│   │   ├── 2025_01_01_000041_create_productos_table.php
│   │   ├── 2025_01_01_000042_create_movimientos_inventario_productos_table.php
│   │   ├── 2025_01_01_000050_create_ordenes_produccion_table.php
│   │   ├── 2025_01_01_000051_create_lotes_produccion_table.php
│   │   ├── 2025_01_01_000052_create_registros_produccion_table.php
│   │   ├── 2025_01_01_000060_create_inspecciones_calidad_table.php
│   │   ├── 2025_01_01_000061_create_defectos_calidad_table.php
│   │   ├── 2025_01_01_000062_create_registro_defectos_table.php
│   │   ├── 2025_01_01_000070_create_kpis_diarios_table.php
│   │   ├── 2025_01_01_000071_create_kpis_mensuales_table.php
│   │   ├── 2025_01_01_000080_create_alertas_table.php
│   │   ├── 2025_01_01_000090_create_certificaciones_table.php
│   │   └── 2025_01_01_000091_create_auditorias_table.php
│   └── seeders/
│       ├── DatabaseSeeder.php
│       ├── RolSeeder.php
│       ├── UsuarioSeeder.php
│       ├── TurnoSeeder.php
│       ├── TipoMaquinaSeeder.php
│       ├── MaquinaSeeder.php
│       ├── CategoriaInsumoSeeder.php
│       ├── InsumoSeeder.php
│       ├── CategoriaProductoSeeder.php
│       ├── ProductoSeeder.php
│       ├── FormulacionSeeder.php
│       └── DefectoCalidadSeeder.php
├── public/
│   ├── sounds/
│   │   ├── alert.mp3
│   │   └── notification.mp3
│   └── images/
│       └── logo-ecoplast.png
├── resources/
│   ├── css/
│   │   └── app.css
│   ├── js/
│   │   ├── app.js
│   │   ├── bootstrap.js        # Config de Laravel Echo
│   │   ├── router/
│   │   │   └── index.js
│   │   ├── stores/             # Pinia Stores
│   │   │   ├── auth.js
│   │   │   ├── dashboard.js
│   │   │   ├── produccion.js
│   │   │   └── alertas.js
│   │   ├── composables/        # Composables de Vue
│   │   │   ├── useWebSocket.js
│   │   │   ├── useNotifications.js
│   │   │   └── useOEE.js
│   │   ├── components/
│   │   │   ├── Layout/
│   │   │   │   ├── AppLayout.vue
│   │   │   │   ├── Navbar.vue
│   │   │   │   ├── Sidebar.vue
│   │   │   │   └── Footer.vue
│   │   │   ├── Dashboard/
│   │   │   │   ├── DashboardGerencia.vue
│   │   │   │   ├── DashboardAdmin.vue
│   │   │   │   ├── DashboardOperador.vue
│   │   │   │   ├── DashboardCientifico.vue
│   │   │   │   ├── KPICard.vue
│   │   │   │   ├── MaquinaStatus.vue
│   │   │   │   ├── AlertasPanel.vue
│   │   │   │   └── GraficoOEE.vue
│   │   │   ├── Produccion/
│   │   │   │   ├── OrdenProduccionForm.vue
│   │   │   │   ├── OrdenProduccionList.vue
│   │   │   │   ├── RegistroProduccionForm.vue
│   │   │   │   ├── MaquinaMonitor.vue
│   │   │   │   └── TurnoCalendar.vue
│   │   │   ├── Calidad/
│   │   │   │   ├── InspeccionForm.vue
│   │   │   │   ├── LotesEnCuarentena.vue
│   │   │   │   └── DefectosChart.vue
│   │   │   ├── Inventario/
│   │   │   │   ├── InsumosTable.vue
│   │   │   │   ├── ProductosTable.vue
│   │   │   │   └── StockAlerts.vue
│   │   │   └── Common/
│   │   │       ├── DataTable.vue
│   │   │       ├── Modal.vue
│   │   │       ├── Toast.vue
│   │   │       └── LoadingSpinner.vue
│   │   └── views/
│   │       ├── Auth/
│   │       │   ├── Login.vue
│   │       │   └── ForgotPassword.vue
│   │       ├── Dashboard.vue
│   │       ├── Produccion/
│   │       │   ├── Index.vue
│   │       │   ├── Create.vue
│   │       │   └── Show.vue
│   │       ├── Calidad/
│   │       │   └── Index.vue
│   │       ├── Inventario/
│   │       │   └── Index.vue
│   │       └── Reportes/
│   │           └── Index.vue
│   ├── lang/
│   │   └── es/
│   │       ├── auth.php
│   │       ├── pagination.php
│   │       ├── passwords.php
│   │       └── validation.php
│   └── views/
│       ├── layouts/
│       │   └── app.blade.php
│       ├── auth/
│       │   └── login.blade.php
│       └── app.blade.php        # SPA entry point
├── routes/
│   ├── web.php
│   ├── api.php
│   ├── channels.php           # Broadcasting Channels
│   └── console.php
├── storage/
│   ├── app/
│   │   ├── certificates/      # Certificados de lotes
│   │   ├── reports/           # Reportes generados
│   │   └── exports/           # Exportaciones Excel
│   ├── framework/
│   └── logs/
├── tests/
│   ├── Feature/
│   │   ├── OrdenProduccionTest.php
│   │   ├── RegistroProduccionTest.php
│   │   └── KPICalculationTest.php
│   └── Unit/
│       ├── OEECalculatorTest.php
│       └── AlertServiceTest.php
├── .env
├── .env.example
├── composer.json
├── package.json
├── vite.config.js
├── tailwind.config.js
├── phpunit.xml
└── README.md
```

---

## ⚙️ PASO 5: CONFIGURAR ARCHIVOS CLAVE

### 5.1 vite.config.js

```javascript
import { defineConfig } from 'vite';
import laravel from 'laravel-vite-plugin';
import vue from '@vitejs/plugin-vue';

export default defineConfig({
    plugins: [
        laravel({
            input: ['resources/css/app.css', 'resources/js/app.js'],
            refresh: true,
        }),
        vue({
            template: {
                transformAssetUrls: {
                    base: null,
                    includeAbsolute: false,
                },
            },
        }),
    ],
    resolve: {
        alias: {
            '@': '/resources/js',
        },
    },
});
```

### 5.2 tailwind.config.js

```javascript
/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./resources/**/*.blade.php",
    "./resources/**/*.js",
    "./resources/**/*.vue",
  ],
  theme: {
    extend: {
      colors: {
        'ecoplast': {
          50: '#f0fdf4',
          100: '#dcfce7',
          200: '#bbf7d0',
          300: '#86efac',
          400: '#4ade80',
          500: '#22c55e',
          600: '#16a34a',
          700: '#15803d',
          800: '#166534',
          900: '#14532d',
        },
      },
    },
  },
  plugins: [],
}
```

### 5.3 config/ecoplast.php (Configuración Personalizada)

```php
<?php

return [

    /*
    |--------------------------------------------------------------------------
    | Configuración General de Ecoplast SRL
    |--------------------------------------------------------------------------
    */

    'company' => [
        'name' => 'Ecoplast SRL',
        'address' => 'Santa Cruz de la Sierra, Bolivia',
        'phone' => '+591 3 123-4567',
        'email' => 'info@ecoplast.com.bo',
    ],

    /*
    |--------------------------------------------------------------------------
    | Parámetros de Producción
    |--------------------------------------------------------------------------
    */

    'produccion' => [
        'oee_objetivo' => 85.0, // OEE objetivo en porcentaje
        'tasa_defectos_maxima' => 5.0, // Máximo 5% de defectos
        'tiempo_ciclo_alerta' => 15, // % de variación para alerta
        'temperatura_tolerancia' => 3.0, // ±3°C de tolerancia
    ],

    /*
    |--------------------------------------------------------------------------
    | Configuración de Alertas
    |--------------------------------------------------------------------------
    */

    'alertas' => [
        'tiempo_respuesta_critica' => 15, // minutos
        'tiempo_respuesta_advertencia' => 60, // minutos
        'escalar_no_atendidas' => 24, // horas
        'sonido_alertas_criticas' => true,
    ],

    /*
    |--------------------------------------------------------------------------
    | Configuración de KPIs
    |--------------------------------------------------------------------------
    */

    'kpis' => [
        'calcular_cada' => 30, // segundos
        'disponibilidad_objetivo' => 95.0,
        'rendimiento_objetivo' => 90.0,
        'calidad_objetivo' => 98.0,
        'mtbf_objetivo' => 200, // horas
        'mttr_objetivo' => 4, // horas
    ],

    /*
    |--------------------------------------------------------------------------
    | Configuración de Inventario
    |--------------------------------------------------------------------------
    */

    'inventario' => [
        'stock_minimo_dias' => 15,
        'rotacion_objetivo' => 12, // veces al año
        'eficiencia_material_minima' => 90.0, // %
        'scrap_reciclado_objetivo' => 80.0, // %
    ],

    /*
    |--------------------------------------------------------------------------
    | Configuración de Calidad
    |--------------------------------------------------------------------------
    */

    'calidad' => [
        'aql_estandar' => 2.5, // %
        'tiempo_cuarentena_maximo' => 72, // horas
        'muestra_inspeccion_proceso' => 10, // piezas
        'muestra_inspeccion_final' => 50, // piezas
        'test_biodegradacion_dias' => 90,
    ],

    /*
    |--------------------------------------------------------------------------
    | Tipos de Material Biodegradable
    |--------------------------------------------------------------------------
    */

    'materiales' => [
        'PLA' => 'Ácido Poliláctico',
        'PHA' => 'Polihidroxialcanoatos',
        'PBS' => 'Polibutileno Succinato',
        'PBAT' => 'Polibutileno Adipato Tereftalato',
        'Almidon' => 'Almidón Termoplástico',
        'Celulosa' => 'Celulosa',
    ],

    /*
    |--------------------------------------------------------------------------
    | Certificaciones Disponibles
    |--------------------------------------------------------------------------
    */

    'certificaciones' => [
        'EN 13432' => 'Compostabilidad Europea',
        'ASTM D6400' => 'Estándar USA',
        'OK Compost' => 'TÜV Austria',
        'BPI' => 'Biodegradable Products Institute',
        'ISO 9001' => 'Sistema de Gestión de Calidad',
    ],

];
```

---

## 🗄️ PASO 6: MIGRAR BASE DE DATOS

```bash
# Opción 1: Usar el script SQL completo (recomendado)
mysql -u root -p ecoplast_produccion < ecoplast_database_mysql.sql

# Opción 2: Usar migrations de Laravel (vamos a crearlas)
php artisan migrate

# Opción 3: Migrar con seeders
php artisan migrate --seed
```

---

## 🌱 PASO 7: CONFIGURAR BROADCASTING

### 7.1 Descomentar BroadcastServiceProvider

```php
// config/app.php

'providers' => ServiceProvider::defaultProviders()->merge([
    // ...
    App\Providers\BroadcastServiceProvider::class, // ← Descomentar
])->toArray(),
```

### 7.2 Instalar Pusher

```bash
composer require pusher/pusher-php-server
npm install --save laravel-echo pusher-js
```

---

## 🚀 PASO 8: INICIAR SERVICIOS

### 8.1 Iniciar Servidor de Desarrollo

```bash
# Terminal 1: Laravel Server
php artisan serve
# Acceder en: http://localhost:8000

# Terminal 2: Vite (Frontend)
npm run dev
# Hot reload activado

# Terminal 3: Queue Worker (para Jobs)
php artisan queue:work --tries=3

# Terminal 4: Schedule Runner (para CRON)
php artisan schedule:work
```

### 8.2 Configurar Supervisor (Producción)

```bash
# Editar archivo de configuración
sudo nano /etc/supervisor/conf.d/ecoplast.conf
```

```ini
[program:ecoplast-queue]
process_name=%(program_name)s_%(process_num)02d
command=php /var/www/ecoplast-srl/artisan queue:work redis --sleep=3 --tries=3
autostart=true
autorestart=true
user=www-data
numprocs=4
redirect_stderr=true
stdout_logfile=/var/www/ecoplast-srl/storage/logs/worker.log
```

```bash
# Reiniciar supervisor
sudo supervisorctl reread
sudo supervisorctl update
sudo supervisorctl start ecoplast-queue:*
```

---

## ✅ PASO 9: VERIFICAR INSTALACIÓN

### Checklist de Verificación:

```bash
# ✅ Verificar Laravel
php artisan --version

# ✅ Verificar base de datos
php artisan migrate:status

# ✅ Verificar conexión
php artisan tinker
>>> DB::connection()->getPdo();

# ✅ Verificar cache
php artisan cache:clear
php artisan config:clear
php artisan route:clear

# ✅ Verificar permisos
sudo chown -R www-data:www-data storage bootstrap/cache
sudo chmod -R 775 storage bootstrap/cache

# ✅ Generar optimizaciones
php artisan optimize
```

---

## 📊 RESUMEN DE COMANDOS ÚTILES

```bash
# Desarrollo
php artisan serve                    # Iniciar servidor
npm run dev                          # Hot reload frontend
php artisan queue:work              # Procesar jobs
php artisan schedule:work           # Ejecutar tareas programadas

# Base de Datos
php artisan migrate                 # Ejecutar migraciones
php artisan migrate:fresh --seed    # Reiniciar DB con datos
php artisan db:seed                 # Llenar con datos de prueba

# Cache y Optimización
php artisan optimize                # Optimizar todo
php artisan optimize:clear          # Limpiar optimizaciones
php artisan config:cache            # Cachear configuración
php artisan route:cache             # Cachear rutas
php artisan view:cache              # Cachear vistas

# Broadcasting
php artisan queue:listen            # Escuchar cola
php artisan queue:restart           # Reiniciar workers

# Mantenimiento
php artisan down                    # Modo mantenimiento
php artisan up                      # Activar sitio

# Testing
php artisan test                    # Ejecutar tests
php artisan test --parallel         # Tests en paralelo
```

---

## 🎯 PRÓXIMOS PASOS

Ahora que tienes la estructura base, vamos a crear:

1. ✅ **Migrations** completas (27 tablas)
2. ✅ **Models** con relaciones
3. ✅ **Seeders** con datos de prueba
4. ✅ **Controllers** para API
5. ✅ **Events** y **Observers** para tiempo real
6. ✅ **Jobs** para procesamiento en background
7. ✅ **Componentes Vue** para dashboards
8. ✅ **Routes** (web, api, channels)

---

**¿Quieres que ahora creemos las Migrations completas?** 🚀

*****va salir el error pero se debe de tomar en cuenta cuando se requiera realizar en tiempo real ******

Done: Diagnosing the Error
¡Ah, ya veo el problema! Gracias por el informe de error, es muy claro.

El error Class "Redis" not found significa exactamente lo que dice: PHP no puede encontrar la clase Redis. Esto sucede porque, aunque hemos configurado Laravel para que use Redis como gestor de sesiones, caché y colas, la extensión de PHP para Redis no está instalada o activada en tu XAMPP.

XAMPP no incluye esta extensión por defecto, por lo que es necesario instalarla manualmente.

Tengo dos soluciones para ti:

Solución 1: Rápida y Temporal (para que veas la bienvenida ahora)
Puedo modificar tu archivo .env para que temporalmente no use Redis, sino la base de datos (que ya está preparada para ello). Esto hará que el error desaparezca inmediatamente y puedas ver tu página de bienvenida.

Los cambios serían:

SESSION_DRIVER de redis a database.
CACHE_STORE de redis a database.
QUEUE_CONNECTION de redis a database.
BROADCAST_CONNECTION de pusher a log (para evitar errores mientras no esté configurado).
Ventaja: Es instantáneo y puedo hacerlo por ti ahora mismo.
Desventaja: No estarás usando Redis, que es lo que tu guía recomienda para un mejor rendimiento y para el sistema de broadcasting en tiempo real.

Solución 2: Permanente y Correcta (requiere pasos manuales de tu parte)
La solución definitiva es instalar la extensión de Redis para PHP en tu XAMPP. Una vez hecho esto, todo funcionará como estaba previsto en tu guía.

Pasos a seguir:

Descargar la extensión: Ve al repositorio de PECL para Redis y descarga la versión correcta para tu sistema. Necesitas una que sea compatible con PHP 8.2, Thread Safe (TS) y x64.
Copiar el archivo: Descomprime el archivo y copia php_redis.dll en la carpeta de extensiones de tu XAMPP, que normalmente es ext.
Editar php.ini: Abre el archivo php.ini y añade la siguiente línea al final de la sección de extensiones: extension=redis.
Reiniciar Apache: Abre el panel de control de XAMPP y reinicia el servidor Apache para que cargue la nueva extensión.
Mi recomendación:

Permíteme aplicar la Solución 1 (Rápida y Temporal) ahora para que puedas continuar trabajando y ver tu proyecto funcionando. Luego, cuando tengas un momento, puedes seguir los pasos de la Solución 2 (Permanente) para tener tu entorno configurado correctamente.

¿Quieres que proceda con la solución rápida y temporal modificando tu archivo .env?
