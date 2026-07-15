//parametros de variables enlazados
#include <iostream>
using namespace std;
void menu(int&opcion){
	cout << "*******BIENVENIDOS AL MENU********" << endl;
	cout << "1  ingresar numeros" << endl;
	cout << "2  sumar los numeros: "<< endl;
	cout << "3  verificar i un numero es primo" << endl;
	cout << "0  salir " << endl;
	cin >> opcion;
	
}
void ingresar(int &n1, int &n2) //variable enlazados para n1 y n2
{
	cout<<"ingrese un numero";  // pedimoa al usuario que ingrese un numero 
	cin>>n1;                              // almacenamos en n1
	cout<<"ingrese el segundo numero";// pedimos al usuario que ingrese  el segundo numero
	cin>>n2;
}
void sumar(int n1, int n2, int &resultado)
{
	resultado=n1+n2;
	
}



int main(int argc, char *argv[])
{
	int n1, n2, resultado,opcion;
	do
	{
		menu(opcion);
		switch(opcion)
		{
		case 1 :
			ingresar(n1,n2);
			break;
		case 2 :
			ingresar(n1,n2);
			break;
		case 3 :
			ingresar(n1,n2);
			break;
		case 0 :
		    ingresar(n1,n2);
			break;
			default
		}
	}while(opcion!=0);
	
		ingresar(n1,n2);
		sumar(n1,n2,resultado);
		cout<<"la suma es :"<<resultado<<endl;
	return 0;
  }

//realizar un menu que tenga 3 opciones 
//1 ingresar numeros
//2 sumar los numeros
//3 verificar si es primo
