/*
 * Clase Casilla - Representa cada casilla del tablero de ajedrez
 */
package negocio;

public class clsCasilla {
    private int fila;
    private int columna;
    private String color; // "BLANCO" o "NEGRO" (color de la casilla)
    private clsPieza pieza; // Pieza que ocupa la casilla (null si está vacía)

    public clsCasilla(int fila, int columna) {
        this.fila = fila;
        this.columna = columna;
        this.pieza = null;
        // Color de casilla: patrón de tablero de ajedrez
        this.color = (fila + columna) % 2 == 0 ? "NEGRO" : "BLANCO";
    }

    // Getters y Setters
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

    public String getColor() {
        return color;
    }

    public clsPieza getPieza() {
        return pieza;
    }

    public void setPieza(clsPieza pieza) {
        this.pieza = pieza;
    }

    // Métodos auxiliares
    public boolean estaOcupada() {
        return pieza != null;
    }

    public void limpiar() {
        this.pieza = null;
    }

    // Obtener notación algebraica de la casilla (ej: "e4", "a1")
    public String getNotacionAlgebraica() {
        char columnaLetra = (char) ('a' + columna);
        int filaNumero = 8 - fila; // Las filas en ajedrez van de 1-8, invertidas
        return "" + columnaLetra + filaNumero;
    }
}
