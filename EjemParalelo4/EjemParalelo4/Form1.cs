using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemParalelo4
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
            // Crear una lista de URLs de archivos a descargar de manera asíncrona
            List<string> urls = new List<string>
            {
                "https://example.com/file1.txt",
                "https://example.com/file2.txt",
                "https://example.com/file3.txt"
            };

            // Utilizar async/await para realizar operaciones asíncronas
            await DescargarArchivosAsync(urls);

            MessageBox.Show("Todas las descargas han sido completadas de manera asíncrona.");
        }

        private async Task DescargarArchivosAsync(List<string> urls)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                // Primer bloque Task.WhenAll
                await Task.WhenAll(urls.Select(async url =>
                {
                    try
                    {
                        string contenido = await httpClient.GetStringAsync(url);
                        ActualizarInterfazDeUsuario($"Descarga completada desde {url}");
                    }
                    catch (Exception ex)
                    {
                        ActualizarInterfazDeUsuario($"Error al descargar desde {url}: {ex.Message}");
                    }
                }));

                // Segundo bloque Task.WhenAll
                await Task.WhenAll(urls.Select(async url =>
                {
                    try
                    {
                        string contenido = await httpClient.GetStringAsync(url);
                        ActualizarInterfazDeUsuario($"Descarga completada desde {url}");
                    }
                    catch (Exception ex)
                    {
                        ActualizarInterfazDeUsuario($"Error al descargar desde {url}: {ex.Message}");
                    }
                }));
            }
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
