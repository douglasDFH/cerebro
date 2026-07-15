package presentacion;

import negocio.clsListaCircular;
import negocio.clsNodoDoble;
import java.awt.*;
import javax.swing.*;
import java.util.List;

/**
 * Formulario de Ruleta de la Suerte
 * Utiliza listas circulares dobles para gestionar jugadores y premios
 */
public class frmRuleta extends javax.swing.JFrame {

    private clsListaCircular listaJugadores;
    private clsListaCircular listaPremios;

    // Componentes adicionales para paneles laterales
    private JTextArea txtListaJugadores;
    private JTextArea txtListaPremios;
    private JTextField txtCantidadPremio;  // Campo para cantidad de premios

    // 10 colores diferentes para las ruletas
    private Color[] coloresJugadores = {
        new Color(255, 99, 71),    // 1. Rojo tomate
        new Color(135, 206, 250),  // 2. Azul cielo
        new Color(144, 238, 144),  // 3. Verde claro
        new Color(255, 215, 0),    // 4. Dorado
        new Color(221, 160, 221),  // 5. Ciruela
        new Color(255, 165, 0),    // 6. Naranja
        new Color(64, 224, 208),   // 7. Turquesa
        new Color(255, 192, 203),  // 8. Rosa
        new Color(173, 216, 230),  // 9. Azul claro
        new Color(152, 251, 152)   // 10. Verde pálido
    };

    private Color[] coloresPremios = {
        new Color(255, 99, 71),    // 1. Rojo tomate
        new Color(135, 206, 250),  // 2. Azul cielo
        new Color(144, 238, 144),  // 3. Verde claro
        new Color(255, 215, 0),    // 4. Dorado
        new Color(221, 160, 221),  // 5. Ciruela
        new Color(255, 165, 0),    // 6. Naranja
        new Color(64, 224, 208),   // 7. Turquesa
        new Color(255, 192, 203),  // 8. Rosa
        new Color(173, 216, 230),  // 9. Azul claro
        new Color(152, 251, 152)   // 10. Verde pálido
    };

    public frmRuleta() {
        initComponents();
        setTitle("PROYECTO - RULETA DE LA SUERTE");
        setLocationRelativeTo(null);

        // Inicializar listas circulares VACÍAS
        listaJugadores = new clsListaCircular();
        listaPremios = new clsListaCircular();

        // Inicializar paneles laterales
        inicializarPanelesLaterales();

        // Ocultar los componentes txtJugador y txtPremio y sus etiquetas
        txtJugador.setVisible(false);
        txtPremio.setVisible(false);
        jLabel4.setVisible(false);
        jLabel5.setVisible(false);

       
    }

