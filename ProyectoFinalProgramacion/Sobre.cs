using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Sobre
    {
        public static List<Pokemon> CargarTodosPokemons()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            string[] separacion;
            string [] pokemonsFicheroCompleto = File.ReadAllLines("../../../Ficheros/pokemon_primera_generacion(modificado).txt");
            foreach (string pokemon in pokemonsFicheroCompleto)
            { 
                separacion = pokemon.Split(';');
                pokemons.Add(new Pokemon(Convert.ToInt32(separacion[0]), separacion[1], Convert.ToInt32(separacion[2]), separacion[3], separacion[4], separacion[5],null, separacion[7]));
            }
            return pokemons;
        }
        public static List<Pokemon> CargarPokemonsUsuario(string rutaUsuario)
        {
            List<Pokemon> pokemonsUsuario = new List<Pokemon>();
            string[] separacion;
            string[] pokemonsFicheroUsuario = File.ReadAllLines(rutaUsuario);
            foreach (string pokemon in pokemonsFicheroUsuario)
            {
                separacion = pokemon.Split(';');
                pokemonsUsuario.Add(new Pokemon(Convert.ToInt32(separacion[0]), separacion[1], Convert.ToInt32(separacion[2]), separacion[3], separacion[4], separacion[5], DateTime.Parse(separacion[6], new CultureInfo("es-ES")), separacion[7]));
            }
            return pokemonsUsuario;
        }

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
                Console.WriteLine($"Pokemon obtenido: {pokemons[numero].GetNombre()}");
                if (!pokemonsUsuario.Contains(pokemons[numero]))
                {
                    pokemons[numero].SetFechaObtencion(DateTime.Now);
                    pokemonsUsuario.Add(pokemons[numero]);
                }
                pokemonsUsuario.Sort((a,b) => a.GetId().CompareTo(b.GetId()));
            }
            File.WriteAllLines(rutaUsuario, pokemonsUsuario.Select(p => p.ToString()).ToArray());
        }
    }
}
