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
    public clsPila(){
        cima = null;
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
        }
        return dato;
    }
  
    public boolean pilaVacia(){
        return this.cima == null;
    }

 
    public void vaciarPila(){
        this.cima = null;
    }

    
}
