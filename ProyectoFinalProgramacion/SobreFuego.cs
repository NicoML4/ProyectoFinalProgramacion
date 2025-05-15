using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class SobreFuego : Sobre
    {
        public static void AbrirSobre(Usuario usuario)
        {
            string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
            Random numRandom = new Random();
            List<Pokemon> pokemons = CargarTodosPokemons();
            List<Pokemon> tipoFuego = pokemons.Where(p => p.GetTipo() == "fuego").ToList();
            List<Pokemon> pokemonsUsuario = CargarPokemonsUsuario(rutaUsuario);
            HashSet<int> randoms = new HashSet<int>();

            while (randoms.Count < 5)
            {
                randoms.Add(numRandom.Next(0, tipoFuego.Count));
            }
            foreach (int numero in randoms)
            {
                Console.WriteLine($"Pokemon obtenido: {tipoFuego[numero].GetNombre()}");
                if (!pokemonsUsuario.Contains(tipoFuego[numero]))
                {
                    tipoFuego[numero].SetFechaObtencion(DateTime.Now);
                    pokemonsUsuario.Add(tipoFuego[numero]);
                }
                pokemonsUsuario.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
            }
            File.WriteAllLines(rutaUsuario, pokemonsUsuario.Select(p => p.ToString()).ToArray());
        }
    }
}
