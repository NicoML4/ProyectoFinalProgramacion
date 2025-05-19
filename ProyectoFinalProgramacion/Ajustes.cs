using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Ajustes
    {
        public ReproductorMusica reproductor;
        

        public Ajustes(ReproductorMusica reproductor)
        {
            this.reproductor = reproductor;
        }

        public Ajustes()
        {
            this.reproductor = new ReproductorMusica();
        }

        public void ReproducirMusica()
        {
            reproductor.Reproducir();
        }

        public void DetenerMusica()
        {
            reproductor.Detener();
        }
        public void ReiniciarPartida(Usuario usuarioLogeado)
        {
            File.WriteAllText($"../../../Usuarios/{usuarioLogeado.NombreUsuario}.txt","");
        }
        public void DesbloquearTodo()
        {
            // Desbloqueamos todo
        }
    }
}
