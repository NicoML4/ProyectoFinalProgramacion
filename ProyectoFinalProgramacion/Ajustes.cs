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
            string[] opciones = {
            "Reproducir música",
            "Detener música",
            "Reiniciar partida",
            "Desbloquear todos los Pokémon",
            "Volver al menú principal"
        };

            int opcionSeleccionada = 0;
            bool continuar = true;

            Console.CursorVisible = false;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== AJUSTES ===");
                for (int i = 0; i < opciones.Length; i++)
                {
                    if (i == opcionSeleccionada)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"{(i + 1)}. {opciones[i]}");

                    Console.ResetColor();
                }

                ConsoleKeyInfo tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.UpArrow:
                        opcionSeleccionada = (opcionSeleccionada - 1 + opciones.Length) % opciones.Length;
                        break;

                    case ConsoleKey.DownArrow:
                        opcionSeleccionada = (opcionSeleccionada + 1) % opciones.Length;
                        break;

                    case ConsoleKey.Enter:
                        EjecutarOpcion(opcionSeleccionada);
                        if (opcionSeleccionada == 4) 
                            continuar = false;
                        break;
                }
            }
        }

        private void EjecutarOpcion(int opcion)
        {
            bool salir = true;
            Console.Clear();
            while (!salir)
            {
                switch (opcion)
                {
                    case 0:
                        ReproducirMusica();
                        break;
                    case 1:
                        DetenerMusica();
                        break;
                    case 2:
                        ReiniciarPartida();
                        break;
                    case 3:
                        DesbloquearTodo();
                        break;
                    case 4:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Presiona cualquier tecla para continuar...");
                        Console.ReadKey();
                        salir = true;
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
