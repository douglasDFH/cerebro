// Variables globales para el mapa de Leaflet
let map;
let marker;
let direccionSeleccionada = '';
let latitud = 0;
let longitud = 0;

// Función para inicializar el mapa
window.inicializarMapa = function () {
    try {
        console.log("Inicializando mapa...");
        const mapContainer = document.getElementById('leaflet-map');
        if (!mapContainer) {
            console.error('No se encontró el contenedor del mapa');
            return false;
        }

        // Coordenadas predeterminadas (Santa Cruz, Bolivia)
        const defaultLocation = [-17.783, -63.182];

        // Inicializar el mapa si no existe ya
        if (!map) {
            // Inicializar el mapa de Leaflet
            map = L.map('leaflet-map').setView(defaultLocation, 13);

            // Agregar capa de OpenStreetMap (tiles)
            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors',
                maxZoom: 19
            }).addTo(map);

            // Agregar marcador draggable
            marker = L.marker(defaultLocation, {
                draggable: true,
                title: 'Arrastra para seleccionar tu ubicación'
            }).addTo(map);

            // Evento cuando se arrastra el marcador
            marker.on('dragend', function (event) {
                const position = marker.getLatLng();
                latitud = position.lat;
                longitud = position.lng;

                // Obtener la dirección usando OpenStreetMap Nominatim
                obtenerDireccionDesdeLatLng(latitud, longitud);
            });

            // También agregar evento de clic en el mapa
            map.on('click', function (e) {
                latitud = e.latlng.lat;
                longitud = e.latlng.lng;

                // Mover el marcador a la posición clicada
                marker.setLatLng([latitud, longitud]);

                // Obtener la dirección desde las coordenadas
                obtenerDireccionDesdeLatLng(latitud, longitud);
            });
        } else {
            // Si el mapa ya existe, actualiza la vista
            map.setView(defaultLocation, 13);
            marker.setLatLng(defaultLocation);
        }

        // Después de un pequeño retraso, configurar la búsqueda
        setTimeout(() => {
            const searchInput = document.getElementById('map-search-input');
            const searchButton = document.getElementById('btn-buscar-direccion');

            if (searchButton) {
                // Eliminar listeners previos para evitar duplicados
                searchButton.replaceWith(searchButton.cloneNode(true));
                const newSearchButton = document.getElementById('btn-buscar-direccion');

                // Búsqueda al hacer clic en el botón
                newSearchButton.addEventListener('click', function () {
                    if (searchInput) {
                        buscarDireccion(searchInput.value);
                    }
                });
            }

            if (searchInput) {
                // Eliminar listeners previos para evitar duplicados
                searchInput.replaceWith(searchInput.cloneNode(true));
                const newSearchInput = document.getElementById('map-search-input');

                // Búsqueda al presionar Enter
                newSearchInput.addEventListener('keypress', function (e) {
                    if (e.key === 'Enter') {
                        e.preventDefault();
                        buscarDireccion(newSearchInput.value);
                    }
                });
            }
        }, 500);

        // Refrescar el mapa después de mostrarlo
        setTimeout(() => {
            if (map) {
                map.invalidateSize(true);
                console.log("Mapa refrescado después de inicialización");
            }
        }, 500);

        return true;
    } catch (err) {
        console.error('Error al inicializar el mapa: ', err);
        return false;
    }
};

// Función para buscar una dirección por texto
function buscarDireccion(query) {
    if (!query || query.trim() === '') return;

    console.log("Buscando dirección:", query);

    // Usar la API de Nominatim para geocodificar (convertir texto a coordenadas)
    fetch(`https://nominatim.openstreetmap.org/search?format=json&q=${encodeURIComponent(query)}&limit=1`)
        .then(response => response.json())
        .then(data => {
            if (data && data.length > 0) {
                const result = data[0];
                latitud = parseFloat(result.lat);
                longitud = parseFloat(result.lon);

                console.log("Coordenadas encontradas:", latitud, longitud);

                // Mover el mapa y el marcador a la ubicación
                map.setView([latitud, longitud], 16);
                marker.setLatLng([latitud, longitud]);

                // Obtener la dirección completa
                direccionSeleccionada = result.display_name;
                const dirElement = document.getElementById('direccion-seleccionada');
                if (dirElement) {
                    dirElement.innerText = direccionSeleccionada;
                }
            } else {
                alert('No se encontraron resultados para: ' + query);
            }
        })
        .catch(error => {
            console.error('Error al buscar la dirección: ', error);
            alert('Error al buscar la dirección: ' + error.message);
        });
}

