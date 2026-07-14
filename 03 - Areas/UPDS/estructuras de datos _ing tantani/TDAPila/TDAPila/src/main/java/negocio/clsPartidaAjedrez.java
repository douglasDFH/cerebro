/*
 * Clase PartidaAjedrez - Lógica principal del juego
 * Maneja el flujo de la partida, turnos, reglas completas
 */
package negocio;

import java.util.ArrayList;

public class clsPartidaAjedrez {
    private clsTablero tablero;
    private clsJugador jugadorBlanco;
    private clsJugador jugadorNegro;
    private clsJugador jugadorActual;
    private ArrayList<clsMovimiento> historialMovimientos;
    private boolean partidaTerminada;
    private String ganador; // "BLANCO", "NEGRO", "EMPATE", null

    // Constructor
    public clsPartidaAjedrez(String nombreBlanco, String nombreNegro) {
        this.tablero = new clsTablero();
        this.jugadorBlanco = new clsJugador(nombreBlanco, "BLANCO");
        this.jugadorNegro = new clsJugador(nombreNegro, "NEGRO");
        this.jugadorActual = jugadorBlanco; // Las blancas siempre empiezan
        this.historialMovimientos = new ArrayList<>();
        this.partidaTerminada = false;
        this.ganador = null;
    }

    // Getters
    public clsTablero getTablero() {
        return tablero;
    }

    public clsJugador getJugadorBlanco() {
        return jugadorBlanco;
    }

    public clsJugador getJugadorNegro() {
        return jugadorNegro;
    }

    public clsJugador getJugadorActual() {
        return jugadorActual;
    }

    public ArrayList<clsMovimiento> getHistorialMovimientos() {
        return historialMovimientos;
    }

    public boolean isPartidaTerminada() {
        return partidaTerminada;
    }

    public String getGanador() {
        return ganador;
    }

    // Método principal para realizar un movimiento
    public boolean realizarMovimiento(int filaOrigen, int columnaOrigen,
                                      int filaDestino, int columnaDestino) {
        if (partidaTerminada) {
            return false; // La partida ya terminó
        }

        clsPieza pieza = tablero.getPieza(filaOrigen, columnaOrigen);

        if (pieza == null) {
            return false; // No hay pieza en el origen
        }

        // Verificar que sea el turno del jugador correcto
        if (!pieza.getColor().equals(jugadorActual.getColor())) {
            return false; // No es el turno de este jugador
        }

        // Verificar que el movimiento sea válido
        if (!pieza.esMovimientoValido(filaDestino, columnaDestino, tablero.getTablero())) {
            return false;
        }

        // Simular el movimiento para verificar que no deja al rey en jaque
        clsPieza piezaDestino = tablero.getPieza(filaDestino, columnaDestino);
        clsPieza[][] tableroTemp = tablero.getTablero();
        int filaOriginal = pieza.getFila();
        int columnaOriginal = pieza.getColumna();

        tableroTemp[filaDestino][columnaDestino] = pieza;
        tableroTemp[filaOrigen][columnaOrigen] = null;
        pieza.setFila(filaDestino);
        pieza.setColumna(columnaDestino);

        // Verificar si el movimiento deja al propio rey en jaque
        boolean quedaEnJaque = tablero.estEnJaque(jugadorActual.getColor());

        // Restaurar el tablero
        tableroTemp[filaOrigen][columnaOrigen] = pieza;
        tableroTemp[filaDestino][columnaDestino] = piezaDestino;
        pieza.setFila(filaOriginal);
        pieza.setColumna(columnaOriginal);

        if (quedaEnJaque) {
            return false; // El movimiento deja al rey en jaque (ilegal)
        }

        // Realizar el movimiento definitivamente
        clsMovimiento movimiento = new clsMovimiento(pieza, filaOrigen, columnaOrigen,
                                                     filaDestino, columnaDestino);

        // Verificar captura
        if (piezaDestino != null) {
            movimiento.setPiezaCapturada(piezaDestino);
            jugadorActual.capturarPieza(piezaDestino);
        }

        // Verificar enroque
        if (pieza.getTipo().equals("REY") && Math.abs(columnaDestino - columnaOrigen) == 2) {
            movimiento.setEsEnroque(true);
        }

        // Realizar el movimiento en el tablero
        tablero.moverPieza(filaOrigen, columnaOrigen, filaDestino, columnaDestino);

        // Verificar promoción de peón
        if (pieza.getTipo().equals("PEON")) {
            clsPeon peon = (clsPeon) pieza;
            if (peon.puedePromoverse()) {
                movimiento.setEsPromocion(true);
                // Por defecto promover a reina (puede ser modificado desde la interfaz)
                tablero.promoverPeon(filaDestino, columnaDestino, "REINA");
                movimiento.setPiezaPromocionada("REINA");
            }
        }

        // Agregar movimiento al historial
        historialMovimientos.add(movimiento);

        // Cambiar turno
        cambiarTurno();

        // Verificar estado del juego
        verificarEstadoJuego();

        return true;
    }

