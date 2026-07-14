// SumaDivisoresPropios/SumaDivisoresPropios.java
public class ejer12 {
    /**
    Calcula recursivamente la suma de los divisores propios de un número
    (divisores excluyendo al número mismo)
     
    @param n El número
    @param divisor El divisor actual (comenzar con 1)
    @return Suma de divisores propios
    */
    public static int sumaDivisoresPropios(int n, int divisor) {
        // Caso base: divisor alcanza n (lo excluimos)
        if (divisor >= n) {
            return 0;
        }
        
        // Paso recursivo
        if (n % divisor == 0) {
            return divisor + sumaDivisoresPropios(n, divisor + 1);
        } else {
            return sumaDivisoresPropios(n, divisor + 1);
        }
    }
    
    public static void main(String[] args) {
        int n = 12;
        System.out.println("La Suma de divisores propios de " + n + ": " + sumaDivisoresPropios(n, 1));
    }
}