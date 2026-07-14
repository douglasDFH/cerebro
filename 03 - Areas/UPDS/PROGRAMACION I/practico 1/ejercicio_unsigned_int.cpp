#include <iostream>
using namespace std;

int main(int argc, char *argv[]) {
	unsigned int nn1,nn2;// definiendo variables
    int n1;
	n1=2147483645;// asignando valoes a la variable este valor esta dentro del rango de int
	nn1=n1+3; // se produce una conversion  de  int a unsigne int 
	nn2=0;
	cout<<"n1 :"<<n1<<endl;// mostrando los valores de la variables
	cout<<"nn1 :"<<nn1<<endl;
	cout<<"nn2 :"<<nn2<<endl;
	return 0;
}
