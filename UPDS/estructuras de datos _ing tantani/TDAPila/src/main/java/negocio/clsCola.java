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
    
    //constructor por defecto 
    public clsCola(){
        this.ultimo = null; //this permite hacer referencia de manera correcta 
        this.primero = null;
        
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
        }
        return dato;
    }

    public boolean colaVacia(){
        return this.primero == null;
    }

    public void vaciarCola(){
        this.primero = null;
        this.ultimo = null;
    }


}

