using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Combate
    {
        private static bool CantidadMinimaPokemons(Usuario usuario)
        {
            string[] cantidadPokemons = File.ReadAllLines("../../../Usuarios/" + usuario.NombreUsuario + ".txt");
            return cantidadPokemons.Length>=6 ? true : false;
        }
        private static List<Pokemon> CargarPokemonsUsuario(string rutaUsuario)
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
        private static Pokemon[] ElegirPokemons(List<Pokemon> pokemonsUsuario) 
        {
            Pokemon[] baraja = new Pokemon[6];
            int eleccion;
            Pokemon pokemonEncontrado;
            int contador=0;

            while (contador!=6)
            {
                foreach (Pokemon pokemon in pokemonsUsuario)
                {
                    Console.WriteLine($"{pokemon.GetId()} - {pokemon.GetNombre()}");
                }
                Console.WriteLine($"Elige tu {contador + 1}º pokemon");
                eleccion = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if (pokemonsUsuario.Exists(p => p.GetId() == eleccion))
                {
                    pokemonEncontrado = pokemonsUsuario.Find(p => p.GetId() == eleccion);
                    baraja[contador] = pokemonEncontrado;
                    pokemonsUsuario.Remove(pokemonEncontrado);
                    contador++;
                }
                else 
                {
                    Console.WriteLine("Opción no valida");
                }
                for (int i = 0; i < baraja.Length; i++)
                {
                    if (baraja[i] != null)
                    {
                        Console.Write($"[{baraja[i].GetNombre()}]");
                    }
                    else 
                    {
                        Console.Write("[]");
                    }
                }
                Console.WriteLine();
            }
            return baraja;
        }
        public static void Inicializar(Usuario usuario)
        {
            if (CantidadMinimaPokemons(usuario))
            {
                string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
                Console.WriteLine("Elige tus pokemons: ");

                Pokemon [] baraja = ElegirPokemons(CargarPokemonsUsuario(rutaUsuario));

            }
            else 
            {
                Console.WriteLine("No puedes combatir hasta que no tengas al menos 6 pokemons");
            }
            
        }
    }
}
