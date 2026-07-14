# Sistema Veterinario - Documentación Completa

## Descripción General

El **Sistema Veterinario** es una aplicación de escritorio desarrollada en C# con Windows Forms que implementa un sistema de gestión básico para una veterinaria. La aplicación utiliza una arquitectura de 3 capas (Presentación, Negocio y Datos) y se conecta a una base de datos SQL Server LocalDB.

## Tecnologías Utilizadas

- **.NET Framework 4.8**
- **C# Windows Forms** - Interfaz de usuario
- **SQL Server LocalDB** - Base de datos
- **FontAwesome.Sharp 6.6.0** - Iconografía
- **Visual Studio 2022** - IDE de desarrollo

## Arquitectura del Sistema

### Estructura de Capas

```
VeterinariaApp/
├── Presentación (UI)
│   ├── FrmPrincipal.cs       # Formulario principal
│   ├── FrmUsers.cs           # Gestión de usuarios
│   └── FormPersona.cs        # Gestión de personas/clientes
├── CapaNegocio/              # Lógica de negocio
│   ├── Nusers.cs            # Negocio de usuarios
│   └── NPersona.cs          # Negocio de personas
└── CapaDatos/               # Acceso a datos
    ├── DbConnection.cs      # Conexión a base de datos
    ├── Dusers.cs           # Datos de usuarios
    └── DPersona.cs         # Datos de personas
```

## Funcionalidades Principales

### 1. Formulario Principal (FrmPrincipal.cs)

**Ubicación:** `FrmPrincipal.cs:15`

El formulario principal actúa como contenedor MDI y centro de navegación:

- **Menú lateral dinámico** con botones de navegación
- **Sistema de colores** personalizados para cada módulo
- **Gestión de formularios hijos** - solo un formulario activo a la vez
- **Controles de ventana** personalizados (minimizar, maximizar, cerrar)
- **Arrastre de ventana** mediante API de Windows

#### Características Técnicas:
- Uso de FontAwesome.Sharp para iconografía
- Sistema de colores RGB personalizado definido en `FrmPrincipal.cs:43-52`
- Implementación de controles de ventana nativos con `ReleaseCapture()` y `SendMessage()`

### 2. Gestión de Usuarios (FrmUsers.cs)

**Ubicación:** `FrmUsers.cs:14`

Módulo completo CRUD para administración de usuarios:

#### Funcionalidades:
- **Insertar usuarios:** `FrmUsers.cs:117-158`
- **Editar usuarios:** `FrmUsers.cs:167-180`
- **Buscar usuarios:** `FrmUsers.cs:81-86`
- **Eliminar usuarios:** `FrmUsers.cs:203-231` (comentado por seguridad)
- **Validación de datos** con ErrorProvider

#### Campos del Usuario:
- ID Usuario (auto-generado)
- Nombre de usuario
- Contraseña
- Email

### 3. Gestión de Personas/Clientes (FormPersona.cs)

**Ubicación:** `FormPersona.cs:9`

Sistema de gestión de clientes de la veterinaria:

#### Funcionalidades:
- **CRUD completo** para personas
- **Búsqueda por email:** `FormPersona.cs:35-39`
- **Validación de campos obligatorios**
- **Interfaz tabbed** para navegación

#### Campos de Persona:
- ID Persona (auto-generado)
- Email (obligatorio)
- Dirección
- Teléfono (obligatorio)

## Capa de Datos

### Conexión a Base de Datos (DbConnection.cs)

**Ubicación:** `CapaDatos\DbConnection.cs:10`

Clase abstracta que maneja la conexión a SQL Server LocalDB:

```csharp
public static string cn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SistemaVeterinario;Integrated Security=True";
```

### Entidades de Datos

#### 1. DPersona.cs
**Ubicación:** `CapaDatos\DPersona.cs:7`

Maneja todas las operaciones de base de datos para personas:
- **Insertar:** Stored procedure `spInsertar_Persona`
- **Editar:** Stored procedure `spEditar_Persona`
- **Eliminar:** Stored procedure `spEliminar_Persona`
- **Mostrar:** Stored procedure `spMostrar_Personas`
- **Buscar por email:** Stored procedure `spBuscar_Persona_Email`

#### 2. Dusers.cs
**Ubicación:** `CapaDatos\Dusers.cs:11`

Gestiona las operaciones de usuarios:
- **Insertar:** `spinsertar_users`
- **Editar:** `speditar_users`
- **Eliminar:** `speliminar_users`
- **Mostrar:** `spmostrar_users`
- **Buscar por nombre:** `spbuscar_user_name`
- **Login:** `splogin`

## Capa de Negocio

### NPersona.cs
**Ubicación:** `CapaNegocio\NPersona.cs:7`

