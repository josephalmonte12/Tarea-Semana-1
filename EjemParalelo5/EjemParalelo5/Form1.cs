using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemParalelo5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            CenterToScreen();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void EjecutarTareasEnParalelo()
        {
            Task[] tasks = new Task[]
            {
                Task.Run(() => RealizarTarea("Tarea 1")),
                Task.Run(() => RealizarTarea("Tarea 2")),
                Task.Run(() => RealizarTarea("Tarea 3"))
            };

            Task.WaitAll(tasks);

            MessageBox.Show("Todas las tareas han finalizado.");
        }

        private void RealizarTarea(string tarea)
        {
            // Simular una tarea que toma un tiempo
            System.Threading.Thread.Sleep(2000);

            // Actualizar la interfaz de usuario desde el hilo principal
            BeginInvoke((MethodInvoker)delegate
            {
                LogTextBox.AppendText($"{tarea} completada\n");
            });
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            EjecutarTareasEnParalelo();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
