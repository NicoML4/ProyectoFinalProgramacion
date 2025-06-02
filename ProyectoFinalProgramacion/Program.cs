using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinalProgramacion
{
    internal class Program
    {
        const string FICHERO_POKEMON = "../../../Ficheros/pokemon_primera_generacion(modificado).txt";
        const string CONTRASENYA_ADMIN = "P0kem0n";

        static Ajustes ajustesglobales = new Ajustes();
        public static Usuario IniciarSesion(string rutaUsuarios)
        {
            Usuario usuarioGuardado;
            string nombreUsuarioLogeado = "";
            string contrasenaUsuarioLogeado = "";
            string json = File.ReadAllText(rutaUsuarios);
            bool aceptado = false;
            while (nombreUsuarioLogeado == "")
            {
                ConsolaInterfaz.WriteLineCentr("Dime tu nombre de usuario");
                nombreUsuarioLogeado = Console.ReadLine();
                if (nombreUsuarioLogeado == "")
                {
                    ConsolaInterfaz.WriteLineCentr("No has introducido nada");
                }

            }
            while (contrasenaUsuarioLogeado == "")
            {
                ConsolaInterfaz.WriteLineCentr("Dime tu contraseña");
                contrasenaUsuarioLogeado = Console.ReadLine();
                if (contrasenaUsuarioLogeado == "")
                {
                    ConsolaInterfaz.WriteLineCentr("No has introducido nada");
                }
            }
            Console.Clear();
            usuarioGuardado = new Usuario(nombreUsuarioLogeado, contrasenaUsuarioLogeado);
            try
            {
                List<Usuario> usuariosGuardados = JsonSerializer.Deserialize<List<Usuario>>(json);
                if (usuariosGuardados.Contains(usuarioGuardado))
                {
                    aceptado = true;
                }
                else
                {
                    ConsolaInterfaz.WriteLineCentr("El usuario o contraseña que estás introduciendo es incorrecto");
                    Console.ReadKey(true);
                }
            }
            catch (JsonException e)
            {
                ConsolaInterfaz.WriteLineCentr("Aun no se ha creado ningún usuario");
            }
            return aceptado ? usuarioGuardado : null;
        }
        public static Usuario Registrarse(string rutaUsuarios)
        {
            string nombreUsuarioRegistro = "";
            string contrasenaRegistro = "";
            string contrasenaRegistroRepetida = "";
            List<Usuario> usuariosRegistrados;
            bool aceptado = false;
            string json = File.ReadAllText(rutaUsuarios);
            try
            {
                usuariosRegistrados = JsonSerializer.Deserialize<List<Usuario>>(json);
            }
            catch (JsonException e)
            {
                usuariosRegistrados = new List<Usuario>();
            }
            while (nombreUsuarioRegistro == "")
            {
                ConsolaInterfaz.WriteLineCentr("Dime tu nombre de usuario");
                nombreUsuarioRegistro = Console.ReadLine();
                if (nombreUsuarioRegistro == "")
                {
                    ConsolaInterfaz.WriteLineCentr("No has introducido nada");
                }
            }
            while (contrasenaRegistro == "")
            {
                ConsolaInterfaz.WriteLineCentr("Dime tu contraseña");
                contrasenaRegistro = Console.ReadLine();
                if (contrasenaRegistro == "")
                {
                    ConsolaInterfaz.WriteLineCentr("No has introducido nada");
                }
            }
            while (contrasenaRegistro != contrasenaRegistroRepetida)
            {
                ConsolaInterfaz.WriteLineCentr("Repiteme la contraseña");
                contrasenaRegistroRepetida = Console.ReadLine();
                if (contrasenaRegistro != contrasenaRegistroRepetida)
                {
                    ConsolaInterfaz.WriteLineCentr("Ambas contraseñas no coinciden");
                }
            }
            Console.Clear();
            Usuario usuarioCreado = new Usuario(nombreUsuarioRegistro, contrasenaRegistro);
            if (usuariosRegistrados.Exists(u => u.NombreUsuario == usuarioCreado.NombreUsuario))
            {
                ConsolaInterfaz.WriteLineCentr("Ya hay un usuario con ese nombre");
                Console.ReadKey(true);
            }
            else
            {
                ConsolaInterfaz.WriteLineCentr("¡Usuario nuevo creado con éxito!");
                usuariosRegistrados.Add(usuarioCreado);
                JsonSerializerOptions opciones = new JsonSerializerOptions { WriteIndented = true };
                string datosSerializados = JsonSerializer.Serialize(usuariosRegistrados, opciones);
                File.WriteAllText(rutaUsuarios, datosSerializados);
                StreamWriter creacionFichero = new StreamWriter($"../../../Usuarios/{usuarioCreado.NombreUsuario}.txt");
                creacionFichero.Close();
                aceptado = true;
                Console.ReadKey(true);
            }
            return aceptado ? usuarioCreado : null; 
            
        }
        public static Usuario Login(string rutaUsuarios)
        {
            string[] opcionesLogin = {
                "Iniciar Sesión",
                "Registrarse"
            };
            
            Usuario usuarioGuardado = null;
            while (usuarioGuardado == null)
            { 
                int opcionSeleccionada = ConsolaInterfaz.SeleccionarOpcion(opcionesLogin);
                if (opcionSeleccionada == 0)
                {
                    usuarioGuardado = IniciarSesion(rutaUsuarios);
                }
                else if (opcionSeleccionada == 1)
                {
                    usuarioGuardado = Registrarse(rutaUsuarios);
                }
            }
            
            return usuarioGuardado;
        }
        public static void AbrirSobres(Usuario usuarioLogeado)
        {
            Console.Clear();
            string[] opcionesSobres = {
                "Sobre de Fuego",
                "Sobre de Agua",
                "Sobre de Tierra",
                "Sobre de Planta",
                "Sobre Mixto",
                "Atrás"
            };
            int opcion = ConsolaInterfaz.SeleccionarOpcion(opcionesSobres);
            switch (opcion)
            {
                case 0:
                    SobreFuego.AbrirSobre(usuarioLogeado);
                    break;
                case 1:
                    SobreAgua.AbrirSobre(usuarioLogeado);
                    break;
                case 2:
                    SobreTierra.AbrirSobre(usuarioLogeado);
                    break;
                case 3:
                    SobrePlanta.AbrirSobre(usuarioLogeado);
                    break;
                case 4:
                    Sobre.AbrirSobre(usuarioLogeado);
                    break;
                case 5:
                    break;
            }
        }
        public static void Pokedex(Usuario usuario)
        {
            Console.Clear();
            string[] opcionesPokedex = {
        "Mis Pokémon",
        "Todos",
        "Pokémons bloqueados",
        "Filtrar por tipo",
        "Atrás"
    };
            int opcion = ConsolaInterfaz.SeleccionarOpcion(opcionesPokedex);
            string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
            List<Pokemon> listaPokemons = ListaPokemonCompleta(FICHERO_POKEMON);
            List<Pokemon> listaPokemonUsuario = ListaPokemonUsuario(rutaUsuario);

            switch (opcion)
            {
                case 0:
                    Console.Clear();
                    ConsolaInterfaz.MostrarListaConScroll(listaPokemonUsuario);
                    break;

                case 1:
                    Console.Clear();
                    ConsolaInterfaz.MostrarListaConScroll(listaPokemons);
                    break;

                case 2:
                    Console.Clear();
                    var bloqueados = listaPokemons
                        .Where(p => !listaPokemonUsuario.Any(u => u.GetId() == p.GetId()))
                        .ToList();
                    ConsolaInterfaz.MostrarListaConScroll(bloqueados);
                    break;

                case 3:
                    Console.Clear();
                    string[] opcionesTipos = {
                "Filtrar por tipo fuego",
                "Filtrar por tipo agua",
                "Filtrar por tipo tierra",
                "Filtrar por tipo planta",
                "Atrás"
            };
                    int opcionTipo = ConsolaInterfaz.SeleccionarOpcion(opcionesTipos);
                    SwitchTipos(opcionTipo, rutaUsuario);
                    break;

                case 4:
                    break;
            }
        }

        public static void SwitchTipos(int opcion, string rutaUsuario)
        {
            List<Pokemon> listaPokemonUsuario = ListaPokemonUsuario(rutaUsuario);
            bool salir = false;
            while (!salir)
            {
                switch (opcion)
                {
                    case 0:
                        Console.Clear();
                        listaPokemonUsuario.Where(p => p.GetTipo() == "fuego").Select(p => $"{p.GetId()} - {p.GetNombre()}")
                            .ToList().ForEach(Console.WriteLine);
                        break;
                    case 1:
                        Console.Clear();
                        listaPokemonUsuario.Where(p => p.GetTipo() == "agua").Select(p => $"{p.GetId()} - {p.GetNombre()}")
                            .ToList().ForEach(Console.WriteLine);
                        break;
                    case 2:
                        Console.Clear();
                        listaPokemonUsuario.Where(p => p.GetTipo() == "tierra").Select(p => $"{p.GetId()} - {p.GetNombre()}")
                            .ToList().ForEach(Console.WriteLine);
                        break;
                    case 3:
                        Console.Clear();
                        listaPokemonUsuario.Where(p => p.GetTipo() == "planta").Select(p => $"{p.GetId()} - {p.GetNombre()}")
                            .ToList().ForEach(Console.WriteLine);
                        break;
                    case 4:
                        salir = true;
                        break;
                }
                break;
            }
        }
        public static void Ajustes(Usuario usuarioLogeado,string rutaUsuario)
        {
            Console.Clear();
            string[] opcionesAjustes = {
            "Reproducir Música",
            "Detener Música",
            "Reiniciar partida",
            "Desbloquear todo",
            "Atrás"
            };

            int opcion = ConsolaInterfaz.SeleccionarOpcion(opcionesAjustes);

            switch (opcion)
            {
                case 0:
                    ajustesglobales.ReproducirMusica();
                    break;
                case 1:
                    ajustesglobales.DetenerMusica();
                    break;
                case 2:
                    ajustesglobales.ReiniciarPartida(usuarioLogeado);
                    break;
                case 3:
                    DesbloquearTodo(rutaUsuario);
                    break;
                case 4:
                    // Volver o salir
                    break;
            }
        }
        public static void DesbloquearTodo(string rutaUsuario)
        {
            string contrasenya;
            Console.Write("Introduce contraseña: ");
            contrasenya = Console.ReadLine();
            Console.WriteLine();

            if (contrasenya == CONTRASENYA_ADMIN)
            {
                List<Pokemon> listaPokemons = ListaPokemonCompleta(FICHERO_POKEMON);
                DateTime fechaActual = DateTime.Now;

                List<string> lineas = new List<string>();

                foreach (Pokemon p in listaPokemons)
                {
                    string linea = p.GetId() + ";" + p.GetNombre() + ";" + p.GetVida() + ";" +
                                   p.GetTipo() + ";" + p.GetAtaque1().GetNombreAtaque() + ";" +
                                   p.GetAtaque2().GetNombreAtaque() + ";" + fechaActual.ToString("dd/MM/yyyy") + ";" +
                                   p.getAsset();

                    lineas.Add(linea);
                }
                File.WriteAllLines(rutaUsuario, lineas);
                Console.WriteLine("Todos los pokemons han sido desbloqueados!!");
            }
            else
            {
                Console.WriteLine("Contraseña incorrecta!!");
            }
        }
        public static void MenuOpciones(Usuario usuarioLogeado,string rutaUsuario)
        {
            ajustesglobales.ReproducirMusica();
            string[] opcionesMenuPrincipal = {
                "Abrir sobres",
                "Album",
                "Combatir",
                "Ajustes",
                "Salir"
            };
            bool salir = false;
            while (!salir)
            {
                int opcionSeleccionada = ConsolaInterfaz.SeleccionarOpcion(opcionesMenuPrincipal);
                switch (opcionSeleccionada)
                {
                    case 0:
                        AbrirSobres(usuarioLogeado);
                        break;
                    case 1:
                        Pokedex(usuarioLogeado);
                        break;
                    case 2:
                        ajustesglobales.DetenerMusica();
                        Combate.Inicializar(usuarioLogeado);
                        ajustesglobales.ReproducirMusica();
                        break;
                    case 3:
                        Ajustes(usuarioLogeado,rutaUsuario);
                        break;
                    case 4:
                        ConsolaInterfaz.WriteLineCentr("Saliendo");
                        salir = true;
                        ajustesglobales.DetenerMusica();
                        break;
                }
                if (!salir)
                {
                    Console.WriteLine("\nPresiona cualquier tecla para volver al menú...");
                    Console.ReadKey(true);
                }
            }
        }
        public static List<Pokemon> ListaPokemonCompleta(string fichero)
        {
            List<Pokemon> pokemons = new List<Pokemon>();
            foreach (string linea in File.ReadLines(fichero))
            {
                string[] atributos = linea.Split(';');
                Pokemon pokemon = new Pokemon(Convert.ToInt32(atributos[0]), atributos[1], Convert.ToInt32(atributos[2]),
                    atributos[3], atributos[4], atributos[5],null, atributos[7]);
                pokemons.Add(pokemon);
            }
            return pokemons;
        }

        public static List<Pokemon> ListaPokemonUsuario(string rutaUsuario)
        {
            List<Pokemon> listaPokemonUsuario = new List<Pokemon>();
            foreach (string linea in File.ReadLines(rutaUsuario))
            {
                string[] atributos = linea.Split(";");
                Pokemon pokemon = new Pokemon(Convert.ToInt32(atributos[0]), atributos[1], Convert.ToInt32(atributos[2]),
                    atributos[3], atributos[4], atributos[5], DateTime.Parse(atributos[6]), atributos[7]);
                listaPokemonUsuario.Add(pokemon);
            }
            return listaPokemonUsuario; 
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ConsolaInterfaz.ColorLetras();
            string rutaUsuarios = "../../../Usuarios/UsuariosRegistrados.json";
            Usuario usuarioLogeado = Login(rutaUsuarios);
            string rutaUsuario = "../../../Usuarios/" + usuarioLogeado.NombreUsuario + ".txt";
            if (!File.Exists(rutaUsuarios))
            {
                File.WriteAllText(rutaUsuarios, "");
            }
            else 
            {
                Console.WriteLine("Esto existe");
            }
            MenuOpciones(usuarioLogeado,rutaUsuario);
        }
    }
}