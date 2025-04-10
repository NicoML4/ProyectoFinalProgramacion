namespace ProyectoFinalProgramacion
{
    internal class Program
    {

        public static void comprobarExistenciaUsuario()
        {
            string nombre;
            string contrasena;

            File.ReadAllLines("");
        }

        public static void login()
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
                        comprobarExistenciaUsuario();
                        break;

                    case 2:



                        break;

                    default:
                        Console.WriteLine("Opcion no válida");
                        break;

                }
            }
        }
        public static void menuOpciones()
        {
            Console.WriteLine("1. Abrir sobres");
            Console.WriteLine("2. Album");
            Console.WriteLine("3. Ajustes");
            Console.Write("Introduce una opción");
            int opcion = Convert.ToInt32(Console.ReadLine());
            switch(opcion)
            {
                case 1:
                    abrirSobres();
                    break;
                case 2:
                    Album()
                    break;
                case 3:
                    ajustes()
                    break;
            }    
        }

        
        static void Main(string[] args)
        {
            login();
            menuOpciones();
        }
    }
}