// Función para obtener la dirección a partir de coordenadas
function obtenerDireccionDesdeLatLng(lat, lng) {
    console.log("Obteniendo dirección desde coordenadas:", lat, lng);

    fetch(`https://nominatim.openstreetmap.org/reverse?format=json&lat=${lat}&lon=${lng}&zoom=18&addressdetails=1`)
        .then(response => response.json())
        .then(data => {
            if (data && data.display_name) {
                direccionSeleccionada = data.display_name;
                console.log("Dirección encontrada:", direccionSeleccionada);

                const dirElement = document.getElementById('direccion-seleccionada');
                if (dirElement) {
                    dirElement.innerText = direccionSeleccionada;
                }
            }
        })
        .catch(error => {
            console.error('Error al obtener la dirección: ', error);
        });
}

// Función para obtener la dirección seleccionada
window.obtenerDireccionSeleccionada = function () {
    // Asegurar que devolvemos valores por defecto si las variables son null
    const resultado = {
        Direccion: direccionSeleccionada || "",
        Latitud: latitud || 0,
        Longitud: longitud || 0
    };

    console.log("Retornando dirección seleccionada:", resultado);
    return resultado;
};

// Función para inicializar el mapa con una posición existente
window.inicializarMapaConPosicion = function (latitud, longitud, direccion) {
    try {
        console.log("Inicializando mapa con posición existente:", latitud, longitud);

        // Inicializar el mapa normalmente primero
        const inicializado = window.inicializarMapa();
        if (!inicializado) {
            return false;
        }

        // Esperar un poco para asegurarnos de que el mapa está listo
        setTimeout(() => {
            // Establecer la vista y el marcador en la ubicación guardada
            if (map && marker) {
                // Actualizar las variables globales
                window.latitud = latitud;
                window.longitud = longitud;
                window.direccionSeleccionada = direccion || "";

                // Actualizar el mapa y el marcador
                map.setView([latitud, longitud], 16);
                marker.setLatLng([latitud, longitud]);

                // Actualizar el texto de la dirección seleccionada
                const dirElement = document.getElementById('direccion-seleccionada');
                if (dirElement && direccion) {
                    dirElement.innerText = direccion;
                }

                // Refrescar el mapa para asegurarse de que se renderice correctamente
                map.invalidateSize(true);
            }
        }, 500);

        return true;
    } catch (err) {
        console.error('Error al inicializar el mapa con posición:', err);
        return false;
    }
};

// Función para refrescar el mapa (soluciona problemas de visualización)
window.refreshMap = function () {
    console.log("Refrescando mapa manualmente...");
    if (map) {
        // Forzar la actualización del tamaño del mapa
        map.invalidateSize(true);
        console.log("Mapa actualizado correctamente");
        return true;
    }
    console.log("No se pudo actualizar el mapa - no existe instancia");
    return false;
};

// Función para verificar el estado del mapa y depurar problemas
window.checkMapStatus = function () {
    console.log('Estado del contenedor del mapa:');
    const mapContainer = document.getElementById('leaflet-map');
    console.log('Existe el contenedor:', !!mapContainer);

    if (mapContainer) {
        console.log('Dimensiones:', mapContainer.offsetWidth, 'x', mapContainer.offsetHeight);
        console.log('Visibilidad:', window.getComputedStyle(mapContainer).display);
        console.log('Posición:', mapContainer.getBoundingClientRect());
    }

    console.log('Estado de Leaflet:', typeof L !== 'undefined' ? 'Cargado' : 'No cargado');
    console.log('Estado de la instancia del mapa:', !!window.map);

    return {
        containerExists: !!mapContainer,
        leafletLoaded: typeof L !== 'undefined',
        mapInitialized: !!window.map
    };
};