#include <iostream>
#include <string>
using namespace std;

int main (int argc, char *argv[]){
	string cadena1, cadena2, universidad;
	cadena1="bienvenido a ";
	cadena2=" universidad ";
	cout<<"ingrese el nombre de la universidad:";
	cin>>universidad;
	
	cout<<cadena1<<"la"<<cadena2<<" "<<universidad<<endl;
	return 0;
}
