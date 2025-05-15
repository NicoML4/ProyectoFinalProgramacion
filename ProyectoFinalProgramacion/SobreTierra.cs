using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class SobreTierra : Sobre
    {
        public static void AbrirSobre(Usuario usuario)
        {
            string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
            Random numRandom = new Random();
            List<Pokemon> pokemons = CargarTodosPokemons();
            List<Pokemon> tipoTierra = pokemons.Where(p => p.GetTipo() == "tierra").ToList();
            List<Pokemon> pokemonsUsuario = CargarPokemonsUsuario(rutaUsuario);
            HashSet<int> randoms = new HashSet<int>();

            while (randoms.Count < 5)
            {
                randoms.Add(numRandom.Next(0, tipoTierra.Count));
            }
            foreach (int numero in randoms)
            {
                Console.WriteLine($"Pokemon obtenido: {tipoTierra[numero].GetNombre()}");
                if (!pokemonsUsuario.Contains(tipoTierra[numero]))
                {
                    tipoTierra[numero].SetFechaObtencion(DateTime.Now);
                    pokemonsUsuario.Add(tipoTierra[numero]);
                }
                pokemonsUsuario.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
            }
            File.WriteAllLines(rutaUsuario, pokemonsUsuario.Select(p => p.ToString()).ToArray());
        }
    }
}
