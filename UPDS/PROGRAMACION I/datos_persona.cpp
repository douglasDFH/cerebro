/*
programa que lea los datos de entrada estandar los siguiente datos 
de una persona 
  edad: dato de tipo entero
  sexo: dato de tipo caracter
  altura: dato de tipo realloc

*/
#include <iostream>
using namespace std;

int main() {
	int edad;
	char sexo[10];
	float altura;
	
	cout<<"digite su edad: ";
	cin>>edad;
	cout<<"digite su sexo: ";
	cin>>sexo;
	cout<<"digite su altura en metros: ";
	cin>>altura;
	
	cout<<"\n edad:"<<edad<<endl;
	cout<<"sexo:"<<sexo<<endl;
	cout<<" altura en metros:"<<altura<<endl;
	return 0;
}

