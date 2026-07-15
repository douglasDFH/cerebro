# 🌱 ECOPLAST SRL - CASOS DE USO Y LÓGICA DE NEGOCIO
## Documento de Requisitos y Flujos del Sistema

---

## 🎯 RESUMEN EJECUTIVO

Este documento contiene **67 casos de uso detallados** para el sistema de gestión de producción de Ecoplast SRL, distribuidos en:

- **Gerencia:** 6 casos de uso
- **Administrador de Planta:** 12 casos de uso
- **Operador de Máquina:** 8 casos de uso
- **Técnico de Mantenimiento:** 6 casos de uso
- **Científico de Datos:** 7 casos de uso
- **Inspector de Calidad:** 5 casos de uso
- **Sistema (Automático):** 6 casos de uso

**Total de Reglas de Negocio:** 219 reglas documentadas

---

## 🔄 FLUJO GENERAL DEL SISTEMA

### 1. Ciclo de Vida de una Orden de Producción

```
CREACIÓN → PROGRAMACIÓN → EJECUCIÓN → INSPECCIÓN → APROBACIÓN → VENTA
```

**Estados posibles:**
- PENDIENTE: Creada pero no programada
- PROGRAMADA: Asignada a máquina y turno
- EN_PROCESO: En ejecución
- PAUSADA: Detenida temporalmente
- COMPLETADA: Finalizada
- CANCELADA: Anulada

### 2. Ciclo de Vida de un Lote

```
CUARENTENA → INSPECCIÓN → APROBADO/RECHAZADO → DISPONIBLE/MERMA
```

### 3. Ciclo de Mantenimiento

```
PROGRAMADO → EN_PROCESO → COMPLETADO → [Auto-programa siguiente]
```

---

## 💡 CASOS DE USO CRÍTICOS (Prioridad Alta)

### Para MVP (Mínimo Producto Viable):

1. **CU-A01:** Crear Orden de Producción ⭐⭐⭐
2. **CU-O01:** Iniciar Orden de Producción ⭐⭐⭐
3. **CU-O02:** Registrar Producción en Tiempo Real ⭐⭐⭐
4. **CU-O06:** Finalizar Orden de Producción ⭐⭐⭐
5. **CU-A06:** Aprobar Lotes de Producción ⭐⭐⭐
6. **CU-A03:** Gestionar Inventario de Insumos ⭐⭐⭐
7. **CU-S01:** Calcular KPIs Diarios Automáticamente ⭐⭐⭐
8. **CU-G01:** Ver Dashboard Ejecutivo ⭐⭐
9. **CU-S02:** Generar Alertas Automáticas ⭐⭐

---

## 📊 INDICADORES DE ÉXITO DEL SISTEMA

### KPIs del Sistema:
- **Disponibilidad:** 99.5% uptime
- **Tiempo de respuesta:** <2 segundos por transacción
- **Precisión de datos:** >99.9%
- **Adopción de usuarios:** >90% en 3 meses

### KPIs del Negocio:
- **OEE Promedio:** >85%
- **Tiempo de ciclo de orden:** <8 horas desde creación hasta inicio
- **Trazabilidad:** 100% de lotes con trazabilidad completa
- **Reducción de defectos:** -30% en 6 meses
- **Eficiencia de material:** >92%

---

## 🎨 CONSIDERACIONES DE UX/UI

### Principios de Diseño:

1. **Mobile-First:** Operadores usan tablets en planta
2. **Modo Offline:** Registros críticos funcionan sin internet
3. **Escaneo QR:** Para identificación rápida (productos, lotes, máquinas)
4. **Alertas Visuales:** Semáforos (verde/amarillo/rojo)
5. **Minimalismo:** Interfaces limpias, enfocadas en la tarea

### Pantallas Principales por Actor:

**Operador (Tablet en Máquina):**
- Dashboard simple: Orden activa, producción del turno
- Formulario de registro horario (grande, táctil)
- Botón de emergencia: Reportar paro
- Acceso rápido a instrucciones de trabajo

