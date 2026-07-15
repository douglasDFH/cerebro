// generar numero aleatorios 
//objetivos escribir un programa en c++ que imprima N numeros aleatorios
// entre 0 y 99don de n sera ingresado por el usuario
// cuantos numero quiere generar :3 
//3,45,12
#include <iostream>// libreria principal 
#include <cstdlib> // para usar la función rand()
#include <ctime> // para usar la función time()
using namespace std;

int main()
{
	int n; // declarar una variable para almacenar el número de números aleatorios
	cout << "Ingrese un numero : "; // pedir al usuario que ingrese el número
	cin >> n; // leer el número desde la entrada estándar
	                
	// inicializar el generador de números aleatorios con la hora actual
    // para que cada vez que se ejecute el programa se obtengan valores diferentes
	srand(time(0));
	// ejecutar un bucle desde 1 hasta n
	// generar e imprimir los números aleatorios
+	for (int i = 1; i <= n; i++)// obtener un número aleatorio entre 0 y RAND_MAX, por ejemplo 32767
	{
		int r = rand() % 100;// para reducir el rango a 0 y 99, se usa el operador módulo (%)
		cout << r << " ";// mostrar el número aleatorio en la salida estándar
	}
	  
	cout << endl;// mostrar un salto de línea al final
	return 0;
}
