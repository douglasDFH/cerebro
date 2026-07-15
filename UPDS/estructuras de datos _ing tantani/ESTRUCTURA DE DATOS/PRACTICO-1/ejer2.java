public class ejer2 {
    // Método recursivo para calcular la sumatoria de un número n
    public static int sumatoria(int n) {
        if (n <= 0) { //5<=0
            return 0;
        }
        // Llamada recursiva: n + sumatoria(n - 1)
        return n + sumatoria(n - 1);
        /*  
         * 1. 5 + sumatoria(4)
         * 2. 4 + sumatoria(3)
         * 3. 3 + sumatoria(2)
         * 4. 2 + sumatoria(1)
         * 5. 1 + sumatoria(0)
         * 6. 0 (caso base)
         * Resultado final: 5 + 4 + 3 + 2 + 1 + 0 = 15
         */
    }

    public static void main(String[] args) {
        int n = 5; // Puedes cambiar este valor para probar con otros números
        System.out.println("La sumatoria de " + n + " es: " + sumatoria(n));
    }
}