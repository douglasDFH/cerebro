/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Main.java to edit this template
 */
package listadoble;
import listadoble.clsListaCircular;
import listadoble.ListaDoble;
/**
 *
 * @author Brayan Cuenca T
 */
public class ListaDoble {

    /**
     * @param args the command line arguments
     */
    public static void main(String[] args) {
        System.out.println("=== DEMOSTRACIÓN DE LISTAS ===");
        
       //LISTA CIRCULAR
        System.out.println("\n--- LISTA CIRCULAR ---");
        clsListaCircular objLC = new clsListaCircular();
        System.out.println("Insertando elementos: 20, 50, 80, 30, 40, 20");
        objLC.insertarDerecha(20);
        objLC.insertarDerecha(50);
        objLC.insertarDerecha(80);
        objLC.insertarDerecha(30);
        objLC.insertarDerecha(40);
        objLC.insertarDerecha(20);
        objLC.mostrar();
       
        //LISTA DOBLE
        System.out.println("\n--- LISTA DOBLE ---");
        clsListaDoble objLD = new clsListaDoble();
        System.out.println("Insertando elementos: 20, 50, 80, 30, 40, 20");
        objLD.insertarDerecha(20);
        objLD.insertarDerecha(50);
        objLD.insertarDerecha(80);
        objLD.insertarDerecha(30);
        objLD.insertarDerecha(40);
        objLD.insertarDerecha(20);
        objLD.mostrar();
        
        System.out.println("\n=== FIN DE LA DEMOSTRACIÓN ===");
    }
}
