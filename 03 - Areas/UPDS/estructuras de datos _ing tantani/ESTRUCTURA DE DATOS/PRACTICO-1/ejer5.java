public class ejer5 {
    /**
    Calcular recursivamente la suma de los factores primos de un número
 
    @param n El número a factorizar
    @param divisor El divisor actual a probar (comenzar con 2)
    @return La suma de los factores primos
    */
    public static int sumaFactoresPrimos(int n, int divisor) {
        // Caso base: n se ha reducido a 1
        if (n == 1) {
            return 0;
        }
        
        // Si el divisor actual es un factor
        if (n % divisor == 0) {
            //Se suma y continua factorizando n/divisor
            return divisor + sumaFactoresPrimos(n / divisor, divisor);
        } else {
            // Probamos con el siguiente divisor
            return sumaFactoresPrimos(n, divisor + 1);
        }
    }
    
    public static void main(String[] args) {
        int n = 12;
        System.out.println("Suma de factores primos de " + n + ": " + sumaFactoresPrimos(n, 2));
    }
}