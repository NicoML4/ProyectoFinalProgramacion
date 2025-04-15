using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public void MostrarMenuAjustes()
        {
            bool continuar = true;
            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("Bienvenido al menú de Ajustes");
                Console.WriteLine("1. Reproducir música");
                Console.WriteLine("2. Detener música");
                Console.WriteLine("3. Reiniciar partida");
                Console.WriteLine("4. Desbloquear todos los Pokémon");
                Console.WriteLine("5. Volver al menú principal");
                Console.Write("Elige una opción: ");

                string opcion = Console.ReadLine();
                bool acabarbucle = true;
                switch (opcion)
                {
                    case "1":
                        ReproducirMusica();
                        break;
                    case "2":
                        DetenerMusica();
                        break;
                    case "3":
                        ReiniciarPartida();
                        break;
                    case "4":
                        DesbloquearTodo();
                        break;
                    case "5":
                        Program.MenuOpciones();
                        acabarbucle = false;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intenta nuevamente.");
                        break;
                }
            }
        }

        public void ReiniciarPartida()
        {
            // Lógica para reiniciar la partida
        }

        public void DesbloquearTodo()
        {
            // Lógica para desbloquear todos los Pokémon
        }
    }
}
