#include <iostream>
using namespace std;
string palabra ;
void cambiarVocales (char vocal, char numero)
{ int site=0;
	do{
		site = palabra.find(vocal,site);
		if(site != -1)
			palabra[site]=numero;
		site++;
			
	} while(site !=0);
}

int main(int argc, char *argv[]) {
	palabra="el eucalipto esta muy alto";
	cambiarVocales('a','4');
	cambiarVocales('e','3');
	cambiarVocales('i','1');
	cambiarVocales('o','0');
	cambiarVocales('u','5');
	cout<< "Palabra:"<<palabra<<endl;
	return 0;
}

