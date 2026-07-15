/*
 * Clase Peón - Movimiento más complejo: avanza 1 (o 2 desde inicio),
 * captura en diagonal, y tiene promoción
 */
package negocio;

public class clsPeon extends clsPieza {

    public clsPeon(String color, int fila, int columna) {
        super(color, "PEON", fila, columna);
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

        // Dirección del movimiento según el color
        int direccion = color.equals("BLANCO") ? -1 : 1; // Blancas suben (-1), negras bajan (+1)

        int deltaFila = filaDestino - fila;
        int deltaColumna = Math.abs(columnaDestino - columna);

        // Movimiento hacia adelante (1 casilla)
        if (deltaFila == direccion && deltaColumna == 0) {
            return tablero[filaDestino][columnaDestino] == null; // Debe estar vacía
        }

        // Movimiento inicial (2 casillas desde posición inicial)
        if (!seHaMovido && deltaFila == 2 * direccion && deltaColumna == 0) {
            // Verificar que ambas casillas estén vacías
            return tablero[fila + direccion][columna] == null &&
                   tablero[filaDestino][columnaDestino] == null;
        }

        // Captura en diagonal
        if (deltaFila == direccion && deltaColumna == 1) {
            return tablero[filaDestino][columnaDestino] != null; // Debe haber pieza enemiga
        }

        return false;
    }

    // Método para verificar si el peón puede ser promovido
    public boolean puedePromoverse() {
        if (color.equals("BLANCO") && fila == 0) {
            return true;
        }
        if (color.equals("NEGRO") && fila == 7) {
            return true;
        }
        return false;
    }
}
