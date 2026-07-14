namespace ejercicio_01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[,] m1Matriz = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };//inicializacion  de un arreglo
            int suma = SumaDePares(m1Matriz);
            MessageBox.Show("La suma de los elementos pares es: " + suma);   // mostramos en una ventana emergente 

        }

        public static int SumaDePares(int[,] matriz)
        {
            int suma = 0;
            for (int i = 0; i < matriz.GetLength(0); i++)
            {
                for (int j = 0; j < matriz.GetLength(1); j++)
                {
                    if (matriz[i, j] % 2 == 0)
                    {
                        suma += matriz[i, j];
                    }
                }
            }
            return suma;
        }
    }
}