    /**
     * Inicializar paneles laterales con listas de jugadores y premios
     */
    private void inicializarPanelesLaterales() {
        // Panel lateral de jugadores (a la izquierda de la ruleta)
        JPanel panelListaJugadores = new JPanel();
        panelListaJugadores.setLayout(new BoxLayout(panelListaJugadores, BoxLayout.Y_AXIS));
        panelListaJugadores.setBorder(BorderFactory.createTitledBorder(
            BorderFactory.createLineBorder(Color.BLACK, 2),
            "Lista de Jugadores",
            javax.swing.border.TitledBorder.DEFAULT_JUSTIFICATION,
            javax.swing.border.TitledBorder.DEFAULT_POSITION,
            new Font("Segoe UI", Font.BOLD, 12),
            Color.BLACK
        ));
        panelListaJugadores.setPreferredSize(new Dimension(200, 320));
        panelListaJugadores.setBackground(new Color(245, 245, 255));

        JLabel lblInstruccionJug = new JLabel("<html><center>Escribe un nombre<br>por línea:</center></html>");
        lblInstruccionJug.setFont(new Font("Segoe UI", Font.ITALIC, 10));
        lblInstruccionJug.setAlignmentX(Component.CENTER_ALIGNMENT);
        lblInstruccionJug.setForeground(Color.BLACK);

        txtListaJugadores = new JTextArea(14, 14);
        txtListaJugadores.setFont(new Font("Arial", Font.PLAIN, 12));
        txtListaJugadores.setLineWrap(false);
        JScrollPane scrollJugadores = new JScrollPane(txtListaJugadores);
        scrollJugadores.setPreferredSize(new Dimension(180, 270));

        panelListaJugadores.add(Box.createVerticalStrut(5));
        panelListaJugadores.add(lblInstruccionJug);
        panelListaJugadores.add(Box.createVerticalStrut(5));
        panelListaJugadores.add(scrollJugadores);
        panelListaJugadores.add(Box.createVerticalStrut(5));

        // Panel lateral de premios (a la izquierda de la ruleta)
        JPanel panelListaPremios = new JPanel();
        panelListaPremios.setLayout(new BoxLayout(panelListaPremios, BoxLayout.Y_AXIS));
        panelListaPremios.setBorder(BorderFactory.createTitledBorder(
            BorderFactory.createLineBorder(Color.BLACK, 2),
            "Lista de Premios",
            javax.swing.border.TitledBorder.DEFAULT_JUSTIFICATION,
            javax.swing.border.TitledBorder.DEFAULT_POSITION,
            new Font("Segoe UI", Font.BOLD, 12),
            Color.BLACK
        ));
        panelListaPremios.setPreferredSize(new Dimension(200, 320));
        panelListaPremios.setBackground(new Color(245, 245, 255));

        JLabel lblInstruccionPrem = new JLabel("<html><center>Escribe un premio<br>por línea:</center></html>");
        lblInstruccionPrem.setFont(new Font("Segoe UI", Font.ITALIC, 10));
        lblInstruccionPrem.setAlignmentX(Component.CENTER_ALIGNMENT);
        lblInstruccionPrem.setForeground(Color.BLACK);

        txtListaPremios = new JTextArea(10, 14);
        txtListaPremios.setFont(new Font("Arial", Font.PLAIN, 12));
        txtListaPremios.setLineWrap(false);
        JScrollPane scrollPremios = new JScrollPane(txtListaPremios);
        scrollPremios.setPreferredSize(new Dimension(180, 200));

        // Campo para cantidad
        JLabel lblCantidad = new JLabel("Cantidad:");
        lblCantidad.setFont(new Font("Segoe UI", Font.BOLD, 11));
        lblCantidad.setAlignmentX(Component.CENTER_ALIGNMENT);
        lblCantidad.setForeground(Color.BLACK);

        txtCantidadPremio = new JTextField("1");
        txtCantidadPremio.setFont(new Font("Arial", Font.PLAIN, 12));
        txtCantidadPremio.setMaximumSize(new Dimension(180, 25));
        txtCantidadPremio.setHorizontalAlignment(JTextField.CENTER);

        panelListaPremios.add(Box.createVerticalStrut(5));
        panelListaPremios.add(lblInstruccionPrem);
        panelListaPremios.add(Box.createVerticalStrut(5));
        panelListaPremios.add(scrollPremios);
        panelListaPremios.add(Box.createVerticalStrut(10));
        panelListaPremios.add(lblCantidad);
        panelListaPremios.add(Box.createVerticalStrut(3));
        panelListaPremios.add(txtCantidadPremio);
        panelListaPremios.add(Box.createVerticalStrut(5));

        // Crear paneles contenedores horizontales para cada ruleta + su lista
        JPanel contenedorJugadores = new JPanel(new BorderLayout(10, 0));
        contenedorJugadores.setBackground(new Color(240, 240, 240));
        contenedorJugadores.add(panelListaJugadores, BorderLayout.WEST);
        contenedorJugadores.add(panelJugadores, BorderLayout.CENTER);

        JPanel contenedorPremios = new JPanel(new BorderLayout(10, 0));
        contenedorPremios.setBackground(new Color(240, 240, 240));
        contenedorPremios.add(panelListaPremios, BorderLayout.WEST);
        contenedorPremios.add(panelPremio, BorderLayout.CENTER);

        // Reemplazar los panelJugadores y panelPremios en jPanel1 y jPanel2
        // Necesitamos reorganizar el layout de jPanel1 y jPanel2
        reorganizarLayoutJugadores(contenedorJugadores);
        reorganizarLayoutPremios(contenedorPremios);
    }

