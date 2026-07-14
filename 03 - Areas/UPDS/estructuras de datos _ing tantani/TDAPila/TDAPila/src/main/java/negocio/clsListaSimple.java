package negocio;

public class clsListaSimple {
    private clsNodo cabeza;
    private clsNodo punteroActual; // Puntero para navegación y visualización

    public clsListaSimple(){
        this.cabeza = null;
        this.punteroActual = null;
    }

    public void insertarInicio(int dato){
        clsNodo n = new clsNodo(dato);
        n.setRef(cabeza);
        cabeza = n;
    }

    public void insertarFinal(int dato){
        clsNodo n = new clsNodo(dato);
        if(cabeza == null){
            cabeza = n;
            return;
        }
        clsNodo aux = cabeza;
        while(aux.getRef() != null){
            aux = aux.getRef();
        }
        aux.setRef(n);
    }

    public boolean insertarEnPos(int pos, int dato){
        if(pos < 0) return false;
        if(pos == 0){
            insertarInicio(dato);
            return true;
        }
        clsNodo aux = cabeza;
        int i = 0;
        while(aux != null && i < pos - 1){
            aux = aux.getRef();
            i++;
        }
        if(aux == null) return false;
        clsNodo n = new clsNodo(dato);
        n.setRef(aux.getRef());
        aux.setRef(n);
        return true;
    }

    public int eliminarInicio(){
        if(cabeza == null) return -1;
        int val = cabeza.getDato();
        cabeza = cabeza.getRef();
        // TDA CONSISTENCIA: Actualizar puntero al nuevo inicio
        punteroActual = cabeza;
        return val;
    }

    public int eliminarFinal(){
        if(cabeza == null) return -1;
        if(cabeza.getRef() == null){
            int val = cabeza.getDato();
            cabeza = null;
            // TDA CONSISTENCIA: Lista vacía, puntero a null
            punteroActual = null;
            return val;
        }
        clsNodo aux = cabeza;
        while(aux.getRef().getRef() != null){
            aux = aux.getRef();
        }
        int val = aux.getRef().getDato();
        aux.setRef(null);
        // TDA CONSISTENCIA: Si el puntero apuntaba al nodo eliminado, moverlo al anterior
        if(punteroActual != null && punteroActual.getRef() == null){
            punteroActual = aux;
        }
        return val;
    }

    public int eliminarPorPos(int pos){
        if(pos < 0 || cabeza == null) return -1;
        if(pos == 0) {
            int eliminado = eliminarInicio();
            punteroActual = cabeza; // Actualizar el puntero al nuevo inicio
            return eliminado;
        }
        clsNodo aux = cabeza;
        int i = 0;
        while(aux.getRef() != null && i < pos - 1){
            aux = aux.getRef();
            i++;
        }
        if(aux.getRef() == null) return -1;
        int val = aux.getRef().getDato();
        aux.setRef(aux.getRef().getRef());
        punteroActual = aux.getRef(); // Actualizar el puntero al siguiente nodo
        return val;
    }

    public boolean eliminarPorValor(int valor){
        if(cabeza == null) return false;
        if(cabeza.getDato() == valor){
            cabeza = cabeza.getRef();
            return true;
        }
        clsNodo aux = cabeza;
        while(aux.getRef() != null && aux.getRef().getDato() != valor){
            aux = aux.getRef();
        }
        if(aux.getRef() == null) return false;
        aux.setRef(aux.getRef().getRef());
        return true;
    }

    public boolean buscar(int valor){
        clsNodo aux = cabeza;
        while(aux != null){
            if(aux.getDato() == valor) return true;
            aux = aux.getRef();
        }
        return false;
    }

    /**
     * Busca un valor y retorna todas las posiciones donde se encuentra
     * @param valor Valor a buscar
     * @return Array con todas las posiciones (0-indexadas) donde se encuentra el valor
     */
    public int[] buscarTodasLasPosiciones(int valor) {
        java.util.ArrayList<Integer> posiciones = new java.util.ArrayList<>();
        clsNodo aux = cabeza;
        int posicion = 0;
        
        while(aux != null) {
            if(aux.getDato() == valor) {
                posiciones.add(posicion);
            }
            aux = aux.getRef();
            posicion++;
        }
        
        // Convertir ArrayList a array int[]
        int[] resultado = new int[posiciones.size()];
        for(int i = 0; i < posiciones.size(); i++) {
            resultado[i] = posiciones.get(i);
        }
        return resultado;
    }

