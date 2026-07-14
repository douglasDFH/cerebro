// SumaDigitos/SumaDigitos.java
public class ejer7 {
    /**
    Calcula recursivamente la suma de los dígitos de un número
     
    @param n El número del cual sumar los dígitos
    @return La suma de los dígitos
     */
    public static int sumaDigitos(int n) {
        // Caso base: número de un solo dígito
        if (n < 10) {
            return n;
        }
        
        // Paso recursivo: último dígito + suma de los demás dígitos
        return (n % 10) + sumaDigitos(n / 10);
    }
    
    public static void main(String[] args) {
        int n = 12345;
        System.out.println("Suma de dígitos de " + n + ": " + sumaDigitos(n));
    }
}