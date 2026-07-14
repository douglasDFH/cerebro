#include <iostream>
#include <sctdlib>
using namespace std;

int aleatorio(){  // funcion aleatorio 
	return rand() % 100;
}
bool esImpar(int numero){ //realizamos la funcion para generar el ciclo
	return numero %2!=0;
}

int main(int argc, char *argv[]) {
	int n,c=0,num;  //variables n, c, se inicializa en cero, num
	cout<<"generar numero impares,ingrese n :";//pedimos al usuario que ingrese n numero
	cin>>n;
	while(c<n){               //mientras c sea menor a n numero se realiza la sentencia 
		num=aleatorio();
		if(esImpar(num)){   //si es impar los numeros aleatorios 
			c++;
			cout<<"impar["<<c<<"]."<<num<<endl; 
		}
	}
	return 0;
}

