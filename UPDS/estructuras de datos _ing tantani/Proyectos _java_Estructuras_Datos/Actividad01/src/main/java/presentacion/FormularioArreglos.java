package presentacion;

import negocio.NegocioArreglos;
import javax.swing.*;
import javax.swing.table.DefaultTableModel;
import java.awt.*;
import java.awt.event.ActionEvent;

public class FormularioArreglos extends JFrame {
    private NegocioArreglos negocio;
    private JTable tablaVector, tablaMatriz;
    private DefaultTableModel modelVector, modelMatriz;
    private JTextField txtTamVector, txtFilas, txtColumnas;
    private JTextField txtMin, txtMax;
    private JTextArea areaResultadosVectores, areaResultadosMatrices;
    private JTabbedPane tabbedPane;
    
    public FormularioArreglos() {
        negocio = new NegocioArreglos();
        initComponents();
        setTitle("Gestión de Vectores y Matrices - Actividad01");
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setSize(1000, 750);
        setLocationRelativeTo(null);
    }
    
    private void initComponents() {
        // Panel principal con pestañas
        tabbedPane = new JTabbedPane();
        
        // Pestaña para Vectores (con su propia área de resultados)
        tabbedPane.addTab("Vectores", crearPanelVector());
        
        // Pestaña para Matrices (con su propia área de resultados)
        tabbedPane.addTab("Matrices", crearPanelMatriz());
        
        add(tabbedPane, BorderLayout.CENTER);
    }
    
    private JPanel crearPanelVector() {
        JPanel panel = new JPanel(new BorderLayout());
        
        // Panel de controles
        JPanel panelControles = new JPanel(new GridLayout(2, 1));
        
        // Fila 1: Dimensiones
        JPanel panelDimensiones = new JPanel(new FlowLayout());
        panelDimensiones.add(new JLabel("Tamaño Vector:"));
        txtTamVector = new JTextField(5);
        panelDimensiones.add(txtTamVector);
        
        JButton btnCrearVector = new JButton("Crear Vector");
        btnCrearVector.addActionListener(this::crearVector);
        panelDimensiones.add(btnCrearVector);
        
        // Fila 2: Rango aleatorio
        JPanel panelRango = new JPanel(new FlowLayout());
        panelRango.add(new JLabel("Mín:"));
        txtMin = new JTextField(3);
        txtMin.setText("1");
        panelRango.add(txtMin);
        panelRango.add(new JLabel("Máx:"));
        txtMax = new JTextField(3);
        txtMax.setText("100");
        panelRango.add(txtMax);
        
        JButton btnLlenarAleatorio = new JButton("Llenar Aleatorio");
        btnLlenarAleatorio.addActionListener(this::llenarVectorAleatorio);
        panelRango.add(btnLlenarAleatorio);
        
        panelControles.add(panelDimensiones);
        panelControles.add(panelRango);
        
        // Tabla para mostrar el vector
        modelVector = new DefaultTableModel(new String[]{"Índice", "Valor"}, 0);
        tablaVector = new JTable(modelVector);
        JScrollPane scrollVector = new JScrollPane(tablaVector);
        
        // Panel de operaciones
        JPanel panelOperaciones = new JPanel(new FlowLayout());
        JButton btnSumar = new JButton("Sumar Elementos");
        btnSumar.addActionListener(this::sumarVector);
        panelOperaciones.add(btnSumar);
        
        JButton btnContarPrimos = new JButton("Contar Primos");
        btnContarPrimos.addActionListener(this::contarPrimosVector);
        panelOperaciones.add(btnContarPrimos);
        
        JButton btnEditar = new JButton("Editar Valor");
        btnEditar.addActionListener(this::editarValorVector);
        panelOperaciones.add(btnEditar);
        
        JButton btnEliminar = new JButton("Eliminar Elemento");
        btnEliminar.addActionListener(this::eliminarElementoVector);
        panelOperaciones.add(btnEliminar);
        
        // Área de resultados ESPECÍFICA para Vectores
        areaResultadosVectores = new JTextArea(5, 50);
        areaResultadosVectores.setEditable(false);
        areaResultadosVectores.setBackground(new Color(240, 240, 240));
        areaResultadosVectores.setText(">>> Resultados de operaciones con VECTORES:\n");
        JScrollPane scrollResultadosVectores = new JScrollPane(areaResultadosVectores);
        
        // Organizar componentes en un panel principal para Vectores
        JPanel panelCentral = new JPanel(new BorderLayout());
        panelCentral.add(scrollVector, BorderLayout.CENTER);
        panelCentral.add(panelOperaciones, BorderLayout.SOUTH);
        
        panel.add(panelControles, BorderLayout.NORTH);
        panel.add(panelCentral, BorderLayout.CENTER);
        panel.add(scrollResultadosVectores, BorderLayout.SOUTH);
        
        return panel;
    }
    
