/*
 * Clase Caballo - Se mueve en forma de "L" (2 casillas en una dirección, 1 en perpendicular)
 * Es la única pieza que puede saltar sobre otras
 */
package negocio;

public class clsCaballo extends clsPieza {

    public clsCaballo(String color, int fila, int columna) {
        super(color, "CABALLO", fila, columna);
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

        // El caballo se mueve en forma de "L"
        int deltaFila = Math.abs(filaDestino - fila);
        int deltaColumna = Math.abs(columnaDestino - columna);

        // Dos casos válidos: (2,1) o (1,2)
        return (deltaFila == 2 && deltaColumna == 1) || (deltaFila == 1 && deltaColumna == 2);
    }
}