Métodos estáticos que actúan como interfaz entre la UI y la capa de datos:
- Validación de reglas de negocio
- Transformación de datos
- Control de flujo de operaciones

### Nusers.cs
**Ubicación:** `CapaNegocio\Nusers.cs:11`

Lógica de negocio para usuarios con métodos como:
- `Insertar()`: `Nusers.cs:14`
- `Editar()`: `Nusers.cs:22` 
- `Login()`: `Nusers.cs:48`

**Nota:** Existe un bug en el método `Editar()` línea 30 que llama a `Insertar()` en lugar de `Editar()`.

## Base de Datos

### Stored Procedures Requeridos

La aplicación requiere los siguientes stored procedures en la base de datos:

#### Para Personas:
- `spInsertar_Persona`
- `spEditar_Persona` 
- `spEliminar_Persona`
- `spMostrar_Personas`
- `spBuscar_Persona_Email`

#### Para Usuarios:
- `spinsertar_users`
- `speditar_users`
- `speliminar_users`
- `spmostrar_users`
- `spbuscar_user_name`
- `splogin`

## Configuración y Dependencias

### Packages NuGet
- **FontAwesome.Sharp v6.6.0** - Para iconografía del menú

### Configuración de Base de Datos
- **Servidor:** SQL Server LocalDB
- **Base de datos:** SistemaVeterinario
- **Autenticación:** Windows Integrated Security

## Estructura de Archivos del Proyecto

```
SistemaVeterinario/
├── VeterinariaApp.csproj      # Proyecto principal
├── Veterinaria.sln           # Solución de Visual Studio
├── Program.cs                # Punto de entrada
├── App.config               # Configuración de aplicación
├── packages.config          # Dependencias NuGet
├── CapaDatos/              # Proyecto de acceso a datos
│   ├── CapaDatos.csproj
│   ├── DbConnection.cs
│   ├── DPersona.cs
│   └── Dusers.cs
├── CapaNegocio/           # Proyecto de lógica de negocio
│   ├── CapaNegocio.csproj
│   ├── NPersona.cs
│   └── Nusers.cs
└── Resources/             # Recursos gráficos
    ├── imagen-vacia.jpg
    ├── imgbin-user.jpg
    └── png-transparent-online-store-browser-illustration.png
```

## Instalación y Configuración

### Requisitos Previos
1. Visual Studio 2019 o superior
2. .NET Framework 4.8
3. SQL Server LocalDB

### Pasos de Instalación
1. Clonar o descargar el proyecto
2. Abrir `Veterinaria.sln` en Visual Studio
3. Restaurar paquetes NuGet
4. Crear la base de datos `SistemaVeterinario` en LocalDB
5. Ejecutar los scripts de stored procedures
6. Compilar y ejecutar el proyecto

## Punto de Entrada

**Archivo:** `Program.cs:15`

La aplicación inicia en el método `Main()` que:
1. Habilita estilos visuales
2. Configura renderizado de texto
3. Ejecuta el formulario principal `FrmPrincipal`

## Características de la Interfaz de Usuario

### Diseño Moderno
- **Formulario sin bordes** con controles personalizados
- **Menú lateral** con iconos FontAwesome
- **Sistema de colores** temático por módulo
- **Formularios hijos** integrados en panel principal

### Experiencia de Usuario
- **Búsqueda en tiempo real** en los listados
- **Validación visual** con ErrorProvider
- **Mensajes informativos** para operaciones CRUD
- **Navegación intuitiva** mediante tabs y menús

## Seguridad

### Medidas Implementadas
- **Parámetros SQL** para prevenir inyección SQL
- **Validación de entrada** en formularios
- **Gestión de errores** con try-catch
- **Función de login** disponible para autenticación

### Consideraciones de Seguridad
- Las contraseñas se almacenan en texto plano (recomendación: implementar hash)
- No hay control de sesiones implementado
- Función de eliminación deshabilitada por seguridad

## Limitaciones Conocidas

1. **Bug en edición de usuarios:** `Nusers.cs:30` llama a `Insertar()` en lugar de `Editar()`
2. **Eliminación deshabilitada** en usuarios por seguridad
3. **Contraseñas en texto plano**
4. **Sin sistema de roles o permisos**
5. **Formularios con eventos vacíos** en FormPersona.cs

## Posibles Mejoras

1. **Sistema de autenticación** completo con hash de contraseñas
2. **Control de roles y permisos** de usuario
3. **Logging de actividades** del sistema
4. **Validaciones más robustas** de entrada de datos
5. **Implementación de patrones** como Repository y Unit of Work
6. **Manejo centralizado de excepciones**
7. **Configuración externa** de cadena de conexión

---

**Desarrollado para:** Sistema de Gestión Veterinaria  
**Versión:** 1.0  
**Framework:** .NET Framework 4.8  
**Tipo:** Aplicación de Escritorio Windows Forms