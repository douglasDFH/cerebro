// Incluir las librerías necesarias
#include <iostream>
#include <vector>
#include <string>
#include <cstdlib>
using namespace std;

// Declarar los vectores globales
vector<string> nombres(10);
vector<string> apellidos(10);
vector<string> contrasenas(10);
vector<int> notas(10);

// Definir las funciones del menú

// Función que llena los vectores de forma aleatoria
void llenarAleatorio(vector<string> &nombres, vector<string> &apellidos, vector<string> &contrasenas, vector<int> &notas) {
	// Arreglos auxiliares con nombres y apellidos posibles
	string nombresPosibles[] = {"Ana", "Carlos", "David", "Elena", "Fernando", "Gloria", "Hugo", "Irene", "Juan", "Karen"};
	string apellidosPosibles[] = {"Perez", "Gomez", "Lopez", "Diaz", "Sanchez", "Torres", "Garcia", "Rodriguez", "Martinez", "Flores"};
	// Bucle para recorrer los vectores
	for (int i = 0; i < 10; i++) {
		// Generar un nombre y un apellido aleatorios
		nombres[i] = nombresPosibles[rand() % 10];
		apellidos[i] = apellidosPosibles[rand() % 10];
		// Generar una contraseña vacía
		contrasenas[i] = "";
		// Generar una nota aleatoria entre 0 y 20
		notas[i] = rand() % 21;
	}
}

// Función que ofusca los nombres y apellidos para llenar las contraseñas
void ofuscarNombres(vector<string> &nombres, vector<string> &apellidos, vector<string> &contrasenas) {
	// Bucle para recorrer los vectores
	for (int i = 0; i < 10; i++) {
		// Obtener el nombre y el apellido del vector
		string nombre = nombres[i];
		string apellido = apellidos[i];
		// Reemplazar cada letra por un asterisco
		nombre.replace(0, nombre.length(), nombre.length(), '*');
		apellido.replace(0, apellido.length(), apellido.length(), '*');
		// Asignar la contraseña ofuscada al vector
		contrasenas[i] = nombre + apellido;
	}
}

// Función que saca la media, el mayor y el menor de las notas
void estadisticasNotas(vector<int> &notas) {
	// Variables para almacenar la media, el mayor y el menor
	double media = 0;
	int mayor = 0;
	int menor = 20;
	// Bucle para recorrer el vector
	for (int i = 0; i < 10; i++) {
		// Obtener la nota del vector
		int nota = notas[i];
		// Acumular la nota para la media
		media += nota;
		// Comparar la nota con el mayor y el menor
		if (nota > mayor) {
			mayor = nota;
		}
		if (nota < menor) {
			menor = nota;
		}
	}
	// Calcular la media dividiendo por el número de elementos
	media /= 10;
	// Mostrar los resultados
	cout << "La media de las notas es: " << media << endl;
	cout << "La nota mayor es: " << mayor << endl;
	cout << "La nota menor es: " << menor << endl;
}

// Función que imprime los datos de los vectores
void imprimirDatos(vector<string> &nombres, vector<string> &apellidos, vector<int> &notas) {
	// Mostrar un encabezado
	cout << "NOMBRE\tAPELLIDO\tNOTA" << endl;
	// Bucle para recorrer los vectores
	for (int i = 0; i < 10; i++) {
		// Mostrar el nombre, el apellido y la nota del vector
		cout << nombres[i] << "\t" << apellidos[i] << "\t\t" << notas[i] << endl;
	}
}

// Función principal
int main() {
	// Variable para almacenar la opción del usuario
	char opcion;
	// Bucle para mostrar el menú hasta que el usuario elija salir
	do {
		// Mostrar el menú
		cout << "\t\t\tMenu principal" << endl;
		cout << "(1)-Llenar nombres, apellidos y notas de forma aleatoria" << endl;
		cout << "(2)-Ofuscar nombres y apellidos para llenar las contraseñas" << endl;
		cout << "(3)-Sacar la media, el mayor y el menor de todas las notas" << endl;
		cout << "(4)-Imprimir datos" << endl;
		cout << "(s/S)-Salir" << endl;
		// Solicitar la opción al usuario
		cout << "Ingrese una opcion: ";
		cin >> opcion;
		// Ejecutar la opción correspondiente
		switch (opcion) {
		case '1':
			// Llenar los vectores de forma aleatoria
			llenarAleatorio(nombres, apellidos, contrasenas, notas);
			cout << "Los vectores se han llenado de forma aleatoria." << endl;
			break;
		case '2':
			// Ofuscar los nombres y apellidos para llenar las contraseñas
			ofuscarNombres(nombres, apellidos, contrasenas);
			cout << "Los nombres y apellidos se han ofuscado para llenar las contraseñas." << endl;
			break;
		case '3':
			// Sacar la media, el mayor y el menor de las notas
			estadisticasNotas(notas);
			break;
		case '4':
			// Imprimir los datos de los vectores
			imprimirDatos(nombres, apellidos, notas);
			break;
		case 's':
		case 'S':
			// Salir del programa
			cout << "Gracias por usar el programa. Adios!" << endl;
			break;
		default:
			// Mostrar un mensaje de error
			cout << "Opcion invalida. Intenta de nuevo." << endl;
			break;
		}
	} while (opcion != 's' && opcion != 'S');
 return 0;
}
