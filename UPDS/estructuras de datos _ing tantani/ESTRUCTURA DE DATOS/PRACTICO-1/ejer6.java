// Factorial/Factorial.java
public class ejer6 {
    /**
    Calcula recursivamente el factorial de un número
     
    @param n El número para calcular el factorial
    @return n!
    */
    public static int factorial(int n) {
        // Caso base: factorial de 0 o 1 es 1
        if (n <= 1) {
            return 1;
        }
        
        // Paso recursivo: n! = n * (n-1)!
        return n * factorial(n - 1);
    }
    
    public static void main(String[] args) {
        int n = 5;
        System.out.println("Factorial de " + n + ": " + factorial(n));
    }
}