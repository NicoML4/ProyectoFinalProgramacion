using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    class PantallaInicio
    {
        public static void Mostrar()
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.Clear();
            Console.CursorVisible = false;
            int ancho = Console.WindowWidth;

            string[] logo = new string[]
            {
                "██████╗  ██████╗ ██╗  ██╗███████╗██████╗  █████╗ ██╗    ██╗",
                "██╔══██╗██╔═══██╗██║ ██╔╝██╔════╝██   ██╗██╔══██╗██║    ██║",
                "██████╔╝██║   ██║█████╔╝ █████╗  ██   ██╝███████║██║ █╗ ██║",
                "██╔═══╝ ██║   ██║██╔═██╗ ██╔══╝  ██   ██ ██╔══██║██║███╗██║",
                "██║     ╚██████╔╝██║  ██╗███████╗██████  ██║╚ ██╔█████████╝",
                "╚═╝      ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═╝  ╚═╝╚═╝  ╚═╝ ╚══╝╚══╝ "
            };

            Console.ForegroundColor = ConsoleColor.Green;

            foreach (string linea in logo)
            {
                int espacios = (ancho - linea.Length) / 2;
                Console.WriteLine(new string(' ', espacios) + linea);
            }

            Console.ResetColor();

            Console.WriteLine();

            string mensaje = "Pulsa cualquier tecla para entrar";
            int espaciosMensaje = (ancho - mensaje.Length) / 2;
            Console.WriteLine(new string(' ', espaciosMensaje) + mensaje);

            Console.ReadKey(true);
            Console.Beep(150, 800);
            Console.Beep(200, 800);
            Console.Clear();
            Console.CursorVisible = true;
        }
    }
}
