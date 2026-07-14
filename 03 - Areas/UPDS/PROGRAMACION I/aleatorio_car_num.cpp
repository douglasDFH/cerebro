#include <iostream>
using namespace std;
char caracter [27]= {'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','k','r','s','t','u','v','w','x','y','z'};//completar hasta la Z
//stringletras="abcdefgh";
int numero[27];
char c[27];
bool usado[27]{false};//ciclo para marcar los caracteres usados
//utilizar funciones
char generarCaracter()
{//generamos caracter de forma aleatoria
	int indice=rand()%27;
	char c=caracter[indice];
	while(usado[indice])
	{   //verificamos el caracter ya fue usado
		indice=rand()%28;  // si fue usado genera otro caracter
		c=caracter[indice];
	}
	usado[indice]=true;   // marca el caracter como usado
	    return c; //devuelve el caracter
		
}
int generarNumero(){

	   return rand()%51;  //no vas a dar numero de 0 a 51 funcion randonica 
}
void imprimir ()
{
	  
	cout<<"CAR\tNUM"<<endl;// tabulacion 
	for(int i=0;i<27;i++)
		cout<<c[i]<<"\t"<<numero[i]<<endl;
}
void generarDatos()
{
	for(int i=0;i<27;i++)
	{
		numero[i]=generarNumero();
		c[i]=generarCaracter();
	}
}
int main(int argc, char *argv[]) {
	// convertir en una funcion para generar los datos 
	
	generarDatos ();
	imprimir();
	
	
	return 0;
}

