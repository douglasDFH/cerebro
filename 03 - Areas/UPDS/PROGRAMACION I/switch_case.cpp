#include <iostream>
#include<cctype>
using namespace std;

int main(int argc, char *argv[]) {
char caracter;
cout<<"ingrese una letra :"; cin>> caracter;
//buscar la funcionpara convertir a mayusculas
caracter = tolower(caracter);
switch(caracter)
{
case 'a': 
case 'e':
case 'i': 
case 'o': 
case 'u': cout<< "es una vocal";
	break;
default: cout<<"no es una vocal";
};
	
	return 0;
}

