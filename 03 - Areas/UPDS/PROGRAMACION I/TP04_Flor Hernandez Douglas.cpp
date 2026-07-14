	//Incluir las librerías necesarias
	#include <iostream>
	#include <vector>
	#include <string>
	#include <cstdlib>
	#include <ctime>
		using namespace std;

	// Declarar los vectores de nombres y apellidos con los valores deseados
	vector<string> nombres = {"Ana", "María", "Pedro", "José", "Luisa", "Carlos", "Elena", "David", "Gloria", "juan"};
	vector<string> apellidos = {"Pérez", "Martín", "Suárez", "Gómez", "López", "Sánchez", "Díaz", "Torres", "García", "Roldan"};

	// Declarar la constante para la opción de salir del menú
	const string SALIR = "s";
	const string SALIR_MAY = "S";

	// Declarar las funciones
	void llenar_aleatorio(vector<int>& notas);
	void ofuscar(vector<string>& contrasenas, string opcion);
	void calcular_estadisticas(vector<int>& notas, double& media, int& mayor, int& menor, vector<string>& contrasenas);
	void imprimir_datos(vector<int>& notas, vector<string>& contrasenas);
	string mostrar_menu();

	// Función principal
	int main() {
		// Crear los vectores para contraseña y nota, usando el tamaño de los vectores de nombres y apellidos
		vector<string> contrasenas(nombres.size());
		vector<int> notas(nombres.size());
		
		// Inicializar la semilla para generar números aleatorios
		srand(time(NULL));
		
		// Declarar las variables para la media, el mayor y el menor de las notas
		double media;
		int mayor;
		int menor;
		
		// Declarar la variable para la opción del menú
		string opcion;
		
		// Mostrar el menú principal y repetir hasta que el usuario elija salir
		do {
			opcion = mostrar_menu();
			if (opcion == "1") {
				// Llenar notas de forma aleatoria
				llenar_aleatorio(notas);
				cout << "\nMUY BIEN SE LLENO DE FORMA CORRECTA, NOMBRES,APELLIDOS Y NOTAS DE FORMA ALEATORIA.\n";
			}
			else if (opcion == "2") {
				// Ofuscar nombres y apellidos para llenar las contraseñas
				ofuscar(contrasenas, opcion);
				cout << "\nMUY BIEN SE HAN OFUSCADO LOS NOMBRES,APELLIDOS PARA LLENAR LAS CONTRASEÑAS.\n";
			}
			else if (opcion == "3") {
				// Sacar el promedio, el mayor y el menor de todas las notas con los nombres, apellidos y contraseña
				calcular_estadisticas(notas, media, mayor, menor, contrasenas);
			}
			else if (opcion == "4") {
				// Imprimir datos
				imprimir_datos(notas, contrasenas);
			}
			else if (opcion == SALIR) {
				// Salir del programa
				cout << "Gracias por usar el programa. Hasta pronto.\n";
			}
			else {
				// Opción inválida
				cout << "Opción inválida. Intente de nuevo.\n";
			}
		} while (opcion != SALIR);
		
		return 0;
	}

	// Función para llenar notas de forma aleatoria
	void llenar_aleatorio(vector<int>& notas) {
		// Recorrer el vector de notas y asignar valores aleatorios entre 0 y 100
		for (int i = 0; i < notas.size(); i++) {
			notas[i] = rand() % 101;
		}
	}

	// Función para ofuscar nombres y apellidos para llenar las contraseñas
	void ofuscar(vector<string>& contrasenas, string opcion) {
		// Recorrer el vector de contraseñas y asignar valores ofuscados
		for (int i = 0; i < contrasenas.size(); i++) {
			// Concatenar el nombre y el apellido en mayúsculas
			string nombre_apellido = nombres[i] + apellidos[i];
			for (char& c : nombre_apellido) {
				c = toupper(c);
			}
			// Reemplazar las vocales por números
			for (char& c : nombre_apellido) {
				switch (c) {
				case 'A':
					c = '4';
					break;
				case 'E':
					c = '3';
					break;
				case 'I':
					c = '1';
					break;
				case 'O':
					c = '0';
					break;
				case 'U':
					c = '9';
					break;
				}
			}
			// Asignar el valor ofuscado a la contraseña
			contrasenas[i] = nombre_apellido;
		}
	}

	// Función para sacar el promedio, el mayor y el menor de todas las notas con los nombres, apellidos y contraseña
	void calcular_estadisticas(vector<int>& notas, double& media, int& mayor, int& menor, vector<string>& contrasenas) {
		// Inicializar el mayor y el menor con el primer valor del vector
		int suma = notas [0];
		mayor = notas[0];
		menor = notas[0];
		int indice_mayor = 0;
		int indice_menor = 0;
		
		// Recorrer el vector de notas y comparar cada valor con el mayor y el menor
		for (int i = 0; i < notas.size(); i++) {
			suma+=notas[i];
			if (notas[i] > mayor) {
				mayor = notas[i];
				indice_mayor= i;
			}
			if (notas[i] < menor) {
				menor = notas[i];
				indice_menor = i;
			}
		}
		
		// Calcular el promedio solo con el mayor y el menor
		media = (mayor + menor) / 2.0;
		
		// Mostrar los resultados por pantalla
		cout << "\n------------------------------------------------------------------------------\n";
		cout << "Nombres\t\tApellidos\tNota\t\tContraseña\testadisticas\n";
		cout << "-------\t\t---------\t----\t\t----------\t-------------\n";
		cout <<"" <<nombres[indice_mayor]<<"\t\t" <<apellidos[indice_mayor]<<"\t\t"<<mayor<< "\t\t" << contrasenas[indice_mayor] << "\tmayor nota\n";
		cout <<""<<nombres[indice_menor]<<"\t\t"<<apellidos[indice_menor]<<"\t\t"<<menor<<"\t\t"<<contrasenas[indice_menor]<<"\tmenor nota\n"; 
		cout << "El promedio de las notas es: " << media << "\n";
		cout << "-------------------------------------------------------------------------------\n";
	}

	// Función para imprimir datos
	void imprimir_datos(vector<int>& notas, vector<string>& contrasenas) {
		// Recorrer los vectores y mostrar los datos por pantalla7
		cout << "-------------------------------------------------------------\n";
		cout << "Nombres\t\tApellidos\tNota\t\tContraseña\n";
		cout << "-------------------------------------------------------------\n";
		for (int i = 0; i < notas.size(); i++) {
			cout << ""<< nombres[i]<<"\t\t"<< apellidos[i] <<"\t\t"<<notas[i] <<"\t\t"<< contrasenas[i] << "\n";
		}
		cout << "-------------------------------------------------------------\n";
	}

	// Función para mostrar el menú y devolver la opción elegida
	string mostrar_menu() {
		// Declarar la variable para la opción
		string opcion;
		// Mostrar el menú por pantalla
		cout << "\nMenú principal\n";
		cout << "(1). Llenar notas de forma aleatoria\n";
		cout << "(2). Ofuscar nombres y apellidos para las contraseñas\n";
		cout << "(3). Sacar la media,el mayor y menor de todas las notas\n";
		cout << "(4). Imprimir datos\n";
		cout << "(s/S). Salir\n";
		cout << "Elija una opción: ";
		// Leer la opción del teclado
		cin >> opcion;
		// Devolver la opción
		return opcion;
	}