    public int size(){
        int cnt = 0;
        clsNodo aux = cabeza;
        while(aux != null){
            cnt++;
            aux = aux.getRef();
        }
        return cnt;
    }

    public void vaciar(){
        cabeza = null;
        punteroActual = null;
    }
    
    // ==================== MÉTODOS DE NAVEGACIÓN DE PUNTERO ====================
    
    /**
     * Mover el puntero al inicio (cabeza) de la lista
     */
    public void moverPunteroInicio() {
        punteroActual = cabeza;
    }
    
    /**
     * Mover el puntero al siguiente elemento
     */
    public boolean moverPunteroSiguiente() {
        if (punteroActual != null && punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
            return true;
        }
        return false;
    }
    
    /**
     * Mover el puntero al final de la lista
     */
    public void moverPunteroFinal() {
        if (cabeza == null) {
            punteroActual = null;
            return;
        }
        punteroActual = cabeza;
        while (punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
        }
    }
    
    /**
     * Mover el puntero a una posición específica
     */
    public boolean moverPunteroAPosicion(int pos) {
        if (pos < 0 || cabeza == null) {
            punteroActual = null;
            return false;
        }
        
        punteroActual = cabeza;
        int i = 0;
        
        while (punteroActual != null && i < pos) {
            punteroActual = punteroActual.getRef();
            i++;
        }
        
        return punteroActual != null;
    }
    
    /**
     * Obtener el nodo donde está el puntero actual
     */
    public clsNodo getPunteroActual() {
        return punteroActual;
    }
    
    /**
     * Obtener la posición del puntero actual (0-indexada)
     */
    public int getPosicionPuntero() {
        if (punteroActual == null || cabeza == null) return -1;
        
        clsNodo temp = cabeza;
        int posicion = 0;
        
        while (temp != null) {
            if (temp == punteroActual) return posicion;
            temp = temp.getRef();
            posicion++;
        }
        return -1;
    }
    
    /**
     * Verificar si el puntero es nulo
     */
    public boolean esPunteroNulo() {
        return punteroActual == null;
    }

    @Override
    public String toString(){
        StringBuilder sb = new StringBuilder();
        sb.append("[");
        clsNodo aux = cabeza;
        while(aux != null){
            sb.append(aux.getDato());
            if(aux.getRef() != null) sb.append(",");
            aux = aux.getRef();
        }
        sb.append("]");
        return sb.toString();
    }

    // Getter para permitir dibujo/recorrido desde la GUI
    public clsNodo getCabeza(){
        return this.cabeza;
    }

    // ==================== MÉTODOS ADICIONALES TDA ====================

    /**
     * Insertar a la derecha de una posición específica (después del puntero)
     * @param pos Posición después de la cual insertar (0-indexada)
     * @param dato Valor a insertar
     * @return true si se insertó correctamente, false si la posición no existe
     */
    public boolean insertarDerecha(int pos, int dato) {
        if (pos < 0 || cabeza == null) return false;
        
        clsNodo aux = cabeza;
        int i = 0;
        
        // Avanzar hasta la posición indicada
        while (aux != null && i < pos) {
            aux = aux.getRef();
            i++;
        }
        
        if (aux == null) return false; // Posición fuera de rango
        
        // Insertar después del nodo actual (a la derecha)
        clsNodo nuevo = new clsNodo(dato);
        nuevo.setRef(aux.getRef());
        aux.setRef(nuevo);
        return true;
    }

    /**
     * Insertar manteniendo orden ascendente en la lista
     * @param dato Valor a insertar ordenadamente
     */
    public void insertarOrdenado(int dato) {
        clsNodo nuevo = new clsNodo(dato);
        
        // Lista vacía o insertar al inicio
        if (cabeza == null || cabeza.getDato() > dato) {
            nuevo.setRef(cabeza);
            cabeza = nuevo;
            return;
        }
        
        // Buscar la posición correcta
        clsNodo aux = cabeza;
        while (aux.getRef() != null && aux.getRef().getDato() < dato) {
            aux = aux.getRef();
        }
        
        // Insertar en la posición encontrada
        nuevo.setRef(aux.getRef());
        aux.setRef(nuevo);
    }

    /**
     * Eliminar el nodo que está a la derecha de una posición específica
     * @param pos Posición del nodo cuyo siguiente será eliminado
     * @return Valor del nodo eliminado, -1 si no se pudo eliminar
     */
    public int eliminarDerecha(int pos) {
        if (pos < 0 || cabeza == null) return -1;
        
        clsNodo aux = cabeza;
        int i = 0;
        
        // Avanzar hasta la posición indicada
        while (aux != null && i < pos) {
            aux = aux.getRef();
            i++;
        }
        
        // Verificar que existe el nodo y que tiene un siguiente
        if (aux == null || aux.getRef() == null) return -1;
        
        // Eliminar el nodo siguiente (derecha)
        int valor = aux.getRef().getDato();
        aux.setRef(aux.getRef().getRef());
        return valor;
    }

