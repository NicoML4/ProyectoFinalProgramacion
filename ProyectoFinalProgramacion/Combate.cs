using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        private static Pokemon[] ElegirPokemonEnemigo(List<Pokemon> pokemonsEnemigo)
        {
            Pokemon[] baraja = new Pokemon[6];
            int eleccion;
            Pokemon pokemonEncontrado;
            int contador = 0;
            Random r = new Random();

            while (contador != 6)
            {
                eleccion = r.Next(1,152);
                Console.Clear();
                if (pokemonsEnemigo.Exists(p => p.GetId() == eleccion))
                {
                    pokemonEncontrado = pokemonsEnemigo.Find(p => p.GetId() == eleccion);
                    baraja[contador] = pokemonEncontrado;
                    pokemonsEnemigo.Remove(pokemonEncontrado);
                    contador++;
                }
            }
            Console.WriteLine("Pokemons del enemigo:");
            for (int i = 0; i < baraja.Length; i++)
            {
                Console.Write($"[{baraja[i].GetNombre()}]");
            }
                Console.WriteLine();
            return baraja;
        }

        public static List<Usuario> Combatientes(Usuario usuario)
        { 
            string json = File.ReadAllText("../../../Usuarios/UsuariosRegistrados.json");
            List<Usuario> usuariosGuardados = JsonSerializer.Deserialize<List<Usuario>>(json);
            List<Usuario> combatientesDisponibles = new List<Usuario>();
            usuariosGuardados.Remove(usuario);
            foreach (Usuario combatiente in usuariosGuardados)
            {
                if (CargarPokemonsUsuario("../../../Usuarios/" + combatiente.NombreUsuario + ".txt").Count>=6)
                {
                    combatientesDisponibles.Add(combatiente);
                }
            }
            return combatientesDisponibles;   
        }
        public static Usuario elegirCombatiente(List<Usuario> combatientesDisponibles)
        {
            int eleccion = 0;
            while (eleccion == 0)
            {
                Console.WriteLine("Elige un combatiente: ");
                for (int i = 0; i < combatientesDisponibles.Count; i++)
                {
                    Console.WriteLine($"{i + 1}-{combatientesDisponibles[i]}");
                }
                try
                {
                    eleccion = Convert.ToInt32(Console.ReadLine());
                    if (eleccion <= 0 || eleccion > combatientesDisponibles.Count)
                    {
                        Console.Clear();
                        Console.WriteLine("No has elegido ningún combatiente disponible");
                        eleccion = 0;
                    }
                    else
                    {
                        Usuario enemigo = combatientesDisponibles[eleccion - 1];
                        Console.WriteLine($"Preparate para enfrentarte a {enemigo.NombreUsuario}!");
                        return enemigo;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    eleccion = 0;
                    Console.WriteLine("Solo puedes poner números");
                }
                
            }
            return null;
        }
        public static void Inicializar(Usuario usuario)
        {
            if (CantidadMinimaPokemons(usuario))
            {
                string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
                Console.WriteLine("Elige tus pokemons: ");
                Pokemon [] baraja = ElegirPokemons(CargarPokemonsUsuario(rutaUsuario));

                Usuario enemigo = elegirCombatiente(Combatientes(usuario));
                Pokemon[] barajaEnemiga = ElegirPokemonEnemigo(CargarPokemonsUsuario("../../../Usuarios/" + enemigo.NombreUsuario + ".txt"));
            }
            else 
            {
                Console.WriteLine("No puedes combatir hasta que no tengas al menos 6 pokemons");
            }
        }
        
    }
}
