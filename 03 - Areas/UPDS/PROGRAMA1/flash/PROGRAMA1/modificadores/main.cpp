#include <iostream>
#include <limits>
using namespace std;

int main () {
  /*  signed int n{-234};
    unsigned int y{2666};
    cout << "el numero es: "<< n <<endl;
    cout << "el tamaño es: "<< sizeof(n) << " Byte"<<endl;
    cout << "el numero es: "<< y <<endl;
    cout << "el tamaño es: "<< sizeof(y) << " Byte"<<endl;
    cout << "el numero menor con signed es de " << numeric_limits<signed int>::lowest()
            <<"el numero mayor con signed es de " << numeric_limits<signed int>::max()<<endl;
    cout << "el numero menor con unsigned es de " << numeric_limits<unsigned int>::lowest()
            <<"el numero mayor con unsigned es de " << numeric_limits<unsigned int>::max()<<endl;

short signed int n{-234};
    short unsigned int y{2666};
    cout << "el numero es: "<< n <<endl;
    cout << "el tamaño es: "<< sizeof(n) << " Byte"<<endl;
    cout << "el numero es: "<< y <<endl;
    cout << "el tamaño es: "<< sizeof(y) << " Byte"<<endl;
    cout << "el numero menor con short signed es de " << numeric_limits<short signed int>::lowest()
            <<"el numero mayor con short signed es de " << numeric_limits<short signed int>::max()<<endl;
    cout << "el numero menor con unsigned es de " << numeric_limits<short unsigned int>::lowest()
            <<"el numero mayor con unsigned es de " << numeric_limits<short  unsigned int>::max()<<endl;

*/
    long long signed int n{-234};
    long long unsigned int y{2666};
    cout << "el numero es: "<< n <<endl;
    cout << "el tamaño es: "<< sizeof(n) << " Byte"<<endl;
    cout << "el numero es: "<< y <<endl;
    cout << "el tamaño es: "<< sizeof(y) << " Byte"<<endl;
    cout << "el numero menor con long signed es de " << numeric_limits<long long signed int>::lowest()
            <<"el numero mayor con long signed es de " << numeric_limits<long long signed int>::max()<<endl;
    cout << "el numero menor con long es de " << numeric_limits<long long unsigned int>::lowest()
            <<"el numero mayor con long es de " << numeric_limits<long long  unsigned int>::max()<<endl;

return 0;
}