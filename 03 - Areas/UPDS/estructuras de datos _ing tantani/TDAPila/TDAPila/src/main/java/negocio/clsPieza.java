/*
 * Clase base abstracta para todas las piezas de ajedrez
 * Siguiendo principios TDA (Tipo de Dato Abstracto)
 */
package negocio;

public abstract class clsPieza {
    protected String color; // "BLANCO" o "NEGRO"
    protected String tipo; // "REY", "REINA", "TORRE", "ALFIL", "CABALLO", "PEON"
    protected int fila;
    protected int columna;
    protected boolean seHaMovido; // Para enroque y movimiento especial de peones

    // Constructor
    public clsPieza(String color, String tipo, int fila, int columna) {
        this.color = color;
        this.tipo = tipo;
        this.fila = fila;
        this.columna = columna;
        this.seHaMovido = false;
    }

    // Getters y Setters
    public String getColor() {
        return color;
    }

    public void setColor(String color) {
        this.color = color;
    }

    public String getTipo() {
        return tipo;
    }

    public void setTipo(String tipo) {
        this.tipo = tipo;
    }

    public int getFila() {
        return fila;
    }

    public void setFila(int fila) {
        this.fila = fila;
    }

    public int getColumna() {
        return columna;
    }

    public void setColumna(int columna) {
        this.columna = columna;
    }

    public boolean isSeHaMovido() {
        return seHaMovido;
    }

    public void setSeHaMovido(boolean seHaMovido) {
        this.seHaMovido = seHaMovido;
    }

    // Método abstracto que cada pieza debe implementar
    // Verifica si el movimiento es válido según las reglas de la pieza
    public abstract boolean esMovimientoValido(int filaDestino, int columnaDestino, clsPieza[][] tablero);

    // Método para obtener símbolo de la pieza
    public String getSimbolo() {
        String simbolo = "";
        switch(tipo) {
            case "REY":
                simbolo = color.equals("BLANCO") ? "♔" : "♚";
                break;
            case "REINA":
                simbolo = color.equals("BLANCO") ? "♕" : "♛";
                break;
            case "TORRE":
                simbolo = color.equals("BLANCO") ? "♖" : "♜";
                break;
            case "ALFIL":
                simbolo = color.equals("BLANCO") ? "♗" : "♝";
                break;
            case "CABALLO":
                simbolo = color.equals("BLANCO") ? "♘" : "♞";
                break;
            case "PEON":
                simbolo = color.equals("BLANCO") ? "♙" : "♟";
                break;
        }
        return simbolo;
    }

    // Método auxiliar para verificar si hay camino libre (para torres, alfiles, reinas)
    protected boolean caminoLibre(int filaDestino, int columnaDestino, clsPieza[][] tablero) {
        int deltaFila = Integer.signum(filaDestino - fila);
        int deltaColumna = Integer.signum(columnaDestino - columna);

        int filaActual = fila + deltaFila;
        int columnaActual = columna + deltaColumna;

        while (filaActual != filaDestino || columnaActual != columnaDestino) {
            if (tablero[filaActual][columnaActual] != null) {
                return false; // Hay una pieza en el camino
            }
            filaActual += deltaFila;
            columnaActual += deltaColumna;
        }

        return true;
    }
}
