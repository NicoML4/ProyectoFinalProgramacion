using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class ReproductorMusica
    {
        Process procesoMusica;
        readonly string RutaPredeterminada;
        readonly string RutaCombate;
        bool EstarReproduciendo = false;

        public ReproductorMusica()
        {
            // Construimos la ruta relativa a partir de la ruta de ejecucuion del proyecto
            RutaPredeterminada = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Musica", "musicafondo.mp3"));
            RutaCombate = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Musica", "musicacombate.mp3"));

            if (!File.Exists(RutaPredeterminada))
            {
                Console.WriteLine($"Ruta inválida: {RutaPredeterminada}");
            }
        }

        public void Reproducir()
        {
            Detener();

            if (File.Exists(RutaPredeterminada))
            {
                procesoMusica = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "wmplayer.exe",
                        Arguments = $"\"{RutaPredeterminada}\"",
                        UseShellExecute = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                procesoMusica.Start();
                EstarReproduciendo = true;
            }
        }
        public void Reproducircombate()
        {
            if ((procesoMusica == null || procesoMusica.HasExited) && File.Exists(RutaCombate))
            {
                procesoMusica = new Process();
                procesoMusica.StartInfo.FileName = "wmplayer.exe";
                procesoMusica.StartInfo.Arguments = $"\"{RutaCombate}\"";
                procesoMusica.StartInfo.UseShellExecute = true;
                procesoMusica.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procesoMusica.Start();
                EstarReproduciendo = true;
            }
        }

        public void Detener()
        {
            if (procesoMusica != null)
            {
                try
                {
                    if (!procesoMusica.HasExited)
                    {
                        procesoMusica.Kill();
                    }

                    procesoMusica.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" Error al detener la musica: {ex.Message}");
                }
                finally //codigo que se ejecutara independientemente de lo anterior
                {
                    procesoMusica = null;
                    EstarReproduciendo = false;
                }
            }
        }

        public bool GetEstadoReproduccion()
        {
            return EstarReproduciendo;
        }

        public void SetEstadoReproduccion(bool estado)
        {
            EstarReproduciendo = estado;
        }
        public string Tostring()
        {
            return EstarReproduciendo ? "Reproduciendo" : "Detenido";
        }
    }
}
