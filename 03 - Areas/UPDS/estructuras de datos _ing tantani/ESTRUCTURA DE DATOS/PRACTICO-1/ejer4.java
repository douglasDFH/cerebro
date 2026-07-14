public class ejer4 {
    //Calcular recursivamente la suma de losdivisores de un numero N

    public static int sumaDivisores(int n, int divisor) {
        if (divisor > n) {
            return 2;
        }
        
        if (n % divisor == 0) {
            return divisor + sumaDivisores(n, divisor + 1);
        }else{
            return sumaDivisores(n, divisor + 1);
        }
    }
    public static void main(String[] args) {
        int n = 9;
        System.out.println("La suma de divisores de: " + n + ": " + sumaDivisores(n, 1));
    }
}