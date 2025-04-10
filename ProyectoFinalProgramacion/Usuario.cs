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
    }
}
