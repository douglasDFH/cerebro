/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/GUIForms/JFrame.java to edit this template
 */
package presentacion;
import negocio.clsPila;
import negocio.clsCola;
import negocio.clsListaSimple;
import negocio.clsListaDoble;
import java.awt.Graphics;

import negocio.clsNodo;
import negocio.clsNodoDoble;
/**
 *
 * @author dell
 */
public class frmprincipal extends javax.swing.JFrame {

    /**
     * Creates new form frmprincipal
     */
    private final clsPila objPila;
    private final clsCola objCola;
    private Graphics objPintor;
    private clsListaSimple objListaSimple;
    private clsListaDoble objListaDoble;
    
    // Sistema de análisis gráfico
    private java.util.ArrayList<OperacionAnalisis> historialOperaciones;
    private class OperacionAnalisis {
        String estructura;      // "PILA", "COLA", "LISTA_SIMPLE", "LISTA_DOBLE"
        String operacion;       // "INSERTAR", "ELIMINAR", "MOVER_PUNTERO", "VACIAR"
        String valor;           // valor insertado/eliminado o posición del puntero
        String estadoAntes;     // representación del estado antes de la operación
        String estadoDespues;   // representación del estado después de la operación
        
        public OperacionAnalisis(String estructura, String operacion, String valor, 
                               String antes, String despues) {
            this.estructura = estructura;
            this.operacion = operacion;
            this.valor = valor != null ? valor : "";
            this.estadoAntes = antes;
            this.estadoDespues = despues;
        }
    }
    // (Los controles específicos por pestaña no se usan; usamos los botones y txtDato del form)
    public frmprincipal() {
        initComponents();
        objPila = new clsPila();
        objCola = new clsCola();
        objListaSimple = new clsListaSimple();
        objListaDoble = new clsListaDoble();
        objPintor = getGraphics();
        
        // Inicializar sistema de análisis
        historialOperaciones = new java.util.ArrayList<>();
        // Configurar event listener para el tabbedPane del .form
        if (tabbedPane != null) {
            tabbedPane.addChangeListener(e -> {
                int selectedIndex = tabbedPane.getSelectedIndex();
                adaptarControlesSegunPestaña(selectedIndex);
                drawSelected();
            });
            tabbedPane.setSelectedIndex(0);
            adaptarControlesSegunPestaña(0); // Configurar interfaz inicial
        }
        // Forzar repaint inicial
        this.repaint();
    }






    private void adaptarControlesSegunPestaña(int selectedIndex) {
        // selectedIndex: 0=Pila, 1=Cola, 2=Lista Simple, 3=Lista Doble esta son las pestañas segun la necesidad 
        
        // Controles básicos siempre visibles
        dato.setVisible(true);
        txtDato.setVisible(true);
        jButton1.setVisible(true); // Insertar
        jButton2.setVisible(true); // Eliminar
        
        if (selectedIndex == 0) { // Pestaña Pila
            // Solo controles básicos para Pila
            btnInsertarOrdenado.setVisible(false);
            btnInsertarDerecha.setVisible(false);
            btnEliminarDerecha.setVisible(false);
            btnOrdenarAsc.setVisible(false);
            btnOrdenarRef.setVisible(false);
            btnSumarElementos.setVisible(false);
            jButtonBuscar.setVisible(true);
            jButtonVaciar.setVisible(true);
            jButton3.setVisible(false); // Ocultar "Mostrar Pila" cuando ya estamos en Pila
            // Ocultar botones específicos de Lista Doble
            btnInsertarIzquierda.setVisible(false);
            btnEliminarIzquierda.setVisible(false);
            btnRecorridoForward.setVisible(false);
            btnRecorridoBackward.setVisible(false);
            btnPermutarNodos.setVisible(false);
            
        } else if (selectedIndex == 1) { // Pestaña Cola
            // Solo controles básicos para Cola
            btnInsertarOrdenado.setVisible(false);
            btnInsertarDerecha.setVisible(false);
            btnEliminarDerecha.setVisible(false);
            btnOrdenarAsc.setVisible(false);
            btnOrdenarRef.setVisible(false);
            btnSumarElementos.setVisible(false);
            jButtonBuscar.setVisible(true);
            jButtonVaciar.setVisible(true);
            jButton3.setVisible(true); // Mostrar información de cola
            // Ocultar botones específicos de Lista Doble
            btnInsertarIzquierda.setVisible(false);
            btnEliminarIzquierda.setVisible(false);
            btnRecorridoForward.setVisible(false);
            btnRecorridoBackward.setVisible(false);
            btnPermutarNodos.setVisible(false);
            
        } else if (selectedIndex == 2) { // Pestaña Lista Simple
            // Todos los controles avanzados visibles
            btnInsertarOrdenado.setVisible(true);
            btnInsertarDerecha.setVisible(true);
            btnEliminarDerecha.setVisible(true);
            btnOrdenarAsc.setVisible(true);
            btnOrdenarRef.setVisible(true);
            btnSumarElementos.setVisible(true);
            jButtonBuscar.setVisible(true);
            jButtonVaciar.setVisible(true);
            jButton3.setVisible(false);
            // Ocultar botones específicos de Lista Doble
            btnInsertarIzquierda.setVisible(false);
            btnEliminarIzquierda.setVisible(false);
            btnRecorridoForward.setVisible(false);
            btnRecorridoBackward.setVisible(false);
            btnPermutarNodos.setVisible(false);
            
        } else if (selectedIndex == 3) { // Pestaña Lista Doble
            // Todos los controles para Lista Doble (incluyendo posición y botones específicos)
            btnInsertarOrdenado.setVisible(true);
            btnInsertarDerecha.setVisible(true);
            btnEliminarDerecha.setVisible(true);
            btnOrdenarAsc.setVisible(true);
            btnOrdenarRef.setVisible(false); // No implementado para Lista Doble
            btnSumarElementos.setVisible(true);
            jButtonBuscar.setVisible(true);
            jButtonVaciar.setVisible(true);
            jButton3.setVisible(false);
            // Botones específicos de Lista Doble
            btnInsertarIzquierda.setVisible(true);
            btnEliminarIzquierda.setVisible(true);
            btnRecorridoForward.setVisible(true);
            btnRecorridoBackward.setVisible(true);
            btnPermutarNodos.setVisible(true);
        }
        
        this.revalidate();
        this.repaint();
    }

    // Método eliminado - mantenemos tooltips originales

    @Override
    public void paint(Graphics g){
        super.paint(g);
        // Mantener objPintor para compatibilidad con tu diseño
        objPintor = g;
        drawSelected();
    }

    private void drawSelected(){
        if (objPintor == null) objPintor = getGraphics();
        if (objPintor == null) return;
        if (tabbedPane == null) return;
        int idx = tabbedPane.getSelectedIndex();
        
        // Actualizar información del puntero según la pestaña seleccionada
        switch(idx){
            case 0: 
                graficarPila(objPila.getCima()); 
                lblInfoPuntero.setText("Puntero: " + objPila.obtenerInfoPuntero());
                break; // Pila
            case 1: 
                graficarCola(objCola.getPrimero()); 
                lblInfoPuntero.setText("Puntero: " + objCola.obtenerInfoPuntero());
                break; // Cola
            case 2: 
                graficarListaSimple(); 
                lblInfoPuntero.setText("Puntero: " + (objListaSimple.getPunteroActual() != null ? objListaSimple.getPunteroActual().getDato() : "Nulo"));
                break; // Lista Simple
            case 3: 
                graficarListaDoble(); 
                lblInfoPuntero.setText("Puntero: " + (objListaDoble.getPunteroActual() != null ? objListaDoble.getPunteroActual().getDato() : "Nulo"));
                break; // Lista Doble
        }
        repaint(); // Asegurar actualización visual
    }

    public void graficarCola(clsNodo primero){
        if (objPintor == null) objPintor = getGraphics();
        if (objPintor == null) return;
        // Limpiar área de dibujo en la zona de pestañas
        objPintor.clearRect(50, 50, 600, 200);
        //objPintor.drawString("COLA", 60, 70);
        objPintor.drawString("(FIFO - Primero en entrar, primero en salir)", 60, 85);
        int j = 0;
        clsNodo n = primero;
        clsNodo puntero = objCola.getPunteroActual();
        while(n != null){
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (n == puntero);
            
            // Dibujar nodo normal
            objPintor.drawRect(80 + j*60, 105, 40, 20);
            objPintor.drawString(""+n.getDato(), 85 + j*60, 120);
            
            // Dibujar indicador del puntero si está activado y es el nodo correcto
            if (esPuntero) {
                dibujarIndicadorPuntero(80 + j*60, 105);
            }
            
            if(n.getRef() != null) {
                // Dibujar flecha hacia el siguiente
                objPintor.drawString("->", 80 + j*60 + 45, 120);
            }
            n = n.getRef();
            j++;
        }
        if(primero == null) {
            objPintor.drawString("Cola vacía", 80, 125);
        }
    }

    public void graficarListaSimple(){
        if (objPintor == null) objPintor = getGraphics();
        if (objPintor == null) return;
        // Limpiar área de dibujo en la zona de pestañas
        objPintor.clearRect(50, 50, 600, 200);
       // objPintor.drawString("LISTA SIMPLE", 60, 70);
        objPintor.drawString("(Cabeza -> ... -> null)", 60, 85);
        int j = 0;
        clsNodo n = objListaSimple == null ? null : objListaSimple.getCabeza();
        clsNodo puntero = objListaSimple.getPunteroActual();
        while(n != null){
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (n == puntero);
            
            // Dibujar nodo normal
            objPintor.drawRect(80 + j*60, 105, 40, 20);
            objPintor.drawString(""+n.getDato(), 85 + j*60, 120);
            
            // Dibujar indicador del puntero si está activado y es el nodo correcto
            if (esPuntero) {
                dibujarIndicadorPuntero(80 + j*60, 105);
            }
            
            if(n.getRef() != null) {
                objPintor.drawString("->", 80 + j*60 + 45, 120);
            }
            n = n.getRef();
            j++;
        }
        if(objListaSimple.getCabeza() == null) {
            objPintor.drawString("Lista Simple vacía", 80, 125);
        }
    }

    public void graficarListaDoble(){
        if (objPintor == null) objPintor = getGraphics();
        if (objPintor == null) return;
        // Limpiar área de dibujo en la zona de pestañas
        objPintor.clearRect(50, 50, 600, 200);
        //objPintor.drawString("LISTA DOBLE", 60, 70);
        objPintor.drawString("(null <- cabeza <-> ... <-> cola -> null)", 60, 85);
        clsNodoDoble cur = objListaDoble == null ? null : objListaDoble.getCabeza();
        clsNodoDoble puntero = objListaDoble.getPunteroActual();
        int j = 0;
        while(cur != null){
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (cur == puntero);
            
            // Dibujar nodo normal
            objPintor.drawRect(80 + j*70, 105, 40, 20);
            objPintor.drawString(""+cur.getDato(), 85 + j*70, 120);
            
            // Dibujar indicador del puntero si está activado y es el nodo correcto
            if (esPuntero) {
                dibujarIndicadorPuntero(80 + j*70, 105);
            }
            
            if(cur.getNext() != null) {
                objPintor.drawString("<->", 80 + j*70 + 45, 120);
            }
            cur = cur.getNext();
            j++;
        }
        if(objListaDoble.getCabeza() == null) {
            objPintor.drawString("Lista Doble vacía", 80, 125);
        }
    }

