// Esperar a que el documento esté listo
document.addEventListener('DOMContentLoaded', function() {
    // Aplicar animación a las filas al cargar la página
    applyRowAnimation();
    
    // Confirmación antes de eliminar un cliente
    setupDeleteConfirmation();
    
    // Ordenamiento de columnas
    setupTableSorting();
});

// Función para aplicar animación a las filas
function applyRowAnimation() {
    const rows = document.querySelectorAll('.animate-row');
    rows.forEach((row, index) => {
        // Añadir clase para iniciar la animación con retraso
        setTimeout(() => {
            row.style.opacity = '1';
        }, 50 * index);
    });
}

// Función para configurar confirmación de eliminación
function setupDeleteConfirmation() {
    const deleteButtons = document.querySelectorAll('.delete-button');
    
    deleteButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            
            const confirmDelete = confirm('¿Está seguro que desea eliminar este cliente?');
            
            if (confirmDelete) {
                window.location.href = this.getAttribute('href');
            }
        });
    });
}

// Función para habilitar ordenamiento de la tabla
function setupTableSorting() {
    const table = document.querySelector('.table-mercantil');
    const headers = table.querySelectorAll('thead th');
    
    headers.forEach((header, index) => {
        if (index < headers.length - 1) { // No ordenar la columna de acciones
            header.addEventListener('click', function() {
                sortTable(index);
            });
            
            // Añadir cursor de puntero y estilo para indicar que es ordenable
            header.style.cursor = 'pointer';
            header.classList.add('sortable');
            
            // Añadir ícono de ordenamiento
            const sortIcon = document.createElement('span');
            sortIcon.innerHTML = ' ↕️';
            sortIcon.style.fontSize = '0.8em';
            sortIcon.style.opacity = '0.5';
            header.appendChild(sortIcon);
        }
    });
}

// Función para ordenar la tabla
function sortTable(columnIndex) {
    const table = document.querySelector('.table-mercantil');
    let switching = true;
    let direction = 'asc';
    let switchcount = 0;
    
    while (switching) {
        switching = false;
        const rows = table.rows;
        
        for (let i = 1; i < rows.length - 1; i++) {
            let shouldSwitch = false;
            const x = rows[i].getElementsByTagName('td')[columnIndex];
            const y = rows[i + 1].getElementsByTagName('td')[columnIndex];
            
            // Verificar si es numérico
            const xContent = isNaN(x.innerHTML) ? x.innerHTML.toLowerCase() : parseFloat(x.innerHTML);
            const yContent = isNaN(y.innerHTML) ? y.innerHTML.toLowerCase() : parseFloat(y.innerHTML);
            
            if (direction === 'asc') {
                if (xContent > yContent) {
                    shouldSwitch = true;
                    break;
                }
            } else if (direction === 'desc') {
                if (xContent < yContent) {
                    shouldSwitch = true;
                    break;
                }
            }
        }
        
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
            switchcount++;
        } else {
            if (switchcount === 0 && direction === 'asc') {
                direction = 'desc';
                switching = true;
            }
        }
    }
    
    // Actualizar iconos de ordenamiento
    updateSortIcons(columnIndex, direction);
}

// Actualizar iconos de ordenamiento
function updateSortIcons(activeIndex, direction) {
    const headers = document.querySelectorAll('.table-mercantil thead th');
    
    headers.forEach((header, index) => {
        if (index < headers.length - 1) {
            const icon = header.querySelector('span');
            
            if (index === activeIndex) {
                icon.innerHTML = direction === 'asc' ? ' ↑' : ' ↓';
                icon.style.opacity = '1';
            } else {
                icon.innerHTML = ' ↕️';
                icon.style.opacity = '0.5';
            }
        }
    });
}