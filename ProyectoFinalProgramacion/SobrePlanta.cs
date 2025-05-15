using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class SobrePlanta : Sobre
    {
        public static void AbrirSobre(Usuario usuario)
        {
            string rutaUsuario = "../../../Usuarios/" + usuario.NombreUsuario + ".txt";
            Random numRandom = new Random();
            List<Pokemon> pokemons = CargarTodosPokemons();
            List<Pokemon> tipoPlanta = pokemons.Where(p => p.GetTipo() == "planta").ToList();
            List<Pokemon> pokemonsUsuario = CargarPokemonsUsuario(rutaUsuario);
            HashSet<int> randoms = new HashSet<int>();

            while (randoms.Count < 5)
            {
                randoms.Add(numRandom.Next(0, tipoPlanta.Count));
            }
            foreach (int numero in randoms)
            {
                Console.WriteLine($"Pokemon obtenido: {tipoPlanta[numero].GetNombre()}");
                if (!pokemonsUsuario.Contains(tipoPlanta[numero]))
                {
                    tipoPlanta[numero].SetFechaObtencion(DateTime.Now);
                    pokemonsUsuario.Add(tipoPlanta[numero]);
                }
                pokemonsUsuario.Sort((a, b) => a.GetId().CompareTo(b.GetId()));
            }
            File.WriteAllLines(rutaUsuario, pokemonsUsuario.Select(p => p.ToString()).ToArray());
        }
    }
}

