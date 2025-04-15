using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProyectoFinalProgramacion
{
    internal class Program
    {

        public static void IniciarSesion(string rutaUsuarios)
        {
            string nombreUsuarioLogeado="";
            string contrasenaUsuarioLogeado="";

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
        }
        public static void Registrarse(string rutaUsuarios)
        {
            string nombreUsuarioRegistro = "";
            string contrasenaRegistro = "";
            string contrasenaRegistroRepetida = "";
            bool nombreDeUsuarioExistente = false;

            string json = File.ReadAllText(rutaUsuarios);

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
                else 
                {
                    foreach (Usuario usuariosLista in usuariosRegistrados)
                    {
                        if (usuariosLista.NombreUsuario == nombreUsuarioRegistro)
                        {
                            Console.WriteLine("Ese nombre de usuario ya existe");
                            nombreDeUsuarioExistente = false;

                        }
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
                if (contrasenaRegistro != contrasenaRegistroRepetida)
                {
                    Console.WriteLine("Ambas contraseñas no coinciden");
                }
            }
            Usuario usuarioNuevo = new Usuario(nombreUsuarioRegistro, contrasenaRegistro);
            usuariosRegistrados.Add(usuarioNuevo);
            JsonSerializerOptions opciones = new JsonSerializerOptions { WriteIndented = true };
            string datosSerializados = JsonSerializer.Serialize(usuariosRegistrados,opciones);
            File.WriteAllText(rutaUsuarios,datosSerializados);
        }
        public static void Login()
        {
            string rutaUsuarios = "../../../Usuarios/UsuariosRegistrados.json";
            string eleccion;
            bool valido = false;
            while(!valido)
            { 
                Console.WriteLine("1.Iniciar Sesión \n");
                Console.WriteLine("2.Registrarse");
                eleccion = Console.ReadLine();

                int.TryParse(eleccion, out int numero);
               
                switch (numero)
                {
                    case 1:
                        IniciarSesion(rutaUsuarios);
                        break;

                    case 2:
                        Registrarse(rutaUsuarios);
                        break;
                    case 123456789:
                        string json = File.ReadAllText(rutaUsuarios);

                        List<Usuario> usuarios = JsonSerializer.Deserialize<List<Usuario>>(json);

                        foreach (Usuario usuario in usuarios)
                        {
                            Console.WriteLine(usuario);
                        }
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
                    // listado solo de los pokemons bloqueados
                    break;
                case 4:
                    Console.Clear();
                    MenuTipos();
                    SwitchTipos();
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
                    MenuOpciones();
                    break;
            }
        }

        public static void MenuAjustes()
        {
            Console.WriteLine("1. Reiniciar partida");
            Console.WriteLine("2. Desbloquear todo");
            Console.WriteLine("3. Atrás");
        }

        public static void MenuOpciones()
        {
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
                    Ajustes();
                    break;
            }    
        }

        
        static void Main(string[] args)
        {
            string rutaUsuarios = "../../../Usuarios/UsuariosRegistrados.json";
            
            Login();
            MenuOpciones();
        }
    }
}