    private JPanel crearPanelMatriz() {
        JPanel panel = new JPanel(new BorderLayout());
        
        // Panel de controles
        JPanel panelControles = new JPanel(new GridLayout(2, 1));
        
        // Fila 1: Dimensiones
        JPanel panelDimensiones = new JPanel(new FlowLayout());
        panelDimensiones.add(new JLabel("Filas:"));
        txtFilas = new JTextField(3);
        panelDimensiones.add(txtFilas);
        panelDimensiones.add(new JLabel("Columnas:"));
        txtColumnas = new JTextField(3);
        panelDimensiones.add(txtColumnas);
        
        JButton btnCrearMatriz = new JButton("Crear Matriz");
        btnCrearMatriz.addActionListener(this::crearMatriz);
        panelDimensiones.add(btnCrearMatriz);
        
        // Fila 2: Operaciones
        JPanel panelOperaciones = new JPanel(new FlowLayout());
        JButton btnLlenarMatriz = new JButton("Llenar Aleatorio");
        btnLlenarMatriz.addActionListener(this::llenarMatrizAleatoria);
        panelOperaciones.add(btnLlenarMatriz);
        
        JButton btnSumarMatriz = new JButton("Sumar Matriz");
        btnSumarMatriz.addActionListener(this::sumarMatriz);
        panelOperaciones.add(btnSumarMatriz);
        
        JButton btnPrimosMatriz = new JButton("Primos Matriz");
        btnPrimosMatriz.addActionListener(this::contarPrimosMatriz);
        panelOperaciones.add(btnPrimosMatriz);
        
        JButton btnEditarMatriz = new JButton("Editar Celda");
        btnEditarMatriz.addActionListener(this::editarValorMatriz);
        panelOperaciones.add(btnEditarMatriz);
        
        JButton btnEliminarFila = new JButton("Eliminar Fila");
        btnEliminarFila.addActionListener(this::eliminarFilaMatriz);
        panelOperaciones.add(btnEliminarFila);
        
        JButton btnEliminarColumna = new JButton("Eliminar Columna");
        btnEliminarColumna.addActionListener(this::eliminarColumnaMatriz);
        panelOperaciones.add(btnEliminarColumna);
        
        panelControles.add(panelDimensiones);
        panelControles.add(panelOperaciones);
        
        // Tabla para matriz
        modelMatriz = new DefaultTableModel();
        tablaMatriz = new JTable(modelMatriz);
        JScrollPane scrollMatriz = new JScrollPane(tablaMatriz);
        
        // Área de resultados ESPECÍFICA para Matrices
        areaResultadosMatrices = new JTextArea(5, 50);
        areaResultadosMatrices.setEditable(false);
        areaResultadosMatrices.setBackground(new Color(240, 240, 240));
        areaResultadosMatrices.setText(">>> Resultados de operaciones con MATRICES:\n");
        JScrollPane scrollResultadosMatrices = new JScrollPane(areaResultadosMatrices);
        
        // Organizar componentes en un panel principal para Matrices
        JPanel panelCentral = new JPanel(new BorderLayout());
        panelCentral.add(scrollMatriz, BorderLayout.CENTER);
        
        panel.add(panelControles, BorderLayout.NORTH);
        panel.add(panelCentral, BorderLayout.CENTER);
        panel.add(scrollResultadosMatrices, BorderLayout.SOUTH);
        
        return panel;
    }
    
    // ========== MÉTODOS PARA VECTORES ==========
    
    private void crearVector(ActionEvent e) {
        try {
            int tamaño = Integer.parseInt(txtTamVector.getText());
            if (tamaño <= 0) {
                mostrarError("El tamaño debe ser mayor a 0");
                return;
            }
            negocio.crearVector(tamaño);
            actualizarTablaVector();
            mostrarResultadoVectores("Vector creado con tamaño: " + tamaño);
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese un tamaño válido");
        }
    }
    
    private void llenarVectorAleatorio(ActionEvent e) {
        try {
            int min = Integer.parseInt(txtMin.getText());
            int max = Integer.parseInt(txtMax.getText());
            if (min >= max) {
                mostrarError("El valor mínimo debe ser menor al máximo");
                return;
            }
            negocio.llenarVectorAleatorio(min, max);
            actualizarTablaVector();
            mostrarResultadoVectores("Vector llenado aleatoriamente entre " + min + " y " + max);
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese valores mín y máx válidos");
        }
    }
    
    private void sumarVector(ActionEvent e) {
        int suma = negocio.sumarVector();
        mostrarResultadoVectores("Suma del vector: " + suma);
    }
    
