using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class SobreAgua : Sobre
    {
        public static void AbrirSobre(Usuario usuario)
        {
            string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
            Random numRandom = new Random();
            List<Pokemon> pokemons = CargarTodosPokemons();
            List<Pokemon> pokemonsUsuario = CargarPokemonsUsuario(rutaUsuario);
            HashSet<int> randoms = new HashSet<int>();

            while (randoms.Count < 5)
            {
                randoms.Add(numRandom.Next(0, 151));
            }

            foreach (int numero in randoms)
            {
                MostrarPokemon(pokemons[numero]);

                if (!pokemonsUsuario.Contains(pokemons[numero]))
                {
                    pokemons[numero].SetFechaObtencion(DateTime.Now);
                    pokemonsUsuario.Add(pokemons[numero]);
                }

                pokemonsUsuario.Sort((a, b) => a.GetId().CompareTo(b.GetId()));

                Console.WriteLine("\nPresiona la barra espaciadora para ver el siguiente Pokémon...");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                Console.Clear();
            }

            File.WriteAllLines(rutaUsuario, pokemonsUsuario.Select(p => p.ToString()).ToArray());
        }
        private static string AjustarLinea(string linea, int maxWidth)
        {
            if (string.IsNullOrWhiteSpace(linea)) return linea;

            int originalWidth = linea.Length;
            if (originalWidth <= maxWidth) return linea;

            double factor = (double)maxWidth / originalWidth;
            int newWidth = (int)(originalWidth * factor);

            return new string(linea.Take(newWidth).ToArray());
        }


        private static void MostrarPokemon(Pokemon pokemon)
        {
            Console.Clear();
            string assetPath = $"../../../assets/{pokemon.GetNombre()}.txt";

            if (File.Exists(assetPath))
            {
                string[] assetLines = File.ReadAllLines(assetPath);
                int maxWidth = 100;

                foreach (string line in assetLines)
                {
                    Console.WriteLine(AjustarLinea(line, maxWidth));
                }
            }
            else
            {
                Console.WriteLine("[No se encontró el dibujo ASCII]");
            }
            Console.WriteLine($"Pokemon obtenido: {pokemon.GetNombre()}");
        }
    }
}
