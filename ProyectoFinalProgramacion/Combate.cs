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
                try
                {
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
                catch (Exception e)
                {
                    Console.WriteLine("Has escrito algo que no es un número");
                }
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
        private static Pokemon PokemonsVivos(Pokemon[] baraja,Pokemon pokemonAnterior)
        {
            Pokemon pokemonElegido = null;
            int eleccion=0;
            while (pokemonElegido == null)
            { 
                for(int i = 0; i < baraja.Length; i++)
                {
                    if (baraja[i].GetVida() != 0)
                    {
                        Console.Write($"[{i+1}-{baraja[i].GetNombre()}-Vida:{baraja[i].GetVida()}]");
                    }
                    else 
                    {
                        Console.Write($"[{i+1}-{baraja[i].GetNombre()}-Debilitado]");
                    }
                }
                Console.WriteLine();
                Console.Write("Elige un pokemon que no este debilitado (primer numero): ");
                try
                { 
                    eleccion = Convert.ToInt32(Console.ReadLine());
                    if (baraja[eleccion - 1].GetVida() != 0)
                    {
                        Console.WriteLine($"Adelante {baraja[eleccion - 1].GetNombre()}!");
                        pokemonElegido = baraja[eleccion - 1];
                        if (pokemonElegido == pokemonAnterior)
                        {
                            Console.WriteLine("(Otra vez)");                      
                        }
                    }
                    else
                    {
                        Console.WriteLine("Este pokemon está debilitado");
                        Console.ReadKey(true);
                    }
                }
                catch (Exception e)
                {
                    Console.Clear();
                    Console.WriteLine("Has escrito algo no válido");
                }
            }
            return pokemonElegido;
        }
        private static Ataque AtaqueUsuario(Pokemon pokemonActualUsuario)
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
                            Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque1().GetNombreAtaque()}!");
                            ataque = pokemonActualUsuario.GetAtaque1();
                            break;
                        case 2:
                            Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque2().GetNombreAtaque()}!");
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
                    Console.WriteLine("Introduce 1 o 2 para elegir la opción que quieras hacer");
                    eleccion = 0;
                }
            }
            return ataque;
        }
        private static Ataque AtaqueEnemigo(Pokemon pokemonActualUsuario)
        {
            Random r = new Random();
            Ataque ataque = null;
            int eleccion = r.Next(1,3);
            switch (eleccion)
            {
                case 1:
                    Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque1().GetNombreAtaque()}!");
                    ataque = pokemonActualUsuario.GetAtaque1();
                    break;
                case 2:
                    Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque2().GetNombreAtaque()}!");
                    ataque = pokemonActualUsuario.GetAtaque2();
                    break;
                default:
                    Console.WriteLine("Opción no valida");
                    eleccion = 0;
                    break;
            }
            return ataque;

        }

        private static int Calcular(Ataque ataque, Pokemon pokemonActualEnemigo)
        {
            int danyo = 0;
            string tipo = "";
            switch (ataque.GetNombreAtaque())
            {
                case "Ascuas":
                    danyo = 30;
                    tipo = "Fuego";
                    break;

                case "Pistola Agua":
                    danyo = 50;
                    tipo = "Agua";
                    break;

                case "Gruñido":
                    danyo = 10;
                    tipo = "Tierra";
                    break;

                case "Impactrueno":
                    danyo = 60;
                    tipo = "Tierra";
                    break;

                case "Látigo Cepa":
                    danyo = 30;
                    tipo = "Planta";
                    break;

                case "Arañazo":
                    danyo = 20;
                    tipo = "Fuego";
                    break;

                case "Placaje":
                    danyo = 15;
                    tipo = "Planta";
                    break;

                case "Burbuja":
                    danyo = 30;
                    tipo = "Agua";
                    break;
            }
            return Porcentaje(danyo, tipo, pokemonActualEnemigo);
        }

        private static int Porcentaje(int danyo, string tipo, Pokemon pokemonActualEnemigo)
        {
            switch (pokemonActualEnemigo.GetTipo())
            {
                case "fuego":
                    switch (tipo)
                    {
                        case "Fuego":
                            danyo = danyo;
                            break;

                        case "Agua":
                            danyo = (int)(danyo * 1.5);
                            Console.WriteLine("¡Es superefizaz!");
                            break;

                        case "Planta":
                            danyo = (int)(danyo * 0.5);
                            Console.WriteLine("Es poco efectivo...");
                            break;

                        case "Tierra":
                            danyo = danyo;
                            break;
                    }
                    break;

                case "agua":
                    switch (tipo)
                    {
                        case "Fuego":
                            danyo = (int)(danyo * 0.5);
                            Console.WriteLine("Es poco efectivo...");
                            break;

                        case "Agua":
                            danyo = danyo;
                            break;

                        case "Planta":
                            danyo = (int)(danyo * 1.5);
                            Console.WriteLine("¡Es superefizaz!");
                            break;

                        case "Tierra":
                            danyo = danyo;
                            break;
                    }
                    break;

                case "planta":
                    switch (tipo)
                    {
                        case "Fuego":
                            danyo = (int)(danyo * 1.5);
                            Console.WriteLine("¡Es superefizaz!");
                            break;

                        case "Agua":
                            danyo = (int)(danyo * 0.5);
                            Console.WriteLine("Es poco efectivo...");
                            break;

                        case "Planta":
                            danyo = danyo;
                            break;

                        case "Tierra":
                            danyo = danyo;
                            break;
                    }
                    break;

                case "tierra":
                    switch (tipo)
                    {
                        case "Fuego":
                            danyo = (int)(danyo * 0.5);
                            Console.WriteLine("Es poco efectivo...");
                            break;

                        case "Agua":
                            danyo = danyo;
                            break;

                        case "Planta":
                            danyo = danyo;
                            break;

                        case "Tierra":
                            danyo = (int)(danyo * 1.5);
                            Console.WriteLine("¡Es superefizaz!");
                            break;
                    }
                    break;
            }
            Console.WriteLine($"Inflige {danyo} puntos de daño");
            return danyo;
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
            int contadorPokemonEnemigo =1;
            while (RecorrerBaraja(baraja) && RecorrerBaraja(barajaEnemiga))
            {
                if (pokemonActualUsuario == null)
                {
                    pokemonActualUsuario = PokemonsVivos(baraja,pokemonActualUsuario);
                }

                eleccion = 0;
                while (eleccion == 0)
                {
                    Console.WriteLine($"¿Que quieres hacer {usuario.NombreUsuario}?\n");
                    Console.WriteLine($"1 - Atacar");
                    Console.WriteLine($"2 - Cambiar de pokemon\n");
                    try
                    {
                        eleccion = Convert.ToInt32(Console.ReadLine());
                        switch (eleccion)
                        {
                            case 1:
                                Console.Clear();
                                pokemonActualEnemigo.RecibirDano(Calcular(AtaqueUsuario(pokemonActualUsuario), pokemonActualEnemigo));
                                Console.WriteLine($"Vida actual de {pokemonActualEnemigo.GetNombre()}:{pokemonActualEnemigo.GetVida()}");
                                Console.ReadKey(true);
                                Console.Clear();
                                break;
                            case 2:
                                Console.Clear();
                                pokemonActualUsuario = PokemonsVivos(baraja, pokemonActualUsuario);
                                break;
                            default:
                                Console.Clear();
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
                    if (pokemonActualEnemigo.GetVida() == 0 && contadorPokemonEnemigo != 6)
                    {
                        Console.WriteLine($"¡{pokemonActualEnemigo.GetNombre()} ha sido debilitado!");
                        pokemonActualEnemigo = barajaEnemiga[contadorPokemonEnemigo];
                        contadorPokemonEnemigo++;
                        Console.WriteLine($"¡{enemigo.NombreUsuario} saca a {pokemonActualEnemigo.GetNombre()}!");
                    }
                    if (!RecorrerBaraja(barajaEnemiga))
                    {
                        Console.WriteLine("Has ganado felicidades");
                    }
                    else 
                    {
                        Console.WriteLine($"Turno de {enemigo.NombreUsuario}");
                        pokemonActualUsuario.RecibirDano(Calcular(AtaqueEnemigo(pokemonActualEnemigo), pokemonActualEnemigo));
                        Console.WriteLine($"Vida actual de {pokemonActualUsuario.GetNombre()}:{pokemonActualUsuario.GetVida()}");
                        if (pokemonActualUsuario.GetVida() == 0)
                        {
                            if (RecorrerBaraja(baraja))
                            {
                                Console.WriteLine($"¡{pokemonActualUsuario.GetNombre()} ha sido debilitado!");
                                pokemonActualUsuario = PokemonsVivos(baraja, pokemonActualUsuario);
                                Console.WriteLine($"¡Sacas a {pokemonActualUsuario.GetNombre()}!");
                            }
                            else
                            {
                                Console.WriteLine("Has perdido...");
                            }
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
                if (Combatientes(usuario).Count > 0)
                {
                    Usuario enemigo = elegirCombatiente(Combatientes(usuario));
                    Pokemon[] barajaEnemiga = ElegirPokemonEnemigo(CargarPokemonsUsuario("../../../Usuarios/" + enemigo.NombreUsuario + ".txt"));
                    Jugar(usuario, enemigo, baraja, barajaEnemiga);
                }
                else
                {
                    Console.WriteLine("No hay combatientes con suficientes pokemón para enfrentarte");
                }
            }
            else 
            {
                Console.WriteLine("No puedes combatir hasta que no tengas al menos 6 pokemons");
            }
        }

        
    }
}
