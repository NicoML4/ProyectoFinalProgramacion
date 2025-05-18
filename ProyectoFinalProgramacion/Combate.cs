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
            Console.ReadKey(true);
            return baraja;
        }

        private static List<Usuario> Combatientes(Usuario usuario)
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
        private static Usuario elegirCombatiente(List<Usuario> combatientesDisponibles)
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
        private static bool RecorrerBaraja(Pokemon[] baraja)
        {
            int contador = 0;
            foreach (Pokemon pokemon in baraja)
            {
                if (pokemon.GetVida() == 0)
                { 
                    contador++;
                }
            }
            return contador == 6? false:true;
        }
        private static Pokemon PokemonsVivos(Pokemon[] baraja)
        {
            Pokemon pokemonElegido = null;
            int eleccion=0;
            while (pokemonElegido == null)
            { 
                for(int i = 0; i < baraja.Length; i++)
                {
                    if (baraja[i].GetVida() != 0)
                    {
                        Console.Write($"[{i+1}-{baraja[i].GetNombre()}-{baraja[i].GetVida()}]");
                    }
                    else 
                    {
                        Console.Write($"[{i+1}-{baraja[i].GetNombre()}-Debilitado]");
                    }
                }
                Console.WriteLine();
                Console.Write("Elige un pokemon que no este debilitado (numero): ");
                try
                { 
                    eleccion = Convert.ToInt32(Console.ReadLine());
                    if (baraja[eleccion - 1].GetVida() != 0)
                    {
                        Console.WriteLine($"Adelante {baraja[eleccion - 1].GetNombre}!");
                        pokemonElegido = baraja[eleccion - 1];
                    }
                    else
                    {
                        Console.WriteLine("Este pokemon está debilitado");
                        Console.ReadKey(true);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Has escrito algo no válido");
                }
            }
            return pokemonElegido;
        }
        private static Ataque ataqueUsuario(Pokemon pokemonActualUsuario)
        {
            int eleccion = 0;
            Ataque ataque = null;
            while (eleccion == 0)
            {
                Console.WriteLine($"Que ataque quieres que haga {pokemonActualUsuario.GetNombre()}\n");
                Console.WriteLine($"1 - {pokemonActualUsuario.GetAtaque1().GetNombreAtaque()}");
                Console.WriteLine($"2 - {pokemonActualUsuario.GetAtaque2().GetNombreAtaque()}\n");
                try
                {
                    eleccion = Convert.ToInt32(Console.ReadLine());
                    switch (eleccion)
                    {
                        case 1:
                            Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque1().GetNombreAtaque()}");
                            ataque = pokemonActualUsuario.GetAtaque1();
                            break;
                        case 2:
                            Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque2().GetNombreAtaque()}");
                            ataque = pokemonActualUsuario.GetAtaque2();
                            break;
                        default:
                            Console.WriteLine("Opción no valida");
                            eleccion = 0;
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Introduce 1 o 2 para elegir el ataque que quieras");
                    eleccion = 0;
                }
            }
            return ataque;
        }
        private static void Jugar(Usuario usuario, Usuario enemigo, Pokemon[] baraja, Pokemon[] barajaEnemiga)
        {
            Pokemon pokemonActualUsuario = baraja[0];
            Pokemon pokemonActualEnemigo = barajaEnemiga[0];
            Console.Clear();
            Console.WriteLine($"{usuario.NombreUsuario} saca a {pokemonActualUsuario.GetNombre()}\n");
            Console.WriteLine($"{enemigo.NombreUsuario} saca a {pokemonActualEnemigo.GetNombre()}\n");
            Console.WriteLine("¡Que empiece el combate!");
            int eleccion = 0;
            while (RecorrerBaraja(baraja) && RecorrerBaraja(barajaEnemiga))
            {
                if (pokemonActualUsuario == null)
                {
                    pokemonActualUsuario = PokemonsVivos(baraja);
                }

                eleccion = 0;
                while (eleccion == 0)
                {
                    Console.WriteLine($"Que quieres hacer {usuario.NombreUsuario}\n");
                    Console.WriteLine($"1 - Atacar");
                    Console.WriteLine($"2 - Cambiar de pokemon\n");
                    try
                    {
                        eleccion = Convert.ToInt32(Console.ReadLine());
                        switch (eleccion)
                        {
                            case 1:
                                ataqueUsuario(pokemonActualUsuario);
                                break;
                            case 2:
                                pokemonActualUsuario = PokemonsVivos(baraja);
                                break;
                            default:
                                Console.WriteLine("Opción no valida");
                                eleccion = 0;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.Clear();
                        Console.WriteLine("Introduce 1 o 2 para elegir el ataque que quieras");
                        eleccion = 0;
                    }
                }

            }
            
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
                Jugar(usuario,enemigo,baraja,barajaEnemiga);
            }
            else 
            {
                Console.WriteLine("No puedes combatir hasta que no tengas al menos 6 pokemons");
            }
        }
        
    }
}
