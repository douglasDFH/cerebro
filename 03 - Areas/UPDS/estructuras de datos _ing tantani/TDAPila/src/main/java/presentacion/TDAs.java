/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 */

package presentacion;
import negocio.clsCola;

/**
 *
 * @author SCPC503
 */
public class TDAs {

    public static void main(String[] args) {
        clsCola objCola = new clsCola();
        objCola.insertar(30);
      
        objCola.insertar(50);
        System.out.println("atendido"+objCola.eliminar());
        objCola.insertar(70);
        objCola.insertar(55);
        objCola.insertar(0);
    }
    
    
}
