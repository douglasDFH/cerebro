/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package negocio;

/**
 *
 * @author SCPC503
 */
public class clsPila {
    private clsNodo cima; 
    private clsNodo punteroActual; // Puntero para navegación y visualización
    
    public clsPila(){
        cima = null;
        punteroActual = null;
    }

    public clsNodo getCima() {
        return cima;
    }

    public void setCima(clsNodo cima) {
        this.cima = cima;
    }
    
    public void insert(int dato){
        clsNodo nAux = new clsNodo(dato);
            if(this.cima == null){
                this.cima = nAux;
                
            }else{
                nAux.setRef(cima);
                cima = nAux;
            }
    }
    public int eliminar(){
        int dato = -1;   
        if(this.cima != null){
            dato = cima.getDato();
            this.cima = this.cima.getRef();
            this.punteroActual = this.cima; // Actualizar el puntero al nuevo nodo en la cima
        }
        return dato;
    }
  
    public boolean pilaVacia(){
        return this.cima == null;
    }

 
    public void vaciarPila(){
        this.cima = null;
        this.punteroActual = null;
    }
    
    // ==================== MÉTODOS DE NAVEGACIÓN DE PUNTERO ====================
    
    /**
     * Mover el puntero al inicio (cima) de la pila
     */
    public void moverPunteroInicio() {
        punteroActual = cima;
    }
    
    /**
     * Mover el puntero al siguiente elemento (hacia abajo en la pila)
     */
    public boolean moverPunteroSiguiente() {
        if (punteroActual != null && punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
            return true;
        }
        return false;
    }
    
    /**
     * Mover el puntero al final de la pila
     */
    public void moverPunteroFinal() {
        if (cima == null) {
            punteroActual = null;
            return;
        }
        punteroActual = cima;
        while (punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
        }
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
        if (punteroActual == null || cima == null) return -1;
        
        clsNodo temp = cima;
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
    
    /**
     * Mover el puntero al nodo anterior
     */
    public void moverPunteroAnterior() {
        if (cima == null || punteroActual == null) {
            punteroActual = null;
            return;
        }
        
        // En pila, el anterior es hacia el fondo
        if (punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
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
    
    /**
     * Verificar si la pila está vacía
     */
    public boolean estaVacia() {
        return cima == null;
    }

    
}
