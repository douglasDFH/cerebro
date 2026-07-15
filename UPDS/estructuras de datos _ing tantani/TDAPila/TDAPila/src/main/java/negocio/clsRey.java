/*
 * Clase Rey - Puede moverse una casilla en cualquier dirección
 */
package negocio;

public class clsRey extends clsPieza {

    public clsRey(String color, int fila, int columna) {
        super(color, "REY", fila, columna);
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

        // El rey se mueve solo una casilla en cualquier dirección
        int deltaFila = Math.abs(filaDestino - fila);
        int deltaColumna = Math.abs(columnaDestino - columna);

        // Movimiento normal del rey (una casilla)
        if (deltaFila <= 1 && deltaColumna <= 1) {
            return true;
        }

        // Enroque (movimiento especial)
        if (!seHaMovido && deltaFila == 0 && deltaColumna == 2) {
            return verificarEnroque(filaDestino, columnaDestino, tablero);
        }

        return false;
    }

    // Método auxiliar para verificar si el enroque es válido
    private boolean verificarEnroque(int filaDestino, int columnaDestino, clsPieza[][] tablero) {
        // Enroque corto (hacia la derecha)
        if (columnaDestino == columna + 2) {
            clsPieza torre = tablero[fila][7];
            if (torre != null && torre.getTipo().equals("TORRE") && !torre.isSeHaMovido()) {
                // Verificar que las casillas entre el rey y la torre estén vacías
                return tablero[fila][columna + 1] == null && tablero[fila][columna + 2] == null;
            }
        }
        // Enroque largo (hacia la izquierda)
        else if (columnaDestino == columna - 2) {
            clsPieza torre = tablero[fila][0];
            if (torre != null && torre.getTipo().equals("TORRE") && !torre.isSeHaMovido()) {
                // Verificar que las casillas entre el rey y la torre estén vacías
                return tablero[fila][columna - 1] == null &&
                       tablero[fila][columna - 2] == null &&
                       tablero[fila][columna - 3] == null;
            }
        }

        return false;
    }
}