    /**
     * Calcular la suma de todos los elementos de la lista
     * @return Suma total de los elementos
     */
    public int sumarElementos() {
        int suma = 0;
        clsNodo aux = cabeza;
        
        while (aux != null) {
            suma += aux.getDato();
            aux = aux.getRef();
        }
        
        return suma;
    }

    /**
     * Ordenar la lista en orden ascendente usando algoritmo burbuja
     */
    public void ordenarAscendente() {
        if (cabeza == null || cabeza.getRef() == null) return;
        
        boolean intercambio;
        do {
            intercambio = false;
            clsNodo actual = cabeza;
            
            while (actual.getRef() != null) {
                if (actual.getDato() > actual.getRef().getDato()) {
                    // Intercambiar valores
                    int temp = actual.getDato();
                    actual.setDato(actual.getRef().getDato());
                    actual.getRef().setDato(temp);
                    intercambio = true;
                }
                actual = actual.getRef();
            }
        } while (intercambio);
    }

    /**
     * Ordenar la lista por intercambio de referencias (no valores)
     * Implementación de ordenamiento burbuja manipulando punteros
     */
    public void ordenarPorReferencia() {
        if (cabeza == null || cabeza.getRef() == null) return;
        
        boolean intercambio;
        do {
            intercambio = false;
            clsNodo prev = null;
            clsNodo actual = cabeza;
            clsNodo siguiente = cabeza.getRef();
            
            while (siguiente != null) {
                if (actual.getDato() > siguiente.getDato()) {
                    // Intercambiar nodos por referencia
                    actual.setRef(siguiente.getRef());
                    siguiente.setRef(actual);
                    
                    if (prev == null) {
                        cabeza = siguiente; // Nuevo inicio
                    } else {
                        prev.setRef(siguiente);
                    }
                    
                    // Actualizar referencias para siguiente iteración
                    prev = siguiente;
                    siguiente = actual.getRef();
                    intercambio = true;
                } else {
                    // Avanzar normalmente
                    prev = actual;
                    actual = siguiente;
                    siguiente = siguiente.getRef();
                }
            }
        } while (intercambio);
    }

    /**
     * Obtener el valor en una posición específica (método auxiliar)
     * @param pos Posición a consultar (0-indexada)
     * @return Valor en la posición, -1 si no existe
     */
    public int obtenerEnPosicion(int pos) {
        if (pos < 0 || cabeza == null) return -1;
        
        clsNodo aux = cabeza;
        int i = 0;
        
        while (aux != null && i < pos) {
            aux = aux.getRef();
            i++;
        }
        
        return (aux != null) ? aux.getDato() : -1;
    }

    /**
     * Verificar si la lista está vacía
     * @return true si está vacía, false en caso contrario
     */
    public boolean estaVacia() {
        return cabeza == null;
    }

    /**
     * Verificar si la lista está ordenada ascendentemente
     * @return true si está ordenada, false en caso contrario
     */
    public boolean estaOrdenada() {
        if (cabeza == null || cabeza.getRef() == null) return true;
        
        clsNodo aux = cabeza;
        while (aux.getRef() != null) {
            if (aux.getDato() > aux.getRef().getDato()) {
                return false;
            }
            aux = aux.getRef();
        }
        return true;
    }
    
    /**
     * Mover el puntero al nodo anterior
     */
    public void moverPunteroAnterior() {
        if (cabeza == null || punteroActual == null) {
            punteroActual = null;
            return;
        }
        
        // Si estamos en la cabeza, no hay anterior
        if (punteroActual == cabeza) {
            return;
        }
        
        // Buscar el nodo anterior al actual
        clsNodo temp = cabeza;
        while (temp != null && temp.getRef() != punteroActual) {
            temp = temp.getRef();
        }
        
        if (temp != null) {
            punteroActual = temp;
        }
    }
    
    /**
     * Obtener información del puntero actual
     */
    public String obtenerInfoPuntero() {
        if (punteroActual == null) {
            return "Nulo";
        }
        int posicion = getPosicionPuntero();
        return "Pos " + posicion + " (Valor: " + punteroActual.getDato() + ")";
    }
}
