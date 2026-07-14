#include <iostream>
#include <iomanip>
using namespace std;

int main () {
    
    float numero1 {1.12345678901234567890};  // presion de 6 apro digitos 4 bytes
    double numero2 {1.12345678901234567890}; // presion de 15 aprox digitos 8 bytes
    long double numero3 {1.12345678901234567890L}; // presion de 31 aprox digitos 16 bytes

    cout << " tamaño de float : " << sizeof(float) << endl;
    cout << " tamaño de double : " << sizeof(double) << endl;
    cout << " tamaño de long double : " << sizeof(long double) << endl;

    //presion 
    cout << setprecision(20) ; //controlar la presion de la los digitos
    cout << "Numero1 es : " << numero1 << endl;
    cout << "Numero2 es : " << numero2 << endl;
    cout << "Numero3 es : " << numero3 << endl;

    //aprovechando iomanip
    //infinito
    double y {5};
    double x {};
    double z {};
    cout << "numero / 0 : " << y/x << endl;
    //NaN
    cout << "0 / 0 : " << x/z << endl;

return 0;
}