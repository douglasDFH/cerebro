// wwwroot/js/checkout.js

// Variables globales
let map = null;
let markers = [];
let selectedStoreId = 1; // ID de la tienda seleccionada por defecto

// Paleta de colores del sitio
const siteColors = {
    primary: "#e60000",        // Rojo principal
    primaryHover: "#cc0000",   // Rojo oscuro para hover
    secondary: "#000000",      // Negro
    white: "#ffffff",          // Blanco
    lightGray: "#f5f5f5",      // Gris claro para fondos
    mediumGray: "#e0e0e0"      // Gris medio para bordes
};

// Coordenadas de las tiendas
const tiendasData = [
    {
        id: 1,
        nombre: "Santa Cruz - Oficina Central",
        latitud: -17.783335,
        longitud: -63.182126,
        direccion: "Av. Irala #123",
        telefono: "+591 3 334-5678",
        horario: "Lun-Vie: 8:00-18:00, Sáb: 9:00-13:00"
    },
    {
        id: 2,
        nombre: "La Paz - Sucursal",
        latitud: -16.489689,
        longitud: -68.119293,
        direccion: "Av. 16 de Julio #456",
        telefono: "+591 2 245-6789",
        horario: "Lun-Vie: 8:30-18:30, Sáb: 9:00-14:00"
    },
    {
        id: 3,
        nombre: "Cochabamba - Sucursal",
        latitud: -17.393688,
        longitud: -66.157097,
        direccion: "Av. América #789",
        telefono: "+591 4 456-7890",
        horario: "Lun-Vie: 8:00-18:00, Sáb: 9:00-13:00"
    }
];

// Función para crear un icono personalizado para los marcadores
function crearIconoPersonalizado(esSeleccionado) {
    return L.divIcon({
        className: 'marcador-personalizado',
        html: `<div class="marcador-pin" style="background-color: ${esSeleccionado ? siteColors.primary : siteColors.secondary}">
                <div class="marcador-pin-inner">
                    <i class="oi oi-map-marker" style="color: ${siteColors.white}; font-size: 16px;"></i>
                </div>
                <div class="marcador-pin-shadow"></div>
               </div>`,
        iconSize: [40, 40],
        iconAnchor: [20, 40],
        popupAnchor: [0, -40]
    });
}

// Función para inicializar el mapa con estilo mejorado
function inicializarMapa() {
    try {
        console.log("Inicializando mapa con estilo mejorado...");

        // Verificar si Leaflet está disponible
        if (typeof L === 'undefined') {
            console.error("Leaflet no está cargado. Cargando dinámicamente...");
            cargarLeaflet();
            return false;
        }

        // Verificar si el contenedor del mapa existe
        const mapContainer = document.getElementById('mapa');
        if (!mapContainer) {
            console.error("El contenedor del mapa no existe en el DOM");
            // Mostrar un mensaje de error estilizado
            renderizarMensajeError();
            return false;
        }

        // Agregar estilos personalizados para los marcadores
        agregarEstilosPersonalizados();

        // Si ya existe un mapa, destruirlo primero
        if (map) {
            map.remove();
            map = null;
        }

        // Inicializar el mapa centrado en Santa Cruz con un estilo personalizado
        map = L.map('mapa', {
            zoomControl: false, // Desactivar controles de zoom por defecto
            attributionControl: false // Desactivar atribución por defecto
        }).setView([-17.783335, -63.182126], 13);

        // Agregar controles de zoom en la esquina inferior derecha
        L.control.zoom({
            position: 'bottomright'
        }).addTo(map);

        // Agregar la capa de OpenStreetMap con estilo mejorado
        L.tileLayer('https://{s}.basemaps.cartocdn.com/light_all/{z}/{x}/{y}{r}.png', {
            attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
            subdomains: 'abcd',
            maxZoom: 19
        }).addTo(map);

        // Agregar atribución personalizada
        L.control.attribution({
            position: 'bottomleft',
            prefix: '<a href="#">DISMACTEC</a>'
        }).addTo(map);

        // Agregar marcadores para todas las tiendas
        agregarMarcadores(selectedStoreId);

        // Ajustar el mapa después de un breve retraso para asegurar que se renderice correctamente
        setTimeout(() => {
            if (map) map.invalidateSize();
        }, 300);

        // Agregar el panel de información de tiendas
        crearPanelInfoTiendas();

        console.log("Mapa inicializado correctamente");
        return true;
    } catch (error) {
        console.error("Error al inicializar el mapa:", error);
        renderizarMensajeError(error.message);
        return false;
    }
}

