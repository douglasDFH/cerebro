#include <iostream>
#include <string>
#include <cstdlib>
using namespace std;

// Función para sumar los caracteres numéricos de una cadena
int suma_cadena (string cadena) {
	int suma = 0;
	for (int i = 0; i < cadena.length (); i++) {
		suma += atoi (cadena.substr (i, 1).c_str ());
	}
	return suma;
}

// Función para invertir el orden del nombre y el apellido
string nombre_completo (string nombre) {
	int espacio = nombre.find (" ");
	string apellido = nombre.substr (0, espacio);
	string resto = nombre.substr (espacio + 1);
	return resto + " " + apellido;
}

// Función para ofuscar una cadena reemplazando letras por números
string ofuscar (string cadena) {
	cadena.replace (cadena.find ("A"), 1, "4");
	cadena.replace (cadena.find ("a"), 1, "4");
	cadena.replace (cadena.find ("E"), 1, "3");
	cadena.replace (cadena.find ("e"), 1, "3");
	cadena.replace (cadena.find ("I"), 1, "1");
	cadena.replace (cadena.find ("i"), 1, "1");
	cadena.replace (cadena.find ("O"), 1, "0");
	cadena.replace (cadena.find ("o"), 1, "0");
	cadena.replace (cadena.find ("S"), 1, "5");
	cadena.replace (cadena.find ("s"), 1, "5");
	cadena.replace (cadena.find ("T"), 1, "7");
	cadena.replace (cadena.find ("t"), 1, "7");
	return cadena;
}

// Función para crear un menú y seleccionar una opción
void menu (string titulo, string opciones [], int n, void (*funciones []) (void)) {
	int opcion;
	bool salir = false;
	while (!salir) {
		cout << "==== " << titulo << " ====" << endl;
		for (int i = 0; i < n; i++) {
			cout << i + 1 << ". " << opciones [i] << endl;
		}
		cout << "0. Salir" << endl;
		cout << "Ingrese una opción: ";
		cin >> opcion;
		if (opcion > 0 && opcion <= n) {
			funciones [opcion - 1] (); // Ejecuta la función correspondiente a la opción
		}
		else if (opcion == 0) {
			salir = true;
		}
		else {
			cout << "Opción inválida. Intente de nuevo." << endl;
		}
	}
}

// Funciones para cada opción del submenú
void opcion1 () {
	string cadena;
	cout << "Ingrese una cadena: ";
	cin >> cadena;
	cout << "La suma de los caracteres numéricos de la cadena es: " << suma_cadena (cadena) << endl;
}

void opcion2 () {
	string nombre;
	cout << "Ingrese un nombre completo: ";
	cin.ignore ();
	getline (cin, nombre);
	cout << "El nombre invertido es: " << nombre_completo (nombre) << endl;
}

void opcion3 () {
	string cadena;
	cout << "Ingrese una cadena: ";
	cin >> cadena;
	cout << "La cadena ofuscada es: " << ofuscar (cadena) << endl;
}

// Función principal
int main () {
	// Crear el submenú con las opciones y las funciones
	string opciones_sub [] = {"Suma de una cadena", "Nombre completo", "Ofuscar"};
	int n_sub = sizeof (opciones_sub) / sizeof (opciones_sub [0]);
	void (*funciones_sub []) (void) = {opcion1, opcion2, opcion3};
	
	// Crear el menú principal con la opción del submenú
	string opciones_main [] = {"Submenú"};
	int n_main = sizeof (opciones_main) / sizeof (opciones_main [0]);
	//void (*funciones_main []) (void) = {[] () {menu ("Submenú", opciones_sub, n_sub, funciones_sub);}}; // Usar una lambda para llamar al submenú//
	
	// Mostrar el menú principal
	menu ("Menú principal", opciones_main, n_main, funciones_sub);
	
	return 0;
}
