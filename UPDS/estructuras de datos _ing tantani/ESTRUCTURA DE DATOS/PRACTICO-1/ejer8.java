// ContarUnosBinarios/ContarUnosBinarios.java
public class ejer8 {
    /**
    Cuenta recursivamente los bits en 1 en la representación binaria de un número
     
    @param n El número a analizar
    @return Cantidad de unos en su representación binaria
    */
    public static int contarUnos(int n) {
        // Caso base: n es 0
        if (n == 0) {
            return 0;
        }
        
        // Paso recursivo: último bit + conteo en los demás bits
        return (n & 1) + contarUnos(n >> 1);
    }
    
    public static void main(String[] args) {
        int n = 5; // 101 en binario
        System.out.println("Unos en binario de " + n + ": " + contarUnos(n));
    }
}