/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package negocio;

/**
 *
 * @author SCPC503
 */
public class clsNodo {
    private int dato;
    private clsNodo ref;
    public clsNodo(){
        dato = 0;
        ref = null;
    }
    public clsNodo(int dato){
       this.dato = dato; 
       this.ref = null;
    }

    public int getDato() {
        return dato;
    }

    public void setDato(int dato) {
        this.dato = dato;
    }

    public clsNodo getRef() {
        return ref;
    }

    public void setRef(clsNodo ref) {
        this.ref = ref;
    }
    
}
