# Proyecto de Desarrollo de Software — App Móvil de Gestión de Club de Atletismo
**Universidad Privada Domingo Savio — Carrera de Ingeniería de Sistemas**
**Santa Cruz de la Sierra, Bolivia · 2026**

---

## Índice General

1. [Entrevista al atleta (fuente de requerimientos)](#1-entrevista-al-atleta)
2. [Historias de Usuario (HU-01 a HU-13)](#2-historias-de-usuario)
3. [Requisitos Funcionales (RF-01 a RF-18)](#3-requisitos-funcionales)
4. [Requisitos No Funcionales (RNF-01 a RNF-06)](#4-requisitos-no-funcionales)
5. [Casos de Uso (CU-01 a CU-06)](#5-casos-de-uso)
6. [Modelado del Dominio — Diagrama de Clases Conceptual](#6-modelado-del-dominio)
7. [Capítulo 4 — Diseño del Software](#7-diseño-del-software)
   - 4.1 Arquitectura de 3 capas
   - 4.2 Modelo Lógico de BD
   - 4.3 Diccionario de Datos
   - 4.4 Diseño de Componentes y Módulos
8. [Capítulo 5 — Implementación y Pruebas (Java / Spring Boot)](#8-implementación-y-pruebas)
   - 5.1 Entorno de implementación
   - 5.2 Proceso de desarrollo (Scrum)
   - 5.3 Estructura del proyecto
   - 5.4 Pruebas y validación

---

## 1. Entrevista al Atleta

**Entrevistado:** Marco Antonio Gutiérrez — Atleta/Gimnasta, Club Atlético Santa Cruz de la Sierra, Bolivia.

### Bloque 1 — Contexto del club

**P1. ¿Cuántos atletas tiene el club y cómo están organizados?**
Somos alrededor de 45 atletas en total. Divididos en categorías: Pre-Infantil (8-10 años), Infantil (11-13), Juvenil (14-17) y Mayores (+18). Disciplinas: velocidad (100m, 200m), salto largo, lanzamiento de bala y gimnasia artística. Hay 4 grupos de entrenamiento según disciplina y entrenador asignado.

**P2. ¿Quiénes usan la información del club?**
Principalmente los entrenadores. Los atletas mayores también necesitamos ver horarios. Los padres de los menores a veces preguntan por WhatsApp porque no tienen acceso oficial a nada.

**P3. ¿Hay diferentes roles con diferentes permisos?**
Sí. El entrenador ve marcas de todos los atletas, puede modificar horarios y resultados. El atleta debería ver solo su propia información y la agenda general. Los padres deberían poder ver la agenda y el rendimiento de su hijo.

### Bloque 2 — Gestión de agenda y entrenamientos

**P4. ¿Cómo organizan hoy los turnos y horarios?**
Usamos un grupo de WhatsApp y a veces el entrenador manda foto de un papel escrito a mano con el horario semanal. No hay nada formal.

**P5. ¿Con qué frecuencia cambia la agenda?**
Cambia bastante, al menos una o dos veces por semana por clima, disponibilidad de la pista o competencias. Todos necesitan enterarse rápido.

**P6. ¿Necesitan registrar la asistencia?**
Sí, sería clave. Ahora el entrenador lleva planilla en papel y a veces la pasa a Excel, pero se pierde información.

**P7. ¿Gestionan competencias o torneos?**
Sí, pero es muy desorganizado. La inscripción se hace por mensaje privado al entrenador. No hay registro formal de resultados — solo fotos del cronómetro guardadas en el teléfono.

### Bloque 3 — Seguimiento del rendimiento

**P8. ¿Registran los tiempos y marcas?**
Las anota el entrenador en una libreta o en WhatsApp. A veces en Excel, pero no siempre. Se pierde mucho historial.

**P9. ¿Los entrenadores necesitan ver la evolución?**
Sí, es fundamental para planificar el entrenamiento y detectar si un atleta está bajando el rendimiento.

**P10. ¿Los atletas deben poder ver su historial?**
Sí. Yo quiero saber si mejoré en el 100m respecto al mes pasado. Eso nos motiva.

### Bloque 4 — Comunicación y notificaciones

**P11. ¿Cómo avisan cuando hay un cambio?**
Todo por WhatsApp. A veces el mensaje se pierde entre otros y alguien no se entera del cambio de horario.

**P12. ¿Les gustaría notificaciones automáticas?**
Sí. Principalmente para: cambio de horario, cancelación de entrenamiento, convocatoria a competencia y publicación de resultados.

**P13. ¿Necesitan un canal de mensajes interno?**
Con notificaciones alcanza por ahora. Un chat interno sería bueno a futuro pero no es urgente.

### Bloque 5 — Seguridad y privacidad

**P14. ¿Qué tan sensible es la información?**
Hay menores de edad, así que es importante proteger sus datos. No queremos que cualquiera vea información personal de los chicos.

**P15. ¿Cada atleta debería tener su cuenta?**
Sí. Los menores pueden tener la cuenta a nombre del padre/tutor.

**P16. ¿Quién puede modificar información del atleta?**
El entrenador debería poder modificar todo. El atleta puede editar solo sus datos personales básicos, no sus marcas.

### Bloque 6 — Prioridades

**P17. Las 3 funciones más importantes:**
1. Ver y recibir notificaciones de la agenda de entrenamientos
2. Registro y consulta de marcas/rendimiento por atleta
3. Gestión de asistencia

**P18. ¿Experiencia con otras apps?**
He visto TeamApp y una que usaban en Cochabamba pero era muy complicada y en inglés. Lo que no me gustó: demasiados menús, difícil de entender para los chicos más jóvenes.

**P19. ¿App simple o completa?**
Prefiero algo simple y directo. Que sea intuitivo, bonito, y que funcione bien con internet lento que a veces tenemos en la cancha.

**P20. ¿Qué problema definitivamente debe resolver la app?**
Lo que más tiempo nos hace perder: confirmar si hay entrenamiento o no. A veces llego a la pista y estaba cancelado y nadie avisó a tiempo. Eso definitivamente tiene que resolverlo la app.

---

## 2. Historias de Usuario

> **Formato:** Como [rol], quiero [funcionalidad] para [beneficio].

### Módulo M1 — Autenticación y Gestión de Usuarios

---

#### HU-01 — Registro de cuenta de usuario
| Campo | Detalle |
|---|---|
| **Rol** | Atleta / Padre de atleta menor |
| **Historia** | Como Atleta/Padre, quiero registrarme con mi correo y contraseña en la app para tener acceso seguro a mis datos y los de mi hijo. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El sistema permite registrarse con nombre completo, correo electrónico y contraseña.
- La contraseña debe tener mínimo 8 caracteres, una mayúscula y un número.
- El sistema envía un correo de verificación antes de activar la cuenta.
- Si el correo ya está registrado, el sistema muestra un mensaje de error claro.
- Para atletas menores, el tutor puede vincular el perfil del menor a su cuenta.

**Criterios de calidad:**
- **Seguridad:** Contraseñas almacenadas con hash (bcrypt o equivalente).
- **Usabilidad:** Formulario completado en menos de 2 minutos.
- **Disponibilidad:** El proceso de registro funciona sin conexión (modo cola).

---

#### HU-02 — Inicio de sesión
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador / Atleta / Padre |
| **Historia** | Como Entrenador/Atleta/Padre, quiero iniciar sesión con mi correo y contraseña para acceder a las funcionalidades correspondientes a mi rol. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El sistema autentica al usuario con correo y contraseña.
- Si las credenciales son incorrectas, muestra mensaje de error sin revelar cuál campo falló.
- Después de 5 intentos fallidos, bloquea la cuenta temporalmente por 15 minutos.
- El sistema recuerda la sesión del usuario por 30 días si marca "Recordarme".
- Cada rol redirige a una pantalla de inicio diferente.

**Criterios de calidad:**
- **Seguridad:** Uso de JWT o token seguro para manejo de sesiones.
- **Rendimiento:** Inicio de sesión en menos de 3 segundos con conexión normal.

---

### Módulo M2 — Gestión de Agenda y Entrenamientos

---

#### HU-03 — Consultar agenda semanal de entrenamientos
| Campo | Detalle |
|---|---|
| **Rol** | Atleta |
| **Historia** | Como Atleta, quiero ver la agenda semanal de entrenamientos en la app para saber con anticipación cuándo y dónde es el próximo entrenamiento sin depender de WhatsApp. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- La agenda muestra los entrenamientos de la semana actual con fecha, hora, lugar y grupo.
- El atleta puede navegar hacia la semana siguiente o anterior.
- Los entrenamientos cancelados aparecen tachados con etiqueta "CANCELADO".
- La vista funciona correctamente en pantallas de 5 pulgadas o más.
- Si no hay entrenamientos, muestra "Sin entrenamientos esta semana".

**Criterios de calidad:**
- **Usabilidad:** Información principal visible sin hacer scroll.
- **Rendimiento:** Agenda carga en menos de 2 segundos con conexión 3G.

---

#### HU-04 — Crear y editar sesión de entrenamiento
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero crear, editar y cancelar sesiones de entrenamiento en la agenda para mantener a todos los atletas informados de forma centralizada sin usar WhatsApp. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El entrenador puede crear una sesión con: fecha, hora inicio/fin, lugar, grupo y descripción.
- Puede editar cualquier campo de una sesión existente.
- Puede cancelar una sesión indicando el motivo (clima, pista no disponible, otro).
- Al guardar cualquier cambio, el sistema envía notificación push a los atletas del grupo.
- No se pueden crear sesiones en fechas pasadas.

**Criterios de calidad:**
- **Seguridad:** Solo usuarios con rol Entrenador o Administrador pueden crear/editar sesiones.
- **Eficiencia:** Formulario de creación completable en menos de 1 minuto.

---

#### HU-05 — Registro de asistencia
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero registrar la asistencia de los atletas a cada sesión de entrenamiento para tener un historial digital sin depender de planillas en papel. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El entrenador accede a la lista de atletas del grupo desde la sesión del día.
- Puede marcar a cada atleta como: Presente, Ausente o Justificado.
- La asistencia se puede registrar hasta 2 horas después de finalizada la sesión.
- El sistema muestra un resumen del % de asistencia por sesión.
- Una vez guardada, la asistencia solo puede modificarla un Administrador.

**Criterios de calidad:**
- **Eficiencia:** Registrar asistencia de 15 atletas en menos de 3 minutos.
- **Mantenibilidad:** El historial de asistencia se conserva mínimo 1 año.

---

### Módulo M3 — Seguimiento del Rendimiento

---

#### HU-06 — Registro de marcas y resultados
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero registrar las marcas y tiempos obtenidos por cada atleta en cada sesión o competencia para contar con un historial digital preciso. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El entrenador selecciona al atleta, la disciplina, la fecha y registra el resultado.
- Se puede asociar el resultado a un entrenamiento o a una competencia específica.
- El sistema admite registros para: 100m, 200m, 400m, salto largo, lanzamiento de bala y gimnasia.
- Si el resultado es el mejor del atleta, el sistema lo marca automáticamente como "Marca Personal".
- Los registros no pueden ser modificados por el propio atleta.

**Criterios de calidad:**
- **Seguridad:** Solo Entrenador/Admin pueden crear o modificar registros de rendimiento.
- **Integridad:** No se permiten tiempos negativos o fuera de rangos lógicos.

---

#### HU-07 — Consultar historial de rendimiento propio
| Campo | Detalle |
|---|---|
| **Rol** | Atleta |
| **Historia** | Como Atleta, quiero ver mi historial de marcas y tiempos a lo largo del tiempo para monitorear mi progreso personal y mantenerme motivado para mejorar. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El atleta visualiza su historial de resultados ordenado por fecha (más reciente primero).
- Puede filtrar por disciplina.
- Se muestra una gráfica de evolución con los últimos 10 registros por disciplina.
- La mejor marca personal aparece destacada con un ícono especial.
- El atleta no puede ver los registros de otros atletas.

**Criterios de calidad:**
- **Usabilidad:** Gráfica legible en pantalla de celular sin necesidad de zoom.
- **Rendimiento:** Historial de hasta 100 registros carga en menos de 3 segundos.
- **Privacidad:** Solo el propio atleta, su entrenador y tutor pueden ver el historial.

---

#### HU-08 — Ver evolución de atletas del grupo
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero ver la evolución de rendimiento de todos los atletas de mi grupo para identificar quién está mejorando y planificar entrenamientos específicos. |
| **Prioridad** | MEDIA |

**Criterios de aceptación:**
- El entrenador selecciona un grupo y disciplina para ver la comparativa.
- Puede ver la evolución individual de cada atleta con gráfica de línea.
- Puede exportar el listado de marcas del grupo en formato PDF o Excel.
- El sistema resalta en verde los atletas con mejora y en rojo los con retroceso.

---

### Módulo M4 — Gestión de Competencias

---

#### HU-09 — Publicar convocatoria a competencia
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero publicar una convocatoria a competencia con todos los detalles para notificar a los atletas de forma oficial y centralizada. |
| **Prioridad** | MEDIA |

**Criterios de aceptación:**
- El entrenador puede crear una competencia con: nombre, fecha, lugar, disciplinas y descripción.
- Puede asignar la convocatoria a grupos o atletas específicos.
- Al publicar, se envía notificación push automática a los convocados.
- Los atletas convocados pueden confirmar o declinar su participación desde la app.
- El entrenador ve en tiempo real cuántos atletas confirmaron asistencia.

---

#### HU-10 — Registrar resultados de competencia
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero registrar los resultados obtenidos por mis atletas en una competencia para tener un historial oficial de resultados por evento. |
| **Prioridad** | MEDIA |

**Criterios de aceptación:**
- El entrenador accede a la competencia y registra el resultado de cada atleta participante.
- Se registra: posición final, marca obtenida, observaciones.
- Los resultados se asocian automáticamente al historial de rendimiento individual.
- Si la marca supera el récord personal, el sistema lo indica automáticamente.

---

### Módulo M5 — Notificaciones

---

#### HU-11 — Recibir notificaciones push
| Campo | Detalle |
|---|---|
| **Rol** | Atleta / Padre |
| **Historia** | Como Atleta/Padre, quiero recibir notificaciones push automáticas ante cambios de horario, cancelaciones y convocatorias para enterarme a tiempo sin depender del grupo de WhatsApp. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El sistema envía notificación push cuando: se cancela un entrenamiento, cambia el horario, hay nueva convocatoria o se publican resultados.
- La notificación indica claramente el tipo de novedad y el grupo afectado.
- El usuario puede configurar qué tipo de notificaciones desea recibir.
- Las notificaciones no enviadas se reintentan automáticamente hasta 3 veces.
- El historial de notificaciones es accesible dentro de la app por los últimos 30 días.

**Criterios de calidad:**
- **Rendimiento:** Las notificaciones se entregan en menos de 60 segundos tras el evento.

---

### Módulo M6 — Perfiles y Roles

---

#### HU-12 — Gestionar perfil del atleta
| Campo | Detalle |
|---|---|
| **Rol** | Entrenador |
| **Historia** | Como Entrenador, quiero crear, editar y consultar el perfil completo de cada atleta del club para tener toda la información centralizada y actualizada. |
| **Prioridad** | ALTA |

**Criterios de aceptación:**
- El perfil incluye: nombre completo, fecha de nacimiento, categoría, disciplina, grupo, datos del tutor (si es menor) y foto.
- El entrenador puede editar cualquier campo del perfil.
- Al cumplir años, el sistema actualiza automáticamente la categoría del atleta.
- El entrenador puede desactivar un perfil sin borrar el historial.

**Criterios de calidad:**
- **Privacidad:** Datos de menores solo accesibles por Entrenador, Admin y tutor vinculado.
- **Seguridad:** Cumplimiento con protección de datos de menores (consentimiento del tutor).

---

#### HU-13 — Consultar y editar datos personales propios
| Campo | Detalle |
|---|---|
| **Rol** | Atleta |
| **Historia** | Como Atleta, quiero consultar y actualizar mis datos personales básicos en mi perfil para mantener mi información de contacto actualizada sin tener que pedirle al entrenador. |
| **Prioridad** | BAJA |

**Criterios de aceptación:**
- El atleta puede ver y editar: foto de perfil, número de teléfono y correo.
- No puede modificar: nombre completo, fecha de nacimiento, categoría ni disciplina.
- Los cambios requieren confirmación mediante contraseña antes de guardar.

---

## 3. Requisitos Funcionales

### RF — Gestión de Usuarios y Autenticación

| ID | Nombre | Descripción | Prioridad |
|---|---|---|---|
| RF-01 | Registro de usuario | El sistema debe permitir registrar nuevos usuarios con nombre, correo, contraseña y rol (Administrador, Entrenador, Atleta, Padre/Tutor). Los menores deben vincularse a un tutor. | Alta |
| RF-02 | Autenticación con roles | El sistema debe autenticar usuarios y redirigirlos a vistas diferenciadas según su rol. Las contraseñas deben almacenarse con hash seguro. | Alta |
| RF-03 | Recuperación de contraseña | El sistema debe permitir recuperar la contraseña mediante correo electrónico con enlace válido por 24 horas. | Alta |
| RF-04 | Gestión de perfiles de atletas | El entrenador debe poder crear, editar, desactivar y consultar el perfil completo de cada atleta, incluyendo foto, categoría, disciplina y datos del tutor. | Alta |

### RF — Gestión de Agenda y Entrenamientos

| ID | Nombre | Descripción | Prioridad |
|---|---|---|---|
| RF-05 | Crear y editar sesiones | El entrenador debe poder crear sesiones indicando: fecha, hora inicio/fin, lugar, grupo asignado y descripción. Debe poder editarlas o cancelarlas. | Alta |
| RF-06 | Consultar agenda semanal | Los atletas deben poder visualizar la agenda semanal con navegación por semanas. Las sesiones canceladas deben mostrarse con etiqueta visible. | Alta |
| RF-07 | Registro de asistencia | El entrenador debe poder registrar la asistencia por sesión marcando a cada atleta como Presente, Ausente o Justificado. El sistema debe calcular el porcentaje. | Alta |
| RF-08 | Consultar historial de asistencia | El entrenador puede ver el historial de asistencia por atleta y por sesión. El atleta puede ver su propio historial. | Media |

### RF — Seguimiento del Rendimiento

| ID | Nombre | Descripción | Prioridad |
|---|---|---|---|
| RF-09 | Registrar marcas y resultados | El entrenador debe poder registrar resultados por atleta indicando: disciplina, fecha, valor de la marca y contexto (entrenamiento o competencia). | Alta |
| RF-10 | Consultar historial de rendimiento | El atleta debe poder consultar su historial de marcas por disciplina, ordenado cronológicamente, con gráfica de evolución. | Alta |
| RF-11 | Ver evolución grupal | El entrenador debe poder comparar el rendimiento de todos los atletas de su grupo por disciplina, con indicadores de mejora o retroceso. | Media |
| RF-12 | Detectar marca personal | El sistema debe identificar automáticamente cuándo un nuevo resultado supera la marca personal del atleta y registrarlo con distinción visual. | Media |

### RF — Gestión de Competencias

| ID | Nombre | Descripción | Prioridad |
|---|---|---|---|
| RF-13 | Publicar convocatorias | El entrenador debe poder crear convocatorias con: nombre del evento, fecha, lugar, disciplinas y atletas/grupos convocados. | Media |
| RF-14 | Confirmación de participación | Los atletas convocados deben poder confirmar o declinar su participación desde la app. | Media |
| RF-15 | Registrar resultados de competencia | El entrenador debe poder ingresar los resultados de cada atleta asociados a la competencia correspondiente. | Media |

### RF — Notificaciones y Comunicación

| ID | Nombre | Descripción | Prioridad |
|---|---|---|---|
| RF-16 | Notificaciones push automáticas | El sistema debe enviar notificaciones push automáticas ante: cancelación de sesión, cambio de horario, nueva convocatoria y publicación de resultados. | Alta |
| RF-17 | Configuración de notificaciones | El usuario debe poder configurar qué tipos de notificaciones desea recibir, activándolas o desactivándolas por categoría. | Media |
| RF-18 | Historial de notificaciones | El sistema debe conservar las notificaciones enviadas al usuario durante los últimos 30 días. | Baja |

---

## 4. Requisitos No Funcionales

| ID | Categoría | Requisitos |
|---|---|---|
| RNF-01 | Rendimiento | La app debe cargar la pantalla principal en < 3 segundos con 3G. La agenda semanal en < 2 segundos. Las notificaciones push en < 60 segundos. El sistema debe soportar al menos 200 usuarios simultáneos. |
| RNF-02 | Seguridad | Contraseñas con hash bcrypt. Sesiones con JWT con expiración. Datos de menores protegidos. Comunicaciones sobre HTTPS/TLS. Bloqueo tras 5 intentos fallidos. |
| RNF-03 | Usabilidad | Utilizable por personas con conocimiento básico en < 10 min. Elementos táctiles mínimo 44×44 pt. Compatible con Android 8.0+ e iOS 13+. Mensajes de error en español. |
| RNF-04 | Disponibilidad | Disponibilidad del 99% mensual. App muestra última agenda cacheada sin conexión. Asistencia registrable offline con sincronización posterior. |
| RNF-05 | Mantenibilidad | Arquitectura en capas documentada. Historial conservado mínimo 3 años. Actualizaciones sin migración manual. Logs de auditoría de operaciones críticas. |
| RNF-06 | Portabilidad | Disponible en Google Play y App Store. Backend deployable en Firebase/AWS/Railway. Exportaciones en PDF y Excel legibles en cualquier dispositivo. |

---

## 5. Casos de Uso

### Actores del sistema

| Actor | Tipo | Descripción |
|---|---|---|
| Administrador | Principal | Gestiona el sistema completo: usuarios, permisos, configuración general del club. |
| Entrenador | Principal | Gestiona agenda, asistencia, rendimiento y competencias de su grupo de atletas. |
| Atleta | Principal | Consulta su agenda, historial de rendimiento y recibe notificaciones. |
| Padre / Tutor | Secundario | Consulta la agenda y el rendimiento de su hijo menor. Recibe notificaciones relacionadas. |

---

### CU-01 — Iniciar Sesión

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador, Atleta, Padre/Tutor |
| **Precondición** | El usuario tiene una cuenta registrada y verificada. |
| **Postcondición** | El usuario accede a la vista correspondiente a su rol. |
| **RF relacionados** | RF-01, RF-02 |
| **RNF relacionados** | RNF-02 (Seguridad), RNF-01 (Rendimiento) |

**Flujo principal:**
1. El usuario abre la aplicación móvil.
2. El sistema muestra la pantalla de inicio de sesión.
3. El usuario ingresa su correo electrónico y contraseña.
4. El usuario presiona el botón "Iniciar Sesión".
5. El sistema valida las credenciales contra la base de datos.
6. El sistema genera un token de sesión (JWT) y redirige al usuario a su pantalla de inicio según su rol.

**Flujos alternos:**
- FA-01: Si las credenciales son incorrectas, el sistema muestra "Correo o contraseña incorrectos" sin especificar cuál falló. Vuelve al paso 2.
- FA-02: Si el usuario falló 5 veces, el sistema bloquea la cuenta 15 minutos y muestra aviso.
- FA-03: Si el usuario presiona "¿Olvidaste tu contraseña?", el sistema inicia el flujo de recuperación.

---

### CU-02 — Registrar Atleta

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador / Administrador |
| **Precondición** | El actor ha iniciado sesión con rol Entrenador o Administrador. |
| **Postcondición** | El perfil del atleta queda registrado en el sistema. |
| **RF relacionados** | RF-04 |
| **RNF relacionados** | RNF-02 (Seguridad - privacidad de menores), RNF-03 (Usabilidad) |

**Flujo principal:**
1. El entrenador accede al módulo "Atletas" desde el menú principal.
2. Presiona el botón "Nuevo Atleta".
3. El sistema muestra el formulario de registro.
4. El entrenador ingresa: nombre completo, fecha de nacimiento, disciplina, categoría, grupo y (si es menor) datos del tutor.
5. El entrenador sube la foto del atleta (opcional).
6. El entrenador presiona "Guardar".
7. El sistema valida los datos, genera un ID único para el atleta y guarda el perfil.
8. El sistema muestra confirmación: "Atleta registrado correctamente".

**Flujos alternos:**
- FA-01: Si el nombre ya existe en el club, el sistema muestra advertencia.
- FA-02: Si faltan campos obligatorios, el sistema resalta los campos en rojo y no permite guardar.
- FA-03: Si el atleta es menor y no se ingresan datos del tutor, el sistema solicita confirmación.

---

### CU-03 — Gestionar Agenda de Entrenamientos

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador (principal), Atleta (consulta) |
| **Precondición** | El entrenador ha iniciado sesión. Existen grupos de atletas registrados. |
| **Postcondición** | La sesión queda registrada/modificada y los atletas son notificados. |
| **RF relacionados** | RF-05, RF-06, RF-16 |
| **RNF relacionados** | RNF-01 (Rendimiento), RNF-04 (Disponibilidad offline) |

**Flujo principal:**
1. El entrenador accede al módulo "Agenda".
2. Selecciona la semana deseada en el calendario.
3. Presiona "Nueva Sesión".
4. El sistema muestra el formulario: fecha, hora inicio/fin, lugar, grupo, descripción.
5. El entrenador completa los campos y presiona "Guardar".
6. El sistema valida que no exista conflicto de horario para el mismo grupo.
7. La sesión se registra y el sistema envía notificación push al grupo asignado.

**Flujos alternos:**
- FA-01 (Cancelar sesión): El entrenador selecciona una sesión existente, elige "Cancelar" e ingresa el motivo. El sistema actualiza el estado y notifica a los atletas.
- FA-02 (Editar sesión): Si cambia la hora, el sistema re-notifica al grupo.
- FA-03 (Conflicto de horario): El sistema muestra advertencia.

---

### CU-04 — Registrar Asistencia

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador |
| **Precondición** | Existe al menos una sesión programada para el día. |
| **Postcondición** | La asistencia queda registrada y el sistema actualiza el porcentaje de cada atleta. |
| **RF relacionados** | RF-07, RF-08 |
| **RNF relacionados** | RNF-04 (Disponibilidad offline), RNF-03 (Usabilidad en campo) |

**Flujo principal:**
1. El entrenador accede al módulo "Asistencia" o desde la sesión del día.
2. El sistema muestra la lista de atletas del grupo asignado.
3. El entrenador marca a cada atleta como: Presente, Ausente o Justificado.
4. El entrenador presiona "Guardar Asistencia".
5. El sistema registra la asistencia, calcula el % del grupo y actualiza el historial individual.
6. El sistema muestra el resumen: X presentes, Y ausentes, Z justificados.

**Flujos alternos:**
- FA-01 (Sin conexión): El registro se guarda localmente y se sincroniza al recuperar conexión.
- FA-02 (Registro tardío): El entrenador puede registrar asistencia hasta 2 horas después de finalizada la sesión.
- FA-03 (Corrección posterior): Solo el Administrador puede modificar un registro ya guardado.

---

### CU-05 — Registrar y Consultar Rendimiento

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador (registro), Atleta (consulta) |
| **Precondición** | El atleta tiene un perfil registrado. |
| **Postcondición** | La marca queda registrada en el historial del atleta. |
| **RF relacionados** | RF-09, RF-10, RF-11, RF-12 |
| **RNF relacionados** | RNF-02 (Seguridad), RNF-01 (Rendimiento gráficas) |

**Flujo principal:**
1. El entrenador accede al módulo "Rendimiento".
2. Selecciona al atleta y la disciplina.
3. Ingresa la fecha, el valor de la marca y el contexto (entrenamiento o competencia).
4. Presiona "Registrar".
5. El sistema compara el resultado con el historial del atleta.
6. Si el valor supera la marca personal anterior, el sistema lo marca automáticamente como "Marca Personal".
7. El registro queda guardado y visible en el historial del atleta.
8. El atleta puede acceder a su historial y ver la gráfica de evolución.

---

### CU-06 — Publicar Convocatoria a Competencia

| Campo | Detalle |
|---|---|
| **Actores** | Entrenador |
| **Precondición** | Existen atletas registrados en el sistema. |
| **Postcondición** | La convocatoria queda publicada y los atletas convocados son notificados. |
| **RF relacionados** | RF-13, RF-14, RF-15, RF-16 |
| **RNF relacionados** | RNF-01 (Rendimiento notificaciones), RNF-03 (Usabilidad) |

**Flujo principal:**
1. El entrenador accede al módulo "Competencias".
2. Presiona "Nueva Competencia".
3. Completa el formulario: nombre, fecha, lugar, disciplinas y grupos/atletas convocados.
4. Presiona "Publicar".
5. El sistema guarda la competencia y envía notificación push a los convocados.
6. Los atletas reciben la notificación y pueden confirmar o declinar participación.
7. El entrenador visualiza en tiempo real el estado de confirmaciones.

---

## 6. Modelado del Dominio

### Entidades del sistema (12 clases)

| Entidad | Grupo funcional | Descripción resumida |
|---|---|---|
| Club | Gestión del Club | Entidad raíz que agrupa usuarios y grupos |
| Usuario | Gestión de Usuarios | Base para todos los actores del sistema |
| Atleta | Gestión de Usuarios | Perfil deportivo del atleta |
| Tutor | Gestión de Usuarios | Vinculación adulto-menor para menores de edad |
| GrupoEntrenamiento | Agenda y Entrenamientos | Agrupación de atletas por disciplina y entrenador |
| SesionEntrenamiento | Agenda y Entrenamientos | Entrenamiento programado con estado y lugar |
| RegistroAsistencia | Agenda y Entrenamientos | Presencia de atleta en sesión de entrenamiento |
| RegistroRendimiento | Rendimiento Deportivo | Marca o resultado de un atleta en una disciplina |
| Competencia | Competencias | Evento deportivo oficial con convocatoria |
| Convocatoria | Competencias | Invitación formal de atleta a competencia |
| ResultadoCompetencia | Competencias | Resultado oficial de atleta en competencia |
| Notificacion | Comunicación | Mensaje push automático al usuario |

### Relaciones principales

```
Club ||--o{ Usuario : "tiene"
Club ||--o{ GrupoEntrenamiento : "organiza"
Club ||--o{ Competencia : "realiza"
Usuario ||--o| Atleta : "es (especialización)"
Usuario ||--o{ Tutor : "actúa como"
Atleta ||--o{ Tutor : "tiene (si es menor)"
GrupoEntrenamiento }o--o{ Atleta : "incluye (muchos a muchos)"
GrupoEntrenamiento ||--o{ SesionEntrenamiento : "tiene"
SesionEntrenamiento ||--o{ RegistroAsistencia : "genera"
Atleta ||--o{ RegistroAsistencia : "registra"
Atleta ||--o{ RegistroRendimiento : "acumula"
Atleta ||--o{ Convocatoria : "recibe"
Atleta ||--o{ ResultadoCompetencia : "obtiene"
Competencia ||--o{ Convocatoria : "genera"
Competencia ||--o{ ResultadoCompetencia : "produce"
Usuario ||--o{ Notificacion : "recibe"
```

### Atributos clave por entidad

#### Usuario
```
id: SERIAL PK
club_id: INT FK → club.id
nombre_completo: VARCHAR(150) NOT NULL
correo: VARCHAR(200) NOT NULL UNIQUE
contrasena_hash: VARCHAR(255) NOT NULL
rol: ENUM (Admin, Entrenador, Atleta, Padre)
activo: BOOLEAN DEFAULT true
creado_en: TIMESTAMP DEFAULT NOW()
```

#### Atleta
```
id: SERIAL PK
usuario_id: INT FK → usuario.id UNIQUE
fecha_nacimiento: DATE NOT NULL
categoria: ENUM (Pre-infantil, Infantil, Juvenil, Mayores)
disciplina_principal: VARCHAR(60) NOT NULL
foto_url: VARCHAR(300)
activo: BOOLEAN DEFAULT true
```

#### SesionEntrenamiento
```
id: SERIAL PK
grupo_id: INT FK → grupo_entrenamiento.id
hora_inicio: TIMESTAMP NOT NULL
hora_fin: TIMESTAMP NOT NULL
lugar: VARCHAR(150) NOT NULL
estado: ENUM (Programada, Activa, Finalizada, Cancelada) DEFAULT 'Programada'
motivo_cancelacion: TEXT
```

#### RegistroRendimiento
```
id: SERIAL PK
atleta_id: INT FK → atleta.id
disciplina: VARCHAR(60) NOT NULL
valor_marca: FLOAT NOT NULL
unidad: VARCHAR(20) NOT NULL  -- seg, m, pts
fecha: DATE NOT NULL
es_marca_personal: BOOLEAN DEFAULT false
contexto: ENUM (Entrenamiento, Competencia)
competencia_id: INT FK → competencia.id (nullable)
```

---

## 7. Diseño del Software

### 4.1 Arquitectura del Software — 3 Capas

```
┌─────────────────────────────────────────────────────────────────┐
│                    CAPA CLIENTE (Capa 1)                         │
│              React Native + Expo (Android / iOS)                 │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐           │
│  │ UI/Vistas│ │  Redux   │ │  Caché   │ │   FCM    │           │
│  │ Pantallas│ │  State   │ │ Offline  │ │  Push    │           │
│  └──────────┘ └──────────┘ └──────────┘ └──────────┘           │
│                          ↕ HTTPS + JWT                           │
├─────────────────────────────────────────────────────────────────┤
│                  CAPA SERVIDOR (Capa 2)                          │
│            Java 21 + Spring Boot 3.3 + Maven                    │
│  ┌──────────┐ ┌──────────┐ ┌──────────┐ ┌──────────┐           │
│  │   Auth   │ │  Agenda  │ │Rendimien.│ │Competen. │           │
│  │  Module  │ │  Module  │ │  Module  │ │  Module  │           │
│  └──────────┘ └──────────┘ └──────────┘ └──────────┘           │
│                    ┌──────────────┐                              │
│                    │  Notif. Mod. │ → Firebase FCM               │
│                    └──────────────┘                              │
│                          ↕ JPA + Hikari Pool                     │
├─────────────────────────────────────────────────────────────────┤
│                   CAPA DE DATOS (Capa 3)                         │
│  ┌──────────────┐  ┌───────┐  ┌──────────────────┐             │
│  │ PostgreSQL 16│  │ Redis │  │ Firebase Storage  │             │
│  │ (principal)  │  │ Cache │  │ (fotos atletas)   │             │
│  └──────────────┘  └───────┘  └──────────────────┘             │
└─────────────────────────────────────────────────────────────────┘
```

| Capa | Tecnología | Responsabilidad |
|---|---|---|
| Presentación | React Native + Expo | Interfaz móvil multiplataforma. Estado con Redux, caché offline con AsyncStorage/SQLite, push con FCM. |
| Lógica de negocio | Java 21 + Spring Boot 3.3 | API REST modular. JWT + control de roles. Servicios de agenda, rendimiento, competencias, notificaciones. |
| Datos | PostgreSQL 16 + Redis | BD relacional principal con Hibernate ORM. Redis para caché de sesiones y consultas frecuentes. |
| Almacenamiento | Firebase Storage | Fotos de perfil de atletas. |
| Despliegue | Docker + Railway/Render | Backend en contenedor Docker. Migraciones con Flyway. |

---

### 4.2 Diseño de la Base de Datos — Modelo Lógico

**Tablas y relaciones:**

```sql
-- Tabla de referencia: club
club (id PK, nombre, ciudad, pais, fecha_fundacion)

-- Usuarios del sistema
usuario (id PK, club_id FK, nombre_completo, correo UNIQUE, 
         contrasena_hash, rol ENUM, activo, creado_en)

-- Perfil deportivo
atleta (id PK, usuario_id FK UNIQUE, fecha_nacimiento, 
        categoria ENUM, disciplina_principal, foto_url, activo)

-- Tutores de menores
tutor (id PK, usuario_id FK, atleta_id FK, relacion)

-- Grupos de entrenamiento
grupo_entrenamiento (id PK, club_id FK, entrenador_id FK, 
                     nombre, disciplina ENUM)

-- Tabla pivote atleta ↔ grupo (N:M)
atleta_grupo (atleta_id FK, grupo_id FK, fecha_ingreso) PK(atleta_id, grupo_id)

-- Sesiones programadas
sesion_entrenamiento (id PK, grupo_id FK, hora_inicio, hora_fin, 
                      lugar, estado ENUM, motivo_cancelacion)

-- Asistencia por sesión
registro_asistencia (id PK, sesion_id FK, atleta_id FK, 
                     estado ENUM, registrado_por FK, registrado_en)

-- Marcas deportivas
registro_rendimiento (id PK, atleta_id FK, disciplina, valor_marca FLOAT,
                      unidad, fecha, es_marca_personal, contexto ENUM, 
                      competencia_id FK nullable)

-- Competencias oficiales
competencia (id PK, club_id FK, nombre, fecha, lugar, estado ENUM)

-- Invitaciones a competencias
convocatoria (id PK, competencia_id FK, atleta_id FK, 
              estado_confirmacion ENUM, respondido_en)

-- Resultados oficiales
resultado_competencia (id PK, competencia_id FK, atleta_id FK,
                       posicion, marca_obtenida FLOAT, 
                       es_marca_personal, observaciones)

-- Notificaciones push
notificacion (id PK, usuario_id FK, titulo, mensaje, 
              tipo ENUM, leida, enviado_en)
```

**Convenciones aplicadas:**
- Todas las tablas usan `SERIAL` como PK excepto `atleta_grupo` (PK compuesta).
- Claves foráneas con restricción `ON DELETE RESTRICT` para preservar historial.
- Campos de auditoría con `DEFAULT NOW()`.
- ENUMs implementados como tipos PostgreSQL nativos.
- Índices en: `usuario.correo`, `atleta.usuario_id`, `registro_rendimiento.atleta_id`, `sesion_entrenamiento.grupo_id`.

---

### 4.3 Diccionario de Datos (resumen por tabla)

#### Tabla: `registro_rendimiento` (representativa)

| Columna | Tipo | NN | Clave | FK ref. | Descripción |
|---|---|---|---|---|---|
| id | SERIAL | Sí | PK | — | Identificador único |
| atleta_id | INT | Sí | FK | atleta.id | Atleta al que pertenece la marca |
| disciplina | VARCHAR(60) | Sí | — | — | 100m, 200m, salto largo, etc. |
| valor_marca | FLOAT | Sí | — | — | Valor numérico (segundos o metros) |
| unidad | VARCHAR(20) | Sí | — | — | seg, m, pts |
| fecha | DATE | Sí | — | — | Fecha en que se obtuvo la marca |
| es_marca_personal | BOOLEAN | Sí | — | — | True si supera el récord anterior |
| contexto | ENUM | Sí | — | — | Entrenamiento o Competencia |
| competencia_id | INT | No | FK | competencia.id | Competencia asociada (opcional) |

*(El documento Word incluye el diccionario completo de las 13 tablas)*

---

### 4.4 Diseño de Componentes y Módulos (Spring Boot)

#### AuthModule
- `AuthController` — endpoints: `POST /auth/login`, `POST /auth/register`, `POST /auth/refresh`, `POST /auth/forgot-password`
- `AuthService` — lógica de autenticación y manejo de tokens
- `JwtService` — generación y validación de JWT
- `JwtAuthFilter` — filtro de seguridad en cada request
- `RolesGuard` — middleware de verificación de roles
- `PasswordService` — hash bcrypt y validación

#### AgendaModule
- `SesionController` — `GET /sesiones`, `POST /sesiones`, `PUT /sesiones/{id}`, `DELETE /sesiones/{id}/cancelar`
- `SesionService` — lógica de negocio + validación de conflictos de horario
- `AsistenciaController` — `POST /sesiones/{id}/asistencia`, `GET /sesiones/{id}/asistencia`
- `AsistenciaService` — cálculo de porcentajes + sincronización offline
- `NotificacionTrigger` — dispara push al crear/modificar/cancelar sesión

#### RendimientoModule
- `RendimientoController` — `POST /rendimiento`, `GET /rendimiento/{atleta_id}`, `GET /rendimiento/{atleta_id}/evolucion`
- `RendimientoService` — detección automática de marca personal
- `EvolucionService` — cálculo de gráficas de evolución temporal
- `ReporteService` — exportación PDF/Excel

#### CompetenciasModule
- `CompetenciaController` — `GET /competencias`, `POST /competencias`, `POST /competencias/{id}/convocar`
- `ConvocatoriaController` — `PUT /convocatorias/{id}/responder`
- `ResultadoController` — `POST /competencias/{id}/resultados`, `GET /competencias/{id}/resultados`
- `CompetenciaService` — gestión de estados y sincronización con rendimiento

#### NotificacionModule
- `NotificacionService` — envío push con reintentos automáticos (hasta 3x)
- `FcmService` — integración con Firebase Cloud Messaging API
- `NotificacionController` — `GET /notificaciones`, `PUT /notificaciones/{id}/leer`

---

## 8. Implementación y Pruebas

### 5.1 Entorno de Implementación

#### Backend — Java / Spring Boot

| Tecnología | Versión | Rol |
|---|---|---|
| Java (JDK) | 21 LTS | Lenguaje principal del backend. Virtual threads, records, pattern matching. |
| Spring Boot | 3.3.x | Framework principal. Autoconfiguración, servidor Tomcat embebido, módulos REST/Security/JPA. |
| Spring Security | 6.x | Autenticación y autorización. JWT stateless + control de roles por endpoint con `@PreAuthorize`. |
| Spring Data JPA | 3.3.x | Repositorios JPA para CRUD sin SQL manual en operaciones estándar. |
| Hibernate ORM | 6.x | Implementación JPA. Mapeo objeto-relacional con PostgreSQL. |
| PostgreSQL | 16 | Base de datos relacional principal. Tipos ENUM nativos, índices compuestos. |
| Redis | 7.x | Caché en memoria. Sesiones activas, tokens de refresco, consultas frecuentes. |
| Maven | 3.9.x | Gestión de dependencias y ciclo de vida. |
| Lombok | 1.18.x | Generación automática de getters, setters, constructores, builders. |
| MapStruct | 1.5.x | Mappers entre DTOs y entidades sin overhead de reflexión. |
| Firebase Admin SDK | 9.x (Java) | Envío de notificaciones push via FCM desde el servidor. |
| Flyway | 9.x | Migraciones de BD versionadas. Se aplican automáticamente al arrancar. |
| JUnit 5 | 5.10.x | Framework de pruebas unitarias e integración. |
| Mockito | 5.x | Mocking de dependencias en pruebas unitarias de servicios. |
| Docker | 24.x | Containerización del backend. `Dockerfile` + `docker-compose.yml` incluidos. |
| IntelliJ IDEA | 2024.x | IDE principal de desarrollo Java. |

#### Frontend — React Native

| Tecnología | Versión | Rol |
|---|---|---|
| React Native | 0.74.x | Framework móvil multiplataforma (Android/iOS). |
| Expo | 51.x | Plataforma de desarrollo y despliegue simplificado. |
| TypeScript | 5.x | Superset tipado de JavaScript. |
| Redux Toolkit | 2.x | Gestión de estado global (sesión, agenda, marcas). |
| React Navigation | 6.x | Navegación entre pantallas (stack + bottom tabs). |
| Axios | 1.7.x | Cliente HTTP con interceptores JWT automáticos. |
| AsyncStorage | 2.x | Persistencia local: caché de agenda, token de sesión, offline. |
| Expo Notifications | 0.28.x | Recepción de notificaciones push de FCM. |

---

### 5.2 Proceso de Desarrollo — Scrum

| Sprint | Objetivo | Historias | Duración |
|---|---|---|---|
| S-01 | Base del proyecto y autenticación | HU-01, HU-02, HU-12, HU-13 | 2 semanas |
| S-02 | Gestión de agenda | HU-03, HU-04, HU-05 | 2 semanas |
| S-03 | Notificaciones push | HU-11, integración FCM + Spring Boot | 2 semanas |
| S-04 | Seguimiento de rendimiento | HU-06, HU-07, HU-08 | 2 semanas |
| S-05 | Competencias | HU-09, HU-10, exportación PDF | 2 semanas |
| S-06 | Pruebas, ajustes y despliegue | Pruebas unitarias e integración, Docker + Railway | 2 semanas |

**Convenciones de desarrollo:**
- Control de versiones: Git Flow (`main`, `develop`, `feature/HU-XX`, `hotfix/`)
- Estilo de código Java: Google Java Style Guide. PascalCase para clases, camelCase para métodos/variables.
- Arquitectura: Controller → Service → Repository (nunca saltar capas).
- DTOs: toda comunicación entre capas usa Data Transfer Objects. Las entidades JPA nunca se exponen en endpoints.
- Validación: Bean Validation (Jakarta) en todos los DTOs de entrada con `@NotNull`, `@NotBlank`, `@Size`, `@Email`.
- Manejo de errores: `GlobalExceptionHandler` con `@RestControllerAdvice` para respuestas JSON uniformes.
- Pruebas: cobertura mínima del 70% en la capa de servicios.

---

### 5.3 Estructura del Proyecto Java / Spring Boot

#### 5.3.1 Árbol de paquetes

```
atletismo-app/
  ├── src/main/java/com/club/atletismo/
  │   ├── config/               # SecurityConfig, CorsConfig, FcmConfig, RedisConfig
  │   ├── shared/               # BaseEntity, ApiResponse, GlobalExceptionHandler
  │   ├── auth/
  │   │   ├── AuthController.java
  │   │   ├── AuthService.java
  │   │   ├── JwtService.java
  │   │   ├── JwtAuthFilter.java
  │   │   └── dto/              # LoginRequest, RegisterRequest, TokenResponse
  │   ├── usuario/
  │   │   ├── UsuarioController.java
  │   │   ├── UsuarioService.java
  │   │   ├── UsuarioRepository.java
  │   │   ├── Usuario.java
  │   │   └── dto/
  │   ├── atleta/
  │   │   ├── AtletaController.java
  │   │   ├── AtletaService.java
  │   │   ├── AtletaRepository.java
  │   │   ├── Atleta.java
  │   │   └── dto/
  │   ├── agenda/
  │   │   ├── sesion/
  │   │   │   ├── SesionController.java
  │   │   │   ├── SesionService.java
  │   │   │   ├── SesionRepository.java
  │   │   │   └── SesionEntrenamiento.java
  │   │   └── asistencia/
  │   │       ├── AsistenciaController.java
  │   │       ├── AsistenciaService.java
  │   │       ├── AsistenciaRepository.java
  │   │       └── RegistroAsistencia.java
  │   ├── rendimiento/
  │   │   ├── RendimientoController.java
  │   │   ├── RendimientoService.java
  │   │   ├── RendimientoRepository.java
  │   │   ├── RegistroRendimiento.java
  │   │   └── dto/
  │   ├── competencia/
  │   │   ├── CompetenciaController.java
  │   │   ├── CompetenciaService.java
  │   │   ├── CompetenciaRepository.java
  │   │   ├── Competencia.java
  │   │   ├── convocatoria/
  │   │   │   ├── ConvocatoriaController.java
  │   │   │   ├── ConvocatoriaService.java
  │   │   │   ├── ConvocatoriaRepository.java
  │   │   │   └── Convocatoria.java
  │   │   └── resultado/
  │   │       ├── ResultadoController.java
  │   │       ├── ResultadoService.java
  │   │       ├── ResultadoRepository.java
  │   │       └── ResultadoCompetencia.java
  │   └── notificacion/
  │       ├── NotificacionService.java
  │       ├── FcmService.java
  │       ├── NotificacionController.java
  │       └── NotificacionRepository.java
  ├── src/main/resources/
  │   ├── application.yml
  │   └── db/migration/         # V1__init.sql, V2__seed_data.sql ...
  ├── src/test/java/
  │   └── com/club/atletismo/   # Tests JUnit 5 + Mockito por módulo
  ├── Dockerfile
  ├── docker-compose.yml
  └── pom.xml
```

#### 5.3.2 Fragmentos de código Java

**Entidad JPA — Atleta.java**
```java
@Entity
@Table(name = "atleta")
@Getter @Setter @NoArgsConstructor
public class Atleta extends BaseEntity {

    @OneToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "usuario_id", nullable = false, unique = true)
    private Usuario usuario;

    @Column(nullable = false)
    private LocalDate fechaNacimiento;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private Categoria categoria;

    @Column(nullable = false)
    private String disciplinaPrincipal;

    @Column
    private String fotoUrl;

    @Column(nullable = false)
    private boolean activo = true;
}
```

**Controlador REST — SesionController.java**
```java
@RestController
@RequestMapping("/api/v1/sesiones")
@RequiredArgsConstructor
public class SesionController {

    private final SesionService sesionService;

    @GetMapping
    @PreAuthorize("hasAnyRole('ENTRENADOR','ATLETA','PADRE')")
    public ResponseEntity<List<SesionResponseDTO>> listarPorSemana(
            @RequestParam Long grupoId,
            @RequestParam @DateTimeFormat(iso = DATE) LocalDate semana) {
        return ResponseEntity.ok(sesionService.listarPorSemana(grupoId, semana));
    }

    @PostMapping
    @PreAuthorize("hasRole('ENTRENADOR')")
    public ResponseEntity<SesionResponseDTO> crear(
            @Valid @RequestBody SesionCreateDTO dto) {
        return ResponseEntity.status(HttpStatus.CREATED)
                             .body(sesionService.crear(dto));
    }

    @PutMapping("/{id}/cancelar")
    @PreAuthorize("hasRole('ENTRENADOR')")
    public ResponseEntity<SesionResponseDTO> cancelar(
            @PathVariable Long id,
            @Valid @RequestBody CancelarSesionDTO dto) {
        return ResponseEntity.ok(sesionService.cancelar(id, dto));
    }
}
```

**Servicio — lógica de marca personal en RendimientoService.java**
```java
@Service
@RequiredArgsConstructor
@Transactional
public class RendimientoService {

    private final RendimientoRepository rendimientoRepo;
    private final RendimientoMapper mapper;

    public RendimientoResponseDTO registrar(RendimientoCreateDTO dto) {
        RegistroRendimiento registro = mapper.toEntity(dto);

        // Detectar si supera la marca personal histórica
        Optional<RegistroRendimiento> mejorAnterior = rendimientoRepo
            .findTopByAtletaIdAndDisciplinaOrderByValorMarcaAsc(
                dto.getAtletaId(), dto.getDisciplina());

        boolean esMarcaPersonal = mejorAnterior
            .map(m -> dto.getValorMarca() < m.getValorMarca())
            .orElse(true);

        registro.setEsMarcaPersonal(esMarcaPersonal);
        return mapper.toDTO(rendimientoRepo.save(registro));
    }
}
```

**Configuración de seguridad JWT — SecurityConfig.java**
```java
@Configuration
@EnableMethodSecurity
@RequiredArgsConstructor
public class SecurityConfig {

    private final JwtAuthFilter jwtAuthFilter;

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        return http
            .csrf(AbstractHttpConfigurer::disable)
            .sessionManagement(s ->
                s.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/v1/auth/**").permitAll()
                .anyRequest().authenticated())
            .addFilterBefore(jwtAuthFilter,
                UsernamePasswordAuthenticationFilter.class)
            .build();
    }
}
```

---

### 5.4 Pruebas y Validación

#### 5.4.1 Pruebas unitarias — JUnit 5 + Mockito

| ID | Clase bajo prueba | Caso de prueba | Resultado esperado |
|---|---|---|---|
| PU-01 | AuthService | `login()` con credenciales correctas | Retorna TokenResponse con accessToken y refreshToken válidos |
| PU-02 | AuthService | `login()` con contraseña incorrecta | Lanza `BadCredentialsException` |
| PU-03 | AuthService | `register()` con correo ya existente | Lanza `DuplicateEmailException` HTTP 409 |
| PU-04 | SesionService | `crear()` con conflicto de horario | Lanza `HorarioConflictoException` antes de persistir |
| PU-05 | SesionService | `cancelar()` actualiza estado | Estado cambia a CANCELADA, se persiste motivo |
| PU-06 | SesionService | `cancelar()` dispara notificación push | `NotificacionService.enviarGrupo()` invocado exactamente 1 vez |
| PU-07 | AsistenciaService | `registrar()` calcula porcentaje | 14/18 = 77.78% con precisión de 2 decimales |
| PU-08 | AsistenciaService | `registrar()` fuera del plazo de 2 horas | Lanza `AsistenciaFueraDePlayoException` |
| PU-09 | RendimientoService | `registrar()` marca que supera récord | `es_marca_personal = true` en la entidad persistida |
| PU-10 | RendimientoService | `registrar()` primera marca del atleta | `es_marca_personal = true` sin consulta de historial previo |
| PU-11 | RendimientoService | `registrar()` valor fuera de rango en 100m | Lanza `ValorMarcaInvalidoException` para tiempos < 9.0s o > 30s |
| PU-12 | CompetenciaService | `publicar()` envía convocatoria al grupo | `ConvocatoriaRepository.saveAll()` invocado con lista correcta |
| PU-13 | CompetenciaService | `responderConvocatoria()` por atleta no convocado | Lanza `ConvocatoriaNotFoundException` HTTP 404 |
| PU-14 | NotificacionService | `enviar()` reintenta hasta 3 veces ante fallo FCM | `FcmService.send()` invocado máximo 3 veces |
| PU-15 | JwtService | `validateToken()` con token expirado | Retorna false sin lanzar excepción no controlada |

**Ejemplo de prueba unitaria — RendimientoServiceTest.java**
```java
@ExtendWith(MockitoExtension.class)
class RendimientoServiceTest {

    @Mock  private RendimientoRepository rendimientoRepo;
    @Mock  private RendimientoMapper mapper;
    @InjectMocks private RendimientoService rendimientoService;

    @Test
    @DisplayName("PU-09: registrar() detecta nueva marca personal")
    void registrar_nuevaMarcaPersonal() {
        // Arrange
        RendimientoCreateDTO dto = new RendimientoCreateDTO();
        dto.setAtletaId(1L);
        dto.setDisciplina("100m");
        dto.setValorMarca(10.92);

        RegistroRendimiento marcaAnterior = new RegistroRendimiento();
        marcaAnterior.setValorMarca(11.05);

        RegistroRendimiento nuevoRegistro = new RegistroRendimiento();
        when(mapper.toEntity(dto)).thenReturn(nuevoRegistro);
        when(rendimientoRepo
            .findTopByAtletaIdAndDisciplinaOrderByValorMarcaAsc(1L, "100m"))
            .thenReturn(Optional.of(marcaAnterior));
        when(rendimientoRepo.save(any())).thenReturn(nuevoRegistro);
        when(mapper.toDTO(nuevoRegistro))
            .thenReturn(new RendimientoResponseDTO());

        // Act
        rendimientoService.registrar(dto);

        // Assert
        assertTrue(nuevoRegistro.isEsMarcaPersonal(),
            "Debería marcarse como marca personal");
        verify(rendimientoRepo, times(1)).save(nuevoRegistro);
    }
}
```

---

#### 5.4.2 Pruebas de integración — @SpringBootTest

| ID | Endpoint | Escenario | Resultado esperado |
|---|---|---|---|
| PI-01 | POST /api/v1/auth/login | Credenciales válidas de entrenador | HTTP 200, body contiene accessToken y rol = ENTRENADOR |
| PI-02 | POST /api/v1/auth/login | Credenciales inválidas | HTTP 401 con mensaje "Credenciales inválidas" |
| PI-03 | GET /api/v1/sesiones | Sin token JWT en header | HTTP 403 Forbidden |
| PI-04 | GET /api/v1/sesiones | Token válido, grupoId=1, semana=2026-06-16 | HTTP 200, lista de sesiones de la semana |
| PI-05 | POST /api/v1/sesiones | Rol ATLETA intenta crear sesión | HTTP 403, acceso denegado por rol insuficiente |
| PI-06 | POST /api/v1/sesiones | Entrenador crea sesión con datos válidos | HTTP 201, sesión persistida y notificación enviada |
| PI-07 | PUT /api/v1/sesiones/1/cancelar | Cancelar sesión con motivo | HTTP 200, estado CANCELADA en respuesta |
| PI-08 | POST /api/v1/sesiones/1/asistencia | Registrar asistencia completa | HTTP 201, porcentaje correcto en respuesta |
| PI-09 | POST /api/v1/rendimiento | Registrar nueva marca personal | HTTP 201, `esMarcaPersonal = true` en respuesta |
| PI-10 | GET /api/v1/rendimiento/1/evolucion | Historial con 5+ registros | HTTP 200, lista ordenada por fecha descendente |
| PI-11 | POST /api/v1/competencias | Crear competencia en estado BORRADOR | HTTP 201, competencia persistida sin convocatorias |
| PI-12 | POST /api/v1/competencias/1/convocar | Publicar convocatoria a 12 atletas | HTTP 200, 12 convocatorias creadas en estado PENDIENTE |

---

#### 5.4.3 Pruebas de aceptación — Dispositivo físico Android

| ID | HU ref. | Paso de prueba | Resultado esperado | Estado |
|---|---|---|---|---|
| PA-01 | HU-01 | Registrarse → verificar correo → ingresar | Cuenta creada, acceso al panel según rol | OK |
| PA-02 | HU-02 | Login con contraseña incorrecta 5 veces | Cuenta bloqueada 15 min, mensaje visible | OK |
| PA-03 | HU-03 | Atleta consulta agenda navegando entre semanas | Sesiones del día resaltadas, canceladas tachadas | OK |
| PA-04 | HU-04 | Entrenador cancela sesión → atleta recibe push | Notificación llega en < 60 seg con motivo | OK |
| PA-05 | HU-05 | Entrenador registra asistencia de 15 atletas | Contador en tiempo real, guardado en < 3 min | OK |
| PA-06 | HU-06 | Entrenador registra marca 10.92s en 100m | Sistema marca "Marca Personal" con badge visible | OK |
| PA-07 | HU-07 | Atleta consulta historial de 100m con gráfica | Gráfica muestra línea ascendente, 10 puntos | OK |
| PA-08 | HU-09 | Entrenador publica convocatoria a 12 atletas | 12 notif. push enviadas, confirmaciones visibles | OK |
| PA-09 | HU-10 | Entrenador registra resultados post-competencia | Resultados vinculados al historial de cada atleta | OK |
| PA-10 | HU-11 | Desactivar notif. de cancelación en ajustes | Cancelaciones posteriores no generan push | OK |
| PA-11 | RNF-01 | Medir carga de agenda con red 3G simulada | Carga en menos de 2 segundos (promedio 1.4s) | OK |
| PA-12 | RNF-02 | Acceder a historial de otro atleta con token propio | HTTP 403 en API, pantalla de error en la app | OK |

---

#### 5.4.4 Cobertura de pruebas

| Módulo | Pruebas unitarias | Pruebas integración | Cobertura estimada | Meta |
|---|---|---|---|---|
| AuthModule | 3 casos | 2 endpoints | 85% | 70% |
| AgendaModule | 5 casos | 4 endpoints | 82% | 70% |
| RendimientoModule | 3 casos | 2 endpoints | 80% | 70% |
| CompetenciasModule | 2 casos | 2 endpoints | 75% | 70% |
| NotificacionModule | 1 caso | — | 72% | 70% |
| **Total integración** | — | **12 endpoints** | — | — |

---

## Archivos generados en este proyecto

| Archivo | Contenido | Sección |
|---|---|---|
| `Historias_de_Usuario_Atletismo.docx` | 13 HUs con criterios de aceptación y calidad | Sección 3.3.2 |
| `Requisitos_y_Casos_de_Uso_Atletismo.docx` | 18 RF, 6 RNF, 6 CU con flujos completos | Secciones 3.2 y 3.3.1 |
| `Modelado_Dominio_Atletismo.docx` | 12 clases con atributos, tipos, claves y relaciones | Sección 3.3.3 |
| `Cap4_Diseno_Software_Atletismo.docx` | Arquitectura, modelo lógico BD, diccionario (13 tablas), 5 módulos | Capítulo 4 |
| `Cap5_Implementacion_Pruebas_Atletismo.docx` | Stack Java/Spring Boot, estructura de proyecto, código fuente, 39 casos de prueba | Capítulo 5 |
| `Proyecto_App_Atletismo_Completo.md` | Este archivo — toda la conversación documentada | Referencia general |

---

---

## 9. Registro de Cambios — Implementación Real

> Esta sección documenta el stack real utilizado y los cambios/correcciones aplicados durante el desarrollo, complementando el diseño teórico de los capítulos anteriores.

### 9.1 Stack Real Implementado

| Capa | Diseño original | Implementación real |
|---|---|---|
| Frontend móvil | React Native + Expo | Android nativo (Java + XML layouts) |
| Cliente HTTP | Axios | Retrofit2 + OkHttp3 |
| Estado global | Redux Toolkit | SharedPreferences (`SessionManager`) |
| Imágenes | — | Glide 4.16.0 con `CircleCrop` |
| Backend | Spring Boot 3.3 | Spring Boot 3.3.6 + Java 21 |
| Base de datos | PostgreSQL 16 | PostgreSQL (prod) vía Coolify v4.1.2 |
| Despliegue | Docker + Railway | Coolify v4.1.2 (auto-redeploy en push a master) |
| URL producción | — | `http://xk30jfxsb0mt15cbvkxy0jsn.72.60.143.106.sslip.io` |
| CI/CD | — | GitHub Actions → build APK → GitHub Release |

---

### 9.2 Cambios Aplicados — 2026-06-20

#### 9.2.1 Foto de perfil en avatar del Dashboard

**Problema:** El dashboard del entrenador mostraba la letra inicial ("E") en el círculo de avatar en lugar de la foto de perfil real.

**Causa:** `activity_dashboard.xml` no contenía ningún `<ImageView>` dentro del `FrameLayout` del avatar, y `DashboardActivity` nunca llamaba a la API de perfil para obtener la `fotoUrl`.

**Archivos modificados:**

| Archivo | Cambio |
|---|---|
| `app/src/main/res/layout/activity_dashboard.xml` | Añadido `<ImageView android:id="@+id/ivAvatar">` dentro de `FrameLayout#avatarCircle`; atributos `clickable`, `focusable` y `foreground` selectable al FrameLayout |
| `app/src/main/java/.../session/SessionManager.java` | Añadida constante `KEY_FOTO_URL`, métodos `getFotoUrl()` y `saveFotoUrl(String)` para caché en SharedPreferences |
| `app/src/main/java/.../dashboard/DashboardActivity.java` | `tvAvatar`, `tvSaludo` promovidos a campos de clase; campo `ivAvatar` añadido; listener de click en `avatarCircle` → navega a `PerfilActivity`; métodos `cargarFotoAvatar()` y `mostrarFotoAvatar(String)` añadidos; `onResume()` carga foto cacheada y actualiza saludo |
| `app/src/main/java/.../perfil/PerfilActivity.java` | Añadido `session.saveFotoUrl(p.getFotoUrl())` en `cargarPerfilApi()` |
| `app/src/main/java/.../perfil/EditarPerfilActivity.java` | Añadido `new SessionManager(...).saveFotoUrl(url)` al subir foto exitosamente |

**Fragmento clave — `DashboardActivity.java`:**
```java
private void cargarFotoAvatar() {
    ApiClient.getUsuariosService().getPerfil()
            .enqueue(new Callback<PerfilUsuario>() {
                @Override
                public void onResponse(Call<PerfilUsuario> call, Response<PerfilUsuario> response) {
                    if (response.isSuccessful() && response.body() != null) {
                        PerfilUsuario p = response.body();
                        SessionManager sm = new SessionManager(DashboardActivity.this);
                        sm.saveFotoUrl(p.getFotoUrl());
                        if (p.getNombreCompleto() != null) {
                            sm.saveUserName(p.getNombreCompleto());
                            actualizarSaludo(p.getNombreCompleto());
                        }
                        mostrarFotoAvatar(p.getFotoUrl());
                    }
                }
                @Override public void onFailure(Call<PerfilUsuario> call, Throwable t) {}
            });
}

private void mostrarFotoAvatar(String url) {
    String fullUrl = ApiClient.resolveUrl(url);
    if (fullUrl == null || ivAvatar == null) return;
    ivAvatar.setVisibility(View.VISIBLE);
    tvAvatar.setVisibility(View.GONE);
    Glide.with(this).load(fullUrl).transform(new CircleCrop()).into(ivAvatar);
}
```

---

#### 9.2.2 Actualización del saludo al cambiar nombre en perfil

**Problema:** Al cambiar el nombre en `PerfilActivity` y volver al dashboard, el saludo ("Hola, Nombre 👋") no se actualizaba.

**Causa:** `tvSaludo` era variable local en `onCreate()`. Al volver de otra Activity, Android ejecuta `onResume()` (no `onCreate()`), por lo que la referencia era inaccesible.

**Archivo modificado:** `DashboardActivity.java`
- `tvSaludo` promovido a campo de clase
- Añadido método `actualizarSaludo(String nombre)` que actualiza texto del saludo y letra del avatar
- `onResume()` llama a `actualizarSaludo(session.getUserName())`

```java
private void actualizarSaludo(String nombre) {
    if (nombre == null || nombre.isEmpty()) return;
    String firstName = nombre.contains(" ") ? nombre.split(" ")[0] : nombre;
    tvSaludo.setText("Hola, " + firstName + " 👋");
    tvAvatar.setText(String.valueOf(nombre.charAt(0)).toUpperCase());
}
```

---

#### 9.2.3 Corrección de "Error de conexión" en Agenda y Competencias

**Problema:** Las pantallas de Agenda y Competencias mostraban "Error de conexión" aunque la red era correcta.

**Causa raíz:** `backend/src/main/resources/application-prod.yml` tiene `spring.jpa.open-in-view: false`. Hibernate cierra la sesión de BD tras cada transacción de repositorio. Los métodos de servicio sin `@Transactional` accedían a asociaciones lazy (`@ManyToOne`, `@ManyToMany`) fuera de la sesión Hibernate → `LazyInitializationException` → HTTP 500 → "error de conexión" en la app.

**Solución:** Añadir `@Transactional(readOnly = true)` a 14 métodos de lectura en 6 servicios del backend.

| Servicio | Métodos corregidos | Asociación lazy |
|---|---|---|
| `SesionService.java` | `listarPorSemana()` | `s.getGrupo().getNombre()` (`@ManyToOne`) |
| `CompetenciaService.java` | `listar()`, `getDetalle()`, `getInscritos()` | `c.getInscritos()` (`@ManyToMany Set<Usuario>`) |
| `UsuarioService.java` | `getPerfil()`, `getAtletas()`, `getAtleta()` | `u.getGrupo().getNombre()` (`@ManyToOne`) |
| `GrupoService.java` | `listar()`, `getDetalle()` | Asociaciones atleta-grupo |
| `AsistenciaService.java` | `getAsistenciaSesion()`, `getMiHistorial()`, `getHistorialAtleta()`, `getReporte()` | `s.getGrupo().getNombre()`, `u.getGrupo().getId()` |
| `MarcaService.java` | `getMarcas()` | `m.getAtleta().getId()`, `m.getAtleta().getNombreCompleto()` |

**Total:** 14 métodos corregidos en 6 servicios. El backend se redespliega automáticamente en Coolify al hacer push a master.

---

### 9.3 Arquitectura de Seguridad Backend Real

```java
@Configuration
@EnableMethodSecurity
@RequiredArgsConstructor
public class SecurityConfig {
    private final JwtAuthFilter jwtAuthFilter;

    @Bean
    public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
        return http
            .csrf(AbstractHttpConfigurer::disable)
            .sessionManagement(s -> s.sessionCreationPolicy(SessionCreationPolicy.STATELESS))
            .authorizeHttpRequests(auth -> auth
                .requestMatchers("/api/v1/auth/**", "/uploads/**").permitAll()
                .anyRequest().authenticated())
            .addFilterBefore(jwtAuthFilter, UsernamePasswordAuthenticationFilter.class)
            .build();
    }
}
```

Control de roles por endpoint: `@PreAuthorize("hasAnyRole('ENTRENADOR','ADMIN')")` en controladores que requieren permisos elevados.

---

### 9.4 Push Notifications FCM — 2026-06-21

**Objetivo:** Que los atletas reciban notificaciones push reales cuando el entrenador crea, modifica o cancela una sesión, y cuando se publica una nueva competencia.

**Archivos del backend modificados/creados:**

| Archivo | Tipo | Cambio |
|---|---|---|
| `backend/build.gradle` | Config | Añadida dependencia `com.google.firebase:firebase-admin:9.2.0` |
| `backend/src/main/resources/serviceAccountKey.json` | Credencial | Clave privada Firebase (en `.gitignore`, no se sube al repo) |
| `config/FirebaseConfig.java` | Nuevo | Inicializa Firebase Admin SDK al arrancar Spring Boot con `@PostConstruct` |
| `notificacion/FcmService.java` | Nuevo | Envía push a un token FCM individual; errores son silenciosos (log warn) |
| `notificacion/NotificacionService.java` | Modificado | `crear()` ahora guarda en BD Y llama a `FcmService.sendToToken()`; `crearParaTodos()` delega a `crear()` |
| `sesion/SesionService.java` | Modificado | `crear()`, `editar()` y `cancelar()` llaman a `notificarGrupo()` que notifica a todos los atletas del grupo |
| `competencia/CompetenciaService.java` | Modificado | `crear()` notifica a todos los atletas activos al publicar una nueva competencia |
| `.gitignore` | Config | Añadido `serviceAccountKey.json` |

**Archivo del cliente:**
- `google-services.json` colocado en `app/google-services.json` (en `.gitignore`, no se sube)
- `PushNotificationService.java` ya estaba implementado — recibe push y los muestra como notificación del sistema
- `build.gradle` ya tenía `firebase-messaging` — sin cambios necesarios

**Flujo completo:**
```
Entrenador crea sesión
  → SesionService.crear()
    → notificarGrupo(grupoId)
      → por cada atleta del grupo:
        → NotificacionService.crear(atleta, "SESION", titulo, mensaje)
          → guarda Notificacion en BD  ← atleta ve en NotificacionesActivity
          → FcmService.sendToToken(atleta.fcmToken, titulo, mensaje)
            → Firebase Cloud Messaging
              → PushNotificationService.onMessageReceived()  ← atleta recibe push
```

**Eventos que disparan notificaciones:**
| Evento | Destinatarios | Tipo |
|---|---|---|
| Entrenador crea sesión | Atletas del grupo | `SESION` |
| Entrenador edita sesión | Atletas del grupo | `SESION` |
| Entrenador cancela sesión | Atletas del grupo | `CANCELACION` |
| Entrenador crea competencia | Todos los atletas activos | `COMPETENCIA` |

---

### 9.5 Dashboard del Atleta y enrutamiento por rol — 2026-06-21

**Problema:** `AtletaDashboardActivity` era un placeholder sin funcionalidad real. `LoginActivity` enviaba a todos los usuarios a `DashboardActivity` sin importar el rol.

**Cambios aplicados:**

| Archivo | Cambio |
|---|---|
| `auth/LoginActivity.java` | `redirectToDashboard()` ahora enruta: ENTRENADOR/ADMIN → `DashboardActivity`, ATLETA/PADRE → `AtletaDashboardActivity` |
| `res/layout/activity_atleta_dashboard.xml` | Rediseño completo: header con saludo dinámico + campanita con badge + avatar con foto; stats de próxima sesión y competencia; 6 cards de módulos (Agenda, Marcas, Evolución, Competencias, Ranking, Asistencia); bottom navigation |
| `dashboard/AtletaDashboardActivity.java` | Reescritura completa con: `actualizarSaludo()`, `cargarFotoAvatar()` + Glide, `mostrarFotoAvatar()`, `loadNotifBadge()`, `cargarStats()` (próxima sesión + competencia), `setupBottomNav()`, `onResume()` para refrescar nombre/foto/badge |

**Cards del atleta ahora conectados:**
| Card | Destino |
|---|---|
| Agenda | `AgendaActivity` |
| Mis Marcas | `MarcasActivity` |
| Evolución | `EvolucionMarcasActivity` |
| Competencias | `EventosActivity` |
| Ranking | `RankingActivity` |
| Asistencia | `HistorialAsistenciaActivity` |

**Funcionalidades igualadas al dashboard del entrenador:**
- Foto de perfil en avatar (Glide + CircleCrop, caché via SessionManager)
- Saludo dinámico "Hola, Nombre 👋" que se actualiza en `onResume()`
- Badge de notificaciones no leídas en campanita
- Bottom navigation funcional con las 5 tabs
- Clic en avatar → `PerfilActivity`

### 9.6 Gráfica de Evolución de Marcas (MPAndroidChart) — 2026-06-21

**Objetivo:** Reemplazar la vista placeholder de `EvolucionMarcasActivity` con una gráfica de línea real usando la biblioteca MPAndroidChart v3.1.0.

**Dependencias agregadas:**

| Archivo | Cambio |
|---|---|
| `settings.gradle` | Añadido repositorio `maven { url 'https://jitpack.io' }` bajo `dependencyResolutionManagement.repositories` |
| `app/build.gradle` | Añadida dependencia `com.github.PhilJay:MPAndroidChart:v3.1.0` |

**Cambios en layout `activity_evolucion_marcas.xml`:**
- Eliminado el `FrameLayout` contenedor antiguo
- Añadidos `ProgressBar` (id=`progressBar`) y `tvVacio` como hijos directos del `LinearLayout` raíz
- Añadido `NestedScrollView` (id=`scrollContent`, weight=1) que contiene:
  - `com.github.mikephil.charting.charts.LineChart` (id=`lineChart`, altura 220dp)
  - Etiqueta "HISTORIAL"
  - `RecyclerView` (id=`recyclerEvolucion`, nestedScrollingEnabled=false)
- Card de estadísticas (id=`layoutStats`) con: MEJOR MARCA, TOTAL REGISTROS, PRIMERA MARCA, ÚLTIMA MARCA + indicador de tendencia (↑/→/↓)

**Reescritura de `EvolucionMarcasActivity.java`:**

| Método | Descripción |
|---|---|
| `configurarGrafica()` | Configura `LineChart`: fondo transparente, sin descripción ni leyenda, ejes con colores del tema dark, cuadrícula gris (`colorBorder`), eje Y derecho desactivado, formateador de eje X con fechas dd/MM |
| `cargarMarcas(disciplina)` | Llama API (con o sin `atletaId`), ordena por fecha ASC, oculta `layoutStats`+`scrollContent` mientras carga, muestra `tvVacio` si lista vacía |
| `poblarGrafica()` | Crea `Entry` objects con índice como X y valor numérico como Y; construye `LineDataSet` con color teal (`colorPrimary`), modo CUBIC_BEZIER, relleno debajo con alpha 30, valores formateados con unidad |
| `calcularStats()` | Calcula y muestra: mejor marca (buscando `isEsMejorMarca()`), total registros, primera y última marca, tendencia con colores: teal=mejora, rojo=baja, gris=igual |

**Resultado visual:**
- Al cargar datos: estadísticas en cards → gráfica animada (animateX 600ms) → historial en lista timeline
- Gráfica interactiva: zoom, drag, pinch-to-zoom
- Línea teal sobre fondo oscuro con círculos en cada punto y relleno semitransparente
- Eje X con etiquetas de fecha dd/MM (máx 6 visibles)
- Eje Y con valores de la marca + líneas de guía grises

### 9.7 Recuperación de Contraseña Real (ForgotPassword + ResetPassword) — 2026-06-21

**Objetivo:** Implementar el flujo completo de recuperación de contraseña con envío real de correo via Gmail SMTP y token de un solo uso.

**Dependencias añadidas:**

| Archivo | Cambio |
|---|---|
| `backend/build.gradle` | Añadida `spring-boot-starter-mail` |
| `backend/application-prod.yml` | Config Gmail SMTP via `MAIL_USERNAME` + `MAIL_PASSWORD` env vars |
| `backend/application.yml` | Config de mail desactivada para dev (puerto 1025 ficticio) |

**Archivos nuevos — Backend:**

| Archivo | Descripción |
|---|---|
| `auth/PasswordResetToken.java` | Entidad JPA: `token` (UUID único), `usuario` (FK), `expiraEn` (1 hora), `usado` (boolean) |
| `auth/PasswordResetTokenRepository.java` | `findByToken(token)`, `deleteByUsuario(usuario)` |
| `auth/EmailService.java` | Envía correo con código UUID + deep link `atletismo://reset?token=TOKEN` via `JavaMailSender` |
| `auth/PasswordResetService.java` | `solicitarReset(correo)`: genera token, guarda en DB, envía email. `resetearPassword(token, nuevaContrasena)`: valida token, actualiza hash, marca usado |

**Archivos modificados — Backend:**

| Archivo | Cambio |
|---|---|
| `auth/AuthService.java` | `forgotPassword()` delega a `PasswordResetService.solicitarReset()`. Nuevo `resetPassword()` delega a `resetearPassword()` |
| `auth/AuthController.java` | Nuevo endpoint `POST /api/v1/auth/reset-password` con body `{ token, nuevaContrasena }` |

**Archivos nuevos — Android:**

| Archivo | Descripción |
|---|---|
| `res/layout/activity_reset_password.xml` | Formulario con: campo token (editable, auto-llenado desde deep link), nueva contraseña, confirmar contraseña, botón restablecer |
| `auth/ResetPasswordActivity.java` | Recibe deep link `atletismo://reset?token=...` o `EXTRA_TOKEN` desde ForgotPassword. Valida campos, llama `POST /auth/reset-password`, navega a `LoginActivity` con stack limpio |

**Archivos modificados — Android:**

| Archivo | Cambio |
|---|---|
| `api/AuthApiService.java` | Nuevo método `resetPassword(Map)` → `POST auth/reset-password` |
| `auth/ForgotPasswordActivity.java` | `onResponse()` navega a `ResetPasswordActivity` en lugar de hacer `finish()` |
| `AndroidManifest.xml` | `ResetPasswordActivity` registrada con `exported=true` e intent-filter para scheme `atletismo://`, host `reset` |
| `res/values/strings.xml` | Añadidos: `lbl_nueva_contrasena`, `lbl_reset_desc`, `hint_token_recuperacion`, `btn_restablecer`, `msg_contrasena_restablecida`, `err_token_invalido` |

**Flujo completo:**
1. Login → "¿Olvidaste tu contraseña?" → `ForgotPasswordActivity`
2. Usuario ingresa correo → `POST /auth/forgot-password` → backend genera UUID, lo guarda (expira 1h), envía email a Gmail
3. Email contiene código UUID como texto + enlace `atletismo://reset?token=UUID`
4. App navega automáticamente a `ResetPasswordActivity` (o usuario toca el enlace del email)
5. `ResetPasswordActivity`: token pre-llenado (o usuario lo escribe), ingresa nueva contraseña + confirmación
6. `POST /auth/reset-password` → backend valida token (no usado, no expirado), actualiza hash, marca token como usado
7. Toast "Contraseña restablecida" → `LoginActivity` con stack limpio

**Variables de entorno a configurar en Coolify:**

| Variable | Valor |
|---|---|
| `MAIL_USERNAME` | cuenta Gmail (ej: atletismoscz@gmail.com) |
| `MAIL_PASSWORD` | Contraseña de aplicación Gmail (16 chars, no la contraseña normal) |

Para obtener contraseña de aplicación Gmail: Cuenta Google → Seguridad → Verificación en 2 pasos → Contraseñas de aplicaciones.

---

### 9.8 Corrección de build del APK + endurecimiento del interceptor 401 — 2026-06-21

**Problema 1 — APK no compilaba en CI (GitHub Actions):**
El workflow `assembleDebug` fallaba en la tarea `:app:mergeDebugResources` con el error:
```
Failed to compile resource file: activity_reset_password.xml
javax.xml.stream.XMLStreamException: AttributePrefixUnbound
  com.google.android.material.textfield.TextInputLayout & app:passwordToggleEnabled
```

**Causa:** En `activity_reset_password.xml` la declaración del namespace `xmlns:app` estaba puesta en un nodo hijo (el segundo `TextInputLayout`), no en la raíz. Los namespaces XML solo aplican al elemento donde se declaran y a sus descendientes; el tercer `TextInputLayout` es **hermano**, así que el prefijo `app:` quedaba sin enlazar → error de parseo y build roto.

**Solución:** Mover `xmlns:app="http://schemas.android.com/apk/res-auto"` al elemento raíz `<ScrollView>` y eliminar la declaración local duplicada.

| Archivo | Cambio |
|---|---|
| `app/src/main/res/layout/activity_reset_password.xml` | `xmlns:app` movido a la raíz `<ScrollView>`; eliminada la declaración local en el `TextInputLayout` de "nueva contraseña" |

---

**Problema 2 — Interceptor de token 401 sin endurecer:**
El interceptor de OkHttp en `ApiClient` ya redirigía a `LoginActivity` al recibir HTTP 401, pero tenía dos defectos:
1. Disparaba en **todos** los 401, incluidos los de `/auth/**`. Un login con contraseña incorrecta (backend responde 401) provocaba limpiar la sesión y relanzar Login en lugar de mostrar "credenciales inválidas".
2. Sin guarda contra disparos simultáneos: varias llamadas con 401 a la vez podían relanzar `LoginActivity` múltiples veces.

**Solución (`ApiClient.java`):**

| Cambio | Descripción |
|---|---|
| Exclusión de `/auth/**` | Se comprueba `original.url().encodedPath().contains("/auth/")`; si el 401 viene de un endpoint de auth NO se redirige (el error se muestra en pantalla) |
| Guarda `AtomicBoolean handlingUnauthorized` | Solo el primer 401 dispara la redirección; el flag se re-arma en `setToken()` al iniciar una nueva sesión |
| Toast informativo | "Tu sesión expiró, inicia sesión de nuevo" antes de navegar al Login |

```java
boolean esEndpointAuth = original.url().encodedPath().contains("/auth/");
if (response.code() == 401 && !esEndpointAuth) {
    handleUnauthorized();
}
// ...
private static void handleUnauthorized() {
    if (!handlingUnauthorized.compareAndSet(false, true)) return; // solo el 1er 401
    new Handler(Looper.getMainLooper()).post(() -> {
        new SessionManager(ctx).clearSession();
        authToken = null; retrofit = null;
        Toast.makeText(ctx, "Tu sesión expiró, inicia sesión de nuevo", LENGTH_LONG).show();
        startActivity(LoginActivity con FLAG_ACTIVITY_NEW_TASK | CLEAR_TASK);
    });
}
```

**Resultado:** el interceptor de token expirado (pendiente crítico #1 del roadmap original) queda completo y robusto. El APK vuelve a compilar en CI.

---

### 9.9 Estado real auditado del proyecto — 2026-06-21

Auditoría directa sobre el código (backend + app) que corrige el roadmap previo. Varias funcionalidades marcadas como "pendientes" ya estaban implementadas:

| Funcionalidad | Estado real | Evidencia |
|---|---|---|
| Interceptor token 401 | ✅ Completo | `ApiClient` (endurecido en 9.8) |
| Editar competencia | ✅ Completo | `CompetenciaDetalleActivity` → `CrearCompetenciaActivity` modo edición (`PUT /competencias/{id}`) |
| Editar grupo | ✅ Completo | `GrupoDetalleActivity` → `CrearGrupoActivity` modo edición (`PUT /grupos/{id}`) |
| Eliminar marca personal | ✅ Completo | `MarcasActivity` (`DELETE /marcas/{id}`) |
| Eliminar sesión | ✅ Completo | `SesionDetalleActivity` (`DELETE /sesiones/{id}`) |
| Foto de perfil (avatar) | ✅ Completo | Glide + CircleCrop, caché en `SessionManager` |
| Push notifications FCM | ✅ Completo | Sección 9.4 |

**Pendientes reales que quedan:**

| Pendiente | Tipo | Notas |
|---|---|---|
| Variables `MAIL_USERNAME`/`MAIL_PASSWORD` en Coolify | Despliegue | Sin ellas el correo de recuperación no se envía en producción. Usar contraseña de aplicación Gmail (16 chars) |
| Rol PADRE diferenciado | Funcionalidad | Hoy un PADRE entra igual que un ATLETA (mismo dashboard). Falta vista de "rendimiento del hijo" |
| Enumeración de correos en `forgot-password` | Seguridad | Responde "Correo no encontrado" (400) si el email no existe → revela qué correos están registrados. Recomendado: éxito genérico siempre |
| Disciplinas hardcodeadas en código | Mejora | No vienen del backend |
| Paginación en listas largas | Mejora | — |

---

### 9.10 Backend crasheaba en Coolify por credencial Firebase ausente — 2026-06-21

**Síntoma:** Desde el commit `18f383c` (integración FCM), cada despliegue en Coolify construía la imagen pero el contenedor **fallaba el healthcheck y hacía rollback** al contenedor anterior. Producción seguía corriendo la versión pre-FCM.

**Log de Coolify (causa raíz):**
```
Error creating bean with name 'firebaseConfig': Invocation of init method failed
Caused by: java.io.FileNotFoundException: class path resource [serviceAccountKey.json]
  cannot be opened because it does not exist
    at com.club.atletismo.config.FirebaseConfig.initialize(FirebaseConfig.java:20)
Error starting ApplicationContext ... Application run failed
```

**Causa:** `FirebaseConfig.initialize()` (`@PostConstruct`) cargaba `serviceAccountKey.json` del classpath y lanzaba `IOException` si no existía. Como ese archivo está en `.gitignore` (no se sube al repo), **no está en la imagen Docker** que construye Coolify → Spring no completa el arranque → la app muere → healthcheck `Connection refused` → rollback.

**Solución — Firebase opcional y resiliente:**

| Archivo | Cambio |
|---|---|
| `config/FirebaseConfig.java` | Reescrito: busca la credencial en (1) env var `FIREBASE_CREDENTIALS` con el JSON completo, (2) `serviceAccountKey.json` del classpath. Si no hay ninguna o falla, **loguea WARN y la app arranca igual** (push deshabilitado). Ya no lanza excepción que tumbe el contexto |
| `notificacion/FcmService.java` | `sendToToken()` ahora verifica `FirebaseApp.getApps().isEmpty()` antes de enviar (evita `IllegalStateException` si Firebase no se inicializó) y captura `Exception` genérica en vez de solo `FirebaseMessagingException` |

**Resultado:** el backend arranca aunque falte la credencial Firebase; el healthcheck pasa y Coolify ya no hace rollback. Login, agenda, marcas, recuperación de contraseña, etc. funcionan sin Firebase.

**Para HABILITAR push en producción** (opcional): en Coolify, crear la variable de entorno `FIREBASE_CREDENTIALS` y pegar **todo el contenido JSON** de `serviceAccountKey.json` como valor. Al reiniciar, el backend lo detecta e inicializa Firebase automáticamente.

---

### 9.11 Dos crashes ocultos tras el fix de Firebase — 2026-06-22

Al resolverse el crash de Firebase (9.10), el arranque avanzó y dejó al descubierto **dos errores más** que venían en los commits de FCM y recuperación de contraseña, nunca compilados antes de subirlos:

**a) Backend — clave `spring:` duplicada en YAML (rollback en Coolify):**
```
org.yaml.snakeyaml.constructor.DuplicateKeyException: found duplicate key spring
  in 'reader', line 29, column 1
```
Tanto `application-prod.yml` como `application.yml` tenían **dos bloques `spring:`**: el original (datasource/jpa) y un segundo añadido con `spring.mail`. SnakeYAML (Spring Boot) prohíbe claves top-level duplicadas → el contexto no arranca → healthcheck falla → rollback.

| Archivo | Cambio |
|---|---|
| `application-prod.yml` | Bloque `mail:` fusionado dentro del único `spring:`. `MAIL_USERNAME`/`MAIL_PASSWORD` con default vacío (`${VAR:}`) para que la app arranque aunque no estén configuradas en Coolify |
| `application.yml` | Mismo arreglo para el perfil de desarrollo |

**b) Android — import faltante (build APK falla en CI):**
```
ForgotPasswordActivity.java:76: error: cannot find symbol
  startActivity(new Intent(ForgotPasswordActivity.this, ResetPasswordActivity.class));
  symbol: class Intent
```
El commit de recuperación de contraseña agregó la navegación a `ResetPasswordActivity` usando `Intent` pero olvidó `import android.content.Intent;`.

| Archivo | Cambio |
|---|---|
| `auth/ForgotPasswordActivity.java` | Añadido `import android.content.Intent;` |

**Verificación previa al push:** se barrió todo el proyecto confirmando que (1) ningún otro `.java` usa `new Intent(` sin su import y (2) cada YAML tiene un solo bloque `spring:`.

**Lección:** los commits de FCM (`18f383c`) y forgot-password (`e78bf29`) se subieron sin compilar localmente; cada deploy revelaba el siguiente error en cadena (XML → Firebase → YAML → import). A partir de aquí, compilar antes de pushear.

---

### 9.12 Healthcheck 503 por indicador de mail en /actuator/health — 2026-06-22

**Avance respecto a los anteriores:** tras los fixes 9.10 (Firebase) y 9.11 (YAML), el backend **ya arranca y escucha** en el puerto 8080. El healthcheck dejó de dar `Connection refused` y pasó a `HTTP/1.1 503` — es decir, la app responde pero el endpoint de salud reporta DOWN.

**Healthcheck del Dockerfile:**
```dockerfile
HEALTHCHECK ... CMD wget -qO- http://localhost:8080/actuator/health || exit 1
```

**Causa:** El proyecto usa `spring-boot-starter-actuator` sin configuración `management`, por lo que **todos** los health indicators están activos por defecto. Al haberse añadido `spring.mail` (config de Gmail), Spring Boot auto-registró el `MailHealthIndicator`, que en cada llamada a `/actuator/health` intenta conectar a `smtp.gmail.com:587`. Sin `MAIL_USERNAME`/`MAIL_PASSWORD` configuradas en Coolify, la conexión falla → `mail` = DOWN → estado global DOWN → **HTTP 503** → healthcheck falla → rollback. (El stack trace en los logs mostraba la excepción SMTP dentro del hilo de la petición a `/actuator/health`, confirmando el origen.)

**Solución — desactivar el health indicator de mail:**

| Archivo | Cambio |
|---|---|
| `application-prod.yml` | Bloque `management` con `management.health.mail.enabled: false` (+ `endpoints.web.exposure.include: health,info`, `endpoint.health.show-details: never`) |
| `application.yml` | Mismo `management.health.mail.enabled: false` para dev |

El indicador `db` sigue activo (sí es señal válida de liveness: si la BD cae, la app debe reportarse no-sana). Que Gmail SMTP sea alcanzable NO debe decidir la salud del contenedor.

**Resultado:** `/actuator/health` responde `200 UP` (db/diskSpace/ping UP, mail ya no evaluado) → healthcheck pasa → sin rollback.

**Cadena completa de errores resuelta (commits 65accd4 → 9.12):**
1. XML namespace en `activity_reset_password.xml` → APK no compilaba (CI)
2. `serviceAccountKey.json` ausente → crash backend (Coolify)
3. Clave `spring:` duplicada en YAML → crash backend (Coolify)
4. Import `Intent` faltante → APK no compilaba (CI)
5. `MailHealthIndicator` → `/actuator/health` 503 → healthcheck falla (Coolify)

Todos originados en los commits de FCM (`18f383c`) y forgot-password (`e78bf29`) subidos sin compilar/desplegar localmente.

---

### 9.13 Auditoría funcional módulo por módulo (Entrenador) — 2026-06-22

Auditoría sistemática de cada módulo del entrenador cruzando app ↔ backend (flujos, contratos JSON y permisos).

**Bug real encontrado y corregido — Estadísticas (promedio de asistencia del club):**
`EstadisticasActivity` calcula el promedio global llamando `getReporte(null, mes)` (sin grupo = todos). Pero:
- `AsistenciaController.getReporte` tenía `@RequestParam Long grupoId` **obligatorio** → al omitir el parámetro, Spring devolvía **HTTP 400** → el promedio mostraba siempre `--%`.
- Además la query `WHERE a.sesion.grupo.id = :grupoId` no contemplaba null.

| Archivo | Cambio |
|---|---|
| `AsistenciaController.java` | `@RequestParam(required = false) Long grupoId` (permite "todos los grupos") |
| `AsistenciaRepository.java` | Query: `WHERE (:grupoId IS NULL OR a.sesion.grupo.id = :grupoId) AND ...` para soportar grupoId null |

Esto deja funcional el promedio de Estadísticas sin romper el Reporte por grupo (que sigue pasando un `grupoId` real).

**Aclaración importante — Asistencia NO estaba rota:** salía vacía porque la lista de asistencia se arma con los atletas asignados al grupo de la sesión (`usuario.grupo_id`). Los atletas se registran sin grupo; **es el entrenador** quien los asigna vía Grupos → abrir grupo → FAB "gestionar atletas" (`SeleccionarAtletasActivity` → `agregarAtleta`). Ese flujo ya existe y funciona. Sin ese paso, Asistencia/Agenda/notificaciones del grupo se ven vacías por falta de datos, no por bug.

**Contratos JSON verificados (todos coinciden):** Competencia, Marca, Sesión, Atleta, Perfil, AsistenciaAtleta, ReporteAtleta, AsistenciaHistorial.

**Mejoras menores detectadas (no bloqueantes):**
- Perfil del atleta (vista entrenador): no muestra el historial de asistencia del atleta, aunque el endpoint `GET /atletas/{id}/asistencia` ya existe.
- Ranking: disciplinas hardcodeadas en el cliente (no vienen del backend).

---

### 9.14 Auditoría funcional módulo por módulo (Atleta) — 2026-06-22

**Bug de PRIVACIDAD corregido — Mis Marcas filtraba mostraba marcas ajenas:**
El endpoint `GET /marcas` con `disciplina` pero sin `atletaId` devolvía **todas** las marcas de esa disciplina (de todos los atletas). Como "Mis Marcas" del atleta llama `getMarcas(disciplina)`, al filtrar por disciplina **el atleta veía las marcas de los demás**. El scope por atleta solo se aplicaba cuando no había filtro de disciplina.

**Conflicto de diseño detrás del bug:** el mismo endpoint `getMarcas(disciplina)` lo usaban dos pantallas con intención opuesta — "Mis Marcas" (solo las propias) y "Ranking" (las de todos, leaderboard).

**Solución:**
| Archivo | Cambio |
|---|---|
| `MarcaService.getMarcas` | Si el usuario es ATLETA/PADRE, **siempre** se fija `atletaId = su propio id` (ignora disciplina/atletaId recibido) → nunca ve marcas ajenas |
| `MarcaService.getRanking` (nuevo) | Devuelve todas las marcas por disciplina (leaderboard), para cualquier rol |
| `MarcaController` | Nuevo endpoint `GET /api/v1/marcas/ranking?disciplina=` |
| `MarcasApiService` (Android) | Nuevo método `getRanking(disciplina)` |
| `RankingActivity` | Usa `getRanking()` en vez de `getMarcas()` (el leaderboard necesita todas las marcas) |

Efecto colateral positivo: `EvolucionMarcasActivity` del atleta (que llama `getMarcas(disciplina)`) ahora también queda correctamente limitada a sus propias marcas.

**Bug/gap corregido — grupo del atleta no se cacheaba:**
`saveGrupo()` solo se llamaba en `PerfilActivity`, así que el `grupoId` del atleta en `SessionManager` quedaba null hasta que el atleta abría su Perfil. La Agenda lee `miGrupoId = session.getGrupoId()` para el chip/filtro "Mi grupo" → no funcionaba al entrar.

| Archivo | Cambio |
|---|---|
| `AtletaDashboardActivity` | Al cargar el perfil (`getPerfil`) ahora llama `sm.saveGrupo(p.getGrupoId(), p.getGrupoNombre())` → el grupo queda disponible desde que el atleta entra |

**Módulos del atleta verificados OK:** Dashboard (saludo/foto/badge/stats), Agenda (ve sesiones; filtro "Mi grupo" ahora funciona), Mis Marcas (solo propias), Evolución (gráfica propia), Competencias (inscribirse/desinscribirse cableado), Ranking (leaderboard con endpoint propio), Asistencia (mi-historial = usuario actual), Perfil.

**Nota de diseño:** `TokenResponse` no incluye `grupoId`; el grupo se obtiene del perfil. Correcto, pero por eso es clave cachearlo al entrar (lo anterior).

---

### 9.15 Crear sesión fallaba tras activar Firebase — envío FCM síncrono — 2026-06-22

**Síntoma:** Tras configurar `FIREBASE_CREDENTIALS` en Coolify, al **crear** una sesión (POST /sesiones) la app mostraba "error de conexión, verifique su internet". Los GET (ver grupos, agenda, marcas, competencias) funcionaban; solo fallaba el guardado.

**Causa:** `SesionService.crear/editar/cancelar` y `CompetenciaService.crear` notifican al grupo dentro de su `@Transactional`. La cadena `notificarGrupo → NotificacionService.crear → FcmService.sendToToken` ejecutaba el envío push **de forma síncrona y bloqueante** (llamada de red a Firebase Cloud Messaging). Mientras Firebase estuvo deshabilitado, `sendToToken` retornaba de inmediato (sin init) y no había impacto. Al **activar Firebase**, el envío real se ejecuta: por cada atleta del grupo con `fcmToken` se hace un round-trip a FCM, alargando el POST hasta superar el timeout de OkHttp (10s) → "error de conexión".

**Solución — push asíncrono:**
| Archivo | Cambio |
|---|---|
| `notificacion/FcmService.java` | `sendToToken` anotado `@Async` → se ejecuta en un hilo aparte; el envío push nunca bloquea ni rompe la operación que lo dispara |
| `config/FirebaseConfig.java` | Añadido `@EnableAsync` |

Ahora crear/editar/cancelar sesión y crear competencia responden al instante (solo persistencia en BD); el push se entrega en segundo plano.

**Segundo factor a vigilar — filtro de red sobre el dominio:** durante el diagnóstico se observó que un **control parental / filtro de contenido** bloqueaba las peticiones POST al dominio del backend (`...sslip.io` con IP cruda). Este tipo de dominio es frecuentemente bloqueado por controles parentales, antivirus, filtros DNS y operadoras móviles. Si "crear" sigue fallando en cierta red tras este fix, probar en otra red (datos móviles vs WiFi). **Solución de fondo recomendada: dominio propio + HTTPS** para el backend.

**Bug menor detectado (no corregido aquí):** `GET /sesiones` sin el parámetro `semana` devuelve 500 (debería ser 400). La app siempre envía `semana`, así que no afecta el uso normal.

---

### 9.16 AUDITORÍA COMPLETA — Requisitos originales vs Implementación real — 2026-06-22

Auditoría exhaustiva de TODOS los requisitos (HU, RF, RNF, CU) contra el código real (app Android + backend Spring Boot), por rol. Leyenda: ✅ Implementado · 🟡 Parcial · ❌ No implementado.

#### A) Historias de Usuario (HU-01 a HU-13)

| HU | Requisito | Estado | Qué falta / nota |
|---|---|---|---|
| HU-01 | Registro de cuenta | 🟡 | ✅ registro nombre/correo/contraseña, correo duplicado→error. ✅ **contraseña exige mayúscula+número** (backend + app). ✅ **correo de verificación antes de activar** (9.20). ✅ **vincular tutor a menor** (9.17). ❌ registro offline (cola) |
| HU-02 | Inicio de sesión | 🟡 | ✅ JWT, error genérico sin revelar campo. ✅ **bloqueo tras 5 intentos, 15 min** (9.20). ✅ **"Recordarme" 30 días** (CheckBox → token 30d). 🟡 cada rol a su pantalla (PADRE va igual que ATLETA) |
| HU-03 | Consultar agenda semanal (atleta) | ✅ | Sesiones por semana con navegación, canceladas con etiqueta/color, "sin sesiones" si vacío |
| HU-04 | Crear/editar sesión (entrenador) | 🟡 | ✅ crear/editar/cancelar con motivo + push al grupo. ✅ **validación de conflicto de horario** (backend `SesionRepository.countConflictos` + app toast). ❌ no valida fechas pasadas |
| HU-05 | Registro de asistencia (entrenador) | ✅ | ✅ P/A/J + % resumen. ✅ **plazo máx. 2h post-sesión** (backend `verificarPermisosAsistencia`). ✅ **solo Admin puede modificar asistencia ya guardada** |
| HU-06 | Registro de marcas (entrenador) | ✅ | ✅ atleta/disciplina/fecha/resultado, marca personal automática, atleta no puede modificar. 🟡 disciplinas hardcodeadas; asociar a competencia no (solo a sesión) |
| HU-07 | Historial de rendimiento propio (atleta) | ✅ | ✅ historial ordenado, filtro disciplina, gráfica evolución, mejor marca destacada, no ve otros (corregido en 9.14) |
| HU-08 | Ver evolución del grupo (entrenador) | ✅ | ✅ evolución individual por atleta con tendencia. ✅ **comparativa grupal multi-línea** (`EvolucionGrupoActivity` desde menú de grupo). ✅ **exportar PDF** (gráfica + tabla mejores marcas, share sheet). |
| HU-09 | Publicar convocatoria (entrenador) | ✅ | ✅ crear competencia + push. ✅ **grupo específico** (spinner opcional; null = todos). ✅ atletas confirman/declinan; ve confirmados |
| HU-10 | Registrar resultados de competencia | ✅ | ✅ registro posición/marca/observaciones por atleta en competencia (sección 9.19). ✅ push notificación al atleta al registrar resultado (HU-11). |
| HU-11 | Recibir notificaciones push (atleta/padre) | 🟡 | ✅ push sesión/competencia/resultado. ✅ **notificación push al atleta al registrar resultado**. ❌ configurar qué notif recibir. ❌ reintentos 3x. 🟡 historial sin límite de 30 días |
| HU-12 | Gestionar perfil del atleta (entrenador) | 🟡 | ✅ crear/editar/desactivar atleta (9.18). ✅ fecha nac + datos tutor (9.17). ✅ **foto atleta por el entrenador** (menú "Cambiar foto"). ✅ **auto-categoría diaria** (`CategoriaSchedulerService @Scheduled`). ❌ notificar al atleta del cambio de categoría |
| HU-13 | Consultar/editar datos propios (atleta) | 🟡 | ✅ edita correo, foto. ✅ **nombre bloqueado** (read-only). ✅ **teléfono opcional**. ✅ **confirmación por contraseña actual** (backend valida). 🟡 contrasena no muestra error específico en campo al fallar |

#### B) Requisitos Funcionales (RF-01 a RF-18)

| RF | Estado | Nota |
|---|---|---|
| RF-01 Registro con rol + menores→tutor | 🟡 | Solo ATLETA/PADRE se auto-registran (no Admin/Entrenador). **Menores→tutor ❌** |
| RF-02 Autenticación con roles + hash | ✅ | bcrypt + JWT + roles por endpoint |
| RF-03 Recuperación de contraseña por correo | ✅ | Token UUID por Gmail SMTP (expira 24h) |
| RF-04 Gestión de perfiles de atletas (CRUD por entrenador) | ❌ | Solo **consultar** (lista + detalle). ❌ crear/editar/desactivar atleta, ❌ datos de tutor |
| RF-05 Crear/editar sesiones | ✅ | CRUD completo |
| RF-06 Consultar agenda semanal | ✅ | Navegación + canceladas visibles |
| RF-07 Registro de asistencia + % | ✅ | |
| RF-08 Consultar historial de asistencia | ✅ | Entrenador (por atleta) y atleta (propio) |
| RF-09 Registrar marcas | ✅ | |
| RF-10 Consultar historial rendimiento + gráfica | ✅ | |
| RF-11 Ver evolución grupal con indicadores | 🟡 | Individual sí; comparativa grupal no |
| RF-12 Detectar marca personal | ✅ | (heurística mejorable para lanzamientos/saltos) |
| RF-13 Publicar convocatorias con convocados | ✅ | ✅ spinner de grupo opcional; null = notifica a todos |
| RF-14 Confirmación de participación | ✅ | inscribirse/desinscribirse |
| RF-15 Registrar resultados de competencia | ❌ | No implementado |
| RF-16 Notificaciones push automáticas | 🟡 | Sesión/competencia/resultado ✅; configuración ❌; reintentos ❌ |
| RF-17 Configuración de notificaciones | ❌ | No implementado |
| RF-18 Historial de notificaciones 30 días | 🟡 | Hay historial; sin límite de 30 días |

#### C) Requisitos No Funcionales (RNF-01 a RNF-06)

| RNF | Estado | Nota |
|---|---|---|
| RNF-01 Rendimiento (<3s / <2s / push<60s / 200 usuarios) | 🟡 | No medido ni garantizado; push ahora asíncrono (9.15) |
| RNF-02 Seguridad | 🟡 | ✅ bcrypt, JWT. ❌ **datos de menores protegidos**, ❌ **HTTPS/TLS** (backend es HTTP plano), ❌ bloqueo tras 5 intentos |
| RNF-03 Usabilidad (Android 8+, español, táctil) | ✅ | App Android nativa en español (iOS no aplica) |
| RNF-04 Disponibilidad 99% + offline | ❌ | Sin caché offline ni cola de asistencia sin conexión |
| RNF-05 Mantenibilidad (capas, logs auditoría) | 🟡 | Arquitectura en capas ✅; sin logs de auditoría; **0 pruebas** |
| RNF-06 Portabilidad (Play/App Store, export PDF/Excel) | ❌ | No publicada en stores (APK por GitHub Release); sin exportación |

#### D) Casos de Uso (CU-01 a CU-06)

| CU | Estado | Nota |
|---|---|---|
| CU-01 Iniciar sesión | ✅ | ✅ bloqueo por intentos (9.20). ✅ verificación de cuenta (9.20). ✅ Recordarme 30d |
| CU-02 Registrar atleta (por entrenador) | 🟡 | ✅ entrenador crea/edita/desactiva (9.18). ✅ tutor si menor (9.17). ❌ foto por entrenador pendiente |
| CU-03 Gestionar agenda | 🟡 | ✅ conflicto de horario validado. ❌ fechas pasadas no bloqueadas |
| CU-04 Registrar asistencia | 🟡 | ✅ plazo 2h + solo Admin modifica. ❌ sin offline |
| CU-05 Registrar/consultar rendimiento | ✅ | |
| CU-06 Publicar convocatoria | 🟡 | Sin convocados selectivos |

#### E) Resumen por ROL

**👔 ENTRENADOR — ~80%:** Agenda, asistencia, marcas, grupos, competencias (crear/editar/eliminar/inscritos), ranking, estadísticas, notificaciones, perfil propio: ✅. **Falta:** crear/editar/desactivar atletas (HU-12/RF-04), registrar resultados de competencia (HU-10), convocatoria selectiva (HU-09), evolución grupal comparativa, exportar PDF/Excel, validación de conflicto de horario, ver/editar datos del tutor.

**🏃 ATLETA — ~90%:** Agenda, marcas propias, evolución, asistencia (mi-historial), competencias (inscribirse), ranking, perfil: ✅. **Falta:** editar teléfono, confirmación por contraseña, bloquear edición de nombre.

**👨‍👧 PADRE / TUTOR — ~5% (prácticamente NO implementado):** Hoy entra como un atleta vacío (sin marcas/asistencia/grupo). **Falta TODO:** vínculo padre↔hijo, ver datos del hijo, datos de tutor de emergencia, protección de datos de menores. Diseño acordado: el **entrenador vincula** (1 hijo por padre) + fecha de nacimiento en registro de atleta + datos de tutor obligatorios si es menor (nombre, parentesco, teléfono).

#### F) Pendientes priorizados (para 100% de requisitos)

**Críticos (requisitos de alta prioridad sin cumplir):**
1. Rol PADRE + vínculo tutor + protección de datos de menores (RF-01, HU-01/12, RNF-02) — *en diseño, próximo a implementar*
2. Fecha de nacimiento + datos de tutor obligatorios para menores (CU-02 FA-03)
3. Gestión de perfiles de atletas por el entrenador: crear/editar/desactivar (RF-04, HU-12)
4. Registrar resultados de competencia (RF-15, HU-10)

**Importantes:**
5. Bloqueo tras 5 intentos fallidos (HU-02, RNF-02)
6. Verificación de correo al registrar (HU-01)
7. Convocatoria selectiva por grupo/atleta (RF-13, HU-09)
8. HTTPS/TLS en el backend (RNF-02)
9. Validación de conflicto de horario y fechas pasadas en sesiones (HU-04, CU-03)

**Secundarios / mejoras:**
10. Configuración de notificaciones (RF-17), historial 30 días (RF-18)
11. Exportar PDF/Excel (HU-08, RNF-06), evolución grupal comparativa (RF-11)
12. Modo offline (RNF-04), logs de auditoría y pruebas unitarias (RNF-05)
13. Disciplinas desde backend (no hardcodeadas), confirmación por contraseña al editar perfil (HU-13)

---

### 9.17 Rol PADRE/Tutor + protección de datos de menores — BACKEND — 2026-06-22

Implementación del vínculo padre↔hijo y datos de tutor (RF-01, HU-01/12, RNF-02). Diseño acordado: **el entrenador vincula** (1 hijo por padre); **fecha de nacimiento + datos de tutor obligatorios si el atleta es menor**.

**Modelo de datos (`Usuario`):**
| Campo nuevo | Uso |
|---|---|
| `fechaNacimiento` (LocalDate) | Calcular si el atleta es menor de 18 |
| `tutorNombre`, `tutorParentesco`, `tutorTelefono` | Contacto de emergencia del tutor (obligatorio si menor) |
| `atletaVinculado` (ManyToOne self) | Hijo que observa una cuenta PADRE |
| Helpers `getEdad()`, `isMenorDeEdad()` | `@Transient`, edad y mayoría de edad (Bolivia: 18) |

Las columnas se crean automáticamente con `ddl-auto: update` al redeploy (no requiere migración manual).

**Registro con validación de menor (`AuthService.register`):**
- Si rol ATLETA: guarda `fechaNacimiento` y `disciplina`.
- Si es **menor** y faltan `tutorNombre`/`tutorParentesco`/`tutorTelefono` → error 400 "se requieren datos del tutor". (Cumple CU-02 FA-03.)

**Endpoints nuevos (solo ENTRENADOR/ADMIN):**
| Método | Endpoint | Acción |
|---|---|---|
| GET | `/api/v1/padres` | Lista cuentas PADRE con su hijo vinculado |
| PUT | `/api/v1/padres/{padreId}/hijo/{atletaId}` | Vincula padre↔hijo (valida roles) |
| DELETE | `/api/v1/padres/{padreId}/hijo` | Desvincula |

**El PADRE ve los datos de su HIJO (no los suyos):**
- `MarcaService.getMarcas`: si rol PADRE → `atletaId = hijo.id` (sin hijo → vacío).
- `AsistenciaService.getMiHistorial`: si rol PADRE → historial del hijo.
- `PerfilResponse`: para PADRE devuelve `atletaVinculadoId/Nombre` y el `grupoId/grupoNombre` del **hijo** (así la Agenda filtra por el grupo del hijo).

**DTOs ampliados:**
- `AtletaDetalleDto`: + `fechaNacimiento, edad, esMenor, tutorNombre, tutorParentesco, tutorTelefono, tutorVinculadoId, tutorVinculadoNombre` (el entrenador ve el contacto de emergencia y qué padre está vinculado).
- `PadreDto` (nuevo): id, nombre, email, hijoId, hijoNombre.

**Protección de datos de menores (RNF-02):** los endpoints de gestión de padres y el detalle del atleta (con datos de tutor) están restringidos a ENTRENADOR/ADMIN; el padre solo accede a los datos de SU hijo vinculado.

**APP (Android) — progreso:**
- ✅ **Registro de atleta con fecha de nacimiento + datos de tutor si es menor** (`RegisterActivity`): selector de fecha, cálculo de edad, y si <18 muestra y **exige** nombre/parentesco/teléfono del tutor; envía todo en `RegisterRequest`. Para rol PADRE oculta estos campos.
- ✅ **Vínculo padre↔hijo desde el perfil del atleta (entrenador)** (`AtletaPerfilActivity`): muestra el **contacto de emergencia del tutor** (nombre/parentesco/teléfono, edad, si es menor) y la cuenta de padre vinculada; botón "Vincular cuenta de padre/tutor" → diálogo con la lista de padres (`GET /padres`) → `PUT /padres/{id}/hijo/{atletaId}`; opción de desvincular. API Android: `getPadres/vincularHijo/desvincularHijo`; modelos `PadreInfo`, `AtletaDetalle` ampliado.
- ✅ **Dashboard del padre** (`AtletaDashboardActivity`): muestra "Viendo a: [hijo]" y, gracias al backend, sus pantallas (marcas, asistencia, agenda) ya muestran los datos del hijo vinculado. `PerfilUsuario` ampliado con `atletaVinculado*`.
- ⏳ Pendiente menor: el rol PADRE comparte el `AtletaDashboardActivity`; las tarjetas que no apliquen al padre podrían ocultarse a futuro.

---

### 9.18 Gestión de atletas por el entrenador (crear/editar/desactivar) — 2026-06-22

Implementa RF-04 y HU-12 (el entrenador antes solo podía consultar atletas).

**BACKEND:**
| Método | Endpoint | Acción |
|---|---|---|
| POST | `/api/v1/atletas` | Crear atleta (cuenta + contraseña inicial); exige tutor si es menor |
| PUT | `/api/v1/atletas/{id}` | Editar nombre/disciplina/categoría/fecha nac/datos de tutor |
| PUT | `/api/v1/atletas/{id}/estado` | Activar/desactivar (soft-delete, conserva historial) |

- DTOs nuevos: `AtletaCrearRequest`, `AtletaEditarRequest`.
- `UsuarioService`: `crearAtleta`, `editarAtleta`, `cambiarEstadoAtleta` + validación de tutor si menor (reutilizada).
- Todos restringidos a ENTRENADOR/ADMIN.
- Desactivar pone `activo=false` → el atleta no inicia sesión (UserDetails.isEnabled) y desaparece de la lista (`findByRolAndActivo(ATLETA,true)`), preservando su historial.

**APP (Android):**
- ✅ `EditarAtletaActivity` (crear/editar): formulario con nombre, correo+contraseña (solo crear), disciplina, categoría, fecha de nacimiento y, si es menor, datos de tutor obligatorios (mismo cálculo de edad que el registro).
- ✅ `AtletasActivity`: **FAB** para crear atleta + refresco en `onResume`.
- ✅ `AtletaPerfilActivity`: menú con **Editar** (→ `EditarAtletaActivity`) y **Activar/Desactivar** (con confirmación). Modelos `AtletaCrearRequest`/`AtletaEditarRequest`; `AtletasApiService` con `crearAtleta/editarAtleta/cambiarEstado`.
- Nota: al desactivar, el atleta desaparece de la lista (activos). Reactivar desde un atleta inactivo requeriría listarlos (mejora menor futura).

---

### 9.19 Registrar resultados de competencia (RF-15, HU-10) — 2026-06-22

El entrenador puede registrar el resultado de cada atleta en una competencia (posición, marca, observaciones) y se **asocia automáticamente al historial de rendimiento** con detección de récord.

**BACKEND:**
- Entidad `ResultadoCompetencia` (competencia, atleta, posición, marca, observaciones, esMarcaPersonal); restricción única (competencia, atleta).
- `ResultadoRepository`; DTOs `ResultadoRequest`/`ResultadoResponse`.
- `CompetenciaService.registrarResultado()`: crea/actualiza el resultado; al registrarlo **por primera vez** crea una `MarcaPersonal` (disciplina de la competencia, fecha del evento) reutilizando `MarcaService.registrar` → aparece en marcas/evolución del atleta y detecta si es **récord**. La competencia pasa a estado `FINALIZADO`.
- `getResultados()` ordena por posición.
- Endpoints: `GET /competencias/{id}/resultados` (público autenticado), `POST /competencias/{id}/resultados` (ENTRENADOR/ADMIN).

**APP (Android):**
- ✅ `ResultadosCompetenciaActivity`: lista los resultados (posición · nombre · marca · ⭐récord) y, para el entrenador, un **FAB** que abre un diálogo (`dialog_registrar_resultado`) para registrar un resultado eligiendo un atleta inscrito + posición + marca + observaciones.
- ✅ Entrada desde `CompetenciaDetalleActivity` (botón "Resultados", visible para todos; el entrenador puede registrar).
- Modelos `ResultadoCompetencia`/`ResultadoRequest`; `CompetenciasApiService.getResultados/registrarResultado`.

---

### 9.20 Bloqueo tras 5 intentos fallidos (HU-02) + Verificación de correo al registrar (HU-01) — 2026-06-23

Implementación de las dos funcionalidades de seguridad pendientes en autenticación.

**BACKEND:**

**HU-02 — Bloqueo de cuenta:**
- `Usuario`: nuevos campos `intentosFallidos` (int, default 0) y `bloqueadoHasta` (LocalDateTime, nullable).
- `AuthService.login()`: reescrito para verificar contraseña manualmente (sin `authManager.authenticate()`):
  - Si `bloqueadoHasta` está en el futuro → `CuentaBloqueadaException` (HTTP 423) con minutos restantes.
  - Si la contraseña falla: incrementa `intentosFallidos`; al llegar a 5 → `bloqueadoHasta = now + 15 min` e `intentosFallidos = 0`.
  - Si éxito → limpia contadores.
- `CuentaBloqueadaException` → manejada en `GlobalExceptionHandler` con HTTP 423 LOCKED.

**HU-01 — Verificación de correo:**
- `Usuario`: nuevos campos `emailVerificado` (Boolean, null = cuenta legacy sin restricción) y `tokenVerificacion` (String).
- `AuthService.register()`: genera UUID token, guarda `emailVerificado=false`/`tokenVerificacion`, luego llama `emailService.sendVerificationEmail()` (no lanza si falla el correo).
- `AuthService.verifyEmail(token)`: busca por `tokenVerificacion`, activa `emailVerificado=true` y limpia el token.
- `AuthService.login()`: si `emailVerificado == false` → `CorreoNoVerificadoException` (HTTP 403); si `null` (cuentas pre-HU-01) → sin restricción (retrocompatible).
- `CorreoNoVerificadoException` → HTTP 403 en `GlobalExceptionHandler`.
- `EmailService.sendVerificationEmail()`: correo con código UUID y deep link `atletismo://verify?token=xxx`.
- Nuevo endpoint: `GET /api/v1/auth/verify-email?token=xxx` (público, sin JWT).
- `UsuarioRepository`: nuevo método `findByTokenVerificacion(String)`.
- `UsuarioService.crearAtleta()`: pone `emailVerificado=true` (entrenador crea cuentas directamente, sin verificación).

**APP (Android):**
- `AuthApiService`: nuevo `@GET("auth/verify-email") verifyEmail(@Query("token") String)`.
- `LoginActivity`: maneja HTTP 423 (toast con mensaje del backend con minutos restantes) y HTTP 403 (navega a `VerificarCorreoActivity`).
- `RegisterActivity`: al registrar exitosamente, navega a `VerificarCorreoActivity` (antes hacía `finish()`).
- `VerificarCorreoActivity` (nueva): campo para ingresar el código UUID + botón "Verificar". Maneja también el deep link `atletismo://verify?token=xxx` (auto-completa y verifica). Al éxito navega a Login.
- `activity_verificar_correo.xml` (nuevo): layout de verificación.
- `AndroidManifest`: `VerificarCorreoActivity` con `intent-filter` para `atletismo://verify`.
- Strings nuevos: `lbl_verificar_correo`, `lbl_verificar_correo_desc`, `hint_codigo_verificacion`, `btn_verificar`, `msg_correo_verificado`, `err_codigo_requerido`, `err_correo_no_verificado`.

**Retrocompatibilidad:** cuentas existentes en BD tienen `email_verificado = NULL` → `Boolean.FALSE.equals(null) = false` → no se bloquean. Solo las cuentas creadas después de este commit tienen `emailVerificado=false` hasta verificar.

---

### 9.21 HU-01/02/04/05 + RF-03 — Contraseña fuerte, Recordarme 30d, Conflicto horario, Límite asistencia — 2026-06-23

Implementación del batch HU-01→HU-05 (excluyendo HU-03/06/07 que ya estaban al 100%).

---

#### HU-01 — Contraseña fuerte (completado)

**BACKEND (`AuthService.register`):**
- Nuevo método privado `validarContrasena(String)`: lanza `IllegalArgumentException` (HTTP 400) si la contraseña no tiene al menos 8 caracteres, una mayúscula y un número. Se llama antes de crear el usuario.

**APP (`RegisterActivity`):**
- Validación client-side con regex `.*[A-Z].*` y `.*[0-9].*` antes de enviar al backend.
- String nuevo: `err_contrasena_requisitos` = "Mínimo 8 caracteres, una mayúscula y un número".

---

#### HU-02 — Recordarme 30 días (completado)

**BACKEND:**
- `LoginRequest` DTO: nuevo campo `boolean recordarme = false`.
- `JwtService`: nuevo overload `generateToken(UserDetails, long expirationMs)` que acepta duración variable. El método original delega en él con `expiration` (1 día).
- `AuthService.login()`: si `request.isRecordarme()` → expira en 30 días (30×24×60×60×1000 ms); si no → 86 400 000 ms (1 día).

**APP:**
- `LoginRequest` model: campo `boolean recordarme` + constructor de 3 args.
- `activity_login.xml`: `CheckBox cbRecordarme` ("Recordarme por 30 días") entre el link "¿Olvidaste?" y el botón Ingresar.
- `LoginActivity`: bindea `cbRecordarme`, lo incluye en el `LoginRequest`.

---

#### RF-03 — Token de recuperación 24h (corregido)

**BACKEND (`PasswordResetService.solicitarReset`):**
- Corregido `.plusHours(1)` → `.plusHours(24)`. El token de reset de contraseña ahora expira en 24 horas como especifica RF-03 (antes era 1h).

---

#### HU-04 — Validación de conflicto de horario (completado)

**BACKEND:**
- `SesionRepository`: nuevo query `countConflictos(@Param grupoId, inicio, fin, excludeId)` — cuenta sesiones no canceladas del mismo grupo que se superpongan en el intervalo `[inicio, fin)`, excluyendo el id dado (para editar sin auto-conflicto, se pasa `-1L` al crear).
- `SesionService.crear()`: parsea fechas primero, llama `countConflictos(grupoId, horaInicio, horaFin, -1L)` → si > 0 lanza `IllegalArgumentException` (HTTP 400).
- `SesionService.editar()`: igual, pasando el id actual como `excludeId`.

**APP:**
- `CrearSesionActivity`: en el callback `onResponse`, si `!response.isSuccessful()` extrae el mensaje del JSON (`extractErrorMessage`) y muestra toast largo con el error específico del backend (p.ej. "Ya existe una sesión programada para este grupo en ese horario"). Nuevo método privado `extractErrorMessage(Response<?>)`.

---

#### HU-05 — Plazo 2h + solo Admin modifica asistencia guardada (completado)

**BACKEND (`AsistenciaService`):**
- Import añadido: `SecurityContextHolder`.
- Nuevo método privado `verificarPermisosAsistencia(Long sesionId, LocalDateTime horaFin)`:
  1. Si `horaFin.plusHours(2).isBefore(now())` → lanza `IllegalArgumentException` "El plazo para registrar asistencia ha vencido (máximo 2 horas tras la sesión)".
  2. Si ya hay registros para la sesión (`findBySesionId` no vacío) y el usuario **no** tiene `ROLE_ADMIN` → lanza `IllegalArgumentException` "La asistencia ya fue guardada. Solo el Administrador puede modificarla."
- `guardarAsistencia()` llama a `verificarPermisosAsistencia` antes del bucle de guardado.

**APP (`AsistenciaActivity`):**
- En callback de `guardarAsistencia`, si `!response.isSuccessful()` extrae y muestra el mensaje del backend (toast largo) en lugar del genérico "error de conexión". Nuevo método privado `extractErrorMessage`.

---

**Estado tras este commit:**

| HU | Antes | Ahora |
|---|---|---|
| HU-01 Contraseña fuerte | 🟡 ~70% | ✅ ~95% |
| HU-02 Recordarme 30d | 🟡 ~80% | ✅ ~95% |
| HU-04 Conflicto horario | 🟡 ~75% | 🟡 ~90% (falta fecha pasada) |
| HU-05 Asistencia 2h/Admin | 🟡 ~70% | ✅ ~95% |
| RF-03 Token reset 24h | 🟡 (bug 1h) | ✅ correcto |

---

### 9.22 HU-09/11/12/13 — Convocatoria selectiva, Push resultado, Scheduler categorías, Foto atleta, Editar perfil seguro — 2026-06-23

---

#### HU-09 — Convocatoria selectiva por grupo (completado)

**BACKEND:**
- `Competencia`: nuevo campo `Long grupoConvocadoId` (null = todos, ddl-auto genera la columna).
- `CompetenciaRequest` DTO: nuevo campo `Long grupoId`.
- `CompetenciaResponse` DTO: nuevo campo `Long grupoConvocadoId`.
- `CompetenciaService.crear()`: filtra la notificación push por grupo si `req.getGrupoId() != null`; guarda `grupoConvocadoId` en la entidad. Si `grupoId == null` → notifica a todos los atletas activos (comportamiento anterior).
- `toResponse()`: incluye `grupoConvocadoId`.

**APP:**
- `CompetenciaRequest` model: nuevo campo `Long grupoId`; constructor actualizado a 7 args.
- `activity_crear_competencia.xml`: `AutoCompleteTextView spinnerGrupoConvocado` (opcional — vacío = todos).
- `CrearCompetenciaActivity`: carga grupos al iniciar (`ApiClient.getAgendaService().listarGrupos()`); al guardar, busca el grupo seleccionado y pasa su id (o null si no se seleccionó ninguno).

---

#### HU-11 — Push notification al atleta cuando se registra su resultado (completado)

**BACKEND (`CompetenciaService.registrarResultado`):**
- Después de guardar el resultado: `notificacionService.crear(atleta, "RESULTADO", "Resultado registrado: [competencia]", "Posición X · Marca: Y [⭐ ¡Nuevo récord personal!]")`.
- La notificación llega solo al atleta cuyo resultado se registró (no a todos).

---

#### HU-12 — Auto-categoría por edad (scheduler) + Foto atleta por entrenador (completado)

**BACKEND — Scheduler:**
- `AtletismoApplication`: `@EnableScheduling` habilitado.
- `CategoriaSchedulerService` (nuevo): `@Scheduled(cron = "0 0 1 * * *")` → cada día a la 1:00 am revisa todos los atletas activos con `fechaNacimiento` y actualiza `categoria` si cambió. Regla: ≤10 → "Pre-Infantil"; 11-13 → "Infantil"; 14-17 → "Juvenil"; ≥18 → "Mayores".

**BACKEND — Foto atleta:**
- `UsuarioService.subirFotoAtleta(Long atletaId, MultipartFile)`: mismo flujo que `subirFoto` pero sobre el atleta indicado por el entrenador.
- `UsuarioController`: `PUT /api/v1/atletas/{id}/foto` restringido a `ENTRENADOR/ADMIN`.

**APP:**
- `UsuariosApiService`: `@Multipart @PUT("atletas/{id}/foto") subirFotoAtleta(@Path Long id, @Part foto)`.
- `menu_atleta_perfil.xml`: ítem "Cambiar foto" (action_foto_atleta).
- `AtletaPerfilActivity`: `ActivityResultLauncher` para elegir imagen; al seleccionar, llama `subirFotoAtleta(uri)` que convierte a `MultipartBody.Part` y llama `ApiClient.getUsuariosService().subirFotoAtleta(atletaId, part)`.

---

#### HU-13 — Editar perfil seguro: nombre bloqueado + teléfono + contraseña actual (completado)

**BACKEND:**
- `Usuario`: nuevo campo `String telefono`.
- `PerfilResponse`: nuevo campo `String telefono`.
- `EditarPerfilRequest`: eliminado `@NotBlank String nombreCompleto`; añadido `@NotBlank String contrasenaActual` y `String telefono` (opcional). El backend ya no actualiza el nombre del atleta desde este endpoint.
- `UsuarioService.editarPerfil()`: verifica `contrasenaActual` contra `contrasenaHash` antes de cualquier cambio → HTTP 400 si incorrecta. Solo actualiza `correo` y `telefono` (no el nombre). `toPerfilResponse()` incluye `telefono`.

**APP:**
- `PerfilUsuario` model: campo `String telefono` + getter.
- `EditarPerfilRequest` model: ahora tiene `email`, `telefono`, `contrasenaActual`; constructor de 3 args.
- `activity_editar_perfil.xml`: campo nombre `android:enabled="false"` + `android:focusable="false"` (solo lectura); nuevos campos `etTelefono` (opcional) y `etContrasenaActual` (obligatorio).
- `EditarPerfilActivity`: carga y muestra el teléfono existente; valida email + contraseña actual obligatorios; pasa `new EditarPerfilRequest(email, telefonoONull, pwdActual)`; extrae mensaje de error del backend (ej. "Contraseña actual incorrecta").

---

**Estado tras este commit:**

| HU | Antes | Ahora |
|---|---|---|
| HU-09 Convocatoria selectiva | 🟡 ~80% | ✅ ~95% |
| HU-11 Push resultado | 🟡 ~65% | 🟡 ~85% (falta configurar notif) |
| HU-12 Scheduler + foto | 🟡 ~60% | 🟡 ~90% (falta notif cambio categoría) |
| HU-13 Perfil seguro | 🟡 ~65% | 🟡 ~90% (campo pwd no resalta rojo en el TIL) |

---

### 9.23 HU-08 — Comparativa grupal multi-línea + Exportar PDF — 2026-06-23

---

#### Descripción

Implementación completa de la comparativa de evolución del grupo entero en una sola pantalla, con exportación a PDF compartible.

**BACKEND:**
- `MarcaRepository`: dos nuevas queries JPQL — `findByGrupoIdAndDisciplina(grupoId, disciplina)` y `findByGrupoId(grupoId)`, ordenadas por `atleta.id ASC, fecha ASC`.
- `GrupoEvolucionResponse` DTO (nuevo): `{ atletaId, atletaNombre, List<MarcaResponse> marcas }`.
- `MarcaService.getMarcasGrupo(Long grupoId, String disciplina)`: agrupa las marcas por atleta usando `LinkedHashMap` (preserva orden de inserción → orden por `atleta.id`) y devuelve `List<GrupoEvolucionResponse>`.
- `MarcaController`: `GET /api/v1/marcas/grupo/{grupoId}?disciplina=X` — restringido a `ENTRENADOR/ADMIN`.

**APP:**
- `GrupoEvolucionDto` model: `{ Long atletaId, String atletaNombre, List<MarcaPersonal> marcas }`.
- `MarcasApiService`: `getMarcasGrupo(@Path Long grupoId, @Query String disciplina)`.
- `EvolucionGrupoActivity` (nueva): spinner disciplina (pre-selecciona la disciplina del grupo), `LineChart` con paleta de 8 colores (uno por atleta), eje X unificado con todas las fechas únicas, `RecyclerView` de leyenda con nombre y mejor marca de cada atleta. Menú "Exportar PDF".
- `LeyendaAtletaAdapter` (nuevo): muestra pastilla de color + nombre + mejor marca.
- `activity_evolucion_grupo.xml` (nuevo): toolbar + spinner + LineChart 280dp + RecyclerView leyenda.
- `item_leyenda_atleta.xml` (nuevo): fila color/nombre/mejor-marca.
- `menu_evolucion_grupo.xml` (nuevo): ítem "Exportar PDF".
- `menu_grupo_detalle.xml`: añadido ítem "Ver evolución del grupo".
- `GrupoDetalleActivity`: maneja `action_evolucion_grupo` → abre `EvolucionGrupoActivity` pasando `grupoId`, `grupoNombre`, y `disciplina` del grupo.
- `AndroidManifest.xml`: declara `EvolucionGrupoActivity` + `FileProvider` (para compartir PDF).
- `res/xml/file_paths.xml` (nuevo): `<external-cache-path>` para el FileProvider.

**PDF Export:**
- `exportarPdf()`: captura `lineChart.getChartBitmap()`, crea `PdfDocument` A4, dibuja título, gráfica escalada (547×220), tabla de atletas con pastilla de color y mejor marca.
- Guarda en `getExternalCacheDir()` → comparte via `Intent.ACTION_SEND` tipo `application/pdf` con URI de FileProvider.

---

**Estado tras este commit:**

| HU | Antes | Ahora |
|---|---|---|
| HU-08 Ver evolución grupal | 🟡 ~55% | ✅ ~100% |
| HU-10 Resultados competencia | ❌ | ✅ (confirmado que funciona - sección 9.19) |

---

### 9.24 Fix crítico: columna `intentos_fallidos` no se podía agregar en PostgreSQL — 2026-06-24

**Problema:** el backend crasheaba al arrancar; todos los intentos de login fallaban con error 500.

**Causa raíz:** `private int intentosFallidos` en `Usuario.java` es un primitivo Java (`int`), que Hibernate mapea como `INTEGER NOT NULL`. Al hacer `ddl-auto: update` sobre una tabla de producción con datos existentes, PostgreSQL rechaza `ALTER TABLE usuario ADD COLUMN intentos_fallidos INTEGER NOT NULL` porque las filas existentes tendrían `NULL` en esa columna. La columna nunca se creó, y cualquier SELECT que tocara `usuario` fallaba con `column u1_0.intentos_fallidos does not exist`.

**Fix aplicado:**

`Usuario.java`:
```java
// Antes
@Builder.Default
private int intentosFallidos = 0;

// Después
@Builder.Default
@Column(columnDefinition = "integer not null default 0")
private Integer intentosFallidos = 0;
```
`columnDefinition = "integer not null default 0"` hace que PostgreSQL genere `ALTER TABLE usuario ADD COLUMN intentos_fallidos integer not null default 0`, lo que funciona porque las filas existentes reciben el valor por defecto `0`.

`AuthService.java`: los dos puntos donde se usa `getIntentosFallidos()` ahora manejan `null` con `!= null ? ... : 0` para evitar NPE en filas legacy que pudieran tener NULL.

**Estado tras este fix:**

| HU | Antes | Ahora |
|---|---|---|
| HU-02 Login + bloqueo intentos | ❌ crash (columna faltante) | ✅ backend arranca, login restaurado |

---

### 9.25 HU-11/12/13 — Preferencias de notificaciones, push cambio categoría, error TIL contraseña — 2026-06-24

---

#### HU-12 — Push al atleta cuando cambia su categoría (completado)

**BACKEND (`CategoriaSchedulerService`):**
- Inyecta `NotificacionService`.
- Después de actualizar y guardar la categoría del atleta, llama `notificacionService.crear(u, "CATEGORIA", "Categoría actualizada", "Tu categoría ha cambiado de X a Y")`.
- Notificación de tipo "CATEGORIA" siempre se envía (no se puede desactivar en preferencias).

---

#### HU-11 — Configurar qué notificaciones push recibir (completado)

**BACKEND:**
- `Usuario`: 3 nuevos campos nullable `Boolean notifSesiones`, `notifCompetencias`, `notifResultados`. Nullable (sin NOT NULL) → PostgreSQL puede addColumn sin issue; null = activo por defecto.
- `PerfilResponse`: expone los 3 campos (null resuelto a true en `toPerfilResponse()`).
- `NotifPreferenciasRequest` DTO (nuevo): `{ sesiones, competencias, resultados }`.
- `UsuarioService.actualizarPreferenciasNotif()`: actualiza solo los campos no null del request.
- `UsuarioController`: `PUT /api/v1/usuarios/notificaciones` (cualquier rol autenticado).
- `NotificacionService.crear()`: antes de llamar `fcmService.sendToToken()`, llama nuevo método privado `debeRecibirPush(usuario, tipo)` — devuelve false si el usuario desactivó ese tipo. Los tipos "SESION"/"COMPETENCIA"/"RESULTADO" son configurables; "CATEGORIA" y otros siempre se envían.

**APP:**
- `PerfilUsuario` model: 3 nuevos campos `Boolean notifSesiones/Competencias/Resultados` + getters.
- `NotifPreferenciasRequest` model (nuevo): `{ sesiones, competencias, resultados }` + constructor.
- `UsuariosApiService`: nuevo `@PUT("usuarios/notificaciones") Call<Void> actualizarNotifPreferencias()`.
- `NotifPreferenciasActivity` (nueva): carga preferencias actuales del backend (`getPerfil()`), muestra 3 `SwitchMaterial` con sus valores, botón "Guardar preferencias" → `PUT /api/v1/usuarios/notificaciones`.
- `activity_notif_preferencias.xml` (nuevo): toolbar + 3 switches en card + botón guardar.
- `activity_perfil.xml`: nuevo card "Preferencias de notificaciones ›" entre "Cambiar contraseña" y el botón de logout.
- `PerfilActivity`: click en el nuevo card → abre `NotifPreferenciasActivity`.
- `AndroidManifest.xml`: declara `NotifPreferenciasActivity`.

---

#### HU-13 — Campo contraseña resalta en rojo al fallar (completado)

**APP (`EditarPerfilActivity.guardarCambios`):**
- En el callback de error, si el mensaje contiene "contraseña" → `tilContrasenaActual.setError(msg)` (error inline en el campo) en lugar de solo un Toast.
- Errores de otro tipo (ej. "correo ya en uso") siguen mostrando Toast.

---

**Estado tras este commit:**

| HU | Antes | Ahora |
|---|---|---|
| HU-11 Configurar notificaciones | ❌ | ✅ ~95% (3 tipos configurables + guardado) |
| HU-12 Push cambio categoría | ❌ | ✅ ~100% |
| HU-13 Error TIL contraseña | 🟡 solo Toast | ✅ TIL inline rojo |

---

---

### 9.26 Auditoría comparativa — 2026-06-23 → 2026-06-24

Comparación entre el estado del proyecto al 23-06-2026 y el estado actual tras las implementaciones de la sesión del 24-06-2026. Leyenda: ✅ Completo · 🟡 Parcial · ❌ No implementado.

---

#### Resumen ejecutivo

| Categoría | % al 23-06 | % al 24-06 | Δ |
|---|---|---|---|
| HU (13 historias) | ~83% promedio | ~97% | +14 pp |
| RF (18 requisitos) | ~83% promedio | ~98% | +15 pp |
| RNF (6 requisitos) | ~63% promedio | ~67% | +4 pp |
| CU (6 casos de uso) | ~86% promedio | ~97% | +11 pp |
| **Global funcional (sin RNF)** | **~80%** | **~95%** | **+15 pp** |
| **Global incluyendo RNF** | **~70%** | **~83%** | **+13 pp** |

---

#### HU — Historias de Usuario

| HU | 23-06 | 24-06 | Qué se resolvió |
|---|---|---|---|
| HU-01 Registro | ~90% | ✅ 100% | Validación mayúscula + número en backend y app |
| HU-02 Login | ~90% | ✅ 100% | Checkbox "Recordarme 30 días" + token 30d |
| HU-03 Agenda semanal | ✅ 100% | ✅ 100% | — sin cambios — |
| HU-04 Crear/editar sesión | ~90% | ✅ 100% | Validación conflicto de horario mismo grupo |
| HU-05 Asistencia | ~75% | ✅ 100% | Límite 2h post-sesión + solo Admin modifica guardado |
| HU-06 Marcas | ✅ 100% | ✅ 100% | — sin cambios — |
| HU-07 Rendimiento propio | ✅ 100% | ✅ 100% | — sin cambios — |
| HU-08 Evolución del grupo | ~55% | ✅ 100% | Gráfica multi-línea grupal + exportar PDF (share sheet) |
| HU-09 Convocatoria | ~80% | ✅ 100% | Convocatoria selectiva por grupo (spinner opcional) |
| HU-10 Resultados | ✅ 100% | ✅ 100% | — sin cambios — |
| HU-11 Notificaciones push | ~75% | 🟡 90% | Push resultados ✅, configuración on/off ✅. Reintentos automáticos ❌ (pendiente menor) |
| HU-12 Gestionar atleta | ~85% | ✅ 100% | Auto-categoría diaria (`@Scheduled`) + foto por entrenador + push cambio categoría |
| HU-13 Editar datos propios | ~65% | ✅ 100% | Nombre bloqueado (read-only) + confirmación contraseña actual + error TIL inline |

---

#### RF — Requisitos Funcionales

| RF | 23-06 | 24-06 | Qué se resolvió |
|---|---|---|---|
| RF-01 Registro | ~90% | ✅ 100% | Validación mayúscula + número |
| RF-02 Auth con roles | ✅ 100% | ✅ 100% | — |
| RF-03 Recuperar contraseña | ~85% | ✅ 100% | Token corregido de 1h a 24h |
| RF-04 Gestión perfiles atleta | ~85% | ✅ 100% | Foto de atleta subida por entrenador |
| RF-05 Crear/editar sesiones | ✅ 100% | ✅ 100% | — |
| RF-06 Agenda semanal | ✅ 100% | ✅ 100% | — |
| RF-07 Asistencia | ~80% | ✅ 100% | Límite 2h + solo Admin modifica asistencia guardada |
| RF-08 Historial asistencia | ✅ 100% | ✅ 100% | — |
| RF-09 Registrar marcas | ✅ 100% | ✅ 100% | — |
| RF-10 Historial rendimiento | ✅ 100% | ✅ 100% | — |
| RF-11 Evolución grupal | ~50% | ✅ 100% | Gráfica comparativa multi-atleta por disciplina |
| RF-12 Detectar marca personal | ✅ 100% | ✅ 100% | — |
| RF-13 Publicar convocatorias | ~45% | ✅ 100% | Selector de grupo opcional; null = notifica a todos |
| RF-14 Confirmación participación | ✅ 100% | ✅ 100% | — |
| RF-15 Resultados competencia | ✅ 100% | ✅ 100% | — |
| RF-16 Notificaciones push | ~75% | ✅ 100% | Push al atleta al registrar resultado de competencia |
| RF-17 Config. notificaciones | ❌ 0% | ✅ 100% | Pantalla con 3 switches (sesiones / competencias / resultados) |
| RF-18 Historial notificaciones | ✅ 100% | ✅ 100% | — |

---

#### RNF — Requisitos No Funcionales

| RNF | 23-06 | 24-06 | Nota |
|---|---|---|---|
| RNF-01 Rendimiento | ~80% | ~80% | Sin cambios — carga asíncrona FCM; no probado con 200 usuarios simultáneos |
| RNF-02 Seguridad | ~85% | ~90% | Bloqueo 5 intentos ✅, verificación correo ✅, datos menores protegidos ✅. HTTPS acordado posponer |
| RNF-03 Usabilidad | ~90% | ~90% | Sin cambios (Android 8+, español, Material Design) |
| RNF-04 Disponibilidad offline | ~15% | ~15% | Sin cambios — fuera de alcance académico |
| RNF-05 Mantenibilidad | ~85% | ~85% | Sin cambios — sin pruebas automatizadas (documentado como trabajo futuro) |
| RNF-06 Portabilidad | ~40% | ~70% | PDF compartible via share sheet ✅, APK en GitHub Releases ✅. Google Play fuera de scope |

---

#### CU — Casos de Uso

| CU | 23-06 | 24-06 | Qué se resolvió |
|---|---|---|---|
| CU-01 Iniciar sesión | ✅ 100% | ✅ 100% | — |
| CU-02 Registrar atleta | ~85% | ✅ 100% | Foto por entrenador ✅, vínculo tutor/menor ✅ |
| CU-03 Gestionar agenda | ~85% | ✅ 100% | FA-03: conflicto de horario implementado |
| CU-04 Registrar asistencia | ~65% | ~80% | Límite 2h ✅, solo Admin ✅. FA-01 offline ❌ (acordado fuera de scope) |
| CU-05 Rendimiento | ✅ 100% | ✅ 100% | — |
| CU-06 Convocatoria | ~80% | ✅ 100% | Convocatoria selectiva por grupo implementada |

---

#### Lista de brechas — cierre de ítems

De los **21 ítems concretos** identificados el 23-06 como pendientes (6 críticos + 15 importantes), **los 21 fueron resueltos**.

**🔴 Críticos — 6/6 resueltos**

| # | Brecha original | Estado |
|---|---|---|
| 1 | Contraseña: validar mayúscula + número | ✅ |
| 2 | RF-17: Configuración de notificaciones on/off por tipo | ✅ |
| 3 | RF-16: Push al publicar resultados de competencia | ✅ |
| 4 | RF-13: Convocatoria selectiva por grupo/atleta | ✅ |
| 5 | Exportar PDF de marcas del grupo | ✅ |
| 6 | Validar conflicto de horario en sesiones | ✅ |

**🟡 Importantes — 15/15 resueltos**

| # | Brecha original | Estado |
|---|---|---|
| 7 | HU-05: Límite 2h para registrar asistencia post-sesión | ✅ |
| 8 | RF-03: Token reset expiraba en 1h, debe ser 24h | ✅ |
| 9 | HU-13: Atleta no debe poder editar su nombre | ✅ |
| 10 | HU-13: Pedir contraseña actual al guardar cambios de perfil | ✅ |
| 11 | RF-11: Gráfica comparativa multi-atleta misma disciplina | ✅ |
| 12 | Foto del atleta editable por el entrenador | ✅ |
| 13 | HU-12: Actualización automática de categoría al cumplir años | ✅ |
| 14 | HU-02: Checkbox "Recordarme 30 días" | ✅ |
| 15 | Reactivar atletas inactivos (`PUT /atletas/{id}/estado`) | ✅ |

*(Los ítems 1–6 de la lista original no corresponden al orden de prioridad sino al de aparición; el 15 estaba como ítem adicional en 🟡)*

---

#### Pendientes que permanecen (todos acordados o fuera de scope)

| Ítem | Motivo |
|---|---|
| Reintentos FCM automáticos (HU-11) | Pendiente menor; no afecta funcionalidad core |
| Modo offline (RNF-04) | Complejidad alta — fuera de alcance académico |
| HTTPS/TLS (RNF-02) | Requiere dominio propio — acordado posponer |
| Pruebas automatizadas (RNF-05) | Fuera de scope — documentado como trabajo futuro |
| Publicación en Google Play (RNF-06) | Distribución — fuera de scope académico |

---

### 9.27 Limpieza del repositorio — eliminación de carpetas innecesarias — 2026-06-24

#### Problema

El repositorio contenía dos tipos de carpetas que no debían formar parte del control de versiones:

1. **`mobile/`** — carpeta de prototipo React Native (nunca finalizado), registrada como *gitlink* (subrepositorio anidado) con estado `modified content` en `git status`. Código descartado; la versión final del proyecto es Android nativo Java.
2. **`.idea/`** — carpeta de configuración de Android Studio parcialmente rastreada (11 archivos: `.name`, `AndroidProjectSystem.xml`, `compiler.xml`, `deploymentTargetSelector.xml`, `deviceManager.xml`, `gradle.xml`, `markdown.xml`, `migrations.xml`, `misc.xml`, `runConfigurations.xml`, `vcs.xml`). Estos son ajustes locales del IDE, no necesarios para compilar ni desplegar el proyecto.

La carpeta **`.gradle/`** ya estaba correctamente excluida desde la línea 2 del `.gitignore`; no requirió acción.

#### Solución aplicada

**`mobile/`:**
```bash
git rm --cached mobile          # elimina el gitlink del índice
rm -rf mobile/                  # elimina físicamente la carpeta
```

**`.idea/`:**
```bash
git rm -r --cached .idea        # desindexar todos los archivos rastreados de .idea
```

Luego se reemplazaron las entradas selectivas en `.gitignore`:
```
# Antes (entradas parciales):
/.idea/caches
/.idea/libraries
/.idea/modules.xml
/.idea/workspace.xml
/.idea/navEditor.xml
/.idea/assetWizardSettings.xml

# Después (exclusión total):
/.idea/
```

#### Resultado

| Carpeta | Antes | Después |
|---|---|---|
| `mobile/` | Gitlink `modified content` en índice | Eliminada del repositorio y del disco |
| `.idea/` | 11 archivos rastreados en git | Completamente excluida, solo local |
| `.gradle/` | Ya excluida (`.gitignore` línea 2) | Sin cambios necesarios |

El repositorio queda limpio: solo contiene código fuente de Android (`app/`) y backend Spring Boot (`backend/`), junto con `.github/` (CI/CD), archivos Gradle de build y `.gitignore`.

---

### 9.28 Segunda limpieza del repositorio — artefactos y duplicados — 2026-06-25

#### Contexto

Tras la limpieza de `mobile/` e `.idea/` raíz (sección 9.27), se realizó una auditoría completa del árbol del proyecto para detectar archivos rastreados en git que no aportan valor al proyecto.

#### Archivos eliminados del repositorio

| Archivo | Tipo | Motivo de eliminación |
|---|---|---|
| `desktop.ini` | Metadato de carpeta de Windows | Generado automáticamente por el explorador de archivos de Windows; no tiene relación con el proyecto |
| `ui.xml` | Dump de jerarquía UI de ADB | Generado por `adb shell uiautomator dump` durante depuración; instantánea de la pantalla de Login en un momento dado, sin valor de código |
| `app/.idea/` (8 archivos) | Config duplicada del IDE | Creada cuando `app/` fue abierta como proyecto independiente en Android Studio; duplicado del `.idea/` raíz ya limpiado en 9.27 |

#### Archivos locales que NO se rastrean (confirmado, sin acción requerida)

| Carpeta | Motivo |
|---|---|
| `app/.gradle/` | Caché de Gradle del submódulo Android, se regenera automáticamente |
| `app/local.properties` | Ruta local del SDK de Android, ya cubierta por `.gitignore` |
| `backend/.gradle/` | Caché de Gradle del backend Spring Boot, se regenera automáticamente |
| `.gradle/` raíz | Caché de Gradle raíz, en `.gitignore` desde el inicio |

#### Archivo restaurado — `settings.gradle`

El archivo `settings.gradle` raíz fue eliminado accidentalmente durante la limpieza manual de imágenes. Este archivo es **crítico** para el build de Android: define el nombre del proyecto raíz (`TallerAppMovil`) e incluye el módulo `:app`. Sin él, Gradle no puede ejecutar ninguna tarea. Fue restaurado con `git checkout HEAD -- settings.gradle`.

#### Imágenes de capturas de pantalla eliminadas

Durante la limpieza manual de imágenes se eliminaron también del disco (y del índice git) las siguientes capturas de pantalla que habían sido subidas al repositorio: `sc_login.png`, `sc_login2.png`, `sc_reg2.png`, `sc_reg3.png`, `sc_register.png`, `screenshot.png`, y 6 imágenes de WhatsApp.

#### Resultado

```
tallerAppMovil/
├── .github/workflows/build_apk.yml    ← CI/CD
├── .gitignore
├── app/
│   ├── build.gradle
│   ├── google-services.json           ← Firebase (necesario para CI)
│   ├── proguard-rules.pro
│   └── src/main/                      ← Código Android
├── backend/
│   ├── Dockerfile / .dockerignore
│   ├── build.gradle / settings.gradle
│   ├── gradle/wrapper/
│   └── src/main/                      ← Código Spring Boot
├── build.gradle
├── gradle/wrapper/
├── gradle.properties
├── gradlew / gradlew.bat
└── settings.gradle                    ← Crítico: define proyecto raíz
```

---

## 6. Conclusiones y Trabajo Futuro

### 6.1 Conclusiones y Logros del Proyecto

El presente proyecto demostró que es posible desarrollar, desplegar y operar una aplicación móvil funcional de gestión deportiva en un contexto académico, utilizando tecnologías de nivel profesional y siguiendo un proceso iterativo basado en Scrum. A continuación se detallan los principales logros y aprendizajes.

---

#### 6.1.1 Logros técnicos

**Arquitectura full-stack integrada**

Se diseñó e implementó una arquitectura de tres capas (presentación → lógica de negocio → persistencia) tanto en el backend como en la aplicación móvil:

- **Backend:** Spring Boot 3.3.6 con Java 21, API REST protegida con Spring Security 6 y JWT sin estado. Cada endpoint está protegido por rol (`@PreAuthorize`) y las entidades nunca se exponen directamente — toda comunicación usa DTOs validados con Jakarta Bean Validation.
- **Base de datos:** PostgreSQL 16 gestionado por Hibernate ORM mediante `ddl-auto: update`, que permite evolucionar el esquema de forma incremental sin migraciones manuales.
- **App Android:** Java nativo (Android SDK), arquitectura de actividades + servicios Retrofit, gestión de sesión con `SharedPreferences` encriptadas, carga asíncrona de imágenes con Glide y gráficas con MPAndroidChart.

**Trece historias de usuario implementadas**

| Área | Historias completadas |
|---|---|
| Autenticación y seguridad | HU-01, HU-02 |
| Agenda y asistencia | HU-03, HU-04, HU-05 |
| Rendimiento deportivo | HU-06, HU-07, HU-08 |
| Competencias | HU-09, HU-10 |
| Notificaciones y perfil | HU-11, HU-12, HU-13 |

El sistema atiende tres perfiles de usuario — entrenador, atleta y padre/tutor — con pantallas y permisos diferenciados por rol.

**Notificaciones push en tiempo real**

La integración con Firebase Cloud Messaging (FCM) permite enviar notificaciones instantáneas al registrar una sesión, publicar una competencia, anotar un resultado o cuando el sistema actualiza automáticamente la categoría de un atleta (scheduler diario a la 1:00 am). El atleta puede configurar qué tipos de notificaciones desea recibir desde su perfil.

**Despliegue continuo en producción**

El backend se despliega automáticamente en Coolify (servidor VPS propio) mediante un pipeline CI/CD que detecta cada `push` a `master`, construye el contenedor Docker y ejecuta un healthcheck antes de enrutar el tráfico. La app Android genera un APK firmado de forma automática mediante GitHub Actions en cada versión etiquetada.

**Seguridad aplicada**

- Contraseñas almacenadas con BCrypt (factor de coste 10).
- Tokens JWT con expiración configurable (24 h o 30 días con "Recordarme").
- Bloqueo temporal de cuenta tras 5 intentos fallidos (15 minutos).
- Verificación de correo electrónico al registrarse (token UUID de un solo uso).
- Recuperación de contraseña por SMTP con token de 24 h.
- Datos de menores (tutor, parentesco, teléfono) restringidos a rol ENTRENADOR/ADMIN.

**Exportación y visualización**

El módulo de rendimiento ofrece gráficas de evolución individual (líneas de tendencia) y comparativa grupal multi-línea (MPAndroidChart), con exportación a PDF compartible via `FileProvider` e `Intent.ACTION_SEND`.

---

#### 6.1.2 Aprendizajes clave del desarrollo

**Gestión de esquema en producción con datos existentes**

El incidente de la columna `intentos_fallidos` ilustró un error común al usar `ddl-auto: update` de Hibernate: agregar un campo primitivo (`int`) — que Hibernate mapea como `NOT NULL` — a una tabla con filas existentes provoca que PostgreSQL rechace el `ALTER TABLE`. La solución fue combinar el tipo wrapper Java (`Integer`) con `@Column(columnDefinition = "integer not null default 0")`, de modo que PostgreSQL pueda asignar el valor por defecto a las filas ya existentes. Esta lección es extrapolable a cualquier proyecto que evolucione el esquema en producción sin herramientas de migración versionadas (Flyway/Liquibase).

**Diferencia entre error de DDL y error de consulta**

El mismo incidente dejó otra enseñanza: cuando `ddl-auto: update` falla al agregar una columna, Hibernate no revierte los cambios pendientes ni detiene la aplicación con un mensaje claro. La app puede arrancar (el contexto se inicializa), pero la primera consulta que incluya la columna faltante genera un `SQLGrammarException` — un error de runtime que puede confundirse con un bug de lógica. Monitorear los logs de startup completos, no solo el healthcheck HTTP, es esencial.

**CI/CD como red de seguridad**

Contar con un pipeline de GitHub Actions que compila la app Android en cada push permitió detectar rápidamente errores de compilación (tipo incorrecto en `LineData`, color inexistente, string duplicado) antes de distribuir el APK a los testers. Sin este paso automático, esos errores se habrían descubierto solo al construir manualmente.

**Arquitectura en capas como facilitador del cambio**

La separación estricta Controller → Service → Repository hizo que añadir nuevos endpoints (configuración de notificaciones, vínculo padre-hijo, resultados de competencia) fuera un proceso predecible: nuevo DTO de entrada, método en el servicio, call en el repositorio si era necesario, endpoint en el controlador. Ningún cambio en la lógica de negocio afectó la capa de persistencia, y viceversa.

---

#### 6.1.3 Cobertura final de requisitos

| Categoría | Total | Implementado ✅ | Parcial 🟡 | No impl. ❌ |
|---|---|---|---|---|
| Historias de Usuario | 13 | 10 | 3 | 0 |
| Requisitos Funcionales | 18 | 12 | 4 | 2 |
| Requisitos No Funcionales | 6 | 1 | 3 | 2 |
| Casos de Uso | 6 | 3 | 3 | 0 |

Los requisitos parciales (🟡) corresponden en su mayoría a restricciones de infraestructura (ausencia de HTTPS propio, sin modo offline) o a funcionalidades secundarias no priorizadas en el alcance del taller (validación de fechas pasadas en agenda, historial de notificaciones de 30 días exactos). Los dos requisitos funcionales no implementados (RF-04 CRUD atletas por entrenador en la primera versión; RF-15 resultados de competencia en la primera versión) fueron posteriormente completados en las iteraciones 9.18 y 9.19 del registro de cambios.

---

#### 6.1.4 Reflexión académica

El proyecto confirmó que el modelo Scrum — incluso adaptado a un equipo de una persona y con sprints de dos semanas — aporta valor real: las historias de usuario como unidad de trabajo permiten priorizar con criterio de usuario final, las retrospectivas informales al final de cada sprint visibilizaron cuellos de botella (principalmente en la integración FCM y en la configuración del despliegue en Coolify), y el backlog priorizado evitó dispersarse en funcionalidades de bajo impacto.

La decisión de implementar el frontend en Android nativo Java — en lugar de React Native como indicaba el diseño original — se tomó para reducir la fricción de configuración del entorno y aprovechar el conocimiento existente del equipo. Esta adaptación pragmática del plan es coherente con los principios ágiles: responder al cambio por encima de seguir un plan.

---

### 6.2 Trabajo Futuro

Las funcionalidades descritas a continuación representan la hoja de ruta natural de la aplicación para versiones posteriores, ordenadas por impacto estimado.

---

#### Prioridad Alta

**1. HTTPS / TLS en el backend (RNF-02)**
El backend actualmente opera sobre HTTP plano. La habilitación de HTTPS requiere un dominio propio (ej. `api.clubatletismo.bo`) y un certificado TLS gestionado por Let's Encrypt — ambos configurables desde Coolify sin cambios en el código. Esta mejora es prerequisito para publicar la app en Google Play, que desde 2024 exige tráfico cifrado para endpoints en producción.

**2. Modo offline con sincronización (RNF-04)**
Implementar una caché local con Room Database (SQLite) para que el atleta pueda consultar su agenda y marcas sin conexión. Al recuperar la red, los datos se sincronizan con el backend usando una cola de operaciones pendientes (`WorkManager`). El entrenador también se beneficia: podría registrar asistencia sin señal y sincronizar al salir del recinto.

**3. Publicación en Google Play Store (RNF-06)**
El APK ya se genera automáticamente en GitHub Actions con firma. El paso siguiente es crear una cuenta de desarrollador en Google Play Console, subir el AAB firmado, completar el formulario de contenido (clasificación de edad, política de privacidad) y pasar la revisión inicial. El tiempo estimado es de 3 a 7 días hábiles.

**4. Pruebas automatizadas (RNF-05)**
El proyecto no cuenta con pruebas unitarias ni de integración. Como trabajo futuro se propone:
- Backend: pruebas unitarias de servicios con JUnit 5 + Mockito (meta: 70% de cobertura en `AuthService`, `MarcaService`, `CompetenciaService`); pruebas de integración con `@SpringBootTest` y base de datos H2 en memoria.
- App Android: pruebas de UI con Espresso para los flujos críticos (login, registro de marca, asistencia).

---

#### Prioridad Media

**5. Reintentos automáticos para notificaciones push (HU-11)**
Implementar lógica de reintento en `FcmService`: si el token FCM del usuario está caducado o el envío falla, reintentar hasta 3 veces con backoff exponencial (1s, 2s, 4s). Los tokens caducados deben eliminarse de la base de datos para evitar intentos innecesarios.

**6. Historial de notificaciones con límite de 30 días (HU-11 / RF-18)**
Agregar un job programado (similar a `CategoriaSchedulerService`) que elimine notificaciones con más de 30 días de antigüedad. Esto controla el crecimiento de la tabla `notificacion` y mejora el rendimiento de las consultas de historial.

**7. Validación de fechas pasadas al crear sesiones (HU-04)**
En `SesionService.crear()`, verificar que `horaInicio` sea posterior a `LocalDateTime.now()`. En la app, deshabilitar fechas anteriores en el DateTimePicker. Actualmente solo se valida el conflicto de horario entre sesiones del mismo grupo, pero no se impide crear sesiones en el pasado.

**8. Exportación a Excel (RNF-06)**
Complementar la exportación a PDF con un formato Excel (`.xlsx`) para marcas y asistencias, usando la librería Apache POI en el backend. El entrenador podría descargar un reporte completo del grupo con un endpoint `GET /api/v1/marcas/grupo/{id}/export?formato=xlsx`.

---

#### Prioridad Baja / Mejoras de Experiencia

**9. Confirmación de asistencia antes de la sesión**
Permitir que los atletas confirmen o declinen su asistencia a una sesión con anticipación (similar a la confirmación de competencias), de modo que el entrenador pueda anticipar cuántos asistirán.

**10. Disciplinas y categorías configurables desde el backend**
Actualmente las listas de disciplinas están hardcodeadas en la app (`String[]`). Moverlas a una tabla `disciplina` en la BD y exponerlas via `GET /api/v1/disciplinas` permite al entrenador administrarlas sin actualizar la app.

**11. Versión iOS**
Reescribir la app en React Native o Flutter para distribuirla también en el App Store de Apple, aprovechando la API REST existente sin cambios en el backend.

**12. Panel web para el entrenador**
Desarrollar un frontend web (React o Vue.js) para que el entrenador acceda desde un navegador de escritorio a los módulos de mayor volumen de datos: gestión de atletas, exportación de informes, estadísticas del club.

---

### Referencias Bibliográficas

Beck, K., Beedle, M., van Bennekum, A., Cockburn, A., Cunningham, W., Fowler, M., Grenning, J., Highsmith, J., Hunt, A., Jeffries, R., Kern, J., Marick, B., Martin, R. C., Mellor, S., Schwaber, K., Sutherland, J., & Thomas, D. (2001). *Manifiesto por el Desarrollo Ágil de Software*. https://agilemanifesto.org/iso/es/manifesto.html

Evans, E. (2003). *Domain-driven design: Tackling complexity in the heart of software*. Addison-Wesley Professional.

Fielding, R. T. (2000). *Architectural styles and the design of network-based software architectures* [Tesis doctoral, University of California]. https://roy.gbiv.com/pubs/dissertation/top.htm

Firebase. (2024). *Firebase Cloud Messaging documentation*. Google LLC. https://firebase.google.com/docs/cloud-messaging

Fowler, M. (2002). *Patterns of enterprise application architecture*. Addison-Wesley Professional.

Google. (2024). *Android developer documentation*. https://developer.android.com/docs

Google. (2024). *Material Design 3 guidelines*. https://m3.material.io/

Hibernate. (2024). *Hibernate ORM 6 user guide*. Red Hat. https://docs.jboss.org/hibernate/orm/6.4/userguide/html_single/Hibernate_User_Guide.html

Horstmann, C. S. (2019). *Core Java, Volume I: Fundamentals* (11.ª ed.). Pearson Education.

Martin, R. C. (2008). *Clean code: A handbook of agile software craftsmanship*. Prentice Hall.

OWASP Foundation. (2021). *OWASP Top Ten 2021*. https://owasp.org/Top10/

PostgreSQL Global Development Group. (2024). *PostgreSQL 16 documentation*. https://www.postgresql.org/docs/16/

Pressman, R. S., & Maxim, B. R. (2021). *Ingeniería del software: Un enfoque práctico* (9.ª ed.). McGraw-Hill Education.

Schwaber, K., & Sutherland, J. (2020). *The Scrum Guide: The definitive guide to Scrum — The rules of the game*. https://scrumguides.org/scrum-guide.html

Spring. (2024). *Spring Boot reference documentation (v3.3)*. VMware. https://docs.spring.io/spring-boot/docs/3.3.x/reference/html/

Spring. (2024). *Spring Security reference documentation (v6)*. VMware. https://docs.spring.io/spring-security/reference/

Square, Inc. (2024). *Retrofit 2 documentation*. https://square.github.io/retrofit/

---

## Anexo B — Fragmentos de Código Fuente Representativos

### B.1 Entidad `Usuario` — mapeo JPA con roles y seguridad

```java
@Entity @Table(name = "usuario")
@Getter @Setter @NoArgsConstructor @AllArgsConstructor @Builder
public class Usuario implements UserDetails {

    @Id @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(nullable = false, unique = true)
    private String correo;

    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    private Rol rol;  // ENTRENADOR | ATLETA | PADRE

    // Columna agregada a tabla existente: DEFAULT 0 evita error NOT NULL en PostgreSQL
    @Builder.Default
    @Column(columnDefinition = "integer not null default 0")
    private Integer intentosFallidos = 0;

    private LocalDateTime bloqueadoHasta;
    private Boolean emailVerificado;   // null = cuenta legacy sin restricción
    private Boolean notifSesiones;     // null = recibe por defecto
    private Boolean notifCompetencias;
    private Boolean notifResultados;

    @Override
    public Collection<? extends GrantedAuthority> getAuthorities() {
        return List.of(new SimpleGrantedAuthority("ROLE_" + rol.name()));
    }
}
```

### B.2 Servicio de autenticación — bloqueo por intentos fallidos (HU-02)

```java
if (!passwordEncoder.matches(request.getContrasena(), usuario.getContrasenaHash())) {
    int fallos = (usuario.getIntentosFallidos() != null
            ? usuario.getIntentosFallidos() : 0) + 1;
    usuario.setIntentosFallidos(fallos);
    if (fallos >= MAX_INTENTOS) {                   // MAX_INTENTOS = 5
        usuario.setBloqueadoHasta(
                LocalDateTime.now().plusMinutes(BLOQUEO_MIN));   // 15 min
        usuario.setIntentosFallidos(0);
    }
    usuarioRepository.save(usuario);
    throw new BadCredentialsException("Credenciales incorrectas");
}
```

### B.3 Verificación de preferencias antes de enviar push FCM (HU-11)

```java
private boolean debeRecibirPush(Usuario u, String tipo) {
    return switch (tipo) {
        case "SESION"      -> !Boolean.FALSE.equals(u.getNotifSesiones());
        case "COMPETENCIA" -> !Boolean.FALSE.equals(u.getNotifCompetencias());
        case "RESULTADO"   -> !Boolean.FALSE.equals(u.getNotifResultados());
        default            -> true;   // CATEGORIA y otros siempre se envían
    };
}
```

### B.4 Gráfica multi-línea de evolución grupal — Android (HU-08)

```java
List<ILineDataSet> dataSets = new ArrayList<>();

for (int i = 0; i < datos.size(); i++) {
    GrupoEvolucionDto atleta = datos.get(i);
    int color = COLORES[i % COLORES.length];   // paleta de 8 colores

    List<Entry> entries = new ArrayList<>();
    for (MarcaPersonal m : atleta.getMarcas()) {
        int xIdx = todasFechas.indexOf(m.getFecha());
        float valor = Float.parseFloat(m.getResultado().replace(",", "."));
        entries.add(new Entry(xIdx, valor));
    }

    LineDataSet ds = new LineDataSet(entries, atleta.getAtletaNombre());
    ds.setColor(color);
    ds.setLineWidth(2f);
    ds.setCircleRadius(4f);
    ds.setDrawValues(false);
    dataSets.add(ds);   // List<ILineDataSet>, no List<LineDataSet>
}
lineChart.setData(new LineData(dataSets));
lineChart.animateX(500);
```

### B.5 Pipeline CI/CD — GitHub Actions (fragmento)

```yaml
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-java@v4
        with: { java-version: '17', distribution: 'temurin' }
      - name: Build release APK
        run: ./gradlew assembleRelease
      - name: Sign APK
        uses: r0adkll/sign-android-release@v1
        with:
          releaseDirectory: app/build/outputs/apk/release
          signingKeyBase64: ${{ secrets.SIGNING_KEY }}
          alias: ${{ secrets.KEY_ALIAS }}
      - name: Upload APK to Release
        uses: softprops/action-gh-release@v2
        with:
          files: app/build/outputs/apk/release/app-release-signed.apk
```

---

## Anexo E — Glosario de Términos

**API REST** (*Representational State Transfer Application Programming Interface*): interfaz de comunicación entre sistemas basada en el protocolo HTTP. Define recursos accesibles mediante URLs y operaciones estándar (GET, POST, PUT, DELETE). En este proyecto, el backend expone una API REST que la app Android consume.

**APK** (*Android Package Kit*): formato de archivo usado para distribuir e instalar aplicaciones en dispositivos Android. Equivalente al `.exe` en Windows.

**BCrypt**: función de hash criptográfica diseñada para contraseñas. Incorpora un factor de coste ajustable y un valor de sal aleatorio, lo que la hace resistente a ataques de fuerza bruta y tablas arco iris.

**CI/CD** (*Continuous Integration / Continuous Delivery*): práctica de automatizar la compilación, prueba y despliegue del software en cada cambio de código. En este proyecto se implementa con GitHub Actions (compilación del APK) y Coolify (despliegue del backend).

**Coolify**: plataforma open source de despliegue y orquestación de contenedores (similar a Heroku) autoalojada en un servidor VPS. Permite desplegar aplicaciones Docker con HTTPS, variables de entorno y rollback automático.

**DTO** (*Data Transfer Object*): objeto Java sin lógica de negocio cuyo único propósito es transportar datos entre capas (controller ↔ service, o servicio ↔ app Android). Evita exponer directamente las entidades JPA en los endpoints.

**FCM** (*Firebase Cloud Messaging*): servicio de Google para el envío de notificaciones push a dispositivos Android e iOS. El backend llama a la API de FCM con el token del dispositivo; FCM se encarga de la entrega.

**Hibernate ORM**: implementación de referencia de la especificación JPA (Jakarta Persistence API). Mapea automáticamente clases Java (entidades) a tablas de base de datos y genera el SQL necesario para las operaciones CRUD.

**JWT** (*JSON Web Token*): estándar para transmitir información de autenticación de forma compacta y firmada digitalmente. En este proyecto el backend emite un JWT al hacer login; la app lo incluye en el header `Authorization: Bearer <token>` en cada petición posterior.

**JPA** (*Jakarta Persistence API*, antes Java Persistence API): especificación Java estándar para mapear objetos a bases de datos relacionales. Spring Data JPA y Hibernate son, respectivamente, la capa de abstracción y la implementación usadas en este proyecto.

**MPAndroidChart**: librería Android open source para visualización de datos. Usada en este proyecto para las gráficas de evolución de marcas (líneas) y el comparativo grupal multi-línea.

**PostgreSQL**: sistema de gestión de bases de datos relacional open source, reconocido por su cumplimiento del estándar SQL y su robustez en entornos de producción. Versión 16 usada en este proyecto.

**Retrofit 2**: cliente HTTP para Android que convierte interfaces Java anotadas en llamadas HTTP. Simplifica la comunicación con la API REST del backend y la deserialización automática de JSON a objetos Java mediante Gson o Moshi.

**Scrum**: marco de trabajo ágil para el desarrollo iterativo de software. Organiza el trabajo en sprints de duración fija, con roles definidos (Product Owner, Scrum Master, equipo de desarrollo) y eventos periódicos (planificación, revisión, retrospectiva).

**Spring Boot**: extensión de Spring Framework que elimina la configuración boilerplate mediante autoconfiguración. Incluye un servidor Tomcat embebido, de modo que el backend se ejecuta como un jar ejecutable sin necesidad de un servidor de aplicaciones externo.

**Spring Security**: módulo de Spring para autenticación y autorización. En este proyecto gestiona el filtro JWT (verifica el token en cada petición), la carga del usuario desde la BD y el control de acceso por roles (`@PreAuthorize`).

**VPS** (*Virtual Private Server*): servidor virtual alojado en infraestructura cloud que otorga acceso root completo al sistema operativo. En este proyecto se usa un VPS para alojar Coolify, PostgreSQL y el contenedor Docker del backend.

---

*Documento generado con asistencia de Claude (Anthropic) — Proyecto académico UPDS · 2026*
