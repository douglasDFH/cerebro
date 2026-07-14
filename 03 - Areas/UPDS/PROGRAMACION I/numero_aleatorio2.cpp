#include <iostream>
#include <random>


using namespace std;
//funcion para genrear un numero aleatorio entre 0 y 99
int generar_numaleatorio(){
	random_device rd; //dispositivo para obtener una semilla aleatoria
	mt19937 gen(rd());// motor de generador mersenne twister
	uniform_int_distribution<> distrib(0, 99);
	
	return distrib(gen);
	
	
	
}
	int main(int argc, char *argv[]) {
		int N, numeroAleatorio;
		//generar N numeros aleatorios
		cout<<"Ingrese la cantidad de numeros aleatorios a generar: ";
		cin>> N;
		for (int i=0; i<N; i++) {
			numeroAleatorio=generar_numaleatorio();
			cout << numeroAleatorio<< " ";
		}
		return 0;
	}

	
	