    /**
     * This method is called from within the constructor to initialize the form.
     * WARNING: Do NOT modify this code. The content of this method is always
     * regenerated by the Form Editor.
     */
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        tabbedPane = new javax.swing.JTabbedPane();
        btnEliminarDerecha = new javax.swing.JButton();
        btnInsertarDerecha = new javax.swing.JButton();
        btnInsertarOrdenado = new javax.swing.JButton();
        btnOrdenarAsc = new javax.swing.JButton();
        btnOrdenarRef = new javax.swing.JButton();
        btnSumarElementos = new javax.swing.JButton();
        // Nuevos botones para Lista Doble
        btnInsertarIzquierda = new javax.swing.JButton();
        btnEliminarIzquierda = new javax.swing.JButton();
        btnRecorridoForward = new javax.swing.JButton();
        btnRecorridoBackward = new javax.swing.JButton();
        btnPermutarNodos = new javax.swing.JButton();
        // Botones de navegación del puntero
        btnPunteroInicio = new javax.swing.JButton();
        btnPunteroSiguiente = new javax.swing.JButton();
        btnPunteroAnterior = new javax.swing.JButton();
        btnPunteroFinal = new javax.swing.JButton();
        btnTogglePuntero = new javax.swing.JButton();
        btnAnalisis = new javax.swing.JButton();
        lblInfoPuntero = new javax.swing.JLabel();
        dato = new javax.swing.JLabel();
        jButton1 = new javax.swing.JButton();
        jButton2 = new javax.swing.JButton();
        jButton3 = new javax.swing.JButton();
        jButtonBuscar = new javax.swing.JButton();
        jButtonVaciar = new javax.swing.JButton();
        txtDato = new javax.swing.JTextField();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);

        tabbedPane.addTab("Pila", new javax.swing.JLabel(""));
        tabbedPane.addTab("Cola", new javax.swing.JLabel(""));
        tabbedPane.addTab("Lista Simple", new javax.swing.JLabel(""));
        tabbedPane.addTab("Lista Doble", new javax.swing.JLabel(""));

        jButton1.setText("Insertar");
        jButton1.setToolTipText("Insertar el valor en la estructura seleccionada");
        jButton1.addActionListener(evt -> jButton1ActionPerformed(evt));

        jButton2.setText("Eliminar");
        jButton2.setToolTipText("Eliminar (pop/poll/eliminarInicio) en la estructura seleccionada");
        jButton2.addActionListener(evt -> jButton2ActionPerformed(evt));

        dato.setText("dato");
        txtDato.setToolTipText("Ingrese un número entero aquí");

        jButton3.setText("Mostrar Pila");
        jButton3.setToolTipText("Ir a la pestaña Pila");
        jButton3.addActionListener(evt -> jButton3ActionPerformed(evt));

        jButtonBuscar.setText("Buscar");
        jButtonBuscar.setToolTipText("Buscar valor en la estructura seleccionada");
        jButtonBuscar.addActionListener(evt -> jButtonBuscarActionPerformed(evt));

        jButtonVaciar.setText("Vaciar");
        jButtonVaciar.setToolTipText("Vaciar la estructura seleccionada");
        jButtonVaciar.addActionListener(evt -> jButtonVaciarActionPerformed(evt));

        btnInsertarOrdenado.setText("Ins. Ordenado");
        btnInsertarOrdenado.addActionListener(evt -> btnInsertarOrdenadoActionPerformed(evt));

        btnInsertarDerecha.setText("Ins. Derecha");
        btnInsertarDerecha.setToolTipText("Insertar a la derecha de la posición del puntero actual");
        btnInsertarDerecha.addActionListener(evt -> btnInsertarDerechaActionPerformed(evt));

        btnEliminarDerecha.setText("Elim. Derecha");
        btnEliminarDerecha.setToolTipText("Eliminar el nodo a la derecha de la posición del puntero actual");
        btnEliminarDerecha.addActionListener(evt -> btnEliminarDerechaActionPerformed(evt));

        btnOrdenarAsc.setText("Ordenar Asc.");
        btnOrdenarAsc.addActionListener(evt -> btnOrdenarAscActionPerformed(evt));

        btnOrdenarRef.setText("Ordenar Ref.");
        btnOrdenarRef.addActionListener(evt -> btnOrdenarRefActionPerformed(evt));

        btnSumarElementos.setText("Sumar");
        btnSumarElementos.addActionListener(evt -> btnSumarElementosActionPerformed(evt));

        // Configuración de botones específicos para Lista Doble
        btnInsertarIzquierda.setText("Ins. Izq.");
        btnInsertarIzquierda.setToolTipText("Insertar a la izquierda de la posición del puntero actual");
        btnInsertarIzquierda.addActionListener(evt -> btnInsertarIzquierdaActionPerformed(evt));

        btnEliminarIzquierda.setText("Elim. Izq.");
        btnEliminarIzquierda.setToolTipText("Eliminar el nodo a la izquierda de la posición del puntero actual");
        btnEliminarIzquierda.addActionListener(evt -> btnEliminarIzquierdaActionPerformed(evt));

        btnRecorridoForward.setText("→ Adelante");
        btnRecorridoForward.addActionListener(evt -> btnRecorridoForwardActionPerformed(evt));

        btnRecorridoBackward.setText("← Atrás");
        btnRecorridoBackward.addActionListener(evt -> btnRecorridoBackwardActionPerformed(evt));

        btnPermutarNodos.setText("🔀 Permutar");
        btnPermutarNodos.setToolTipText("Permutar (intercambiar) dos nodos por posición");
        btnPermutarNodos.addActionListener(evt -> btnPermutarNodosActionPerformed(evt));

        // Configuración de botones de navegación del puntero
        btnPunteroInicio.setText("⟦⤶⟧");
        btnPunteroInicio.setToolTipText("Ir al inicio");
        btnPunteroInicio.addActionListener(evt -> btnPunteroInicioActionPerformed(evt));

        btnPunteroAnterior.setText("◀");
        btnPunteroAnterior.setToolTipText("Anterior");
        btnPunteroAnterior.addActionListener(evt -> btnPunteroAnteriorActionPerformed(evt));

        btnPunteroSiguiente.setText("▶");
        btnPunteroSiguiente.setToolTipText("Siguiente");
        btnPunteroSiguiente.addActionListener(evt -> btnPunteroSiguienteActionPerformed(evt));

        btnPunteroFinal.setText("⟦⤷⟧");
        btnPunteroFinal.setToolTipText("Ir al final");
        btnPunteroFinal.addActionListener(evt -> btnPunteroFinalActionPerformed(evt));

        btnTogglePuntero.setText("⬛");
        btnTogglePuntero.setToolTipText("Mostrar/Ocultar indicador visual del puntero");
        btnTogglePuntero.setBackground(java.awt.Color.BLUE);
        btnTogglePuntero.setForeground(java.awt.Color.WHITE);
        btnTogglePuntero.addActionListener(evt -> btnTogglePunteroActionPerformed(evt));

        btnAnalisis.setText("📊 Análisis");
        btnAnalisis.setToolTipText("Ver análisis paso a paso de las operaciones");
        btnAnalisis.setBackground(java.awt.Color.GREEN);
        btnAnalisis.setForeground(java.awt.Color.WHITE);
        btnAnalisis.addActionListener(evt -> btnAnalisisActionPerformed(evt));

        lblInfoPuntero.setText("Puntero: Inicio");
        lblInfoPuntero.setForeground(java.awt.Color.BLUE);

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(tabbedPane)
            .addGroup(layout.createSequentialGroup()
                .addGap(10, 10, 10)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(dato, javax.swing.GroupLayout.PREFERRED_SIZE, 37, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addGap(5, 5, 5)
                        .addComponent(txtDato, javax.swing.GroupLayout.PREFERRED_SIZE, 71, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnInsertarOrdenado)
                        .addGap(5, 5, 5)
                        .addComponent(btnInsertarDerecha)
                        .addGap(5, 5, 5)
                        .addComponent(btnEliminarDerecha)
                        .addGap(5, 5, 5)
                        .addComponent(btnOrdenarAsc))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnOrdenarRef)
                        .addGap(10, 10, 10)
                        .addComponent(btnSumarElementos)
                        .addGap(20, 20, 20)
                        .addComponent(jButton1)
                        .addGap(10, 10, 10)
                        .addComponent(jButton2)
                        .addGap(10, 10, 10)
                        .addComponent(jButtonBuscar)
                        .addGap(10, 10, 10)
                        .addComponent(jButtonVaciar))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnInsertarIzquierda)
                        .addGap(10, 10, 10)
                        .addComponent(btnEliminarIzquierda)
                        .addGap(10, 10, 10)
                        .addComponent(btnRecorridoForward)
                        .addGap(10, 10, 10)
                        .addComponent(btnRecorridoBackward)
                        .addGap(10, 10, 10)
                        .addComponent(btnPermutarNodos))
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(btnPunteroInicio)
                        .addGap(5, 5, 5)
                        .addComponent(btnPunteroAnterior)
                        .addGap(5, 5, 5)
                        .addComponent(btnTogglePuntero)
                        .addGap(5, 5, 5)
                        .addComponent(btnPunteroSiguiente)
                        .addGap(5, 5, 5)
                        .addComponent(btnPunteroFinal)
                        .addGap(15, 15, 15)
                        .addComponent(lblInfoPuntero)
                        .addGap(20, 20, 20)
                        .addComponent(btnAnalisis))
                    .addGroup(javax.swing.GroupLayout.Alignment.CENTER, layout.createSequentialGroup()
                        .addGap(150, 150, 150)
                        .addComponent(jButton3)))
                .addContainerGap(20, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(tabbedPane, javax.swing.GroupLayout.PREFERRED_SIZE, 200, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(20, 20, 20)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(dato)
                    .addComponent(txtDato, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(20, 20, 20)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnInsertarOrdenado)
                    .addComponent(btnInsertarDerecha)
                    .addComponent(btnEliminarDerecha)
                    .addComponent(btnOrdenarAsc))
                .addGap(10, 10, 10)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnOrdenarRef)
                    .addComponent(btnSumarElementos)
                    .addComponent(jButton1)
                    .addComponent(jButton2)
                    .addComponent(jButtonBuscar)
                    .addComponent(jButtonVaciar))
                .addGap(10, 10, 10)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnInsertarIzquierda)
                    .addComponent(btnEliminarIzquierda)
                    .addComponent(btnRecorridoForward)
                    .addComponent(btnRecorridoBackward)
                    .addComponent(btnPermutarNodos))
                .addGap(10, 10, 10)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(btnPunteroInicio)
                    .addComponent(btnPunteroAnterior)
                    .addComponent(btnTogglePuntero)
                    .addComponent(btnPunteroSiguiente)
                    .addComponent(btnPunteroFinal)
                    .addComponent(lblInfoPuntero)
                    .addComponent(btnAnalisis))
                .addGap(10, 10, 10)
                .addComponent(jButton3)
                .addGap(25, 25, 25))
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
     try{
         int dato = Integer.parseInt(txtDato.getText());
         int idx = tabbedPane.getSelectedIndex();
         
         // Verificar si las estructuras están vacías antes de insertar
         boolean eraVacia = false;
         switch(idx){
             case 0: 
                 eraVacia = objPila.estaVacia();
                 String estadoAntes0 = obtenerEstadoPila();
                 objPila.insert(dato); 
                 String estadoDespues0 = obtenerEstadoPila();
                 registrarOperacion("PILA", "PUSH", String.valueOf(dato), estadoAntes0, estadoDespues0);
                 // Inicializar puntero automáticamente si era la primera inserción
                 if (eraVacia) objPila.moverPunteroInicio();
                 break; // Pila
             case 1: 
                 eraVacia = objCola.estaVacia();
                 String estadoAntes1 = obtenerEstadoCola();
                 objCola.insertar(dato); 
                 String estadoDespues1 = obtenerEstadoCola();
                 registrarOperacion("COLA", "ENQUEUE", String.valueOf(dato), estadoAntes1, estadoDespues1);
                 // Inicializar puntero automáticamente si era la primera inserción
                 if (eraVacia) objCola.moverPunteroInicio();
                 break; // Cola
             case 2: 
                 eraVacia = objListaSimple.estaVacia();
                 String estadoAntes2 = obtenerEstadoListaSimple();
                 objListaSimple.insertarFinal(dato); 
                 String estadoDespues2 = obtenerEstadoListaSimple();
                 registrarOperacion("LISTA_SIMPLE", "INSERTAR_FINAL", String.valueOf(dato), estadoAntes2, estadoDespues2);
                 // Inicializar puntero automáticamente si era la primera inserción
                 if (eraVacia) objListaSimple.moverPunteroInicio();
                 break; // Lista Simple
             case 3: 
                 eraVacia = objListaDoble.estaVacia();
                 String estadoAntes3 = obtenerEstadoListaDoble();
                 objListaDoble.insertarFinal(dato); 
                 String estadoDespues3 = obtenerEstadoListaDoble();
                 registrarOperacion("LISTA_DOBLE", "INSERTAR_FINAL", String.valueOf(dato), estadoAntes3, estadoDespues3);
                 // Inicializar puntero automáticamente si era la primera inserción
                 if (eraVacia) objListaDoble.moverPunteroInicio();
                 break; // Lista Doble
         }
         txtDato.setText(""); // Limpiar campo después de insertar
         drawSelected();
     }catch(Exception ex){ System.out.println("Valor inválido"); }
    }//GEN-LAST:event_jButton1ActionPerformed

    private void jButton2ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton2ActionPerformed
         int idx = tabbedPane.getSelectedIndex();
         int dato = -1;
         switch(idx){
              case 0: 
                  String estadoAntes0 = obtenerEstadoPila();
                  dato = objPila.eliminar(); 
                  String estadoDespues0 = obtenerEstadoPila();
                  if (dato != -1) {
                      registrarOperacion("PILA", "POP", String.valueOf(dato), estadoAntes0, estadoDespues0);
                  }
                  break; // Pila
              case 1: 
                  String estadoAntes1 = obtenerEstadoCola();
                  dato = objCola.eliminar(); 
                  String estadoDespues1 = obtenerEstadoCola();
                  if (dato != -1) {
                      registrarOperacion("COLA", "DEQUEUE", String.valueOf(dato), estadoAntes1, estadoDespues1);
                  }
                  break; // Cola
              case 2: 
                  String estadoAntes2 = obtenerEstadoListaSimple();
                  dato = objListaSimple.eliminarInicio(); 
                  String estadoDespues2 = obtenerEstadoListaSimple();
                  if (dato != -1) {
                      registrarOperacion("LISTA_SIMPLE", "ELIMINAR_INICIO", String.valueOf(dato), estadoAntes2, estadoDespues2);
                  }
                  break; // Lista Simple
              case 3: 
                  String estadoAntes3 = obtenerEstadoListaDoble();
                  dato = objListaDoble.eliminarInicio(); 
                  String estadoDespues3 = obtenerEstadoListaDoble();
                  if (dato != -1) {
                      registrarOperacion("LISTA_DOBLE", "ELIMINAR_INICIO", String.valueOf(dato), estadoAntes3, estadoDespues3);
                  }
                  break; // Lista Doble
         }
         System.out.println("dato"+dato);
         drawSelected();
    }//GEN-LAST:event_jButton2ActionPerformed

    private void jButton3ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton3ActionPerformed
        int idx = tabbedPane.getSelectedIndex();
        switch(idx){
            case 0: // Pila - Mostrar información en consola
                if(objPila.getCima() != null) {
                    System.out.println("Cima: " + objPila.getCima().getDato());
                } else {
                    System.out.println("Pila vacía");
                }
                break;
            case 1: // Cola - Mostrar información
                if(objCola.getPrimero() != null) {
                    System.out.println("Primero en cola: " + objCola.getPrimero().getDato());
                    System.out.println("Último en cola: " + objCola.getUltimo().getDato());
                } else {
                    System.out.println("Cola vacía");
                }
                break;
            case 2: // Lista Simple - Función especial (recorrer)
                String recorridoLS = objListaSimple.toString();
                System.out.println("Recorrido Lista Simple: " + recorridoLS);
                break;
            case 3: // Lista Doble - Recorrer
                String recorrido = objListaDoble.recorridoForward();
                System.out.println("Recorrido Lista Doble: " + recorrido);
                break;
        }
        drawSelected();
    }//GEN-LAST:event_jButton3ActionPerformed

    public clsPila getPila(){
        return this.objPila;
    }
    
    public void graficarNodo(int px, int py, String dato){
          objPintor.drawRect(px, py, 40 ,20 );
          objPintor.drawString(dato, px+5, py+15);
    }
    
    public void graficarNodoConPuntero(int px, int py, String dato, boolean esPuntero){
        // Guardar color original
        java.awt.Color colorOriginal = objPintor.getColor();
        
        if (esPuntero) {
            // Cambiar a color rojo para resaltar el puntero
            objPintor.setColor(java.awt.Color.RED);
            objPintor.fillRect(px, py, 40, 20);
            objPintor.setColor(java.awt.Color.WHITE);
            objPintor.drawString(dato, px+5, py+15);
            objPintor.setColor(java.awt.Color.RED);
            objPintor.drawRect(px, py, 40, 20);
        } else {
            // Nodo normal
            objPintor.drawRect(px, py, 40, 20);
            objPintor.drawString(dato, px+5, py+15);
        }
        
        // Restaurar color original
        objPintor.setColor(colorOriginal);
    }
    
    public void graficarNodoDobleConPuntero(int px, int py, String dato, boolean esPuntero){
        // Guardar color original
        java.awt.Color colorOriginal = objPintor.getColor();
        
        if (esPuntero) {
            // Cambiar a color azul para resaltar el puntero en lista doble
            objPintor.setColor(java.awt.Color.BLUE);
            objPintor.fillRect(px, py, 40, 20);
            objPintor.setColor(java.awt.Color.WHITE);
            objPintor.drawString(dato, px+5, py+15);
            objPintor.setColor(java.awt.Color.BLUE);
            objPintor.drawRect(px, py, 40, 20);
        } else {
            // Nodo normal
            objPintor.drawRect(px, py, 40, 20);
            objPintor.drawString(dato, px+5, py+15);
        }
        
        // Restaurar color original
        objPintor.setColor(colorOriginal);
    }
    
    /**
     * Dibujar indicador visual del puntero (cuadrado azul superpuesto)
     */
    public void dibujarIndicadorPuntero(int px, int py) {
        if (mostrarPunteroVisual) {
            // Guardar color original
            java.awt.Color colorOriginal = objPintor.getColor();
            
            // Dibujar cuadrado azul semitransparente más grande que el nodo
            objPintor.setColor(new java.awt.Color(0, 0, 255, 100)); // Azul semitransparente
            objPintor.fillRect(px - 3, py - 3, 46, 26); // Ligeramente más grande que el nodo
            
            // Dibujar borde azul sólido
            objPintor.setColor(java.awt.Color.BLUE);
            objPintor.drawRect(px - 3, py - 3, 46, 26);
            objPintor.drawRect(px - 2, py - 2, 44, 24); // Doble borde para mayor visibilidad
            
            // Restaurar color original
            objPintor.setColor(colorOriginal);
        }
    }
    
    public void graficarPila(clsNodo cima){
        if (objPintor == null) objPintor = getGraphics();
        if (objPintor == null) return;
        // Limpiar área de dibujo en la zona de pestañas
        objPintor.clearRect(50, 50, 600, 200);
      //  objPintor.drawString("PILA", 60, 70);
        objPintor.drawString("(LIFO - Último en entrar, primero en salir)", 60, 85);
        objPintor.drawString("CIMA", 150, 100);
        objPintor.drawString("|", 160, 110);
        objPintor.drawString("v", 160, 120);
        int j = 0;
        clsNodo actual = cima;
        clsNodo puntero = objPila.getPunteroActual();
        while(actual != null){
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (actual == puntero);
            
            // Dibujar nodo normal
            objPintor.drawRect(140, 125 + j*30, 40, 20);
            objPintor.drawString(""+actual.getDato(), 145, 140 + j*30);
            
            // Dibujar indicador del puntero si está activado y es el nodo correcto
            if (esPuntero) {
                dibujarIndicadorPuntero(140, 125 + j*30);
            }
            
            if(actual.getRef() != null) {
                objPintor.drawString("|", 160, 150 + j*30);
                objPintor.drawString("v", 160, 160 + j*30);
            }
            actual = actual.getRef();
            j++;
        }
        if(cima == null) {
            objPintor.drawString("Pila vacía", 140, 135);
        }
    }
    
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
            java.util.logging.Logger.getLogger(frmprincipal.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (InstantiationException ex) {
            java.util.logging.Logger.getLogger(frmprincipal.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (IllegalAccessException ex) {
            java.util.logging.Logger.getLogger(frmprincipal.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        } catch (javax.swing.UnsupportedLookAndFeelException ex) {
            java.util.logging.Logger.getLogger(frmprincipal.class.getName()).log(java.util.logging.Level.SEVERE, null, ex);
        }
        //</editor-fold>

        /* Create and display the form */
        java.awt.EventQueue.invokeLater(() -> new frmprincipal().setVisible(true));
    }

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JButton btnEliminarDerecha;
    private javax.swing.JButton btnInsertarDerecha;
    private javax.swing.JButton btnInsertarOrdenado;
    private javax.swing.JButton btnOrdenarAsc;
    private javax.swing.JButton btnOrdenarRef;
    private javax.swing.JButton btnSumarElementos;
    // Botones específicos para Lista Doble
    private javax.swing.JButton btnInsertarIzquierda;
    private javax.swing.JButton btnEliminarIzquierda;
    private javax.swing.JButton btnRecorridoForward;
    private javax.swing.JButton btnRecorridoBackward;
    private javax.swing.JButton btnPermutarNodos;
    // Botones de navegación del puntero
    private javax.swing.JButton btnPunteroInicio;
    private javax.swing.JButton btnPunteroSiguiente;
    private javax.swing.JButton btnPunteroAnterior;
    private javax.swing.JButton btnPunteroFinal;
    private javax.swing.JButton btnTogglePuntero;
    private javax.swing.JButton btnAnalisis;
    private javax.swing.JLabel lblInfoPuntero;
    // Control de visualización del puntero
    private boolean mostrarPunteroVisual = true;
    private javax.swing.JLabel dato;
    private javax.swing.JButton jButton1;
    private javax.swing.JButton jButton2;
    private javax.swing.JButton jButton3;
    private javax.swing.JButton jButtonBuscar;
    private javax.swing.JButton jButtonVaciar;
    private javax.swing.JTabbedPane tabbedPane;
    private javax.swing.JTextField txtDato;
    // End of variables declaration//GEN-END:variables
    


    private void jButtonBuscarActionPerformed(java.awt.event.ActionEvent evt) {
        try{
            int val = Integer.parseInt(txtDato.getText());
            int idx = tabbedPane.getSelectedIndex();
            boolean found = false;
            String mensaje = "";
            String titulo = "";
            
            switch(idx){
                case 0: // Pila: buscar en toda la pila
                    String estadoAntes0 = obtenerEstadoPila();
                    clsNodo p = objPila.getCima();
                    int posicionPila = 0;
                    java.util.ArrayList<Integer> posicionesPila = new java.util.ArrayList<>();
                    while(p != null){ 
                        if(p.getDato() == val){ 
                            found = true; 
                            posicionesPila.add(posicionPila);
                        } 
                        p = p.getRef(); 
                        posicionPila++;
                    }
                    String estadoDespues0 = obtenerEstadoPila();
                    registrarOperacion("PILA", "BUSCAR", String.valueOf(val), estadoAntes0, estadoDespues0);
                    
                    titulo = "Búsqueda en Pila";
                    if(found) {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "✅ Estado: ENCONTRADO\n";
                        if(posicionesPila.size() == 1) {
                            mensaje += "📍 Posición: " + posicionesPila.get(0);
                        } else {
                            mensaje += "📍 Posiciones encontradas: " + posicionesPila.toString() + "\n";
                            mensaje += "📊 Total de coincidencias: " + posicionesPila.size();
                        }
                    } else {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "❌ Estado: NO ENCONTRADO\n";
                        mensaje += "📍 El dato no existe en la pila";
                    }
                    break;
                    
                case 1: // Cola: buscar en toda la cola
                    String estadoAntes1 = obtenerEstadoCola();
                    clsNodo pCola = objCola.getPrimero();
                    int posicionCola = 0;
                    java.util.ArrayList<Integer> posicionesCola = new java.util.ArrayList<>();
                    while(pCola != null){ 
                        if(pCola.getDato() == val){ 
                            found = true; 
                            posicionesCola.add(posicionCola);
                        } 
                        pCola = pCola.getRef(); 
                        posicionCola++;
                    }
                    String estadoDespues1 = obtenerEstadoCola();
                    registrarOperacion("COLA", "BUSCAR", String.valueOf(val), estadoAntes1, estadoDespues1);
                    
                    titulo = "Búsqueda en Cola";
                    if(found) {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "✅ Estado: ENCONTRADO\n";
                        if(posicionesCola.size() == 1) {
                            mensaje += "📍 Posición: " + posicionesCola.get(0);
                        } else {
                            mensaje += "📍 Posiciones encontradas: " + posicionesCola.toString() + "\n";
                            mensaje += "📊 Total de coincidencias: " + posicionesCola.size();
                        }
                    } else {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "❌ Estado: NO ENCONTRADO\n";
                        mensaje += "📍 El dato no existe en la cola";
                    }
                    break;
                    
                case 2: // Lista Simple: buscar con posiciones múltiples
                    String estadoAntes2 = obtenerEstadoListaSimple();
                    int[] posiciones = objListaSimple.buscarTodasLasPosiciones(val);
                    found = posiciones.length > 0;
                    String estadoDespues2 = obtenerEstadoListaSimple();
                    registrarOperacion("LISTA_SIMPLE", "BUSCAR", String.valueOf(val), estadoAntes2, estadoDespues2);
                    
                    titulo = "Búsqueda en Lista Simple";
                    if(found) {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "✅ Estado: ENCONTRADO\n";
                        if(posiciones.length == 1) {
                            mensaje += "📍 Posición: " + posiciones[0];
                        } else {
                            StringBuilder sb = new StringBuilder();
                            sb.append("[");
                            for(int i = 0; i < posiciones.length; i++) {
                                sb.append(posiciones[i]);
                                if(i < posiciones.length - 1) sb.append(", ");
                            }
                            sb.append("]");
                            mensaje += "📍 Posiciones encontradas: " + sb.toString() + "\n";
                            mensaje += "📊 Total de coincidencias: " + posiciones.length;
                        }
                    } else {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "❌ Estado: NO ENCONTRADO\n";
                        mensaje += "📍 El dato no existe en la lista";
                    }
                    break;
                    
                case 3: // Lista Doble: buscar with posiciones múltiples
                    String estadoAntes3 = obtenerEstadoListaDoble();
                    int[] posicionesDoble = objListaDoble.buscarTodasLasPosiciones(val);
                    found = posicionesDoble.length > 0;
                    String estadoDespues3 = obtenerEstadoListaDoble();
                    registrarOperacion("LISTA_DOBLE", "BUSCAR", String.valueOf(val), estadoAntes3, estadoDespues3);
                    
                    titulo = "Búsqueda en Lista Doble";
                    if(found) {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "✅ Estado: ENCONTRADO\n";
                        if(posicionesDoble.length == 1) {
                            mensaje += "📍 Posición: " + posicionesDoble[0];
                        } else {
                            StringBuilder sb2 = new StringBuilder();
                            sb2.append("[");
                            for(int i = 0; i < posicionesDoble.length; i++) {
                                sb2.append(posicionesDoble[i]);
                                if(i < posicionesDoble.length - 1) sb2.append(", ");
                            }
                            sb2.append("]");
                            mensaje += "📍 Posiciones encontradas: " + sb2.toString() + "\n";
                            mensaje += "📊 Total de coincidencias: " + posicionesDoble.length;
                        }
                    } else {
                        mensaje = "🔍 Dato buscado: " + val + "\n";
                        mensaje += "❌ Estado: NO ENCONTRADO\n";
                        mensaje += "📍 El dato no existe en la lista";
                    }
                    break;
            }
            
            // Mostrar mensaje modal
            int tipoMensaje = found ? javax.swing.JOptionPane.INFORMATION_MESSAGE : javax.swing.JOptionPane.WARNING_MESSAGE;
            javax.swing.JOptionPane.showMessageDialog(this, mensaje, titulo, tipoMensaje);
            
            drawSelected(); // Refrescar visualización
        }catch(Exception ex){ 
            javax.swing.JOptionPane.showMessageDialog(this, 
                "❌ Error: Ingrese un valor numérico válido", 
                "Valor Inválido", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        }
    }

    private void jButtonVaciarActionPerformed(java.awt.event.ActionEvent evt) {
        int idx = tabbedPane.getSelectedIndex();
        switch(idx){
            case 0: 
                String estadoAntes0 = obtenerEstadoPila();
                objPila.vaciarPila(); 
                String estadoDespues0 = obtenerEstadoPila();
                registrarOperacion("PILA", "VACIAR", "", estadoAntes0, estadoDespues0);
                break; // Pila
            case 1: 
                String estadoAntes1 = obtenerEstadoCola();
                objCola.vaciarCola(); 
                String estadoDespues1 = obtenerEstadoCola();
                registrarOperacion("COLA", "VACIAR", "", estadoAntes1, estadoDespues1);
                break; // Cola
            case 2: 
                String estadoAntes2 = obtenerEstadoListaSimple();
                objListaSimple.vaciar(); 
                String estadoDespues2 = obtenerEstadoListaSimple();
                registrarOperacion("LISTA_SIMPLE", "VACIAR", "", estadoAntes2, estadoDespues2);
                break; // Lista Simple
            case 3: 
                String estadoAntes3 = obtenerEstadoListaDoble();
                objListaDoble.vaciar(); 
                String estadoDespues3 = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "VACIAR", "", estadoAntes3, estadoDespues3);
                break; // Lista Doble
        }
        drawSelected();
    }

    // ==================== MÉTODOS DE ACCIÓN PARA LISTA SIMPLE ====================
    
    private void insertarOrdenadoAction() {
        try {
            int valor = Integer.parseInt(txtDato.getText());
            int idx = tabbedPane.getSelectedIndex();
            
            switch(idx) {
                case 2: // Lista Simple
                    String estadoAntes2 = obtenerEstadoListaSimple();
                    objListaSimple.insertarOrdenado(valor);
                    String estadoDespues2 = obtenerEstadoListaSimple();
                    registrarOperacion("LISTA_SIMPLE", "INSERTAR_ORDENADO", String.valueOf(valor), estadoAntes2, estadoDespues2);
                    break;
                case 3: // Lista Doble
                    String estadoAntes3 = obtenerEstadoListaDoble();
                    objListaDoble.insertarOrdenado(valor);
                    String estadoDespues3 = obtenerEstadoListaDoble();
                    registrarOperacion("LISTA_DOBLE", "INSERTAR_ORDENADO", String.valueOf(valor), estadoAntes3, estadoDespues3);
                    break;
            }
            
            txtDato.setText("");
            drawSelected();
        } catch (Exception ex) {
            System.out.println("Valor inválido para insertar ordenado");
        }
    }
    
    private void insertarDerechaAction() {
        try {
            int valor = Integer.parseInt(txtDato.getText());
            int idx = tabbedPane.getSelectedIndex();
            boolean exito = false;
            int posicionPuntero = -1;
            
            switch(idx) {
                case 2: // Lista Simple
                    if (objListaSimple.esPunteroNulo()) {
                        javax.swing.JOptionPane.showMessageDialog(this, 
                            "El puntero no está posicionado. Use los botones de navegación para posicionar el puntero primero.",
                            "Puntero no posicionado", 
                            javax.swing.JOptionPane.WARNING_MESSAGE);
                        return;
                    }
                    posicionPuntero = objListaSimple.getPosicionPuntero();
                    String estadoAntes2 = obtenerEstadoListaSimple();
                    exito = objListaSimple.insertarDerecha(posicionPuntero, valor);
                    if (exito) {
                        String estadoDespues2 = obtenerEstadoListaSimple();
                        registrarOperacion("LISTA_SIMPLE", "INSERTAR_DERECHA", String.valueOf(valor), estadoAntes2, estadoDespues2);
                    }
                    break;
                case 3: // Lista Doble
                    if (objListaDoble.esPunteroNulo()) {
                        javax.swing.JOptionPane.showMessageDialog(this, 
                            "El puntero no está posicionado. Use los botones de navegación para posicionar el puntero primero.",
                            "Puntero no posicionado", 
                            javax.swing.JOptionPane.WARNING_MESSAGE);
                        return;
                    }
                    posicionPuntero = objListaDoble.getPosicionPuntero();
                    String estadoAntes3 = obtenerEstadoListaDoble();
                    exito = objListaDoble.insertarDerecha(posicionPuntero, valor);
                    if (exito) {
                        String estadoDespues3 = obtenerEstadoListaDoble();
                        registrarOperacion("LISTA_DOBLE", "INSERTAR_DERECHA", String.valueOf(valor), estadoAntes3, estadoDespues3);
                    }
                    break;
            }
            
            if (exito) {
                txtDato.setText("");
                System.out.println("Insertado valor " + valor + " a la derecha del puntero (posición " + posicionPuntero + ")");
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Valor " + valor + " insertado a la derecha del puntero\n(Posición: " + posicionPuntero + ")",
                    "Inserción Exitosa", 
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);
                drawSelected();
            }
        } catch (NumberFormatException ex) {
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Ingrese un valor numérico válido",
                "Error de Entrada", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        } catch (Exception ex) {
            System.out.println("Error al insertar a la derecha: " + ex.getMessage());
        }
    }
    
    private void eliminarDerechaAction() {
        try {
            int idx = tabbedPane.getSelectedIndex();
            int eliminado = -1;
            int posicionPuntero = -1;
            
            switch(idx) {
                case 2: // Lista Simple
                    if (objListaSimple.esPunteroNulo()) {
                        javax.swing.JOptionPane.showMessageDialog(this, 
                            "El puntero no está posicionado. Use los botones de navegación para posicionar el puntero primero.",
                            "Puntero no posicionado", 
                            javax.swing.JOptionPane.WARNING_MESSAGE);
                        return;
                    }
                    posicionPuntero = objListaSimple.getPosicionPuntero();
                    String estadoAntes2 = obtenerEstadoListaSimple();
                    eliminado = objListaSimple.eliminarDerecha(posicionPuntero);
                    if (eliminado != -1) {
                        String estadoDespues2 = obtenerEstadoListaSimple();
                        registrarOperacion("LISTA_SIMPLE", "ELIMINAR_DERECHA", String.valueOf(eliminado), estadoAntes2, estadoDespues2);
                    }
                    break;
                case 3: // Lista Doble
                    if (objListaDoble.esPunteroNulo()) {
                        javax.swing.JOptionPane.showMessageDialog(this, 
                            "El puntero no está posicionado. Use los botones de navegación para posicionar el puntero primero.",
                            "Puntero no posicionado", 
                            javax.swing.JOptionPane.WARNING_MESSAGE);
                        return;
                    }
                    posicionPuntero = objListaDoble.getPosicionPuntero();
                    String estadoAntes3 = obtenerEstadoListaDoble();
                    eliminado = objListaDoble.eliminarDerecha(posicionPuntero);
                    if (eliminado != -1) {
                        String estadoDespues3 = obtenerEstadoListaDoble();
                        registrarOperacion("LISTA_DOBLE", "ELIMINAR_DERECHA", String.valueOf(eliminado), estadoAntes3, estadoDespues3);
                    }
                    break;
            }
            
            if (eliminado != -1) {
                System.out.println("Eliminado a la derecha del puntero (posición " + posicionPuntero + "): " + eliminado);
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Eliminado valor " + eliminado + " a la derecha del puntero\n(Posición: " + posicionPuntero + ")",
                    "Eliminación Exitosa", 
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);
                drawSelected();
            } else {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "No se pudo eliminar: no hay nodo a la derecha del puntero actual",
                    "No se puede eliminar", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
            }
        } catch (Exception ex) {
            System.out.println("Error al eliminar a la derecha: " + ex.getMessage());
        }
    }
    
    private void ordenarAscendenteAction() {
        int idx = tabbedPane.getSelectedIndex();
        
        switch(idx) {
            case 2: // Lista Simple
                String estadoAntes2 = obtenerEstadoListaSimple();
                objListaSimple.ordenarAscendente();
                String estadoDespues2 = obtenerEstadoListaSimple();
                registrarOperacion("LISTA_SIMPLE", "ORDENAR_ASCENDENTE", "", estadoAntes2, estadoDespues2);
                break;
            case 3: // Lista Doble
                String estadoAntes3 = obtenerEstadoListaDoble();
                objListaDoble.ordenarAscendente();
                String estadoDespues3 = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "ORDENAR_ASCENDENTE", "", estadoAntes3, estadoDespues3);
                break;
        }
        
        drawSelected();
    }
    
    private void ordenarReferenciaAction() {
        objListaSimple.ordenarPorReferencia();
        drawSelected();
    }
    
    private void sumarElementosAction() {
        int idx = tabbedPane.getSelectedIndex();
        int suma = 0;
        int tamaño = 0;
        String tipoLista = "";
        
        switch(idx) {
            case 2: // Lista Simple
                String estadoAntes2 = obtenerEstadoListaSimple();
                suma = objListaSimple.sumarElementos();
                tamaño = objListaSimple.size();
                tipoLista = "Lista Simple";
                String estadoDespues2 = obtenerEstadoListaSimple();
                registrarOperacion("LISTA_SIMPLE", "SUMAR_ELEMENTOS", "", estadoAntes2, estadoDespues2);
                break;
            case 3: // Lista Doble
                String estadoAntes3 = obtenerEstadoListaDoble();
                suma = objListaDoble.sumarElementos();
                tamaño = objListaDoble.size();
                tipoLista = "Lista Doble";
                String estadoDespues3 = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "SUMAR_ELEMENTOS", "", estadoAntes3, estadoDespues3);
                break;
        }
        
        if (idx == 2 || idx == 3) {
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Suma de elementos: " + suma + "\nTamaño de la lista: " + tamaño,
                "Información de " + tipoLista, 
                javax.swing.JOptionPane.INFORMATION_MESSAGE);
        }
        
        // Actualizar solo la visualización
        drawSelected();
    }

    // ==================== MÉTODOS DE EVENTOS PARA ARCHIVO .FORM ====================
    
    private void btnInsertarOrdenadoActionPerformed(java.awt.event.ActionEvent evt) {
        insertarOrdenadoAction();
    }
    
    private void btnInsertarDerechaActionPerformed(java.awt.event.ActionEvent evt) {
        insertarDerechaAction();
    }
    
    private void btnEliminarDerechaActionPerformed(java.awt.event.ActionEvent evt) {
        eliminarDerechaAction();
    }
    
    private void btnOrdenarAscActionPerformed(java.awt.event.ActionEvent evt) {
        ordenarAscendenteAction();
    }
    
    private void btnOrdenarRefActionPerformed(java.awt.event.ActionEvent evt) {
        ordenarReferenciaAction();
    }
    
    private void btnSumarElementosActionPerformed(java.awt.event.ActionEvent evt) {
        sumarElementosAction();
    }
    
    // ==================== MÉTODOS DE EVENTOS PARA LISTA DOBLE ====================
    
    private void btnInsertarIzquierdaActionPerformed(java.awt.event.ActionEvent evt) {
        insertarIzquierdaAction();
    }
    
    private void btnEliminarIzquierdaActionPerformed(java.awt.event.ActionEvent evt) {
        eliminarIzquierdaAction();
    }
    
    private void btnRecorridoForwardActionPerformed(java.awt.event.ActionEvent evt) {
        recorridoForwardAction();
    }
    
    private void btnRecorridoBackwardActionPerformed(java.awt.event.ActionEvent evt) {
        recorridoBackwardAction();
    }
    
    private void btnPermutarNodosActionPerformed(java.awt.event.ActionEvent evt) {
        permutarNodosAction();
    }

    // ==================== MÉTODOS DE ACCIÓN PARA LISTA DOBLE ====================
    
    private void insertarIzquierdaAction() {
        try {
            // TDA CONSISTENCIA: Verificar que el valor sea válido
            if (txtDato.getText().trim().isEmpty()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Debe ingresar un valor numérico.",
                    "Campo Vacío - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            int valor = Integer.parseInt(txtDato.getText());
            
            // TDA CONSISTENCIA: Verificar que la lista no esté vacía
            if (objListaDoble.estaVacia()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "La lista doble está vacía.\nUse el botón 'Insertar' para agregar el primer elemento.",
                    "Lista Vacía - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            // TDA CONSISTENCIA: Verificar que el puntero esté posicionado
            if (objListaDoble.esPunteroNulo()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "El puntero no está posicionado.\nUse los botones de navegación para posicionar el puntero primero.",
                    "Puntero no posicionado - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            int posicionPuntero = objListaDoble.getPosicionPuntero();
            
            String estadoAntes = obtenerEstadoListaDoble();
            boolean exito = objListaDoble.insertarIzquierda(posicionPuntero, valor);
            if (exito) {
                String estadoDespues = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "INSERTAR_IZQUIERDA", String.valueOf(valor), estadoAntes, estadoDespues);
                txtDato.setText("");
                System.out.println("TDA: Insertado valor " + valor + " a la izquierda del puntero (posición " + posicionPuntero + ")");
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Valor " + valor + " insertado a la izquierda del puntero\n(Posición: " + posicionPuntero + ")\n\nEl puntero ahora está en posición " + (posicionPuntero + 1),
                    "Inserción Exitosa - TDA", 
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);
                drawSelected();
            } else {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "No se pudo insertar el valor.\nVerifique la integridad de la estructura.",
                    "Error TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
            }
        } catch (NumberFormatException ex) {
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Ingrese un valor numérico entero válido.\nEjemplo: 10, 25, -5, etc.",
                "Error de Entrada - TDA", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        } catch (Exception ex) {
            System.out.println("Error al insertar a la izquierda: " + ex.getMessage());
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Error inesperado al insertar: " + ex.getMessage(),
                "Error de Sistema", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        }
    }
    
    private void eliminarIzquierdaAction() {
        try {
            // TDA CONSISTENCIA: Verificar que la lista no esté vacía
            if (objListaDoble.estaVacia()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "La lista doble está vacía. No hay elementos para eliminar.",
                    "Lista Vacía", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            // TDA CONSISTENCIA: Verificar que el puntero esté posicionado
            if (objListaDoble.esPunteroNulo()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "El puntero no está posicionado. Use los botones de navegación para posicionar el puntero primero.",
                    "Puntero no posicionado", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            int posicionPuntero = objListaDoble.getPosicionPuntero();
            
            // TDA CONSISTENCIA: No se puede eliminar a la izquierda de posición 0
            if (posicionPuntero <= 0) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "No se puede eliminar a la izquierda del primer elemento (posición 0).\n" +
                    "Principio TDA: No existe elemento anterior al primer nodo.",
                    "Operación Inválida - Principio TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
                return;
            }
            
            String estadoAntes = obtenerEstadoListaDoble();
            int eliminado = objListaDoble.eliminarIzquierda(posicionPuntero);
            
            if (eliminado != -1) {
                String estadoDespues = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "ELIMINAR_IZQUIERDA", String.valueOf(eliminado), estadoAntes, estadoDespues);
                System.out.println("TDA: Eliminado a la izquierda del puntero (posición " + posicionPuntero + "): " + eliminado);
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Eliminado valor " + eliminado + " a la izquierda del puntero\n(Posición: " + posicionPuntero + ")",
                    "Eliminación Exitosa", 
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);
                drawSelected();
            } else {
                // Este caso no debería ocurrir con las validaciones previas, pero mantenemos por seguridad
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Error interno: No se pudo realizar la eliminación.\nVerifique la integridad de la estructura.",
                    "Error TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
            }
        } catch (Exception ex) {
            System.out.println("Error al eliminar a la izquierda: " + ex.getMessage());
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Error inesperado al eliminar: " + ex.getMessage(),
                "Error de Sistema", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        }
    }
    
    private void recorridoForwardAction() {
        String recorrido = objListaDoble.recorridoForward();
        System.out.println("Recorrido Forward (Head → Tail): " + recorrido);
        javax.swing.JOptionPane.showMessageDialog(this, 
            "Recorrido Forward (Head → Tail):\n" + recorrido,
            "Lista Doble - Recorrido Directo", 
            javax.swing.JOptionPane.INFORMATION_MESSAGE);
    }
    
    private void recorridoBackwardAction() {
        String recorrido = objListaDoble.recorridoBackward();
        System.out.println("Recorrido Backward (Tail ← Head): " + recorrido);
        javax.swing.JOptionPane.showMessageDialog(this, 
            "Recorrido Backward (Tail ← Head):\n" + recorrido,
            "Lista Doble - Recorrido Inverso", 
            javax.swing.JOptionPane.INFORMATION_MESSAGE);
    }
    
    private void permutarNodosAction() {
        try {
            // TDA CONSISTENCIA: Verificar que la lista no esté vacía
            if (objListaDoble.estaVacia()) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "No se pueden permutar nodos: La lista está vacía.\n" +
                    "Inserte al menos 2 elementos antes de permutar.",
                    "Lista Vacía - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            // TDA CONSISTENCIA: Verificar que haya al menos 2 nodos
            if (objListaDoble.size() < 2) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Se necesitan al menos 2 nodos para realizar una permutación.\n" +
                    "Tamaño actual de la lista: " + objListaDoble.size(),
                    "Insuficientes Nodos - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                return;
            }
            
            // Solicitar las posiciones al usuario
            String pos1Str = javax.swing.JOptionPane.showInputDialog(this,
                "Ingrese la primera posición a permutar (0 a " + (objListaDoble.size()-1) + "):",
                "Permutación de Nodos - Posición 1",
                javax.swing.JOptionPane.QUESTION_MESSAGE);
                
            if (pos1Str == null) return; // Usuario canceló
            
            String pos2Str = javax.swing.JOptionPane.showInputDialog(this,
                "Ingrese la segunda posición a permutar (0 a " + (objListaDoble.size()-1) + "):",
                "Permutación de Nodos - Posición 2",
                javax.swing.JOptionPane.QUESTION_MESSAGE);
                
            if (pos2Str == null) return; // Usuario canceló
            
            // Convertir a enteros y validar
            int pos1 = Integer.parseInt(pos1Str.trim());
            int pos2 = Integer.parseInt(pos2Str.trim());
            
            // Validar rango de posiciones
            int tamaño = objListaDoble.size();
            if (pos1 < 0 || pos1 >= tamaño || pos2 < 0 || pos2 >= tamaño) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Posiciones inválidas. Deben estar entre 0 y " + (tamaño-1) + ".\n" +
                    "Posición 1: " + pos1 + "\n" +
                    "Posición 2: " + pos2,
                    "Error de Rango - TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
                return;
            }
            
            // Mostrar confirmación con información de los nodos
            int valor1 = objListaDoble.obtenerValorEnPosicion(pos1);
            int valor2 = objListaDoble.obtenerValorEnPosicion(pos2);
            
            if (valor1 == -1 || valor2 == -1) {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Error: No se pudieron encontrar los nodos en las posiciones especificadas.",
                    "Error de Búsqueda - TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
                return;
            }
            
            int confirmacion = javax.swing.JOptionPane.showConfirmDialog(this,
                "¿Confirma la permutación de nodos?\n\n" +
                "Posición " + pos1 + " (Valor: " + valor1 + ") ↔ " +
                "Posición " + pos2 + " (Valor: " + valor2 + ")\n\n" +
                "Esta operación intercambiará completamente los nodos, no solo sus valores.",
                "Confirmar Permutación - TDA",
                javax.swing.JOptionPane.YES_NO_OPTION,
                javax.swing.JOptionPane.QUESTION_MESSAGE);
                
            if (confirmacion != javax.swing.JOptionPane.YES_OPTION) return;
            
            // Realizar la permutación
            String estadoAntes = obtenerEstadoListaDoble();
            boolean exito = objListaDoble.permutarNodos(pos1, pos2);
            
            if (exito) {
                String estadoDespues = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "PERMUTAR_NODOS", 
                    "pos1:" + pos1 + ",pos2:" + pos2, estadoAntes, estadoDespues);
                    
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "¡Permutación exitosa!\n\n" +
                    "Los nodos en las posiciones " + pos1 + " y " + pos2 + " han sido intercambiados.\n" +
                    "El puntero se ha mantenido consistente con la nueva estructura.",
                    "Permutación Completada - TDA", 
                    javax.swing.JOptionPane.INFORMATION_MESSAGE);
                    
                drawSelected();
            } else {
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Error: No se pudo completar la permutación.\n" +
                    "Verifique que las posiciones sean válidas.",
                    "Error en Permutación - TDA", 
                    javax.swing.JOptionPane.ERROR_MESSAGE);
            }
            
        } catch (NumberFormatException ex) {
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Error: Ingrese únicamente números enteros válidos para las posiciones.\n" +
                "Ejemplo: 0, 1, 2, 3, etc.",
                "Error de Entrada - TDA", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        } catch (Exception ex) {
            System.out.println("Error al permutar nodos: " + ex.getMessage());
            javax.swing.JOptionPane.showMessageDialog(this, 
                "Error inesperado al permutar: " + ex.getMessage(),
                "Error de Sistema", 
                javax.swing.JOptionPane.ERROR_MESSAGE);
        }
    }
    
    // Métodos de acción para navegación del puntero
    private void btnPunteroInicioActionPerformed(java.awt.event.ActionEvent evt) {
        int idx = tabbedPane.getSelectedIndex();
        switch(idx) {
            case 0: // Pila
                if (objPila.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La pila está vacía. No hay elementos para navegar.",
                        "Pila Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objPila.moverPunteroInicio();
                lblInfoPuntero.setText("Puntero: " + objPila.obtenerInfoPuntero());
                break;
            case 1: // Cola
                if (objCola.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La cola está vacía. No hay elementos para navegar.",
                        "Cola Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objCola.moverPunteroInicio();
                lblInfoPuntero.setText("Puntero: " + objCola.obtenerInfoPuntero());
                break;
            case 2: // Lista Simple
                if (objListaSimple.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista simple está vacía. No hay elementos para navegar.",
                        "Lista Simple Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                String estadoAntes = obtenerEstadoListaSimple();
                objListaSimple.moverPunteroInicio();
                String estadoDespues = obtenerEstadoListaSimple();
                registrarOperacion("LISTA_SIMPLE", "MOVER_PUNTERO_INICIO", "", estadoAntes, estadoDespues);
                lblInfoPuntero.setText("Puntero: " + objListaSimple.obtenerInfoPuntero());
                break;
            case 3: // Lista Doble
                if (objListaDoble.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista doble está vacía. No hay elementos para navegar.",
                        "Lista Doble Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                String estadoAntes3 = obtenerEstadoListaDoble();
                objListaDoble.moverPunteroInicio();
                String estadoDespues3 = obtenerEstadoListaDoble();
                registrarOperacion("LISTA_DOBLE", "MOVER_PUNTERO_INICIO", "", estadoAntes3, estadoDespues3);
                lblInfoPuntero.setText("Puntero: " + objListaDoble.obtenerInfoPuntero());
                break;
        }
        drawSelected();
    }
    
    private void btnPunteroSiguienteActionPerformed(java.awt.event.ActionEvent evt) {
        int idx = tabbedPane.getSelectedIndex();
        switch(idx) {
            case 0: // Pila
                if (objPila.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La pila está vacía. No hay elementos para navegar.",
                        "Pila Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                if (!objPila.moverPunteroSiguiente()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "Ya está en el último elemento de la pila.\nPrincipio TDA: No hay siguiente elemento.",
                        "Fin de Pila - TDA", 
                        javax.swing.JOptionPane.INFORMATION_MESSAGE);
                }
                lblInfoPuntero.setText("Puntero: " + objPila.obtenerInfoPuntero());
                break;
            case 1: // Cola
                if (objCola.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La cola está vacía. No hay elementos para navegar.",
                        "Cola Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                if (!objCola.moverPunteroSiguiente()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "Ya está en el último elemento de la cola.\nPrincipio TDA: No hay siguiente elemento.",
                        "Fin de Cola - TDA", 
                        javax.swing.JOptionPane.INFORMATION_MESSAGE);
                }
                lblInfoPuntero.setText("Puntero: " + objCola.obtenerInfoPuntero());
                break;
            case 2: // Lista Simple
                if (objListaSimple.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista simple está vacía. No hay elementos para navegar.",
                        "Lista Simple Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                String estadoAntes = obtenerEstadoListaSimple();
                if (!objListaSimple.moverPunteroSiguiente()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "Ya está en el último elemento de la lista.\nPrincipio TDA: No hay siguiente elemento.",
                        "Fin de Lista Simple - TDA", 
                        javax.swing.JOptionPane.INFORMATION_MESSAGE);
                } else {
                    String estadoDespues = obtenerEstadoListaSimple();
                    registrarOperacion("LISTA_SIMPLE", "MOVER_PUNTERO_SIGUIENTE", "", estadoAntes, estadoDespues);
                }
                lblInfoPuntero.setText("Puntero: " + objListaSimple.obtenerInfoPuntero());
                break;
            case 3: // Lista Doble
                if (objListaDoble.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista doble está vacía. No hay elementos para navegar.",
                        "Lista Doble Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                String estadoAntes4 = obtenerEstadoListaDoble();
                if (!objListaDoble.moverPunteroSiguiente()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "Ya está en el último elemento de la lista.\nPrincipio TDA: No hay siguiente elemento.",
                        "Fin de Lista Doble - TDA", 
                        javax.swing.JOptionPane.INFORMATION_MESSAGE);
                } else {
                    String estadoDespues4 = obtenerEstadoListaDoble();
                    registrarOperacion("LISTA_DOBLE", "MOVER_PUNTERO_SIGUIENTE", "", estadoAntes4, estadoDespues4);
                }
                lblInfoPuntero.setText("Puntero: " + objListaDoble.obtenerInfoPuntero());
                break;
        }
        drawSelected();
    }
    
    private void btnPunteroAnteriorActionPerformed(java.awt.event.ActionEvent evt) {
        int idx = tabbedPane.getSelectedIndex();
        switch(idx) {
            case 0: // Pila - TDA: En pilas no hay concepto de "anterior" lógico
                if (objPila.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La pila está vacía. No hay elementos para navegar.",
                        "Pila Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objPila.moverPunteroAnterior();
                lblInfoPuntero.setText("Puntero: " + objPila.obtenerInfoPuntero());
                break;
            case 1: // Cola - TDA: En colas no hay concepto de "anterior" lógico
                if (objCola.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La cola está vacía. No hay elementos para navegar.",
                        "Cola Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objCola.moverPunteroAnterior();
                lblInfoPuntero.setText("Puntero: " + objCola.obtenerInfoPuntero());
                break;
            case 2: // Lista Simple - TDA: No hay navegación bidireccional
                if (objListaSimple.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista simple está vacía. No hay elementos para navegar.",
                        "Lista Simple Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                javax.swing.JOptionPane.showMessageDialog(this, 
                    "Lista Simple no soporta navegación hacia atrás.\nPrincipio TDA: Solo enlaces hacia adelante.",
                    "Operación No Soportada - TDA", 
                    javax.swing.JOptionPane.WARNING_MESSAGE);
                break;
            case 3: // Lista Doble - TDA: Sí soporta navegación bidireccional
                if (objListaDoble.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista doble está vacía. No hay elementos para navegar.",
                        "Lista Doble Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                if (!objListaDoble.moverPunteroAnterior()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "Ya está en el primer elemento de la lista.\nPrincipio TDA: No hay elemento anterior.",
                        "Inicio de Lista Doble - TDA", 
                        javax.swing.JOptionPane.INFORMATION_MESSAGE);
                }
                lblInfoPuntero.setText("Puntero: " + objListaDoble.obtenerInfoPuntero());
                break;
        }
        drawSelected();
    }
    
    private void btnPunteroFinalActionPerformed(java.awt.event.ActionEvent evt) {
        int idx = tabbedPane.getSelectedIndex();
        switch(idx) {
            case 0: // Pila
                if (objPila.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La pila está vacía. No hay elementos para navegar.",
                        "Pila Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objPila.moverPunteroFinal();
                lblInfoPuntero.setText("Puntero: " + objPila.obtenerInfoPuntero());
                break;
            case 1: // Cola
                if (objCola.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La cola está vacía. No hay elementos para navegar.",
                        "Cola Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objCola.moverPunteroFinal();
                lblInfoPuntero.setText("Puntero: " + objCola.obtenerInfoPuntero());
                break;
            case 2: // Lista Simple
                if (objListaSimple.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista simple está vacía. No hay elementos para navegar.",
                        "Lista Simple Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objListaSimple.moverPunteroFinal();
                lblInfoPuntero.setText("Puntero: " + objListaSimple.obtenerInfoPuntero());
                break;
            case 3: // Lista Doble
                if (objListaDoble.estaVacia()) {
                    javax.swing.JOptionPane.showMessageDialog(this, 
                        "La lista doble está vacía. No hay elementos para navegar.",
                        "Lista Doble Vacía - TDA", 
                        javax.swing.JOptionPane.WARNING_MESSAGE);
                    return;
                }
                objListaDoble.moverPunteroFinal();
                lblInfoPuntero.setText("Puntero: " + objListaDoble.obtenerInfoPuntero());
                break;
        }
        drawSelected();
    }
    
    private void btnTogglePunteroActionPerformed(java.awt.event.ActionEvent evt) {
        // Cambiar el estado del toggle
        mostrarPunteroVisual = !mostrarPunteroVisual;
        
        // Cambiar la apariencia del botón según el estado
        if (mostrarPunteroVisual) {
            btnTogglePuntero.setBackground(java.awt.Color.BLUE);
            btnTogglePuntero.setForeground(java.awt.Color.WHITE);
            btnTogglePuntero.setText("⬛");
            btnTogglePuntero.setToolTipText("Ocultar indicador visual del puntero");
        } else {
            btnTogglePuntero.setBackground(java.awt.Color.LIGHT_GRAY);
            btnTogglePuntero.setForeground(java.awt.Color.BLACK);
            btnTogglePuntero.setText("⬜");
            btnTogglePuntero.setToolTipText("Mostrar indicador visual del puntero");
        }
        
        // Redibujar para aplicar el cambio
        drawSelected();
    }
    
    private void registrarOperacion(String estructura, String operacion, String valor, String estadoAntes, String estadoDespues) {
        OperacionAnalisis op = new OperacionAnalisis(estructura, operacion, valor, estadoAntes, estadoDespues);
        historialOperaciones.add(op);
    }
    
    private void btnAnalisisActionPerformed(java.awt.event.ActionEvent evt) {
        abrirVentanaAnalisis();
    }
    
    private String obtenerNombreEstructura(int indice) {
        switch(indice) {
            case 0: return "PILA";
            case 1: return "COLA";
            case 2: return "LISTA_SIMPLE";
            case 3: return "LISTA_DOBLE";
            default: return "DESCONOCIDA";
        }
    }
    
    private void abrirVentanaAnalisis() {
        // Obtener la estructura actualmente seleccionada
        int idx = tabbedPane.getSelectedIndex();
        String estructuraActual = obtenerNombreEstructura(idx);
        
        // Filtrar operaciones solo para la estructura actual
        java.util.ArrayList<OperacionAnalisis> operacionesFiltradas = new java.util.ArrayList<>();
        for (OperacionAnalisis op : historialOperaciones) {
            if (op.estructura.equals(estructuraActual)) {
                operacionesFiltradas.add(op);
            }
        }
        
        // Crear ventana modal para el análisis
        javax.swing.JDialog dialogAnalisis = new javax.swing.JDialog(this, 
            "Análisis Paso a Paso - " + estructuraActual, true);
        dialogAnalisis.setSize(900, 700);
        dialogAnalisis.setLocationRelativeTo(this);
        
        // Panel principal con BorderLayout
        javax.swing.JPanel panelPrincipal = new javax.swing.JPanel(new java.awt.BorderLayout());
        
        // Panel superior con controles
        javax.swing.JPanel panelControles = new javax.swing.JPanel();
        javax.swing.JLabel lblPaso = new javax.swing.JLabel("Paso: 0 / " + operacionesFiltradas.size());
        javax.swing.JButton btnAnterior = new javax.swing.JButton("← Anterior");
        javax.swing.JButton btnSiguiente = new javax.swing.JButton("Siguiente →");
        javax.swing.JButton btnReiniciar = new javax.swing.JButton("🔄 Reiniciar");
        
        panelControles.add(btnReiniciar);
        panelControles.add(btnAnterior);
        panelControles.add(lblPaso);
        panelControles.add(btnSiguiente);
        
        // Área de texto para mostrar el estado actual
        javax.swing.JTextArea areaEstado = new javax.swing.JTextArea();
        areaEstado.setFont(new java.awt.Font("Monospaced", java.awt.Font.PLAIN, 12));
        areaEstado.setEditable(false);
        javax.swing.JScrollPane scrollEstado = new javax.swing.JScrollPane(areaEstado);
        
        // Variables para controlar el paso actual y sub-paso
        final int[] pasoActual = {0};
        final int[] subPaso = {0}; // Para manejar pasos dentro de cada operación
        
        // Panel para visualización gráfica
        javax.swing.JPanel panelVisualizacion = new javax.swing.JPanel() {
            @Override
            protected void paintComponent(java.awt.Graphics g) {
                super.paintComponent(g);
                // Aquí se dibujará la visualización paso a paso con sub-pasos
                dibujarVisualizacionAnalisisConSubPasos(g, this.getWidth(), this.getHeight(), pasoActual[0], subPaso[0], operacionesFiltradas);
            }
        };
        panelVisualizacion.setPreferredSize(new java.awt.Dimension(400, 300));
        panelVisualizacion.setBackground(java.awt.Color.WHITE);
        panelVisualizacion.setBorder(javax.swing.BorderFactory.createTitledBorder("Visualización Gráfica"));
        
        // Actualizar información del paso con sub-pasos
        Runnable actualizarPaso = () -> {
            if (operacionesFiltradas.isEmpty()) {
                lblPaso.setText("Paso: 0 / 0");
                areaEstado.setText("No hay operaciones registradas para " + estructuraActual + " aún.\n\nRealiza algunas operaciones en esta estructura y luego abre el análisis.");
                btnAnterior.setEnabled(false);
                btnSiguiente.setEnabled(false);
                subPaso[0] = 0;
            } else if (pasoActual[0] == 0) {
                lblPaso.setText("Paso: 0 / " + operacionesFiltradas.size() + " (Estado Inicial)");
                areaEstado.setText("ESTADO INICIAL:\nLa estructura " + estructuraActual + " está vacía.\n\nHaz clic en 'Siguiente' para ver la primera operación.");
                btnAnterior.setEnabled(false);
                btnSiguiente.setEnabled(true);
                subPaso[0] = 0;
            } else if (pasoActual[0] <= operacionesFiltradas.size()) {
                OperacionAnalisis op = operacionesFiltradas.get(pasoActual[0] - 1);
                int maxSubPasos = obtenerMaxSubPasos(op);
                
                lblPaso.setText("Paso: " + pasoActual[0] + " / " + operacionesFiltradas.size() + 
                              " (Sub-paso: " + (subPaso[0] + 1) + " / " + (maxSubPasos + 1) + ")");
                
                StringBuilder info = new StringBuilder();
                info.append("OPERACIÓN ").append(pasoActual[0]).append(":\n");
                info.append("Estructura: ").append(op.estructura).append("\n");
                info.append("Operación: ").append(op.operacion).append("\n");
                if (!op.valor.isEmpty()) {
                    info.append("Valor: ").append(op.valor).append("\n");
                }
                info.append("\n").append(obtenerExplicacionSubPaso(op, subPaso[0]));
                info.append("\n\nMÉTODO DE NEGOCIO:\n").append(obtenerCodigoNegocio(op));
                areaEstado.setText(info.toString());
                
                // Control de navegación de sub-pasos
                btnAnterior.setEnabled(pasoActual[0] > 0 || subPaso[0] > 0);
                btnSiguiente.setEnabled(pasoActual[0] < operacionesFiltradas.size() || subPaso[0] < maxSubPasos);
            }
            panelVisualizacion.repaint();
        };
        
        // Eventos de botones con manejo de sub-pasos
        btnAnterior.addActionListener(e -> {
            if (subPaso[0] > 0) {
                // Retroceder en sub-pasos
                subPaso[0]--;
            } else if (pasoActual[0] > 0) {
                // Retroceder al paso anterior y ir al último sub-paso
                pasoActual[0]--;
                if (pasoActual[0] > 0) {
                    OperacionAnalisis op = operacionesFiltradas.get(pasoActual[0] - 1);
                    subPaso[0] = obtenerMaxSubPasos(op);
                } else {
                    subPaso[0] = 0;
                }
            }
            actualizarPaso.run();
        });
        
        btnSiguiente.addActionListener(e -> {
            if (pasoActual[0] == 0) {
                // Del estado inicial al primer paso
                pasoActual[0] = 1;
                subPaso[0] = 0;
            } else if (pasoActual[0] <= operacionesFiltradas.size()) {
                OperacionAnalisis op = operacionesFiltradas.get(pasoActual[0] - 1);
                int maxSubPasos = obtenerMaxSubPasos(op);
                
                if (subPaso[0] < maxSubPasos) {
                    // Avanzar en sub-pasos
                    subPaso[0]++;
                } else if (pasoActual[0] < operacionesFiltradas.size()) {
                    // Avanzar al siguiente paso
                    pasoActual[0]++;
                    subPaso[0] = 0;
                }
            }
            actualizarPaso.run();
        });
        
        btnReiniciar.addActionListener(e -> {
            pasoActual[0] = 0;
            subPaso[0] = 0;
            actualizarPaso.run();
        });
        
        // Ensamblar la ventana
        panelPrincipal.add(panelControles, java.awt.BorderLayout.NORTH);
        panelPrincipal.add(scrollEstado, java.awt.BorderLayout.CENTER);
        panelPrincipal.add(panelVisualizacion, java.awt.BorderLayout.EAST);
        
        dialogAnalisis.add(panelPrincipal);
        
        // Mostrar estado inicial
        actualizarPaso.run();
        
        dialogAnalisis.setVisible(true);
    }
    
    /**
     * Dibuja la visualización paso a paso del análisis
     */
    private void dibujarVisualizacionAnalisis(java.awt.Graphics g, int ancho, int alto, int pasoActual, java.util.ArrayList<OperacionAnalisis> operaciones) {
        java.awt.Graphics2D g2d = (java.awt.Graphics2D) g;
        g2d.setRenderingHint(java.awt.RenderingHints.KEY_ANTIALIASING, java.awt.RenderingHints.VALUE_ANTIALIAS_ON);
        
        // Configuración de colores y fuentes
        java.awt.Font fuenteNodo = new java.awt.Font("Arial", java.awt.Font.BOLD, 12);
        g2d.setFont(fuenteNodo);
        
        int inicioX = 30;
        int inicioY = 80;
        int tamanoNodo = 40;
        int espacioEntre = 60;
        
        if (pasoActual == 0) {
            // Estado inicial: Lista vacía
            dibujarEstadoInicial(g2d, inicioX, inicioY, ancho, alto);
        } else if (pasoActual <= operaciones.size()) {
            // Dibujar estado después de cada operación
            OperacionAnalisis op = operaciones.get(pasoActual - 1);
            dibujarEstadoOperacion(g2d, op, inicioX, inicioY, tamanoNodo, espacioEntre, pasoActual, ancho, alto);
        }
    }
    
    /**
     * Dibuja el estado inicial (lista vacía)
     */
    private void dibujarEstadoInicial(java.awt.Graphics2D g2d, int x, int y, int ancho, int alto) {
        // Título
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("ESTADO INICIAL", x, y - 40);
        
        // Indicador de lista vacía
        g2d.setColor(java.awt.Color.GRAY);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("Lista Vacía", x, y);
        
        // Cabeza apuntando a null
        g2d.setColor(java.awt.Color.BLUE);
        g2d.drawString("cabeza", x, y + 20);
        g2d.drawLine(x + 45, y + 15, x + 80, y + 15);
        g2d.drawString("null", x + 85, y + 20);
        
        // Puntero apuntando a null
        g2d.setColor(java.awt.Color.RED);
        g2d.drawString("puntero", x, y + 40);
        g2d.drawLine(x + 50, y + 35, x + 80, y + 35);
        g2d.drawString("null", x + 85, y + 40);
    }
    
    /**
     * Dibuja el estado después de una operación específica con análisis de caso
     */
    private void dibujarEstadoOperacion(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo, int espacioEntre, int paso, int ancho, int alto) {
        // Título con información de la operación
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("PASO " + paso + ": " + op.operacion, inicioX, inicioY - 40);
        
        if (!op.valor.isEmpty()) {
            g2d.setColor(java.awt.Color.DARK_GRAY);
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 11));
            g2d.drawString("Valor: " + op.valor, inicioX, inicioY - 25);
        }

        // Si es una operación de insertar derecha en lista doble, dibujar caso específico
        if (op.operacion.equals("INSERTAR_DERECHA") && op.estructura.equals("LISTA_DOBLE")) {
            dibujarCasoInsertarDerecha(g2d, op, inicioX, inicioY, tamanoNodo, ancho, alto);
            return;
        }

        // Si es insertar izquierda en lista doble, dibujar caso específico  
        if (op.operacion.equals("INSERTAR_IZQUIERDA") && op.estructura.equals("LISTA_DOBLE")) {
            dibujarCasoInsertarIzquierda(g2d, op, inicioX, inicioY, tamanoNodo, ancho, alto);
            return;
        }
        
        // Parsear el estado después para obtener los nodos
        String[] nodos = parsearEstadoLista(op.estadoDespues);
        
        if (nodos.length == 0) {
            // Lista quedó vacía después de la operación
            dibujarEstadoInicial(g2d, inicioX, inicioY + 20, ancho, alto);
            return;
        }
        
        // Obtener posición del puntero para resaltarlo
        int posicionPuntero = obtenerPosicionPuntero(op.estadoDespues);
        
        // Determinar si es lista doble
        boolean esListaDoble = op.estructura.equals("LISTA_DOBLE");
        
        // Ajustar espaciado mejorado
        int espacioMejorado = Math.max(80, espacioEntre + 20);
        
        // Dibujar cabeza
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 11));
        g2d.drawString("cabeza", inicioX, inicioY - 5);
        
        // Dibujar cola para lista doble
        if (esListaDoble && nodos.length > 0) {
            int colaX = inicioX + ((nodos.length - 1) * espacioMejorado);
            g2d.setColor(java.awt.Color.GREEN);
            g2d.drawString("cola", colaX, inicioY + tamanoNodo + 40);
            dibujarFlechaMejorada(g2d, colaX + 20, inicioY + tamanoNodo + 35, colaX + tamanoNodo/2, inicioY + tamanoNodo + 5, "cola");
        }
        
        // Dibujar nodos y enlaces
        for (int i = 0; i < nodos.length; i++) {
            int nodoX = inicioX + (i * espacioMejorado);
            int nodoY = inicioY;
            
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (i == posicionPuntero);
            
            // Dibujar enlace desde cabeza al primer nodo
            if (i == 0) {
                g2d.setColor(java.awt.Color.BLUE);
                dibujarFlechaMejorada(g2d, inicioX + 35, inicioY - 10, nodoX + tamanoNodo/2, nodoY, "cabeza");
            }
            
            // Dibujar nodo con resaltado si es donde está el puntero
            dibujarNodoMejorado(g2d, nodoX, nodoY, tamanoNodo, nodos[i], esPuntero, esListaDoble);
            
            // Dibujar enlaces entre nodos
            if (i < nodos.length - 1) {
                int siguienteX = nodoX + espacioMejorado;
                
                if (esListaDoble) {
                    // Lista doble: enlaces bidireccionales
                    // Flecha hacia adelante (arriba)
                    g2d.setColor(java.awt.Color.BLACK);
                    dibujarFlechaMejorada(g2d, nodoX + tamanoNodo, nodoY + tamanoNodo/3, 
                                        siguienteX, nodoY + tamanoNodo/3, "next");
                    
                    // Flecha hacia atrás (abajo)
                    g2d.setColor(java.awt.Color.GRAY);
                    dibujarFlechaMejorada(g2d, siguienteX, nodoY + (2*tamanoNodo)/3, 
                                        nodoX + tamanoNodo, nodoY + (2*tamanoNodo)/3, "prev");
                } else {
                    // Lista simple: solo enlace hacia adelante
                    g2d.setColor(java.awt.Color.BLACK);
                    dibujarFlechaMejorada(g2d, nodoX + tamanoNodo, nodoY + tamanoNodo/2, 
                                        siguienteX, nodoY + tamanoNodo/2, "next");
                }
            } else {
                // Último nodo apunta a null
                g2d.setColor(java.awt.Color.GRAY);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 9));
                int nullX = nodoX + tamanoNodo + 15;
                g2d.drawLine(nodoX + tamanoNodo, nodoY + tamanoNodo/2, nullX, nodoY + tamanoNodo/2);
                g2d.drawString("null", nullX + 5, nodoY + tamanoNodo/2 + 4);
                
                if (esListaDoble) {
                    // En lista doble, también mostrar prev del último nodo
                    g2d.drawString("prev", nodoX + tamanoNodo/4, nodoY + (3*tamanoNodo)/4);
                }
            }
        }
        
        // Dibujar indicador del puntero
        if (nodos.length > 0 && posicionPuntero >= 0 && posicionPuntero < nodos.length) {
            int punteroX = inicioX + (posicionPuntero * espacioMejorado);
            g2d.setColor(java.awt.Color.RED);
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 11));
            g2d.drawString("puntero", punteroX, inicioY - 20);
            dibujarFlechaMejorada(g2d, punteroX + 35, inicioY - 15, punteroX + tamanoNodo/2, inicioY, "ptr");
        } else if (nodos.length > 0 && posicionPuntero == -1) {
            // Puntero es null
            g2d.setColor(java.awt.Color.RED);
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 11));
            g2d.drawString("puntero -> null", inicioX, inicioY - 20);
        }
        
        // Explicación de la operación con mejor formato
        g2d.setColor(java.awt.Color.DARK_GRAY);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 10));
        String explicacion = obtenerExplicacionGraficaOperacion(op);
        String[] lineas = explicacion.split("\n");
        int explicacionY = inicioY + tamanoNodo + (esListaDoble ? 70 : 50);
        for (int i = 0; i < lineas.length && i < 4; i++) {
            g2d.drawString(lineas[i], inicioX, explicacionY + (i * 14));
        }
    }
    
    /**
     * Dibuja un nodo mejorado con mejor visualización
     */
    private void dibujarNodoMejorado(java.awt.Graphics2D g2d, int x, int y, int tamano, String valor, boolean esPuntero, boolean esListaDoble) {
        // Color de fondo según si es el puntero
        if (esPuntero) {
            // Nodo resaltado donde está el puntero
            g2d.setColor(new java.awt.Color(255, 255, 0, 200)); // Amarillo translúcido
            g2d.fillRect(x - 2, y - 2, tamano + 4, tamano + 4);
            g2d.setColor(java.awt.Color.RED);
            g2d.setStroke(new java.awt.BasicStroke(3));
            g2d.drawRect(x - 2, y - 2, tamano + 4, tamano + 4);
            g2d.setStroke(new java.awt.BasicStroke(1)); // Reset stroke
        }
        
        // Nodo principal
        g2d.setColor(esPuntero ? new java.awt.Color(255, 255, 200) : java.awt.Color.WHITE);
        g2d.fillRect(x, y, tamano, tamano);
        g2d.setColor(esPuntero ? java.awt.Color.RED : java.awt.Color.BLACK);
        g2d.setStroke(new java.awt.BasicStroke(esPuntero ? 2 : 1));
        g2d.drawRect(x, y, tamano, tamano);
        g2d.setStroke(new java.awt.BasicStroke(1)); // Reset stroke
        
        // Valor del nodo
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, esPuntero ? 13 : 12));
        g2d.setColor(esPuntero ? java.awt.Color.RED : java.awt.Color.BLACK);
        java.awt.FontMetrics fm = g2d.getFontMetrics();
        int textX = x + (tamano - fm.stringWidth(valor)) / 2;
        int textY = y + (tamano + fm.getAscent()) / 2;
        g2d.drawString(valor, textX, textY);
        
        // Para lista doble, dividir el nodo para mostrar prev y next
        if (esListaDoble) {
            g2d.setColor(java.awt.Color.LIGHT_GRAY);
            // Línea horizontal para separar prev/data/next
            g2d.drawLine(x, y + tamano/3, x + tamano, y + tamano/3);
            g2d.drawLine(x, y + 2*tamano/3, x + tamano, y + 2*tamano/3);
            
            // Etiquetas pequeñas
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
            g2d.setColor(java.awt.Color.GRAY);
            g2d.drawString("prev", x + 2, y + tamano/6 + 3);
            g2d.drawString("next", x + 2, y + 5*tamano/6 + 3);
        }
        
        // Sombra para mejor efecto visual
        if (esPuntero) {
            g2d.setColor(new java.awt.Color(255, 0, 0, 50));
            g2d.fillRect(x + 3, y + 3, tamano, tamano);
        }
    }
    
    /**
     * Dibuja una flecha mejorada con etiqueta
     */
    private void dibujarFlechaMejorada(java.awt.Graphics2D g2d, int x1, int y1, int x2, int y2, String etiqueta) {
        // Línea principal
        g2d.drawLine(x1, y1, x2, y2);
        
        // Calcular la punta de la flecha
        double angulo = Math.atan2(y2 - y1, x2 - x1);
        int longitud = 10;
        double anguloFlecha = Math.PI / 6;
        
        int x3 = (int) (x2 - longitud * Math.cos(angulo - anguloFlecha));
        int y3 = (int) (y2 - longitud * Math.sin(angulo - anguloFlecha));
        int x4 = (int) (x2 - longitud * Math.cos(angulo + anguloFlecha));
        int y4 = (int) (y2 - longitud * Math.sin(angulo + anguloFlecha));
        
        // Dibujar punta de flecha
        g2d.drawLine(x2, y2, x3, y3);
        g2d.drawLine(x2, y2, x4, y4);
        
        // Etiqueta en el medio de la flecha (opcional)
        if (etiqueta != null && !etiqueta.isEmpty() && etiqueta.length() <= 4) {
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
            int midX = (x1 + x2) / 2;
            int midY = (y1 + y2) / 2 - 5;
            g2d.drawString(etiqueta, midX - 8, midY);
        }
    }
    
    /**
     * Dibuja una flecha entre dos puntos
     */

    
    /**
     * Obtiene la posición del puntero desde el estado de la estructura
     */
    private int obtenerPosicionPuntero(String estado) {
        if (estado.contains("Puntero: null")) {
            return -1;
        }
        
        // Buscar patrón "Puntero: pos X"
        if (estado.contains("Puntero: pos ")) {
            try {
                int inicio = estado.indexOf("Puntero: pos ") + "Puntero: pos ".length();
                int fin = estado.indexOf(" ", inicio);
                if (fin == -1) fin = estado.length();
                String posStr = estado.substring(inicio, fin);
                return Integer.parseInt(posStr);
            } catch (Exception e) {
                return 0; // Por defecto posición 0
            }
        }
        
        return 0; // Por defecto posición 0
    }
    
    /**
     * Parsea el estado de la lista para obtener los valores de los nodos
     */
    private String[] parsearEstadoLista(String estado) {
        if (estado == null || estado.trim().isEmpty() || estado.contains("Lista vacía") || estado.contains("Estructura vacía")) {
            return new String[0];
        }
        
        // Buscar patrones como "[10] -> [20] -> [30] -> null" o "10, 20, 30"
        if (estado.contains("->")) {
            String[] partes = estado.split("->");
            java.util.ArrayList<String> valores = new java.util.ArrayList<>();
            for (String parte : partes) {
                parte = parte.trim();
                if (parte.startsWith("[") && parte.endsWith("]")) {
                    valores.add(parte.substring(1, parte.length() - 1));
                } else if (!parte.equals("null") && !parte.isEmpty()) {
                    valores.add(parte);
                }
            }
            return valores.toArray(new String[0]);
        } else if (estado.contains(",")) {
            String[] partes = estado.split(",");
            java.util.ArrayList<String> valores = new java.util.ArrayList<>();
            for (String parte : partes) {
                parte = parte.trim();
                if (!parte.isEmpty() && !parte.equals("null")) {
                    valores.add(parte);
                }
            }
            return valores.toArray(new String[0]);
        } else {
            // Un solo valor
            String valor = estado.trim();
            if (!valor.isEmpty() && !valor.equals("null") && !valor.contains("vacía")) {
                return new String[]{valor};
            }
        }
        
        return new String[0];
    }
    
    /**
     * Obtiene explicación gráfica específica para cada operación
     */
    private String obtenerExplicacionGraficaOperacion(OperacionAnalisis op) {
        switch (op.operacion) {
            case "INSERTAR_INICIO":
                return "Se crea nuevo nodo con valor " + op.valor + "\nSe actualiza 'cabeza' para apuntar al nuevo nodo\nEl puntero se posiciona en el nuevo nodo";
            case "INSERTAR_FINAL":
                return "Se crea nuevo nodo con valor " + op.valor + "\nSe conecta al final de la lista\nSe actualizan las referencias";
            case "INSERTAR_DERECHA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    // Analizar qué caso se ejecutó basándose en el estado
                    String caso = determinarCasoInsertarDerecha(op.estadoAntes, op.estadoDespues, op.valor);
                    return "📍 " + caso + "\nNuevo nodo: " + op.valor + "\nRefD y RefI actualizadas correctamente";
                } else {
                    return "Se crea nuevo nodo con valor " + op.valor + "\nSe inserta a la derecha del puntero\nSe actualizan los enlaces correctamente";
                }
            case "INSERTAR_IZQUIERDA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    String caso = determinarCasoInsertarIzquierda(op.estadoAntes, op.estadoDespues, op.valor);
                    return "📍 " + caso + "\nNuevo nodo: " + op.valor + "\nRefD y RefI actualizadas correctamente";
                } else {
                    return "Se crea nuevo nodo con valor " + op.valor + "\nSe inserta a la izquierda del puntero\nSe actualizan los enlaces correctamente";
                }
            case "ELIMINAR_INICIO":
                return "Se elimina el primer nodo (valor " + op.valor + ")\nLa 'cabeza' ahora apunta al siguiente nodo\nEl puntero se reposiciona automáticamente";
            case "ELIMINAR_FINAL":
                return "Se elimina el último nodo (valor " + op.valor + ")\nSe actualiza la referencia del penúltimo nodo\nSe verifica la posición del puntero";
            case "MOVER_PUNTERO_INICIO":
                return "El puntero se mueve al primer nodo\nPosición actualizada: Inicio de la lista\nNavegación TDA: Cabeza de la estructura";
            case "MOVER_PUNTERO_SIGUIENTE":
                return "El puntero avanza al siguiente nodo\nNavegación TDA: Recorrido secuencial\nSe sigue el enlace 'next'";
            case "MOVER_PUNTERO_ANTERIOR":
                return "El puntero retrocede al nodo anterior\nNavegación TDA: Solo en listas dobles\nSe sigue el enlace 'prev'";
            case "MOVER_PUNTERO_FINAL":
                return "El puntero se mueve al último nodo\nPosición actualizada: Final de la lista\nNavegación TDA: Cola de la estructura";
            default:
                return "Operación: " + op.operacion + "\nValor procesado: " + op.valor + "\nEstructura actualizada correctamente";
        }
    }

    private String obtenerExplicacionOperacion(OperacionAnalisis op) {
        StringBuilder explicacion = new StringBuilder();
        explicacion.append("EXPLICACIÓN TÉCNICA:\n");
        
        switch (op.operacion.toLowerCase()) {
            case "push":
            case "apilar":
                explicacion.append("1. Se crea un nuevo nodo en memoria\n");
                explicacion.append("2. Se asigna el valor al nodo\n");
                explicacion.append("3. El puntero 'siguiente' del nuevo nodo apunta al nodo que estaba en la cima\n");
                explicacion.append("4. El puntero 'cima' se actualiza para apuntar al nuevo nodo\n");
                explicacion.append("5. El tamaño se incrementa en 1");
                break;
            case "pop":
            case "desapilar":
                explicacion.append("1. Se verifica que la pila no esté vacía\n");
                explicacion.append("2. Se guarda la referencia al nodo de la cima\n");
                explicacion.append("3. El puntero 'cima' se actualiza para apuntar al siguiente nodo\n");
                explicacion.append("4. Se extrae el valor del nodo original\n");
                explicacion.append("5. Se libera la memoria del nodo eliminado\n");
                explicacion.append("6. El tamaño se decrementa en 1");
                break;
            case "enqueue":
            case "encolar":
                explicacion.append("1. Se crea un nuevo nodo en memoria\n");
                explicacion.append("2. Se asigna el valor al nodo\n");
                explicacion.append("3. Si la cola está vacía, tanto 'frente' como 'final' apuntan al nuevo nodo\n");
                explicacion.append("4. Si no está vacía, el 'siguiente' del nodo final apunta al nuevo nodo\n");
                explicacion.append("5. El puntero 'final' se actualiza para apuntar al nuevo nodo\n");
                explicacion.append("6. El tamaño se incrementa en 1");
                break;
            case "dequeue":
            case "desencolar":
                explicacion.append("1. Se verifica que la cola no esté vacía\n");
                explicacion.append("2. Se guarda la referencia al nodo del frente\n");
                explicacion.append("3. El puntero 'frente' se actualiza para apuntar al siguiente nodo\n");
                explicacion.append("4. Si era el último nodo, 'final' se actualiza a null\n");
                explicacion.append("5. Se extrae el valor del nodo original\n");
                explicacion.append("6. Se libera la memoria del nodo eliminado\n");
                explicacion.append("7. El tamaño se decrementa en 1");
                break;
            case "insertar inicio":
                explicacion.append("1. Se crea un nuevo nodo en memoria\n");
                explicacion.append("2. Se asigna el valor al nodo\n");
                explicacion.append("3. El puntero 'siguiente' del nuevo nodo apunta al primer nodo actual\n");
                explicacion.append("4. El puntero 'primero' se actualiza para apuntar al nuevo nodo\n");
                explicacion.append("5. El tamaño se incrementa en 1");
                break;
            case "insertar final":
                explicacion.append("1. Se crea un nuevo nodo en memoria\n");
                explicacion.append("2. Se asigna el valor al nodo\n");
                explicacion.append("3. Se recorre la lista hasta encontrar el último nodo\n");
                explicacion.append("4. El puntero 'siguiente' del último nodo apunta al nuevo nodo\n");
                explicacion.append("5. El tamaño se incrementa en 1");
                break;
            case "eliminar":
                explicacion.append("1. Se busca el nodo que contiene el valor\n");
                explicacion.append("2. Se actualiza el puntero 'siguiente' del nodo anterior\n");
                explicacion.append("3. Se desconecta el nodo a eliminar\n");
                explicacion.append("4. Se libera la memoria del nodo eliminado\n");
                explicacion.append("5. El tamaño se decrementa en 1");
                break;
            case "insertar_derecha":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    explicacion.append("ANÁLISIS EDUCATIVO POR CASOS:\n\n");
                    explicacion.append("📍 CASO 1 - Lista vacía (pLD = null):\n");
                    explicacion.append("   • El nuevo nodo se convierte en cabeza y cola\n");
                    explicacion.append("   • pLD apunta al nuevo nodo\n");
                    explicacion.append("   • RefD y RefI del nuevo nodo quedan null\n\n");
                    explicacion.append("📍 CASO 2 - Puntero en último nodo (pLD.RefD = null):\n");
                    explicacion.append("   • El nuevo nodo se agrega después del puntero\n");
                    explicacion.append("   • pLD.RefD → nuevo nodo (referencia derecha)\n");
                    explicacion.append("   • nuevo.RefI → pLD (referencia izquierda)\n");
                    explicacion.append("   • Cola se actualiza al nuevo nodo\n\n");
                    explicacion.append("📍 CASO 3 - Puntero en medio (pLD.RefD ≠ null):\n");
                    explicacion.append("   • Se guarda referencia al nodo siguiente\n");
                    explicacion.append("   • pLD.RefD → nuevo nodo\n");
                    explicacion.append("   • nuevo.RefI → pLD\n");
                    explicacion.append("   • nuevo.RefD → nodo siguiente original\n");
                    explicacion.append("   • siguiente.RefI → nuevo nodo\n");
                    explicacion.append("   • ¡Se mantiene integridad bidireccional!\n\n");
                    explicacion.append("🎯 RefD = Referencia Derecha (→)\n");
                    explicacion.append("🎯 RefI = Referencia Izquierda (←)");
                } else {
                    explicacion.append("Se inserta un nuevo nodo a la derecha de la posición indicada");
                }
                break;
            case "insertar_izquierda":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    explicacion.append("ANÁLISIS EDUCATIVO POR CASOS:\n\n");
                    explicacion.append("📍 CASO 1 - Lista vacía (pLD = null):\n");
                    explicacion.append("   • Similar a insertarDerecha cuando lista está vacía\n\n");
                    explicacion.append("📍 CASO 2 - Puntero en primer nodo (pLD.RefI = null):\n");
                    explicacion.append("   • El nuevo nodo se convierte en nueva cabeza\n");
                    explicacion.append("   • nuevo.RefD → pLD\n");
                    explicacion.append("   • pLD.RefI → nuevo nodo\n");
                    explicacion.append("   • Cabeza se actualiza al nuevo nodo\n\n");
                    explicacion.append("📍 CASO 3 - Puntero en medio (pLD.RefI ≠ null):\n");
                    explicacion.append("   • Se inserta entre el anterior y el puntero actual\n");
                    explicacion.append("   • anterior.RefD → nuevo nodo\n");
                    explicacion.append("   • nuevo.RefI → anterior\n");
                    explicacion.append("   • nuevo.RefD → pLD\n");
                    explicacion.append("   • pLD.RefI → nuevo nodo");
                } else {
                    explicacion.append("Se inserta un nuevo nodo a la izquierda de la posición indicada");
                }
                break;
            default:
                explicacion.append("Operación personalizada: ").append(op.operacion);
        }
        
        return explicacion.toString();
    }

    /**
     * Determina qué caso específico de insertarDerecha se ejecutó
     */
    private String determinarCasoInsertarDerecha(String estadoAntes, String estadoDespues, String valor) {
        if (estadoAntes.contains("Lista vacía") || estadoAntes.trim().isEmpty()) {
            return "CASO 1: Lista estaba vacía - Nuevo nodo es cabeza y cola";
        }
        
        // Contar nodos antes y después
        int nodosAntes = contarNodosEnEstado(estadoAntes);
        int nodosDespues = contarNodosEnEstado(estadoDespues);
        
        if (nodosDespues == nodosAntes + 1) {
            // Se insertó correctamente
            if (estadoDespues.endsWith(" " + valor + " ")) {
                return "CASO 2: Insertado al final - Puntero estaba en último nodo";
            } else if (estadoDespues.contains(" " + valor + " ")) {
                return "CASO 3: Insertado en medio - Puntero tenía nodo siguiente";
            }
        }
        
        return "Inserción completada - RefD y RefI enlazadas";
    }

    /**
     * Determina qué caso específico de insertarIzquierda se ejecutó
     */
    private String determinarCasoInsertarIzquierda(String estadoAntes, String estadoDespues, String valor) {
        if (estadoAntes.contains("Lista vacía") || estadoAntes.trim().isEmpty()) {
            return "CASO 1: Lista estaba vacía - Nuevo nodo es cabeza y cola";
        }
        
        if (estadoDespues.startsWith(" " + valor + " ")) {
            return "CASO 2: Insertado al inicio - Puntero estaba en primer nodo";
        } else if (estadoDespues.contains(" " + valor + " ")) {
            return "CASO 3: Insertado en medio - Puntero tenía nodo anterior";
        }
        
        return "Inserción completada - RefD y RefI enlazadas";
    }

    /**
     * Cuenta el número de nodos en un estado representado como string
     */
    private int contarNodosEnEstado(String estado) {
        if (estado.contains("Lista vacía") || estado.trim().isEmpty()) {
            return 0;
        }
        
        // Método simple: contar espacios entre números
        String[] partes = estado.trim().split("\\s+");
        int contador = 0;
        for (String parte : partes) {
            try {
                Integer.parseInt(parte);
                contador++;
            } catch (NumberFormatException e) {
                // Ignorar palabras que no sean números
            }
        }
        return contador;
    }

    /**
     * Dibuja específicamente los casos del método insertarDerecha
     */
    private void dibujarCasoInsertarDerecha(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Determinar qué caso fue ejecutado
        String caso = determinarCasoInsertarDerecha(op.estadoAntes, op.estadoDespues, op.valor);
        
        // Limpiar área
        g2d.setColor(java.awt.Color.WHITE);
        g2d.fillRect(0, inicioY, ancho, alto - inicioY);
        
        if (caso.contains("CASO 1")) {
            dibujarCaso1InsertarDerecha(g2d, op.valor, inicioX, inicioY, tamanoNodo);
        } else if (caso.contains("CASO 2")) {
            dibujarCaso2InsertarDerecha(g2d, op, inicioX, inicioY, tamanoNodo);
        } else if (caso.contains("CASO 3")) {
            dibujarCaso3InsertarDerecha(g2d, op, inicioX, inicioY, tamanoNodo);
        }
    }

    /**
     * CASO 1: Lista vacía - El nuevo nodo se convierte en cabeza y cola
     */
    private void dibujarCaso1InsertarDerecha(java.awt.Graphics2D g2d, String valor, int inicioX, int inicioY, int tamanoNodo) {
        // Título del caso
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("📍 CASO 1: Lista vacía (pLD = null)", inicioX, inicioY + 20);
        
        // Estado ANTES (lista vacía)
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("ANTES:", inicioX, inicioY + 50);
        
        // Dibujar cabeza apuntando a null
        g2d.setColor(java.awt.Color.BLUE);
        g2d.drawString("cabeza", inicioX, inicioY + 70);
        dibujarFlecha(g2d, inicioX + 50, inicioY + 65, inicioX + 100, inicioY + 65);
        g2d.setColor(java.awt.Color.RED);
        g2d.drawString("null", inicioX + 105, inicioY + 70);
        
        // Puntero pLD también null
        g2d.setColor(java.awt.Color.ORANGE);
        g2d.drawString("pLD", inicioX, inicioY + 90);
        dibujarFlecha(g2d, inicioX + 30, inicioY + 85, inicioX + 70, inicioY + 85);
        g2d.setColor(java.awt.Color.RED);
        g2d.drawString("null", inicioX + 75, inicioY + 90);
        
        // Flecha de transformación
        g2d.setColor(java.awt.Color.GREEN);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 16));
        g2d.drawString("⬇ insertarDerecha(" + valor + ") ⬇", inicioX + 50, inicioY + 120);
        
        // Estado DESPUÉS
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("DESPUÉS:", inicioX, inicioY + 150);
        
        // Dibujar el nuevo nodo (amarillo porque es nuevo)
        int nodoX = inicioX + 100;
        int nodoY = inicioY + 170;
        
        // Nodo nuevo resaltado
        g2d.setColor(java.awt.Color.YELLOW);
        g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
        g2d.setColor(java.awt.Color.BLACK);
        g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
        g2d.drawString(valor, nodoX + 15, nodoY + 25);
        
        // RefI del nuevo nodo apunta a null
        g2d.setColor(java.awt.Color.GRAY);
        g2d.drawString("RefI", nodoX - 40, nodoY + 15);
        dibujarFlecha(g2d, nodoX - 5, nodoY + 10, nodoX - 5, nodoY + 10);
        g2d.drawString("null", nodoX - 40, nodoY + 35);
        
        // RefD del nuevo nodo apunta a null
        g2d.setColor(java.awt.Color.GRAY);
        g2d.drawString("RefD", nodoX + 50, nodoY + 15);
        dibujarFlecha(g2d, nodoX + 45, nodoY + 10, nodoX + 45, nodoY + 10);
        g2d.drawString("null", nodoX + 50, nodoY + 35);
        
        // Cabeza apunta al nuevo nodo
        g2d.setColor(java.awt.Color.BLUE);
        g2d.drawString("cabeza", inicioX, inicioY + 180);
        dibujarFlecha(g2d, inicioX + 50, inicioY + 175, nodoX, nodoY + 20);
        
        // Cola apunta al nuevo nodo
        g2d.setColor(java.awt.Color.GREEN);
        g2d.drawString("cola", inicioX, inicioY + 200);
        dibujarFlecha(g2d, inicioX + 35, inicioY + 195, nodoX, nodoY + 30);
        
        // pLD apunta al nuevo nodo
        g2d.setColor(java.awt.Color.ORANGE);
        g2d.drawString("pLD", inicioX, inicioY + 220);
        dibujarFlecha(g2d, inicioX + 30, inicioY + 215, nodoX, nodoY + 40);
        
        // Explicación
        g2d.setColor(java.awt.Color.DARK_GRAY);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 10));
        g2d.drawString("• Nuevo nodo " + valor + " se convierte en cabeza y cola", inicioX, inicioY + 250);
        g2d.drawString("• pLD apunta al nuevo nodo", inicioX, inicioY + 265);
        g2d.drawString("• RefD y RefI del nuevo quedan null", inicioX, inicioY + 280);
    }

    /**
     * CASO 2: Puntero en último nodo - Insertar al final
     */
    private void dibujarCaso2InsertarDerecha(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo) {
        // Título del caso
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("📍 CASO 2: Puntero en último nodo (pLD.RefD = null)", inicioX, inicioY + 20);
        
        // Parsear nodos del estado anterior
        String[] nodosAntes = parsearEstadoLista(op.estadoAntes);
        
        // Estado ANTES
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("ANTES:", inicioX, inicioY + 50);
        
        // Dibujar lista existente
        int espacioNodo = 80;
        for (int i = 0; i < nodosAntes.length; i++) {
            int nodoX = inicioX + 50 + (i * espacioNodo);
            int nodoY = inicioY + 70;
            
            // Es el último nodo (donde está pLD)?
            boolean esUltimo = (i == nodosAntes.length - 1);
            
            // Dibujar nodo
            if (esUltimo) {
                // Resaltar nodo donde está el puntero pLD
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(nodoX - 2, nodoY - 2, tamanoNodo + 4, tamanoNodo + 4);
            }
            g2d.setColor(java.awt.Color.WHITE);
            g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.setColor(java.awt.Color.BLACK);
            g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.drawString(nodosAntes[i], nodoX + 15, nodoY + 25);
            
            // Enlaces RefD
            if (i < nodosAntes.length - 1) {
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 15, nodoX + espacioNodo - 5, nodoY + 15);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
                g2d.drawString("RefD", nodoX + tamanoNodo + 15, nodoY + 10);
            } else {
                // Último nodo: RefD apunta a null
                g2d.setColor(java.awt.Color.RED);
                g2d.drawString("RefD→null", nodoX + tamanoNodo + 5, nodoY + 15);
            }
            
            // pLD apunta al último nodo
            if (esUltimo) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("pLD", nodoX + 15, nodoY - 15);
                dibujarFlecha(g2d, nodoX + 20, nodoY - 10, nodoX + 20, nodoY);
            }
        }
        
        // Flecha de transformación
        g2d.setColor(java.awt.Color.GREEN);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 16));
        g2d.drawString("⬇ insertarDerecha(" + op.valor + ") ⬇", inicioX + 80, inicioY + 130);
        
        // Estado DESPUÉS
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("DESPUÉS:", inicioX, inicioY + 160);
        
        // Dibujar lista con nuevo nodo
        for (int i = 0; i < nodosAntes.length; i++) {
            int nodoX = inicioX + 50 + (i * espacioNodo);
            int nodoY = inicioY + 180;
            
            boolean esUltimo = (i == nodosAntes.length - 1);
            
            // Dibujar nodo existente
            if (esUltimo) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(nodoX - 2, nodoY - 2, tamanoNodo + 4, tamanoNodo + 4);
            }
            g2d.setColor(java.awt.Color.WHITE);
            g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.setColor(java.awt.Color.BLACK);
            g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.drawString(nodosAntes[i], nodoX + 15, nodoY + 25);
            
            // pLD sigue en el mismo nodo
            if (esUltimo) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("pLD", nodoX + 15, nodoY - 15);
                dibujarFlecha(g2d, nodoX + 20, nodoY - 10, nodoX + 20, nodoY);
            }
            
            // Enlaces RefD (ahora el último nodo apunta al nuevo)
            if (!esUltimo) {
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 15, nodoX + espacioNodo - 5, nodoY + 15);
            }
        }
        
        // Dibujar nuevo nodo (amarillo)
        int nuevoNodoX = inicioX + 50 + (nodosAntes.length * espacioNodo);
        int nuevoNodoY = inicioY + 180;
        
        g2d.setColor(java.awt.Color.YELLOW);
        g2d.fillRect(nuevoNodoX, nuevoNodoY, tamanoNodo, tamanoNodo);
        g2d.setColor(java.awt.Color.BLACK);
        g2d.drawRect(nuevoNodoX, nuevoNodoY, tamanoNodo, tamanoNodo);
        g2d.drawString(op.valor, nuevoNodoX + 15, nuevoNodoY + 25);
        
        // RefD del último nodo anterior apunta al nuevo
        int ultimoNodoX = inicioX + 50 + ((nodosAntes.length - 1) * espacioNodo);
        g2d.setColor(java.awt.Color.RED);
        dibujarFlecha(g2d, ultimoNodoX + tamanoNodo, nuevoNodoY + 15, nuevoNodoX, nuevoNodoY + 15);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
        g2d.drawString("RefD", ultimoNodoX + tamanoNodo + 5, nuevoNodoY + 10);
        
        // RefI del nuevo nodo apunta al anterior
        g2d.setColor(java.awt.Color.BLUE);
        dibujarFlecha(g2d, nuevoNodoX, nuevoNodoY + 25, ultimoNodoX + tamanoNodo, nuevoNodoY + 25);
        g2d.drawString("RefI", nuevoNodoX - 25, nuevoNodoY + 30);
        
        // RefD del nuevo nodo apunta a null
        g2d.setColor(java.awt.Color.RED);
        g2d.drawString("RefD→null", nuevoNodoX + tamanoNodo + 5, nuevoNodoY + 15);
        
        // Cola ahora apunta al nuevo nodo
        g2d.setColor(java.awt.Color.GREEN);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
        g2d.drawString("cola", nuevoNodoX + 15, nuevoNodoY + 55);
        dibujarFlecha(g2d, nuevoNodoX + 20, nuevoNodoY + 50, nuevoNodoX + 20, nuevoNodoY + tamanoNodo);
        
        // Explicación
        g2d.setColor(java.awt.Color.DARK_GRAY);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 10));
        g2d.drawString("• pLD.RefD → nuevo nodo " + op.valor + " (referencia derecha)", inicioX, inicioY + 250);
        g2d.drawString("• nuevo.RefI → pLD (referencia izquierda)", inicioX, inicioY + 265);
        g2d.drawString("• Cola se actualiza al nuevo nodo", inicioX, inicioY + 280);
    }

    /**
     * CASO 3: Puntero en medio - Insertar entre nodos
     */
    private void dibujarCaso3InsertarDerecha(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo) {
        // Título del caso
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("📍 CASO 3: Puntero en medio (pLD.RefD ≠ null)", inicioX, inicioY + 20);
        
        // Para simplificar, mostrar ejemplo con 3 nodos: [10] <-> [20] <-> [30]
        // pLD apunta a [20], insertar 25 a la derecha
        
        // Estado ANTES
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("ANTES: pLD en medio", inicioX, inicioY + 50);
        
        // Dibujar 3 nodos de ejemplo
        String[] nodosEjemplo = {"10", "20", "30"};
        int espacioNodo = 80;
        int punteroPos = 1; // pLD apunta al nodo del medio (índice 1)
        
        for (int i = 0; i < nodosEjemplo.length; i++) {
            int nodoX = inicioX + 30 + (i * espacioNodo);
            int nodoY = inicioY + 70;
            
            boolean esPuntero = (i == punteroPos);
            
            // Resaltar nodo donde está pLD
            if (esPuntero) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(nodoX - 2, nodoY - 2, tamanoNodo + 4, tamanoNodo + 4);
            }
            
            g2d.setColor(java.awt.Color.WHITE);
            g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.setColor(java.awt.Color.BLACK);
            g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.drawString(nodosEjemplo[i], nodoX + 15, nodoY + 25);
            
            // pLD apunta al nodo del medio
            if (esPuntero) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("pLD", nodoX + 15, nodoY - 15);
                dibujarFlecha(g2d, nodoX + 20, nodoY - 10, nodoX + 20, nodoY);
            }
            
            // Enlaces RefD
            if (i < nodosEjemplo.length - 1) {
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo - 5, nodoY + 12);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
                g2d.drawString("RefD", nodoX + tamanoNodo + 15, nodoY + 8);
            }
            
            // Enlaces RefI
            if (i > 0) {
                g2d.setColor(java.awt.Color.BLUE);
                dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo + 5, nodoY + 28);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 8));
                g2d.drawString("RefI", nodoX - 25, nodoY + 35);
            }
        }
        
        // Flecha de transformación
        g2d.setColor(java.awt.Color.GREEN);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 16));
        g2d.drawString("⬇ insertarDerecha(" + op.valor + ") ⬇", inicioX + 50, inicioY + 130);
        
        // Estado DESPUÉS
        g2d.setColor(java.awt.Color.BLACK);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 12));
        g2d.drawString("DESPUÉS: Nuevo nodo entre pLD y siguiente", inicioX, inicioY + 160);
        
        // Dibujar nodos existentes + nuevo nodo insertado
        String[] nodosConNuevo = {"10", "20", op.valor, "30"};
        int espacioReducido = 65;
        
        for (int i = 0; i < nodosConNuevo.length; i++) {
            int nodoX = inicioX + 20 + (i * espacioReducido);
            int nodoY = inicioY + 180;
            
            boolean esPuntero = (i == 1); // pLD sigue en el nodo original (índice 1)
            boolean esNuevo = (i == 2);   // El nuevo nodo está en índice 2
            
            // Resaltar nodos especiales
            if (esPuntero) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(nodoX - 2, nodoY - 2, tamanoNodo + 4, tamanoNodo + 4);
            } else if (esNuevo) {
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nodoX - 1, nodoY - 1, tamanoNodo + 2, tamanoNodo + 2);
            }
            
            g2d.setColor(java.awt.Color.WHITE);
            g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.setColor(java.awt.Color.BLACK);
            g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.drawString(nodosConNuevo[i], nodoX + 12, nodoY + 25);
            
            // Marcar pLD
            if (esPuntero) {
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("pLD", nodoX + 12, nodoY - 15);
                dibujarFlecha(g2d, nodoX + 17, nodoY - 10, nodoX + 17, nodoY);
            }
            
            // Marcar nuevo nodo
            if (esNuevo) {
                g2d.setColor(java.awt.Color.RED);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("NUEVO", nodoX + 5, nodoY + 55);
            }
            
            // Enlaces RefD con colores especiales
            if (i < nodosConNuevo.length - 1) {
                if (esPuntero || esNuevo) {
                    g2d.setColor(java.awt.Color.RED); // Enlaces nuevos en rojo
                } else {
                    g2d.setColor(java.awt.Color.BLACK);
                }
                dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioReducido - 5, nodoY + 12);
                
                if (esPuntero) {
                    g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 8));
                    g2d.drawString("RefD", nodoX + tamanoNodo + 8, nodoY + 8);
                }
            }
            
            // Enlaces RefI con colores especiales
            if (i > 0) {
                if (esPuntero || esNuevo) {
                    g2d.setColor(java.awt.Color.BLUE); // Enlaces nuevos en azul
                } else {
                    g2d.setColor(java.awt.Color.GRAY);
                }
                dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioReducido + tamanoNodo + 5, nodoY + 28);
                
                if (esNuevo) {
                    g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 8));
                    g2d.drawString("RefI", nodoX - 20, nodoY + 35);
                }
            }
        }
        
        // Explicación con pasos específicos
        g2d.setColor(java.awt.Color.DARK_GRAY);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 10));
        g2d.drawString("• Se guarda referencia al nodo siguiente (30)", inicioX, inicioY + 250);
        g2d.drawString("• pLD.RefD → nuevo nodo " + op.valor, inicioX, inicioY + 265);
        g2d.drawString("• nuevo.RefI → pLD, nuevo.RefD → nodo siguiente", inicioX, inicioY + 280);
        g2d.drawString("• siguiente.RefI → nuevo nodo ¡Integridad mantenida!", inicioX, inicioY + 295);
    }

    /**
     * Método similar para dibujar casos de insertarIzquierda
     */
    private void dibujarCasoInsertarIzquierda(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Similar implementación pero para insertar a la izquierda
        // Por ahora usar método genérico
        dibujarEstadoGenerico(g2d, op, inicioX, inicioY, tamanoNodo, ancho, alto);
    }

    /**
     * Método genérico para otras operaciones
     */
    private void dibujarEstadoGenerico(java.awt.Graphics2D g2d, OperacionAnalisis op, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Parsear el estado después para obtener los nodos
        String[] nodos = parsearEstadoLista(op.estadoDespues);
        
        if (nodos.length == 0) {
            // Lista quedó vacía después de la operación
            dibujarEstadoInicial(g2d, inicioX, inicioY + 20, ancho, alto);
            return;
        }
        
        // Obtener posición del puntero para resaltarlo
        int posicionPuntero = obtenerPosicionPuntero(op.estadoDespues);
        
        // Ajustar espaciado mejorado
        int espacioMejorado = Math.max(80, 60 + 20);
        
        // Dibujar nodos y enlaces usando método simple
        for (int i = 0; i < nodos.length; i++) {
            int nodoX = inicioX + (i * espacioMejorado);
            int nodoY = inicioY;
            
            // Determinar si este nodo es donde está el puntero
            boolean esPuntero = (i == posicionPuntero);
            
            // Dibujar nodo simple
            if (esPuntero) {
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nodoX - 2, nodoY - 2, tamanoNodo + 4, tamanoNodo + 4);
            }
            g2d.setColor(java.awt.Color.WHITE);
            g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.setColor(java.awt.Color.BLACK);
            g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
            g2d.drawString(nodos[i], nodoX + 15, nodoY + 25);
            
            // Enlaces entre nodos
            if (i < nodos.length - 1) {
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + tamanoNodo/2, 
                            nodoX + espacioMejorado, nodoY + tamanoNodo/2);
            }
        }
    }

    /**
     * Obtiene la posición real del puntero desde el estado
     */
    private int obtenerPosicionPunteroReal(String estado) {
        // Intentar extraer posición del puntero del estado
        if (estado.contains("Puntero: pos ")) {
            try {
                int inicio = estado.indexOf("Puntero: pos ") + "Puntero: pos ".length();
                int fin = estado.indexOf(" ", inicio);
                if (fin == -1) fin = estado.length();
                String posStr = estado.substring(inicio, fin);
                return Integer.parseInt(posStr);
            } catch (Exception e) {
                // Si no se puede parsear, usar heurística
            }
        }
        
        // Heurística: si hay 3 o más nodos, asumir que está en el medio
        String[] nodos = parsearEstadoLista(estado);
        if (nodos.length >= 3) {
            return nodos.length / 2; // Posición del medio
        } else if (nodos.length == 2) {
            return 0; // Primer nodo
        }
        
        return 0; // Por defecto
    }

    /**
     * Método auxiliar para dibujar flechas simples
     */
    private void dibujarFlecha(java.awt.Graphics2D g2d, int x1, int y1, int x2, int y2) {
        // Línea principal
        g2d.drawLine(x1, y1, x2, y2);
        
        // Calcular punta de flecha
        double dx = x2 - x1;
        double dy = y2 - y1;
        double angulo = Math.atan2(dy, dx);
        
        int longitud = 8;
        double anguloFlecha = Math.PI / 6; // 30 grados
        
        int x3 = (int) (x2 - longitud * Math.cos(angulo - anguloFlecha));
        int y3 = (int) (y2 - longitud * Math.sin(angulo - anguloFlecha));
        int x4 = (int) (x2 - longitud * Math.cos(angulo + anguloFlecha));
        int y4 = (int) (y2 - longitud * Math.sin(angulo + anguloFlecha));
        
        // Dibujar punta de flecha
        g2d.drawLine(x2, y2, x3, y3);
        g2d.drawLine(x2, y2, x4, y4);
    }

    /**
     * Determina el número máximo de sub-pasos para una operación
     */
    private int obtenerMaxSubPasos(OperacionAnalisis op) {
        switch (op.operacion) {
            case "INSERTAR_DERECHA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    // Sub-pasos: 1) Crear nodo, 2) Determinar caso, 3) Enlazar RefD, 4) Enlazar RefI, 5) Actualizar cabeza/cola
                    return 5;
                }
                return 3; // Lista simple: menos pasos
            case "INSERTAR_IZQUIERDA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    return 5; // Similar a insertar derecha
                }
                return 3;
            case "INSERTAR_FINAL":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    return 4; // 1) Estado inicial, 2) Crear nodo, 3) Enlaces, 4) Actualizar punteros
                }
                return 3;
            case "INSERTAR_INICIO":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    return 4; // 1) Estado inicial, 2) Crear nodo, 3) Enlaces, 4) Actualizar punteros
                }
                return 3;
            default:
                return 2; // Por defecto: antes y después
        }
    }

    /**
     * Obtiene la explicación específica para un sub-paso
     */
    private String obtenerExplicacionSubPaso(OperacionAnalisis op, int subPaso) {
        if (op.operacion.equals("INSERTAR_DERECHA") && op.estructura.equals("LISTA_DOBLE")) {
            return obtenerExplicacionSubPasoInsertarDerecha(op, subPaso);
        } else if (op.operacion.equals("INSERTAR_IZQUIERDA") && op.estructura.equals("LISTA_DOBLE")) {
            return obtenerExplicacionSubPasoInsertarIzquierda(op, subPaso);
        } else if (op.operacion.equals("INSERTAR_INICIO") && op.estructura.equals("LISTA_DOBLE")) {
            return obtenerExplicacionSubPasoInsertarInicio(op, subPaso);
        } else if (op.operacion.equals("INSERTAR_FINAL") && op.estructura.equals("LISTA_DOBLE")) {
            return obtenerExplicacionSubPasoInsertarFinal(op, subPaso);
        } else {
            // Para otras operaciones, usar explicación genérica
            switch (subPaso) {
                case 0: return "ANTES: " + op.estadoAntes;
                case 1: return "Ejecutando operación: " + op.operacion;
                case 2: return "DESPUÉS: " + op.estadoDespues;
                default: return "Operación completada";
            }
        }
    }

    /**
     * Explicación detallada por sub-pasos para insertarDerecha
     */
    private String obtenerExplicacionSubPasoInsertarDerecha(OperacionAnalisis op, int subPaso) {
        String caso = determinarCasoInsertarDerecha(op.estadoAntes, op.estadoDespues, op.valor);
        
        switch (subPaso) {
            case 0:
                return "SUB-PASO 1/5: ESTADO INICIAL\n" + 
                       "Estado antes: " + op.estadoAntes + "\n" + 
                       "Caso detectado: " + caso;
            case 1:
                return "SUB-PASO 2/5: CREAR NUEVO NODO\n" + 
                       "clsNodoDoble nAux = new clsNodoDoble(" + op.valor + ");\n" +
                       "El nuevo nodo está en memoria con RefD=null, RefI=null";
            case 2:
                if (caso.contains("CASO 1")) {
                    return "SUB-PASO 3/5: CASO 1 - LISTA VACÍA\n" + 
                           "if (pLD == null) {\n" + 
                           "    pLD = nAux;\n" + 
                           "    cabeza = cola = nAux;\n" + 
                           "}";
                } else if (caso.contains("CASO 2")) {
                    return "SUB-PASO 3/5: CASO 2 - ENLAZAR RefD\n" + 
                           "pLD.setRefD(nAux);  // pLD → nuevo nodo\n" + 
                           "El nodo donde está pLD ahora apunta al nuevo nodo";
                } else {
                    return "SUB-PASO 3/5: CASO 3 - GUARDAR SIGUIENTE\n" + 
                           "clsNodoDoble p1Siguiente = pLD.getRefD();\n" + 
                           "Se guarda referencia al nodo que sigue después de pLD";
                }
            case 3:
                if (caso.contains("CASO 1")) {
                    return "SUB-PASO 4/5: CASO 1 COMPLETADO\n" + 
                           "Lista inicializada con el primer nodo\n" + 
                           "cabeza, cola y pLD apuntan al nuevo nodo";
                } else if (caso.contains("CASO 2")) {
                    return "SUB-PASO 4/5: CASO 2 - ENLAZAR RefI\n" + 
                           "nAux.setRefI(pLD);  // nuevo nodo ← pLD\n" + 
                           "El nuevo nodo ahora apunta hacia atrás al pLD";
                } else {
                    return "SUB-PASO 4/5: CASO 3 - ENLAZAR AL NUEVO\n" + 
                           "pLD.setRefD(nAux);     // pLD → nuevo\n" + 
                           "nAux.setRefI(pLD);     // nuevo ← pLD";
                }
            case 4:
                if (caso.contains("CASO 2")) {
                    return "SUB-PASO 5/5: CASO 2 - ACTUALIZAR COLA\n" + 
                           "cola = nAux;  // La cola ahora es el nuevo nodo\n" + 
                           "Inserción al final completada";
                } else if (caso.contains("CASO 3")) {
                    return "SUB-PASO 5/5: CASO 3 - COMPLETAR ENLACES\n" + 
                           "nAux.setRefD(p1Siguiente);     // nuevo → siguiente\n" + 
                           "p1Siguiente.setRefI(nAux);     // siguiente ← nuevo\n" + 
                           "¡Integridad bidireccional mantenida!";
                } else {
                    return "SUB-PASO 5/5: INSERCIÓN COMPLETADA\n" + 
                           "Estado final: " + op.estadoDespues;
                }
            case 5:
                return "RESULTADO FINAL:\n" + 
                       "Estado después: " + op.estadoDespues + "\n" + 
                       "Operación insertarDerecha completada exitosamente";
            default:
                return "Operación completada";
        }
    }

    /**
     * Explicación detallada por sub-pasos para insertarIzquierda
     */
    private String obtenerExplicacionSubPasoInsertarIzquierda(OperacionAnalisis op, int subPaso) {
        String caso = determinarCasoInsertarIzquierda(op.estadoAntes, op.estadoDespues, op.valor);
        
        switch (subPaso) {
            case 0:
                return "SUB-PASO 1/5: ESTADO INICIAL\n" + 
                       "Estado antes: " + op.estadoAntes + "\n" + 
                       "Caso detectado: " + caso;
            case 1:
                return "SUB-PASO 2/5: CREAR NUEVO NODO\n" + 
                       "clsNodoDoble nAux = new clsNodoDoble(" + op.valor + ");\n" +
                       "El nuevo nodo está en memoria con RefD=null, RefI=null";
            case 2:
                if (caso.contains("CASO 1")) {
                    return "SUB-PASO 3/5: CASO 1 - LISTA VACÍA\n" + 
                           "if (pLD == null) {\n" + 
                           "    pLD = nAux;\n" + 
                           "    cabeza = cola = nAux;\n" + 
                           "}";
                } else if (caso.contains("CASO 2")) {
                    return "SUB-PASO 3/5: CASO 2 - INSERTAR AL INICIO\n" + 
                           "pLD está en la cabeza (RefI == null)\n" + 
                           "nAux.setRefD(pLD);  // nuevo → pLD\n" + 
                           "El nuevo nodo apunta hacia la derecha al pLD";
                } else {
                    return "SUB-PASO 3/5: CASO 3 - GUARDAR ANTERIOR\n" + 
                           "clsNodoDoble p1Anterior = pLD.getRefI();\n" + 
                           "Se guarda referencia al nodo anterior al pLD";
                }
            case 3:
                if (caso.contains("CASO 1")) {
                    return "SUB-PASO 4/5: CASO 1 COMPLETADO\n" + 
                           "Lista inicializada con el primer nodo\n" + 
                           "cabeza, cola y pLD apuntan al nuevo nodo";
                } else if (caso.contains("CASO 2")) {
                    return "SUB-PASO 4/5: CASO 2 - ENLAZAR RefI\n" + 
                           "pLD.setRefI(nAux);  // pLD ← nuevo\n" + 
                           "El pLD ahora apunta hacia atrás al nuevo nodo";
                } else {
                    return "SUB-PASO 4/5: CASO 3 - ENLAZAR AL ANTERIOR\n" + 
                           "p1Anterior.setRefD(nAux);  // anterior → nuevo\n" + 
                           "nAux.setRefI(p1Anterior);  // nuevo ← anterior";
                }
            case 4:
                if (caso.contains("CASO 2")) {
                    return "SUB-PASO 5/5: CASO 2 - ACTUALIZAR CABEZA\n" + 
                           "cabeza = nAux;  // La cabeza ahora es el nuevo nodo\n" + 
                           "Inserción al inicio completada";
                } else if (caso.contains("CASO 3")) {
                    return "SUB-PASO 5/5: CASO 3 - COMPLETAR ENLACES\n" + 
                           "nAux.setRefD(pLD);    // nuevo → pLD\n" + 
                           "pLD.setRefI(nAux);    // pLD ← nuevo\n" + 
                           "¡Integridad bidireccional mantenida!";
                } else {
                    return "SUB-PASO 5/5: INSERCIÓN COMPLETADA\n" + 
                           "Estado final: " + op.estadoDespues;
                }
            case 5:
                return "RESULTADO FINAL:\n" + 
                       "Estado después: " + op.estadoDespues + "\n" + 
                       "Operación insertarIzquierda completada exitosamente";
            default:
                return "Operación completada";
        }
    }

    /**
     * Explicación detallada por sub-pasos para insertarInicio
     */
    private String obtenerExplicacionSubPasoInsertarInicio(OperacionAnalisis op, int subPaso) {
        switch (subPaso) {
            case 0: 
                return "SUB-PASO 1/4: ESTADO INICIAL\n" + 
                       "Estado antes: " + op.estadoAntes + "\n" + 
                       "Preparando inserción al inicio de la lista";
            case 1:
                return "SUB-PASO 2/4: CREAR NUEVO NODO\n" + 
                       "clsNodoDoble nuevo = new clsNodoDoble(" + op.valor + ");\n" +
                       "Nuevo nodo creado en memoria";
            case 2:
                if (op.estadoAntes.equals("[]") || op.estadoAntes.isEmpty()) {
                    return "SUB-PASO 3/4: LISTA VACÍA\n" + 
                           "cabeza = cola = nuevo;\n" + 
                           "pLD = nuevo;\n" + 
                           "El nuevo nodo se convierte en el único elemento";
                } else {
                    return "SUB-PASO 3/4: ENLAZAR CON CABEZA EXISTENTE\n" + 
                           "nuevo.setRefD(cabeza);  // nuevo → cabeza actual\n" + 
                           "cabeza.setRefI(nuevo);  // cabeza ← nuevo";
                }
            case 3:
                if (op.estadoAntes.equals("[]") || op.estadoAntes.isEmpty()) {
                    return "SUB-PASO 4/4: INSERCIÓN COMPLETADA\n" + 
                           "Lista inicializada con el primer elemento\n" + 
                           "Estado final: " + op.estadoDespues;
                } else {
                    return "SUB-PASO 4/4: ACTUALIZAR CABEZA\n" + 
                           "cabeza = nuevo;\n" + 
                           "El nuevo nodo ahora es la cabeza de la lista\n" + 
                           "Estado final: " + op.estadoDespues;
                }
            default: 
                return "Inserción al inicio completada";
        }
    }

    /**
     * Explicación detallada por sub-pasos para insertarFinal
     */
    private String obtenerExplicacionSubPasoInsertarFinal(OperacionAnalisis op, int subPaso) {
        switch (subPaso) {
            case 0: 
                return "SUB-PASO 1/4: ESTADO INICIAL\n" + 
                       "Estado antes: " + op.estadoAntes + "\n" + 
                       "Preparando inserción al final de la lista";
            case 1:
                return "SUB-PASO 2/4: CREAR NUEVO NODO\n" + 
                       "clsNodoDoble nuevo = new clsNodoDoble(" + op.valor + ");\n" +
                       "Nuevo nodo creado en memoria";
            case 2:
                if (op.estadoAntes.equals("[]") || op.estadoAntes.isEmpty()) {
                    return "SUB-PASO 3/4: LISTA VACÍA\n" + 
                           "cabeza = cola = nuevo;\n" + 
                           "pLD = nuevo;\n" + 
                           "El nuevo nodo se convierte en el único elemento";
                } else {
                    return "SUB-PASO 3/4: ENLAZAR CON COLA EXISTENTE\n" + 
                           "cola.setRefD(nuevo);  // cola actual → nuevo\n" + 
                           "nuevo.setRefI(cola);  // nuevo ← cola actual";
                }
            case 3:
                if (op.estadoAntes.equals("[]") || op.estadoAntes.isEmpty()) {
                    return "SUB-PASO 4/4: INSERCIÓN COMPLETADA\n" + 
                           "Lista inicializada con el primer elemento\n" + 
                           "Estado final: " + op.estadoDespues;
                } else {
                    return "SUB-PASO 4/4: ACTUALIZAR COLA\n" + 
                           "cola = nuevo;\n" + 
                           "El nuevo nodo ahora es la cola de la lista\n" + 
                           "Estado final: " + op.estadoDespues;
                }
            default: 
                return "Inserción al final completada";
        }
    }

    /**
     * Nueva visualización con sub-pasos gráficos
     */
    private void dibujarVisualizacionAnalisisConSubPasos(java.awt.Graphics g, int ancho, int alto, int paso, int subPaso, java.util.ArrayList<OperacionAnalisis> operaciones) {
        java.awt.Graphics2D g2d = (java.awt.Graphics2D) g;
        g2d.setRenderingHint(java.awt.RenderingHints.KEY_ANTIALIASING, java.awt.RenderingHints.VALUE_ANTIALIAS_ON);
        
        if (paso == 0) {
            // Estado inicial
            dibujarEstadoInicial(g2d, 30, 50, ancho, alto);
            return;
        }
        
        if (paso <= operaciones.size()) {
            OperacionAnalisis op = operaciones.get(paso - 1);
            
            // Dibujar específicamente según la operación y sub-paso
            if (op.operacion.equals("INSERTAR_DERECHA") && op.estructura.equals("LISTA_DOBLE")) {
                dibujarSubPasosInsertarDerecha(g2d, op, subPaso, 30, 50, 40, ancho, alto);
            } else if (op.operacion.equals("INSERTAR_INICIO") && op.estructura.equals("LISTA_DOBLE")) {
                dibujarSubPasosInsertarInicio(g2d, op, subPaso, 30, 50, 40, ancho, alto);
            } else if (op.operacion.equals("INSERTAR_FINAL") && op.estructura.equals("LISTA_DOBLE")) {
                dibujarSubPasosInsertarFinal(g2d, op, subPaso, 30, 50, 40, ancho, alto);
            } else if (op.operacion.equals("INSERTAR_IZQUIERDA") && op.estructura.equals("LISTA_DOBLE")) {
                dibujarSubPasosInsertarIzquierda(g2d, op, subPaso, 30, 50, 40, ancho, alto);
            } else {
                // Para otras operaciones, usar visualización genérica
                dibujarVisualizacionGenerica(g2d, op, subPaso, 30, 50, 40, ancho, alto);
            }
        }
    }

    /**
     * Dibuja los sub-pasos específicos del método insertarDerecha
     */
    private void dibujarSubPasosInsertarDerecha(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Limpiar área
        g2d.setColor(java.awt.Color.WHITE);
        g2d.fillRect(0, 0, ancho, alto);
        
        // Título
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("insertarDerecha(" + op.valor + ") - Sub-paso " + (subPaso + 1) + "/5", inicioX, inicioY - 10);
        
        String caso = determinarCasoInsertarDerecha(op.estadoAntes, op.estadoDespues, op.valor);
        
        if (caso.contains("CASO 1")) {
            dibujarSubPasosCaso1(g2d, op.valor, subPaso, inicioX, inicioY + 20, tamanoNodo);
        } else if (caso.contains("CASO 2")) {
            dibujarSubPasosCaso2(g2d, op, subPaso, inicioX, inicioY + 20, tamanoNodo);
        } else {
            dibujarSubPasosCaso3(g2d, op, subPaso, inicioX, inicioY + 20, tamanoNodo);
        }
    }

    /**
     * Sub-pasos gráficos para Caso 1: Lista vacía
     */
    private void dibujarSubPasosCaso1(java.awt.Graphics2D g2d, String valor, int subPaso, int x, int y, int tamanoNodo) {
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 1: Estado inicial - Lista vacía", x, y);
                g2d.setColor(java.awt.Color.BLUE);
                g2d.drawString("cabeza → null", x, y + 30);
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("pLD → null", x, y + 50);
                break;
                
            case 1: // Crear nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 2: Crear nuevo nodo", x, y);
                g2d.drawString("clsNodoDoble nAux = new clsNodoDoble(" + valor + ");", x, y + 15);
                
                // Dibujar nuevo nodo con referencias internas visibles
                dibujarNodoNuevo(g2d, x + 100, y + 30, tamanoNodo, valor, "nAux");
                break;
                
            case 2: // Verificar condición
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 3: if (pLD == null) - VERDADERO", x, y);
                g2d.setColor(java.awt.Color.GREEN);
                g2d.drawString("✓ Lista está vacía, ejecutar CASO 1", x, y + 20);
                
                // Mostrar el nodo con referencias visibles
                dibujarNodoConReferencias(g2d, x + 100, y + 40, tamanoNodo, valor, 
                                        java.awt.Color.YELLOW, "nAux", false, false, null, null);
                break;
                
            case 3: // Asignar pLD
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 4: pLD = nAux;", x, y);
                g2d.drawString("El puntero ahora apunta al nuevo nodo", x, y + 15);
                
                // Nodo con referencias aún en null
                dibujarNodoConReferencias(g2d, x + 100, y + 40, tamanoNodo, valor, 
                                        java.awt.Color.YELLOW, "nAux/pLD", false, false, null, null);
                
                // Flecha de pLD apuntando al nodo
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("pLD apunta aquí", x + 50, y + 25);
                dibujarFlecha(g2d, x + 115, y + 30, x + 115, y + 40);
                break;
                
            case 4: // Asignar cabeza y cola
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 5: cabeza = cola = nAux", x, y);
                g2d.drawString("Lista inicializada - nodo único con RefD=null, RefI=null", x, y + 15);
                
                // Nodo final con referencias claramente visibles
                dibujarNodoConReferencias(g2d, x + 100, y + 60, tamanoNodo, valor, 
                                        java.awt.Color.GREEN, "cabeza/cola/pLD", false, false, null, null);
                
                // Indicadores de punteros
                g2d.setColor(java.awt.Color.BLUE);
                g2d.drawString("cabeza →", x + 40, y + 75);
                g2d.setColor(java.awt.Color.RED);
                g2d.drawString("cola →", x + 40, y + 90);
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("pLD →", x + 40, y + 105);
                
                // Nota importante
                g2d.setColor(java.awt.Color.GRAY);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.ITALIC, 10));
                g2d.drawString("Nota: Como es el único nodo, RefD y RefI permanecen null", x, y + 130);
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡Lista inicializada!", x, y + 145);
                break;
        }
    }

    /**
     * Sub-pasos gráficos para Caso 2: Insertar al final
     */
    private void dibujarSubPasosCaso2(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo) {
        String[] nodosAntes = parsearEstadoLista(op.estadoAntes);
        int espacioNodo = 70;
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 1: Estado inicial - pLD en último nodo", x, y);
                g2d.drawString("Lista existente con pLD apuntando al final", x, y + 15);
                
                // Dibujar lista existente con referencias visibles
                for (int i = 0; i < nodosAntes.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 40;
                    
                    boolean esUltimo = (i == nodosAntes.length - 1);
                    boolean esPrimero = (i == 0);
                    
                    // Determinar valores de referencias
                    String valorAnterior = esPrimero ? null : nodosAntes[i - 1];
                    String valorSiguiente = esUltimo ? null : nodosAntes[i + 1];
                    
                    // Dibujar nodo con referencias completas
                    java.awt.Color colorNodo = esUltimo ? java.awt.Color.ORANGE : java.awt.Color.WHITE;
                    String etiqueta = esUltimo ? "pLD" : null;
                    
                    dibujarNodoConReferencias(g2d, nodoX, nodoY, tamanoNodo, 
                                            nodosAntes[i], colorNodo, etiqueta,
                                            !esUltimo, // RefD solo si no es último
                                            !esPrimero, // RefI solo si no es primero
                                            valorSiguiente, valorAnterior);
                    
                    // Flecha entre nodos si no es el último
                    if (!esUltimo) {
                        g2d.setColor(java.awt.Color.BLACK);
                        dibujarFlecha(g2d, nodoX + tamanoNodo + 10, nodoY + 15, 
                                    nodoX + espacioNodo - 10, nodoY + 15);
                    }
                }
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 2: Crear nuevo nodo", x, y);
                g2d.drawString("clsNodoDoble nAux = new clsNodoDoble(" + op.valor + ");", x, y + 15);
                
                // Lista existente con nodo pLD mostrando sus referencias
                if (nodosAntes.length > 0) {
                    int ultimoX = x + ((nodosAntes.length - 1) * espacioNodo);
                    int ultimoY = y + 40;
                    
                    // Último nodo (pLD) con sus referencias visibles
                    String valorAnterior = (nodosAntes.length > 1) ? nodosAntes[nodosAntes.length - 2] : null;
                    dibujarNodoConReferencias(g2d, ultimoX, ultimoY, tamanoNodo, 
                                            nodosAntes[nodosAntes.length - 1], 
                                            java.awt.Color.ORANGE, "pLD",
                                            false, // RefD → null (está al final)
                                            nodosAntes.length > 1, // RefI → anterior si existe
                                            null, valorAnterior);
                }
                
                // Nuevo nodo con referencias en null
                if (nodosAntes.length > 0) {
                    int nuevoX = x + (nodosAntes.length * espacioNodo);
                    int nuevoY = y + 40;
                    dibujarNodoNuevo(g2d, nuevoX, nuevoY, tamanoNodo, op.valor, "nAux");
                }
                
                break;
                
            case 2: // Enlazar RefD del pLD al nuevo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 3: pLD.setRefD(nAux)", x, y);
                g2d.drawString("El último nodo (pLD) conecta su RefD al nuevo nodo", x, y + 15);
                
                // Lista con enlaces
                if (nodosAntes.length > 0) {
                    int ultimoX = x + 50;
                    int ultimoY = y + 40;
                    
                    // Último nodo (pLD) ahora con RefD apuntando al nuevo
                    String valorAnterior = (nodosAntes.length > 1) ? nodosAntes[nodosAntes.length - 2] : null;
                    dibujarNodoConReferencias(g2d, ultimoX, ultimoY, tamanoNodo, 
                                            nodosAntes[nodosAntes.length - 1], 
                                            java.awt.Color.ORANGE, "pLD",
                                            true, // RefD → nAux (¡CAMBIÓ!)
                                            nodosAntes.length > 1, // RefI → anterior si existe
                                            op.valor, valorAnterior);
                    
                    // Nuevo nodo aún con referencias null
                    int nuevoX2 = x + 150;
                    dibujarNodoNuevo(g2d, nuevoX2, ultimoY, tamanoNodo, op.valor, "nAux");
                    
                    // Flecha visual del nuevo enlace RefD
                    g2d.setColor(java.awt.Color.RED);
                    dibujarFlecha(g2d, ultimoX + tamanoNodo + 10, ultimoY + 15, nuevoX2 - 5, ultimoY + 15);
                    g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                    g2d.drawString("¡NUEVO ENLACE RefD!", ultimoX + tamanoNodo + 5, ultimoY + 35);
                }
                break;
                
            case 3: // Enlazar RefI del nuevo al pLD
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 4: nAux.setRefI(pLD)", x, y);
                g2d.drawString("El nuevo nodo establece su RefI hacia el nodo pLD", x, y + 15);
                
                if (nodosAntes.length > 0) {
                    int ultimoX = x + 50;
                    int ultimoY = y + 40;
                    
                    // Nodo pLD con su RefD ya apuntando al nuevo
                    String valorAnterior = (nodosAntes.length > 1) ? nodosAntes[nodosAntes.length - 2] : null;
                    dibujarNodoConReferencias(g2d, ultimoX, ultimoY, tamanoNodo, 
                                            nodosAntes[nodosAntes.length - 1], 
                                            java.awt.Color.ORANGE, "pLD",
                                            true, // RefD → nAux 
                                            nodosAntes.length > 1, // RefI → anterior
                                            op.valor, valorAnterior);
                    
                    // Nuevo nodo estableciendo su RefI
                    int nuevoX3 = x + 150;
                    dibujarNodoConReferencias(g2d, nuevoX3, ultimoY, tamanoNodo, 
                                            op.valor, java.awt.Color.YELLOW, "nAux",
                                            false, // RefD → null (será la cola)
                                            true, // RefI → pLD (¡CAMBIÓ!)
                                            null, nodosAntes[nodosAntes.length - 1]);
                    
                    // Flechas visuales de los enlaces bidireccionales
                    g2d.setColor(java.awt.Color.RED);
                    dibujarFlecha(g2d, ultimoX + tamanoNodo, ultimoY + 12, nuevoX3, ultimoY + 12);
                    
                    g2d.setColor(java.awt.Color.BLUE);
                    dibujarFlecha(g2d, nuevoX3, ultimoY + 28, ultimoX + tamanoNodo, ultimoY + 28);
                    g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                    g2d.drawString("¡ENLACE BIDIRECCIONAL!", ultimoX + tamanoNodo + 5, ultimoY + 50);
                }
                break;
                
            case 4: // Actualizar cola
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 5: cola = nAux", x, y);
                g2d.drawString("El puntero cola ahora apunta al nuevo nodo", x, y + 15);
                
                if (nodosAntes.length > 0) {
                    int ultimoX = x + 50;
                    int ultimoY = y + 40;
                    
                    // Último nodo (ex-cola, ahora pLD) con referencias completas
                    String valorAnterior = (nodosAntes.length > 1) ? nodosAntes[nodosAntes.length - 2] : null;
                    dibujarNodoConReferencias(g2d, ultimoX, ultimoY, tamanoNodo, 
                                            nodosAntes[nodosAntes.length - 1], 
                                            java.awt.Color.WHITE, "pLD",
                                            true, // RefD → nAux
                                            nodosAntes.length > 1, // RefI → anterior
                                            op.valor, valorAnterior);
                    
                    // Nuevo nodo (ahora es la nueva cola) con referencias completas
                    int nuevoX4 = x + 150;
                    dibujarNodoConReferencias(g2d, nuevoX4, ultimoY, tamanoNodo, 
                                            op.valor, java.awt.Color.GREEN, "NUEVA COLA",
                                            false, // RefD → null (es la cola)
                                            true, // RefI → pLD 
                                            null, nodosAntes[nodosAntes.length - 1]);
                    
                    // Flecha visual entre nodos
                    g2d.setColor(java.awt.Color.BLACK);
                    dibujarFlecha(g2d, ultimoX + tamanoNodo + 10, ultimoY + 15, nuevoX4 - 5, ultimoY + 15);
                    
                    // Indicador del puntero cola
                    g2d.setColor(java.awt.Color.GREEN);
                    g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                    g2d.drawString("cola →", nuevoX4 + 5, ultimoY + 65);
                    
                    g2d.setColor(java.awt.Color.GREEN);
                    g2d.drawString("¡Lista extendida correctamente!", x, ultimoY + 80);
                }
                break;
        }
    }

    /**
     * Sub-pasos gráficos para Caso 3: Insertar en medio
     */
    private void dibujarSubPasosCaso3(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo) {
        // Usar datos reales del usuario en lugar de ejemplo hardcodeado
        String[] nodosReales = parsearEstadoLista(op.estadoAntes);
        int espacioNodo = 65;
        
        // Determinar posición real del puntero
        int punteroPos = obtenerPosicionPunteroReal(op.estadoAntes);
        if (punteroPos == -1 && nodosReales.length > 1) {
            punteroPos = nodosReales.length / 2; // Usar posición del medio si no se puede determinar
        }
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 1: Estado inicial - pLD en posición " + (punteroPos + 1), x, y);
                g2d.drawString("Se insertará entre pLD y el nodo siguiente", x, y + 15);
                
                for (int i = 0; i < nodosReales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 40;
                    
                    boolean esUltimo = (i == nodosReales.length - 1);
                    boolean esPrimero = (i == 0);
                    boolean esPLD = (i == punteroPos);
                    
                    // Determinar valores de referencias
                    String valorAnterior = esPrimero ? null : nodosReales[i - 1];
                    String valorSiguiente = esUltimo ? null : nodosReales[i + 1];
                    
                    // Color y etiqueta del nodo
                    java.awt.Color colorNodo = esPLD ? java.awt.Color.ORANGE : java.awt.Color.WHITE;
                    String etiqueta = esPLD ? "pLD" : null;
                    
                    dibujarNodoConReferencias(g2d, nodoX, nodoY, tamanoNodo, 
                                            nodosReales[i], colorNodo, etiqueta,
                                            !esUltimo, // RefD solo si no es último
                                            !esPrimero, // RefI solo si no es primero
                                            valorSiguiente, valorAnterior);
                    
                    // Flecha entre nodos consecutivos
                    if (!esUltimo) {
                        g2d.setColor(java.awt.Color.BLACK);
                        dibujarFlecha(g2d, nodoX + tamanoNodo + 10, nodoY + 15, 
                                    nodoX + espacioNodo - 10, nodoY + 15);
                    }
                }
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 2: Crear nAux = new clsNodoDoble(" + op.valor + ")", x, y);
                
                // Lista simplificada con pLD usando datos reales
                if (punteroPos < nodosReales.length) {
                    int pldX = x + (punteroPos * espacioNodo);
                    int pldY = y + 30;
                    g2d.setColor(java.awt.Color.ORANGE);
                    g2d.fillRect(pldX - 2, pldY - 2, tamanoNodo + 4, tamanoNodo + 4);
                    g2d.setColor(java.awt.Color.WHITE);
                    g2d.fillRect(pldX, pldY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(pldX, pldY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[punteroPos], pldX + 15, pldY + 25);
                    g2d.setColor(java.awt.Color.ORANGE);
                    g2d.drawString("pLD", pldX + 10, pldY - 10);
                    
                    // Nodo siguiente (si existe)
                    if (punteroPos + 1 < nodosReales.length) {
                        int sigX = pldX + espacioNodo;
                        g2d.setColor(java.awt.Color.WHITE);
                        g2d.fillRect(sigX, pldY, tamanoNodo, tamanoNodo);
                        g2d.setColor(java.awt.Color.BLACK);
                        g2d.drawRect(sigX, pldY, tamanoNodo, tamanoNodo);
                        g2d.drawString(nodosReales[punteroPos + 1], sigX + 15, pldY + 25);
                    }
                }
                
                // NUEVO NODO con referencias visibles
                int nuevoX = x + 200;
                int nuevoY = y + 70;
                dibujarNodoNuevo(g2d, nuevoX, nuevoY, tamanoNodo, op.valor, "nAux");
                break;
                
            case 2: // Guardar p1Siguiente
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 3: p1Siguiente = pLD.getRefD()", x, y);
                
                // Mostrar la operación de guardar referencia con datos reales
                if (punteroPos < nodosReales.length) {
                    int pldX2 = x + (punteroPos * espacioNodo);
                    int pldY2 = y + 40;
                    
                    // pLD
                    g2d.setColor(java.awt.Color.ORANGE);
                    g2d.fillRect(pldX2 - 2, pldY2 - 2, tamanoNodo + 4, tamanoNodo + 4);
                    g2d.setColor(java.awt.Color.WHITE);
                    g2d.fillRect(pldX2, pldY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(pldX2, pldY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[punteroPos], pldX2 + 15, pldY2 + 25);
                    
                    // Nodo siguiente (p1Siguiente) - usar datos reales
                    if (punteroPos + 1 < nodosReales.length) {
                        int sigX2 = pldX2 + espacioNodo;
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                        g2d.fillRect(sigX2 - 2, pldY2 - 2, tamanoNodo + 4, tamanoNodo + 4);
                        g2d.setColor(java.awt.Color.WHITE);
                        g2d.fillRect(sigX2, pldY2, tamanoNodo, tamanoNodo);
                        g2d.setColor(java.awt.Color.BLACK);
                        g2d.drawRect(sigX2, pldY2, tamanoNodo, tamanoNodo);
                        g2d.drawString(nodosReales[punteroPos + 1], sigX2 + 15, pldY2 + 25);
                        g2d.drawString("p1Siguiente", sigX2 - 5, pldY2 - 10);
                    }
                }
                break;
                
            case 3: // Enlazar pLD al nuevo y nuevo a pLD
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 4: pLD.setRefD(nAux) y nAux.setRefI(pLD)", x, y);
                g2d.drawString("Establecer enlaces bidireccionales entre pLD y nAux", x, y + 15);
                
                int pldX3 = x + 30;
                int pldY3 = y + 40;
                
                // pLD - con RefD ahora apuntando al nuevo nodo
                String valorPLD = (punteroPos < nodosReales.length) ? nodosReales[punteroPos] : "null";
                String valorAnterior = (punteroPos > 0) ? nodosReales[punteroPos - 1] : null;
                dibujarNodoConReferencias(g2d, pldX3, pldY3, tamanoNodo, 
                                        valorPLD, java.awt.Color.ORANGE, "pLD",
                                        true, // RefD → nAux (¡CAMBIÓ!)
                                        punteroPos > 0, // RefI → anterior
                                        op.valor, valorAnterior);
                
                // Nuevo nodo estableciendo RefI hacia pLD
                int nuevoX3 = pldX3 + espacioNodo;
                dibujarNodoConReferencias(g2d, nuevoX3, pldY3, tamanoNodo, 
                                        op.valor, java.awt.Color.YELLOW, "nAux",
                                        false, // RefD → null por ahora
                                        true, // RefI → pLD (¡CAMBIÓ!)
                                        null, valorPLD);
                
                // Flecha visual del enlace bidireccional
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, pldX3 + tamanoNodo + 10, pldY3 + 15, nuevoX3 - 5, pldY3 + 15);
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
                g2d.drawString("¡Enlaces bidireccionales establecidos!", x, pldY3 + 60);
                break;
                
            case 4: // Completar enlaces con p1Siguiente
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 5: nAux.setRefD(p1Siguiente) y p1Siguiente.setRefI(nAux)", x, y);
                g2d.drawString("Completar enlaces para integrar nAux en la cadena", x, y + 15);
                
                int pldX4 = x + 10;
                int pldY4 = y + 40;
                
                // pLD - con sus referencias completas
                String valorPLD4 = (punteroPos < nodosReales.length) ? nodosReales[punteroPos] : "null";
                String valorAnteriorPLD = (punteroPos > 0) ? nodosReales[punteroPos - 1] : null;
                dibujarNodoConReferencias(g2d, pldX4, pldY4, tamanoNodo, 
                                        valorPLD4, java.awt.Color.ORANGE, "pLD",
                                        true, // RefD → nAux
                                        punteroPos > 0, // RefI → anterior
                                        op.valor, valorAnteriorPLD);
                
                // Nuevo nodo con todas sus referencias establecidas
                int nuevoX4 = pldX4 + espacioNodo;
                String valorSiguiente = (punteroPos + 1 < nodosReales.length) ? nodosReales[punteroPos + 1] : null;
                dibujarNodoConReferencias(g2d, nuevoX4, pldY4, tamanoNodo, 
                                        op.valor, java.awt.Color.YELLOW, "nAux",
                                        punteroPos + 1 < nodosReales.length, // RefD → p1Siguiente
                                        true, // RefI → pLD
                                        valorSiguiente, valorPLD4);
                
                // p1Siguiente con RefI actualizada
                int sigX4 = nuevoX4 + espacioNodo;
                if (punteroPos + 1 < nodosReales.length) {
                    String valorSigSig = (punteroPos + 2 < nodosReales.length) ? nodosReales[punteroPos + 2] : null;
                    dibujarNodoConReferencias(g2d, sigX4, pldY4, tamanoNodo, 
                                            nodosReales[punteroPos + 1], java.awt.Color.WHITE, "p1Siguiente",
                                            punteroPos + 2 < nodosReales.length, // RefD → siguiente
                                            true, // RefI → nAux (¡CAMBIÓ!)
                                            valorSigSig, op.valor);
                }
                
                // Flechas visuales entre nodos
                g2d.setColor(java.awt.Color.BLACK);
                dibujarFlecha(g2d, pldX4 + tamanoNodo + 10, pldY4 + 15, nuevoX4 - 5, pldY4 + 15);
                if (punteroPos + 1 < nodosReales.length) {
                    dibujarFlecha(g2d, nuevoX4 + tamanoNodo + 10, pldY4 + 15, sigX4 - 5, pldY4 + 15);
                }
                
                // Mensaje de éxito
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡INTEGRIDAD BIDIRECCIONAL COMPLETA!", x, pldY4 + 70);
                break;
        }
    }

    /**
     * Dibuja los sub-pasos específicos del método insertarInicio
     */
    private void dibujarSubPasosInsertarInicio(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Limpiar área
        g2d.setColor(java.awt.Color.WHITE);
        g2d.fillRect(0, 0, ancho, alto);
        
        // Título
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("insertarInicio(" + op.valor + ") - Sub-paso " + (subPaso + 1) + "/4", inicioX, inicioY - 10);
        
        // Parsear datos reales
        String[] nodosReales = parsearEstadoLista(op.estadoAntes);
        boolean listaVacia = nodosReales.length == 0 || op.estadoAntes.equals("[]");
        
        int x = inicioX + 50;
        int y = inicioY + 30;
        int espacioNodo = tamanoNodo + 30;
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 1: ESTADO INICIAL", x, y);
                g2d.drawString("Estado antes: " + op.estadoAntes, x, y + 20);
                
                if (!listaVacia) {
                    // Dibujar lista existente
                    for (int i = 0; i < nodosReales.length; i++) {
                        int nodoX = x + (i * espacioNodo);
                        int nodoY = y + 40;
                        
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                        g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.setColor(java.awt.Color.BLACK);
                        g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                        
                        if (i == 0) {
                            g2d.setColor(java.awt.Color.GREEN);
                            g2d.drawString("cabeza", nodoX - 5, nodoY - 10);
                        }
                        if (i == nodosReales.length - 1) {
                            g2d.setColor(java.awt.Color.RED);
                            g2d.drawString("cola", nodoX + 10, nodoY + 55);
                        }
                        
                        // Dibujar flechas RefD
                        if (i < nodosReales.length - 1) {
                            g2d.setColor(java.awt.Color.RED);
                            dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                        }
                        // Dibujar flechas RefI
                        if (i > 0) {
                            g2d.setColor(java.awt.Color.BLUE);
                            dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                        }
                    }
                } else {
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawString("Lista vacía: cabeza = null, cola = null", x, y + 60);
                }
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 2: CREAR NUEVO NODO", x, y);
                g2d.drawString("clsNodoDoble nuevo = new clsNodoDoble(" + op.valor + ");", x, y + 20);
                
                // Dibujar nuevo nodo en amarillo
                int nuevoX1 = x - espacioNodo;
                int nuevoY1 = y + 40;
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nuevoX1, nuevoY1, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(nuevoX1, nuevoY1, tamanoNodo, tamanoNodo);
                g2d.drawString(op.valor, nuevoX1 + 15, nuevoY1 + 25);
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("NUEVO", nuevoX1 + 5, nuevoY1 - 10);
                
                // Dibujar lista existente si la hay
                if (!listaVacia) {
                    for (int i = 0; i < nodosReales.length; i++) {
                        int nodoX = x + (i * espacioNodo);
                        int nodoY = y + 40;
                        
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                        g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.setColor(java.awt.Color.BLACK);
                        g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                        
                        if (i == 0) {
                            g2d.setColor(java.awt.Color.GREEN);
                            g2d.drawString("cabeza", nodoX - 5, nodoY - 10);
                        }
                    }
                }
                break;
                
            case 2: // Enlaces
                g2d.setColor(java.awt.Color.BLACK);
                if (listaVacia) {
                    g2d.drawString("PASO 3: LISTA VACÍA - INICIALIZAR", x, y);
                    g2d.drawString("cabeza = cola = nuevo;", x, y + 20);
                    
                    int nodoX = x + espacioNodo;
                    int nodoY = y + 40;
                    g2d.setColor(java.awt.Color.YELLOW);
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(op.valor, nodoX + 15, nodoY + 25);
                    
                    g2d.setColor(java.awt.Color.GREEN);
                    g2d.drawString("cabeza", nodoX - 5, nodoY - 10);
                    g2d.setColor(java.awt.Color.RED);
                    g2d.drawString("cola", nodoX + 10, nodoY + 55);
                } else {
                    g2d.drawString("PASO 3: ENLAZAR CON CABEZA EXISTENTE", x, y);
                    g2d.drawString("nuevo.setRefD(cabeza);  cabeza.setRefI(nuevo);", x, y + 20);
                    
                    int nuevoX2 = x - espacioNodo;
                    int nuevoY2 = y + 40;
                    g2d.setColor(java.awt.Color.YELLOW);
                    g2d.fillRect(nuevoX2, nuevoY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nuevoX2, nuevoY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(op.valor, nuevoX2 + 15, nuevoY2 + 25);
                    
                    // Cabeza actual
                    int cabezaX = x;
                    g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    g2d.fillRect(cabezaX, nuevoY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(cabezaX, nuevoY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[0], cabezaX + 15, nuevoY2 + 25);
                    g2d.setColor(java.awt.Color.GREEN);
                    g2d.drawString("ex-cabeza", cabezaX - 5, nuevoY2 - 10);
                    
                    // Enlaces bidireccionales
                    g2d.setColor(java.awt.Color.RED);
                    dibujarFlecha(g2d, nuevoX2 + tamanoNodo, nuevoY2 + 12, cabezaX, nuevoY2 + 12);
                    g2d.drawString("RefD", nuevoX2 + tamanoNodo + 5, nuevoY2 + 8);
                    
                    g2d.setColor(java.awt.Color.BLUE);
                    dibujarFlecha(g2d, cabezaX, nuevoY2 + 28, nuevoX2 + tamanoNodo, nuevoY2 + 28);
                    g2d.drawString("RefI", cabezaX - 20, nuevoY2 + 35);
                }
                break;
                
            case 3: // Finalizar
                g2d.setColor(java.awt.Color.BLACK);
                if (listaVacia) {
                    g2d.drawString("PASO 4: INSERCIÓN COMPLETADA", x, y);
                    g2d.drawString("pLD = nuevo; (lista inicializada)", x, y + 20);
                } else {
                    g2d.drawString("PASO 4: ACTUALIZAR CABEZA", x, y);
                    g2d.drawString("cabeza = nuevo;", x, y + 20);
                }
                
                // Dibujar estado final
                String[] nodosFinales = parsearEstadoLista(op.estadoDespues);
                for (int i = 0; i < nodosFinales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 40;
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.YELLOW); // El nuevo nodo
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosFinales[i], nodoX + 15, nodoY + 25);
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.GREEN);
                        g2d.drawString("NUEVA cabeza", nodoX - 15, nodoY - 10);
                    }
                    
                    // Enlaces
                    if (i < nodosFinales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                    if (i > 0) {
                        g2d.setColor(java.awt.Color.BLUE);
                        dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                    }
                }
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡INSERCIÓN AL INICIO COMPLETADA!", x, y + 100);
                break;
        }
    }

    /**
     * Dibuja los sub-pasos específicos del método insertarFinal
     */
    private void dibujarSubPasosInsertarFinal(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Limpiar área
        g2d.setColor(java.awt.Color.WHITE);
        g2d.fillRect(0, 0, ancho, alto);
        
        // Título
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("insertarFinal(" + op.valor + ") - Sub-paso " + (subPaso + 1) + "/4", inicioX, inicioY - 10);
        
        // Parsear datos reales
        String[] nodosReales = parsearEstadoLista(op.estadoAntes);
        boolean listaVacia = nodosReales.length == 0 || op.estadoAntes.equals("[]");
        
        int x = inicioX + 50;
        int y = inicioY + 30;
        int espacioNodo = tamanoNodo + 30;
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 1: ESTADO INICIAL", x, y);
                g2d.drawString("Estado antes: " + op.estadoAntes, x, y + 20);
                
                if (!listaVacia) {
                    // Dibujar lista existente con referencias completas
                    for (int i = 0; i < nodosReales.length; i++) {
                        int nodoX = x + (i * espacioNodo);
                        int nodoY = y + 40;
                        
                        boolean esUltimo = (i == nodosReales.length - 1);
                        boolean esPrimero = (i == 0);
                        
                        // Determinar valores de referencias
                        String valorAnteriorFinal = esPrimero ? null : nodosReales[i - 1];
                        String valorSiguienteFinal = esUltimo ? null : nodosReales[i + 1];
                        
                        // Etiquetas especiales
                        String etiqueta = null;
                        if (esPrimero) etiqueta = "cabeza";
                        if (esUltimo) etiqueta = "cola";
                        
                        dibujarNodoConReferencias(g2d, nodoX, nodoY, tamanoNodo, 
                                                nodosReales[i], java.awt.Color.LIGHT_GRAY, etiqueta,
                                                !esUltimo, // RefD solo si no es último
                                                !esPrimero, // RefI solo si no es primero
                                                valorSiguienteFinal, valorAnteriorFinal);
                        
                        // Flecha entre nodos consecutivos
                        if (!esUltimo) {
                            g2d.setColor(java.awt.Color.BLACK);
                            dibujarFlecha(g2d, nodoX + tamanoNodo + 10, nodoY + 15, 
                                        nodoX + espacioNodo - 10, nodoY + 15);
                        }
                    }
                } else {
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawString("Lista vacía: cabeza = null, cola = null", x, y + 60);
                }
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("PASO 2: CREAR NUEVO NODO", x, y);
                g2d.drawString("clsNodoDoble nuevo = new clsNodoDoble(" + op.valor + ");", x, y + 20);
                
                // Dibujar lista existente si la hay
                if (!listaVacia) {
                    for (int i = 0; i < nodosReales.length; i++) {
                        int nodoX = x + (i * espacioNodo);
                        int nodoY = y + 40;
                        
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                        g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.setColor(java.awt.Color.BLACK);
                        g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                        g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                        
                        if (i == nodosReales.length - 1) {
                            g2d.setColor(java.awt.Color.RED);
                            g2d.drawString("cola", nodoX + 10, nodoY - 10);
                        }
                    }
                }
                
                // Dibujar nuevo nodo con referencias en null
                int nuevoX = listaVacia ? x + espacioNodo : x + (nodosReales.length * espacioNodo);
                int nuevoY = y + 40;
                dibujarNodoNuevo(g2d, nuevoX, nuevoY, tamanoNodo, op.valor, "NUEVO");
                break;
                
            case 2: // Enlaces
                g2d.setColor(java.awt.Color.BLACK);
                if (listaVacia) {
                    g2d.drawString("PASO 3: LISTA VACÍA - INICIALIZAR", x, y);
                    g2d.drawString("cabeza = cola = nuevo;", x, y + 20);
                    
                    int nodoX = x + espacioNodo;
                    int nodoY = y + 40;
                    dibujarNodoConReferencias(g2d, nodoX, nodoY, tamanoNodo, 
                                            op.valor, java.awt.Color.GREEN, "cabeza/cola",
                                            false, // RefD → null (único nodo)
                                            false, // RefI → null (único nodo)
                                            null, null);
                } else {
                    g2d.drawString("PASO 3: ENLAZAR CON COLA EXISTENTE", x, y);
                    g2d.drawString("cola.setRefD(nuevo);  nuevo.setRefI(cola);", x, y + 20);
                    
                    // Cola actual con RefD ahora apuntando al nuevo
                    int colaX = x + ((nodosReales.length - 1) * espacioNodo);
                    int colaY = y + 40;
                    String valorAnteriorCola = (nodosReales.length > 1) ? nodosReales[nodosReales.length - 2] : null;
                    dibujarNodoConReferencias(g2d, colaX, colaY, tamanoNodo, 
                                            nodosReales[nodosReales.length - 1], 
                                            java.awt.Color.LIGHT_GRAY, "ex-cola",
                                            true, // RefD → nuevo (¡CAMBIÓ!)
                                            nodosReales.length > 1, // RefI → anterior
                                            op.valor, valorAnteriorCola);
                    
                    // Nuevo nodo estableciendo RefI hacia ex-cola
                    int nuevoX3 = colaX + espacioNodo;
                    dibujarNodoConReferencias(g2d, nuevoX3, colaY, tamanoNodo, 
                                            op.valor, java.awt.Color.YELLOW, "nuevo",
                                            false, // RefD → null (será la nueva cola)
                                            true, // RefI → ex-cola (¡CAMBIÓ!)
                                            null, nodosReales[nodosReales.length - 1]);
                    
                    // Flecha visual del enlace
                    g2d.setColor(java.awt.Color.BLACK);
                    dibujarFlecha(g2d, colaX + tamanoNodo + 10, colaY + 15, nuevoX3 - 5, colaY + 15);
                }
                break;
                
            case 3: // Finalizar
                g2d.setColor(java.awt.Color.BLACK);
                if (listaVacia) {
                    g2d.drawString("PASO 4: INSERCIÓN COMPLETADA", x, y);
                    g2d.drawString("pLD = nuevo; (lista inicializada)", x, y + 20);
                } else {
                    g2d.drawString("PASO 4: ACTUALIZAR COLA", x, y);
                    g2d.drawString("cola = nuevo;", x, y + 20);
                }
                
                // Dibujar estado final con referencias completas
                String[] nodosFinales = parsearEstadoLista(op.estadoDespues);
                for (int i = 0; i < nodosFinales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 40;
                    
                    boolean esUltimoFinal = (i == nodosFinales.length - 1);
                    boolean esPrimeroFinal = (i == 0);
                    boolean esNuevo = esUltimoFinal; // El nuevo siempre es el último
                    
                    // Determinar valores de referencias
                    String valorAnteriorFin = esPrimeroFinal ? null : nodosFinales[i - 1];
                    String valorSiguienteFin = esUltimoFinal ? null : nodosFinales[i + 1];
                    
                    // Color y etiqueta
                    java.awt.Color colorFinal = esNuevo ? java.awt.Color.GREEN : java.awt.Color.LIGHT_GRAY;
                    String etiquetaFinal = null;
                    if (esPrimeroFinal) etiquetaFinal = "cabeza";
                    if (esUltimoFinal) etiquetaFinal = "NUEVA cola";
                    
                    dibujarNodoConReferencias(g2d, nodoX, nodoY, tamanoNodo, 
                                            nodosFinales[i], colorFinal, etiquetaFinal,
                                            !esUltimoFinal, // RefD solo si no es último
                                            !esPrimeroFinal, // RefI solo si no es primero
                                            valorSiguienteFin, valorAnteriorFin);
                    
                    // Flecha entre nodos consecutivos
                    if (!esUltimoFinal) {
                        g2d.setColor(java.awt.Color.BLACK);
                        dibujarFlecha(g2d, nodoX + tamanoNodo + 10, nodoY + 15, 
                                    nodoX + espacioNodo - 10, nodoY + 15);
                    }
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.GREEN);
                        g2d.drawString("cabeza", nodoX - 5, nodoY - 10);
                    }
                    
                    // Enlaces
                    if (i < nodosFinales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                    if (i > 0) {
                        g2d.setColor(java.awt.Color.BLUE);
                        dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                    }
                }
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡INSERCIÓN AL FINAL COMPLETADA!", x, y + 100);
                break;
        }
    }

    /**
     * Dibuja los sub-pasos específicos del método insertarIzquierda
     */
    private void dibujarSubPasosInsertarIzquierda(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int inicioX, int inicioY, int tamanoNodo, int ancho, int alto) {
        // Limpiar área
        g2d.setColor(java.awt.Color.WHITE);
        g2d.fillRect(0, 0, ancho, alto);
        
        // Título
        g2d.setColor(java.awt.Color.BLUE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 14));
        g2d.drawString("insertarIzquierda(" + op.valor + ") - Sub-paso " + (subPaso + 1) + "/5", inicioX, inicioY - 10);
        
        String caso = determinarCasoInsertarIzquierda(op.estadoAntes, op.estadoDespues, op.valor);
        
        if (caso.contains("CASO 1")) {
            dibujarSubPasosCaso1Izquierda(g2d, op, subPaso, inicioX, inicioY, tamanoNodo);
        } else if (caso.contains("CASO 2")) {
            dibujarSubPasosCaso2Izquierda(g2d, op, subPaso, inicioX, inicioY, tamanoNodo);
        } else {
            dibujarSubPasosCaso3Izquierda(g2d, op, subPaso, inicioX, inicioY, tamanoNodo);
        }
    }

    /**
     * Sub-pasos para Caso 1: Lista vacía (insertarIzquierda)
     */
    private void dibujarSubPasosCaso1Izquierda(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo) {
        int espacioNodo = tamanoNodo + 30;
        
        switch (subPaso) {
            case 0: // Estado inicial vacío
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("CASO 1: Lista vacía - pLD = null", x, y + 20);
                g2d.drawString("Estado inicial: " + op.estadoAntes, x, y + 40);
                
                g2d.setColor(java.awt.Color.RED);
                g2d.drawString("cabeza = null, cola = null, pLD = null", x, y + 80);
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("Crear nuevo nodo nAux = new clsNodoDoble(" + op.valor + ")", x, y + 20);
                
                int nodoX = x + espacioNodo;
                int nodoY = y + 60;
                dibujarNodoNuevo(g2d, nodoX, nodoY, tamanoNodo, op.valor, "nAux");
                break;
                
            case 2: // Asignar pLD
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("pLD = nAux; (primera asignación)", x, y + 20);
                
                int pldX = x + espacioNodo;
                int pldY = y + 60;
                dibujarNodoConReferencias(g2d, pldX, pldY, tamanoNodo, 
                                        op.valor, java.awt.Color.ORANGE, "pLD",
                                        false, // RefD → null
                                        false, // RefI → null
                                        null, null);
                break;
                
            case 3: // Asignar cabeza y cola
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("cabeza = cola = nAux; (inicializar lista)", x, y + 20);
                
                int inicioX = x + espacioNodo;
                int inicioY = y + 60;
                dibujarNodoConReferencias(g2d, inicioX, inicioY, tamanoNodo, 
                                        op.valor, java.awt.Color.GREEN, "cabeza/cola/pLD",
                                        false, // RefD → null (único nodo)
                                        false, // RefI → null (único nodo)
                                        null, null);
                break;
                
            case 4: // Resultado final
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("CASO 1 COMPLETADO", x, y + 20);
                g2d.drawString("Estado final: " + op.estadoDespues, x, y + 40);
                
                int finalX = x + espacioNodo;
                int finalY = y + 60;
                g2d.setColor(java.awt.Color.GREEN);
                g2d.fillRect(finalX, finalY, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(finalX, finalY, tamanoNodo, tamanoNodo);
                g2d.drawString(op.valor, finalX + 15, finalY + 25);
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡LISTA INICIALIZADA!", x, finalY + 80);
                break;
        }
    }

    /**
     * Sub-pasos para Caso 2: Insertar al inicio (pLD en cabeza)
     */
    private void dibujarSubPasosCaso2Izquierda(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo) {
        // Parsear datos reales
        String[] nodosReales = parsearEstadoLista(op.estadoAntes);
        int espacioNodo = tamanoNodo + 30;
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("CASO 2: pLD en la cabeza (RefI == null)", x, y + 20);
                
                // Dibujar lista inicial con pLD en cabeza
                for (int i = 0; i < nodosReales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 60;
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.ORANGE); // pLD en cabeza
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.GREEN);
                        g2d.drawString("cabeza", nodoX - 5, nodoY - 10);
                        g2d.setColor(java.awt.Color.ORANGE);
                        g2d.drawString("pLD", nodoX + 35, nodoY + 15);
                    }
                    
                    // Enlaces RefD
                    if (i < nodosReales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                }
                break;
                
            case 1: // Crear nuevo nodo
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("Crear nAux = new clsNodoDoble(" + op.valor + ")", x, y + 20);
                
                // Nuevo nodo a la izquierda de la cabeza
                int nuevoX4 = x - espacioNodo;
                int nuevoY4 = y + 60;
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nuevoX4, nuevoY4, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(nuevoX4, nuevoY4, tamanoNodo, tamanoNodo);
                g2d.drawString(op.valor, nuevoX4 + 15, nuevoY4 + 25);
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("nAux", nuevoX4 + 5, nuevoY4 - 10);
                
                // Cabeza actual (pLD)
                int cabezaX = x;
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(cabezaX, nuevoY4, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(cabezaX, nuevoY4, tamanoNodo, tamanoNodo);
                g2d.drawString(nodosReales[0], cabezaX + 15, nuevoY4 + 25);
                g2d.drawString("pLD", cabezaX + 35, nuevoY4 + 15);
                break;
                
            case 2: // nAux.setRefD(pLD)
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("nAux.setRefD(pLD);", x, y + 20);
                
                int nuevoX5 = x - espacioNodo;
                int nuevoY5 = y + 60;
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nuevoX5, nuevoY5, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(nuevoX5, nuevoY5, tamanoNodo, tamanoNodo);
                g2d.drawString(op.valor, nuevoX5 + 15, nuevoY5 + 25);
                
                int cabezaX2 = x;
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(cabezaX2, nuevoY5, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(cabezaX2, nuevoY5, tamanoNodo, tamanoNodo);
                g2d.drawString(nodosReales[0], cabezaX2 + 15, nuevoY5 + 25);
                
                // Flecha RefD
                g2d.setColor(java.awt.Color.RED);
                dibujarFlecha(g2d, nuevoX5 + tamanoNodo, nuevoY5 + 12, cabezaX2, nuevoY5 + 12);
                g2d.drawString("RefD", nuevoX5 + tamanoNodo + 5, nuevoY5 + 8);
                break;
                
            case 3: // pLD.setRefI(nAux)
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("pLD.setRefI(nAux);", x, y + 20);
                
                int nuevoX6 = x - espacioNodo;
                int nuevoY6 = y + 60;
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nuevoX6, nuevoY6, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(nuevoX6, nuevoY6, tamanoNodo, tamanoNodo);
                g2d.drawString(op.valor, nuevoX6 + 15, nuevoY6 + 25);
                
                int cabezaX3 = x;
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.fillRect(cabezaX3, nuevoY6, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(cabezaX3, nuevoY6, tamanoNodo, tamanoNodo);
                g2d.drawString(nodosReales[0], cabezaX3 + 15, nuevoY6 + 25);
                
                // Flechas bidireccionales
                g2d.setColor(java.awt.Color.RED);
                dibujarFlecha(g2d, nuevoX6 + tamanoNodo, nuevoY6 + 12, cabezaX3, nuevoY6 + 12);
                g2d.setColor(java.awt.Color.BLUE);
                dibujarFlecha(g2d, cabezaX3, nuevoY6 + 28, nuevoX6 + tamanoNodo, nuevoY6 + 28);
                g2d.drawString("RefI", cabezaX3 - 20, nuevoY6 + 35);
                break;
                
            case 4: // cabeza = nAux
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("cabeza = nAux; (actualizar cabeza)", x, y + 20);
                
                // Dibujar estado final con datos reales
                String[] nodosFinales = parsearEstadoLista(op.estadoDespues);
                for (int i = 0; i < nodosFinales.length; i++) {
                    int nodoX = x - espacioNodo + (i * espacioNodo);
                    int nodoY = y + 60;
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.YELLOW); // Nuevo nodo
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosFinales[i], nodoX + 15, nodoY + 25);
                    
                    if (i == 0) {
                        g2d.setColor(java.awt.Color.GREEN);
                        g2d.drawString("NUEVA cabeza", nodoX - 15, nodoY - 10);
                    }
                    
                    // Enlaces
                    if (i < nodosFinales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                    if (i > 0) {
                        g2d.setColor(java.awt.Color.BLUE);
                        dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                    }
                }
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡INSERCIÓN AL INICIO COMPLETADA!", x, y + 120);
                break;
        }
    }

    /**
     * Sub-pasos para Caso 3: Insertar en el medio (pLD tiene nodos a ambos lados)
     */
    private void dibujarSubPasosCaso3Izquierda(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo) {
        // Parsear datos reales
        String[] nodosReales = parsearEstadoLista(op.estadoAntes);
        int punteroPos = obtenerPosicionPunteroReal(op.estadoAntes);
        int espacioNodo = tamanoNodo + 30;
        String valorInsertado = op.valor;
        
        switch (subPaso) {
            case 0: // Estado inicial
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("CASO 3: pLD en el medio (tiene nodos anterior y siguiente)", x, y + 20);
                
                // Dibujar lista inicial
                for (int i = 0; i < nodosReales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 60;
                    
                    if (i == punteroPos) {
                        g2d.setColor(java.awt.Color.ORANGE); // pLD
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                    
                    if (i == punteroPos) {
                        g2d.setColor(java.awt.Color.ORANGE);
                        g2d.drawString("pLD", nodoX + 35, nodoY + 15);
                    }
                    
                    // Enlaces RefD
                    if (i < nodosReales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                    if (i > 0) {
                        g2d.setColor(java.awt.Color.BLUE);
                        dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                    }
                }
                break;
                
            case 1: // Crear nuevo nodo y guardar p1Anterior
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("Crear nAux y guardar p1Anterior = pLD.getRefI()", x, y + 20);
                
                // Nuevo nodo (amarillo)
                int nuevoX7 = x + (punteroPos * espacioNodo) - espacioNodo;
                int nuevoY7 = y + 40;
                g2d.setColor(java.awt.Color.YELLOW);
                g2d.fillRect(nuevoX7, nuevoY7, tamanoNodo, tamanoNodo);
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawRect(nuevoX7, nuevoY7, tamanoNodo, tamanoNodo);
                g2d.drawString(valorInsertado, nuevoX7 + 15, nuevoY7 + 25);
                g2d.setColor(java.awt.Color.ORANGE);
                g2d.drawString("nAux", nuevoX7 + 5, nuevoY7 - 10);
                
                // Lista existente con pLD y p1Anterior marcados
                for (int i = 0; i < nodosReales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 80;
                    
                    if (i == punteroPos) {
                        g2d.setColor(java.awt.Color.ORANGE); // pLD
                    } else if (i == punteroPos - 1) {
                        g2d.setColor(java.awt.Color.CYAN); // p1Anterior
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[i], nodoX + 15, nodoY + 25);
                    
                    if (i == punteroPos) {
                        g2d.setColor(java.awt.Color.ORANGE);
                        g2d.drawString("pLD", nodoX + 10, nodoY - 10);
                    } else if (i == punteroPos - 1) {
                        g2d.setColor(java.awt.Color.CYAN);
                        g2d.drawString("p1Anterior", nodoX - 15, nodoY - 10);
                    }
                }
                break;
                
            case 2: // p1Anterior.setRefD(nAux)
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("p1Anterior.setRefD(nAux);", x, y + 20);
                
                // Mostrar enlace p1Anterior → nAux
                if (punteroPos > 0) {
                    int anteriorX = x + ((punteroPos - 1) * espacioNodo);
                    int anteriorY = y + 60;
                    
                    // p1Anterior
                    g2d.setColor(java.awt.Color.CYAN);
                    g2d.fillRect(anteriorX, anteriorY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(anteriorX, anteriorY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[punteroPos - 1], anteriorX + 15, anteriorY + 25);
                    g2d.setColor(java.awt.Color.CYAN);
                    g2d.drawString("p1Anterior", anteriorX - 15, anteriorY - 10);
                    
                    // Nuevo nodo
                    int nuevoX8 = anteriorX + espacioNodo;
                    g2d.setColor(java.awt.Color.YELLOW);
                    g2d.fillRect(nuevoX8, anteriorY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nuevoX8, anteriorY, tamanoNodo, tamanoNodo);
                    g2d.drawString(valorInsertado, nuevoX8 + 15, anteriorY + 25);
                    g2d.setColor(java.awt.Color.ORANGE);
                    g2d.drawString("nAux", nuevoX8 + 5, anteriorY - 10);
                    
                    // Flecha RefD de anterior a nuevo
                    g2d.setColor(java.awt.Color.RED);
                    dibujarFlecha(g2d, anteriorX + tamanoNodo, anteriorY + 12, nuevoX8, anteriorY + 12);
                    g2d.drawString("RefD", anteriorX + tamanoNodo + 5, anteriorY + 8);
                }
                break;
                
            case 3: // nAux.setRefI(p1Anterior) y nAux.setRefD(pLD)
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("nAux.setRefI(p1Anterior) y nAux.setRefD(pLD)", x, y + 20);
                
                if (punteroPos > 0) {
                    int anteriorX2 = x + ((punteroPos - 1) * espacioNodo);
                    int anteriorY2 = y + 60;
                    
                    // p1Anterior
                    g2d.setColor(java.awt.Color.CYAN);
                    g2d.fillRect(anteriorX2, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(anteriorX2, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[punteroPos - 1], anteriorX2 + 15, anteriorY2 + 25);
                    
                    // nAux
                    int nuevoX9 = anteriorX2 + espacioNodo;
                    g2d.setColor(java.awt.Color.YELLOW);
                    g2d.fillRect(nuevoX9, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nuevoX9, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(valorInsertado, nuevoX9 + 15, anteriorY2 + 25);
                    
                    // pLD
                    int pldX = nuevoX9 + espacioNodo;
                    g2d.setColor(java.awt.Color.ORANGE);
                    g2d.fillRect(pldX, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(pldX, anteriorY2, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosReales[punteroPos], pldX + 15, anteriorY2 + 25);
                    
                    // Enlaces bidireccionales
                    g2d.setColor(java.awt.Color.RED);
                    dibujarFlecha(g2d, anteriorX2 + tamanoNodo, anteriorY2 + 10, nuevoX9, anteriorY2 + 10);
                    dibujarFlecha(g2d, nuevoX9 + tamanoNodo, anteriorY2 + 10, pldX, anteriorY2 + 10);
                    
                    g2d.setColor(java.awt.Color.BLUE);
                    dibujarFlecha(g2d, nuevoX9, anteriorY2 + 30, anteriorX2 + tamanoNodo, anteriorY2 + 30);
                    g2d.drawString("RefI", nuevoX9 - 15, anteriorY2 + 35);
                }
                break;
                
            case 4: // pLD.setRefI(nAux)
                g2d.setColor(java.awt.Color.BLACK);
                g2d.drawString("pLD.setRefI(nAux); - ¡INSERCIÓN COMPLETADA!", x, y + 20);
                
                // Dibujar estado final con datos reales
                String[] nodosFinales = parsearEstadoLista(op.estadoDespues);
                for (int i = 0; i < nodosFinales.length; i++) {
                    int nodoX = x + (i * espacioNodo);
                    int nodoY = y + 60;
                    
                    if (nodosFinales[i].equals(valorInsertado) && i == punteroPos) {
                        g2d.setColor(java.awt.Color.YELLOW); // Nuevo nodo insertado
                    } else {
                        g2d.setColor(java.awt.Color.LIGHT_GRAY);
                    }
                    g2d.fillRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.setColor(java.awt.Color.BLACK);
                    g2d.drawRect(nodoX, nodoY, tamanoNodo, tamanoNodo);
                    g2d.drawString(nodosFinales[i], nodoX + 15, nodoY + 25);
                    
                    // Enlaces
                    if (i < nodosFinales.length - 1) {
                        g2d.setColor(java.awt.Color.RED);
                        dibujarFlecha(g2d, nodoX + tamanoNodo, nodoY + 12, nodoX + espacioNodo, nodoY + 12);
                    }
                    if (i > 0) {
                        g2d.setColor(java.awt.Color.BLUE);
                        dibujarFlecha(g2d, nodoX, nodoY + 28, nodoX - espacioNodo + tamanoNodo, nodoY + 28);
                    }
                }
                
                g2d.setColor(java.awt.Color.GREEN);
                g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
                g2d.drawString("¡INTEGRIDAD BIDIRECCIONAL COMPLETA!", x, y + 110);
                break;
        }
    }

    /**
     * Dibuja un nodo con sus referencias internas RefD y RefI visibles
     */
    private void dibujarNodoConReferencias(java.awt.Graphics2D g2d, int x, int y, int tamanoNodo, 
                                         String valor, java.awt.Color colorFondo, String etiqueta,
                                         boolean tieneRefD, boolean tieneRefI, 
                                         String valorRefD, String valorRefI) {
        // Dibujar el nodo principal
        g2d.setColor(colorFondo);
        g2d.fillRect(x, y, tamanoNodo, tamanoNodo);
        g2d.setColor(java.awt.Color.BLACK);
        g2d.drawRect(x, y, tamanoNodo, tamanoNodo);
        
        // Valor del nodo en el centro
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 12));
        g2d.drawString(valor, x + tamanoNodo/3, y + tamanoNodo/2 + 4);
        
        // Etiqueta del nodo (arriba)
        if (etiqueta != null && !etiqueta.isEmpty()) {
            g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 10));
            g2d.setColor(java.awt.Color.BLUE);
            g2d.drawString(etiqueta, x - 5, y - 5);
            g2d.setColor(java.awt.Color.BLACK);
        }
        
        // Mostrar RefD (Referencia Derecha) - lado derecho del nodo
        int refDX = x + tamanoNodo + 5;
        int refDY = y + 5;
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.PLAIN, 9));
        
        if (tieneRefD) {
            g2d.setColor(java.awt.Color.RED);
            g2d.drawString("RefD:", refDX, refDY);
            g2d.drawString("→" + (valorRefD != null ? valorRefD : "?"), refDX, refDY + 12);
        } else {
            g2d.setColor(java.awt.Color.GRAY);
            g2d.drawString("RefD:", refDX, refDY);
            g2d.drawString("→null", refDX, refDY + 12);
        }
        
        // Mostrar RefI (Referencia Izquierda) - lado izquierdo del nodo
        int refIX = x - 35;
        int refIY = y + 5;
        
        if (tieneRefI) {
            g2d.setColor(java.awt.Color.BLUE);
            g2d.drawString("RefI:", refIX, refIY);
            g2d.drawString("←" + (valorRefI != null ? valorRefI : "?"), refIX, refIY + 12);
        } else {
            g2d.setColor(java.awt.Color.GRAY);
            g2d.drawString("RefI:", refIX, refIY);
            g2d.drawString("←null", refIX, refIY + 12);
        }
        
        g2d.setColor(java.awt.Color.BLACK); // Restaurar color
    }

    /**
     * Dibuja un nodo recién creado mostrando sus referencias iniciales en null
     */
    private void dibujarNodoNuevo(java.awt.Graphics2D g2d, int x, int y, int tamanoNodo, String valor, String etiqueta) {
        dibujarNodoConReferencias(g2d, x, y, tamanoNodo, valor, java.awt.Color.YELLOW, 
                                etiqueta, false, false, null, null);
        
        // Agregar indicador de "NUEVO"
        g2d.setColor(java.awt.Color.ORANGE);
        g2d.setFont(new java.awt.Font("Arial", java.awt.Font.BOLD, 8));
        g2d.drawString("NUEVO", x + 5, y + tamanoNodo + 15);
        g2d.drawString("RefD=null", x + 5, y + tamanoNodo + 25);
        g2d.drawString("RefI=null", x + 5, y + tamanoNodo + 35);
    }

    /**
     * Visualización genérica para otras operaciones
     */
    private void dibujarVisualizacionGenerica(java.awt.Graphics2D g2d, OperacionAnalisis op, int subPaso, int x, int y, int tamanoNodo, int ancho, int alto) {
        g2d.setColor(java.awt.Color.BLACK);
        g2d.drawString("Operación: " + op.operacion + " - Paso " + (subPaso + 1), x, y);
    }
    private String obtenerCodigoNegocio(OperacionAnalisis op) {
        StringBuilder codigo = new StringBuilder();
        
        switch (op.operacion) {
            case "INSERTAR_INICIO":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: insertarInicio(int dato)\n");
                    codigo.append("clsNodo n = new clsNodo(").append(op.valor).append(");\n");
                    codigo.append("n.setRef(cabeza);\n");
                    codigo.append("cabeza = n;\n");
                    codigo.append("// TDA: punteroActual = cabeza;");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: insertarInicio(int dato)\n");
                    codigo.append("clsNodoDoble n = new clsNodoDoble(").append(op.valor).append(");\n");
                    codigo.append("n.setNext(cabeza);\n");
                    codigo.append("if(cabeza != null) cabeza.setPrev(n);\n");
                    codigo.append("cabeza = n;\n");
                    codigo.append("if(cola == null) cola = n;\n");
                    codigo.append("// TDA: punteroActual = cabeza;");
                }
                break;
                
            case "INSERTAR_FINAL":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: insertarFinal(int dato)\n");
                    codigo.append("clsNodo n = new clsNodo(").append(op.valor).append(");\n");
                    codigo.append("if(cabeza == null) {\n");
                    codigo.append("    cabeza = n;  // Primer nodo\n");
                    codigo.append("    punteroActual = cabeza;  // TDA: Inicializar puntero\n");
                    codigo.append("    return;\n");
                    codigo.append("}\n");
                    codigo.append("// Recorrer hasta el final\n");
                    codigo.append("clsNodo aux = cabeza;\n");
                    codigo.append("while(aux.getRef() != null) {\n");
                    codigo.append("    aux = aux.getRef();\n");
                    codigo.append("}\n");
                    codigo.append("aux.setRef(n);  // Enlazar al final");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: insertarFinal(int dato)\n");
                    codigo.append("clsNodoDoble n = new clsNodoDoble(").append(op.valor).append(");\n");
                    codigo.append("if(cabeza == null) {\n");
                    codigo.append("    cabeza = n;  // Primer nodo\n");
                    codigo.append("    cola = n;\n");
                    codigo.append("    punteroActual = cabeza;  // TDA: Inicializar puntero\n");
                    codigo.append("    return;\n");
                    codigo.append("}\n");
                    codigo.append("// Agregar al final\n");
                    codigo.append("n.setPrev(cola);\n");
                    codigo.append("cola.setNext(n);\n");
                    codigo.append("cola = n;  // Actualizar cola");
                }
                break;
                
            case "ELIMINAR_INICIO":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: eliminarInicio()\n");
                    codigo.append("if(cabeza == null) return -1;\n");
                    codigo.append("int val = cabeza.getDato();\n");
                    codigo.append("cabeza = cabeza.getRef();\n");
                    codigo.append("// TDA: punteroActual = cabeza;\n");
                    codigo.append("return val;");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: eliminarInicio()\n");
                    codigo.append("if(cabeza == null) return -1;\n");
                    codigo.append("int val = cabeza.getDato();\n");
                    codigo.append("cabeza = cabeza.getNext();\n");
                    codigo.append("if(cabeza == null) cola = null;\n");
                    codigo.append("else cabeza.setPrev(null);\n");
                    codigo.append("// TDA: punteroActual = cabeza;\n");
                    codigo.append("return val;");
                }
                break;
                
            case "INSERTAR_DERECHA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// MÉTODO EDUCATIVO: insertarDerecha(int dato)\n");
                    codigo.append("// Análisis por casos según posición del puntero pLD\n\n");
                    codigo.append("clsNodoDoble nAux = new clsNodoDoble(").append(op.valor).append(");\n\n");
                    codigo.append("if (pLD == null) {\n");
                    codigo.append("    // CASO 1: Lista vacía o puntero no posicionado\n");
                    codigo.append("    pLD = nAux;\n");
                    codigo.append("    if (cabeza == null) {\n");
                    codigo.append("        cabeza = cola = nAux;\n");
                    codigo.append("    }\n");
                    codigo.append("} else if (pLD.getRefD() == null) {\n");
                    codigo.append("    // CASO 2: Puntero en último nodo (RefD = null)\n");
                    codigo.append("    pLD.setRefD(nAux);      // RefD del puntero → nuevo\n");
                    codigo.append("    nAux.setRefI(pLD);      // RefI del nuevo → puntero\n");
                    codigo.append("    cola = nAux;            // Actualizar cola\n");
                    codigo.append("} else {\n");
                    codigo.append("    // CASO 3: Puntero en medio (tiene RefD)\n");
                    codigo.append("    clsNodoDoble p1Siguiente = pLD.getRefD();\n");
                    codigo.append("    \n");
                    codigo.append("    pLD.setRefD(nAux);          // RefD del puntero → nuevo\n");
                    codigo.append("    nAux.setRefI(pLD);          // RefI del nuevo → puntero\n");
                    codigo.append("    \n");
                    codigo.append("    nAux.setRefD(p1Siguiente);  // RefD del nuevo → siguiente\n");
                    codigo.append("    p1Siguiente.setRefI(nAux);  // RefI del siguiente → nuevo\n");
                    codigo.append("}\n\n");
                    codigo.append("// RefD = Referencia Derecha, RefI = Referencia Izquierda");
                } else if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: insertarDerecha(int pos, int valor)\n");
                    codigo.append("clsNodo aux = cabeza;\n");
                    codigo.append("for(int i = 0; i < pos && aux != null; i++)\n");
                    codigo.append("    aux = aux.getRef();\n");
                    codigo.append("if(aux == null) return false;\n");
                    codigo.append("clsNodo nuevo = new clsNodo(").append(op.valor).append(");\n");
                    codigo.append("nuevo.setRef(aux.getRef());\n");
                    codigo.append("aux.setRef(nuevo);\n");
                    codigo.append("return true;");
                }
                break;
                
            case "MOVER_PUNTERO_INICIO":
                codigo.append("// Método: moverPunteroInicio()\n");
                codigo.append("punteroActual = cabeza;\n");
                codigo.append("// TDA: Posiciona en el primer nodo");
                break;
                
            case "MOVER_PUNTERO_SIGUIENTE":
                codigo.append("// Método: moverPunteroSiguiente()\n");
                codigo.append("if(punteroActual != null && \n");
                codigo.append("   punteroActual.getRef() != null) {\n");
                codigo.append("    punteroActual = punteroActual.getRef();\n");
                codigo.append("    return true;\n");
                codigo.append("}\n");
                codigo.append("return false;");
                break;
                
            case "MOVER_PUNTERO_ANTERIOR":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: moverPunteroAnterior()\n");
                    codigo.append("if(punteroActual != null && \n");
                    codigo.append("   punteroActual.getPrev() != null) {\n");
                    codigo.append("    punteroActual = punteroActual.getPrev();\n");
                    codigo.append("    return true;\n");
                    codigo.append("}\n");
                    codigo.append("return false;");
                } else {
                    codigo.append("// Lista Simple no soporta navegación anterior\n");
                    codigo.append("// TDA: Solo enlaces unidireccionales");
                }
                break;
                
            case "POP":
                codigo.append("// Método: eliminar() - Pila\n");
                codigo.append("if(cima != null) {\n");
                codigo.append("    int dato = cima.getDato();\n");
                codigo.append("    cima = cima.getRef();\n");
                codigo.append("    // TDA: punteroActual = cima;\n");
                codigo.append("    return dato;\n");
                codigo.append("}\n");
                codigo.append("return -1;");
                break;
                
            case "PUSH":
                codigo.append("// Método: insert(int dato) - Pila\n");
                codigo.append("clsNodo n = new clsNodo(").append(op.valor).append(");\n");
                codigo.append("if(cima == null) {\n");
                codigo.append("    cima = n;  // Primera inserción\n");
                codigo.append("    punteroActual = cima;  // TDA: Inicializar puntero\n");
                codigo.append("} else {\n");
                codigo.append("    n.setRef(cima);  // Enlazar al tope\n");
                codigo.append("    cima = n;        // Nuevo tope\n");
                codigo.append("    punteroActual = cima;  // TDA: Actualizar puntero\n");
                codigo.append("}");
                break;
                
            case "DEQUEUE":
                codigo.append("// Método: eliminar() - Cola\n");
                codigo.append("if(primero != null) {\n");
                codigo.append("    int dato = primero.getDato();\n");
                codigo.append("    primero = primero.getRef();\n");
                codigo.append("    if(primero == null) ultimo = null;\n");
                codigo.append("    // TDA: punteroActual = primero;\n");
                codigo.append("    return dato;\n");
                codigo.append("}\n");
                codigo.append("return -1;");
                break;
                
            case "ENQUEUE":
                codigo.append("// Método: insertar(int dato) - Cola\n");
                codigo.append("clsNodo n = new clsNodo(").append(op.valor).append(");\n");
                codigo.append("if(primero == null) {\n");
                codigo.append("    primero = n;  // Primera inserción\n");
                codigo.append("    ultimo = n;\n");
                codigo.append("    punteroActual = primero;  // TDA: Inicializar puntero\n");
                codigo.append("} else {\n");
                codigo.append("    ultimo.setRef(n);  // Enlazar al final\n");
                codigo.append("    ultimo = n;        // Nuevo último\n");
                codigo.append("    // punteroActual mantiene su posición\n");
                codigo.append("}");
                break;
                
            case "INSERTAR_IZQUIERDA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: insertarIzquierda(int pos, int valor)\n");
                    codigo.append("if (pos <= 0 || cabeza == null) return false;\n\n");
                    codigo.append("// Navegar hasta la posición\n");
                    codigo.append("clsNodoDoble aux = cabeza;\n");
                    codigo.append("for (int i = 0; i < pos && aux != null; i++) {\n");
                    codigo.append("    aux = aux.getNext();\n");
                    codigo.append("}\n");
                    codigo.append("if (aux == null || aux.getPrev() == null) return false;\n\n");
                    codigo.append("// Crear nuevo nodo\n");
                    codigo.append("clsNodoDoble nuevo = new clsNodoDoble(").append(op.valor).append(");\n");
                    codigo.append("clsNodoDoble anterior = aux.getPrev();\n\n");
                    codigo.append("// Establecer enlaces\n");
                    codigo.append("nuevo.setNext(aux);\n");
                    codigo.append("nuevo.setPrev(anterior);\n");
                    codigo.append("anterior.setNext(nuevo);\n");
                    codigo.append("aux.setPrev(nuevo);\n");
                    codigo.append("// TDA: punteroActual = aux (mantiene posición)");
                }
                break;
                
            case "ELIMINAR_IZQUIERDA":
                if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: eliminarIzquierda(int pos)\n");
                    codigo.append("if (pos <= 0 || cabeza == null) return -1;\n\n");
                    codigo.append("// Navegar hasta la posición\n");
                    codigo.append("clsNodoDoble aux = cabeza;\n");
                    codigo.append("for (int i = 0; i < pos && aux != null; i++) {\n");
                    codigo.append("    aux = aux.getNext();\n");
                    codigo.append("}\n");
                    codigo.append("if (aux == null || aux.getPrev() == null) return -1;\n\n");
                    codigo.append("// Eliminar el nodo anterior\n");
                    codigo.append("clsNodoDoble aEliminar = aux.getPrev();\n");
                    codigo.append("int valor = aEliminar.getDato();\n");
                    codigo.append("clsNodoDoble anterior = aEliminar.getPrev();\n\n");
                    codigo.append("// Actualizar enlaces\n");
                    codigo.append("aux.setPrev(anterior);\n");
                    codigo.append("if (anterior != null) {\n");
                    codigo.append("    anterior.setNext(aux);\n");
                    codigo.append("} else {\n");
                    codigo.append("    cabeza = aux; // Actualizar cabeza\n");
                    codigo.append("}\n");
                    codigo.append("// TDA: punteroActual = aux\n");
                    codigo.append("return valor;");
                }
                break;
                
            case "ELIMINAR_DERECHA":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: eliminarDerecha(int pos)\n");
                    codigo.append("if (pos < 0 || cabeza == null) return -1;\n\n");
                    codigo.append("// Navegar hasta la posición\n");
                    codigo.append("clsNodo aux = cabeza;\n");
                    codigo.append("for (int i = 0; i < pos && aux != null; i++) {\n");
                    codigo.append("    aux = aux.getRef();\n");
                    codigo.append("}\n");
                    codigo.append("if (aux == null || aux.getRef() == null) return -1;\n\n");
                    codigo.append("// Eliminar el nodo siguiente\n");
                    codigo.append("clsNodo aEliminar = aux.getRef();\n");
                    codigo.append("int valor = aEliminar.getDato();\n");
                    codigo.append("aux.setRef(aEliminar.getRef());\n");
                    codigo.append("// TDA: punteroActual = aux.getRef()\n");
                    codigo.append("return valor;");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: eliminarDerecha(int pos)\n");
                    codigo.append("if (pos < 0 || cabeza == null) return -1;\n\n");
                    codigo.append("// Navegar hasta la posición\n");
                    codigo.append("clsNodoDoble aux = cabeza;\n");
                    codigo.append("for (int i = 0; i < pos && aux != null; i++) {\n");
                    codigo.append("    aux = aux.getNext();\n");
                    codigo.append("}\n");
                    codigo.append("if (aux == null || aux.getNext() == null) return -1;\n\n");
                    codigo.append("// Eliminar el nodo siguiente\n");
                    codigo.append("clsNodoDoble aEliminar = aux.getNext();\n");
                    codigo.append("int valor = aEliminar.getDato();\n");
                    codigo.append("clsNodoDoble siguiente = aEliminar.getNext();\n\n");
                    codigo.append("// Actualizar enlaces\n");
                    codigo.append("aux.setNext(siguiente);\n");
                    codigo.append("if (siguiente != null) {\n");
                    codigo.append("    siguiente.setPrev(aux);\n");
                    codigo.append("} else {\n");
                    codigo.append("    cola = aux; // Actualizar cola\n");
                    codigo.append("}\n");
                    codigo.append("return valor;");
                }
                break;
                
            case "VACIAR":
                if (op.estructura.equals("PILA")) {
                    codigo.append("// Método: vaciarPila()\n");
                    codigo.append("cima = null;\n");
                    codigo.append("punteroActual = null;\n");
                    codigo.append("// TDA: Libera todas las referencias");
                } else if (op.estructura.equals("COLA")) {
                    codigo.append("// Método: vaciarCola()\n");
                    codigo.append("primero = null;\n");
                    codigo.append("ultimo = null;\n");
                    codigo.append("punteroActual = null;\n");
                    codigo.append("// TDA: Libera todas las referencias");
                } else if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: vaciar()\n");
                    codigo.append("cabeza = null;\n");
                    codigo.append("punteroActual = null;\n");
                    codigo.append("// TDA: Libera todas las referencias");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: vaciar()\n");
                    codigo.append("cabeza = null;\n");
                    codigo.append("cola = null;\n");
                    codigo.append("punteroActual = null;\n");
                    codigo.append("// TDA: Libera todas las referencias");
                }
                break;
                
            case "ORDENAR_ASCENDENTE":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: ordenarAscendente()\n");
                    codigo.append("if (cabeza == null || cabeza.getRef() == null) return;\n\n");
                    codigo.append("// Algoritmo burbuja para listas enlazadas\n");
                    codigo.append("boolean intercambio;\n");
                    codigo.append("do {\n");
                    codigo.append("    intercambio = false;\n");
                    codigo.append("    clsNodo actual = cabeza;\n");
                    codigo.append("    while (actual.getRef() != null) {\n");
                    codigo.append("        if (actual.getDato() > actual.getRef().getDato()) {\n");
                    codigo.append("            // Intercambiar valores\n");
                    codigo.append("            int temp = actual.getDato();\n");
                    codigo.append("            actual.setDato(actual.getRef().getDato());\n");
                    codigo.append("            actual.getRef().setDato(temp);\n");
                    codigo.append("            intercambio = true;\n");
                    codigo.append("        }\n");
                    codigo.append("        actual = actual.getRef();\n");
                    codigo.append("    }\n");
                    codigo.append("} while (intercambio);\n");
                    codigo.append("// TDA: punteroActual mantiene su referencia");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: ordenarAscendente()\n");
                    codigo.append("if (cabeza == null || cabeza.getNext() == null) return;\n\n");
                    codigo.append("// Algoritmo burbuja para listas doblemente enlazadas\n");
                    codigo.append("boolean intercambio;\n");
                    codigo.append("do {\n");
                    codigo.append("    intercambio = false;\n");
                    codigo.append("    clsNodoDoble actual = cabeza;\n");
                    codigo.append("    while (actual.getNext() != null) {\n");
                    codigo.append("        if (actual.getDato() > actual.getNext().getDato()) {\n");
                    codigo.append("            // Intercambiar valores\n");
                    codigo.append("            int temp = actual.getDato();\n");
                    codigo.append("            actual.setDato(actual.getNext().getDato());\n");
                    codigo.append("            actual.getNext().setDato(temp);\n");
                    codigo.append("            intercambio = true;\n");
                    codigo.append("        }\n");
                    codigo.append("        actual = actual.getNext();\n");
                    codigo.append("    }\n");
                    codigo.append("} while (intercambio);\n");
                    codigo.append("// TDA: punteroActual mantiene su referencia");
                }
                break;
                
            case "SUMAR_ELEMENTOS":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: sumarElementos()\n");
                    codigo.append("int suma = 0;\n");
                    codigo.append("clsNodo actual = cabeza;\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    suma += actual.getDato();\n");
                    codigo.append("    actual = actual.getRef();\n");
                    codigo.append("}\n");
                    codigo.append("return suma;\n");
                    codigo.append("// TDA: Operación de solo lectura");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: sumarElementos()\n");
                    codigo.append("int suma = 0;\n");
                    codigo.append("clsNodoDoble actual = cabeza;\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    suma += actual.getDato();\n");
                    codigo.append("    actual = actual.getNext();\n");
                    codigo.append("}\n");
                    codigo.append("return suma;\n");
                    codigo.append("// TDA: Operación de solo lectura");
                }
                break;
                
            case "BUSCAR":
                if (op.estructura.equals("PILA")) {
                    codigo.append("// Método: buscar(int valor)\n");
                    codigo.append("clsNodo actual = cima;\n");
                    codigo.append("int posicion = 0;\n");
                    codigo.append("ArrayList<Integer> posiciones = new ArrayList<>();\n\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    if (actual.getDato() == ").append(op.valor).append(") {\n");
                    codigo.append("        posiciones.add(posicion);\n");
                    codigo.append("    }\n");
                    codigo.append("    actual = actual.getRef();\n");
                    codigo.append("    posicion++;\n");
                    codigo.append("}\n");
                    codigo.append("// TDA: Operación de solo lectura, no afecta punteros");
                } else if (op.estructura.equals("COLA")) {
                    codigo.append("// Método: buscar(int valor)\n");
                    codigo.append("clsNodo actual = primero;\n");
                    codigo.append("int posicion = 0;\n");
                    codigo.append("ArrayList<Integer> posiciones = new ArrayList<>();\n\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    if (actual.getDato() == ").append(op.valor).append(") {\n");
                    codigo.append("        posiciones.add(posicion);\n");
                    codigo.append("    }\n");
                    codigo.append("    actual = actual.getRef();\n");
                    codigo.append("    posicion++;\n");
                    codigo.append("}\n");
                    codigo.append("// TDA: Operación de solo lectura, no afecta punteros");
                } else if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: buscarTodasLasPosiciones(int valor)\n");
                    codigo.append("ArrayList<Integer> posiciones = new ArrayList<>();\n");
                    codigo.append("clsNodo actual = cabeza;\n");
                    codigo.append("int indice = 0;\n\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    if (actual.getDato() == ").append(op.valor).append(") {\n");
                    codigo.append("        posiciones.add(indice);\n");
                    codigo.append("    }\n");
                    codigo.append("    actual = actual.getRef();\n");
                    codigo.append("    indice++;\n");
                    codigo.append("}\n");
                    codigo.append("// Convertir a array y retornar\n");
                    codigo.append("return posiciones.toArray(new int[0]);\n");
                    codigo.append("// TDA: Operación de solo lectura");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: buscarTodasLasPosiciones(int valor)\n");
                    codigo.append("ArrayList<Integer> posiciones = new ArrayList<>();\n");
                    codigo.append("clsNodoDoble actual = cabeza;\n");
                    codigo.append("int indice = 0;\n\n");
                    codigo.append("while (actual != null) {\n");
                    codigo.append("    if (actual.getDato() == ").append(op.valor).append(") {\n");
                    codigo.append("        posiciones.add(indice);\n");
                    codigo.append("    }\n");
                    codigo.append("    actual = actual.getNext();\n");
                    codigo.append("    indice++;\n");
                    codigo.append("}\n");
                    codigo.append("// Convertir a array y retornar\n");
                    codigo.append("return posiciones.toArray(new int[0]);\n");
                    codigo.append("// TDA: Operación de solo lectura");
                }
                break;
                
            case "INSERTAR_ORDENADO":
                if (op.estructura.equals("LISTA_SIMPLE")) {
                    codigo.append("// Método: insertarOrdenado(int valor)\n");
                    codigo.append("clsNodo nuevo = new clsNodo(").append(op.valor).append(");\n\n");
                    codigo.append("// Si lista vacía o valor menor al primero\n");
                    codigo.append("if (cabeza == null || ").append(op.valor).append(" <= cabeza.getDato()) {\n");
                    codigo.append("    nuevo.setRef(cabeza);\n");
                    codigo.append("    cabeza = nuevo;\n");
                    codigo.append("    punteroActual = cabeza;\n");
                    codigo.append("    return;\n");
                    codigo.append("}\n\n");
                    codigo.append("// Buscar posición correcta\n");
                    codigo.append("clsNodo actual = cabeza;\n");
                    codigo.append("while (actual.getRef() != null && \n");
                    codigo.append("       actual.getRef().getDato() < ").append(op.valor).append(") {\n");
                    codigo.append("    actual = actual.getRef();\n");
                    codigo.append("}\n\n");
                    codigo.append("// Insertar en posición\n");
                    codigo.append("nuevo.setRef(actual.getRef());\n");
                    codigo.append("actual.setRef(nuevo);\n");
                    codigo.append("// TDA: punteroActual = nuevo");
                } else if (op.estructura.equals("LISTA_DOBLE")) {
                    codigo.append("// Método: insertarOrdenado(int valor)\n");
                    codigo.append("clsNodoDoble nuevo = new clsNodoDoble(").append(op.valor).append(");\n\n");
                    codigo.append("// Si lista vacía\n");
                    codigo.append("if (cabeza == null) {\n");
                    codigo.append("    cabeza = cola = nuevo;\n");
                    codigo.append("    punteroActual = cabeza;\n");
                    codigo.append("    return;\n");
                    codigo.append("}\n\n");
                    codigo.append("// Si valor menor al primero\n");
                    codigo.append("if (").append(op.valor).append(" <= cabeza.getDato()) {\n");
                    codigo.append("    nuevo.setNext(cabeza);\n");
                    codigo.append("    cabeza.setPrev(nuevo);\n");
                    codigo.append("    cabeza = nuevo;\n");
                    codigo.append("    punteroActual = cabeza;\n");
                    codigo.append("    return;\n");
                    codigo.append("}\n\n");
                    codigo.append("// Buscar posición correcta\n");
                    codigo.append("clsNodoDoble actual = cabeza;\n");
                    codigo.append("while (actual.getNext() != null && \n");
                    codigo.append("       actual.getNext().getDato() < ").append(op.valor).append(") {\n");
                    codigo.append("    actual = actual.getNext();\n");
                    codigo.append("}\n\n");
                    codigo.append("// Insertar después de actual\n");
                    codigo.append("nuevo.setNext(actual.getNext());\n");
                    codigo.append("nuevo.setPrev(actual);\n");
                    codigo.append("if (actual.getNext() != null) {\n");
                    codigo.append("    actual.getNext().setPrev(nuevo);\n");
                    codigo.append("} else {\n");
                    codigo.append("    cola = nuevo;\n");
                    codigo.append("}\n");
                    codigo.append("actual.setNext(nuevo);\n");
                    codigo.append("// TDA: punteroActual = nuevo");
                }
                break;
                
            case "PERMUTAR_NODOS":
                codigo.append("// === PERMUTACIÓN DE NODOS EN LISTA DOBLE ===\n");
                codigo.append("// Intercambiar posiciones de nodos\n\n");
                
                codigo.append("public boolean permutarNodos(int pos1, int pos2) {\n");
                codigo.append("    // Validaciones iniciales\n");
                codigo.append("    if (pos1 == pos2) return true; // Misma posición\n");
                codigo.append("    \n");
                codigo.append("    // Obtener los nodos en las posiciones especificadas\n");
                codigo.append("    clsNodoDoble nodo1 = obtenerNodoEnPosicion(pos1);\n");
                codigo.append("    clsNodoDoble nodo2 = obtenerNodoEnPosicion(pos2);\n");
                codigo.append("    \n");
                codigo.append("    if (nodo1 == null || nodo2 == null) {\n");
                codigo.append("        return false; // Posiciones inválidas\n");
                codigo.append("    }\n");
                codigo.append("    \n");
                codigo.append("    // INTERCAMBIO DE DATOS (método simple)\n");
                codigo.append("    Object temp = nodo1.getDato();\n");
                codigo.append("    nodo1.setDato(nodo2.getDato());\n");
                codigo.append("    nodo2.setDato(temp);\n");
                codigo.append("    \n");
                codigo.append("    return true;\n");
                codigo.append("}\n");
                codigo.append("\n");
                codigo.append("// Método auxiliar para obtener nodo por posición\n");
                codigo.append("public clsNodoDoble obtenerNodoEnPosicion(int pos) {\n");
                codigo.append("    if (pos < 0 || punteroLista == null) return null;\n");
                codigo.append("    \n");
                codigo.append("    clsNodoDoble actual = punteroLista;\n");
                codigo.append("    for (int i = 0; i < pos && actual != null; i++) {\n");
                codigo.append("        actual = actual.getNext();\n");
                codigo.append("    }\n");
                codigo.append("    return actual;\n");
                codigo.append("}\n");
                codigo.append("\n");
                codigo.append("// TDA: Los punteros estructurales se mantienen\n");
                codigo.append("// Solo se intercambian los datos, preservando la integridad\n");
                break;
                
            default:
                // Casos adicionales para operaciones no específicamente definidas
                if (op.operacion.contains("INSERTAR") && !op.valor.isEmpty()) {
                    codigo.append("// Operación de Inserción: ").append(op.operacion).append("\n");
                    codigo.append("// Valor insertado: ").append(op.valor).append("\n");
                    codigo.append("// Estructura: ").append(op.estructura).append("\n\n");
                    codigo.append("// Lógica general de inserción:\n");
                    codigo.append("// 1. Crear nuevo nodo con el valor\n");
                    codigo.append("// 2. Establecer enlaces apropiados\n");
                    codigo.append("// 3. Actualizar referencias de cabeza/cola\n");
                    codigo.append("// 4. Posicionar puntero según reglas TDA");
                } else if (op.operacion.contains("ELIMINAR")) {
                    codigo.append("// Operación de Eliminación: ").append(op.operacion).append("\n");
                    if (!op.valor.isEmpty()) {
                        codigo.append("// Valor eliminado: ").append(op.valor).append("\n");
                    }
                    codigo.append("// Estructura: ").append(op.estructura).append("\n\n");
                    codigo.append("// Lógica general de eliminación:\n");
                    codigo.append("// 1. Localizar el nodo a eliminar\n");
                    codigo.append("// 2. Actualizar enlaces de nodos vecinos\n");
                    codigo.append("// 3. Liberar memoria del nodo eliminado\n");
                    codigo.append("// 4. Reposicionar puntero si es necesario");
                } else if (op.operacion.contains("MOVER")) {
                    codigo.append("// Operación de Navegación: ").append(op.operacion).append("\n");
                    codigo.append("// Estructura: ").append(op.estructura).append("\n\n");
                    codigo.append("// Lógica de navegación del puntero:\n");
                    codigo.append("// Actualizar punteroActual según la dirección\n");
                    codigo.append("// Verificar límites de la estructura\n");
                    codigo.append("// Mantener consistencia TDA");
                } else {
                    codigo.append("// Operación: ").append(op.operacion).append("\n");
                    codigo.append("// Estructura: ").append(op.estructura).append("\n");
                    if (!op.valor.isEmpty()) {
                        codigo.append("// Valor: ").append(op.valor).append("\n");
                    }
                    codigo.append("\n// Consulte las clases de negocio:\n");
                    codigo.append("// - clsPila.java, clsCola.java\n");
                    codigo.append("// - clsListaSimple.java, clsListaDoble.java\n");
                    codigo.append("// para ver la implementación específica");
                }
        }
        
        return codigo.toString();
    }

    
    // Métodos auxiliares para obtener estado de las estructuras
    private String obtenerEstadoPila() {
        if (objPila.estaVacia()) {
            return "VACÍA";
        }
        StringBuilder sb = new StringBuilder();
        clsNodo temp = objPila.getCima();
        sb.append("PILA [");
        while (temp != null) {
            sb.append(temp.getDato());
            temp = temp.getRef();
            if (temp != null) sb.append(", ");
        }
        sb.append("]");
        return sb.toString();
    }
    
    private String obtenerEstadoCola() {
        if (objCola.estaVacia()) {
            return "VACÍA";
        }
        StringBuilder sb = new StringBuilder();
        clsNodo temp = objCola.getPrimero();
        sb.append("COLA [");
        while (temp != null) {
            sb.append(temp.getDato());
            temp = temp.getRef();
            if (temp != null) sb.append(", ");
        }
        sb.append("]");
        return sb.toString();
    }
    
    private String obtenerEstadoListaSimple() {
        if (objListaSimple.estaVacia()) {
            return "VACÍA - Puntero: null";
        }
        StringBuilder sb = new StringBuilder();
        clsNodo temp = objListaSimple.getCabeza();
        clsNodo puntero = objListaSimple.getPunteroActual();
        sb.append("LISTA_SIMPLE [");
        int posicion = 0;
        int posicionPuntero = -1;
        while (temp != null) {
            if (temp == puntero) {
                posicionPuntero = posicion;
                sb.append("*").append(temp.getDato()).append("*"); // Marcar nodo donde está el puntero
            } else {
                sb.append(temp.getDato());
            }
            temp = temp.getRef();
            if (temp != null) sb.append(", ");
            posicion++;
        }
        sb.append("] - Puntero: ");
        if (puntero != null) {
            sb.append("pos ").append(posicionPuntero).append(" (valor ").append(puntero.getDato()).append(")");
        } else {
            sb.append("null");
        }
        return sb.toString();
    }
    
    private String obtenerEstadoListaDoble() {
        if (objListaDoble.estaVacia()) {
            return "VACÍA - Puntero: null";
        }
        StringBuilder sb = new StringBuilder();
        clsNodoDoble temp = objListaDoble.getCabeza();
        clsNodoDoble puntero = objListaDoble.getPunteroActual();
        sb.append("LISTA_DOBLE [");
        int posicion = 0;
        int posicionPuntero = -1;
        while (temp != null) {
            if (temp == puntero) {
                posicionPuntero = posicion;
                sb.append("*").append(temp.getDato()).append("*"); // Marcar nodo donde está el puntero
            } else {
                sb.append(temp.getDato());
            }
            temp = temp.getNext();
            if (temp != null) sb.append(", ");
            posicion++;
        }
        sb.append("] - Puntero: ");
        if (puntero != null) {
            sb.append("pos ").append(posicionPuntero).append(" (valor ").append(puntero.getDato()).append(")");
        } else {
            sb.append("null");
        }
        return sb.toString();
    }

}
