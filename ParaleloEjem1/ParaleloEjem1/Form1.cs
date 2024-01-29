using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParaleloEjem1
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

            // Centrar la ventana en la pantalla
            CenterToScreen();
        }

        private async void btnEjecutar_Click(object sender, EventArgs e)
        {
            // Simular una lista de IDs de elementos a consultar en paralelo
            List<int> ids = ObtenerListaIds();

            // Ejecutar consultas en paralelo utilizando TPL
            await EjecutarConsultasParalelas(ids);

            MessageBox.Show("Todas las consultas han sido ejecutadas en paralelo.");
        }

        private Task EjecutarConsultasParalelas(List<int> ids)
        {
            // Utilizar Parallel.ForEach para ejecutar consultas en paralelo
            Parallel.ForEach(ids, id =>
            {
                // Lógica de la consulta (simulación)
                string resultado = RealizarConsultaEnBaseDeDatos(id);

                // Actualizar la UI desde el hilo principal
                ActualizarInterfazDeUsuario(id, resultado);
            });
            return Task.CompletedTask;
        }

        private void ActualizarInterfazDeUsuario(int id, string resultado)
        {
            // Actualizar la interfaz de usuario desde el hilo principal
            Invoke((MethodInvoker)delegate
            {
                // Lógica para actualizar la interfaz de usuario (simulación)
                listBoxResultados.Items.Add($"ID: {id}, Resultado: {resultado}");
            });
        }

        private string RealizarConsultaEnBaseDeDatos(int id)
        {
            // Simular una consulta en base de datos (simulación)
            Task.Delay(1000).Wait(); // Simular una operación que toma tiempo

            return $"Resultado para ID {id}";
        }

        private List<int> ObtenerListaIds()
        {
            // Lógica para obtener una lista de IDs (simulación)
            return Enumerable.Range(1, 10).ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cerrar la aplicación
            Application.Exit();
        }
    }
}
