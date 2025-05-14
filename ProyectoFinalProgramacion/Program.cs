using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinalProgramacion
{
    internal class Program
    {
        const string FICHERO_POKEMON = "../../../Ficheros/pokemon_primera_generacion_SIN_ATAQUES.txt";
        public static bool IniciarSesion(string rutaUsuarios,out Usuario usuarioGuardado)
        {
            string nombreUsuarioLogeado="";
            string contrasenaUsuarioLogeado="";
            string json = File.ReadAllText(rutaUsuarios);
            bool aceptado = false;

            while (nombreUsuarioLogeado == "")
            {
                Console.WriteLine("Dime tu nombre de usuario");
                nombreUsuarioLogeado = Console.ReadLine();
                if (nombreUsuarioLogeado == "")
                {
                    Console.WriteLine("No has introducido nada");
                }
            }
            while (contrasenaUsuarioLogeado == "")
            {
                Console.WriteLine("Dime tu contraseña");
                contrasenaUsuarioLogeado = Console.ReadLine();
                if (contrasenaUsuarioLogeado == "")
                {
                    Console.WriteLine("No has introducido nada");
                }
            }
            Console.Clear();
            usuarioGuardado = new Usuario(nombreUsuarioLogeado,contrasenaUsuarioLogeado);
            try
            {
                List<Usuario> usuariosGuardados = JsonSerializer.Deserialize<List<Usuario>>(json);
                if (usuariosGuardados.Contains(usuarioGuardado))
                {
                    Console.WriteLine($"Bienvenido {usuarioGuardado.NombreUsuario}!");
                    aceptado = true;
                }
                else
                {
                    Console.WriteLine("El usuario o contraseña que estás introduciendo es incorrecto");
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine("Aun no se ha creado ningún usuario");
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
                Console.WriteLine("Dime tu nombre de usuario");
                nombreUsuarioRegistro = Console.ReadLine();
                if (nombreUsuarioRegistro == "")
                {
                    Console.WriteLine("No has introducido nada");
                }
            }
            while (contrasenaRegistro == "")
            {
                Console.WriteLine("Dime tu contraseña");
                contrasenaRegistro = Console.ReadLine();
                if (contrasenaRegistro == "")
                {
                    Console.WriteLine("No has introducido nada");
                }
            }

            while (contrasenaRegistro != contrasenaRegistroRepetida)
            {
                Console.WriteLine("Repiteme la contraseña");
                contrasenaRegistroRepetida = Console.ReadLine();
                if (contrasenaRegistro != contrasenaRegistroRepetida)
                {
                    Console.WriteLine("Ambas contraseñas no coinciden");
                }
            }
            Console.Clear();
            Usuario usuarioCreado = new Usuario(nombreUsuarioRegistro, contrasenaRegistro);
            if (usuariosRegistrados.Exists(u => u.NombreUsuario == usuarioCreado.NombreUsuario))
            {
                Console.WriteLine("Ya hay un usuario con ese nombre");
            }
            else
            {
                Console.WriteLine("¡Usuario nuevo creado con éxito!");
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
            string eleccion;
            bool valido = false;
            int numero;
            Usuario usuarioGuardado = null;
            while (!valido)
            { 
                Console.WriteLine("1.Iniciar Sesión \n");
                Console.WriteLine("2.Registrarse");
                eleccion = Console.ReadLine();

                int.TryParse(eleccion, out numero);
               
                switch (numero)
                {
                    case 1:
                        valido = IniciarSesion(rutaUsuarios,out usuarioGuardado);
                        break;

                    case 2:
                        Registrarse(rutaUsuarios);
                        break;
                    case 123456789: //Comprobador de que el usuario y contraseña existen
                        string json = File.ReadAllText(rutaUsuarios);
                        try
                        {
                            List<Usuario> usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);
                            foreach (Usuario usuario in usuarios)
                            {
                                Console.WriteLine(usuario);
                            }
                        }
                        catch (JsonException e)
                        {
                            Console.WriteLine("Aun no se ha creado ningún usuario");
                        }
                        break;
                    default:
                        Console.WriteLine("Opcion no válida");
                        break;
                }
                
            }
            return usuarioGuardado;
        }
        public static void AbrirSobres()
        {
            Console.Clear();
            MenuSobres();
            Console.Write("Selecciona un sobre: ");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    //SobreFuego();
                    break;
                case 2:
                    //SobreAgua();
                    break;
                case 3:
                    //SobreTierra();
                    break;
                case 4:
                    //SobrePlanta();
                    break;
                case 5:
                    //SobreMixto();
                    break;
                case 6:
                   /* MenuOpciones();*/
                    break;
            }
        }

        public static void MenuSobres()
        {
            Console.WriteLine("1. Sobre de Fuego");
            Console.WriteLine("2. Sobre de Agua");
            Console.WriteLine("3. Sobre de Tierra");
            Console.WriteLine("4. Sobre de Planta");
            Console.WriteLine("5. Sobre Mixto");
            Console.WriteLine("6. Atrás");
        }

        public static void Pokedex()
        {
            Console.Clear();
            MenuPokedex();
            Console.Write("Introduce una opción: ") ;
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    // listado de tus pokemon
                    break;
                case 2:
                    
                    Console.Clear();
                    List<Pokemon> listaPokemons = ListaPokemonCompleta(FICHERO_POKEMON);
                    listaPokemons.Select(p => $"{p.GetId()} - {p.GetNombre()}").ToList().ForEach(Console.WriteLine);
                    break;
                case 3:
                    // listado solo de los pokemons bloqueados
                    break;
                case 4:
                    Console.Clear();
                    MenuTipos();
                    SwitchTipos();
                    break;
                case 5:
                    /*MenuOpciones();*/
                    break;
            }
        }

        public static void MenuPokedex()
        {
            Console.WriteLine("1. Mis Pokémon");
            Console.WriteLine("2. Todos");
            Console.WriteLine("3. Pokémons bloqueados");
            Console.WriteLine("4. Filtrar por tipo");
            Console.WriteLine("5. Atrás");

        }

        public static void MenuTipos()
        {
            Console.WriteLine("1. Filtrar por tipo fuego");
            Console.WriteLine("2. Filtrar por tipo agua");
            Console.WriteLine("3. Filtrar por tipo tierra");
            Console.WriteLine("4. Filtrar por tipo planta");
            Console.WriteLine("5. Atrás");
        }

        public static void SwitchTipos()
        {
            Console.Write("Introduce la opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    // linq tipo fuego
                    break;
                case 2:
                    // linq tipo agua
                    break;
                case 3:
                    // linq tipo tierra
                    break;
                case 4:
                    // linq tipo planta
                    break;
                case 5:
                    Pokedex();
                    break;
            }
        }

        public static void Ajustes()
        {
            Console.Clear();
            MenuAjustes();
            Console.Write("Introduce la opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch (opcion) 
            {
                case 1:
                    // reiniciar partida
                    break;
                case 2:
                    // desbloquear todo
                    break;
                case 3:
                    /*MenuOpciones();*/
                    break;
            }
        }

        public static void MenuAjustes()
        {
            Console.WriteLine("1. Reiniciar partida");
            Console.WriteLine("2. Desbloquear todo");
            Console.WriteLine("3. Atrás");
        }

        public static void MenuOpciones(Usuario usuarioLogeado)
        {
            Console.WriteLine($"{usuarioLogeado.NombreUsuario},{usuarioLogeado.Contrasena}");
            Console.Clear();
            Console.WriteLine("1. Abrir sobres");
            Console.WriteLine("2. Album");
            Console.WriteLine("3. Ajustes");
            Console.Write("Introduce una opción: ");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    AbrirSobres();
                    break;
                case 2:
                    Pokedex();
                    break;
                case 3:
                    Ajustes ajustes = new Ajustes();
                    ajustes.MostrarMenuAjustes();
                    break;
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


                /*Pokemon objeto = new Pokemon(id, nombre, vida, tipo);
                pokemons.Add(objeto);*/
            }
            return pokemons;
        }

        static void Main(string[] args)
        {

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
