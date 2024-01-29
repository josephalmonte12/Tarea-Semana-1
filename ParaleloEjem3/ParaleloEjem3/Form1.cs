using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParaleloEjem3
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
            List<int> numeros = Enumerable.Range(1, 10).ToList();

            // Utilizar PLINQ para realizar operaciones en paralelo
            await Task.Run(() =>
            {
                var resultados = numeros
                    .AsParallel()  // Activar PLINQ
                    .WithDegreeOfParallelism(Environment.ProcessorCount)  // Establecer el grado de paralelismo
                    .Select(numero =>
                    {
                        // Simular una operación que toma tiempo
                        RealizarOperacion(numero);
                        return $"Operación completada para el número {numero}";
                    });

                // Actualizar la interfaz de usuario desde el hilo principal
                ActualizarInterfazDeUsuario(resultados);
            });

            MessageBox.Show("Todas las operaciones han sido completadas en paralelo.");
        }

        private void ActualizarInterfazDeUsuario(ParallelQuery<string> mensajes)
        {
            // Actualizar la interfaz de usuario desde el hilo principal
            Invoke((MethodInvoker)delegate
            {
                // Lógica para actualizar la interfaz de usuario
                foreach (var mensaje in mensajes)
                {
                    listBoxResultados.Items.Add(mensaje);
                }
            });
        }

        private void RealizarOperacion(int numero)
        {
            // Simular una operación que toma tiempo
            Console.WriteLine($"Procesando el número {numero} en el hilo {Task.CurrentId}");
            Task.Delay(1000).Wait(); // Simulación de una operación que toma tiempo
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
