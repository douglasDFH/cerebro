#include <iostream>
using namespace std;
// variables goblales declaracion
int n1,n2;
int numero;
//funciones suma y multiplicacion
int sumar(){
	return n1+n2;
}
int multiplicar()
{
	return n1*n2;
}
	bool espar(int numero){
		return numero%2==0;
			
	}

	void ingresar (){
		cout<<"ingresar el primer numero:";cin >>n1;  //llamamos el primer numero y luego imprimimos 
		cout<<"ingresar el segundo numero:";cin >>n2;//llamamos al segundo numero y luego imprimimos 
		
	}
int main(int argc, char *argv[]) {
	ingresar();  //llamamos las funciones 
	cout<<"suma de los numeros es : "<<sumar()<<endl;    //nos devuelve el resultado de la funcion suma 
	cout<<"multiplicacion de los numeros es : "<<multiplicar()<<endl;  // nos devuelve el resultado de la funcion muntiplicar
	if(espar(n1))
	cout<<"numero&&"<<n1<<"&&es par"<<endl;
	else
		cout<<"numero\n"<<n1<<"&&es impar"<<endl;
	return 0;
}

//realizar una funcion para recibir un parametro y determinar si el parametro es par impar si e spar devuelve true si no devuelve false
