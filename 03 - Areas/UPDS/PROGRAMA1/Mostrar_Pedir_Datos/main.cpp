#include <iostream>
#include <limits>
using namespace std;

int main () {
   /* 
    cout << "Ingrese Su edad: ";
    int edad;
    cin >> edad;
    cout << "Ingrese su Nombre completo";
    cin.ignore(numeric_limits<streamsize>::max(), '\n');
    string nombreCompleto;
    getline(cin, nombreCompleto);
    cout << nombreCompleto << " Usted tiene " << edad << endl;
*/
    cout << "Ingrese Su edad seguido de su ci y nombre separado por espacio";
    int edad;
    string ci, nombre;
    cin >> edad >> ci >> nombre;
    cout << "Ingrese Su edad " << edad << ", ci es " << ci << ", nombre es " << nombre;
return 0;
}