/*
 * Clase Reina - Combina movimientos de Torre y Alfil
 * Se mueve en líneas rectas (horizontal, vertical, diagonal)
 */
package negocio;

public class clsReina extends clsPieza {

    public clsReina(String color, int fila, int columna) {
        super(color, "REINA", fila, columna);
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

        // La reina se mueve como torre o como alfil
        int deltaFila = Math.abs(filaDestino - fila);
        int deltaColumna = Math.abs(columnaDestino - columna);

        // Movimiento vertical u horizontal (como torre)
        if (fila == filaDestino || columna == columnaDestino) {
            return caminoLibre(filaDestino, columnaDestino, tablero);
        }

        // Movimiento diagonal (como alfil)
        if (deltaFila == deltaColumna) {
            return caminoLibre(filaDestino, columnaDestino, tablero);
        }

        return false;
    }
}