// Función para agregar estilos personalizados para los marcadores
function agregarEstilosPersonalizados() {
    // Crear un elemento de estilo si no existe
    if (!document.getElementById('custom-map-styles')) {
        const styleElement = document.createElement('style');
        styleElement.id = 'custom-map-styles';
        styleElement.textContent = `
            .marcador-personalizado {
                background: transparent;
                border: none;
            }
            .marcador-pin {
                width: 30px;
                height: 30px;
                border-radius: 50% 50% 50% 0;
                transform: rotate(-45deg);
                display: flex;
                align-items: center;
                justify-content: center;
                position: relative;
            }
            .marcador-pin-inner {
                width: 20px;
                height: 20px;
                border-radius: 50%;
                background: white;
                display: flex;
                align-items: center;
                justify-content: center;
                transform: rotate(45deg);
            }
            .marcador-pin-shadow {
                position: absolute;
                width: 20px;
                height: 10px;
                background: rgba(0,0,0,0.2);
                border-radius: 50%;
                bottom: -8px;
                left: 5px;
                transform: rotate(45deg);
                z-index: -1;
            }
            .tienda-popup {
                min-width: 250px;
            }
            .tienda-popup-header {
                background: ${siteColors.primary};
                color: ${siteColors.white};
                padding: 10px 15px;
                margin: -13px -19px 10px;
                border-radius: 12px 12px 0 0;
                font-weight: bold;
                font-size: 16px;
            }
            .tienda-popup-content {
                padding: 5px 0;
            }
            .tienda-popup-item {
                display: flex;
                margin-bottom: 8px;
                align-items: flex-start;
            }
            .tienda-popup-icon {
                flex: 0 0 20px;
                color: ${siteColors.primary};
                margin-right: 8px;
                text-align: center;
            }
            .tienda-popup-text {
                flex: 1;
                font-size: 14px;
                line-height: 1.4;
            }
            .tienda-popup-button {
                background: ${siteColors.primary};
                color: ${siteColors.white};
                border: none;
                padding: 8px 15px;
                border-radius: 4px;
                margin-top: 10px;
                cursor: pointer;
                transition: background 0.3s ease;
                font-weight: bold;
                width: 100%;
                text-align: center;
                font-size: 14px;
            }
            .tienda-popup-button:hover {
                background: ${siteColors.primaryHover};
            }
            .info-panel {
                position: absolute;
                top: 10px;
                right: 10px;
                z-index: 1000;
                background: ${siteColors.white};
                border-radius: 8px;
                box-shadow: 0 0 20px rgba(0,0,0,0.2);
                width: 280px;
                overflow: hidden;
            }
            .info-panel-header {
                background: ${siteColors.secondary};
                color: ${siteColors.white};
                padding: 12px 15px;
                font-weight: bold;
                display: flex;
                align-items: center;
                justify-content: space-between;
            }
            .info-panel-title {
                font-size: 16px;
                margin: 0;
            }
            .info-panel-toggle {
                background: transparent;
                border: none;
                color: ${siteColors.white};
                cursor: pointer;
                font-size: 16px;
                padding: 0;
            }
            .info-panel-body {
                padding: 10px 15px;
                max-height: 300px;
                overflow-y: auto;
            }
            .info-panel-store {
                padding: 10px;
                border-bottom: 1px solid ${siteColors.mediumGray};
                cursor: pointer;
                transition: all 0.2s ease;
                display: flex;
                align-items: center;
            }
            .info-panel-store:last-child {
                border-bottom: none;
            }
            .info-panel-store.active {
                background: ${siteColors.lightGray};
                border-left: 4px solid ${siteColors.primary};
            }
            .info-panel-store:hover:not(.active) {
                background: ${siteColors.lightGray};
            }
            .info-panel-store-icon {
                margin-right: 10px;
                width: 20px;
                height: 20px;
                display: flex;
                align-items: center;
                justify-content: center;
                color: ${siteColors.primary};
            }
            .info-panel-store-content {
                flex: 1;
            }
            .info-panel-store-title {
                font-weight: 600;
                font-size: 14px;
                margin-bottom: 4px;
            }
            .info-panel-store-address {
                font-size: 12px;
                color: #777;
            }
            .error-container {
                width: 100%;
                height: 100%;
                display: flex;
                align-items: center;
                justify-content: center;
                background: ${siteColors.lightGray};
                border-radius: 8px;
                padding: 20px;
                text-align: center;
            }
            .error-icon {
                font-size: 32px;
                color: ${siteColors.primary};
                margin-bottom: 10px;
            }
            .error-message {
                font-size: 16px;
                color: ${siteColors.secondary};
                margin-bottom: 15px;
            }
            .error-action {
                background: ${siteColors.primary};
                color: ${siteColors.white};
                border: none;
                padding: 8px 15px;
                border-radius: 4px;
                cursor: pointer;
                transition: background 0.3s ease;
                font-weight: bold;
            }
            .error-action:hover {
                background: ${siteColors.primaryHover};
            }
            .leaflet-container {
                font-family: 'Montserrat', 'Helvetica Neue', Helvetica, Arial, sans-serif;
                border-radius: 8px;
                overflow: hidden;
                box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
            }
        `;
        document.head.appendChild(styleElement);
    }
}

