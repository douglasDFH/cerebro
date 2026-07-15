#include <iostream>

using namespace std;


	double multiplicar(double n0,double n1)
	{
		return n0*n1;
	}
		double dividir(double n0,double n1)
		{
			if (n1==0)
			{
				cout<<"error: no se puede dividir por cero."<<endl;
				return 0;
			}
			return n0/n1;
		}
		char menu()
		{
			char opcion;
			cout<<"menu de opciones "<<endl;
			cout<<"(1) multiplicar dos numeros "<<endl;
			cout<<"(2) dividir dos numeros "<<endl;
			cout<<"(s) salir "<<endl;
			cout<<"ingrese la opcion que desea realizar "<<endl;
			cin>>opcion;
			return opcion;
		}
int main(int argc, char *argv[])
{
     long int opcion;
	double n0, n1 , resul;
	do{
		opcion=menu();
		switch (opcion){
		case '1':
			cout<<"ingrese el primer numero:"; cin>>n0;
			cout<<"ingrese el segundo numero:"; cin>>n1;
			 resul =multiplicar(n0,n1);
			cout<<"el resultado de la multiplicacion es:"<<resul<<endl;
			break;
			case'2':
				cout<<"ingrese el primer numero:"; cin>>n0;
				cout<<"ingrese el segundo numero:"; cin>>n1;
				resul=dividir(n0,n1);
				cout<<"el resultado de la diviccion es:"<<resul<<endl;
				break;
				case's':
					cout<<"gracias por usar el menu . hasta pronto"<<endl;
					break;
		default:
			cout<<"error: opcion invalida intente de nuevo"<<endl;
			break;
				
			
		};
	} while(opcion !='s');
    



	return 0;
 }

