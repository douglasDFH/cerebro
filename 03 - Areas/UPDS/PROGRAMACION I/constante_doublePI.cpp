#include <iostream>
using namespace std ;
//efine PI 31416;// define una constante llamada pi

int main (int arg, char *argv[]){
	const float PI = 3.1416; //definimos una constante llamadda PI
	double diametro, circunferencia;
	circunferencia=40;
		diametro=circunferencia/PI;
	cout<<"el diametro de la circunferencia =40: "<< diametro;//endl;
	//PI=0; // error por asignacion a una variable de solo lectura
	
	return 0;
}
