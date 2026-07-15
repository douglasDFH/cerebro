/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package listadoble;

/**
 *
 * @author Brayan Cuenca T
 */
public class clsNodoLD {
    private int dato;
    private clsNodoLD refI, refD;

    public clsNodoLD(int dato) {
        this.dato = dato;
        this.refI = null;
        this.refD = null;
    }
    
    //geter y seter

    public int getDato() {
        return dato;
    }

    public void setDato(int dato) {
        this.dato = dato;
    }

    public clsNodoLD getRefI() {
        return refI;
    }

    public void setRefI(clsNodoLD refI) {
        this.refI = refI;
    }

    public clsNodoLD getRefD() {
        return refD;
    }

    public void setRefD(clsNodoLD refD) {
        this.refD = refD;
    }
}
