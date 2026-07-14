// NumerosAmigos/NumerosAmigos.java
public class ejer11 {
    /**
    Calcula recursivamente la suma de divisores propios de un número
     
    @param n El número
    @param divisor El divisor actual (comenzar con 1)
    @return Suma de divisores propios (excluyendo al número mismo)
    */
    public static int sumaDivisoresPropios(int n, int divisor) {
        // Caso base: divisor supera n/2 (no hay más divisores posibles)
        if (divisor > n / 2) {
            return 0;
        }
        
        // Paso recursivo
        if (n % divisor == 0) {
            return divisor + sumaDivisoresPropios(n, divisor + 1);
        } else {
            return sumaDivisoresPropios(n, divisor + 1);
        }
    }
    
    /**
     * Determina si dos números son amigos (suma de divisores propios de uno = otro)
     * 
     * @param a Primer número
     * @param b Segundo número
     * @return true si son amigos, false si no
     */
    public static boolean sonAmigos(int a, int b) {
        return sumaDivisoresPropios(a, 1) == b && sumaDivisoresPropios(b, 1) == a;
    }
    
    public static void main(String[] args) {
        int a = 220, b = 284;
        System.out.println(a + " y " + b + " son amigos? " + sonAmigos(a, b));
    }
}