    private void contarPrimosVector(ActionEvent e) {
        int cantidad = negocio.contarPrimosVector();
        String valoresPrimos = negocio.obtenerPrimosVector();
        
        if (cantidad > 0) {
            mostrarResultadoVectores("Números primos en el vector: " + cantidad + " → Valores: [" + valoresPrimos + "]");
        } else {
            mostrarResultadoVectores("Números primos en el vector: " + cantidad + " → " + valoresPrimos);
        }
    }
    
    private void editarValorVector(ActionEvent e) {
        try {
            int filaSeleccionada = tablaVector.getSelectedRow();
            if (filaSeleccionada == -1) {
                mostrarError("Seleccione una fila de la tabla para editar");
                return;
            }
            
            String nuevoValorStr = JOptionPane.showInputDialog(this, 
                "Nuevo valor para la posición " + filaSeleccionada + ":",
                "Editar Valor", JOptionPane.QUESTION_MESSAGE);
            
            if (nuevoValorStr != null && !nuevoValorStr.trim().isEmpty()) {
                int nuevoValor = Integer.parseInt(nuevoValorStr);
                negocio.reemplazarElementoVector(filaSeleccionada, nuevoValor);
                actualizarTablaVector();
                mostrarResultadoVectores("Valor en posición " + filaSeleccionada + " cambiado a: " + nuevoValor);
            }
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese un número válido");
        }
    }
    
    private void eliminarElementoVector(ActionEvent e) {
        try {
            int filaSeleccionada = tablaVector.getSelectedRow();
            if (filaSeleccionada == -1) {
                mostrarError("Seleccione una fila de la tabla para eliminar");
                return;
            }
            
            int confirmacion = JOptionPane.showConfirmDialog(this,
                "¿Está seguro de eliminar el elemento en la posición " + filaSeleccionada + "?",
                "Confirmar Eliminación", JOptionPane.YES_NO_OPTION);
            
            if (confirmacion == JOptionPane.YES_OPTION) {
                negocio.eliminarElementoVector(filaSeleccionada);
                actualizarTablaVector();
                mostrarResultadoVectores("Elemento en posición " + filaSeleccionada + " eliminado");
            }
        } catch (Exception ex) {
            mostrarError("Error al eliminar elemento: " + ex.getMessage());
        }
    }
    
    private void actualizarTablaVector() {
        modelVector.setRowCount(0);
        int[] vector = negocio.getVector();
        if (vector != null) {
            for (int i = 0; i < vector.length; i++) {
                modelVector.addRow(new Object[]{i, vector[i]});
            }
        }
    }
    
    // ========== MÉTODOS PARA MATRICES ==========
    
    private void crearMatriz(ActionEvent e) {
        try {
            int filas = Integer.parseInt(txtFilas.getText());
            int columnas = Integer.parseInt(txtColumnas.getText());
            if (filas <= 0 || columnas <= 0) {
                mostrarError("Filas y columnas deben ser mayores a 0");
                return;
            }
            negocio.crearMatriz(filas, columnas);
            actualizarTablaMatriz();
            mostrarResultadoMatrices("Matriz creada: " + filas + "x" + columnas);
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese filas y columnas válidas");
        }
    }
    
    private void llenarMatrizAleatoria(ActionEvent e) {
        try {
            int min = Integer.parseInt(txtMin.getText());
            int max = Integer.parseInt(txtMax.getText());
            if (min >= max) {
                mostrarError("El valor mínimo debe ser menor al máximo");
                return;
            }
            negocio.llenarMatrizAleatoria(min, max);
            actualizarTablaMatriz();
            mostrarResultadoMatrices("Matriz llenada aleatoriamente entre " + min + " y " + max);
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese valores mín y máx válidos");
        }
    }
    
    private void sumarMatriz(ActionEvent e) {
        int suma = negocio.sumarMatriz();
        mostrarResultadoMatrices("Suma de la matriz: " + suma);
    }
    
    private void contarPrimosMatriz(ActionEvent e) {
        int cantidad = negocio.contarPrimosMatriz();
        String valoresPrimos = negocio.obtenerPrimosMatriz();
        
        if (cantidad > 0) {
            mostrarResultadoMatrices("Números primos en la matriz: " + cantidad + " → Valores: [" + valoresPrimos + "]");
        } else {
            mostrarResultadoMatrices("Números primos en la matriz: " + cantidad + " → " + valoresPrimos);
        }
    }
    
