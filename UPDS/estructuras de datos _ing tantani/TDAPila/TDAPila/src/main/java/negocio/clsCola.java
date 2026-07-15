/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */

/**
 *
 * @author SCPC503
 */
package negocio;

public class clsCola {
    private clsNodo primero,ultimo;  // private no permite acceder otras clases de manera externa pero si de manera local
    private clsNodo punteroActual; // Puntero para navegación y visualización
    
    //constructor por defecto
    public clsCola(){
        this.ultimo = null; //this permite hacer referencia de manera correcta 
        this.primero = null;
        this.punteroActual = null;
    }
    //constructor por parametro
    public clsCola(clsNodo primero, clsNodo ultimo){
        this.ultimo = ultimo;
        this.primero = primero;
    
    }
    public void insertar(int dato){
        clsNodo nAux = new clsNodo(dato);
        if(this.ultimo == null && this.primero == null){
            this.ultimo = nAux;
            this.primero = nAux;
                  
        }else{
            this.ultimo.setRef(nAux);
            this.ultimo = nAux;
        }
    }
    
    public int eliminar(){
        int dato = -1;   
        if(this.primero != null){
            dato = primero.getDato();
            this.primero = this.primero.getRef();
            if (this.primero == null) {  // Si queda vacía
                this.ultimo = null;
            }
            this.punteroActual = this.primero; // Actualizar el puntero al nuevo nodo en la cabeza
        }
        return dato;
    }

    public boolean colaVacia(){
        return this.primero == null;
    }

    public void vaciarCola(){
        this.primero = null;
        this.ultimo = null;
        this.punteroActual = null;
    }

    // Getters para uso en la interfaz (visualización)
    public clsNodo getPrimero() {
        return this.primero;
    }

    public clsNodo getUltimo() {
        return this.ultimo;
    }
    
    // ==================== MÉTODOS DE NAVEGACIÓN DE PUNTERO ====================
    
    /**
     * Mover el puntero al inicio (primero) de la cola
     */
    public void moverPunteroInicio() {
        punteroActual = primero;
    }
    
    /**
     * Mover el puntero al siguiente elemento en la cola
     */
    public boolean moverPunteroSiguiente() {
        if (punteroActual != null && punteroActual.getRef() != null) {
            punteroActual = punteroActual.getRef();
            return true;
        }
        return false;
    }
    
    /**
     * Mover el puntero al final (último) de la cola
     */
    public void moverPunteroFinal() {
        punteroActual = ultimo;
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
        if (punteroActual == null || primero == null) return -1;
        
        clsNodo temp = primero;
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
        if (primero == null || punteroActual == null) {
            punteroActual = null;
            return;
        }
        
        // Si estamos en el primero, no hay anterior
        if (punteroActual == primero) {
            return;
        }
        
        // Buscar el nodo anterior al actual
        clsNodo temp = primero;
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
    
    /**
     * Verificar si la cola está vacía
     */
    public boolean estaVacia() {
        return primero == null;
    }

}

