/*
 * Clase Tablero - Maneja el tablero de ajedrez 8x8
 * Contiene toda la lógica para inicializar, mover piezas y verificar el estado del juego
 */
package negocio;

public class clsTablero {
    private clsPieza[][] tablero; // Matriz 8x8 de piezas
    private clsCasilla[][] casillas; // Matriz 8x8 de casillas

    public clsTablero() {
        this.tablero = new clsPieza[8][8];
        this.casillas = new clsCasilla[8][8];
        inicializarCasillas();
        inicializarPiezas();
    }

    // Inicializar las casillas del tablero
    private void inicializarCasillas() {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                casillas[i][j] = new clsCasilla(i, j);
            }
        }
    }

    // Inicializar las piezas en su posición inicial
    private void inicializarPiezas() {
        // Piezas negras (fila 0 y 1)
        tablero[0][0] = new clsTorre("NEGRO", 0, 0);
        tablero[0][1] = new clsCaballo("NEGRO", 0, 1);
        tablero[0][2] = new clsAlfil("NEGRO", 0, 2);
        tablero[0][3] = new clsReina("NEGRO", 0, 3);
        tablero[0][4] = new clsRey("NEGRO", 0, 4);
        tablero[0][5] = new clsAlfil("NEGRO", 0, 5);
        tablero[0][6] = new clsCaballo("NEGRO", 0, 6);
        tablero[0][7] = new clsTorre("NEGRO", 0, 7);

        // Peones negros
        for (int i = 0; i < 8; i++) {
            tablero[1][i] = new clsPeon("NEGRO", 1, i);
        }

        // Piezas blancas (fila 7 y 6)
        tablero[7][0] = new clsTorre("BLANCO", 7, 0);
        tablero[7][1] = new clsCaballo("BLANCO", 7, 1);
        tablero[7][2] = new clsAlfil("BLANCO", 7, 2);
        tablero[7][3] = new clsReina("BLANCO", 7, 3);
        tablero[7][4] = new clsRey("BLANCO", 7, 4);
        tablero[7][5] = new clsAlfil("BLANCO", 7, 5);
        tablero[7][6] = new clsCaballo("BLANCO", 7, 6);
        tablero[7][7] = new clsTorre("BLANCO", 7, 7);

        // Peones blancos
        for (int i = 0; i < 8; i++) {
            tablero[6][i] = new clsPeon("BLANCO", 6, i);
        }

        // Actualizar las casillas con las piezas
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                casillas[i][j].setPieza(tablero[i][j]);
            }
        }
    }

    // Getters
    public clsPieza[][] getTablero() {
        return tablero;
    }

    public clsCasilla[][] getCasillas() {
        return casillas;
    }

    public clsPieza getPieza(int fila, int columna) {
        if (fila >= 0 && fila < 8 && columna >= 0 && columna < 8) {
            return tablero[fila][columna];
        }
        return null;
    }

    // Colocar una pieza en una posición
    public void setPieza(int fila, int columna, clsPieza pieza) {
        if (fila >= 0 && fila < 8 && columna >= 0 && columna < 8) {
            tablero[fila][columna] = pieza;
            casillas[fila][columna].setPieza(pieza);
            if (pieza != null) {
                pieza.setFila(fila);
                pieza.setColumna(columna);
            }
        }
    }

    // Mover una pieza de una posición a otra
    public boolean moverPieza(int filaOrigen, int columnaOrigen,
                              int filaDestino, int columnaDestino) {
        clsPieza pieza = getPieza(filaOrigen, columnaOrigen);

        if (pieza == null) {
            return false; // No hay pieza en el origen
        }

        // Verificar si el movimiento es válido según las reglas de la pieza
        if (!pieza.esMovimientoValido(filaDestino, columnaDestino, tablero)) {
            return false;
        }

        // Realizar el movimiento
        clsPieza piezaCapturada = getPieza(filaDestino, columnaDestino);

        // Mover la pieza
        setPieza(filaDestino, columnaDestino, pieza);
        setPieza(filaOrigen, columnaOrigen, null);

        // Marcar que la pieza se ha movido (importante para enroque y peones)
        pieza.setSeHaMovido(true);

        // Verificar enroque
        if (pieza.getTipo().equals("REY")) {
            verificarYRealizarEnroque(filaOrigen, columnaOrigen, filaDestino, columnaDestino);
        }

        return true;
    }

    // Método auxiliar para realizar el enroque
    private void verificarYRealizarEnroque(int filaOrigen, int columnaOrigen,
                                           int filaDestino, int columnaDestino) {
        // Si el rey se movió 2 casillas horizontalmente, es enroque
        if (Math.abs(columnaDestino - columnaOrigen) == 2) {
            // Enroque corto (rey hacia la derecha)
            if (columnaDestino > columnaOrigen) {
                clsPieza torre = getPieza(filaDestino, 7);
                setPieza(filaDestino, columnaDestino - 1, torre); // Torre a la izquierda del rey
                setPieza(filaDestino, 7, null);
            }
            // Enroque largo (rey hacia la izquierda)
            else {
                clsPieza torre = getPieza(filaDestino, 0);
                setPieza(filaDestino, columnaDestino + 1, torre); // Torre a la derecha del rey
                setPieza(filaDestino, 0, null);
            }
        }
    }

    // Buscar la posición del rey de un color específico
    public int[] buscarRey(String color) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = tablero[i][j];
                if (pieza != null && pieza.getTipo().equals("REY") &&
                    pieza.getColor().equals(color)) {
                    return new int[]{i, j};
                }
            }
        }
        return null;
    }

    // Verificar si una casilla está siendo atacada por el color enemigo
    public boolean estaAtacada(int fila, int columna, String colorAtacante) {
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = tablero[i][j];
                if (pieza != null && pieza.getColor().equals(colorAtacante)) {
                    if (pieza.esMovimientoValido(fila, columna, tablero)) {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Verificar si el rey de un color está en jaque
    public boolean estEnJaque(String colorRey) {
        int[] posicionRey = buscarRey(colorRey);
        if (posicionRey == null) {
            return false;
        }

        String colorEnemigo = colorRey.equals("BLANCO") ? "NEGRO" : "BLANCO";
        return estaAtacada(posicionRey[0], posicionRey[1], colorEnemigo);
    }

    // Verificar si es jaque mate
    public boolean esJaqueMate(String colorRey) {
        if (!estEnJaque(colorRey)) {
            return false; // No está en jaque, no puede ser jaque mate
        }

        // Intentar todos los movimientos posibles para ver si alguno saca del jaque
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = tablero[i][j];
                if (pieza != null && pieza.getColor().equals(colorRey)) {
                    // Probar todos los movimientos posibles de esta pieza
                    for (int fi = 0; fi < 8; fi++) {
                        for (int co = 0; co < 8; co++) {
                            if (pieza.esMovimientoValido(fi, co, tablero)) {
                                // Simular el movimiento
                                clsPieza piezaTemporal = tablero[fi][co];
                                int filaOriginal = pieza.getFila();
                                int columnaOriginal = pieza.getColumna();

                                tablero[fi][co] = pieza;
                                tablero[filaOriginal][columnaOriginal] = null;
                                pieza.setFila(fi);
                                pieza.setColumna(co);

                                // Verificar si sigue en jaque
                                boolean siguenJaque = estEnJaque(colorRey);

                                // Deshacer el movimiento
                                tablero[filaOriginal][columnaOriginal] = pieza;
                                tablero[fi][co] = piezaTemporal;
                                pieza.setFila(filaOriginal);
                                pieza.setColumna(columnaOriginal);

                                if (!siguenJaque) {
                                    return false; // Hay un movimiento que saca del jaque
                                }
                            }
                        }
                    }
                }
            }
        }

        return true; // No hay movimiento que saque del jaque: jaque mate
    }

    // Verificar si hay ahogado (stalemate)
    public boolean esAhogado(String colorJugador) {
        if (estEnJaque(colorJugador)) {
            return false; // Si está en jaque, no puede ser ahogado
        }

        // Verificar si hay algún movimiento legal
        for (int i = 0; i < 8; i++) {
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = tablero[i][j];
                if (pieza != null && pieza.getColor().equals(colorJugador)) {
                    for (int fi = 0; fi < 8; fi++) {
                        for (int co = 0; co < 8; co++) {
                            if (pieza.esMovimientoValido(fi, co, tablero)) {
                                // Simular movimiento
                                clsPieza piezaTemporal = tablero[fi][co];
                                int filaOriginal = pieza.getFila();
                                int columnaOriginal = pieza.getColumna();

                                tablero[fi][co] = pieza;
                                tablero[filaOriginal][columnaOriginal] = null;
                                pieza.setFila(fi);
                                pieza.setColumna(co);

                                boolean quedariaEnJaque = estEnJaque(colorJugador);

                                // Deshacer
                                tablero[filaOriginal][columnaOriginal] = pieza;
                                tablero[fi][co] = piezaTemporal;
                                pieza.setFila(filaOriginal);
                                pieza.setColumna(columnaOriginal);

                                if (!quedariaEnJaque) {
                                    return false; // Hay un movimiento legal
                                }
                            }
                        }
                    }
                }
            }
        }

        return true; // No hay movimientos legales: ahogado
    }

    // Promover un peón
    public void promoverPeon(int fila, int columna, String tipoPieza) {
        clsPieza peon = getPieza(fila, columna);
        if (peon != null && peon.getTipo().equals("PEON")) {
            clsPieza nuevaPieza = null;
            String color = peon.getColor();

            switch (tipoPieza) {
                case "REINA":
                    nuevaPieza = new clsReina(color, fila, columna);
                    break;
                case "TORRE":
                    nuevaPieza = new clsTorre(color, fila, columna);
                    break;
                case "ALFIL":
                    nuevaPieza = new clsAlfil(color, fila, columna);
                    break;
                case "CABALLO":
                    nuevaPieza = new clsCaballo(color, fila, columna);
                    break;
            }

            if (nuevaPieza != null) {
                setPieza(fila, columna, nuevaPieza);
            }
        }
    }

    // Método para imprimir el tablero en consola (útil para debug)
    public void imprimirTablero() {
        System.out.println("  a b c d e f g h");
        for (int i = 0; i < 8; i++) {
            System.out.print((8 - i) + " ");
            for (int j = 0; j < 8; j++) {
                clsPieza pieza = tablero[i][j];
                if (pieza == null) {
                    System.out.print(". ");
                } else {
                    System.out.print(pieza.getSimbolo() + " ");
                }
            }
            System.out.println((8 - i));
        }
        System.out.println("  a b c d e f g h");
    }
}
