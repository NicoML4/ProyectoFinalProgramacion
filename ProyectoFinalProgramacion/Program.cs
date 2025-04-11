using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinalProgramacion
{
    internal class Program
    {

        public static void IniciarSesion()
        {
            string nombre;
            string contrasena;

            File.ReadAllLines("");
        }
        public static void Registrarse()
        {
            string nombreUsuarioRegistro = "";
            string contrasenaRegistro = "";
            string contrasenaRegistroRepetida = "";
            bool nombreDeUsuarioExistente = false;

            string json = File.ReadAllText("usuarios.json");

            List<Usuario> usuariosRegistrados = JsonSerializer.Deserialize<List<Usuario>>(json);
            
            while (nombreUsuarioRegistro == "" || !nombreDeUsuarioExistente)
            {
                nombreDeUsuarioExistente = true;
                Console.WriteLine("Dime tu nombre de usuario");
                nombreUsuarioRegistro = Console.ReadLine();
                if (nombreUsuarioRegistro == "")
                {
                    Console.WriteLine("No has introducido nada");
                }
                foreach (Usuario usuariosLista in usuariosRegistrados)
                {
                    if (usuariosLista.NombreUsuario == nombreUsuarioRegistro)
                    {
                        Console.WriteLine("Ese nombre de usuario ya existe");
                        nombreDeUsuarioExistente = false;

                    }
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
            }

            Usuario usuarioNuevo = new Usuario(nombreUsuarioRegistro, contrasenaRegistro);

            usuariosRegistrados.Add(usuarioNuevo);

            JsonSerializerOptions opciones = new JsonSerializerOptions { WriteIndented = true };

            string datosSerializados = JsonSerializer.Serialize(usuariosRegistrados,opciones);

            File.WriteAllText("usuarios.json",datosSerializados);
        }
        public static void Login()
        {
            int eleccion;
            bool valido = false;
            while(!valido)
            { 
                Console.WriteLine("1.Iniciar Sesión \n");

                Console.WriteLine("2.Registrarse");

                eleccion = Convert.ToInt32(Console.ReadLine());

                switch (eleccion)
                {
                    case 1:
                        IniciarSesion();
                        break;

                    case 2:
                        Registrarse();
                        break;

                    default:
                        Console.WriteLine("Opcion no válida");
                        break;

                }
            }
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
                    MenuOpciones();
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
                    // listado de todos los pokemons
                    break;
                case 3:
                    // listado 
                    break;
                case 4:
                    break;
                case 5:
                    MenuOpciones();
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

        public static void Ajustes()
        {

        }

        public static void MenuOpciones()
        {
            Console.Clear();
            Console.WriteLine("1. Abrir sobres \n");
            Console.WriteLine("2. Album \n");
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
                    Ajustes();
                    break;
            }    
        }

        
        static void Main(string[] args)
        {
            Login();
            MenuOpciones();
        }
    }
}
