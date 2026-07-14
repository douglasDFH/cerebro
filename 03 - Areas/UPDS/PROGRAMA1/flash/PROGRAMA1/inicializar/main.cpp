#include <iostream>
using namespace std;

int main () {
    // inicializar por {}
   /* int numeroSillas;  // es ramdom lo que tiene la memoria

    int numeroMesas{};  // empieza en 0
    int numeroMouse{2}; //empieza en 2
    //int numeroMouse{2.9}; //sino match tipo de dato error
*/

    // inicializar por ()
   /* int numeroSillas;
    int numeroMesas(3);  // empieza en 0
    int numeroMouse(4.9); //empieza en 2
*/
    // inicializar por =
    int numeroSillas;
    int numeroMesas = 1;  // empieza en 0
    int numeroMouse = 4.9; //empieza en 2

    cout << "Sillas :" <<numeroSillas << endl;
    cout << "Mesas :" <<numeroMesas << endl;
    cout << "Mouse :" <<numeroMouse << endl;

return 0;
}