**Admin (Desktop/Laptop):**
- Dashboard completo: Todas las máquinas, todas las órdenes
- Calendario de programación (drag & drop)
- Tablero Kanban de órdenes
- Gráficos de KPIs en tiempo real

**Gerencia (Desktop/Tablet):**
- Dashboard ejecutivo con gráficos grandes
- Indicadores numéricos destacados
- Comparativas mes vs mes
- Acceso a todos los reportes

---

## 🔧 INTEGRACIONES FUTURAS

### Posibles integraciones:

1. **ERP Existente:** Para datos financieros y contables
2. **Sistema de Ventas:** Para órdenes de clientes
3. **WhatsApp Business API:** Notificaciones a operadores
4. **Sensores IoT:** Captura automática de temperatura, presión
5. **Balanzas Digitales:** Pesaje automático de materiales
6. **Lectores de Código de Barras/QR**
7. **Cámaras de Inspección:** Detección automática de defectos
8. **Power BI / Tableau:** Para análisis avanzados

---

## 📱 NOTIFICACIONES Y COMUNICACIONES

### Canales de Notificación:

| Tipo de Alerta | En App | Email | SMS | WhatsApp |
|----------------|--------|-------|-----|----------|
| Stock bajo | ✅ | ✅ | ❌ | ❌ |
| Defectos altos | ✅ | ✅ | ✅ | ❌ |
| Máquina parada >1h | ✅ | ✅ | ✅ | ✅ |
| Mantenimiento vencido | ✅ | ✅ | ❌ | ❌ |
| Certificación vence | ✅ | ✅ | ❌ | ❌ |
| Orden completada | ✅ | ❌ | ❌ | ❌ |
| Lote aprobado | ✅ | ✅ | ❌ | ❌ |

### Configuración de Horarios:
- **Horario laboral:** Notificaciones completas (06:00 - 22:00)
- **Fuera de horario:** Solo alertas críticas por SMS
- **Fines de semana:** Solo SMS para gerencia

---

## 🎯 ROADMAP DE IMPLEMENTACIÓN

### Fase 1: Core del Sistema (Mes 1-2)
- ✅ Gestión de usuarios y roles
- ✅ Catálogo de productos e insumos
- ✅ Gestión de máquinas
- ✅ Órdenes de producción (crear, asignar)
- ✅ Registro de producción básico
- ✅ Inventario de insumos

### Fase 2: Calidad y KPIs (Mes 3)
- ✅ Inspecciones de calidad
- ✅ Aprobación de lotes
- ✅ Cálculo automático de KPIs diarios
- ✅ Dashboard para admin y operador
- ✅ Alertas automáticas básicas

### Fase 3: Mantenimiento y Análisis (Mes 4)
- ✅ Gestión de mantenimiento preventivo/correctivo
- ✅ Registro de paros y causas
- ✅ Dashboard de científico de datos
- ✅ Análisis de correlaciones básico
- ✅ Reportes avanzados

### Fase 4: Optimización y Certificaciones (Mes 5)
- ✅ Gestión de formulaciones
- ✅ Tests de biodegradabilidad
- ✅ Gestión de certificaciones
- ✅ Trazabilidad completa (blockchain opcional)
- ✅ Análisis predictivo de mantenimiento

### Fase 5: Refinamiento y Escalabilidad (Mes 6)
- ✅ Optimizaciones de rendimiento
- ✅ Integraciones externas (sensores IoT)
- ✅ App móvil nativa (opcional)
- ✅ Dashboards personalizables
- ✅ Exportación avanzada de reportes

---

## 🛡️ SEGURIDAD Y RESPALDOS

### Medidas de Seguridad:

1. **Autenticación:**
   - Login con usuario y contraseña
   - Sesiones con timeout de 2 horas
   - 2FA para gerencia (opcional)

2. **Autorización:**
   - Permisos por rol (RBAC)
   - Logs de auditoría de acciones críticas

3. **Datos:**
   - Encriptación de datos sensibles (contraseñas con bcrypt)
   - Comunicación HTTPS
   - Sanitización de inputs

