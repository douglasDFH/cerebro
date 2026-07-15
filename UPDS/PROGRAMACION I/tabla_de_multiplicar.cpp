//ejercicio 1 ta tabla de multiplicar 
// objetivo escribir un programa en c++ que imprima la tabla de multiplicar de un numero dado por el usuario
//utilizando el for
#include <iostream>
using namespace std;

int main(int argc, char *argv[]) {
	
	int n, rango; // declarar dos variables para almacenar el número y el rango
	cout << "Ingrese un número : "; // pedir al usuario que ingrese el numero
	cin >> n; // leer el número desde la entrada estándar
	cout << "Ingrese el rango: "; // pedir al usuario que ingrese el rango
	cin >> rango; // leer el rango desde la entrada estándar
	// se ejecuta un bucle  desde 1 hasta el rango ingresado por el usuario
	// imprimir la tabla de multiplicar
	for (int i = 1; i <= rango; ++i)
	{
		cout << n << " * " << i << " = " << n * i << endl;
	}
	
	return 0;
}

