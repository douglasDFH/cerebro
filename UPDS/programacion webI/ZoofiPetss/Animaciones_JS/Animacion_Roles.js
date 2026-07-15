// Animación para mostrar u ocultar el formulario
const toggleButton = document.querySelector('.toggle-form');
        const formContainer = document.querySelector('.proforma-form-container');

        toggleButton.addEventListener('click', () => {
            formContainer.classList.toggle('open');
            toggleButton.classList.toggle('open');
        });

    document.addEventListener("DOMContentLoaded", () => {
        const toggleForm = document.querySelector(".toggle-form");

        toggleForm.addEventListener("click", () => {
            toggleForm.classList.toggle("active");
        });
    });








    // Función para agregar más campos de permisos
    function agregarPermiso() {
        const container = document.getElementById('permisosContainer');
        const div = document.createElement('div');
        div.innerHTML = `
            <input type="checkbox" name="Permisos[]" value="Nuevo Permiso"> Nuevo Permiso
            <button type="button" onclick="this.parentElement.remove()" class="remove-button">Eliminar</button>
        `;
        container.appendChild(div);
    }

    // Verificar el estado del login al cargar la página
    document.addEventListener('DOMContentLoaded', function() {
        const isLoggedIn = localStorage.getItem('isLoggedIn');
        const currentPage = window.location.pathname.split('/').pop();
        
        if (!isLoggedIn) {
            // Si no está logueado y no está en la página principal, redirigir al login
            if (currentPage !== './Capa_Plataforma') {
                document.getElementById('loginLayer').style.transform = 'translateX(0)';
                document.getElementById('mainLayer').style.transform = 'translateX(100%)';
            }
        } else {
            checkLoginState();
            // Si está logueado y está en la página principal, mostrar el contenido
            if (currentPage === './Capa_Plataforma') {
                document.getElementById('loginLayer').style.transform = 'translateX(-100%)';
                document.getElementById('mainLayer').style.transform = 'translateX(0)';
            }
        }
        setupEventListeners();
    });

    // Función para verificar el estado del login
    function checkLoginState() {
        const isLoggedIn = localStorage.getItem('isLoggedIn');
        const username = localStorage.getItem('username');
        
        if (isLoggedIn === 'true' && username) {
            document.getElementById('userDisplay').textContent = username;
            document.getElementById('loginLayer').style.transform = 'translateX(-100%)';
            document.getElementById('mainLayer').style.transform = 'translateX(0)';
        }
    }

     // Función de logout
// Reemplaza la función logout existente en capa_proforma.html con esta:
function logout() {
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('username');
    window.location.href = '../index.html';
}

// Agrega esta función al inicio del archivo para verificar la sesión
window.onload = function() {
    const isLoggedIn = localStorage.getItem('isLoggedIn');
    if (!isLoggedIn) {
        window.location.href = 'index.html';
    }
}

    // Función para navegar a la proforma
    function navegarProforma(event) {
        event.preventDefault();
        if (localStorage.getItem('isLoggedIn') === 'true') {
            window.location.href = 'Formularios.html/FORM_Roles.html';
        }
    }

    // Función para manejar la visibilidad de las secciones
    function toggleSectionVisibility(sectionToShow) {
        const sections = document.querySelectorAll('.main-content > div:not(.top-bar)');
        sections.forEach(section => {
            section.style.display = 'none';
        });
        
        if (sectionToShow) {
            sectionToShow.style.display = 'block';
        }
    }

    // Configuración de los event listeners
    function setupEventListeners() {
        const sidebar = document.querySelector('.sidebar');
        const mainContent = document.querySelector('.main-content');
        let activeSubmenu = null;
        
        // Función para cerrar todos los submenús
        function closeAllSubmenus() {
            document.querySelectorAll('.submenu.active').forEach(submenu => {
                submenu.classList.remove('active');
                const arrow = submenu.parentElement.querySelector('.arrow');
                if (arrow) arrow.classList.remove('active');
            });
        }

        // Click en los items del menú
        document.querySelectorAll('.menu-item').forEach(item => {
            item.addEventListener('click', function(e) {
                e.stopPropagation();
                if (!sidebar.classList.contains('expanded')) {
                    sidebar.classList.add('expanded');
                    mainContent.classList.add('expanded');
                    return;
                }

                if (this.id === 'menuInicio') {
                    if (localStorage.getItem('isLoggedIn') === 'true') {
                        window.location.href = '/capa_proforma.html';
                    } else {
                        alert('Debe iniciar sesión primero');
                    }
                    return;
                }

                const submenu = this.parentElement.querySelector('.submenu');
                const arrow = this.querySelector('.arrow');

                if (submenu) {
                    if (activeSubmenu && activeSubmenu !== submenu) {
                        activeSubmenu.classList.remove('active');
                        const activeArrow = activeSubmenu.parentElement.querySelector('.arrow');
                        if (activeArrow) activeArrow.classList.remove('active');
                    }

                    submenu.classList.toggle('active');
                    if (arrow) arrow.classList.toggle('active');
                    activeSubmenu = submenu.classList.contains('active') ? submenu : null;
                }
            });
        });

        document.addEventListener('click', function(e) {
            if (!sidebar.contains(e.target) && !e.target.matches('#menu-toggle')) {
                sidebar.classList.remove('expanded');
                mainContent.classList.remove('expanded');
                closeAllSubmenus();
            }
        });

        document.getElementById('menu-toggle').addEventListener('click', function(e) {
            e.stopPropagation();
            sidebar.classList.toggle('expanded');
            mainContent.classList.toggle('expanded');
            if (!sidebar.classList.contains('expanded')) {
                closeAllSubmenus();
            }
        });

        document.querySelectorAll('.submenu').forEach(submenu => {
            submenu.addEventListener('click', function(e) {
                e.stopPropagation();
            });
        });

        document.querySelectorAll('a').forEach(link => {
            link.addEventListener('click', function(e) {
                if (localStorage.getItem('isLoggedIn') !== 'true') {
                    e.preventDefault();
                    alert('Debe iniciar sesión primero');
                }
            });
        });

        document.querySelectorAll('.submenu li').forEach(item => {
            item.addEventListener('click', function(e) {
                e.stopPropagation();
            });
        });
    }

    // Desplazar al final del contenido
    const welcomeSection = document.querySelector('.welcome-section');
    welcomeSection.scrollTop = welcomeSection.scrollHeight;