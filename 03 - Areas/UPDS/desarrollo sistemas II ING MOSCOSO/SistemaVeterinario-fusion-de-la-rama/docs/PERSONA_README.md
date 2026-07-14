# Documentación de las Clases de Persona

## Descripción General

Se han creado las clases `DPersona` (Capa de Datos) y `NPersona` (Capa de Negocio) para manejar tanto personas físicas como jurídicas (clientes) en el sistema veterinario.

## Arquitectura

### DPersona (Capa de Datos)
- Maneja la conexión directa con la base de datos
- Utiliza procedimientos almacenados para las operaciones CRUD
- Maneja la herencia entre personas físicas y jurídicas

### NPersona (Capa de Negocio)
- Contiene la lógica de negocio
- Realiza validaciones antes de enviar datos a la capa de datos
- Proporciona métodos estáticos para facilitar el uso
- Incluye validaciones de formato y unicidad

## Estructura de la Base de Datos

### Tablas Principales:
- `persona`: Tabla base con datos comunes
- `persona_fisica`: Datos específicos de personas físicas
- `persona_juridica`: Datos específicos de personas jurídicas

### Procedimientos Almacenados:
- `SP01_CreateOrUpdatePFisica`: Crear/actualizar persona física
- `SP02_CreateOrUpdatePJuridica`: Crear/actualizar persona jurídica
- `SP_EliminarPersona`: Eliminación lógica de personas
- `SP_MostrarPersonas`: Listar todas las personas activas
- `SP_BuscarPersonaPorTexto`: Búsqueda por texto
- `SP_BuscarPersonaPorTipo`: Búsqueda por tipo de persona
- `SP_ValidarCIUnico`: Validar unicidad de CI
- `SP_ValidarNITUnico`: Validar unicidad de NIT
- `SP_ValidarEmailUnico`: Validar unicidad de email

## Uso de las Clases

### 1. Insertar Persona Física

```csharp
using CapaNegocio;

string resultado = NPersona.InsertarPersonaFisica(
    ci: "12345678",
    nombre: "Juan",
    apellido: "Pérez",
    email: "juan.perez@email.com",
    direccion: "Calle 123",
    telefono: "70123456",
    fechaNacimiento: new DateTime(1990, 1, 1),
    genero: 'M'
);

if (resultado == "OK")
{
    // Inserción exitosa
}
else
{
    // Mostrar mensaje de error: resultado
}
```

### 2. Insertar Persona Jurídica

```csharp
string resultado = NPersona.InsertarPersonaJuridica(
    razonSocial: "Empresa ABC S.R.L.",
    nit: "1234567890",
    encargadoNombre: "María López",
    encargadoCargo: "Gerente General",
    email: "contacto@empresaabc.com",
    direccion: "Av. Principal 456",
    telefono: "22334455"
);

if (resultado == "OK")
{
    // Inserción exitosa
}
```

### 3. Editar Persona Física

```csharp
string resultado = NPersona.EditarPersonaFisica(
    id: 1,
    ci: "12345678",
    nombre: "Juan Carlos",
    apellido: "Pérez García",
    email: "juancarlos.perez@email.com",
    // ... otros campos
);
```

### 4. Editar Persona Jurídica

```csharp
string resultado = NPersona.EditarPersonaJuridica(
    id: 2,
    razonSocial: "Empresa ABC S.R.L. Actualizada",
    nit: "1234567890",
    // ... otros campos
);
```

### 5. Eliminar Persona

```csharp
string resultado = NPersona.Eliminar(1);
// Eliminación lógica (activo = false)
```

### 6. Cambiar Tipo de Persona

```csharp
// Cambiar de persona física a jurídica
string resultado = NPersona.CambiarTipoPersona(
    id: 1,
    nuevoTipo: "Jurídica",
    razonSocial: "Nueva Empresa S.R.L.",
    nit: "9876543210",
    // ... campos de persona jurídica
);
```

### 7. Consultar Personas

```csharp
// Mostrar todas las personas
DataTable todasPersonas = NPersona.Mostrar();

// Buscar por texto
DataTable resultados = NPersona.BuscarTexto("Juan");

// Buscar por tipo
DataTable personasFisicas = NPersona.BuscarPorTipo("Física");
DataTable personasJuridicas = NPersona.BuscarPorTipo("Jurídica");

// Obtener persona por ID
DataTable persona = NPersona.ObtenerPorId(1);
```

