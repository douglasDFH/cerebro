#include <iostream>

using namespace std;
// declaracion de variables globales
int num1,num2; // declaracion de variables para las operaciones con numeros
string cadena1, cadena2 ; // declaracion de variables para las cadenas y concatenar cadenas
// funciones a utilizar las declaramos fuera de nuestro programa principal
void ingresarNumeros();
void sumarNumeros();
void dividirNumeros();
void concatenar();
void mostrarMenuPrincipal();
void mostrarSubmenuNumeros();
void opcionprincipal(char opcion);
void opcionSubmenuNumeros(char opcion);

int main(int argc, char *argv[]) {
	char opcion;  // declaramos la variable para guardar la opcion que vamos a elegir
	mostrarMenuPrincipal();  // mostramo el menu principal por pantalla y pedimos la opcion
	cin>>opcion;
	while(opcion=='1'||opcion=='2'){  //ejecutamos la opcion con un while para que sea validada 
		opcionprincipal(opcion);
		mostrarMenuPrincipal();// mostramos el menu y pedimos la opcion nuevamente
		cin>>opcion;
		
	}
	cout<<"\ngracias por usar el programa"<<endl; //terminamos el programa 
	return 0;
}	


//creamos la funcion ingresarnumeros para pedir al usuario y alamcenar a la variables globales 
void ingresarNumeros (){
	cout<<"\ningrese el primer numero :"; //realizamos un salto de linea y pedimos al usuario el primer numero 
	cin>>num1;                            //guardamos el primer numero ingresado 
	cout<<"ingrese el segundo numero :"; //pedimos el segundo numero
	cin>>num2;                           // guardamos el segundo numero
	cout<<"**********************************"<<endl; 
	cout<<"* se ingreso los numeros: "<<num1<<" y "<<num2<<endl; // mostramos por pantalla los 2 numero ingresados
	cout<<"**********************************"<<endl<<endl;
}
	//funcion que sumara los 2 numeros globales 
void sumarNumeros(){
		//variavle para guardar la suma
	int sumar = num1+num2;
	cout<<"\nla suma de los numeros ingresados es :"<<sumar<<endl<<endl;//mostramos la suma de los 2 numero ingresdos 
		
	}
		//funcion que va a dividir los dos numeros de la variable globaales
void dividirNumeros (){
	double division; // variable de dividir 
	if(num2 !=0){    // condicional si el divisor es cero
		division=(double)num1/num2; //calculamos la divicion
		cout<<"\nla division de los numeros ingresados es :"<<division<<endl<<endl;//mostramos el calculo de la division 
	}
	else{
		cout<<"no se puede dividir por cero."<<endl;// si no cumple mostramos por pantalla un mensaje
		}
	}
	//funcion que va concatenar las cadenas de las variables globales y va mostrar el resultado
void concatenar (){
	string concatenacion; // declaramos la variable para guardar la concatenacion
	cout<<"\ningrese la primer cadena :";cin>>cadena1; // pedimos al usuario que ingrese la primer cadena1
	cout<<"ingrese la segunda  cadena :";cin>>cadena2;// pedimos al usuario que ingrese la segunda cadena2
	concatenacion = cadena1 + cadena2;       // concatenamos usando el operador suma
	cout<<"\nla concatenacion es : "<<concatenacion<<endl;
	}
//funcion que mostrara el menu principal 
void mostrarMenuPrincipal (){
	//mostramos las opciones disponible del menu principal
	// igual realizamos un salto de linea al comienzo para cuaando se ejecute no este muy apegado las opciones
	cout<<"\nBIENVENIDOS AL MENU PRINCIPAL"<<endl;
	cout<<"1)OPERACIONES CON NUMEROS"<<endl;
	cout<<"2)CONCATENAR 2 CADENAS"<<endl;
	cout<<"(s/S)SALIR"<<endl;
	cout<<"selecione una opcion : ";
	
}
	//funcion que mostrara el submenu al usuario 
void mostrarSubmenuNumeros (){
	// mostramos las opciones disponible del mostrarSubmenuNumeros
	cout<<"BIENVENIDOS AL SUBMENU DE"<<endl;
	cout<<"OPERACIONES CON NUMEROS"<<endl;
	cout<<"1)SUMA DE 2 NUMEROS"<<endl;
	cout<<"2)DIVIDIR 2 NUMEROS"<<endl;
	cout<<"(m/M)VOLVER AL MENU PRINCIPAL"<<endl;
	cout<<"selecione la operacion a realizar : ";
}
		
	//funcion que ejecutara la opcion elegida por el usuario ene l menu principal
void opcionprincipal(char opcion){
	char opcion2; //declaramos una variable para guardar las opciones de l submenu
	switch(opcion){ //usamos el switch para la opcion correspondiente
	case '1':
		ingresarNumeros();// llamamos a la funcion los numeros
		mostrarSubmenuNumeros(); // mostramos el submenu 
		cin>>opcion2;
		while(opcion2=='1'||opcion2 =='2'){// ejecutamos la opcion mientras sea valido
			opcionSubmenuNumeros(opcion2);
			mostrarSubmenuNumeros();  // Mostramos el submenu de opciones con números y pedimos la opción nuevamente
			cin>>opcion2;
		}
		break;
	case'2': 
		concatenar(); // Llamamos a la función para concatenar las cadenas
		break;
	default:
	    cout<<"opcion invalida."<<endl;// Mostramos un mensaje de error
			
	}
	
}
	// Función que ejecuta la opción elegida por el usuario en el submenu de opciones con números
void opcionSubmenuNumeros (char opcion){
	switch(opcion){  // Usamos un switch para ejecutar la opción correspondiente
		case'1':
			sumarNumeros(); // Llamamos a la función para sumar los números
			break;
	    case '2':
			dividirNumeros();// Llamamos a la función para dividir los números
			break;
	default:
		cout<<"opcion invalida"<<endl;// Mostramos un mensaje de error
	}
	
}


