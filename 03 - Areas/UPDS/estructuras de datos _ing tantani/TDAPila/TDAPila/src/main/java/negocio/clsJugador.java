/*
 * Clase Jugador - Representa un jugador (Blanco o Negro)
 */
package negocio;

import java.util.ArrayList;

public class clsJugador {
    private String nombre;
    private String color; // "BLANCO" o "NEGRO"
    private ArrayList<clsPieza> piezasCapturadas; // Piezas que ha capturado
    private boolean enJaque;
    private boolean enJaqueMate;
    private boolean enAhogado;

    public clsJugador(String nombre, String color) {
        this.nombre = nombre;
        this.color = color;
        this.piezasCapturadas = new ArrayList<>();
        this.enJaque = false;
        this.enJaqueMate = false;
        this.enAhogado = false;
    }

    // Getters y Setters
    public String getNombre() {
        return nombre;
    }

    public void setNombre(String nombre) {
        this.nombre = nombre;
    }

    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public ArrayList<clsPieza> getPiezasCapturadas() {
        return piezasCapturadas;
    }

    public boolean isEnJaque() {
        return enJaque;
    }

    public void setEnJaque(boolean enJaque) {
        this.enJaque = enJaque;
    }

    public boolean isEnJaqueMate() {
        return enJaqueMate;
    }

    public void setEnJaqueMate(boolean enJaqueMate) {
        this.enJaqueMate = enJaqueMate;
    }

    public boolean isEnAhogado() {
        return enAhogado;
    }

    public void setEnAhogado(boolean enAhogado) {
        this.enAhogado = enAhogado;
    }

    // Métodos de negocio
    public void capturarPieza(clsPieza pieza) {
        piezasCapturadas.add(pieza);
    }

    public int getPuntuacion() {
        int puntuacion = 0;
        for (clsPieza pieza : piezasCapturadas) {
            switch (pieza.getTipo()) {
                case "PEON":
                    puntuacion += 1;
                    break;
                case "CABALLO":
                case "ALFIL":
                    puntuacion += 3;
                    break;
                case "TORRE":
                    puntuacion += 5;
                    break;
                case "REINA":
                    puntuacion += 9;
                    break;
            }
        }
        return puntuacion;
    }
}
