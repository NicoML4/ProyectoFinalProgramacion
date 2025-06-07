using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Ataque
    {
        string nombre;

        public Ataque(string nombre)
        { 
            this.nombre = nombre;
        }
        public void SetNombreAtaque(string nombre)
        { 
            this.nombre = nombre;
        }
        public string GetNombreAtaque()
        {
            return nombre;
        }
        public override string ToString()
        {
            return $"Ataque: {nombre}";
        }
    }
}