## Validaciones Implementadas

### Para Personas Físicas:
- **Nombre y Apellido**: Obligatorios, solo letras y caracteres especiales permitidos
- **CI**: Opcional, formato numérico 7-15 dígitos, único en el sistema
- **Email**: Opcional, formato válido, único en el sistema
- **Teléfono**: Opcional, formato numérico 7-15 dígitos
- **Género**: Opcional, solo 'M' o 'F'
- **Fecha de Nacimiento**: Opcional, no mayor a fecha actual ni menor a 150 años atrás

### Para Personas Jurídicas:
- **Razón Social**: Obligatoria, longitud 2-255 caracteres
- **NIT**: Opcional, formato numérico 8-15 dígitos, único en el sistema
- **Email**: Opcional, formato válido, único en el sistema
- **Teléfono**: Opcional, formato numérico 7-15 dígitos
- **Encargado**: Opcional, nombre y cargo del responsable

## Métodos de Validación Disponibles

### Validaciones de Formato:
```csharp
bool esValidoCi = NPersona.ValidarCi("12345678");
bool esValidoEmail = NPersona.ValidarEmail("test@email.com");
bool esValidoTelefono = NPersona.ValidarTelefono("70123456");
bool esValidoNit = NPersona.ValidarNit("1234567890");
bool esValidoNombre = NPersona.ValidarNombre("Juan");
bool esValidaRazonSocial = NPersona.ValidarRazonSocial("Empresa ABC");
bool esValidoGenero = NPersona.ValidarGenero('M');
bool esValidaFecha = NPersona.ValidarFechaNacimiento(DateTime.Now.AddYears(-25));
```

### Validaciones de Unicidad:
```csharp
bool existeCi = NPersona.ExistePersonaPorCi("12345678");
bool existeNit = NPersona.ExistePersonaPorNit("1234567890");
bool existeEmail = NPersona.ExistePersonaPorEmail("test@email.com");

// Para validar durante edición (excluir el ID actual):
bool existeCi = NPersona.ExistePersonaPorCi("12345678", idPersonaActual: 1);
```

## Métodos Auxiliares

### Formateo de Datos:
```csharp
// Obtener nombre completo de una persona
DataRow persona = // ... obtener fila de persona
string nombreCompleto = NPersona.ObtenerNombreCompleto(persona);

// Formatear persona para mostrar en UI
string personaFormateada = NPersona.FormatearPersonaParaMostrar(persona);
// Resultado: "Juan Pérez (CI: 12345678)" o "Empresa ABC (NIT: 1234567890)"
```

## Manejo de Errores

Todos los métodos de inserción, edición y eliminación retornan:
- `"OK"` si la operación fue exitosa
- Un mensaje descriptivo del error si hubo algún problema

Los métodos de consulta retornan:
- `DataTable` con los resultados
- `DataTable` vacío si no hay resultados o hay error

## Consideraciones Importantes

1. **Transacciones**: Los procedimientos almacenados manejan transacciones automáticamente
2. **Eliminación**: Es lógica (activo = false), no física
3. **Unicidad**: CI, NIT y Email deben ser únicos en personas activas
4. **Validaciones**: Se realizan tanto en la capa de negocio como en la base de datos
5. **Nullable Types**: Los campos opcionales manejan correctamente valores null
6. **Herencia**: Una persona solo puede ser física O jurídica, nunca ambas

## Ejemplos de Uso en Windows Forms

### Cargar ComboBox con Personas:
```csharp
DataTable personas = NPersona.Mostrar();
comboBoxPersonas.DataSource = personas;
comboBoxPersonas.DisplayMember = "nombre_completo"; // Campo calculado
comboBoxPersonas.ValueMember = "id";
```

### Validar Formulario antes de Guardar:
```csharp
private bool ValidarFormulario()
{
    if (string.IsNullOrEmpty(txtNombre.Text))
    {
        MessageBox.Show("El nombre es obligatorio");
        return false;
    }
    
    if (!NPersona.ValidarEmail(txtEmail.Text))
    {
        MessageBox.Show("Email no válido");
        return false;
    }
    
    if (NPersona.ExistePersonaPorCi(txtCi.Text))
    {
        MessageBox.Show("Ya existe una persona con esta CI");
        return false;
    }
    
    return true;
}
```

Esta documentación proporciona una guía completa para utilizar las clases de persona en el sistema veterinario.
