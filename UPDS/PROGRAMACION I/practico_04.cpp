// Incluir las librerías necesarias
#include <iostream>
#include <vector>
#include <string>
#include <cstdlib>
#include <ctime>
using namespace std;


// Declarar las constantes
const int TAM = 10; // Tamaño de los vectores
const char SALIR = 's'; // Opción para salir del menú

// Declarar las funciones
void llenar_aleatorio(vector<string>& nombres, vector<string>& apellidos, vector<int>& notas);
void ofuscar(vector<string>& nombres, vector<string>& apellidos, vector<string>& contrasenas, char opcion);
void calcular_estadisticas(vector<int>& notas, double& media, int& mayor, int& menor);
void imprimir_datos(vector<string>& nombres, vector<string>& apellidos, vector<int>& notas, vector<string>& contrasenas);
char mostrar_menu();

// Función principal
int main() {
	// Crear los vectores para nombre, apellido, contraseña y nota
	vector<string> nombres(TAM);
	vector<string> apellidos(TAM);
	vector<string> contrasenas(TAM);
	vector<int> notas(TAM);
	
	// Inicializar la semilla para generar números aleatorios
	srand(time(NULL));
	
	// Declarar las variables para la media, el mayor y el menor de las notas
	double media;
	int mayor;
	int menor;
	
	// Declarar la variable para la opción del menú
	char opcion;
	
	// Mostrar el menú principal y repetir hasta que el usuario elija salir
	do {
		opcion = mostrar_menu();
		switch (opcion) {
		case '1':
			// Llenar nombres, apellidos y notas de forma aleatoria
			llenar_aleatorio(nombres, apellidos, notas);
			cout << "Se han llenado los datos de forma aleatoria.\n";
			break;
		case '2':
			// Ofuscar nombres y apellidos para llenar las contraseñas
			ofuscar(nombres, apellidos, contrasenas, opcion);
			cout << "Se han ofuscado los nombres y apellidos para las contraseñas.\n";
			break;
		case '3':
			// Sacar la media, el mayor y el menor de todas las notas
			calcular_estadisticas(notas, media, mayor, menor);
			cout << "La media de las notas es " << media << ".\n";
			cout << "La nota mayor es " << mayor << ".\n";
			cout << "La nota menor es " << menor << ".\n";
			break;
		case '4':
			// Imprimir datos
			imprimir_datos(nombres, apellidos, notas, contrasenas);
			break;
		case 's':
		case 'S':
			// Salir del programa
			cout << "Gracias por usar el programa. Hasta pronto.\n";
			break;
		default:
			// Opción inválida
			cout << "Opción inválida. Intente de nuevo.\n";
		}
	} while (opcion != SALIR);
	
	return 0;
}

// Función para llenar nombres, apellidos y notas de forma aleatoria
void llenar_aleatorio(vector<string>& nombres, vector<string>& apellidos, vector<int>& notas) {
	// Crear los vectores con los posibles nombres y apellidos
	vector<string> lista_nombres = {"Ana", "Carlos", "David", "Elena", "Fernando", "Gloria", "Hector", "Irene", "Juan", "Laura"};
	vector<string> lista_apellidos = {"Perez", "Gomez", "Lopez", "Sanchez", "Diaz", "Torres", "Garcia", "Rodriguez", "Martinez", "Fernandez"};
	
	// Recorrer los vectores y asignar valores aleatorios
	for (int i = 0; i < TAM; i++) {
		// Generar un índice aleatorio entre 0 y 9 para los nombres y apellidos
		int indice = rand() % 10;
		// Asignar el nombre y el apellido correspondiente al índice
		nombres[i] = lista_nombres[indice];
		apellidos[i] = lista_apellidos[indice];
		// Generar una nota aleatoria entre 0 y 100
		notas[i] = rand() % 101;
	}
}

// Función para ofuscar nombres y apellidos para llenar las contraseñas
void ofuscar(vector<string>& nombres, vector<string>& apellidos, vector<string>& contrasenas, char opcion) {
	// Recorrer los vectores y ofuscar cada nombre y apellido
	for (int i = 0; i < TAM; i++) {
		// Crear una variable para almacenar la contraseña ofuscada
		string contrasena = "";
		// Concatenar el nombre y el apellido
		string nombre_completo = nombres[i] + apellidos[i];
		// Recorrer cada caracter del nombre completo y aplicar la tabla de ofuscación
		for (char c : nombre_completo) {
			// Convertir el caracter a minúscula
			c = tolower(c);
			// Reemplazar el caracter según la tabla
			switch (c) {
			case 'a':
				contrasena += '4';
				break;
			case 'e':
				contrasena += '3';
				break;
			case 'i':
				contrasena += '1';
				break;
			case 'o':
				contrasena += '0';
				break;
			case 's':
				contrasena += '5';
				break;
			case 't':
				// Si la opción es 2, reemplazar la t por 7
				if (opcion == '2') {
					contrasena += '7';
				}
				// Si no, dejar la t como está
				else {
					contrasena += 't';
				}
				break;
			default:
				// Dejar el resto de caracteres como están
				contrasena += c;
			}
		}
		// Asignar la contraseña ofuscada al vector de contraseñas
		contrasenas[i] = contrasena;
	}
}

// Función para calcular la media, el mayor y el menor de todas las notas
void calcular_estadisticas(vector<int>& notas, double& media, int& mayor, int& menor) {
	// Inicializar la suma, el mayor y el menor con el primer elemento del vector
	int suma = notas[0];
	mayor = notas[0];
	menor = notas[0];
	
	// Recorrer el resto del vector y actualizar la suma, el mayor y el menor
	for (int i = 1; i < TAM; i++) {
		suma += notas[i];
		if (notas[i] > mayor) {
			mayor = notas[i];
			
		}
		if (notas[i] < menor) {
			menor = notas[i];
		}
	}
	
	// Calcular la media como la suma dividida por el tamaño del vector
	media = (double) suma / TAM;
}

// Función para imprimir datos
void imprimir_datos(vector<string>& nombres, vector<string>& apellidos, vector<int>& notas, vector<string>& contrasenas) {
	// Imprimir los encabezados de la tabla
	cout << "Nombres\tApellidos\tNota\tContraseña\n";
	// Recorrer los vectores e imprimir los datos de cada fila
	for (int i = 0; i < TAM; i++) {
		cout << nombres[i] << "\t" << apellidos[i] << "\t\t" << notas[i] << "\t" << contrasenas[i] << "\n";
	}
}

// Función para mostrar el menú y devolver la opción elegida
char mostrar_menu() {
	// Declarar la variable para la opción
	char opcion;
	// Imprimir el menú
	cout << "\n\t\tMenú principal\n";
	cout << "(1)-Llenar nombres, apellidos y notas de forma aleatoria\n";
	cout << "(2)-Ofuscar nombres y apellidos para llenar las contraseñas de la opción 1\n";
	cout << "(3)-Sacar la media, el mayor y el menor de todas las notas\n";
	cout << "(4)-Imprimir datos\n";
	cout << "(s/S) Salir\n";
	// Leer la opción
	cout << "\nSu opción: ";
	cin >> opcion;
	// Devolver la opción
	return(opcion);
}
