using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal static class Sobre
    {
        private static void Cargar()
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            string[] separacion;

            string [] pokemonsFicheroCompleto = File.ReadAllLines("../../../Ficheros/pokemon_primera_generacion(modificado)");
            foreach (string pokemon in pokemonsFicheroCompleto)
            { 
                separacion = pokemon.Split(';');
                pokemons.Add(new Pokemon(separacion[0],))
            }
        }
        public static void AbrirSobre()
        {
            Cargar();

        }
    }
}
