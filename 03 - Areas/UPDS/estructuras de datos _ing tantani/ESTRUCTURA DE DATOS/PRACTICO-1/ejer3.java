public class ejer3 {
    // Devuelve true si los dígitos de n están en orden no decreciente
    public static boolean estaOrdenado(int n) {
        // Un solo dígito o número negativo (considerando solo valor absoluto)
        n = Math.abs(n);
        if (n < 10) {
            return true; 
        }
        int ultimo = n % 10;
        int penultimo = (n / 10) % 10;
        if (penultimo > ultimo){
            return false; 
        }
        return estaOrdenado(n / 10);
    }
    // Imprime el número y si está ordenado
    public static void mostrarOrden(int n) {
        System.out.println("El " + n + " está ordenado: " + estaOrdenado(n));
    }
    public static void main(String[] args) {
        int n = 1537;
        System.out.println("El " + n + " No se encunetra ordenado: " + estaOrdenado(n));	
    }
}