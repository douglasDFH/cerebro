/*
 * Click nbfs://nbhost/SystemFileSystem/Templates/Licenses/license-default.txt to change this license
 * Click nbfs://nbhost/SystemFileSystem/Templates/Classes/Class.java to edit this template
 */
package negocio;

/**
 *
 * @author dell
 */
public class clsArbolBinario {
    private nodoAB raiz;
    public clsArbolBinario(){
        this.raiz = null;       // estado iicial del arbol vacio
    }
    public void insertarAB(int dato){   // realizamos la llamada 
       nodoAB nA = new nodoAB(dato);    // creamos el nuevo nodo
       insertarAB(raiz, nA);          // llamamos al metodo privado recursivo
       
    }
    private void insertarAB(nodoAB raizA,nodoAB nAux){ // metodo recursivo para insertar
        if(raiz == null){              // si el arbol esta vacio
            raiz = nAux;              // el nuevo nodo sera la raiz
        }else{             // caso contrario si el arbol no esta vacio
            if((int)(nAux.getDato())<= (int) (raizA.getDato())){ // y si el dato es menor o igual a la raiz
                if(raizA.getHijoI()== null){                       // si el hijo izquierdo es nulo
                    raizA.setHijoI(nAux);                               // se inserta el nuevo nodo
                }else{                                        // caso contrario
                    insertarAB(raizA.getHijoI(),nAux);            // se llama recursivamente al metodo con el hijo izquierdo
                }
            }else{                                       // caso contrario si el dato es mayor a la raiz
                if(raizA.getHijoD()== null){             // si el hijo derecho es nulo
                    raizA.setHijoD(nAux);
                }else{                  // caso contrario
                    insertarAB(raizA.getHijoD(),nAux);    // se llama recursivamente al metodo con el hijo derecho
                }
            }
        }
    }

    //metodo publico para eliminar un nodo del arbol binario
    public boolean eliminarAB(int dato){
        if(raiz == null){
            return false; //arbol vacio
        }
        raiz = eliminarAB(raiz, dato);
        return true;
    }

    //metodo privado recursivo para eliminar
    private nodoAB eliminarAB(nodoAB raizA, int dato){
        if(raizA == null){
            return null; //no se encontro el dato
        }

        //buscar el nodo a eliminar
        if(dato < (int)raizA.getDato()){
            raizA.setHijoI(eliminarAB(raizA.getHijoI(), dato));
        }else if(dato > (int)raizA.getDato()){
            raizA.setHijoD(eliminarAB(raizA.getHijoD(), dato));
        }else{
            //caso 1: nodo hoja (sin hijos)
            if(raizA.getHijoI() == null && raizA.getHijoD() == null){
                return null;
            }
            //caso 2: nodo con un solo hijo
            else if(raizA.getHijoI() == null){
                return raizA.getHijoD(); //retorna el hijo derecho
            }else if(raizA.getHijoD() == null){
                return raizA.getHijoI(); //retorna el hijo izquierdo
            }
            //caso 3: nodo con dos hijos
            else{
                //encontrar el sucesor in-orden (menor del subarbol derecho)
                nodoAB sucesor = obtenerMinimo(raizA.getHijoD());
                raizA.setDato(sucesor.getDato());
                raizA.setHijoD(eliminarAB(raizA.getHijoD(), (int)sucesor.getDato()));
            }
        }
        return raizA;
    }

    //metodo auxiliar para obtener el nodo minimo de un subarbol
    private nodoAB obtenerMinimo(nodoAB nodo){
        while(nodo.getHijoI() != null){
            nodo = nodo.getHijoI();
        }
        return nodo;
    }
    
    //recorrido en orden (izquierda-raiz-derecha)
    //resultado: datos ordenados de menor a mayor
    public void EnOrden (){
        EnOrden(raiz);
    }

    private void EnOrden (nodoAB rAux){
        if(rAux != null){
            if(rAux.getHijoI()!= null){
                EnOrden(rAux.getHijoI());
            }
            System.out.println("Dato: "+ rAux.getDato());
            if(rAux.getHijoD()!= null){
              EnOrden(rAux.getHijoD());
            }
        }
    }

    //recorrido pre orden (raiz-izquierda-derecha)
    //resultado: util para copiar el arbol
    public void PreOrden(){
        PreOrden(raiz);
    }

    private void PreOrden(nodoAB rAux){
        if(rAux != null){
            System.out.println("Dato: "+ rAux.getDato());
            if(rAux.getHijoI()!= null){
                PreOrden(rAux.getHijoI());
            }
            if(rAux.getHijoD()!= null){
                PreOrden(rAux.getHijoD());
            }
        }
    }

