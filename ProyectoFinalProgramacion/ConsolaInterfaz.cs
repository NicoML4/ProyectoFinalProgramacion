using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    class ConsolaInterfaz
    {
        public static void ColorLetras()
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public static void WriteLineCentr(string texto)
        {
            int anchoConsola = Console.WindowWidth;
            int espaciosIzquierda = (anchoConsola - texto.Length) / 2;
            Console.WriteLine(new string(' ', Math.Max(0, espaciosIzquierda)) + texto);
        }
        public static void WriteCentr(string texto)
        {
            int anchoConsola = Console.WindowWidth;
            int espaciosIzquierda = (anchoConsola - texto.Length) / 2;
            Console.Write(new string(' ', Math.Max(0, espaciosIzquierda)) + texto);
        }

        public static int SeleccionarOpcion(string[] opciones)
        {
            Console.CursorVisible = false;
            int opcionSeleccionada = 0;

            ConsoleKeyInfo tecla;

            do
            {
                Console.Clear();
                int anchoConsola = Console.WindowWidth;

                for (int i = 0; i < opciones.Length; i++)
                {
                    string linea = opciones[i];
                    int espaciosIzquierda = (anchoConsola - linea.Length) / 2;

                    if (i == opcionSeleccionada)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(new string(' ', Math.Max(0, espaciosIzquierda)) + linea);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(new string(' ', Math.Max(0, espaciosIzquierda)) + linea);
                    }
                }

                tecla = Console.ReadKey(true);

                switch (tecla.Key)
                {
                    case ConsoleKey.UpArrow:
                        opcionSeleccionada = (opcionSeleccionada > 0) ? opcionSeleccionada - 1 : opciones.Length - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        opcionSeleccionada = (opcionSeleccionada < opciones.Length - 1) ? opcionSeleccionada + 1 : 0;
                        break;
                }
            } while (tecla.Key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            return opcionSeleccionada;
        }
    }
}
