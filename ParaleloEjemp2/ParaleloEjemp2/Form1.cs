using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParaleloEjemp2
{
    public partial class Form1 : Form
    {
        // Declarar listBoxResultados como una variable de instancia
        private ListBox listBoxResultados;

        public Form1()
        {
            InitializeComponent();

            // Inicializar listBoxResultados
            listBoxResultados = new ListBox();
            listBoxResultados.Size = new System.Drawing.Size(580, 250);
            listBoxResultados.Location = new System.Drawing.Point(10, 50);

            // Agregar listBoxResultados al formulario
            this.Controls.Add(listBoxResultados);
            CenterToScreen(); // Centra el formulario en la pantalla
        }

        private async void btnEjecutar_Click(object sender, EventArgs e)
        {
            // Crear una lista de números
            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Utilizar Parallel.ForEach para realizar operaciones en paralelo
            await Task.Run(() =>
            {
                Parallel.ForEach(numeros, numero =>
                {
                    // Simular una operación que toma tiempo
                    RealizarOperacion(numero);
                });
            });

            MessageBox.Show("Todas las operaciones han sido completadas en paralelo.");
        }

        private void RealizarOperacion(int numero)
        {
            // Simular una operación que toma tiempo
            Console.WriteLine($"Procesando el número {numero} en el hilo {Task.CurrentId}");
            Task.Delay(1000).Wait(); // Simulación de una operación que toma tiempo

            // Actualizar la interfaz de usuario desde el hilo principal
            ActualizarInterfazDeUsuario($"Operación completada para el número {numero}");
        }

        private void ActualizarInterfazDeUsuario(string mensaje)
        {
            // Actualizar la interfaz de usuario desde el hilo principal
            Invoke((MethodInvoker)delegate
            {
                // Lógica para actualizar la interfaz de usuario
                listBoxResultados.Items.Add(mensaje);
            });
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
