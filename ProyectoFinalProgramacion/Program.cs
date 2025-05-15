using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinalProgramacion
{
    internal class Program
    {
        const string FICHERO_POKEMON = "../../../Ficheros/pokemon_primera_generacion(modificado).txt";
        public static bool IniciarSesion(string rutaUsuarios, out Usuario usuarioGuardado)
        {
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
                    ConsolaInterfaz.WriteLineCentr($"Bienvenido {usuarioGuardado.NombreUsuario}!");
                    aceptado = true;
                }
                else
                {
                    ConsolaInterfaz.WriteLineCentr("El usuario o contraseña que estás introduciendo es incorrecto");
                }
            }
            catch (JsonException e)
            {
                ConsolaInterfaz.WriteLineCentr("Aun no se ha creado ningún usuario");
            }
            return aceptado;
        }
        public static void Registrarse(string rutaUsuarios)
        {
            string nombreUsuarioRegistro = "";
            string contrasenaRegistro = "";
            string contrasenaRegistroRepetida = "";
            List<Usuario> usuariosRegistrados;
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
            }
        }
        public static Usuario Login(string rutaUsuarios)
        {
            string[] opcionesLogin = {
                "Iniciar Sesión",
                "Registrarse"
            };
            int opcionSeleccionada = ConsolaInterfaz.SeleccionarOpcion(opcionesLogin);
            Usuario usuarioGuardado = null;
            if (opcionSeleccionada == 0)
            {
                IniciarSesion(rutaUsuarios, out usuarioGuardado);
            }
            else if (opcionSeleccionada == 1)
            {
                Registrarse(rutaUsuarios);
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
        public static void Pokedex()
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
            switch (opcion)
            {
                case 0:
                    break;
                case 1:
                    Console.Clear();
                    List<Pokemon> listaPokemons = ListaPokemonCompleta(FICHERO_POKEMON);
                    listaPokemons.Select(p => $"{p.GetId()} - {p.GetNombre()}").ToList().ForEach(ConsolaInterfaz.WriteLineCentr);
                    Console.ReadKey(true);
                    break;
                case 2:
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
                    SwitchTipos(opcionTipo);
                    break;
                case 4:
                    break;
            }
        }
        public static void SwitchTipos(int opcion)
        {
            bool salir = false;
            while (!salir)
            {
                switch (opcion)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        salir = true;
                        break;
                }
                break;
            }
        }
        public static void Ajustes()
        {
            Console.Clear();
            string[] opcionesAjustes = {
                "Reiniciar partida",
                "Desbloquear todo",
                "Atrás"
            };
            int opcion = ConsolaInterfaz.SeleccionarOpcion(opcionesAjustes);
            switch (opcion)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
        }
        public static void MenuOpciones(Usuario usuarioLogeado)
        {
            string[] opcionesMenuPrincipal = {
                "Abrir sobres",
                "Album",
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
                        Pokedex();
                        break;
                    case 2:
                        Ajustes();
                        break;
                    case 3:
                        ConsolaInterfaz.WriteLineCentr("Saliendo");
                        salir = true;
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
                int id = int.Parse(atributos[0]);
                string nombre = atributos[1];
                int vida = int.Parse(atributos[2]);
                string tipo = atributos[3];
                string ataque1 = atributos[4];
                string ataque2 = atributos[5];
                string asset = atributos[6];
                Pokemon pokemon = new Pokemon(id, nombre, vida, tipo, ataque1, ataque2, asset);
                pokemons.Add(pokemon);
            }
            return pokemons;
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            ConsolaInterfaz.ColorLetras();
            string rutaUsuarios = "../../../Usuarios/UsuariosRegistrados.json";
            if (!File.Exists("rutaUsuarios"))
            {
                File.WriteAllText("rutaUsuarios", "");
            }
            Usuario usuarioLogeado = Login(rutaUsuarios);
            MenuOpciones(usuarioLogeado);
        }
    }
}