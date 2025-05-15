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
        bool EstarReproduciendo = false;

        public ReproductorMusica()
        {
            // Construimos la ruta relativa a partir de la ruta de ejecucuion del proyecto
            RutaPredeterminada = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "..", "Musica", "musicafondo.mp3"));

            if (!File.Exists(RutaPredeterminada))
            {
                Console.WriteLine($"Ruta inválida: {RutaPredeterminada}");
            }
        }

        public void Reproducir()
        {
            if (!EstarReproduciendo && File.Exists(RutaPredeterminada))
            {
                procesoMusica = new Process();
                procesoMusica.StartInfo.FileName = "wmplayer.exe";
                procesoMusica.StartInfo.Arguments = $"\"{RutaPredeterminada}\"";
                procesoMusica.StartInfo.UseShellExecute = true;
                procesoMusica.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                procesoMusica.Start();
                EstarReproduciendo = true;
            }
        }

        public void Detener()
        {
            if (EstarReproduciendo && procesoMusica != null)
            {
                if (!procesoMusica.HasExited)
                {
                    procesoMusica.Kill();
                }
                procesoMusica.Dispose();
                EstarReproduciendo = false;
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
