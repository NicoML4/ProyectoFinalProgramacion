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
            Ajustes Ajustes = new Ajustes();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("1. Reproducir Música");
                Console.WriteLine("2. Detener Música");
                Console.WriteLine("3. Reiniciar Partida");
                Console.WriteLine("4. Desbloquear Todo");
                Console.WriteLine("5. Salir del Menú de Opciones");
                Console.Write("Introduce una opción: ");
                int opcion;
                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                    {
                        case 1:
                            ReproducirMusica();
                            break;
                        case 2:
                            DetenerMusica();
                            break;
                        case 3:
                            ReiniciarPartida();
                            break;
                        case 4:
                            DesbloquearTodo();
                            break;
                        case 5:
                            salir = true;
                            break;
                        default:
                            Console.WriteLine("Opción no válida. Presiona cualquier tecla para continuar...");
                            Console.ReadKey();
                            break;
                    }
                
            }
        }

        public void ReiniciarPartida()
        {
            // Aquí puedes implementar la lógica para reiniciar la partida
        }

        public void DesbloquearTodo()
        {
            // Aquí puedes implementar la lógica para desbloquear todos los Pokémon
        }
    }
}
