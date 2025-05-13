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

        public int Damage()
        {
            int damage =0;
            switch (nombre)
            { 

                //TODO hay que comprobar cuales son los ataques que existen y para cada uno de ellos decir cuanta vida va a quitar y ponerle ese valor a damage
                default:
                    Console.WriteLine("Esto no deberia de estar saliendo");
                    break;
            }
            return damage;
        }
        public override string ToString()
        {
            return $"Ataque: {nombre}";
        }
    }
}
