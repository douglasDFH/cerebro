# Generador de Reportes de Cierre de Mes - DISMAC

Aplicación con interfaz gráfica para generar reportes de cierre de mes en formato PowerPoint.

## Características

- Interfaz gráfica intuitiva para ingresar datos
- Generación automática de gráficos profesionales
- Exportación directa a PowerPoint (.pptx)
- Plantilla personalizada con colores corporativos DISMAC
- Datos precargados de ejemplo (modificables)

## Requisitos Previos

- Python 3.7 o superior
- pip (gestor de paquetes de Python)

## Instalación

### Paso 1: Instalar Python

Si no tiene Python instalado:
1. Descargue Python desde https://www.python.org/downloads/
2. Durante la instalación, marque la opción "Add Python to PATH"
3. Complete la instalación

### Paso 2: Instalar Dependencias

Abra una terminal o símbolo del sistema en la carpeta `generador_reportes` y ejecute:

```bash
pip install -r requirements.txt
```

Esto instalará:
- python-pptx: Para crear presentaciones PowerPoint
- matplotlib: Para generar gráficos
- Pillow: Para manejo de imágenes

## Uso

### Iniciar la Aplicación

Hay dos formas de ejecutar la aplicación:

**Opción 1: Desde la terminal**
```bash
python main.py
```

**Opción 2: Doble clic**
- En Windows: Doble clic en `main.py`
- Si no funciona, haga clic derecho > Abrir con > Python

### Usar la Interfaz Gráfica

La aplicación tiene 4 pestañas:

#### 1. Datos Generales
Ingrese la información básica del mes:
- Mes y año
- Nombre de la tienda
- Objetivos en dólares y bolivianos
- Ventas alcanzadas
- Ticket promedio
- Porcentajes de ventas por tipo (contado/minicuotas)

#### 2. Categorías
Gestione las categorías de productos:
- **Agregar Categoría**: Añade una nueva categoría con objetivo, ventas y alcance
- **Eliminar Seleccionada**: Elimina la categoría seleccionada
- Datos precargados con 5 categorías de ejemplo

#### 3. Top 10 SKUs
Administre los productos más vendidos:
- **Agregar SKU**: Añade un nuevo SKU con su cantidad vendida
- **Eliminar Seleccionado**: Elimina el SKU seleccionado
- Datos precargados con 10 SKUs de ejemplo

#### 4. Mystery Shopper
Ingrese los resultados de Mystery Shopper:
- Índice general (%)
- Atención del vendedor (%)
- Hasta 5 puntos a mejorar con sus porcentajes

### Generar el Reporte

1. Complete o modifique los datos en las 4 pestañas
2. Haga clic en el botón **"GENERAR REPORTE"** (parte inferior)
3. Elija la ubicación y nombre del archivo .pptx
4. Espere mientras se genera el reporte
5. Recibirá un mensaje de confirmación con la ubicación del archivo

## Estructura del Reporte Generado

El PowerPoint generado incluye:

1. **Portada**: Título con mes, año y nombre de tienda
2. **Objetivos y Alcance**: Comparación de objetivos vs ventas
3. **Ticket Promedio**: Gráfico de dona con distribución de ventas
4. **Categorías Más Vendidas**: Gráfico comparativo con alcance
5. **Categorías Menos Vendidas**: Ordenadas por porcentaje de alcance
6. **Top 10 SKUs**: Gráfico de barras con productos más vendidos
7. **Mystery Shopper**: Resultados y puntos a mejorar
8. **Agradecimiento**: Página final

## Personalización

### Modificar Colores Corporativos

Edite el archivo `exportar_pptx.py`:

```python
COLOR_FONDO = RGBColor(44, 95, 93)  # Color de fondo principal
COLOR_ACENTO = RGBColor(227, 30, 36)  # Color de acento (rojo)
```

### Agregar Logo

Para incluir el logo de la empresa:
1. Guarde el logo como `logo.png` en la carpeta `assets/`
2. El programa lo detectará automáticamente

### Modificar Datos Predeterminados

Los datos de ejemplo se encuentran en `interfaz_gui.py`:
- Busque `categorias_default`
- Busque `skus_default`
- Modifique según sus necesidades

## Estructura de Archivos

```
generador_reportes/
│
├── main.py                  # Archivo principal ejecutable
├── interfaz_gui.py          # Interfaz gráfica
├── generador_principal.py   # Lógica de generación
├── graficos.py              # Generación de gráficos
├── exportar_pptx.py         # Exportación a PowerPoint
├── requirements.txt         # Dependencias
├── README.md               # Este archivo
│
├── assets/                 # Carpeta para gráficos temporales
│   └── (gráficos generados automáticamente)
│
└── output/                 # Carpeta sugerida para reportes
    └── (archivos .pptx generados)
```

## Solución de Problemas

### Error: "No module named 'pptx'"
Solución: Instale las dependencias
```bash
pip install -r requirements.txt
```

### Error: "tkinter no encontrado"
Solución: En Linux, instale tkinter:
```bash
sudo apt-get install python3-tk
```

### La aplicación no inicia
Solución: Verifique la versión de Python
```bash
python --version
```
Debe ser 3.7 o superior.

### Los gráficos no se generan
Solución: Verifique que la carpeta `assets/` exista o será creada automáticamente.

## Soporte

Para reportar problemas o sugerencias, contacte al equipo de desarrollo.

## Licencia

Uso interno - DISMAC

## Autor

Generado con Claude Code
Versión 1.0 - Enero 2026
