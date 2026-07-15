/*
 * Clase IA - Inteligencia Artificial para el juego de ajedrez
 * Implementa una IA básica con estrategias de evaluación de posición
 */
package negocio;

import java.util.ArrayList;
import java.util.Random;

public class clsIA {
    private String color; // Color de la IA (NEGRO o BLANCO)
    private String dificultad; // FACIL, MEDIO, DIFICIL
    private Random random;

    // Valores de las piezas para evaluación
    private static final int VALOR_PEON = 10;
    private static final int VALOR_CABALLO = 30;
    private static final int VALOR_ALFIL = 30;
    private static final int VALOR_TORRE = 50;
    private static final int VALOR_REINA = 90;
    private static final int VALOR_REY = 900;

    public clsIA(String color, String dificultad) {
        this.color = color;
        this.dificultad = dificultad;
        this.random = new Random();
    }

    // Getters y Setters
    public String getColor() {
        return color;
    }

    public String getDificultad() {
        return dificultad;
    }

    public void setDificultad(String dificultad) {
        this.dificultad = dificultad;
    }

    /**
     * Realizar el movimiento de la IA
     * @param partida La partida actual
     * @return true si se realizó un movimiento válido
     */
    public boolean realizarMovimiento(clsPartidaAjedrez partida) {
        clsTablero tablero = partida.getTablero();
        clsPieza[][] matriz = tablero.getTablero();

        // Obtener todas las piezas de la IA
        ArrayList<clsPieza> piezasIA = new ArrayList<>();
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = matriz[i][j];
                if (pieza != null && pieza.getColor().equals(color)) {
                    piezasIA.add(pieza);
                }
            }
        }

        if (piezasIA.isEmpty()) {
            return false;
        }

        // Seleccionar estrategia según dificultad
        switch (dificultad) {
            case "FACIL":
                return movimientoAleatorio(partida, piezasIA);
            case "MEDIO":
                return movimientoInteligente(partida, piezasIA);
            case "DIFICIL":
                return movimientoAvanzado(partida, piezasIA);
            default:
                return movimientoAleatorio(partida, piezasIA);
        }
    }

    /**
     * Movimiento aleatorio (Dificultad FACIL)
     */
    private boolean movimientoAleatorio(clsPartidaAjedrez partida, ArrayList<clsPieza> piezas) {
        // Mezclar las piezas para aleatoriedad
        ArrayList<clsPieza> piezasMezcladas = new ArrayList<>(piezas);

        for (int intento = 0; intento < 100; intento++) {
            clsPieza pieza = piezasMezcladas.get(random.nextInt(piezasMezcladas.size()));
            ArrayList<int[]> movimientos = partida.getMovimientosPosibles(pieza.getFila(), pieza.getColumna());

            if (!movimientos.isEmpty()) {
                int[] movimiento = movimientos.get(random.nextInt(movimientos.size()));
                if (partida.realizarMovimiento(pieza.getFila(), pieza.getColumna(), movimiento[0], movimiento[1])) {
                    return true;
                }
            }
        }

        return false;
    }

    /**
     * Movimiento inteligente (Dificultad MEDIO)
     * Prioriza capturas y protección del rey
     */
    private boolean movimientoInteligente(clsPartidaAjedrez partida, ArrayList<clsPieza> piezas) {
        clsTablero tablero = partida.getTablero();
        int mejorPuntuacion = Integer.MIN_VALUE;
        int[] mejorOrigen = null;
        int[] mejorDestino = null;

        // Evaluar todos los movimientos posibles
        for (clsPieza pieza : piezas) {
            ArrayList<int[]> movimientos = partida.getMovimientosPosibles(pieza.getFila(), pieza.getColumna());

            for (int[] destino : movimientos) {
                int puntuacion = evaluarMovimiento(tablero, pieza, destino[0], destino[1]);

                if (puntuacion > mejorPuntuacion) {
                    mejorPuntuacion = puntuacion;
                    mejorOrigen = new int[]{pieza.getFila(), pieza.getColumna()};
                    mejorDestino = destino;
                }
            }
        }

        // Realizar el mejor movimiento encontrado
        if (mejorOrigen != null && mejorDestino != null) {
            return partida.realizarMovimiento(mejorOrigen[0], mejorOrigen[1], mejorDestino[0], mejorDestino[1]);
        }

        // Si no hay movimiento evaluado, hacer uno aleatorio
        return movimientoAleatorio(partida, piezas);
    }

    /**
     * Movimiento avanzado (Dificultad DIFICIL)
     * Utiliza minimax simplificado con evaluación de posición
     */
    private boolean movimientoAvanzado(clsPartidaAjedrez partida, ArrayList<clsPieza> piezas) {
        clsTablero tablero = partida.getTablero();
        int mejorPuntuacion = Integer.MIN_VALUE;
        int[] mejorOrigen = null;
        int[] mejorDestino = null;

        // Evaluar todos los movimientos con profundidad 2
        for (clsPieza pieza : piezas) {
            ArrayList<int[]> movimientos = partida.getMovimientosPosibles(pieza.getFila(), pieza.getColumna());

            for (int[] destino : movimientos) {
                int puntuacion = evaluarMovimientoAvanzado(tablero, pieza, destino[0], destino[1]);

                // Agregar aleatoriedad para variedad
                puntuacion += random.nextInt(10);

                if (puntuacion > mejorPuntuacion) {
                    mejorPuntuacion = puntuacion;
                    mejorOrigen = new int[]{pieza.getFila(), pieza.getColumna()};
                    mejorDestino = destino;
                }
            }
        }

        // Realizar el mejor movimiento
        if (mejorOrigen != null && mejorDestino != null) {
            return partida.realizarMovimiento(mejorOrigen[0], mejorOrigen[1], mejorDestino[0], mejorDestino[1]);
        }

        return movimientoInteligente(partida, piezas);
    }

    /**
     * Evaluar un movimiento específico (para dificultad MEDIO)
     */
    private int evaluarMovimiento(clsTablero tablero, clsPieza pieza, int filaDestino, int columnaDestino) {
        int puntuacion = 0;

        // Puntuación por captura
        clsPieza piezaCapturada = tablero.getPieza(filaDestino, columnaDestino);
        if (piezaCapturada != null) {
            puntuacion += obtenerValorPieza(piezaCapturada) * 10; // Las capturas son muy valiosas
        }

        // Puntuación por control del centro
        if (filaDestino >= 3 && filaDestino <= 4 && columnaDestino >= 3 && columnaDestino <= 4) {
            puntuacion += 20;
        }

        // Puntuación por desarrollo (mover piezas desde posiciones iniciales)
        if (pieza.getTipo().equals("CABALLO") || pieza.getTipo().equals("ALFIL")) {
            if ((color.equals("BLANCO") && pieza.getFila() == 7) ||
                (color.equals("NEGRO") && pieza.getFila() == 0)) {
                puntuacion += 15; // Desarrollar piezas menores
            }
        }

        // Penalización por mover el rey temprano
        if (pieza.getTipo().equals("REY") && !pieza.isSeHaMovido()) {
            puntuacion -= 30;
        }

        // Bonificación por enroque
        if (pieza.getTipo().equals("REY") && Math.abs(columnaDestino - pieza.getColumna()) == 2) {
            puntuacion += 50; // El enroque es importante
        }

        return puntuacion;
    }

    /**
     * Evaluación avanzada con simulación (para dificultad DIFICIL)
     */
    private int evaluarMovimientoAvanzado(clsTablero tablero, clsPieza pieza, int filaDestino, int columnaDestino) {
        int puntuacion = evaluarMovimiento(tablero, pieza, filaDestino, columnaDestino);

        // Evaluar la posición resultante
        clsPieza piezaCapturada = tablero.getPieza(filaDestino, columnaDestino);

        // Simular el movimiento
        int filaOriginal = pieza.getFila();
        int columnaOriginal = pieza.getColumna();
        clsPieza[][] matriz = tablero.getTablero();

        matriz[filaDestino][columnaDestino] = pieza;
        matriz[filaOriginal][columnaOriginal] = null;
        pieza.setFila(filaDestino);
        pieza.setColumna(columnaDestino);

        // Evaluar la posición completa del tablero
        puntuacion += evaluarPosicionTablero(tablero);

        // Verificar si el movimiento pone en jaque al oponente
        String colorEnemigo = color.equals("BLANCO") ? "NEGRO" : "BLANCO";
        if (tablero.estEnJaque(colorEnemigo)) {
            puntuacion += 100; // Dar jaque es muy valioso
        }

        // Deshacer el movimiento
        matriz[filaOriginal][columnaOriginal] = pieza;
        matriz[filaDestino][columnaDestino] = piezaCapturada;
        pieza.setFila(filaOriginal);
        pieza.setColumna(columnaOriginal);

        return puntuacion;
    }

    /**
     * Evaluar la posición general del tablero
     */
    private int evaluarPosicionTablero(clsTablero tablero) {
        int puntuacion = 0;
        clsPieza[][] matriz = tablero.getTablero();

        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = matriz[i][j];
                if (pieza != null) {
                    int valor = obtenerValorPieza(pieza);
                    if (pieza.getColor().equals(color)) {
                        puntuacion += valor; // Mis piezas suman
                    } else {
                        puntuacion -= valor; // Piezas enemigas restan
                    }
                }
            }
        }

        return puntuacion;
    }

    /**
     * Obtener el valor de una pieza
     */
    private int obtenerValorPieza(clsPieza pieza) {
        switch (pieza.getTipo()) {
            case "PEON":
                return VALOR_PEON;
            case "CABALLO":
                return VALOR_CABALLO;
            case "ALFIL":
                return VALOR_ALFIL;
            case "TORRE":
                return VALOR_TORRE;
            case "REINA":
                return VALOR_REINA;
            case "REY":
                return VALOR_REY;
            default:
                return 0;
        }
    }
}
