
#include <iostream>
using namespace std;

int main(int argc, char *argv[]){
	string Lyu;
	Lyu = "I love you c++";
	cout << "La cadena es :"+ Lyu<< endl; // concatenando una cadena con la variable
	cout << "la longitud de la cadena es :" <<Lyu.length()<<endl;
	//si el valor de retorno es 0, significa que la cadena no esta vacia
	cout <<"¿esta! vacio :"<<Lyu.empty()<<endl<<endl;
	Lyu.append("!");        //agregamos una despues de la cadena
	cout <<"la cadena modificada es :" <<Lyu <<endl<<endl;
	string Lyu_2;      // intercambio de datos de cadenas
	Lyu_2= "I love too";
	Lyu.swap(Lyu_2);
	cout <<"cambie la cadena modificafda como:"<<Lyu<<endl<<endl;
	
	int site;
	site=Lyu.find('l',0); //encuentra la posicion ddonde aparece  l desde 0 y regresa
	cout <<"la posiscion dende aparece 1 en la cadena es :"<< site<<endl;
	site=Lyu.find('oo',0);  // encuentra la posicion donde aparece oo desde 0 y regresa la cadena
	cout <<"la posiscion dende aparece oo en la cadena es :"<< site<<endl;
	site =0;    //recorrer y buscar todas las posiciones 0
	do{
		site = Lyu.find('o',site);
		if(site==-1)
			cout <<"busqueda completada, no hay otros elementos:"<<endl;
		else
			cout <<"la posicion donde aparece o en la cadena es :"<<site<<endl;
		site++;
	} while(site!=0);
	return 0;
}
// a -4
// e -3
//i -1
//o -0
//u-5