// Función para agregar marcadores al mapa con estilo mejorado
function agregarMarcadores(tiendaId) {
    try {
        if (!map) {
            console.error("El mapa no está inicializado");
            return false;
        }

        // Actualizar la tienda seleccionada
        selectedStoreId = parseInt(tiendaId);

        // Limpiar marcadores anteriores
        if (markers.length > 0) {
            markers.forEach(marker => map.removeLayer(marker));
            markers = [];
        }

        // Agregar los nuevos marcadores
        tiendasData.forEach(tienda => {
            // Determinar si es la tienda seleccionada
            const esSeleccionada = tienda.id === selectedStoreId;

            // Crear el contenido del popup con estilo mejorado
            const popupContent = `
                <div class="tienda-popup">
                    <div class="tienda-popup-header">
                        ${tienda.nombre}
                    </div>
                    <div class="tienda-popup-content">
                        <div class="tienda-popup-item">
                            <div class="tienda-popup-icon"><i class="oi oi-home"></i></div>
                            <div class="tienda-popup-text">${tienda.direccion}</div>
                        </div>
                        <div class="tienda-popup-item">
                            <div class="tienda-popup-icon"><i class="oi oi-phone"></i></div>
                            <div class="tienda-popup-text">${tienda.telefono}</div>
                        </div>
                        <div class="tienda-popup-item">
                            <div class="tienda-popup-icon"><i class="oi oi-clock"></i></div>
                            <div class="tienda-popup-text">${tienda.horario}</div>
                        </div>
                        <button class="tienda-popup-button" onclick="seleccionarTienda(${tienda.id})">
                            ${esSeleccionada ? 'Tienda seleccionada' : 'Seleccionar esta tienda'}
                        </button>
                    </div>
                </div>
            `;

            // Crear el marcador con icono personalizado
            const marker = L.marker([tienda.latitud, tienda.longitud], {
                icon: crearIconoPersonalizado(esSeleccionada)
            })
                .addTo(map)
                .bindPopup(popupContent, {
                    className: 'custom-popup',
                    closeButton: true,
                    closeOnClick: false,
                    autoClose: false
                });

            // Guardar referencia al marcador
            markers.push(marker);

            // Si es la tienda seleccionada, centrar el mapa en ella y abrir el popup
            if (esSeleccionada) {
                map.setView([tienda.latitud, tienda.longitud], 14);
                marker.openPopup();

                // Actualizar el estado activo en el panel de información
                actualizarPanelInfoTiendas();
            }
        });

        return true;
    } catch (error) {
        console.error("Error al agregar marcadores:", error);
        return false;
    }
}

// Función para crear el panel de información de tiendas
function crearPanelInfoTiendas() {
    // Verificar si ya existe el panel
    if (document.getElementById('tiendas-info-panel')) {
        return;
    }

    // Crear el contenedor del panel
    const infoPanel = document.createElement('div');
    infoPanel.id = 'tiendas-info-panel';
    infoPanel.className = 'info-panel';

    // Crear el encabezado del panel
    const panelHeader = document.createElement('div');
    panelHeader.className = 'info-panel-header';
    panelHeader.innerHTML = `
        <h3 class="info-panel-title">Nuestras Tiendas</h3>
        <button class="info-panel-toggle" id="toggle-panel">
            <i class="oi oi-chevron-top"></i>
        </button>
    `;

    // Crear el cuerpo del panel
    const panelBody = document.createElement('div');
    panelBody.className = 'info-panel-body';

    // Agregar cada tienda al panel
    tiendasData.forEach(tienda => {
        const tiendaElement = document.createElement('div');
        tiendaElement.className = `info-panel-store ${tienda.id === selectedStoreId ? 'active' : ''}`;
        tiendaElement.dataset.tiendaId = tienda.id;
        tiendaElement.innerHTML = `
            <div class="info-panel-store-icon">
                <i class="oi oi-map-marker"></i>
            </div>
            <div class="info-panel-store-content">
                <div class="info-panel-store-title">${tienda.nombre}</div>
                <div class="info-panel-store-address">${tienda.direccion}</div>
            </div>
        `;

        // Agregar evento de clic para seleccionar una tienda
        tiendaElement.addEventListener('click', () => {
            actualizarMapaTiendaSeleccionada(tienda.id);
        });

        panelBody.appendChild(tiendaElement);
    });

    // Agregar el encabezado y el cuerpo al panel
    infoPanel.appendChild(panelHeader);
    infoPanel.appendChild(panelBody);

    // Agregar el panel al contenedor del mapa
    const mapContainer = document.getElementById('mapa');
    mapContainer.style.position = 'relative';
    mapContainer.appendChild(infoPanel);

    // Agregar evento para mostrar/ocultar el panel
    const toggleButton = document.getElementById('toggle-panel');
    toggleButton.addEventListener('click', () => {
        panelBody.style.display = panelBody.style.display === 'none' ? 'block' : 'none';
        toggleButton.innerHTML = panelBody.style.display === 'none' ?
            '<i class="oi oi-chevron-bottom"></i>' :
            '<i class="oi oi-chevron-top"></i>';
    });
}

