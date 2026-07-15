package negocio;


public class clsListaDoble {
    private clsNodoDoble cabeza;
    private clsNodoDoble cola;
    private clsNodoDoble pLD; // Puntero Lista Doble 

    public clsListaDoble(){
        this.cabeza = null;
        this.cola = null;
        this.pLD = null;
    }
    
    public void insertarInicio(int dato){
        clsNodoDoble nuevo = new clsNodoDoble(dato);
        if(cabeza == null){
            // Lista vacía
            cabeza = cola = nuevo;
            pLD = nuevo; // El puntero apunta al primer nodo
        } else {
            // Lista con elementos
            nuevo.setRefD(cabeza); // Referencia Derecha apunta a la cabeza actual
            cabeza.setRefI(nuevo); // Referencia Izquierda de cabeza apunta al nuevo
            cabeza = nuevo;
        }
    }
    

    public void insertarFinal(int dato){
        clsNodoDoble nuevo = new clsNodoDoble(dato);
        if(cola == null){
            // Lista vacía
            cabeza = cola = nuevo;
            pLD = nuevo; // El puntero apunta al primer nodo
        } else {
            // Lista con elementos
            cola.setRefD(nuevo); // Referencia Derecha de cola apunta al nuevo
            nuevo.setRefI(cola); // Referencia Izquierda del nuevo apunta a cola
            cola = nuevo;
        }
    }

    public void insertarDerecha(int dato) {
        clsNodoDoble nAux = new clsNodoDoble(dato);

        if (pLD == null) {
            // Caso 1: Lista vacía o puntero no posicionado
            pLD = nAux;
            if (cabeza == null) {
                cabeza = cola = nAux;
            }
        } else if (pLD.getRefD() == null) {
            // Caso 2: Puntero está en el último nodo
            pLD.setRefD(nAux);      // RefD del puntero apunta al nuevo nodo
            nAux.setRefI(pLD);      // RefI del nuevo apunta al puntero
            cola = nAux; // Actualizar la cola
        } else {
            // Caso 3: Puntero está en el medio
            clsNodoDoble p1Siguiente = pLD.getRefD();
            
            pLD.setRefD(nAux);          // RefD del puntero apunta al nuevo
            nAux.setRefI(pLD);          // RefI del nuevo apunta al puntero
            
            nAux.setRefD(p1Siguiente);  // RefD del nuevo apunta al siguiente
            p1Siguiente.setRefI(nAux);  // RefI del siguiente apunta al nuevo
        }
    }

    public void insertarIzquierda(int dato) {
        clsNodoDoble nAux = new clsNodoDoble(dato);

        if (pLD == null) {
            // Caso 1: Lista vacía o puntero no posicionado
            pLD = nAux;
            if (cabeza == null) {
                cabeza = cola = nAux;
            }
        } else if (pLD.getRefI() == null) {
            // Caso 2: Puntero está en el primer nodo
            nAux.setRefD(pLD);      // RefD del nuevo apunta al puntero
            pLD.setRefI(nAux);      // RefI del puntero apunta al nuevo
            cabeza = nAux; // Actualizar la cabeza
        } else {
            // Caso 3: Puntero está en el medio
            clsNodoDoble p1Anterior = pLD.getRefI();
            
            p1Anterior.setRefD(nAux);   // RefD del anterior apunta al nuevo
            nAux.setRefI(p1Anterior);   // RefI del nuevo apunta al anterior
            
            nAux.setRefD(pLD);          // RefD del nuevo apunta al puntero
            pLD.setRefI(nAux);          // RefI del puntero apunta al nuevo
        }
    }

    // ==================== ELIMINAR MÉTODOS EDUCATIVOS ====================
    
    /**
     * MÉTODO EDUCATIVO: Eliminar el primer nodo (cabeza) de la lista
     * Usa RefD (Referencia Derecha) y RefI (Referencia Izquierda) para mayor claridad
     */
    public int eliminarInicio() {
        if (cabeza == null) {
            return -1; // Lista vacía
        }
        
        int valor = cabeza.getDato();
        
        if (cabeza == cola) {
            // Solo hay un elemento
            cabeza = cola = null;
            pLD = null; // Resetear puntero
        } else {
            // Hay más de un elemento
            cabeza = cabeza.getRefD();      // La nueva cabeza es el siguiente nodo
            cabeza.setRefI(null);           // RefI de la nueva cabeza apunta a null
            
            // Ajustar puntero si estaba en el nodo eliminado
            if (pLD != null && pLD.getRefI() == null && pLD != cabeza) {
                pLD = cabeza; // Mover puntero a la nueva cabeza
            }
        }
        
        return valor;
    }
   
