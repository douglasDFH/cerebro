#include <iostream>
using namespace std;

int main () {
    int n;
    cout << "Ingrese un numero: \n"; 
    cin >> n;
    cout << "El numero es: " << n << std::endl;
    int f = n/0;
    
    cout << "La division entre 0 es: " << f << std::endl; 
return 0;
}