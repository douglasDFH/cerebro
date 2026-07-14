/*generar  en c ++ un programa donde tenga las opciones de menu y submenu de la siguiente manera 
	menu principal
	1)suma de cadena ->"15+5+3"=23 o "25+10"=35
	2)nombre completo ->"juan perez flores"  imprime "perez flores juan"
	3)string ofuscar-> "mando" ->m4nd0
	para el menu de opcion 3 debe de realizar el sigiente cambio de tabla 
	
	caracter   ofuscado
	A/a     -     4
	E/e     -     3
	I/i     -     1
	O/o     -     0
	S/s     -     5
	T/t     -     7*/
#include <iostream>
#include <string>
using namespace std;

// Función para sumar una cadena de números separados por "+"
int suma_cadena(string cadena) {
	int suma = 0;
	int num = 0;
	for (char c : cadena) {
		if (c == '+') {
			suma += num;
			num = 0;
		}
		else {
			num = num * 10 + (c - '0');
		}
	}
	suma += num;
	return suma;
}

// Función para invertir el primer nombre de una cadena que contiene el nombre completo
string invertir_nombre(string nombre) {
	string invertido = "";
	int i = 0;
	// Saltar los espacios iniciales
	while (nombre[i] == ' ') {
		i++;
	}
	// Copiar el primer nombre al revés
	int j = i;
	while (nombre[j] != ' ' && j < nombre.length()) {
		j++;
	}
	for (int k = j - 1; k >= i; k--) {
		invertido += nombre[k];
	}
	// Copiar el resto del nombre
	invertido += nombre.substr(j);
	return invertido;
}

// Función para ofuscar una cadena según la tabla dada
string ofuscar_cadena(string cadena) {
	string ofuscado = "";
	for (char c : cadena) {
		switch (c) {
		case 'A':
		case 'a':
			ofuscado += '4';
			break;
		case 'E':
		case 'e':
			ofuscado += '3';
			break;
		case 'I':
		case 'i':
			ofuscado += '1';
			break;
		case 'O':
		case 'o':
			ofuscado += '0';
			break;
		case 'S':
		case 's':
			ofuscado += '5';
			break;
		case 'T':
		case 't':
			ofuscado += '7';
			break;
		default:
			ofuscado += c;
			break;
		}
	}
	return ofuscado;
}

// Función para mostrar el menú principal y devolver la opción elegida
int menu_principal() {
	int opcion = 0;
	cout << "Menú principal\n";
	cout << "1) Suma de cadena\n";
	cout << "2) Nombre completo\n";
	cout << "3) String ofuscar\n";
	cout << "4) Salir\n";
	cout << "Elige una opción: ";
	cin >> opcion;
	return opcion;
}

// Función para mostrar el submenú de la opción 1 y devolver la cadena a sumar
string submenu_1() {
	string cadena = "";
	cout << "Submenú de la opción 1\n";
	cout << "Ingresa una cadena de números separados por \"+\": ";
	cin >> cadena;
	return cadena;
}

// Función para mostrar el submenú de la opción 2 y devolver el nombre completo
string submenu_2() {
	string nombre = "";
	cout << "Submenú de la opción 2\n";
	cout << "Ingresa un nombre completo: ";
	cin.ignore();
	getline(cin, nombre);
	return nombre;
}

// Función para mostrar el submenú de la opción 3 y devolver la cadena a ofuscar
string submenu_3() {
	string cadena = "";
	cout << "Submenú de la opción 3\n";
	cout << "Ingresa una cadena a ofuscar: ";
	cin >> cadena;
	return cadena;
}

// Función principal que ejecuta el programa
int main() {
	int opcion = 0;
	string cadena = "";
	string nombre = "";
	string ofuscado = "";
	int suma = 0;
	bool salir = false;
	while (!salir) {
		opcion = menu_principal();
		switch (opcion) {
		case 1:
			cadena = submenu_1();
			suma = suma_cadena(cadena);
			cout << "La suma de la cadena " << cadena << " es " << suma << "\n";
			break;
		case 2:
			nombre = submenu_2();
			nombre = invertir_nombre(nombre);
			cout << "El nombre invertido es " << nombre << "\n";
			break;
		case 3:
			cadena = submenu_3();
			ofuscado = ofuscar_cadena(cadena);
			cout << "La cadena ofuscada es " << ofuscado << "\n";
			break;
		case 4:
			salir = true;
			cout << "Gracias por usar el programa. Adiós.\n";
			break;
		default:
			cout << "Opción inválida. Inténtalo de nuevo.\n";
			break;
		}
	}

}
