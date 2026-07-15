// Incluimos las librerías necesarias
#include <iostream>
#include <cstdlib>
#include <ctime>
	using namespace std;

// Declaramos las funciones que vamos a usar
void generarNumeroEntero(int& numero);
void generarNumeroReal(double& numero);
void generarNumerosPrimos(int n, int* primos);
bool esPrimo(int numero);
void mostrarMenu();
void ejecutarOpcion(char opcion);

int main() {
	// Declaramos una variable para guardar la opción elegida
	char opcion;
	// Mostramos el menú y pedimos la opción
	mostrarMenu();
	cin >> opcion;
	// Ejecutamos la opción mientras sea válida
	while (opcion == '1' || opcion == '2' || opcion == '3') {
		ejecutarOpcion(opcion);
		// Mostramos el menú y pedimos la opción nuevamente
		mostrarMenu();
		cin >> opcion;
	}
	// Terminamos el programa
	cout << "Gracias por usar el programa." << endl;
	return 0;
}

// Función que genera un número entero aleatorio entre 0 y 500 y lo guarda por referencia
void generarNumeroEntero(int& numero) {
	// Inicializamos el generador de números aleatorios con la hora actual
	srand(time(NULL));
	// Generamos un número aleatorio entre 0 y 500 y lo guardamos en la variable
	numero = rand() % 501;
}

// Función que genera un número real aleatorio entre 0 y 1 y lo guarda por referencia
void generarNumeroReal(double& numero) {
	// Inicializamos el generador de números aleatorios con la hora actual
	srand(time(NULL));
	// Generamos un número real aleatorio entre 0 y 1 y lo guardamos en la variable
	numero = (double) rand() / RAND_MAX;
}

// Función que genera n números primos y los guarda en un arreglo por referencia
void generarNumerosPrimos(int n, int* primos) {
	// Declaramos una variable para guardar el número candidato a primo
	int candidato = 2;
	// Declaramos un contador para llevar la cuenta de los números primos generados
	int contador = 0;
	// Generamos números primos hasta completar n
	while (contador < n) {
		// Verificamos si el candidato es primo
		if (esPrimo(candidato)) {
			// Si es primo, lo guardamos en el arreglo
			primos[contador] = candidato;
			// Incrementamos el contador
			contador++;
		}
		// Incrementamos el candidato
		candidato++;
	}
}

// Función que verifica si un número es primo por valor
bool esPrimo(int numero) {
	// Si el número es menor que 2, no es primo
	if (numero < 2) {
		return false;
	}
	// Si el número es 2, es primo
	if (numero == 2) {
		return true;
	}
	// Si el número es par, no es primo
	if (numero % 2 == 0) {
		return false;
	}
	// Si el número tiene algún divisor impar entre 3 y su raíz cuadrada, no es primo
	for (int i = 3; i * i <= numero; i += 2) {
		if (numero % i == 0) {
			return false;
		}
	}
	// Si no se cumple ninguna de las condiciones anteriores, el número es primo
	return true;
}

// Función que muestra el menú al usuario
void mostrarMenu() {
	// Mostramos las opciones disponibles
	
	cout << "\nBIENVENIDO MENU GENERANDO NUMEROS ALEATORIOS" << endl;
	cout << "1) Genere al azar números entre 0-500" << endl;
	cout << "2) Genere un número aleatorio real" << endl;
	cout << "3) Genere N números primos" << endl;
	cout << "S) Salir" << endl;
	cout << "Seleccione una opción: ";
}

// Función que ejecuta la opción elegida por el usuario
void ejecutarOpcion(char opcion) {
	// Declaramos las variables a usar
	int numeroEntero;
	double numeroReal;
	int n;
	int* primos;
	// Usamos un switch para ejecutar la opción correspondiente
	switch (opcion) {
	case '1':
		// Llamamos a la función para generar un número entero aleatorio
		generarNumeroEntero(numeroEntero);
		// Mostramos el resultado
		cout << "\nEl número entero aleatorio entre 0 y 500 es: " << numeroEntero << endl;
		break;
	case '2':
		// Llamamos a la función para generar un número real aleatorio
		generarNumeroReal(numeroReal);
		// Mostramos el resultado
		cout << "\nEl número real aleatorio entre 0 y 1 es: " << numeroReal << endl;
		break;
	case '3':
		// Pedimos al usuario que ingrese el valor de n
		cout << "Ingrese el valor de N: ";
		cin >> n;
		// Reservamos memoria para el arreglo de primos
		primos = new int[n];
		// Llamamos a la función para generar n números primos
		generarNumerosPrimos(n, primos);
		// Mostramos el resultado
		cout << "\nLos primeros " << n << " números primos son: " << endl;
		for (int i = 0; i < n; i++) {
			cout << primos[i] << " ";
		}
		cout << endl;
		// Liberamos la memoria del arreglo
		delete[] primos;
		break;
	default:
		// Mostramos un mensaje de error
		cout << "Opción inválida." << endl;
	}
}
