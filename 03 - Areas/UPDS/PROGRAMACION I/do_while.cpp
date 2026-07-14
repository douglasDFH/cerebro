#include <iostream>
using namespace std;

int main(int argc, char *argv[]) {
	int opcion;
	opcion=1;
	
	do
	{
		cout<<"menu principal"<<endl;
		cout<<"1.- opcion uno "<<endl;
		cout<<"2.-opcion dos"<<endl;
		cout<<"0.-salir "<<endl;
		cin>>opcion;
		switch(opcion){
		case 1:cout<<"eligio la opcion 1"<<endl;
		break;
		case 2:cout<<"eligio la opcion 2"<<endl;
		break;
		case 0:cout<<"eloigio la opcion salir"<<endl;
		break;
		default:cout<<"no escojio una opcion correcta"<<endl;
		
		};
	
	}while(opcion !=0);
	
	return 0;
}

