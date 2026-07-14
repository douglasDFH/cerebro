# Solución - Problema de Inserción de Citas

## Problema Identificado

El error "El veterinario no está disponible en el horario seleccionado" ocurría debido a una **inconsistencia entre los estados de la tabla Diagnostico y los estados que maneja el módulo de citas**.

### Causa Raíz
- **Tabla Diagnostico**: Solo acepta los estados `'PENDIENTE', 'EN_TRATAMIENTO', 'RESUELTO', 'DERIVADO'`
- **Módulo de Citas**: Intenta insertar estados como `'PROGRAMADO', 'CONFIRMADO', 'EN_PROCESO', 'COMPLETADO', 'CANCELADO'`

Esto causaba que el stored procedure fallara silenciosamente y el sistema interpretara esto como "veterinario no disponible".

## Solución Implementada

### 1. Archivos Corregidos

#### **FrmCitas.cs**
- **Línea 112**: Corregida referencia de `NUsuario.Mostrar()` a `Nusers.Mostrar()`

#### **SP_ProgramarCita.sql**
- Mejorados los mensajes de error con `RAISERROR` en lugar de retornar `0`
- Agregada tolerancia de 5 minutos para fechas pasadas
- Mensajes de error más descriptivos

#### **InstalarModuloCitas.sql** (NUEVO)
- Script completo de instalación y corrección
- Actualiza automáticamente la restricción CHECK de la tabla
- Incluye validaciones y pruebas

### 2. Estados Soportados

#### Para Citas
- `PROGRAMADO`: Cita programada, pendiente de confirmación
- `CONFIRMADO`: Cita confirmada por el cliente  
- `EN_PROCESO`: Consulta en curso
- `COMPLETADO`: Consulta terminada
- `CANCELADO`: Cita cancelada
- `NO_ASISTIO`: Cliente no se presentó a la cita

#### Para Diagnósticos Tradicionales
- `PENDIENTE`: Diagnóstico pendiente
- `EN_TRATAMIENTO`: Tratamiento en curso
- `RESUELTO`: Caso resuelto  
- `DERIVADO`: Derivado a especialista

## Instrucciones de Instalación

### Paso 1: Ejecutar Script de Base de Datos
```sql
-- Ejecutar en SQL Server Management Studio
-- Archivo: InstalarModuloCitas.sql
```

Este script:
- ✅ Elimina las restricciones CHECK antiguas
- ✅ Agrega nueva restricción con todos los estados
- ✅ Actualiza el stored procedure SP_ProgramarCita
- ✅ Realiza pruebas automáticas
- ✅ Muestra resultados de la instalación

### Paso 2: Compilar la Aplicación
1. Abrir el proyecto en Visual Studio
2. Compilar la solución (Build → Build Solution)
3. Verificar que no hay errores de compilación

### Paso 3: Probar el Módulo de Citas
1. Ejecutar la aplicación
2. Ir al módulo de Citas
3. Intentar programar una nueva cita
4. Verificar que ya no aparece el error de disponibilidad

## Funcionalidades del Módulo de Citas

### ✅ Programación de Citas
- Selección de animal y veterinario
- Validación de horarios disponibles  
- Tipos de cita: Primera vez, Seguimiento, Urgencia, etc.
- Costo de consulta opcional
- Observaciones

### ✅ Gestión de Estados
- Confirmar citas programadas
- Iniciar consultas  
- Cancelar citas con motivo
- Marcar como "No asistió"

### ✅ Consultas y Reportes
- Citas del día
- Citas de la semana
- Próximas citas
- Búsqueda por texto
- Historial por animal
- Agenda por veterinario

### ✅ Validaciones
- No programar en horarios ocupados
- Validar horario laboral (8:00 AM - 6:30 PM)
- No permitir citas los domingos
- No programar en el pasado
- Veterinarios y animales activos

## Mensajes de Error Mejorados

Ahora en lugar de "veterinario no disponible" genérico, verás mensajes específicos:

- ❌ "El animal seleccionado no existe o no está activo"
- ❌ "El veterinario seleccionado no existe o no está activo"  
- ❌ "No se pueden programar citas en el pasado"
- ❌ "El veterinario no está disponible en el horario seleccionado. Ya tiene una cita programada cerca de esta hora"

## Notas Importantes

⚠️ **Backup**: Se recomienda hacer backup de la base de datos antes de ejecutar el script de instalación.

⚠️ **Datos Existentes**: El script preserva todos los diagnósticos existentes. Los estados antiguos siguen siendo válidos.

⚠️ **Usuarios y Animales**: Para usar el módulo de citas, debe haber al menos:
- Un usuario con rol VETERINARIO o ADMIN activo
- Un animal registrado y activo

## Verificación Post-Instalación

Ejecutar esta consulta para verificar que la instalación fue exitosa:

```sql
-- Verificar estados permitidos
SELECT CONSTRAINT_NAME, CHECK_CLAUSE 
FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS 
WHERE TABLE_NAME = 'Diagnostico' AND CONSTRAINT_NAME = 'CK_Diagnostico_Estado';

-- Probar programación de cita
EXEC SP_ProgramarCita 
    @IdAnimal = 1,
    @IdVeterinario = 1, 
    @FechaCita = '2025-08-27 15:00:00',
    @Motivo = 'Consulta de prueba';
```

Si todo está correcto, deberías ver la nueva restricción CHECK y poder programar citas sin errores.