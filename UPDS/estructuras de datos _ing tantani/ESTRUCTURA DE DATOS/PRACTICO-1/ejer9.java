// ConvertirABinario/ConvertirABinario.java
public class ejer9 {
    /**
    Convierte recursivamente un número decimal a su representación binaria como String
     
    @param n El número a convertir
    @return String con la representación binaria
    */
    public static String aBinario(int n) {
        // Caso base: n es 0 o 1
        if (n <= 1) {
            return Integer.toString(n);
        }
        
        // Paso recursivo: binario de n/2 concatenado con n%2
        return aBinario(n / 2) + Integer.toString(n % 2);
    }
    
    public static void main(String[] args) {
        int n = 12;
        System.out.println(n + " en binario: " + aBinario(n));
    }
}