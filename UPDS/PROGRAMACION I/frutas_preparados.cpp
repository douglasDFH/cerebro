#include <iostream>
using namespace std;

string frutas[] ={"manzana","babana","naranja","fresa","kiwi","mango","pera"};
string preparados[]={"jugo","frape","ensalada","tarta","helado","mermelada ","batido","asado";"coctel","crudo"};
string fruta[5];
string preparado[5];

string generarfruta(){
	return frutas[rand()%10];
}
string generarPreparado(){
	return preparados[rand()%10]
}


	
	


int main(int argc, char *argv[]) {
	
	return 0;
}

