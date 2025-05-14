using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal static class Sobre
    {
        private static List<Pokemon> Cargar()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            string[] separacion;

            string [] pokemonsFicheroCompleto = File.ReadAllLines("../../../Ficheros/pokemon_primera_generacion(modificado)");
            foreach (string pokemon in pokemonsFicheroCompleto)
            { 
                separacion = pokemon.Split(';');
                pokemons.Add(new Pokemon(Convert.ToInt32(separacion[0]), separacion[1], Convert.ToInt32(separacion[2]), separacion[3], separacion[4], separacion[5],null, separacion[7]));
            }
            return pokemons;
        }
        public static void AbrirSobre()
        {
            Random numRandom = new Random();
            List<Pokemon> pokemons = Cargar();
            HashSet<int> randoms = new HashSet<int>();

            while (randoms.Count < 5)
            {
                randoms.Add(numRandom.Next(1, 152));
            }



        }
    }
}
