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
        static Ajustes ajustesglobales = new Ajustes();
        private static bool CantidadMinimaPokemons(Usuario usuario)
        {
            string[] cantidadPokemons = File.ReadAllLines("../../../Usuarios/" + usuario.NombreUsuario + ".txt");
            return cantidadPokemons.Length >= 6 ? true : false;
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
            int contador = 0;

            int consolaAncho = Console.WindowWidth;

            while (contador != 6)
            {
                int seleccionActual = 0;

                while (true)
                {
                    Console.Clear();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Elige tu {contador + 1}º pokemon (Usa ↑↓ y Enter para seleccionar):\n");

                    int columnas = 4;
                    int filas = (int)Math.Ceiling((double)pokemonsUsuario.Count / columnas);

                    int anchoColumna = pokemonsUsuario.Max(p => p.GetNombre().Length) + 7;

                    for (int fila = 0; fila < filas; fila++)
                    {
                        for (int col = 0; col < columnas; col++)
                        {
                            int indice = fila + col * filas;
                            if (indice >= pokemonsUsuario.Count)
                                break;

                            var pokemon = pokemonsUsuario[indice];

                            string texto = $"{pokemon.GetId()} - {pokemon.GetNombre()}";

                            if (indice == seleccionActual)
                            {
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write(texto.PadRight(anchoColumna));
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write(texto.PadRight(anchoColumna));
                                Console.ResetColor();
                            }
                        }
                        Console.WriteLine();
                    }

                    Console.WriteLine();

                    string seleccionados = string.Join(new string(' ', 10), baraja.Where(p => p != null).Select(p => p.GetNombre()));
                    int padding = (consolaAncho - seleccionados.Length) / 2;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(new string(' ', padding > 0 ? padding : 0) + seleccionados);

                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        seleccionActual--;
                        if (seleccionActual < 0)
                            seleccionActual = pokemonsUsuario.Count - 1;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        seleccionActual++;
                        if (seleccionActual >= pokemonsUsuario.Count)
                            seleccionActual = 0;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        var pokemonElegido = pokemonsUsuario[seleccionActual];
                        baraja[contador] = pokemonElegido;
                        pokemonsUsuario.RemoveAt(seleccionActual);
                        contador++;
                        break;
                    }
                }
                
            }

            Console.Clear();

            string seleccionFinal = string.Join(new string(' ', 10), baraja.Select(p => p.GetNombre()));
            int paddingFinal = (consolaAncho - seleccionFinal.Length) / 2;
            Console.WriteLine(new string(' ', paddingFinal > 0 ? paddingFinal : 0) + seleccionFinal);
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey(true);

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
                eleccion = r.Next(1, 152);
                Console.Clear();
                if (pokemonsEnemigo.Exists(p => p.GetId() == eleccion))
                {
                    pokemonEncontrado = pokemonsEnemigo.Find(p => p.GetId() == eleccion);
                    baraja[contador] = pokemonEncontrado;
                    pokemonsEnemigo.Remove(pokemonEncontrado);
                    contador++;
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
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
                if (CargarPokemonsUsuario("../../../Usuarios/" + combatiente.NombreUsuario + ".txt").Count >= 6)
                {
                    combatientesDisponibles.Add(combatiente);
                }
            }
            return combatientesDisponibles;
        }
        private static Usuario elegirCombatiente(List<Usuario> combatientesDisponibles)
        {
            int seleccionActual = 0;
            ConsoleKey tecla;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Elige un combatiente (Usa ↑↓ y Enter para seleccionar):\n");

                for (int i = 0; i < combatientesDisponibles.Count; i++)
                {
                    if (i == seleccionActual)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine($"{i + 1} - {combatientesDisponibles[i].NombreUsuario}");
                    Console.ResetColor();
                }

                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow)
                {
                    seleccionActual--;
                    if (seleccionActual < 0)
                        seleccionActual = combatientesDisponibles.Count - 1;
                }
                else if (tecla == ConsoleKey.DownArrow)
                {
                    seleccionActual++;
                    if (seleccionActual >= combatientesDisponibles.Count)
                        seleccionActual = 0;
                }

            } while (tecla != ConsoleKey.Enter);

            Usuario enemigo = combatientesDisponibles[seleccionActual];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Prepárate para enfrentarte a {enemigo.NombreUsuario}!");
            Console.ResetColor();
            Console.ReadKey(true);
            return enemigo;
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
            return contador == 6 ? false : true;
        }
        private static Pokemon PokemonsVivos(Pokemon[] baraja, Pokemon pokemonAnterior)
        {
            int seleccionActual = 0;
            bool vivo = false;
            while (!vivo)
            {
                if (baraja[seleccionActual].GetVida() > 0)
                {
                    vivo = true;
                }
                else 
                {
                    seleccionActual++;
                }
            }
            Pokemon pokemonElegido = null;
            ConsoleKey tecla;

            // Asegurarse de que al menos uno esté vivo
            if (!baraja.Any(p => p.GetVida() > 0))
            {
                Console.WriteLine("Todos los Pokémon están debilitados.");
                return null;
            }

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Elige un Pokémon que no esté debilitado:\n");

                for (int i = 0; i < baraja.Length; i++)
                {
                    Pokemon p = baraja[i];
                    bool seleccionado = (i == seleccionActual);

                    if (seleccionado)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = p.GetVida() == 0 ? ConsoleColor.DarkGray : ConsoleColor.Green;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    string estado = p.GetVida() > 0 ? $"Vida: {p.GetVida()}" : "Debilitado";
                    Console.WriteLine($"{i + 1} - {p.GetNombre()} - {estado}");
                    Console.ResetColor();
                }

                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
                {
                    do
                    {
                        seleccionActual = (seleccionActual - 1 + baraja.Length) % baraja.Length;
                    } while (baraja[seleccionActual].GetVida() == 0);
                }
                else if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
                {
                    do
                    {
                        seleccionActual = (seleccionActual + 1) % baraja.Length;
                    } while (baraja[seleccionActual].GetVida() == 0);
                }

            } while (tecla != ConsoleKey.Enter);

            pokemonElegido = baraja[seleccionActual];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"¡Adelante {pokemonElegido.GetNombre()}!");
            if (pokemonElegido == pokemonAnterior)
            {
                Console.WriteLine("(Otra vez)");
            }
            Console.ResetColor();
            Console.ReadKey(true);

            return pokemonElegido;
        }

        private static Ataque AtaqueUsuario(Pokemon pokemonActualUsuario)
        {
            int seleccionActual = 0;
            ConsoleKey tecla;
            Ataque ataque = null;

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"¿Qué ataque quieres que haga {pokemonActualUsuario.GetNombre()}?\n");

                for (int i = 0; i < 2; i++)
                {
                    string nombreAtaque = (i == 0) ? pokemonActualUsuario.GetAtaque1().GetNombreAtaque()
                                                   : pokemonActualUsuario.GetAtaque2().GetNombreAtaque();

                    if (i == seleccionActual)
                    {
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.WriteLine($"{i + 1} - {nombreAtaque}");
                    Console.ResetColor();
                }

                tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
                {
                    seleccionActual = (seleccionActual - 1 + 2) % 2;
                }
                else if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
                {
                    seleccionActual = (seleccionActual + 1) % 2;
                }

            } while (tecla != ConsoleKey.Enter);

            ataque = (seleccionActual == 0) ? pokemonActualUsuario.GetAtaque1() : pokemonActualUsuario.GetAtaque2();

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {ataque.GetNombreAtaque()}!");
            Console.ResetColor();
            Console.ReadKey(true);

            return ataque;
        }

        private static Ataque AtaqueEnemigo(Pokemon pokemonActualUsuario)
        {
            Random r = new Random();
            Ataque ataque = null;
            int eleccion = r.Next(1, 3);
            switch (eleccion)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque1().GetNombreAtaque()}!");
                    ataque = pokemonActualUsuario.GetAtaque1();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{pokemonActualUsuario.GetNombre()} ataca con {pokemonActualUsuario.GetAtaque2().GetNombreAtaque()}!");
                    ataque = pokemonActualUsuario.GetAtaque2();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Opción no valida");
                    eleccion = 0;
                    Console.ResetColor();
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
            //iniciar musica combate
            ajustesglobales.Reproducircombate();
            Pokemon pokemonActualUsuario = baraja[0];
            Pokemon pokemonActualEnemigo = barajaEnemiga[0];
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{usuario.NombreUsuario} saca a {pokemonActualUsuario.GetNombre()}\n");
            Console.WriteLine($"{enemigo.NombreUsuario} saca a {pokemonActualEnemigo.GetNombre()}\n");
            Console.WriteLine("¡Que empiece el combate!");
            Console.ReadKey(true);

            int contadorPokemonEnemigo = 1;

            while (RecorrerBaraja(baraja) && RecorrerBaraja(barajaEnemiga))
            {
                if (pokemonActualUsuario == null)
                {
                    pokemonActualUsuario = PokemonsVivos(baraja, pokemonActualUsuario);
                }

                int opcionSeleccionada = 0;
                ConsoleKey tecla;

                do
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"¿Qué quieres hacer {usuario.NombreUsuario}?\n");

                    string[] opciones = { "Atacar", "Cambiar de Pokémon" };

                    for (int i = 0; i < opciones.Length; i++)
                    {
                        if (i == opcionSeleccionada)
                        {
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        Console.WriteLine($"{i + 1} - {opciones[i]}");
                        Console.ResetColor();
                    }

                    tecla = Console.ReadKey(true).Key;

                    if (tecla == ConsoleKey.UpArrow || tecla == ConsoleKey.W)
                    {
                        opcionSeleccionada = (opcionSeleccionada - 1 + opciones.Length) % opciones.Length;
                    }
                    else if (tecla == ConsoleKey.DownArrow || tecla == ConsoleKey.S)
                    {
                        opcionSeleccionada = (opcionSeleccionada + 1) % opciones.Length;
                    }

                } while (tecla != ConsoleKey.Enter);

                switch (opcionSeleccionada)
                {
                    case 0:
                        Console.Clear();
                        pokemonActualEnemigo.RecibirDano(Calcular(AtaqueUsuario(pokemonActualUsuario), pokemonActualEnemigo));
                        Console.WriteLine($"Vida actual de {pokemonActualEnemigo.GetNombre()}: {pokemonActualEnemigo.GetVida()}");
                        Console.ReadKey(true);
                        Console.Clear();
                        break;

                    case 1:
                        Console.Clear();
                        pokemonActualUsuario = PokemonsVivos(baraja, pokemonActualUsuario);
                        break;
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
                    Console.WriteLine("¡Has ganado, felicidades!");
                    break;
                }
                else
                {
                    // Turno enemigo

                    Console.WriteLine($"\nTurno de {enemigo.NombreUsuario}");
                    pokemonActualUsuario.RecibirDano(Calcular(AtaqueEnemigo(pokemonActualEnemigo), pokemonActualEnemigo));
                    Console.WriteLine($"Vida actual de {pokemonActualUsuario.GetNombre()}: {pokemonActualUsuario.GetVida()}");


                    if (pokemonActualUsuario.GetVida() == 0)
                    {
                        if (RecorrerBaraja(baraja))
                        {
                            Console.WriteLine($"¡{pokemonActualUsuario.GetNombre()} ha sido debilitado!");
                            Console.ReadKey(true);
                            pokemonActualUsuario = PokemonsVivos(baraja, pokemonActualUsuario);
                            Console.WriteLine($"¡Sacas a {pokemonActualUsuario.GetNombre()}!");
                        }
                        else
                        {
                            Console.WriteLine("Has perdido...");
                            break;
                        }
                    }
                    else
                    {
                        Console.ReadKey(true);
                    }

                }
            }
            //fin de combate
            ajustesglobales.DetenerMusica();
        }

        public static void Inicializar(Usuario usuario)
        {
            if (CantidadMinimaPokemons(usuario))
            {
                string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
                Console.WriteLine("Elige tus pokemons: ");
                Pokemon[] baraja = ElegirPokemons(CargarPokemonsUsuario(rutaUsuario));
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
