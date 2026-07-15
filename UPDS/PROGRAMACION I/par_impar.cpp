#include <iostream>
using namespace std;

int main (int arg, char *argv[]){
	
	int numero;
	cout<<"ingrese numero";
	cin>>numero;
	if (numero%2)
		cout<<"el numero:"<<numero<<"es impar"<<endl;
	else{
		cout<<"el numero:"<<numero<<"es par"<<endl;
	}
	return 0;
}
