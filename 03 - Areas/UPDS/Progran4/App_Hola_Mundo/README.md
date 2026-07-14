# Hola Mundo App - Electron + React

Aplicación de escritorio "Hola Mundo" desarrollada con Electron y React.

## Estructura del Proyecto

```
hola-mundo-app/
├── src/                    # Código fuente
│   ├── App.js             # Componente principal de React
│   └── renderer.js        # Proceso renderer de Electron
├── public/                # Archivos públicos
│   ├── assets/            # Recursos (íconos, imágenes)
│   └── index.html         # HTML base
├── dist/                  # Archivos compilados (generado)
├── main.js                # Proceso principal de Electron
├── webpack.config.js      # Configuración de Webpack
├── package.json           # Dependencias y scripts
└── .gitignore            # Archivos ignorados por Git
```

## Requisitos

- Node.js 16 o superior
- npm

## Instalación

```bash
npm install
```

## Comandos Disponibles

```bash
# Compilar y ejecutar la aplicación
npm start

# Solo compilar
npm run build

# Modo desarrollo (recompila automáticamente)
npm run dev

# Ejecutar sin recompilar
npm run electron

# Empaquetar para distribución
npm run package
```

## Tecnologías

- **Electron** - Framework para aplicaciones de escritorio
- **React** - Biblioteca de UI
- **Webpack** - Bundler de módulos
- **Babel** - Transpilador de JavaScript

## Características

- Interfaz "Hola Mundo" con React
- Muestra información de la plataforma
- Versión de Electron y Node.js
- DevTools integradas en modo desarrollo

## Licencia

Privado
