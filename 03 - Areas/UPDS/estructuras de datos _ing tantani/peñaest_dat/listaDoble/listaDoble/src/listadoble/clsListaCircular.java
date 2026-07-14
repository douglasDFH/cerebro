/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package listadoble;

/**
 *
 * @author Brayan Cuenca T
 */
public class clsListaCircular {
    private clsNodoLD pLC; //puntero LISA CIRCULAR

    public clsListaCircular() {
        this.pLC = null;
    }
    
    
    public void insertarDerecha(int dato){
        clsNodoLD nAux = new clsNodoLD(dato);
        if(this.pLC==null){
            this.pLC=nAux;
            this.pLC.setRefD(this.pLC);
            this.pLC.setRefI(this.pLC);
        }else{
            nAux.setRefD(this.pLC.getRefD());
            this.pLC.getRefD().setRefI(nAux);
            this.pLC.setRefD(nAux);
            nAux.setRefI(this.pLC);
        }
    }
    
    public void mostrar(){
        if(this.pLC == null){
            System.out.println("Lista circular vacía");
            return;
        }
        
        System.out.print("Lista Circular: ");
        clsNodoLD actual = this.pLC;
        do {
            System.out.print(actual.getDato() + " -> ");
            actual = actual.getRefD();
        } while (actual != this.pLC);
        System.out.println("(circular)");
    }

}
