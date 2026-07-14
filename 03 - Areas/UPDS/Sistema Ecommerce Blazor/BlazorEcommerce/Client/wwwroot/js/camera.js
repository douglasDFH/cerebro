// Ubicación: BlazorEcommerce.Client/wwwroot/js/camera.js

// Variable global para el stream de la cámara
window.camaraStream = null;

// Función para iniciar la cámara
window.iniciarCamara = async function () {
    try {
        console.log("Iniciando cámara...");
        const video = document.getElementById('video');
        if (!video) {
            console.error('No se encontró el elemento de video');
            return false;
        }
        const constraints = {
            video: {
                width: { ideal: 1280 },
                height: { ideal: 720 },
                facingMode: 'user' // Cámara frontal para capturar rostro
            }
        };

        // Detener cualquier stream existente
        if (window.camaraStream) {
            window.camaraStream.getTracks().forEach(track => track.stop());
        }

        // Solicitar acceso a la cámara
        window.camaraStream = await navigator.mediaDevices.getUserMedia(constraints);
        video.srcObject = window.camaraStream;

        // Mostrar botones adecuados
        const btnCapturar = document.getElementById('btnCapturar');
        const btnGuardar = document.getElementById('btnGuardar');
        const btnReintentar = document.getElementById('btnReintentar');
        const capturedImage = document.getElementById('capturedImage');

        if (btnCapturar) btnCapturar.style.display = 'block';
        if (btnGuardar) btnGuardar.style.display = 'none';
        if (btnReintentar) btnReintentar.style.display = 'none';
        if (capturedImage) capturedImage.style.display = 'none';

        return true;
    } catch (err) {
        console.error('Error al acceder a la cámara: ', err);
        return false;
    }
};

// Función para tomar una foto
window.tomarFoto = function () {
    const video = document.getElementById('video');
    const canvas = document.getElementById('canvas');
    const capturedImage = document.getElementById('capturedImage');

    if (!video || !canvas || !capturedImage) {
        console.error('No se encontraron los elementos necesarios para capturar la foto');
        return null;
    }

    // Configurar canvas con las dimensiones del video
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;

    // Dibujar el frame actual del video en el canvas
    const context = canvas.getContext('2d');
    context.drawImage(video, 0, 0, canvas.width, canvas.height);
        
    // Mostrar la imagen capturada
    capturedImage.src = canvas.toDataURL('image/png,0.75');
    capturedImage.style.display = 'block';

    // Ocultar video y mostrar botones adecuados
    video.style.display = 'none';

    const btnCapturar = document.getElementById('btnCapturar');
    const btnGuardar = document.getElementById('btnGuardar');
    const btnReintentar = document.getElementById('btnReintentar');

    if (btnCapturar) btnCapturar.style.display = 'none';
    if (btnGuardar) btnGuardar.style.display = 'block';
    if (btnReintentar) btnReintentar.style.display = 'block';

    return capturedImage.src;
};

// Función para reiniciar la cámara
window.reiniciarCamara = function () {
    const video = document.getElementById('video');
    const capturedImage = document.getElementById('capturedImage');

    if (!video || !capturedImage) {
        console.error('No se encontraron los elementos necesarios para reiniciar la cámara');
        return;
    }

    // Mostrar video y ocultar imagen
    video.style.display = 'block';
    capturedImage.style.display = 'none';

    // Mostrar botones adecuados
    const btnCapturar = document.getElementById('btnCapturar');
    const btnGuardar = document.getElementById('btnGuardar');
    const btnReintentar = document.getElementById('btnReintentar');

    if (btnCapturar) btnCapturar.style.display = 'block';
    if (btnGuardar) btnGuardar.style.display = 'none';
    if (btnReintentar) btnReintentar.style.display = 'none';
};

// Función para detener la cámara
window.detenerCamara = function () {
    if (window.camaraStream) {
        window.camaraStream.getTracks().forEach(track => track.stop());
    }
};

// Función para obtener la imagen capturada
window.obtenerImagenCapturada = function () {
    try {
        const imagen = document.getElementById('capturedImage');
        if (imagen && imagen.style.display !== 'none') {
            return imagen.src;
        }
        return null;
    } catch (err) {
        console.error('Error al obtener la imagen capturada: ', err);
        return null;
    }
};