    private void reorganizarLayoutJugadores(JPanel contenedorJugadores) {
        // Remover panelJugadores del layout actual
        for (Component comp : jPanel1.getComponents()) {
            if (comp == panelJugadores) {
                jPanel1.remove(comp);
                break;
            }
        }

        // Agregar el nuevo contenedor (lista + ruleta) en la misma posición
        GroupLayout layout = (GroupLayout) jPanel1.getLayout();
        jPanel1.add(contenedorJugadores);

        // Actualizar el layout
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel2, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(contenedorJugadores, GroupLayout.DEFAULT_SIZE, 620, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnGirarIzqJugador, GroupLayout.PREFERRED_SIZE, 65, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(btnGirarDerJugador, GroupLayout.PREFERRED_SIZE, 65, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnJugar, GroupLayout.PREFERRED_SIZE, 100, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnAgregarJugador, GroupLayout.PREFERRED_SIZE, 200, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnEliminarJugador, GroupLayout.PREFERRED_SIZE, 100, GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel4, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );

        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel2)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(contenedorJugadores, GroupLayout.PREFERRED_SIZE, 320, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(btnGirarIzqJugador, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnGirarDerJugador, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnJugar, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(btnAgregarJugador, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnEliminarJugador, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel4)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        jPanel1.revalidate();
        jPanel1.repaint();
    }

    private void reorganizarLayoutPremios(JPanel contenedorPremios) {
        // Remover panelPremio del layout actual
        for (Component comp : jPanel2.getComponents()) {
            if (comp == panelPremio) {
                jPanel2.remove(comp);
                break;
            }
        }

        // Agregar el nuevo contenedor (lista + ruleta) en la misma posición
        GroupLayout layout = (GroupLayout) jPanel2.getLayout();
        jPanel2.add(contenedorPremios);

        // Actualizar el layout
        layout.setHorizontalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel3, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(contenedorPremios, GroupLayout.DEFAULT_SIZE, 620, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnGirarIzqPremio, GroupLayout.PREFERRED_SIZE, 65, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(btnGirarDerPremio, GroupLayout.PREFERRED_SIZE, 65, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnReclamar, GroupLayout.PREFERRED_SIZE, 100, GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnAgregarPremio, GroupLayout.PREFERRED_SIZE, 200, GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnEliminarPremio, GroupLayout.PREFERRED_SIZE, 100, GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel5, GroupLayout.DEFAULT_SIZE, GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );

        layout.setVerticalGroup(
            layout.createParallelGroup(GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel3)
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(contenedorPremios, GroupLayout.PREFERRED_SIZE, 320, GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(btnGirarIzqPremio, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnGirarDerPremio, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnReclamar, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(layout.createParallelGroup(GroupLayout.Alignment.BASELINE)
                    .addComponent(btnAgregarPremio, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnEliminarPremio, GroupLayout.PREFERRED_SIZE, 35, GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel5)
                .addContainerGap(GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        jPanel2.revalidate();
        jPanel2.repaint();
    }


  
    class PanelRuletaJugadores extends JPanel {
        @Override
        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            Graphics2D g2d = (Graphics2D) g;
            g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

            dibujarRuleta(g2d, listaJugadores, getWidth(), getHeight());
        }
    }

    /**
     * Panel personalizado para dibujar la ruleta de premios
     */
    class PanelRuletaPremios extends JPanel {
        @Override
        protected void paintComponent(Graphics g) {
            super.paintComponent(g);
            Graphics2D g2d = (Graphics2D) g;
            g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);

            if (listaPremios != null) {
                dibujarRuleta(g2d, listaPremios, getWidth(), getHeight());
            }
        }
    }

    /**
     * Método genérico para dibujar una ruleta
     */
    private void dibujarRuleta(Graphics2D g2d, clsListaCircular lista, int width, int height) {
        // Configurar el círculo (mismo tamaño siempre, vacía o llena)
        int diameter = Math.min(width, height) - 40;
        int x = (width - diameter) / 2;
        int y = (height - diameter) / 2;
        int centerX = width / 2;
        int centerY = height / 2;
        int radius = diameter / 2;

        if (lista.estaVacia()) {
            // Dibujar círculo vacío con borde gris
            g2d.setColor(new Color(240, 240, 240)); // Fondo gris claro
            g2d.fillOval(x, y, diameter, diameter);

            g2d.setColor(new Color(180, 180, 180)); // Borde gris oscuro
            g2d.setStroke(new BasicStroke(3));
            g2d.drawOval(x, y, diameter, diameter);

            // Dibujar círculo central blanco
            int circuloCentralDiameter = diameter / 5;
            g2d.setColor(Color.WHITE);
            g2d.fillOval(centerX - circuloCentralDiameter / 2, centerY - circuloCentralDiameter / 2,
                         circuloCentralDiameter, circuloCentralDiameter);
            g2d.setColor(Color.DARK_GRAY);
            g2d.setStroke(new BasicStroke(2));
            g2d.drawOval(centerX - circuloCentralDiameter / 2, centerY - circuloCentralDiameter / 2,
                         circuloCentralDiameter, circuloCentralDiameter);

            // Texto "Ruleta vacía" centrado
            g2d.setColor(Color.DARK_GRAY);
            g2d.setFont(new Font("Arial", Font.ITALIC, 14));
            FontMetrics fm = g2d.getFontMetrics();
            String texto = "Ruleta vacía";
            int textWidth = fm.stringWidth(texto);
            g2d.drawString(texto, centerX - textWidth / 2, centerY + fm.getAscent() / 2);

            return;
        }

        List<clsNodoDoble> nodos = lista.obtenerTodosLosNodos();
        int numElementos = nodos.size();

        // Calcular ángulo por segmento
        double anguloPorSegmento = 360.0 / numElementos;

        // El primer nodo de la lista (índice 0) es el nodo actual (pLC)
        // La flecha apunta desde la derecha (0°), así que el nodo actual debe centrarse en 0°
        // Comenzamos medio segmento hacia arriba para centrar el segmento en 0°
        double anguloInicio = -anguloPorSegmento / 2;

        clsNodoDoble actual = lista.obtenerNodoActual();

        for (int i = 0; i < numElementos; i++) {
            clsNodoDoble nodo = nodos.get(i);

            // Dibujar segmento con color
            g2d.setColor(nodo.getColor());
            g2d.fillArc(x, y, diameter, diameter, (int) anguloInicio, (int) anguloPorSegmento);

            // Borde del segmento
            g2d.setColor(Color.WHITE);
            g2d.setStroke(new BasicStroke(2));
            g2d.drawArc(x, y, diameter, diameter, (int) anguloInicio, (int) anguloPorSegmento);

            // Calcular posición y rotación del texto
            double anguloMedio = anguloInicio + anguloPorSegmento / 2;
            double anguloMedioRad = Math.toRadians(anguloMedio);
            int textRadius = radius * 2 / 3; // Posicionar texto a 2/3 del radio
            int textX = centerX + (int) (textRadius * Math.cos(anguloMedioRad));
            int textY = centerY - (int) (textRadius * Math.sin(anguloMedioRad));

            // Dibujar nombre ROTADO siguiendo la curvatura del arco
            g2d.setColor(Color.BLACK);
            g2d.setFont(new Font("Arial", Font.BOLD, 16));
            FontMetrics fm = g2d.getFontMetrics();
            // Mostrar nombre con cantidad si es mayor a 1
            String nombre = nodo.getCantidad() > 1
                ? nodo.getNombre() + " (" + nodo.getCantidad() + ")"
                : nodo.getNombre();
            int textWidth = fm.stringWidth(nombre);

            // Guardar transformación original
            java.awt.geom.AffineTransform transformacionOriginal = g2d.getTransform();

            // Trasladar al punto donde se dibujará el texto
            g2d.translate(textX, textY);

            // Rotar el texto siguiendo el arco (tangente al círculo)
            // El texto debe seguir la curvatura, así que rotamos según el ángulo del segmento
            double anguloRotacion = Math.toRadians(-anguloMedio);
            g2d.rotate(anguloRotacion);

            // Dibujar el texto centrado
            g2d.drawString(nombre, -textWidth / 2, fm.getAscent() / 2);

            // Restaurar transformación original
            g2d.setTransform(transformacionOriginal);

            anguloInicio += anguloPorSegmento;
        }

        // Dibujar círculo central blanco
        int circuloCentralDiameter = diameter / 5;
        g2d.setColor(Color.WHITE);
        g2d.fillOval(centerX - circuloCentralDiameter / 2, centerY - circuloCentralDiameter / 2,
                     circuloCentralDiameter, circuloCentralDiameter);
        g2d.setColor(Color.DARK_GRAY);
        g2d.setStroke(new BasicStroke(3));
        g2d.drawOval(centerX - circuloCentralDiameter / 2, centerY - circuloCentralDiameter / 2,
                     circuloCentralDiameter, circuloCentralDiameter);

        // Dibujar indicador (flecha roja) apuntando al elemento actual
        dibujarIndicador(g2d, width, centerY, actual);
    }

    /**
     * Dibujar el indicador (flecha roja) que señala el elemento seleccionado
     * La flecha apunta HACIA ADENTRO de la ruleta (hacia la derecha)
     */
    private void dibujarIndicador(Graphics2D g2d, int width, int centerY, clsNodoDoble actual) {
        g2d.setColor(Color.RED);

        // Flecha apuntando HACIA ADENTRO (hacia la derecha del círculo)
        // Punta de la flecha en el borde del círculo
        int puntoFlechaX = width - 60;  // Cerca del borde de la ruleta
        int baseX = width - 10;         // Base de la flecha fuera del círculo

        int[] xPoints = {puntoFlechaX, baseX, baseX};
        int[] yPoints = {centerY, centerY - 20, centerY + 20};

        g2d.fillPolygon(xPoints, yPoints, 3);

        // Opcional: dibujar borde negro para mejor visibilidad
        g2d.setColor(Color.BLACK);
        g2d.setStroke(new BasicStroke(2));
        g2d.drawPolygon(xPoints, yPoints, 3);
    }

    /**
     * Repintar ambas ruletas
     */
    private void repintarRuletas() {
        panelJugadores.repaint();
        if (panelPremio != null) {
            panelPremio.repaint();
        }
    }

    /**
     * Mostrar diálogo personalizado con el resultado del juego
     */
    private void mostrarDialogoResultado(clsNodoDoble jugadorGanador, clsNodoDoble premioGanador) {
        // Crear el diálogo
        JDialog dialogo = new JDialog(this, "¡RESULTADO DEL JUEGO!", true);
        dialogo.setLayout(new BorderLayout(10, 10));
        dialogo.setSize(450, 200);
        dialogo.setLocationRelativeTo(this);

        // Panel principal con padding
        JPanel panelPrincipal = new JPanel();
        panelPrincipal.setLayout(new BoxLayout(panelPrincipal, BoxLayout.Y_AXIS));
        panelPrincipal.setBorder(BorderFactory.createEmptyBorder(20, 20, 20, 20));
        panelPrincipal.setBackground(Color.WHITE);

        // Panel para el jugador ganador
        JPanel panelJugador = new JPanel(new FlowLayout(FlowLayout.CENTER));
        panelJugador.setBackground(Color.WHITE);
        JLabel lblJugador = new JLabel("Jugador ganador: " + jugadorGanador.getNombre());
        lblJugador.setFont(new Font("Segoe UI", Font.BOLD, 16));
        lblJugador.setForeground(new Color(0, 128, 0));
        panelJugador.add(lblJugador);

        // Panel para el premio ganado
        JPanel panelPremio = new JPanel(new FlowLayout(FlowLayout.CENTER));
        panelPremio.setBackground(Color.WHITE);
        // Mostrar nombre del premio con la cantidad actual
        String textoPremio = premioGanador.getCantidad() > 1
            ? premioGanador.getNombre() + " (Quedan: " + premioGanador.getCantidad() + ")"
            : premioGanador.getNombre();
        JLabel lblPremio = new JLabel("Premio ganado: " + textoPremio);
        lblPremio.setFont(new Font("Segoe UI", Font.BOLD, 16));
        lblPremio.setForeground(new Color(204, 102, 0));
        panelPremio.add(lblPremio);

        // Panel para el botón Cerrar
        JPanel panelBoton = new JPanel(new FlowLayout(FlowLayout.CENTER, 0, 10));
        panelBoton.setBackground(Color.WHITE);
        JButton btnCerrar = new JButton("Cerrar");
        btnCerrar.setBackground(new Color(70, 130, 180));
        btnCerrar.setForeground(Color.WHITE);
        btnCerrar.setFont(new Font("Segoe UI", Font.BOLD, 14));
        btnCerrar.setPreferredSize(new Dimension(120, 35));
        btnCerrar.setFocusPainted(false);
        btnCerrar.addActionListener(e -> {
            // Eliminar jugador ganador
            listaJugadores.eliminarActual();

            // Descontar cantidad del premio
            int cantidadActual = premioGanador.getCantidad();
            if (cantidadActual > 1) {
                // Si quedan más, solo descontar
                premioGanador.setCantidad(cantidadActual - 1);
            } else {
                // Si era el último, eliminar de la lista
                listaPremios.eliminarActual();
            }

            // Repintar ruletas
            repintarRuletas();

            // Cerrar el diálogo
            dialogo.dispose();
        });
        panelBoton.add(btnCerrar);

        // Agregar espaciado entre componentes
        panelPrincipal.add(panelJugador);
        panelPrincipal.add(Box.createVerticalStrut(15));
        panelPrincipal.add(panelPremio);
        panelPrincipal.add(Box.createVerticalStrut(20));
        panelPrincipal.add(panelBoton);

        dialogo.add(panelPrincipal, BorderLayout.CENTER);
        dialogo.setVisible(true);
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jLabel1 = new javax.swing.JLabel();
        btnIniciarJuego = new javax.swing.JButton();
        jPanel1 = new javax.swing.JPanel();
        jLabel2 = new javax.swing.JLabel();
        panelJugadores = new PanelRuletaJugadores();
        btnGirarIzqJugador = new javax.swing.JButton();
        btnGirarDerJugador = new javax.swing.JButton();
        btnJugar = new javax.swing.JButton();
        btnAgregarJugador = new javax.swing.JButton();
        btnEliminarJugador = new javax.swing.JButton();
        jLabel4 = new javax.swing.JLabel();
        txtJugador = new javax.swing.JTextField();
        jPanel2 = new javax.swing.JPanel();
        jLabel3 = new javax.swing.JLabel();
        panelPremio = new PanelRuletaPremios();
        btnGirarIzqPremio = new javax.swing.JButton();
        btnGirarDerPremio = new javax.swing.JButton();
        btnReclamar = new javax.swing.JButton();
        btnAgregarPremio = new javax.swing.JButton();
        btnEliminarPremio = new javax.swing.JButton();
        jLabel5 = new javax.swing.JLabel();
        txtPremio = new javax.swing.JTextField();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        jLabel1.setFont(new java.awt.Font("Segoe UI", 1, 24)); // NOI18N
        jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel1.setText("PROYECTO - RULETA DE LA SUERTE");

        btnIniciarJuego.setBackground(new java.awt.Color(255, 153, 0));
        btnIniciarJuego.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        btnIniciarJuego.setForeground(new java.awt.Color(255, 255, 255));
        btnIniciarJuego.setText("INICIAR JUEGO");
        btnIniciarJuego.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnIniciarJuegoActionPerformed(evt);
            }
        });

        jPanel1.setBackground(new java.awt.Color(240, 240, 240));
        jPanel1.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0), 2));

        jLabel2.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel2.setText("RULETA DE JUGADORES");

        panelJugadores.setBackground(new java.awt.Color(245, 245, 245));
        panelJugadores.setPreferredSize(new java.awt.Dimension(200, 320));

        javax.swing.GroupLayout panelJugadoresLayout = new javax.swing.GroupLayout(panelJugadores);
        panelJugadores.setLayout(panelJugadoresLayout);
        panelJugadoresLayout.setHorizontalGroup(
            panelJugadoresLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 200, Short.MAX_VALUE)
        );
        panelJugadoresLayout.setVerticalGroup(
            panelJugadoresLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 320, Short.MAX_VALUE)
        );

        btnGirarIzqJugador.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        btnGirarIzqJugador.setText("←");
        btnGirarIzqJugador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnGirarIzqJugadorActionPerformed(evt);
            }
        });

        btnGirarDerJugador.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        btnGirarDerJugador.setText("→");
        btnGirarDerJugador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnGirarDerJugadorActionPerformed(evt);
            }
        });

        btnJugar.setBackground(new java.awt.Color(0, 204, 0));
        btnJugar.setFont(new java.awt.Font("Segoe UI", 1, 14)); // NOI18N
        btnJugar.setText("Girar");
        btnJugar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnJugarActionPerformed(evt);
            }
        });

        btnAgregarJugador.setText("Agregar Lista de Jugadores");
        btnAgregarJugador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnAgregarJugadorActionPerformed(evt);
            }
        });

        btnEliminarJugador.setBackground(new java.awt.Color(255, 0, 0));
        btnEliminarJugador.setForeground(new java.awt.Color(255, 255, 255));
        btnEliminarJugador.setText("Eliminar");
        btnEliminarJugador.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnEliminarJugadorActionPerformed(evt);
            }
        });

        jLabel4.setText("Jugador:");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(panelJugadores, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(btnGirarIzqJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 65, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(btnGirarDerJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 65, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnJugar, javax.swing.GroupLayout.PREFERRED_SIZE, 100, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(btnAgregarJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 200, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnEliminarJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 100, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtJugador))
                .addContainerGap())
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel2)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(panelJugadores, javax.swing.GroupLayout.PREFERRED_SIZE, 320, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnGirarIzqJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnGirarDerJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnJugar, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnAgregarJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnEliminarJugador, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel4)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtJugador, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        jPanel2.setBackground(new java.awt.Color(240, 240, 240));
        jPanel2.setBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0), 2));

        jLabel3.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        jLabel3.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel3.setText("RULETA DE PREMIOS");

        panelPremio.setBackground(new java.awt.Color(245, 245, 245));
        panelPremio.setPreferredSize(new java.awt.Dimension(200, 320));

        javax.swing.GroupLayout panelPremioLayout = new javax.swing.GroupLayout(panelPremio);
        panelPremio.setLayout(panelPremioLayout);
        panelPremioLayout.setHorizontalGroup(
            panelPremioLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 200, Short.MAX_VALUE)
        );
        panelPremioLayout.setVerticalGroup(
            panelPremioLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 320, Short.MAX_VALUE)
        );

        btnGirarIzqPremio.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        btnGirarIzqPremio.setText("←");
        btnGirarIzqPremio.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnGirarIzqPremioActionPerformed(evt);
            }
        });

        btnGirarDerPremio.setFont(new java.awt.Font("Segoe UI", 1, 18)); // NOI18N
        btnGirarDerPremio.setText("→");
        btnGirarDerPremio.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnGirarDerPremioActionPerformed(evt);
            }
        });

        btnReclamar.setBackground(new java.awt.Color(0, 204, 204));
        btnReclamar.setFont(new java.awt.Font("Segoe UI", 1, 14)); // NOI18N
        btnReclamar.setText("Girar");
        btnReclamar.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnReclamarActionPerformed(evt);
            }
        });

        btnAgregarPremio.setText("Agregar Lista de Premios");
        btnAgregarPremio.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnAgregarPremioActionPerformed(evt);
            }
        });

        btnEliminarPremio.setBackground(new java.awt.Color(255, 0, 0));
        btnEliminarPremio.setForeground(new java.awt.Color(255, 255, 255));
        btnEliminarPremio.setText("Eliminar");
        btnEliminarPremio.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                btnEliminarPremioActionPerformed(evt);
            }
        });

        jLabel5.setText("Premio:");

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(panelPremio, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(btnGirarIzqPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 65, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(btnGirarDerPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 65, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnReclamar, javax.swing.GroupLayout.PREFERRED_SIZE, 100, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addComponent(btnAgregarPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 200, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(btnEliminarPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 100, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addComponent(jLabel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtPremio))
                .addContainerGap())
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel3)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(panelPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 320, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnGirarIzqPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnGirarDerPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnReclamar, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnAgregarPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(btnEliminarPremio, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel5)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(txtPremio, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnIniciarJuego, javax.swing.GroupLayout.PREFERRED_SIZE, 200, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(0, 0, Short.MAX_VALUE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(18, 18, 18)
                        .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(0, 14, Short.MAX_VALUE)))
                .addContainerGap())
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel1)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addComponent(btnIniciarJuego, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap(14, Short.MAX_VALUE))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void btnGirarIzqJugadorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnGirarIzqJugadorActionPerformed
        listaJugadores.girarIzquierda();
        repintarRuletas();
    }//GEN-LAST:event_btnGirarIzqJugadorActionPerformed

    private void btnGirarDerJugadorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnGirarDerJugadorActionPerformed
        listaJugadores.girarDerecha();
        repintarRuletas();
    }//GEN-LAST:event_btnGirarDerJugadorActionPerformed

    private void btnAgregarJugadorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnAgregarJugadorActionPerformed
        // Obtener texto del JTextArea de jugadores
        String texto = txtListaJugadores.getText().trim();

        if (texto.isEmpty()) {
            JOptionPane.showMessageDialog(this,
                "Por favor, escribe al menos un nombre en la lista de jugadores",
                "Lista vacía",
                JOptionPane.WARNING_MESSAGE);
            return;
        }

        // Dividir por líneas
        String[] nombres = texto.split("\n");
        int insertados = 0;

        for (String nombre : nombres) {
            nombre = nombre.trim();
            if (!nombre.isEmpty()) {
                // Alternar colores
                Color color = coloresJugadores[listaJugadores.size() % coloresJugadores.length];
                listaJugadores.insertarDerecha(nombre, color);
                insertados++;
            }
        }

        if (insertados > 0) {
            JOptionPane.showMessageDialog(this,
                "Se insertaron " + insertados + " jugador(es) en la ruleta",
                "Éxito",
                JOptionPane.INFORMATION_MESSAGE);
            txtListaJugadores.setText(""); // Limpiar lista
            repintarRuletas();
        } else {
            JOptionPane.showMessageDialog(this,
                "No se encontraron nombres válidos en la lista",
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_btnAgregarJugadorActionPerformed

    private void btnEliminarJugadorActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnEliminarJugadorActionPerformed
        if (!listaJugadores.estaVacia()) {
            clsNodoDoble eliminado = listaJugadores.eliminarActual();
            JOptionPane.showMessageDialog(this, "Jugador eliminado: " + eliminado.getNombre());
            repintarRuletas();
        } else {
            JOptionPane.showMessageDialog(this, "No hay jugadores para eliminar", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_btnEliminarJugadorActionPerformed

    private void btnGirarIzqPremioActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnGirarIzqPremioActionPerformed
        listaPremios.girarIzquierda();
        repintarRuletas();
    }//GEN-LAST:event_btnGirarIzqPremioActionPerformed

    private void btnGirarDerPremioActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnGirarDerPremioActionPerformed
        listaPremios.girarDerecha();
        repintarRuletas();
    }//GEN-LAST:event_btnGirarDerPremioActionPerformed

    private void btnAgregarPremioActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnAgregarPremioActionPerformed
        // Obtener texto del JTextArea de premios
        String texto = txtListaPremios.getText().trim();

        if (texto.isEmpty()) {
            JOptionPane.showMessageDialog(this,
                "Por favor, escribe al menos un premio en la lista",
                "Lista vacía",
                JOptionPane.WARNING_MESSAGE);
            return;
        }

        // Obtener cantidad del campo de cantidad
        int cantidad = 1;
        try {
            String txtCantidad = txtCantidadPremio.getText().trim();
            if (!txtCantidad.isEmpty()) {
                cantidad = Integer.parseInt(txtCantidad);
                if (cantidad < 1) {
                    JOptionPane.showMessageDialog(this,
                        "La cantidad debe ser un número mayor a 0",
                        "Cantidad inválida",
                        JOptionPane.WARNING_MESSAGE);
                    return;
                }
            }
        } catch (NumberFormatException e) {
            JOptionPane.showMessageDialog(this,
                "La cantidad debe ser un número válido",
                "Error",
                JOptionPane.ERROR_MESSAGE);
            return;
        }

        // Dividir por líneas
        String[] lineas = texto.split("\n");
        int insertados = 0;

        for (String linea : lineas) {
            linea = linea.trim();
            if (!linea.isEmpty()) {
                // Alternar colores
                Color color = coloresPremios[listaPremios.size() % coloresPremios.length];
                listaPremios.insertarDerecha(linea, color, cantidad);
                insertados++;
            }
        }

        if (insertados > 0) {
            JOptionPane.showMessageDialog(this,
                "Se insertaron " + insertados + " premio(s) con cantidad " + cantidad + " en la ruleta",
                "Éxito",
                JOptionPane.INFORMATION_MESSAGE);
            txtListaPremios.setText(""); // Limpiar lista
            txtCantidadPremio.setText("1"); // Restablecer cantidad a 1
            repintarRuletas();
        } else {
            JOptionPane.showMessageDialog(this,
                "No se encontraron premios válidos en la lista",
                "Error",
                JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_btnAgregarPremioActionPerformed

    private void btnEliminarPremioActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnEliminarPremioActionPerformed
        if (!listaPremios.estaVacia()) {
            clsNodoDoble eliminado = listaPremios.eliminarActual();
            JOptionPane.showMessageDialog(this, "Premio eliminado: " + eliminado.getNombre());
            repintarRuletas();
        } else {
            JOptionPane.showMessageDialog(this, "No hay premios para eliminar", "Error", JOptionPane.ERROR_MESSAGE);
        }
    }//GEN-LAST:event_btnEliminarPremioActionPerformed

    private void btnJugarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnJugarActionPerformed
        if (listaJugadores.estaVacia()) {
            JOptionPane.showMessageDialog(this, "No hay jugadores en la ruleta", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        // Simular giro aleatorio (5-15 giros)
        int giros = (int) (Math.random() * 11) + 5;

        // Crear un Timer para animación
        Timer timer = new Timer(100, null);
        final int[] contador = {0};

        timer.addActionListener(e -> {
            listaJugadores.girarDerecha();
            repintarRuletas();
            contador[0]++;

            if (contador[0] >= giros) {
                timer.stop();
                clsNodoDoble ganador = listaJugadores.obtenerNodoActual();
                JOptionPane.showMessageDialog(this,
                    "¡El ganador es: " + ganador.getNombre() + "!",
                    "Resultado",
                    JOptionPane.INFORMATION_MESSAGE);
            }
        });

        timer.start();
    }//GEN-LAST:event_btnJugarActionPerformed

    private void btnReclamarActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnReclamarActionPerformed
        if (listaPremios.estaVacia()) {
            JOptionPane.showMessageDialog(this, "No hay premios en la ruleta", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        // Simular giro aleatorio (5-15 giros) - misma velocidad que jugadores
        int giros = (int) (Math.random() * 11) + 5;

        // Crear un Timer para animación
        Timer timer = new Timer(100, null);
        final int[] contador = {0};

        timer.addActionListener(e -> {
            listaPremios.girarDerecha();
            repintarRuletas();
            contador[0]++;

            if (contador[0] >= giros) {
                timer.stop();
                clsNodoDoble premioSeleccionado = listaPremios.obtenerNodoActual();
                JOptionPane.showMessageDialog(this,
                    "¡Premio seleccionado: " + premioSeleccionado.getNombre() + "!",
                    "Resultado",
                    JOptionPane.INFORMATION_MESSAGE);
            }
        });

        timer.start();
    }//GEN-LAST:event_btnReclamarActionPerformed

    private void btnIniciarJuegoActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_btnIniciarJuegoActionPerformed
        // Validar que haya jugadores y premios
        if (listaJugadores.estaVacia()) {
            JOptionPane.showMessageDialog(this,
                "No hay jugadores en la ruleta",
                "Error",
                JOptionPane.ERROR_MESSAGE);
            return;
        }

        if (listaPremios.estaVacia()) {
            JOptionPane.showMessageDialog(this,
                "No hay premios en la ruleta",
                "Error",
                JOptionPane.ERROR_MESSAGE);
            return;
        }

        // Deshabilitar el botón durante la animación
        btnIniciarJuego.setEnabled(false);

        // Simular giro aleatorio (10-20 giros)
        int girosJugadores = (int) (Math.random() * 11) + 10;
        int girosPremios = (int) (Math.random() * 11) + 10;

        // Crear un Timer para animación
        Timer timer = new Timer(100, null);
        final int[] contador = {0};
        final int totalGiros = Math.max(girosJugadores, girosPremios);

        timer.addActionListener(e -> {
            // Girar la ruleta de jugadores si aún le quedan giros
            if (contador[0] < girosJugadores) {
                listaJugadores.girarDerecha();
            }

            // Girar la ruleta de premios si aún le quedan giros
            if (contador[0] < girosPremios) {
                listaPremios.girarDerecha();
            }

            repintarRuletas();
            contador[0]++;

            // Detener cuando ambas ruletas hayan terminado
            if (contador[0] >= totalGiros) {
                timer.stop();
                btnIniciarJuego.setEnabled(true);

                // Mostrar resultado con diálogo personalizado
                clsNodoDoble jugadorGanador = listaJugadores.obtenerNodoActual();
                clsNodoDoble premioGanador = listaPremios.obtenerNodoActual();

                mostrarDialogoResultado(jugadorGanador, premioGanador);
            }
        });

        timer.start();
    }//GEN-LAST:event_btnIniciarJuegoActionPerformed

    /**
     * @param args the command line arguments
     */
    public static void main(String args[]) {
        /* Set the Nimbus look and feel */
        //<editor-fold defaultstate="collapsed" desc=" Look and feel setting code (optional) ">
        /* If Nimbus (introduced in Java SE 6) is not available, stay with the default look and feel.
         * For details see http://download.oracle.com/javase/tutorial/uiswing/lookandfeel/plaf.html
         */
        try {
            for (javax.swing.UIManager.LookAndFeelInfo info : javax.swing.UIManager.getInstalledLookAndFeels()) {
                if ("Nimbus".equals(info.getName())) {
                    javax.swing.UIManager.setLookAndFeel(info.getClassName());
                    break;
                }
            }
        } catch (ClassNotFoundException ex) {
            java.util.logging.Logger.getLogger(frmRuleta.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(frmRuleta.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(frmRuleta.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(frmRuleta.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new frmRuleta().setVisible(true);
            }
        });
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnAgregarJugador;
    private javax.swing.JButton btnAgregarPremio;
    private javax.swing.JButton btnEliminarJugador;
    private javax.swing.JButton btnEliminarPremio;
    private javax.swing.JButton btnGirarDerJugador;
    private javax.swing.JButton btnGirarDerPremio;
    private javax.swing.JButton btnGirarIzqJugador;
    private javax.swing.JButton btnGirarIzqPremio;
    private javax.swing.JButton btnIniciarJuego;
    private javax.swing.JButton btnJugar;
    private javax.swing.JButton btnReclamar;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel panelJugadores;
    private javax.swing.JPanel panelPremio;
    private javax.swing.JTextField txtJugador;
    private javax.swing.JTextField txtPremio;
    // End of variables declaration//GEN-END:variables
}
