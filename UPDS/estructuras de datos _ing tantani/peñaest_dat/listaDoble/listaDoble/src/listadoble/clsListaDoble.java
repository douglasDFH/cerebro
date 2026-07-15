/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package listadoble;

/**
 *
 * @author Brayan Cuenca T
 */
public class clsListaDoble {
    private clsNodoLD pLD; //pLD (puntero LISTA DOBLE)
    
    public clsListaDoble(){
        this.pLD = null;
    }
    
    //insetar primero izquierda
    public void insertarDerecha(int dato){
        clsNodoLD nAux = new clsNodoLD(dato);
        if(this.pLD == null){
            this.pLD = nAux;
        } else{
            if(this.pLD.getRefD() == null){
                this.pLD.setRefD(nAux);
                nAux.setRefD(this.pLD);
            }else{
                nAux.setRefD(this.pLD.getRefD());
                this.pLD.getRefD().setRefI(nAux);
                this.pLD.setRefD(nAux);
                nAux.setRefI(this.pLD);
                
            }
        }
    }
    
    public void mostrar(){
        if(this.pLD == null){
            System.out.println("Lista doble vacía");
            return;
        }
        
        System.out.print("Lista Doble: null <- ");
        clsNodoLD actual = this.pLD;
        while(actual != null){
            System.out.print(actual.getDato());
            if(actual.getRefD() != null){
                System.out.print(" <-> ");
            }
            actual = actual.getRefD();
        }
        System.out.println(" -> null");
    }
}
