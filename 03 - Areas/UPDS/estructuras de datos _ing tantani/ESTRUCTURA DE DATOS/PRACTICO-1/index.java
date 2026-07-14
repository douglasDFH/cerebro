import java.util.Scanner;

public class index {

    public static boolean esPrimoRec(int n, int i) { //inicio de la función recursiva
        if (n <= 2){
            return n == 2; // 2 es primo, 1 y 0 no lo son
        }
        if (n % i == 0){
            return false;
        }    
        if (i * i > n) {
            return true; 
        }    
        return esPrimoRec(n, i + 1);
    }
    
    /**
     * Consolas, 'Courier New', monospace .. tipo de letra
     * 
     * 'Lucida Calligraphy', cursive .. tipo de letra
     * 
     * Programa principal que solicita un número al usuario y verifica si es primo
     */
    public static void main(String[] args) {
        // Crear un objeto Scanner para leer la entrada del usuario
        try (Scanner scanner = new Scanner(System.in)){ 

            System.out.print("Ingrese un número: ");
            // Leer un número entero del usuario
            int numero = scanner.nextInt();
            if (esPrimoRec(numero, 2)) { // Llamada a la función recursiva con i=2
                System.out.println(numero + " es primo.");
            } else {
                System.out.println(numero + " no es primo.");
            }       
            scanner.close();
        } 
    }
}