#include <iostream>
using namespace std;

// Función para multiplicar dos números
double multiplicar(double a, double b) {
	return a * b;
}

// Función para dividir dos números
double dividir(double a, double b) {
	if (b == 0) {
		cout << "Error: no se puede dividir por cero." << endl;
		return 0;
	}
	return a / b;
}

// Función para multiplicar tres números
double multiplicar(double a, double b, double c) {
	return a * b * c;
}

// Función para obtener el mayor de tres números
double mayor(double a, double b, double c) {
	if (a > b && a > c) {
		return a;
	}
	if (b > a && b > c) {
		return b;
	}
	return c;
}

// Función para mostrar el menú y obtener la opción del usuario
char menu() {
	char opcion;
	cout << "MENÚ TP01" << endl;
	cout << "Alumno: Douglas Flor Hernandez" << endl;
	cout << "(1) Multiplicación con 2 números" << endl;
	cout << "(2) División con 2 números" << endl;
	cout << "(3) Multiplicación con 3 números" << endl;
	cout << "(4) El mayor de 3 números" << endl;
	cout << "(s) Salir" << endl;
	cout << "Ingrese la opción que desea realizar: ";
	cin >> opcion;
	return opcion;
}

// Función principal del programa
int main() {
	char opcion;
	double a, b, c, resultado;
	do {
		// Mostrar el menú y obtener la opción del usuario
		opcion = menu();
		// Ejecutar la opción elegida
		switch (opcion) {
		case '1':
			// Pedir al usuario que ingrese dos números
			cout << "Ingrese el primer número: ";
			cin >> a;
			cout << "Ingrese el segundo número: ";
			cin >> b;
			// Llamar a la función multiplicar y guardar el resultado
			resultado = multiplicar(a, b);
			// Mostrar el resultado
			cout << "El resultado de la multiplicación es: " << resultado << endl;
			break;
		case '2':
			// Pedir al usuario que ingrese dos números
			cout << "Ingrese el primer número: ";
			cin >> a;
			cout << "Ingrese el segundo número: ";
			cin >> b;
			// Llamar a la función dividir y guardar el resultado
			resultado = dividir(a, b);
			// Mostrar el resultado
			cout << "El resultado de la división es: " << resultado << endl;
			break;
		case '3':
			// Pedir al usuario que ingrese tres números
			cout << "Ingrese el primer número: ";
			cin >> a;
			cout << "Ingrese el segundo número: ";
			cin >> b;
			cout << "Ingrese el tercer número: ";
			cin >> c;
			// Llamar a la función multiplicar y guardar el resultado
			resultado = multiplicar(a, b, c);
			// Mostrar el resultado
			cout << "El resultado de la multiplicación es: " << resultado << endl;
			break;
		case '4':
			// Pedir al usuario que ingrese tres números
			cout << "Ingrese el primer número: ";
			cin >> a;
			cout << "Ingrese el segundo número: ";
			cin >> b;
			cout << "Ingrese el tercer número: ";
			cin >> c;
			// Llamar a la función mayor y guardar el resultado
			resultado = mayor(a, b, c);
			// Mostrar el resultado
			cout << "El mayor de los tres números es: " << resultado << endl;
			break;
		case 's':
			// Salir del programa
			cout << "Gracias por usar el menú. Hasta pronto." << endl;
			break;
		default:
			// Mostrar un mensaje de error si la opción no es válida
			cout << "Error: opción inválida. Intente de nuevo." << endl;
			break;
		}
	} while (opcion != 's'); // Repetir el menú hasta que el usuario elija salir
	return 0;
}