    // Cambiar el turno al otro jugador
    private void cambiarTurno() {
        jugadorActual = (jugadorActual == jugadorBlanco) ? jugadorNegro : jugadorBlanco;
    }

    // Verificar el estado del juego (jaque, jaque mate, ahogado)
    private void verificarEstadoJuego() {
        String colorActual = jugadorActual.getColor();

        // Verificar jaque
        if (tablero.estEnJaque(colorActual)) {
            jugadorActual.setEnJaque(true);

            // Verificar jaque mate
            if (tablero.esJaqueMate(colorActual)) {
                jugadorActual.setEnJaqueMate(true);
                partidaTerminada = true;
                ganador = colorActual.equals("BLANCO") ? "NEGRO" : "BLANCO";
            }
        } else {
            jugadorActual.setEnJaque(false);

            // Verificar ahogado (stalemate)
            if (tablero.esAhogado(colorActual)) {
                jugadorActual.setEnAhogado(true);
                partidaTerminada = true;
                ganador = "EMPATE";
            }
        }
    }

    // Obtener todos los movimientos posibles para una pieza
    public ArrayList<int[]> getMovimientosPosibles(int fila, int columna) {
        ArrayList<int[]> movimientos = new ArrayList<>();
        clsPieza pieza = tablero.getPieza(fila, columna);

        if (pieza == null || !pieza.getColor().equals(jugadorActual.getColor())) {
            return movimientos; // No es una pieza válida o no es su turno
        }

        // Probar todos los movimientos posibles
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                if (pieza.esMovimientoValido(i, j, tablero.getTablero())) {
                    // Simular el movimiento para verificar que no deja al rey en jaque
                    clsPieza piezaDestino = tablero.getPieza(i, j);
                    clsPieza[][] tableroTemp = tablero.getTablero();
                    int filaOriginal = pieza.getFila();
                    int columnaOriginal = pieza.getColumna();

                    tableroTemp[i][j] = pieza;
                    tableroTemp[fila][columna] = null;
                    pieza.setFila(i);
                    pieza.setColumna(j);

                    boolean quedaEnJaque = tablero.estEnJaque(jugadorActual.getColor());

                    // Restaurar
                    tableroTemp[fila][columna] = pieza;
                    tableroTemp[i][j] = piezaDestino;
                    pieza.setFila(filaOriginal);
                    pieza.setColumna(columnaOriginal);

                    if (!quedaEnJaque) {
                        movimientos.add(new int[]{i, j});
                    }
                }
            }
        }

        return movimientos;
    }

    // Reiniciar la partida
    public void reiniciarPartida() {
        this.tablero = new clsTablero();
        this.jugadorBlanco.setEnJaque(false);
        this.jugadorBlanco.setEnJaqueMate(false);
        this.jugadorBlanco.setEnAhogado(false);
        this.jugadorBlanco.getPiezasCapturadas().clear();
        this.jugadorNegro.setEnJaque(false);
        this.jugadorNegro.setEnJaqueMate(false);
        this.jugadorNegro.setEnAhogado(false);
        this.jugadorNegro.getPiezasCapturadas().clear();
        this.jugadorActual = jugadorBlanco;
        this.historialMovimientos.clear();
        this.partidaTerminada = false;
        this.ganador = null;
    }

    // Obtener el estado de la partida como texto
    public String getEstadoPartida() {
        if (partidaTerminada) {
            if (ganador.equals("EMPATE")) {
                return "Partida terminada: Empate por ahogado";
            } else {
                return "Partida terminada: Ganan las " + ganador;
            }
        }

        if (jugadorActual.isEnJaque()) {
            return "Turno de " + jugadorActual.getNombre() + " (EN JAQUE)";
        }

        return "Turno de " + jugadorActual.getNombre() + " (" + jugadorActual.getColor() + ")";
    }

    // Obtener notación algebraica de la partida
    public String getNotacionAlgebraica() {
        StringBuilder notacion = new StringBuilder();
        for (int i = 0; i < historialMovimientos.size(); i++) {
            if (i % 2 == 0) {
                notacion.append((i / 2 + 1)).append(". ");
            }
            notacion.append(historialMovimientos.get(i).getNotacionAlgebraica()).append(" ");
            if (i % 2 == 1) {
                notacion.append("\n");
            }
        }
        return notacion.toString();
    }

    // Deshacer último movimiento (opcional)
    public boolean deshacerMovimiento() {
        if (historialMovimientos.isEmpty()) {
            return false;
        }

        // Esta es una implementación simplificada
        // En una versión completa, necesitarías guardar más información
        // para restaurar completamente el estado anterior
        historialMovimientos.remove(historialMovimientos.size() - 1);
        tablero = new clsTablero(); // Reiniciar tablero

        // Replicar todos los movimientos excepto el último
        ArrayList<clsMovimiento> movimientosTemp = new ArrayList<>(historialMovimientos);
        historialMovimientos.clear();
        jugadorActual = jugadorBlanco;

        for (clsMovimiento mov : movimientosTemp) {
            realizarMovimiento(mov.getFilaOrigen(), mov.getColumnaOrigen(),
                             mov.getFilaDestino(), mov.getColumnaDestino());
        }

        return true;
    }
}