    //recorrido post orden (izquierda-derecha-raiz)
    //resultado: util para eliminar el arbol
    public void PostOrden(){
        PostOrden(raiz);
    }

    private void PostOrden(nodoAB rAux){
        if(rAux != null){
            if(rAux.getHijoI()!= null){
                PostOrden(rAux.getHijoI());
            }
            if(rAux.getHijoD()!= null){
                PostOrden(rAux.getHijoD());
            }
            System.out.println("Dato: "+ rAux.getDato());
        }
    }

    // Métodos para obtener la raíz
    public nodoAB getRaiz() {
        return raiz;
    }

    // Métodos para obtener recorridos como String
    public String getEnOrdenString() {
        StringBuilder sb = new StringBuilder();
        getEnOrdenString(raiz, sb);
        return sb.toString().trim();
    }

    private void getEnOrdenString(nodoAB rAux, StringBuilder sb) {
        if(rAux != null){
            if(rAux.getHijoI()!= null){
                getEnOrdenString(rAux.getHijoI(), sb);
            }
            sb.append(rAux.getDato()).append(" ");
            if(rAux.getHijoD()!= null){
                getEnOrdenString(rAux.getHijoD(), sb);
            }
        }
    }

    public String getPreOrdenString() {
        StringBuilder sb = new StringBuilder();
        getPreOrdenString(raiz, sb);
        return sb.toString().trim();
    }

    private void getPreOrdenString(nodoAB rAux, StringBuilder sb) {
        if(rAux != null){
            sb.append(rAux.getDato()).append(" ");
            if(rAux.getHijoI()!= null){
                getPreOrdenString(rAux.getHijoI(), sb);
            }
            if(rAux.getHijoD()!= null){
                getPreOrdenString(rAux.getHijoD(), sb);
            }
        }
    }

    public String getPostOrdenString() {
        StringBuilder sb = new StringBuilder();
        getPostOrdenString(raiz, sb);
        return sb.toString().trim();
    }

    private void getPostOrdenString(nodoAB rAux, StringBuilder sb) {
        if(rAux != null){
            if(rAux.getHijoI()!= null){
                getPostOrdenString(rAux.getHijoI(), sb);
            }
            if(rAux.getHijoD()!= null){
                getPostOrdenString(rAux.getHijoD(), sb);
            }
            sb.append(rAux.getDato()).append(" ");
        }
    }

    // Método para verificar si el árbol está vacío
    public boolean estaVacio() {
        return raiz == null;
    }

    // Método para limpiar el árbol
    public void limpiar() {
        raiz = null;
    }

   
    public int eliminarHojasAscendentes() {
        if (raiz == null) {
            return 0; // Árbol vacío
        }
        int[] contador = {0}; // Contador de hojas eliminadas
        raiz = eliminarHojasAscendentes(raiz, contador);
        return contador[0];
    }

    // Método de recursividadpara eliminar hojas ascendentes
    private nodoAB eliminarHojasAscendentes(nodoAB nodo, int[] contador) {
        if (nodo == null) {
            return null;
        }

        // empezamosla  recursividad a  los hijos
        nodo.setHijoI(eliminarHojasAscendentes(nodo.getHijoI(), contador));
        nodo.setHijoD(eliminarHojasAscendentes(nodo.getHijoD(), contador));

        // Verificamos si es una hoja (no tiene hijos)
        boolean esHoja = (nodo.getHijoI() == null && nodo.getHijoD() == null);

        if (esHoja) {
            // Es una hoja, verificar si su valor es ascendente
            if (esValorAscendente((int) nodo.getDato())) {
                contador[0]++; // Incrementar contador
                return null; // Eliminar esta hoja
            }
        }

        return nodo; // Mantener el nodo
    }

    //para cuando el nodo tiene un solo digito 
    private boolean esValorAscendente(int valor) {
        String numStr = String.valueOf(Math.abs(valor)); // Convertir a valor absoluto

        // Si tiene un solo dígito, se considera ascendente
        if (numStr.length() <= 1) {
            return true;
        }

        // Verificar que cada dígito sea menor que el siguiente
        for (int i = 0; i < numStr.length() - 1; i++) {
            if (numStr.charAt(i) > numStr.charAt(i + 1)) {
                return false; // No es ascendente
            }
        }

        return true; // Todos los dígitos están en orden ascendente
    }
}

