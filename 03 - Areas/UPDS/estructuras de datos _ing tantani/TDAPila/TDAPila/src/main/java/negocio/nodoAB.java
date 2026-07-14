/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package negocio;

/**
 *
 * @author dell
 */
public class nodoAB {
    private Object dato;
    private nodoAB hijoD, hijoI;
    
    public nodoAB(Object dato){
     this.dato = dato;
     this.hijoD = null;
     this.hijoI = null;
    }

    public Object getDato() {
        return dato;
    }

    public void setDato(Object dato) {
        this.dato = dato;
    }

    public nodoAB getHijoD() {
        return hijoD;
    }

    public void setHijoD(nodoAB hijoD) {
        this.hijoD = hijoD;
    }

    public nodoAB getHijoI() {
        return hijoI;
    }

    public void setHijoI(nodoAB hijoI) {
        this.hijoI = hijoI;
    }
    
}