    private void editarValorMatriz(ActionEvent e) {
        try {
            int filaSeleccionada = tablaMatriz.getSelectedRow();
            int columnaSeleccionada = tablaMatriz.getSelectedColumn();
            
            if (filaSeleccionada == -1 || columnaSeleccionada == -1) {
                mostrarError("Seleccione una celda de la tabla para editar");
                return;
            }
            
            String nuevoValorStr = JOptionPane.showInputDialog(this,
                "Nuevo valor para la posición [" + filaSeleccionada + "," + columnaSeleccionada + "]:",
                "Editar Valor Matriz", JOptionPane.QUESTION_MESSAGE);
            
            if (nuevoValorStr != null && !nuevoValorStr.trim().isEmpty()) {
                int nuevoValor = Integer.parseInt(nuevoValorStr);
                negocio.reemplazarElementoMatriz(filaSeleccionada, columnaSeleccionada, nuevoValor);
                actualizarTablaMatriz();
                mostrarResultadoMatrices("Valor en [" + filaSeleccionada + "," + columnaSeleccionada + "] cambiado a: " + nuevoValor);
            }
        } catch (NumberFormatException ex) {
            mostrarError("Ingrese un número válido");
        }
    }
    
    private void eliminarFilaMatriz(ActionEvent e) {
        try {
            int filaSeleccionada = tablaMatriz.getSelectedRow();
            if (filaSeleccionada == -1) {
                mostrarError("Seleccione una fila para eliminar");
                return;
            }
            
            int confirmacion = JOptionPane.showConfirmDialog(this,
                "¿Está seguro de eliminar la fila " + filaSeleccionada + "?\n" +
                "Tamaño actual: " + negocio.getFilasMatriz() + "x" + negocio.getColumnasMatriz() + 
                " → Nuevo tamaño: " + (negocio.getFilasMatriz() - 1) + "x" + negocio.getColumnasMatriz(),
                "Confirmar Eliminación de Fila", JOptionPane.YES_NO_OPTION);
            
            if (confirmacion == JOptionPane.YES_OPTION) {
                negocio.eliminarFilaMatriz(filaSeleccionada);
                actualizarTablaMatriz();
                mostrarResultadoMatrices("Fila " + filaSeleccionada + " eliminada. Nueva matriz: " + 
                               negocio.getFilasMatriz() + "x" + negocio.getColumnasMatriz());
            }
        } catch (Exception ex) {
            mostrarError("Error al eliminar fila: " + ex.getMessage());
        }
    }
    
    private void eliminarColumnaMatriz(ActionEvent e) {
        try {
            int columnaSeleccionada = tablaMatriz.getSelectedColumn();
            if (columnaSeleccionada == -1) {
                mostrarError("Seleccione una columna para eliminar");
                return;
            }
            
            int confirmacion = JOptionPane.showConfirmDialog(this,
                "¿Está seguro de eliminar la columna " + columnaSeleccionada + "?\n" +
                "Tamaño actual: " + negocio.getFilasMatriz() + "x" + negocio.getColumnasMatriz() + 
                " → Nuevo tamaño: " + negocio.getFilasMatriz() + "x" + (negocio.getColumnasMatriz() - 1),
                "Confirmar Eliminación de Columna", JOptionPane.YES_NO_OPTION);
            
            if (confirmacion == JOptionPane.YES_OPTION) {
                negocio.eliminarColumnaMatriz(columnaSeleccionada);
                actualizarTablaMatriz();
                mostrarResultadoMatrices("Columna " + columnaSeleccionada + " eliminada. Nueva matriz: " + 
                               negocio.getFilasMatriz() + "x" + negocio.getColumnasMatriz());
            }
        } catch (Exception ex) {
            mostrarError("Error al eliminar columna: " + ex.getMessage());
        }
    }
    
    private void actualizarTablaMatriz() {
        modelMatriz.setRowCount(0);
        modelMatriz.setColumnCount(0);
        
        int[][] matriz = negocio.getMatriz();
        if (matriz != null && matriz.length > 0) {
            // Crear columnas
            for (int j = 0; j < matriz[0].length; j++) {
                modelMatriz.addColumn("Col " + j);
            }
            
            // Agregar filas
            for (int i = 0; i < matriz.length; i++) {
                Object[] fila = new Object[matriz[i].length];
                for (int j = 0; j < matriz[i].length; j++) {
                    fila[j] = matriz[i][j];
                }
                modelMatriz.addRow(fila);
            }
        }
    }
    
    // ========== MÉTODOS AUXILIARES MEJORADOS ==========
    
    private void mostrarResultadoVectores(String mensaje) {
        areaResultadosVectores.append(">>> " + mensaje + "\n");
        areaResultadosVectores.setCaretPosition(areaResultadosVectores.getDocument().getLength());
    }
    
    private void mostrarResultadoMatrices(String mensaje) {
        areaResultadosMatrices.append(">>> " + mensaje + "\n");
        areaResultadosMatrices.setCaretPosition(areaResultadosMatrices.getDocument().getLength());
    }
    
    private void mostrarError(String mensaje) {
        JOptionPane.showMessageDialog(this, mensaje, "Error", JOptionPane.ERROR_MESSAGE);
    }
}