#include <iostream>
#include <random>
#include <limits>// 
using namespace std;
//declaracion de variables globañles
int n1,n2;
// desclaracion de funcions

	// funcion sumarnumeros sin parametros

int sumar(){
	int suma;
	suma=n1 + n2;
	return suma;
		 
		
	}
void solicitarnumeros(){
	cout<<"ingrese el primer numero:";
	cin>>n1;
	cout<<"ingrese el primer numero:";
	cin>>n2;
}

int main(int argc, char *argv[]) {
	int resultado;
	// solicitar 2 numeros y despues mostrar el resultado 
	solicitarnumeros();
	resultado = sumar();
	cout <<"ingrese el primer valo :"<<resultado<<endl;
	solicitarnumeros();
	cout <<"ingrese el segundo  valor :"<<resultado<<endl;
	
	return 0;
}

//modificar la funcion de sumar para que no haya parametros