    /**
     * MÉTODO EDUCATIVO: Eliminar a la derecha del puntero
     * Usa RefD (Referencia Derecha) y RefI (Referencia Izquierda) para mayor claridad
     */
    public void eliminarDerecha() {
        if (pLD == null || pLD.getRefD() == null) {
            return; // No hay nada que eliminar
        }
        
        clsNodoDoble aEliminar = pLD.getRefD();
        clsNodoDoble siguienteDelEliminado = aEliminar.getRefD();
        
        if (siguienteDelEliminado == null) {
            // Eliminando el último nodo
            pLD.setRefD(null);  // RefD del puntero queda en null
            cola = pLD;
        } else {
            // Eliminando un nodo del medio
            pLD.setRefD(siguienteDelEliminado);         // RefD del puntero apunta al siguiente
            siguienteDelEliminado.setRefI(pLD);         // RefI del siguiente apunta al puntero
        }
    }

    /**
     * MÉTODO EDUCATIVO: Eliminar a la izquierda del puntero
     * Usa RefD (Referencia Derecha) y RefI (Referencia Izquierda) para mayor claridad
     */
    public void eliminarIzquierda() {
        if (pLD == null || pLD.getRefI() == null) {
            return; // No hay nada que eliminar
        }
        
        clsNodoDoble aEliminar = pLD.getRefI();
        clsNodoDoble anteriorDelEliminado = aEliminar.getRefI();
        
        if (anteriorDelEliminado == null) {
            // Eliminando el primer nodo
            pLD.setRefI(null);  // RefI del puntero queda en null
            cabeza = pLD;
        } else {
            // Eliminando un nodo del medio
            anteriorDelEliminado.setRefD(pLD);          // RefD del anterior apunta al puntero
            pLD.setRefI(anteriorDelEliminado);          // RefI del puntero apunta al anterior
        }
    }

    // ==================== NAVEGACIÓN DEL PUNTERO ====================
    
    public void moverPunteroInicio() {
        pLD = cabeza;
    }

    public boolean moverPunteroSiguiente() {
        if (pLD != null && pLD.getRefD() != null) {
            pLD = pLD.getRefD();    // Mover a la referencia derecha
            return true;
        }
        return false;
    }

    public boolean moverPunteroAnterior() {
        if (pLD != null && pLD.getRefI() != null) {
            pLD = pLD.getRefI();    // Mover a la referencia izquierda
            return true;
        }
        return false;
    }

    public void moverPunteroFinal() {
        pLD = cola;
    }

    // ==================== MÉTODOS DE ACCESO ====================
    
    public clsNodoDoble getCabeza() {
        return cabeza;
    }

    public clsNodoDoble getCola() {
        return cola;
    }

    public clsNodoDoble getPunteroActual() {
        return pLD;
    }

    public boolean estaVacia() {
        return cabeza == null;
    }

    public boolean esPunteroNulo() {
        return pLD == null;
    }

    // ==================== MÉTODOS EDUCATIVOS ADICIONALES ====================
    
    public void vaciar() {
        cabeza = cola = pLD = null;
    }

    public int size() {
        int count = 0;
        clsNodoDoble actual = cabeza;
        while (actual != null) {
            count++;
            actual = actual.getRefD();  // Avanzar con RefD
        }
        return count;
    }

    public String recorridoForward() {
        StringBuilder sb = new StringBuilder();
        clsNodoDoble actual = cabeza;
        while (actual != null) {
            sb.append(actual.getDato());
            if (actual.getRefD() != null) {         // RefD para siguiente
                sb.append(" <-> ");
            }
            actual = actual.getRefD();              // Avanzar con RefD
        }
        return sb.toString();
    }

    public String recorridoBackward() {
        StringBuilder sb = new StringBuilder();
        clsNodoDoble actual = cola;
        while (actual != null) {
            sb.append(actual.getDato());
            if (actual.getRefI() != null) {         // RefI para anterior
                sb.append(" <-> ");
            }
            actual = actual.getRefI();              // Retroceder con RefI
        }
        return sb.toString();
    }

    // ==================== MÉTODOS PARA MANTENER COMPATIBILIDAD ====================
    
    public void insertarOrdenado(int dato) {
        // Versión simple: insertar y luego ordenar
        insertarFinal(dato);
        ordenarAscendente();
    }

    public void ordenarAscendente() {
        // Algoritmo burbuja simple para propósitos educativos
        if (cabeza == null || cabeza == cola) return;
        
        boolean intercambio = true;
        while (intercambio) {
            intercambio = false;
            clsNodoDoble actual = cabeza;
            while (actual.getRefD() != null) {                      // RefD para siguiente
                if (actual.getDato() > actual.getRefD().getDato()) {
                    // Intercambiar valores (más simple que intercambiar nodos)
                    int temp = actual.getDato();
                    actual.setDato(actual.getRefD().getDato());
                    actual.getRefD().setDato(temp);
                    intercambio = true;
                }
                actual = actual.getRefD();                          // Avanzar con RefD
            }
        }
    }

