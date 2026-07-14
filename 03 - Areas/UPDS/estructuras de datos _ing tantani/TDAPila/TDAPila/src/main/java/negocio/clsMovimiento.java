/*
 * Clase Movimiento - Registra un movimiento realizado en la partida
 */
package negocio;

public class clsMovimiento {
    private clsPieza pieza;
    private int filaOrigen;
    private int columnaOrigen;
    private int filaDestino;
    private int columnaDestino;
    private clsPieza piezaCapturada; // null si no hubo captura
    private boolean esEnroque;
    private boolean esPromocion;
    private String piezaPromocionada; // Tipo de pieza promovida (si aplica)

    public clsMovimiento(clsPieza pieza, int filaOrigen, int columnaOrigen,
                         int filaDestino, int columnaDestino) {
        this.pieza = pieza;
        this.filaOrigen = filaOrigen;
        this.columnaOrigen = columnaOrigen;
        this.filaDestino = filaDestino;
        this.columnaDestino = columnaDestino;
        this.piezaCapturada = null;
        this.esEnroque = false;
        this.esPromocion = false;
        this.piezaPromocionada = null;
    }

    // Getters y Setters
    public clsPieza getPieza() {
        return pieza;
    }

    public int getFilaOrigen() {
        return filaOrigen;
    }

    public int getColumnaOrigen() {
        return columnaOrigen;
    }

    public int getFilaDestino() {
        return filaDestino;
    }

    public int getColumnaDestino() {
        return columnaDestino;
    }

    public clsPieza getPiezaCapturada() {
        return piezaCapturada;
    }

    public void setPiezaCapturada(clsPieza piezaCapturada) {
        this.piezaCapturada = piezaCapturada;
    }

    public boolean isEsEnroque() {
        return esEnroque;
    }

    public void setEsEnroque(boolean esEnroque) {
        this.esEnroque = esEnroque;
    }

    public boolean isEsPromocion() {
        return esPromocion;
    }

    public void setEsPromocion(boolean esPromocion) {
        this.esPromocion = esPromocion;
    }

    public String getPiezaPromocionada() {
        return piezaPromocionada;
    }

    public void setPiezaPromocionada(String piezaPromocionada) {
        this.piezaPromocionada = piezaPromocionada;
    }

    // Método para obtener notación algebraica del movimiento
    public String getNotacionAlgebraica() {
        String origen = obtenerCasillaNotacion(filaOrigen, columnaOrigen);
        String destino = obtenerCasillaNotacion(filaDestino, columnaDestino);

        String movimiento = pieza.getTipo().substring(0, 1); // Primera letra de la pieza

        if (pieza.getTipo().equals("PEON")) {
            movimiento = "";
        }

        if (piezaCapturada != null) {
            movimiento += "x"; // Símbolo de captura
        }

        movimiento += destino;

        if (esEnroque) {
            if (columnaDestino > columnaOrigen) {
                return "O-O"; // Enroque corto
            } else {
                return "O-O-O"; // Enroque largo
            }
        }

        if (esPromocion) {
            movimiento += "=" + piezaPromocionada.substring(0, 1);
        }

        return movimiento;
    }

    private String obtenerCasillaNotacion(int fila, int columna) {
        char columnaLetra = (char) ('a' + columna);
        int filaNumero = 8 - fila;
        return "" + columnaLetra + filaNumero;
    }
}
