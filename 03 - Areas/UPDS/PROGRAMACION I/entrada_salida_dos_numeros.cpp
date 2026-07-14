/*
escrine un progtrama que lea de la entrada estandar dos numero y muestre en la salida estandar 
la suma, resta ,multiplicacion y divicion 
*/
#include <iostream>
using namespace std;

int main(int argc, char *argv[]) {
	int n1, n2 ,suma=0, resta=0, multiplicacion=0, divicion=0; // declaramos la variables de numero enteros n1,n1,suma la inicializamos en 0 igual la resta ,multiplicacion y divicion
	
	cout<<"digite un numero: ";//pedimos al usuario que digite el primer numero
	cin>>n1; // guardamos el dato en n1
	cout<<"digite otro numero:";// pedimos al usuario que digite el segundo numero
	cin>>n2;// guardamos el dato en n2
     // resolviendo las operaciones
	suma = n1+n2;  //realizamos la suma logica de la variable n1,n2
	resta = n1-n2; //realizamos la resta logica de la variable n1,n2
	multiplicacion = n1*n2;//realizamos la multiplicacion logica de la variable n1,n2
	divicion = n1/n2;//realizamos la divicion logica de la variable n1,n2
	
	cout<<"la suma de los 2 numero es:"<<suma<<endl; // imprimiendo resultados de la variable suma 
	cout<<"la resta de los 2 numero es:"<<resta<<endl; // imprimiendo resultados de la variable resta
	cout<<"la multiplicacion de los 2 numero es:"<<multiplicacion<<endl; // imprimiendo resultados de la variable multiplicacion
	cout<<"la divicion de los 2 numero es:"<<divicion<<endl; // imprimiendo resultados de la variable divicion
	return 0;
}

    