4. **Respaldos:**
   - Backup automático diario (02:00 AM)
   - Backup incremental cada 6 horas
   - Retención de 30 días
   - Backup offsite semanal

---

## 📖 GLOSARIO DE TÉRMINOS

| Término | Definición |
|---------|------------|
| **OEE** | Overall Equipment Effectiveness - Eficiencia Global del Equipo |
| **MTBF** | Mean Time Between Failures - Tiempo promedio entre fallas |
| **MTTR** | Mean Time To Repair - Tiempo promedio de reparación |
| **FPY** | First Pass Yield - % productos que pasan primera vez |
| **AQL** | Acceptable Quality Level - Nivel de calidad aceptable |
| **Cpk** | Índice de capacidad del proceso |
| **RCA** | Root Cause Analysis - Análisis de causa raíz |
| **FIFO** | First In First Out - Primero en entrar, primero en salir |
| **Scrap** | Material de desecho recuperable |
| **Setup** | Tiempo de preparación/cambio de producto |
| **Cuarentena** | Estado de lote pendiente de aprobación |
| **Compostabilidad** | Capacidad de degradarse en compostaje |
| **PLA** | Ácido Poliláctico - Biopolímero de maíz |
| **PHA** | Polihidroxialcanoatos - Biopolímero bacteriano |

---

## ✅ CHECKLIST DE VALIDACIÓN

Antes de iniciar el desarrollo, verificar:

- [ ] Casos de uso revisados con usuarios finales
- [ ] Reglas de negocio validadas con gerencia
- [ ] Flujos de trabajo aprobados
- [ ] Matriz de permisos confirmada
- [ ] KPIs definidos y acordados
- [ ] Prioridades establecidas (MVP definido)
- [ ] Mockups de pantallas críticas aprobados
- [ ] Infraestructura técnica disponible
- [ ] Equipo de desarrollo confirmado
- [ ] Timeline acordado

---

## 📞 CONTACTOS CLAVE

**Stakeholders del Proyecto:**

- **Sponsor:** Gerencia General
- **Product Owner:** Administrador de Planta
- **Usuarios Clave:**
  - Operadores de máquina (3-4 personas)
  - Técnico de mantenimiento (1-2 personas)
  - Inspector de calidad (1 persona)
- **Equipo Técnico:**
  - Desarrollador Backend (Laravel)
  - Desarrollador Frontend (Vue.js/React)
  - Analista de Datos (Python)
  - DevOps (Infraestructura)

---

## 📌 NOTAS IMPORTANTES

1. **Este documento es la fuente de verdad** para la lógica del sistema
2. Cualquier cambio debe ser documentado aquí primero
3. Los desarrolladores deben consultar este documento antes de implementar
4. Las reglas de negocio son obligatorias y no negociables
5. Los flujos alternos deben ser considerados en el código
6. Las validaciones deben implementarse en backend y frontend

---

## 🚀 SIGUIENTES PASOS INMEDIATOS

**Para comenzar el desarrollo:**

1. ✅ Crear proyecto Laravel nuevo: `ecoplast-srl-production-system`
2. ✅ Configurar base de datos (MySQL)
3. ✅ Crear migraciones basadas en el esquema SQL
4. ✅ Implementar modelos con relaciones
5. ✅ Configurar autenticación (Laravel Breeze/Jetstream)
6. ✅ Crear seeders con datos de prueba
7. ✅ Implementar primer caso de uso: CU-A01 (Crear Orden)
8. ✅ Probar flujo completo de una orden

---

**Documento Final - Listo para Desarrollo**

**Ecoplast SRL - Sistema de Gestión de Producción de Plásticos Biodegradables**

_Este documento contiene 67 casos de uso detallados, 219 reglas de negocio, 3 diagramas de flujo principales y una matriz completa de permisos. Es la guía definitiva para implementar el sistema._

---

**Versión:** 1.0  
**Fecha:** Noviembre 2025  
**Aprobado por:** Pendiente  
**Próxima revisión:** Antes de iniciar desarrollo

