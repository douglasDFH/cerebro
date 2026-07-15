/*
 * Clase Alfil - Se mueve en diagonal
 */
package negocio;

public class clsAlfil extends clsPieza {

    public clsAlfil(String color, int fila, int columna) {
        super(color, "ALFIL", fila, columna);
    }

    @Override
    public boolean esMovimientoValido(int filaDestino, int columnaDestino, clsPieza[][] tablero) {
        // Verificar límites del tablero
        if (filaDestino < 0 || filaDestino > 7 || columnaDestino < 0 || columnaDestino > 7) {
            return false;
        }

        // Verificar que no se mueva a una casilla ocupada por pieza del mismo color
        if (tablero[filaDestino][columnaDestino] != null) {
            if (tablero[filaDestino][columnaDestino].getColor().equals(this.color)) {
                return false;
            }
        }

        // El alfil se mueve solo en diagonal
        int deltaFila = Math.abs(filaDestino - fila);
        int deltaColumna = Math.abs(columnaDestino - columna);

        if (deltaFila != deltaColumna) {
            return false; // No es movimiento diagonal
        }

        // Verificar que el camino esté libre
        return caminoLibre(filaDestino, columnaDestino, tablero);
    }
}
