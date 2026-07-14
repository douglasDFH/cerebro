#include <iostream>
using namespace std;

int sumaNumeros(int primerNumero, int segundoNumero){
    int resultado = primerNumero + segundoNumero;
    return resultado;
}

int main () {
    int n;
    cout << "Ingrese un numero: \n"; 
    cin >> n;
    int y;
    cout << "Ingrese un numero: \n"; 
    cin >> y;
    cout << "La suma es: " << sumaNumeros(n,y) << std::endl;
return 0;
}