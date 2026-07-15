/*
 * Clase Torre - Se mueve en líneas rectas (horizontal y vertical)
 */
package negocio;

public class clsTorre extends clsPieza {

    public clsTorre(String color, int fila, int columna) {
        super(color, "TORRE", fila, columna);
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

        // La torre se mueve solo en línea recta (horizontal o vertical)
        if (fila != filaDestino && columna != columnaDestino) {
            return false; // No es movimiento horizontal ni vertical
        }

        // Verificar que el camino esté libre
        return caminoLibre(filaDestino, columnaDestino, tablero);
    }
}
