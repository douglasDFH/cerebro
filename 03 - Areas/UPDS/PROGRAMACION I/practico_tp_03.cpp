#include <iostream>
#include <string>
#include <sstream>
using namespace std;
string nombre;
// Función para sumar los números de una cadena
int suma_cadena(string cadena) {
	int suma = 0;
	stringstream ss(cadena); // Crear un flujo de entrada con la cadena
	string token; // Variable para almacenar cada número
	while (getline(ss, token, '+')) { // Separar la cadena por el signo +
		suma += stoi(token); // Convertir el número a entero y sumarlo
	}
	return suma; // Devolver la suma
}

// Función para invertir el orden de las palabras de un nombre completo
string nombre_completo(string nombre) {
	
	int espacio = nombre.find (" ");
	string apellido = nombre.substr (0, espacio);
	string resto = nombre.substr (espacio + 1);
	return resto + " " + apellido;
}
// Función para ofuscar una cadena según la tabla dada
string string_ofuscar(string cadena) {
	string ofuscado = " "; // Variable para almacenar la cadena ofuscada
	for (char c : cadena) { // Recorrer cada caracter de la cadena
		switch (c) { // Según el caracter, aplicar la ofuscación correspondiente
		case 'A': case 'a': ofuscado += '4'; break;
		case 'E': case 'e': ofuscado += '3'; break;
		case 'I': case 'i': ofuscado += '1'; break;
		case 'O': case 'o': ofuscado += '0'; break;
		case 'S': case 's': ofuscado += '5'; break;
		case 'T': case 't': ofuscado += '7'; break;
		default: ofuscado += c; // Si no es ninguno de los anteriores, mantener el mismo caracter
		}
	}
	return ofuscado; // Devolver la cadena ofuscada
}

// Función para mostrar el menú principal y leer la opción elegida
int menu_principal() {
	int opcion = 0; // Variable para almacenar la opción elegida
	cout << "*BIENVENIDO AL MENU PRINCIPAL*\n";
	cout << "------------------------------"<<endl;
	cout << "--  1) SUMA DE CADENA      ---\n";
	cout << "--  2) NOMBRE COMPLETO     ---\n";
	cout << "--  3) PALABRA PARA OFUSCAR---\n";
	cout << "--  4) SALIR DEL MENU    ---\n";
	cout << "------------------------------"<<endl;
	cout << "  Elija una opción: ";
	cin >> opcion; // Leer la opción del teclado
	return opcion; // Devolver la opción
}

// Función para mostrar el submenú de la opción 1 y leer la cadena a sumar
string submenu_1() {
	string cadena = ""; // Variable para almacenar la cadena a sumar
	cout <<endl;
	cout << "------------------------------------------------------\n";
	cout << "---     Submenú de la opción 1 suma de cadenas   ----- \n";
	cout << "--Ingrese una cadena de números separados por el signo +:";         
	
	cin >> cadena; // Leer la cadena del teclado
	return cadena; // Devolver la cadena
}

// Función para mostrar el submenú de la opción 2 y leer el nombre completo
string submenu_2() {
	string nombre = ""; // Variable para almacenar el nombre completo
	cout <<endl;
	cout << "-------------------------------------------------------\n";
	cout << "---      Submenú de la opción 2 Nombre Completo     ---\n";
	cout << "- Ingrese un nombre completo para cambiar de posicion : ";
	cin.ignore(); // Ignorar el salto de línea anterior
	getline(cin, nombre); // Leer el nombre completo del teclado
	return nombre; // Devolver el nombre
}

// Función para mostrar el submenú de la opción 3 y leer la cadena a ofuscar
string submenu_3() {
	string cadena = ""; // Variable para almacenar la cadena a ofuscar
	cout <<endl;
	cout << "-------------------------------------------------------\n";
	cout << "---   Submenú de la opción 3 palabra para Ofuscar   ---\n";
	cout << "-------------------------------------------------------\n";
	cout << "-------------|   CARACTER   |  OFUSCADO   |------------\n";
	cout << "-------------|----------------------------|------------\n";
	cout << "-------------|     A/a      |     4       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "-------------|     E/e      |     3       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "-------------|     I/i      |     1       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "-------------|     O/O      |     0       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "-------------|     S/s      |     5       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "-------------|     T/t      |     7       |------------\n";
	cout << "-------------|--------------|-------------|------------\n";
	cout << "Ingrese una cadena a ofuscar: ";
	cin >> cadena; // Leer la cadena del teclado
	return cadena; // Devolver la cadena
}

// Programa principal
int main() {
	int opcion = 0; // Variable para almacenar la opción del menú principal
	string cadena = ""; // Variable para almacenar la cadena del submenú
	int resultado = 0; // Variable para almacenar el resultado de la suma de cadena
	string invertido = ""; // Variable para almacenar el nombre completo invertido
	string ofuscado = ""; // Variable para almacenar la cadena ofuscada
	bool salir = false; // Variable para controlar el bucle del menú principal
	while (!salir) { // Mientras no se elija salir
		opcion = menu_principal(); // Mostrar el menú principal y leer la opción
		switch (opcion) { // Según la opción elegida
		case 1: // Si es la opción 1
			cadena = submenu_1(); // Mostrar el submenú de la opción 1 y leer la cadena
			resultado = suma_cadena(cadena); // Calcular la suma de la cadena
			cout << endl;
			cout << "   *** La suma de la cadena " << cadena << " es " << resultado <<"\n"; // Mostrar el resultado
			cout << endl ;
			break;
		case 2: // Si es la opción 2
			nombre = submenu_2(); // Mostrar el submenú de la opción 2 y leer el nombre
			invertido = nombre_completo(nombre); // Invertir el orden de las palabras del nombre
			cout <<endl;
			cout << "*** El nombre completo " << nombre << " invertido es " << invertido << "\n"; // Mostrar el nombre invertido
			cout <<endl;
			break;
		case 3: // Si es la opción 3
			cadena = submenu_3(); // Mostrar el submenú de la opción 3 y leer la cadena
			ofuscado = string_ofuscar(cadena); // Ofuscar la cadena según la tabla
			cout << endl;
			cout << "La cadena " << cadena << " ofuscada es " << ofuscado << "\n"; // Mostrar la cadena ofuscada
			cout << endl;
			break;
		case 4: // Si es la opción 4
			salir = true; // Cambiar la variable salir a verdadero
			cout << "Gracias por usar el programa. Adiós.\n"; // Mostrar un mensaje de despedida
			break;
		default: // Si no es ninguna de las anteriores
			cout << "Opción inválida. Intente de nuevo.\n"; // Mostrar un mensaje de error
		}
	}
	return 0; // Terminar el programa
 }
