using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CASO_de_ESTUDIO_3_ARCHIVOS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            // Abre un cuadro de diálogo para seleccionar un archivo
            using (var openFileDialog = new OpenFileDialog())
            {
                // Verifica si se ha seleccionado un archivo y se ha presionado el botón "OK"
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Leer el contenido del archivo1
                    var lines = File.ReadAllLines(openFileDialog.FileName);
                    // Agrega cada línea del archivo al ListBox1
                    foreach (var line in lines)
                    {
                        listBox1.Items.Add(line);
                    }
                }
            }

            // Abre un cuadro de diálogo para guardar un archivo
            using (var saveFileDialog = new SaveFileDialog())
            {
                // Verifica si se ha seleccionado un archivo y se ha presionado el botón "OK"
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Escribir en el archivo2 solo las líneas que contienen la palabra
                    // Convertir la palabra buscada a minúsculas
                    var palabraBuscada = PalBus.Text.ToLower();
                    // Iterar a través de cada ítem en ListBox1
                    foreach (var item in listBox1.Items)
                    {
                        // Convertir la línea actual a minúsculas
                        var line = item.ToString().ToLower();
                        // Verificar si la línea contiene la palabra buscada
                        if (line.Contains(palabraBuscada))
                        {
                            // Agregar la línea al ListBox2
                            listBox2.Items.Add(item);
                            // Escribir la línea en el archivo2 y añadir un salto de línea
                            File.AppendAllText(saveFileDialog.FileName, item.ToString() + Environment.NewLine);
                        }
                    }
                }
            }
        }
    }
}
