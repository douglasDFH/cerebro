package negocio;

import java.awt.Color;

/**
 * Nodo Doble para listas doblemente enlazadas
 * Soporta tanto datos numéricos como elementos de ruleta (nombre + color)
 */
public class clsNodoDoble {
    // Atributos para uso general (números)
    private int dato;

    // Atributos para ruleta (nombre y color)
    private String nombre;
    private Color color;
    private int cantidad; // Cantidad disponible del premio

    // Referencias a nodos vecinos
    private clsNodoDoble prev;
    private clsNodoDoble next;

    // Constructor vacío
    public clsNodoDoble(){
        this.dato = 0;
        this.nombre = null;
        this.color = null;
        this.cantidad = 1;
        this.prev = null;
        this.next = null;
    }

    // Constructor para datos numéricos
    public clsNodoDoble(int dato){
        this.dato = dato;
        this.nombre = null;
        this.color = null;
        this.cantidad = 1;
        this.prev = null;
        this.next = null;
    }

    // Constructor para elementos de ruleta (nombre + color)
    public clsNodoDoble(String nombre, Color color){
        this.dato = 0;
        this.nombre = nombre;
        this.color = color;
        this.cantidad = 1;
        this.prev = null;
        this.next = null;
    }

    // Constructor para elementos de ruleta con cantidad (nombre + color + cantidad)
    public clsNodoDoble(String nombre, Color color, int cantidad){
        this.dato = 0;
        this.nombre = nombre;
        this.color = color;
        this.cantidad = cantidad;
        this.prev = null;
        this.next = null;
    }

    // Getters y Setters para dato numérico
    public int getDato() {
        return dato;
    }

    public void setDato(int dato) {
        this.dato = dato;
    }

    // Getters y Setters para elementos de ruleta
    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public Color getColor() {
        return color;
    }

    public void setColor(Color color) {
        this.color = color;
    }

    public int getCantidad() {
        return cantidad;
    }

    public void setCantidad(int cantidad) {
        this.cantidad = cantidad;
    }

    // Métodos con nombres educativos más claros
    public clsNodoDoble getRefI() { // Referencia Izquierda
        return prev;
    }

    public void setRefI(clsNodoDoble refI) {
        this.prev = refI;
    }

    public clsNodoDoble getRefD() { // Referencia Derecha
        return next;
    }

    public void setRefD(clsNodoDoble refD) {
        this.next = refD;
    }

    // Mantener métodos antiguos para compatibilidad
    public clsNodoDoble getPrev() {
        return prev;
    }

    public void setPrev(clsNodoDoble prev) {
        this.prev = prev;
    }

    public clsNodoDoble getNext() {
        return next;
    }

    public void setNext(clsNodoDoble next) {
        this.next = next;
    }
}
