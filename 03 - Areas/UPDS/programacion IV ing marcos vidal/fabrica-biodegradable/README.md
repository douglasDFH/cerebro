# Fábrica Biodegradable - Sistema de Monitoreo en Tiempo Real

Sistema de monitoreo y control de producción para una fábrica de productos biodegradables. Incluye seguimiento en tiempo real del estado de las máquinas, producción, consumos y paradas, utilizando Laravel, Inertia.js, Vue 3 y broadcasting con Reverb.

## Tecnologías Utilizadas

- **Backend**: Laravel 12
- **Frontend**: Vue 3 con Inertia.js
- **Broadcasting**: Laravel Reverb para actualizaciones en tiempo real
- **Base de Datos**: MySQL
- **Estilos**: Tailwind CSS
- **Build Tool**: Vite
- **Autenticación y Roles**: Spatie Laravel Permission

## Requisitos del Sistema

- PHP 8.2 o superior
- Composer
- Node.js 18+ y npm
- MySQL 8.0+
- Git

## Instalación

1. **Clonar el repositorio:**
   ```bash
   git clone https://github.com/douglasDFH/fabrica-biodegradable.git
   cd fabrica-biodegradable
   ```

2. **Instalar dependencias de PHP:**
   ```bash
   composer install
   ```

3. **Instalar dependencias de JavaScript:**
   ```bash
   npm install
   ```

4. **Configurar el entorno:**
   - Copiar el archivo de configuración:
     ```bash
     cp .env.example .env
     ```
   - Editar `.env` y configurar:
     - Conexión a base de datos (DB_*)
     - Broadcasting (REVERB_*)

5. **Generar clave de aplicación:**
   ```bash
   php artisan key:generate
   ```

6. **Migrar la base de datos:**
   ```bash
   php artisan migrate
   ```

7. **Poblar la base de datos con datos iniciales:**
   ```bash
   php artisan db:seed
   ```

## Ejecutar el Proyecto

Para ejecutar la aplicación completa, necesitas tres procesos concurrentes:

1. **Servidor de Laravel:**
   ```bash
   php artisan serve
   ```
   Accede en: http://localhost:8000

2. **Servidor de desarrollo de Vite (para assets):**
   ```bash
   npm run dev
   ```

3. **Servidor de broadcasting (Reverb):**
   ```bash
   php artisan reverb:start
   ```

### Comando conveniente para ejecutar todo:
```bash
npm run start
```
Este comando ejecuta Laravel, Vite y Reverb de forma concurrente.

## Simuladores de Producción

El sistema incluye comandos Artisan para simular la producción y probar el broadcasting:

### Simular producción para una máquina específica:
```bash
php artisan simular:produccion {id_maquina}
```
Ejemplos:
```bash
php artisan simular:produccion 1  # Simula producción para máquina ID 1
php artisan simular:produccion 2  # Simula producción para máquina ID 2
```

### Probar broadcasting:
```bash
php artisan test:broadcast
```
Este comando verifica que el broadcasting esté funcionando correctamente.

## Uso de la Aplicación

1. Accede a la aplicación en http://localhost:8000
2. Ve a `/monitor-maquina` para ver la lista de máquinas
3. Haz clic en "Ver Monitor" para cada máquina para ver el dashboard en tiempo real
4. Ejecuta los simuladores en terminales separadas para ver actualizaciones en vivo

## Estructura del Proyecto

- `app/Models/`: Modelos Eloquent (Maquina, Produccion, etc.)
- `app/Http/Controllers/`: Controladores (MonitorMaquinaController)
- `database/migrations/`: Migraciones de base de datos
- `database/seeders/`: Seeders para datos iniciales
- `resources/js/Pages/Planta/`: Componentes Vue para monitoreo
- `routes/web.php`: Rutas de la aplicación
- `app/Events/`: Eventos para broadcasting

## Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/nueva-funcionalidad`)
3. Commit tus cambios (`git commit -am 'Agrega nueva funcionalidad'`)
4. Push a la rama (`git push origin feature/nueva-funcionalidad`)
5. Abre un Pull Request

## Licencia

Este proyecto está bajo la Licencia MIT.
