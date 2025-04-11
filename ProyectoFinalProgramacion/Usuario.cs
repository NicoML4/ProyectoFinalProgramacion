using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalProgramacion
{
    internal class Usuario
    {
        string nombreUsuario;
        string contrasena;

        public Usuario(string nombreUsuario, string contrasena)
        { 
            this.nombreUsuario = nombreUsuario;
            this.contrasena = contrasena;
        }

        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        public string Contrasena { get => contrasena; set => contrasena = value; }

        public override string ToString()
        {
            return nombreUsuario + " " + contrasena;
        }
    }
}