// Función para actualizar el panel de información de tiendas
function actualizarPanelInfoTiendas() {
    const tiendaElements = document.querySelectorAll('.info-panel-store');
    tiendaElements.forEach(element => {
        const tiendaId = parseInt(element.dataset.tiendaId);
        if (tiendaId === selectedStoreId) {
            element.classList.add('active');
        } else {
            element.classList.remove('active');
        }
    });
}

// Función para seleccionar una tienda cuando se hace clic en el botón del popup
function seleccionarTienda(tiendaId) {
    // Actualizar la tienda seleccionada en el estado
    selectedStoreId = tiendaId;

    // Actualizar los marcadores
    agregarMarcadores(tiendaId);

    // Si hay un elemento para mostrar la tienda seleccionada en la interfaz, actualizarlo
    const tiendaSeleccionadaElement = document.getElementById('tienda-seleccionada');
    if (tiendaSeleccionadaElement) {
        const tiendaSeleccionada = tiendasData.find(t => t.id === tiendaId);
        if (tiendaSeleccionada) {
            tiendaSeleccionadaElement.textContent = tiendaSeleccionada.nombre;
        }
    }

    // Disparar un evento personalizado para notificar sobre el cambio de tienda
    const event = new CustomEvent('tiendaSeleccionada', {
        detail: { tiendaId: tiendaId }
    });
    document.dispatchEvent(event);

    console.log("Tienda seleccionada:", tiendaId);
}

// Función para actualizar el mapa cuando se cambia la tienda seleccionada
function actualizarMapaTiendaSeleccionada(tiendaId) {
    console.log("Actualizando mapa para tienda ID:", tiendaId);

    // Si el mapa no está inicializado, inicializarlo primero
    if (!map) {
        inicializarMapa();
    } else {
        // Si ya está inicializado, solo actualizar los marcadores
        agregarMarcadores(tiendaId);
    }
}

// Función para renderizar un mensaje de error estilizado
function renderizarMensajeError(mensaje = "No se pudo cargar el mapa. Por favor, intenta de nuevo más tarde.") {
    const mapContainer = document.getElementById('mapa');
    if (mapContainer) {
        mapContainer.innerHTML = `
            <div class="error-container">
                <div>
                    <div class="error-icon">
                        <i class="oi oi-warning"></i>
                    </div>
                    <div class="error-message">
                        ${mensaje}
                    </div>
                    <button class="error-action" onclick="inicializarMapa()">
                        Reintentar
                    </button>
                </div>
            </div>
        `;
    }
}

// Función para cargar Leaflet dinámicamente si no está disponible
function cargarLeaflet() {
    // Cargar CSS
    if (!document.getElementById('leaflet-css')) {
        const linkCSS = document.createElement('link');
        linkCSS.id = 'leaflet-css';
        linkCSS.rel = 'stylesheet';
        linkCSS.href = 'https://unpkg.com/leaflet@1.9.4/dist/leaflet.css';
        document.head.appendChild(linkCSS);
    }

    // Cargar JS
    if (!document.getElementById('leaflet-js')) {
        const scriptJS = document.createElement('script');
        scriptJS.id = 'leaflet-js';
        scriptJS.src = 'https://unpkg.com/leaflet@1.9.4/dist/leaflet.js';
        scriptJS.onload = function () {
            console.log("Leaflet cargado correctamente");
            // Intentar inicializar el mapa de nuevo después de un pequeño retraso
            setTimeout(inicializarMapa, 500);
        };
        document.head.appendChild(scriptJS);
    }
}

// Redefinir la función inicializarMapa para compatibilidad con el código antiguo
window.inicializarMapa = inicializarMapa;

// Exponer funciones al contexto DotNet
window.mapFunctions = {
    inicializarMapa: inicializarMapa,
    actualizarMapaTiendaSeleccionada: actualizarMapaTiendaSeleccionada,
    seleccionarTienda: seleccionarTienda
};

// Inicializar el mapa automáticamente cuando se carga la página
document.addEventListener('DOMContentLoaded', function () {
    // Esperar un momento para asegurar que el DOM esté completamente cargado
    setTimeout(() => {
        const mapContainer = document.getElementById('mapa');
        if (mapContainer) {
            inicializarMapa();
        }
    }, 500);
});