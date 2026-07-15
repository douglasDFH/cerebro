// NumeroPerfecto/NumeroPerfecto.java
public class ejer10 {
    /**
    Calcula recursivamente si un número es perfecto (suma de divisores propios = número)
     
    @param n El número a evaluar
    @param divisor El divisor actual (comenzar con 1)
    @param suma Suma acumulada de divisores (comenzar con 0)
    @return true si es perfecto, false si no
    */
    public static boolean esPerfecto(int n, int divisor, int suma) {
        // Caso base: hemos probado todos los divisores posibles (hasta n/2)
        if (divisor > n / 2) {
            return suma == n;
        }
        
        // Paso recursivo: si es divisor, lo sumamos y continuamos
        if (n % divisor == 0) {
            return esPerfecto(n, divisor + 1, suma + divisor);
        } else {
            return esPerfecto(n, divisor + 1, suma);
        }
    }
    
    public static void main(String[] args) {
        int n = 28;
        System.out.println(n + " es perfecto? " + esPerfecto(n, 1, 0));
    }
}