    public int[] buscarTodasLasPosiciones(int valor) {
        java.util.ArrayList<Integer> posiciones = new java.util.ArrayList<>();
        clsNodoDoble actual = cabeza;
        int pos = 0;
        
        while (actual != null) {
            if (actual.getDato() == valor) {
                posiciones.add(pos);
            }
            actual = actual.getRefD();  // Avanzar con RefD
            pos++;
        }
        
        return posiciones.stream().mapToInt(i -> i).toArray();
    }

    public int sumarElementos() {
        int suma = 0;
        clsNodoDoble actual = cabeza;
        while (actual != null) {
            suma += actual.getDato();
            actual = actual.getRefD();  // Avanzar con RefD
        }
        return suma;
    }

    // ==================== MÉTODOS DE COMPATIBILIDAD CON INTERFAZ ====================
    
    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Insertar a la derecha usando posición actual del puntero
     */
    public boolean insertarDerecha(int pos, int dato) {
        // Mover puntero a la posición y luego insertar
        if (moverPunteroAPosicion(pos)) {
            insertarDerecha(dato);
            return true;
        }
        return false;
    }

    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Insertar a la izquierda usando posición actual del puntero
     */
    public boolean insertarIzquierda(int pos, int dato) {
        // Mover puntero a la posición y luego insertar
        if (moverPunteroAPosicion(pos)) {
            insertarIzquierda(dato);
            return true;
        }
        return false;
    }

    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Eliminar a la derecha usando posición actual del puntero
     */
    public int eliminarDerecha(int pos) {
        if (moverPunteroAPosicion(pos) && pLD.getRefD() != null) {
            int valor = pLD.getRefD().getDato();    // Obtener dato del nodo derecho
            eliminarDerecha();
            return valor;
        }
        return -1;
    }

    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Eliminar a la izquierda usando posición actual del puntero
     */
    public int eliminarIzquierda(int pos) {
        if (moverPunteroAPosicion(pos) && pLD.getRefI() != null) {
            int valor = pLD.getRefI().getDato();    // Obtener dato del nodo izquierdo
            eliminarIzquierda();
            return valor;
        }
        return -1;
    }

    /**
     * MÉTODO AUXILIAR: Mover puntero interno a una posición específica
     */
    private boolean moverPunteroAPosicion(int pos) {
        if (pos < 0 || cabeza == null) return false;
        
        pLD = cabeza;
        for (int i = 0; i < pos && pLD != null; i++) {
            pLD = pLD.getRefD();    // Usar RefD para navegación
        }
        return pLD != null;
    }

    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Obtener posición numérica del puntero para mostrar en UI
     */
    public int getPosicionPuntero() {
        if (pLD == null) return -1;
        
        clsNodoDoble actual = cabeza;
        int pos = 0;
        while (actual != null && actual != pLD) {
            actual = actual.getRefD();  // Usar RefD para navegación
            pos++;
        }
        return actual == pLD ? pos : -1;
    }

    /**
     * MÉTODO REQUERIDO POR LA INTERFAZ: Permutar (intercambiar) dos nodos por posición
     */
    public boolean permutarNodos(int pos1, int pos2) {
        if (pos1 == pos2) return true;
        
        // Obtener nodos en las posiciones especificadas
        clsNodoDoble nodo1 = null, nodo2 = null;
        
        // Buscar nodo1
        clsNodoDoble actual = cabeza;
        for (int i = 0; i < pos1 && actual != null; i++) {
            actual = actual.getRefD();
        }
        if (actual != null) nodo1 = actual;
        
        // Buscar nodo2
        actual = cabeza;
        for (int i = 0; i < pos2 && actual != null; i++) {
            actual = actual.getRefD();
        }
        if (actual != null) nodo2 = actual;
        
        if (nodo1 == null || nodo2 == null) return false;
        
        // Intercambiar valores (más simple que intercambiar nodos completos)
        int temp = nodo1.getDato();
        nodo1.setDato(nodo2.getDato());
        nodo2.setDato(temp);
        
        return true;
    }

    // ==================== MÉTODOS ADICIONALES PARA COMPATIBILIDAD ====================
    
    public int obtenerValorEnPosicion(int pos) {
        if (pos < 0 || cabeza == null) return -1;
        
        clsNodoDoble actual = cabeza;
        for (int i = 0; i < pos && actual != null; i++) {
            actual = actual.getRefD();  // Usar RefD para navegación
        }
        return actual != null ? actual.getDato() : -1;
    }

    public String obtenerInfoPuntero() {
        if (pLD == null) {
            return "Nulo";
        }
        return String.valueOf(pLD.getDato());
    }
}

