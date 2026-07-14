namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* int[] lista; //declaracion de un arreglo de una dimension
             lista = new int[4]; // creacion del arreglo liso de 4 elemento*/

            int[] lista = new int[4]; // inicializando el arreglo lista 

            double x;  // declarando x 
            x = 23.56;   // asignando 23.56 a x

            Double x = 23.56; //inicializando x

            int[,] tabla; //declarando un arreglo de 2 dimenciones 
            tabla = new int[2, 3]; // creando un arreglo tabla que tiene 2 filas y 2 columnas 


            int[] vector = { 4, 6, 8, 10, 12 };  //creacion de un arrerglo con valores 
            int[] matriz = { { 2, 3 }, { 4, 5 }, { 6, 7 } }; // creacion de una tabla con arreglos 

            



        }
    }
}
